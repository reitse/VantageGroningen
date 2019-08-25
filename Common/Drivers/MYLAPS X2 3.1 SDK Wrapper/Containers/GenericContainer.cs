using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using MylapsSDK.Exceptions;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Objects;

namespace MylapsSDK.Containers
{
    abstract public class GenericContainer<T>
        where T : IObjectWithID
    {
        private readonly MTA _mta;
        private readonly SortedList<UInt32, T> _objects = new SortedList<UInt32, T>();
        private bool _allDataAvailable;

        public OnNotifyObjects<T> NotifyHandlers = null;

        public GenericContainer(MTA mta)
        {
            _mta = mta;
            
            RegisterNotifyFunction();
        }

        protected MTA MTA
        {
            get { return _mta; }
        }

        protected abstract List<T> FromNativePointerArray(IntPtr array, uint count);
        protected abstract void RegisterNotifyFunction();

        protected void HandleNotify(IntPtr handle, MDP_NOTIFY_TYPE nType, IntPtr array, uint count, IntPtr context)
        {
            var notifyObjects = FromNativePointerArray(array, count);

            switch (nType)
            {
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT:
                    notifyObjects.ForEach(obj => _objects.Add(obj.ID, obj));
                    _allDataAvailable = true;
                    break;

                case MDP_NOTIFY_TYPE.MDP_NOTIFY_UPDATE:
                    notifyObjects.ForEach(obj => _objects[obj.ID] = obj);
                    break;

                case MDP_NOTIFY_TYPE.MDP_NOTIFY_DELETE:
                    notifyObjects.ForEach(obj => _objects.Remove(obj.ID));
                    break;

                case MDP_NOTIFY_TYPE.MDP_NOTIFY_CLEAR:
                    _allDataAvailable = false;
                    _objects.Clear();
                    break;
            }

            if (NotifyHandlers != null)
                NotifyHandlers(nType, notifyObjects, _mta);
        }

        public ReadOnlyCollection<T> All
        {
            get
            {
                return _objects.Values.ToList().AsReadOnly();
            }
        }

        public T Find(UInt32 id)
        {
            T value;
            _objects.TryGetValue(id, out value);
            return value;
        }

        public bool AllDataAvailable
        {
            get { return _allDataAvailable; }
        }

        public void ClearData()
        {
            _objects.Clear();
        }

        public void ClearNotifiers()
        {
            NotifyHandlers = null;
        }
    }   
}
