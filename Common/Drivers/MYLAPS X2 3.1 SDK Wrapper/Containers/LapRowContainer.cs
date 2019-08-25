using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Utilities;

namespace MylapsSDK.Containers
{
    public class LapRowContainer : AbstractSortedGenericContainer<LapRow, UInt32, TableData> 
    {
        private SortedDictionary<UInt32, LapRow> _latestLapRows = new SortedDictionary<UInt32, LapRow>();
        private SortedDictionary<UInt32, LapSectorCell[]> _lapSectorCells = new SortedDictionary<UInt32, LapSectorCell[]>();
        private readonly NativeMethods.pfNotifyLapSectorCell _pfNotifyLapSectorCellDelegate;

        public Action<MDP_NOTIFY_TYPE, LapRow, LapSectorCell, UInt32, TableData> NotifyLapSectorCellHandlers;

        internal LapRowContainer(TableData tableDataHandleWrapper)
            : base(tableDataHandleWrapper, tableDataHandleWrapper.NativeHandle, LapRow.FromNativePointerArray, true)
        {
            _pfNotifyLapSectorCellDelegate = HandleNotifyLapSectorCell;
        }

        protected override UInt32 GetKeyOfObject(LapRow obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_laprow(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
            NativeMethods.mta_notify_lapsectorcell(base.NativeHandle, _pfNotifyLapSectorCellDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_laprow(base.NativeHandle, null);
            NativeMethods.mta_notify_lapsectorcell(base.NativeHandle, null);
        }

        private void HandleNotifyLapSectorCell(IntPtr tableDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr laprow, uint cellindex, System.IntPtr sequencesegmentcell, System.IntPtr context)
        {
            var row = new LapRow(laprow, base.Handle);
            var cell = new LapSectorCell(sequencesegmentcell, base.Handle);

            if (base.Cache)
            {
                switch (nType)
                {
                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_UPDATE:
                        _lapSectorCells[row.ID][cellindex] = cell;
                        break;
                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_CLEAR:
                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT:
                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT:
                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_DELETE:
                        /* not handled */
                        break;
                }
            }

            if (NotifyLapSectorCellHandlers != null)
                NotifyLapSectorCellHandlers(nType, row, cell, cellindex, base.Handle);
        }

        private void InsertLapSectorCellsForLapRow(LapRow row)
        {
            IntPtr ptrIterator = IntPtr.Zero;
            UInt32 count = NativeMethods.mta_laprow_lapsectorcell_get_count(base.NativeHandle, row.ID);
            if (count > 0) {
                _lapSectorCells[row.ID] = new LapSectorCell[count];
                for (UInt32 i = 0; i < count; i++)
                {
                    ptrIterator = NativeMethods.mta_laprow_lapsectorcell_get_at(base.NativeHandle, row.ID, i);
                    _lapSectorCells[row.ID][i] = new LapSectorCell(ptrIterator, base.Handle);
                }
            }
        }

        public ReadOnlyCollection<LapSectorCell> LapSectorCells(LapRow row)
        {
            if (!_lapSectorCells.ContainsKey(row.ID))
                return new List<LapSectorCell>().AsReadOnly();

            return _lapSectorCells[row.ID].ToList().AsReadOnly();
        }

        public UInt32 FindIndexForLapSectorCell(LapSectorCell lapSectorCell)
        {
            return NativeMethods.mta_laprow_lapsectorcell_find_index(base.NativeHandle, lapSectorCell.LapSectorID);
        }

        public double GetSpeedOfLapSectorCellAsKMH(LapRow lapRow, UInt32 indexOfLapSectorCell, Int32 decimals)
        {
            return NativeMethods.mta_laprow_lapsectorcell_get_speed(base.NativeHandle, lapRow.ID, indexOfLapSectorCell, SDKHelperFunctions.SPEED_SCALE_KMH, decimals);
        }

        public double GetSpeedOfLapSectorCellAsMPH(LapRow lapRow, UInt32 indexOfLapSectorCell, Int32 decimals)
        {
            return NativeMethods.mta_laprow_lapsectorcell_get_speed(base.NativeHandle, lapRow.ID, indexOfLapSectorCell, SDKHelperFunctions.SPEED_SCALE_MPH, decimals);
        }

        public Tuple<LapRow, LapSectorCell> GetBestLapSectorCellByIndexForUserCompetitorID(UInt32 userCompetitorID, UInt32 indexOfLapSectorCell)
        {
            Tuple<LapRow, LapSectorCell> result = null;
            UInt32 rowID = 0;
            IntPtr ptrToLapSectorCell = NativeMethods.mta_laprow_lapsectorcell_get_best(base.NativeHandle, userCompetitorID, indexOfLapSectorCell, rowID);

            if (ptrToLapSectorCell != IntPtr.Zero)
            {
                result = new Tuple<LapRow, LapSectorCell>(base.Find(rowID), new LapSectorCell(ptrToLapSectorCell, base.Handle));
            }

            return result;
        }

        public Tuple<LapRow, LapSectorCell> GetBestLapSectorCellByIndexForUserCompetitor(UserCompetitor userCompetitor, UInt32 indexOfLapSectorCell)
        {
            return this.GetBestLapSectorCellByIndexForUserCompetitorID(userCompetitor.ID, indexOfLapSectorCell);
        }

        public Tuple<LapRow, LapSectorCell> GetOverallBestLapSectorCellByIndex(UInt32 indexOfLapSectorCell)
        {
            Tuple<LapRow, LapSectorCell> result = null;
            UInt32 rowID = 0;
            IntPtr ptrToLapSectorCell = NativeMethods.mta_laprow_lapsectorcell_get_best(base.NativeHandle, UInt32.MaxValue, indexOfLapSectorCell, rowID);

            if (ptrToLapSectorCell != IntPtr.Zero)
            {
                result = new Tuple<LapRow, LapSectorCell>(base.Find(rowID), new LapSectorCell(ptrToLapSectorCell, base.Handle));
            }

            return result;
        }

        public LapRow GetBestLaprowForUserCompetitorID(UInt32 userCompetitorID)
        {
            LapRow result = null;
            IntPtr ptrToLapRow = NativeMethods.mta_laprow_get_best(base.NativeHandle, userCompetitorID);

            if (ptrToLapRow != IntPtr.Zero)
            {
                result = new LapRow(ptrToLapRow, base.Handle);
            }

            return result;
        }

        public LapRow GetBestLaprowForUserCompetitor(UserCompetitor userCompetitor)
        {
            return this.GetBestLaprowForUserCompetitorID(userCompetitor.ID);
        }

        public LapRow GetReferenceForLapRow(LapRow laprow)
        {
            LapRow result = null;
            IntPtr ptrToRefLapRow = NativeMethods.mta_laprow_get_reference(base.NativeHandle, laprow.ID);

            if (ptrToRefLapRow != IntPtr.Zero)
            {
                result = new LapRow(ptrToRefLapRow, base.Handle);
            }

            return result;
        }

        public Boolean SetLapRowAsReference(LapRow laprow)
        {
            return NativeMethods.mta_laprow_set_reference(base.NativeHandle, laprow.ID); ;
        }

        protected override void HandleSelect(LapRow lapRow)
        {
            _latestLapRows[lapRow.UserCompetitorID] = lapRow;
            this.InsertLapSectorCellsForLapRow(lapRow);
        }

        protected override void HandleInsert(LapRow lapRow)
        {
            _latestLapRows[lapRow.UserCompetitorID] = lapRow;
            this.InsertLapSectorCellsForLapRow(lapRow);
        }

        protected override void HandleUpdate(LapRow lapRow)
        {
            _latestLapRows[lapRow.UserCompetitorID] = lapRow;
            this.InsertLapSectorCellsForLapRow(lapRow);
        }

        protected override void HandleDelete(LapRow lapRow)
        {
            _latestLapRows.Remove(lapRow.UserCompetitorID);
            _lapSectorCells.Remove(lapRow.ID);
        }

        protected override void ClearData()
        {
            base.ClearData();
            _latestLapRows.Clear();
            _lapSectorCells.Clear();
        }

        protected override void ClearNotifiers()
        {
            base.ClearNotifiers();
            NotifyLapSectorCellHandlers = null;
        }
    }
}
