
/*
 * This file is a part of the Mylaps Development Platform (MDP).
 * Copyright (C) 1999-2010 Track Timing B.V.
 * All rights reserved.
 *
 * This software is confidential and proprietary information of
 * Track Timing B.V. ("Confidential Information"). You shall not
 * disclose such Confidential Information and shall use it only in
 * accordance with the terms of the license agreement you entered
 * into with Track Timing.
 */

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Exceptions;

namespace MylapsSDK.Containers
{
    public class TrackSolutionContainer 
    {
        private readonly MTA _mta;
        private readonly IntPtr _nativeHandle;

        private readonly SortedDictionary<UInt32, TrackSolutionGroup> _tracksolutionGroups = new SortedDictionary<uint, TrackSolutionGroup>();
        private readonly SortedDictionary<UInt32, TrackSolution> _tracksolutions = new SortedDictionary<UInt32, TrackSolution>();
        private readonly SortedDictionary<UInt32, SortedDictionary<UInt32, Sector>> _sectors = new SortedDictionary<UInt32, SortedDictionary<UInt32, Sector>>();
        private readonly SortedDictionary<UInt32, SortedDictionary<UInt32, Sequence>> _sequences = new SortedDictionary<UInt32, SortedDictionary<UInt32, Sequence>>();
        private readonly SortedDictionary<UInt32, SortedDictionary<UInt32, SequenceSegment>> _sequenceSegments = new SortedDictionary<UInt32, SortedDictionary<UInt32, SequenceSegment>>();
        private readonly List<Modifier> _modifiers = new List<Modifier>();

        /** keep a ref to this delegates or else it will be deleted by the GC */
        private readonly NativeNotifyMultiObjectDelegate _pfTrackSolutionGroupNotifier;
        private readonly NativeNotifyMultiObjectDelegate _pfTrackSolutionNotifier;
        private readonly NativeNotifyMultiObjectDelegate _pfSectorNotifier;
        private readonly NativeNotifyMultiObjectDelegate _pfSequenceNotifier;
        private readonly NativeNotifyMultiObjectDelegate _pfSequenceSegmentNotifier;

        public OnNotifyTrackSolutionGroupHandler NotifyTrackSolutionGroupHandlers;
        public OnNotifyTrackSolutionHandler NotifyTrackSolutionHandlers;
        public OnNotifySectorHandler NotifySectorHandlers;
        public OnNotifySequenceHandler NotifySequenceHandlers;
        public OnNotifySequenceSegmentHandler NotifySequenceSegmentHandlers;

        private bool _allTrackSolutionDataAvailable;
        private bool _allTrackSolutionGroupDataAvailable;
        private bool _allSectorDataAvailable;
        private bool _allSequenceDataAvailable;
        private bool _allSequenceSegmentDataAvailable;
        private bool _allTimingConfigurationDataAvailable;

        internal TrackSolutionContainer(MTA mta)
        {
            _mta = mta;
            _nativeHandle = mta.NativeHandle;

            _allTrackSolutionDataAvailable = false;
            _allTrackSolutionGroupDataAvailable = false;
            _allSectorDataAvailable = false;
            _allSequenceDataAvailable = false;
            _allSequenceSegmentDataAvailable = false;
            _allTimingConfigurationDataAvailable = false;

            _pfTrackSolutionGroupNotifier = NotifyTrackSolutionGroup;
            _pfTrackSolutionNotifier = NotifyTrackSolutions;
            _pfSectorNotifier = NotifySectors;
            _pfSequenceNotifier = NotifySequences;
            _pfSequenceSegmentNotifier = NotifySequenceSegments;

            RegisterNotifyDelegate();
        }

        private void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_tracksolutiongroup(_nativeHandle, _pfTrackSolutionGroupNotifier);
            NativeMethods.mta_notify_tracksolution(_nativeHandle, _pfTrackSolutionNotifier);
            NativeMethods.mta_notify_sector(_nativeHandle, _pfSectorNotifier);
            NativeMethods.mta_notify_sequence(_nativeHandle, _pfSequenceNotifier);
            NativeMethods.mta_notify_sequencesegment(_nativeHandle, _pfSequenceSegmentNotifier);
        }

        private void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_tracksolutiongroup(_nativeHandle, null);
            NativeMethods.mta_notify_tracksolution(_nativeHandle, null);
            NativeMethods.mta_notify_sector(_nativeHandle, null);
            NativeMethods.mta_notify_sequence(_nativeHandle, null);
            NativeMethods.mta_notify_sequencesegment(_nativeHandle, null);
        }

        private void NotifyTrackSolutionGroup(IntPtr handle, MDP_NOTIFY_TYPE nType, IntPtr tracksolutiongrouparray, uint count, IntPtr context)
        {
            var trackSolutionGroups = TrackSolutionGroup.FromNativePointerArray(tracksolutiongrouparray, count,
                                                                                _mta);
            switch (nType)
            {
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_UPDATE:
                    trackSolutionGroups.ForEach(tsg => _tracksolutionGroups[tsg.ID] = tsg);
                    _allTrackSolutionGroupDataAvailable = true;
                    break;
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_DELETE:
                    trackSolutionGroups.ForEach(tsg => _tracksolutionGroups.Remove(tsg.ID));
                    break;
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_CLEAR:
                    _allTrackSolutionGroupDataAvailable = false;
                    _tracksolutionGroups.Clear();
                    break;
            }

            if (NotifyTrackSolutionGroupHandlers != null)
                NotifyTrackSolutionGroupHandlers(nType, trackSolutionGroups, _mta);
        }

        private void NotifyTrackSolutions(IntPtr handle, MDP_NOTIFY_TYPE nType, IntPtr tracksolutionarray, uint count, IntPtr context)
        {
            var trackSolutions = TrackSolution.FromNativePointerArray(tracksolutionarray, count, _mta);
            
            switch (nType)
            {
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_UPDATE:
                    trackSolutions.ForEach(ts => _tracksolutions[ts.ID] = ts);
                    _allTrackSolutionDataAvailable = true;
                    break;
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_DELETE:
                    trackSolutions.ForEach(ts => _tracksolutions.Remove(ts.ID));
                    break;
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_CLEAR:
                    _allTrackSolutionDataAvailable = false;
                    _tracksolutions.Clear();
                    break;
            }
            if (NotifyTrackSolutionHandlers != null)
                NotifyTrackSolutionHandlers(nType, trackSolutions, _mta);
        }

        private void NotifySectors(IntPtr handle, MDP_NOTIFY_TYPE nType, IntPtr sectorarray, uint count, IntPtr context)
        {
            var sectors = Sector.FromNativePointerArray(sectorarray, count, _mta);
            switch (nType)
            {
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_UPDATE:
                    sectors.ForEach(sector =>
                                         {
                                             if (!_sectors.ContainsKey(sector.TrackSolutionID))
                                                 _sectors[sector.TrackSolutionID] = new SortedDictionary<uint, Sector>();
                                             _sectors[sector.TrackSolutionID][sector.ID] = sector;
                                         });
                    _allSectorDataAvailable = true;
                    break;

                case MDP_NOTIFY_TYPE.MDP_NOTIFY_DELETE:
                    sectors.ForEach(sector =>
                                         {
                                             if (_sectors.ContainsKey(sector.TrackSolutionID))
                                             {
                                                 _sectors[sector.TrackSolutionID].Remove(sector.ID);
                                                 if (_sectors[sector.TrackSolutionID].Count == 0)
                                                     _sectors.Remove(sector.TrackSolutionID);
                                             }
                                         });
                    break;
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_CLEAR:
                    _allSectorDataAvailable = false;
                    _sectors.Clear();
                    break;
            }

            if (NotifySectorHandlers != null)
            {
                NotifySectorHandlers(nType, sectors, _mta);
            }
        }


        private void NotifySequences(IntPtr handle, MDP_NOTIFY_TYPE nType, IntPtr sequencearray, uint count, IntPtr context)
        {
            var sequences = Sequence.FromNativePointerArray(sequencearray, count, _mta);
            switch (nType)
            {
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_UPDATE:
                    sequences.ForEach(sequence =>
                                         {
                                             if (!_sequences.ContainsKey(sequence.TrackSolutionID))
                                                 _sequences[sequence.TrackSolutionID] = new SortedDictionary<uint, Sequence>();
                                             _sequences[sequence.TrackSolutionID][sequence.ID] = sequence;
                                         });
                    _allSequenceDataAvailable = true;
                    break;

                case MDP_NOTIFY_TYPE.MDP_NOTIFY_DELETE:
                    sequences.ForEach(sequence =>
                                         {
                                             if (_sequences.ContainsKey(sequence.TrackSolutionID))
                                             {
                                                 _sequences[sequence.TrackSolutionID].Remove(sequence.ID);
                                                 if (_sequences[sequence.TrackSolutionID].Count == 0)
                                                     _sequences.Remove(sequence.TrackSolutionID);
                                             }
                                         });
                    break;
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_CLEAR:
                    _allSequenceDataAvailable = false;
                    _sequences.Clear();
                    break;
            }

            if (NotifySequenceHandlers != null)
                NotifySequenceHandlers(nType, sequences, _mta);
        }

        private void NotifySequenceSegments(IntPtr handle, MDP_NOTIFY_TYPE nType, IntPtr sequencsegmentearray, uint count, IntPtr context)
        {
            var sequenceSegments = SequenceSegment.FromNativePointerArray(sequencsegmentearray, count, _mta);
            switch (nType)
            {
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_UPDATE:
                    sequenceSegments.ForEach(sequenceSegment =>
                    {
                        if (!_sequenceSegments.ContainsKey(sequenceSegment.TrackSolutionID))
                            _sequenceSegments[sequenceSegment.TrackSolutionID] = new SortedDictionary<uint, SequenceSegment>();
                        _sequenceSegments[sequenceSegment.TrackSolutionID][sequenceSegment.ID] = sequenceSegment;
                    });
                    _allSequenceSegmentDataAvailable = true;
                    break;

                case MDP_NOTIFY_TYPE.MDP_NOTIFY_DELETE:
                    sequenceSegments.ForEach(sequenceSegment =>
                    {
                        if (_sequenceSegments.ContainsKey(sequenceSegment.TrackSolutionID))
                        {
                            _sequenceSegments[sequenceSegment.TrackSolutionID].Remove(sequenceSegment.ID);
                            if (_sequenceSegments[sequenceSegment.TrackSolutionID].Count == 0)
                                _sequenceSegments.Remove(sequenceSegment.TrackSolutionID);
                        }
                    });
                    break;
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_CLEAR:
                    _allSequenceSegmentDataAvailable = false;
                    _sequenceSegments.Clear();
                    break;
            }

            if (NotifySequenceSegmentHandlers != null)
                NotifySequenceSegmentHandlers(nType, sequenceSegments, _mta);
        }

        public bool IsAllTrackSolutionDataAvailable()
        {
            return _allTrackSolutionDataAvailable;
        }

        public bool IsAllTrackSolutionGroupDataAvailable()
        {
            return _allTrackSolutionGroupDataAvailable;
        }

        public bool IsAllSectorDataAvailable()
        {
            return _allSectorDataAvailable;
        }

        public bool IsAllSequenceDataAvailable()
        {
            return _allSequenceDataAvailable;
        }

        public bool IsAllSequenceSegmentDataAvailable()
        {
            return _allSequenceSegmentDataAvailable;
        }

        public bool IsAllTimingConfigurationDataAvailable()
        {
            return _allTimingConfigurationDataAvailable;
        }

        public ReadOnlyCollection<TrackSolutionGroup> TrackSolutionGroups
        {
            get
            {
                return _tracksolutionGroups.Values.ToList().AsReadOnly();
            }
        }

        public ReadOnlyCollection<TrackSolution> TrackSolutions
        {
            get
            {
                return _tracksolutions.Values.ToList().AsReadOnly();
            }
        }

        public TrackSolutionGroup FindTrackSolutionGroup(UInt32 id)
        {
            return _tracksolutionGroups.ContainsKey(id) ? _tracksolutionGroups[id] : null;
        }

        public TrackSolution FindTrackSolution(UInt32 id)
        {
            return _tracksolutions.ContainsKey(id) ? _tracksolutions[id] : null;
        }

        public TrackSolution FindTrackSolutionInUse()
        {
            foreach (TrackSolution ts in _tracksolutions.Values)
            {
                if (ts.IsInUse())
                {
                    return ts;
                }
            }

            return null;
        }

        private TrackSolutionGroupModifier AddTrackSolutionGroupModifier(IntPtr p)
        {
            if (p == IntPtr.Zero)
                return null;
            var modifier = new TrackSolutionGroupModifier(p, _mta);
            _modifiers.Add(modifier);
            return modifier;
        }

        /// <summary>
        /// Request a modifier to change a TrackSolutionGroup configuration
        /// </summary>
        /// <param name="trackSolutionGroup">
        /// Instance to be updated/changed
        /// </param>
        /// <exception cref="MylapsException">
        /// Throws an exception whenever it failes to create a TrackSolutionGroupModifier instance
        /// </exception>
        /// <remarks>
        /// To commit the changes, use related MTA.CommitChanges()
        /// </remarks>
        public TrackSolutionGroupModifier UpdateTrackSolutionGroup(TrackSolutionGroup trackSolutionGroup)
        {
            var p = NativeMethods.mta_tracksolutiongroup_update(_nativeHandle, trackSolutionGroup.ID);
            return AddTrackSolutionGroupModifier(p);
        }

        /// <summary>
        /// Request a modifier to insert a new TrackSolutionGroup
        /// </summary>
        /// <exception cref="MylapsException">
        /// Throws an exception whenever it failed to create a TrackSolutionGroupModifier instance
        /// </exception>
        /// <remarks>
        /// To commit the changes, use MTA.CommitChanges()
        /// </remarks>
        public TrackSolutionGroupModifier InsertTrackSolutionGroup(UInt32 id)
        {
            var p = NativeMethods.mta_tracksolutiongroup_insert(_nativeHandle, id);
            return AddTrackSolutionGroupModifier(p);
        }

        public bool DeleteTrackSolutionGroup(TrackSolutionGroup trackSolutionGroup)
        {
            return NativeMethods.mta_tracksolutiongroup_delete(_nativeHandle, trackSolutionGroup.ID);
        }

        private TrackSolutionModifier AddTrackSolutionModifier(IntPtr p)
        {
            if (p == IntPtr.Zero)
                return null;

            var modifier = new TrackSolutionModifier(p, _mta);
            _modifiers.Add(modifier);
            return modifier;
        }

        /// <summary>
        /// Request a modifier to change a TrackSolution configuration
        /// </summary>
        /// <param name="trackSolution">
        /// Instance to be updated/changed
        /// </param>
        /// <exception cref="MylapsException">
        /// Throws an exception whenever it failes to create a TrackSolutionModifier instance
        /// </exception>
        /// <remarks>
        /// To commit the changes, use MTA.CommitChanges()
        /// </remarks>
        public TrackSolutionModifier UpdateTrackSolution(TrackSolution trackSolution)
        {
            var p = NativeMethods.mta_tracksolution_update(_nativeHandle, trackSolution.ID);
            return AddTrackSolutionModifier(p);
        }

        /// <summary>
        /// Request a modifier to insert a new TrackSolution
        /// </summary>
        /// <exception cref="MylapsException">
        /// Throws an exception whenever it failed to create a TrackSolutionModifier instance
        /// </exception>
        /// <remarks>
        /// To commit the changes, use MTA.CommitChanges()
        /// </remarks>
        public TrackSolutionModifier InsertTrackSolution(UInt32 id)
        {
            IntPtr p = NativeMethods.mta_tracksolution_insert(_nativeHandle, id);
            return AddTrackSolutionModifier(p);
        }

        public TrackSolutionModifier InsertTrackSolution()
        {
            return InsertTrackSolution(UInt32.MaxValue);
        }

        public bool DeleteTrackSolution(TrackSolution trackSolution)
        {
            return NativeMethods.mta_tracksolution_delete(_nativeHandle, trackSolution.ID);
        }

        public ReadOnlyCollection<Sector> Sectors(TrackSolution trackSolution)
        {
            if (!_sectors.ContainsKey(trackSolution.ID))
                return new List<Sector>().AsReadOnly();

            return _sectors[trackSolution.ID].Values.ToList().AsReadOnly();
        }

        public Sector FindSector(TrackSolution trackSolution, UInt32 id)
        {
            return _sectors.ContainsKey(trackSolution.ID) && _sectors[trackSolution.ID].ContainsKey(id)
                       ? _sectors[trackSolution.ID][id]
                       : null;
        }

        public Int64 CalculateSectorLength(Sector sector)
        {
            return NativeMethods.mta_sector_get_length(_nativeHandle, sector.ID);
        }

        private SectorModifier AddSectorModifier(IntPtr p)
        {
            if (p == IntPtr.Zero)
                return null;
            
            var modifier = new SectorModifier(p, _mta);
            _modifiers.Add(modifier);
            return modifier;
        }

        /// <summary>
        /// Request a modifier to change a Sector configuration
        /// </summary>
        /// <param name="sector">
        /// Instance to be updated/changed
        /// </param>
        /// <exception cref="MylapsException">
        /// Throws an exception whenever it failes to create a SectorModifier instance
        /// </exception>
        /// <remarks>
        /// To commit the changes, use MTA.CommitChanges()
        /// </remarks>
        public SectorModifier UpdateSector(Sector sector)
        {
            if (!_sectors.ContainsKey(sector.TrackSolutionID) ||
                !_sectors[sector.TrackSolutionID].ContainsKey(sector.ID))
                throw new MylapsException("failed to create a SectorModifier, sector ID:" + sector.ID +
                                          " in tracksolution ID:" + sector.TrackSolutionID + " is unknown");

            return AddSectorModifier(NativeMethods.mta_sector_update(_nativeHandle, sector.ID));
        }

        /// <summary>
        /// Request a modifier to insert a new Sector
        /// </summary>
        /// <exception cref="MylapsException">
        /// Throws an exception whenever it failed to create a SectorModifier instance
        /// </exception>
        /// <remarks>
        /// To commit the changes, use MTA.CommitChanges()
        /// </remarks>
        public SectorModifier InsertSector(TrackSolution trackSolution, UInt32 id)
        {
            if (!_tracksolutions.ContainsKey(trackSolution.ID))
                throw new MylapsException("failed to create a SectorModifier, tracksolution ID:" + trackSolution.ID +
                                          " is unknown");
            var p = NativeMethods.mta_sector_insert(_nativeHandle, trackSolution.NativePointer, id);
            return AddSectorModifier(p);
        }

        public SectorModifier InsertSector(TrackSolution trackSolution)
        {
            return InsertSector(trackSolution, UInt32.MaxValue);
        }

        public bool DeleteSector(Sector sector)
        {
            if (!_sectors.ContainsKey(sector.TrackSolutionID) || !_sectors[sector.TrackSolutionID].ContainsKey(sector.ID))
                return false;
            
            return NativeMethods.mta_sector_delete(_nativeHandle, sector.ID);
        }

        public ReadOnlyCollection<Sequence> Sequences(TrackSolution trackSolution)
        {
            if (!_sequences.ContainsKey(trackSolution.ID))
                return new List<Sequence>().AsReadOnly();

            return _sequences[trackSolution.ID].Values.ToList().AsReadOnly();
        }

        public Sequence FindSequence(TrackSolution trackSolution, UInt32 id)
        {
            return _sequences.ContainsKey(trackSolution.ID) && _sequences[trackSolution.ID].ContainsKey(id)
                       ? _sequences[trackSolution.ID][id]
                       : null;
        }

        private SequenceModifier AddSequenceModifier(IntPtr p)
        {
            if (p == IntPtr.Zero)
                return null;
            var modifier = new SequenceModifier(p, _mta);
            _modifiers.Add(modifier);
            return modifier;
        }

        /// <summary>
        /// Request a modifier to change a Sequence configuration
        /// </summary>
        /// <param name="sequence">
        /// Instance to be updated/changed
        /// </param>
        /// <exception cref="MylapsException">
        /// Throws an exception whenever it failes to create a SequenceModifier instance
        /// </exception>
        /// <remarks>
        /// To commit the changes, use MTA.CommitChanges()
        /// </remarks>
        public SequenceModifier UpdateSequence(Sequence sequence)
        {
            if (!_sequences.ContainsKey(sequence.TrackSolutionID) ||
                !_sequences[sequence.TrackSolutionID].ContainsKey(sequence.ID))
                throw new MylapsException("failed to create a SequenceModifier, sequence ID:" + sequence.ID +
                                          " in tracksolution ID:" + sequence.TrackSolutionID + " is unknown");
            return AddSequenceModifier(NativeMethods.mta_sequence_update(_nativeHandle, sequence.ID));
        }

        /// <summary>
        /// Request a modifier to insert a new Sequence
        /// </summary>
        /// <exception cref="MylapsException">
        /// Throws an exception whenever it failed to create a SequenceModifier instance
        /// </exception>
        /// <remarks>
        /// To commit the changes, use MTA.CommitChanges()
        /// </remarks>
        public SequenceModifier InsertSequence(TrackSolution trackSolution, UInt32 id)
        {
            if (!_tracksolutions.ContainsKey(trackSolution.ID))
                throw new MylapsException("failed to create a SequenceModifier, tracksolution ID:" + trackSolution.ID + " is unknown");

            return AddSequenceModifier(NativeMethods.mta_sequence_insert(_nativeHandle, trackSolution.NativePointer, id));
        }

        public SequenceModifier InsertSequence(TrackSolution trackSolution)
        {
            return InsertSequence(trackSolution, UInt32.MaxValue);
        }

        public bool DeleteSequence(Sequence sequence)
        {
            if (_sequences.ContainsKey(sequence.TrackSolutionID) && _sequences[sequence.TrackSolutionID].ContainsKey(sequence.ID))
                return NativeMethods.mta_sequence_delete(_nativeHandle, sequence.ID);
            return false;
        }

        public ReadOnlyCollection<SequenceSegment> SequenceSegments(TrackSolution trackSolution)
        {
            if (!_sequenceSegments.ContainsKey(trackSolution.ID))
                return new List<SequenceSegment>().AsReadOnly();
            return _sequenceSegments[trackSolution.ID].Values.ToList().AsReadOnly();
        }

        public SequenceSegment FindSequenceSegment(TrackSolution trackSolution, UInt32 id)
        {
            return _sequenceSegments.ContainsKey(trackSolution.ID) &&
                   _sequenceSegments[trackSolution.ID].ContainsKey(id)
                       ? _sequenceSegments[trackSolution.ID][id]
                       : null;
        }

        public ReadOnlyCollection<SequenceSegment> FindSequenceSegmentsBySequence(TrackSolution trackSolution, Sequence sequence)
        {
            if (_sequenceSegments.ContainsKey(trackSolution.ID))
            {
                List<SequenceSegment> result = new List<SequenceSegment>();
                foreach (SequenceSegment seqSeg in _sequenceSegments[trackSolution.ID].Values.ToList())
                {
                    if (seqSeg.SequenceID == sequence.ID)
                    {
                        result.Add(seqSeg);
                    }
                }
                return result.AsReadOnly();
            }
            else
            {
                return new List<SequenceSegment>().AsReadOnly();
            }
        }

        private SequenceSegmentModifier AddSequenceSegmentModifier(IntPtr p)
        {
            if (p == IntPtr.Zero)
                return null;
            var modifier = new SequenceSegmentModifier(p, _mta);
            _modifiers.Add(modifier);
            return modifier;
        }

        /// <summary>
        /// Request a modifier to change a SequenceSegment configuration
        /// </summary>
        /// <param name="sequencesegment">
        /// Instance to be updated/changed
        /// </param>
        /// <exception cref="MylapsException">
        /// Throws an exception whenever it failes to create a SequenceSegmentModifier instance
        /// </exception>
        /// <remarks>
        /// To commit the changes, use MTA.CommitChanges()
        /// </remarks>
        public SequenceSegmentModifier UpdateSequenceSegment(SequenceSegment sequencesegment)
        {
            if (_sequenceSegments.ContainsKey(sequencesegment.TrackSolutionID) && 
                _sequenceSegments[sequencesegment.TrackSolutionID].ContainsKey(sequencesegment.ID))
            {
                var p = NativeMethods.mta_sequencesegment_update(_nativeHandle, sequencesegment.ID);
                return AddSequenceSegmentModifier(p);
            }
            throw new MylapsException("failed to create a SequenceSegmentModifier, sequencesegment ID:" +
                                      sequencesegment.ID + " in tracksolution ID:" + sequencesegment.TrackSolutionID +
                                      " is unknown");
        }

        /// <summary>
        /// Request a modifier to insert a new SequenceSegment
        /// </summary>
        /// <exception cref="MylapsException">
        /// Throws an exception whenever it failed to create a SequenceSegmentModifier instance
        /// </exception>
        /// <remarks>
        /// To commit the changes, use MTA.CommitChanges()
        /// </remarks>
        public SequenceSegmentModifier InsertSequenceSegment(TrackSolution trackSolution, uint id)
        {
            if (_tracksolutions.ContainsKey(trackSolution.ID))
                return
                    AddSequenceSegmentModifier(NativeMethods.mta_sequencesegment_insert(_nativeHandle,
                                                                                        trackSolution.NativePointer, id));
            throw new MylapsException("failed to create a SequenceSegmentModifier, tracksolution ID:" + trackSolution.ID +
                                      " is unknown");
        }

        public SequenceSegmentModifier InsertSequenceSegment(TrackSolution trackSolution)
        {
            return InsertSequenceSegment(trackSolution, UInt32.MaxValue);
        }

        public bool DeleteSequenceSegment(SequenceSegment sequencesegment)
        {
            if (_sequenceSegments.ContainsKey(sequencesegment.TrackSolutionID) && 
                    _sequenceSegments[sequencesegment.TrackSolutionID].ContainsKey(sequencesegment.ID))
                return NativeMethods.mta_sequencesegment_delete(_nativeHandle, sequencesegment.ID);
            return false;
        }

        internal void MarkChangesAsApplied()
        {
            _modifiers.ForEach(m => m.MarkApplied());
        }

        internal void ClearChanges()
        {
            MarkChangesAsApplied();
            _modifiers.Clear();
        }

        private void ClearData()
        {
            _allTrackSolutionDataAvailable = false;
            _allTrackSolutionGroupDataAvailable = false;
            _allSectorDataAvailable = false;
            _allSequenceDataAvailable = false;
            _allSequenceSegmentDataAvailable = false;
            _allTimingConfigurationDataAvailable = false;

            _tracksolutions.Clear();
            _sectors.Clear();
            _sequences.Clear();
            _sequenceSegments.Clear();
        }

        private void ClearNotifiers()
        {
            NotifyTrackSolutionHandlers = null;
            NotifySectorHandlers = null;
            NotifySequenceHandlers = null;
            NotifySequenceSegmentHandlers = null;
        }

        internal void Clear()
        {
            UnRegisterNotifyDelegate();
            ClearChanges();
            ClearData();
            ClearNotifiers();
        }
    }
}
