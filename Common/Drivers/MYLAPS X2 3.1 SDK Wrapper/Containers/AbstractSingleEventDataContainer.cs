using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Objects;

namespace MylapsSDK.Containers
{
    public abstract class AbstractSingleEventDataContainer<EventClass>
    {
        private readonly EventData _eventData;

        internal AbstractSingleEventDataContainer(EventData eventData)
        {
            _eventData = eventData;
            RegisterNotifyFunction(_eventData);
        }

        protected abstract void RegisterNotifyFunction(EventData eventData);
        public Action<MDP_NOTIFY_TYPE, EventClass, EventData> EventHandlers;
        protected abstract EventClass FromNativePointer(IntPtr nativePointer, EventData eventData);

        protected EventData EventData
        {
            get { return _eventData; }
        }

        protected void HandleNotify(IntPtr handle, MDP_NOTIFY_TYPE nType, IntPtr nativePointer, IntPtr context)
        {
            if (handle != _eventData.Handle)
                return;

            var obj = (nativePointer == IntPtr.Zero) ? default(EventClass) : FromNativePointer(nativePointer, _eventData);

            switch (nType)
            {
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT:
                    if (obj != null)
                        Select(obj);
                    break;
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT:
                    if (obj != null)
                        Insert(obj);
                    break;
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_UPDATE:
                    if (obj != null)
                        Update(obj);
                    break;
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_DELETE:
                    if (obj != null)
                        Delete(obj);
                    break;
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_CLEAR:
                    Clear();
                    break;
            }

            if (EventHandlers != null)
                EventHandlers(nType, obj, _eventData);
        }

        internal void ClearData()
        {
            Clear();
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
