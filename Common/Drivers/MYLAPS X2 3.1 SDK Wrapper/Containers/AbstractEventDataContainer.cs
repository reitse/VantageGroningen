using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Objects;
using System.Collections.ObjectModel;

namespace MylapsSDK.Containers
{
    public abstract class AbstractEventDataContainer<EventClass> where EventClass : IObjectWithID
    {
        private readonly EventData _eventData;
        private List<EventClass> _events = new List<EventClass>();
        private bool _cache;

        public Action<MDP_NOTIFY_TYPE, List<EventClass>, EventData> EventHandlers;
        
        internal AbstractEventDataContainer(EventData eventData)
        {
            _eventData = eventData;
            _cache = false;

            RegisterNotifyFunction(_eventData);
        }

        protected abstract List<EventClass> FromNativePointerArray(IntPtr nativeArray, uint count, EventData eventData);

        /// <summary>
        /// Register the notify function. Should be implemented by subclasses.
        /// </summary>
        /// <param name="eventData">the EventData object.</param>
        protected abstract void RegisterNotifyFunction(EventData eventData);

        protected void HandleNotify(IntPtr handle, MDP_NOTIFY_TYPE nType, IntPtr nativeArray, uint count, IntPtr context)
        {
            if (handle != _eventData.Handle)
                return;

            var events = FromNativePointerArray(nativeArray, count, _eventData);

            if (_cache)
            {
                switch (nType)
                {
                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT:
                        events.ForEach(e => {
                            _events.Add(e);
                            Select(e);
                        });
                        break;
                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT:
                        events.ForEach(e => {
                            _events.Add(e);
                            Insert(e);
                        });
                        break;

                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_UPDATE:
                        foreach (var e in events)
                        {
                            int index = _events.FindIndex(eventObject => eventObject.ID == e.ID);

                            if (index != -1)
                            {
                                _events[index] = e;
                            }
                            Update(e);
                        }
                        break;

                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_DELETE:
                        events.ForEach(deletedEvent => 
                            {
                                _events.RemoveAll(e => e.ID == deletedEvent.ID);
                                Delete(deletedEvent);
                            });
                        break;

                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_CLEAR:
                        _events.Clear();
                        Clear();
                        break;
                }
            }

            if (EventHandlers != null)
            {
                EventHandlers(nType, events, _eventData);
            }
        }

        protected EventData EventData
        {
            get { return _eventData; }
        }

        protected void AddModifier(Modifier modifier)
        {
            EventData.AddModifier(modifier);
        }

        internal bool Cache
        {
            get { return _cache; }
            set
            {
                _cache = value;
                if (!_cache)
                    ClearData();
            }
        }

        internal void ClearData()
        {
            _events.Clear();
            Clear();
        }

        /// <summary>
        /// All captured event objects.
        /// </summary>
        public ReadOnlyCollection<EventClass> Data
        {
            get
            {
                return _events.AsReadOnly();
            }
        }

        /// <summary>
        /// Get the event object at position index.
        /// </summary>
        /// <param name="index">position in list to get object for.</param>
        /// <returns>the event object or null if index > Count.</returns>
        public EventClass GetAt(uint index)
        {
            if (index > _events.Count - 1)
                return default(EventClass);
            return _events[(int)index];
        }

        /// <summary>
        /// Find the event object with the given id.
        /// </summary>
        /// <param name="id">id to search for.</param>
        /// <returns>the event object with the given id, or null if not found.</returns>r
        public EventClass Find(uint id)
        {
            return _events.Find(e => e.ID == id);
        }

        protected virtual void Select(EventClass anEvent)
        {
            // empty default implementation
        }

        protected virtual void Insert(EventClass anEvent)
        {
            // empty default implementation
        }

        protected virtual void Update(EventClass anEvent)
        {
            // empty default implementation
        }

        protected virtual void Delete(EventClass anEvent)
        {
            // empty default implementation
        }

        protected virtual void Clear()
        {
            // empty default implementation
        }
    }
}
