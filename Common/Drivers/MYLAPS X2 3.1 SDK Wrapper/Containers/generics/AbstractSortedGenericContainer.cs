using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Objects;

namespace MylapsSDK.Containers
{
    abstract public class AbstractSortedGenericContainer<ObjectClass, TypeKeyOfObjectClass, HandleWrapperClass>
    {
        protected abstract void RegisterNotifyDelegate();
        protected abstract void UnRegisterNotifyDelegate();
        protected abstract TypeKeyOfObjectClass GetKeyOfObject(ObjectClass obj);

        public delegate List<ObjectClass> FromNativePointerArrayDelegate(IntPtr nativeArray, uint count, HandleWrapperClass handleObject);

        private readonly HandleWrapperClass _handleWrapper;
        private readonly IntPtr _nativeHandle;

        private readonly FromNativePointerArrayDelegate _fromNativePointerArrayDelegate;

        private SortedList<TypeKeyOfObjectClass, ObjectClass> _objects = new SortedList<TypeKeyOfObjectClass, ObjectClass>();
        private bool _allDataAvailable;
        private bool _cache;

        private NativeNotifyMultiObjectDelegate _defaultNativeNotifyMultiObjectDelegate; // keep a ref to this delegates or else it will be deleted by the GC
        public Action<MDP_NOTIFY_TYPE, List<ObjectClass>, HandleWrapperClass> NotifyHandlers;

        internal AbstractSortedGenericContainer(HandleWrapperClass handleObject, IntPtr nativeHandle, FromNativePointerArrayDelegate fromNativePointerArrayDelegate, bool cacheObjects)
        {
            _handleWrapper = handleObject;
            _nativeHandle = nativeHandle;
            _fromNativePointerArrayDelegate = fromNativePointerArrayDelegate;
            _defaultNativeNotifyMultiObjectDelegate = DefaultNotifyMultiObjectHandler;
            this.RegisterNotifyDelegate();
            _cache = cacheObjects;
        }

        protected IntPtr NativeHandle
        {
            get { return _nativeHandle; }
        }

        public HandleWrapperClass Handle
        {
            get { return _handleWrapper; }
        }

        protected NativeNotifyMultiObjectDelegate DefaultNativeNotifyMultiObjectDelegate
        {
            get { return _defaultNativeNotifyMultiObjectDelegate; }
        }

        private void DefaultNotifyMultiObjectHandler(IntPtr handle, MDP_NOTIFY_TYPE nType, IntPtr nativeArray, uint count, IntPtr context)
        {
            Debug.Assert(_defaultNativeNotifyMultiObjectDelegate != null);

            if (_nativeHandle != handle)
                return;

            var objects = _fromNativePointerArrayDelegate(nativeArray, count, _handleWrapper);

            if (_cache)
            {
                switch (nType)
                {
                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT:
                        objects.ForEach(obj => {
                            _objects[this.GetKeyOfObject(obj)] = obj;
                            this.HandleSelect(obj);
                        });
                        _allDataAvailable = true;
                        break;

                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT:
                        objects.ForEach(obj => {
                            _objects[this.GetKeyOfObject(obj)] = obj;
                            this.HandleInsert(obj);
                        });
                        _allDataAvailable = true;
                        break;

                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_UPDATE:
                        objects.ForEach(obj => {
                            _objects[this.GetKeyOfObject(obj)] = obj;
                            this.HandleUpdate(obj);
                        });
                        break;

                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_DELETE:
                        objects.ForEach(obj => {
                            _objects.Remove(this.GetKeyOfObject(obj));
                            this.HandleDelete(obj);
                        });
                        break;

                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_CLEAR:
                        ClearData();
                        this.HandleClear();
                        break;
                }
            }

            if (NotifyHandlers != null)
            {
                NotifyHandlers(nType, objects, _handleWrapper);
            }
        }

        internal bool Cache
        {
            get { return _cache; }
            set
            {
                _cache = value;
                if (!_cache)
                {
                    ClearData();
                }
            }
        }

        /// <summary>
        /// Get the event object at position index.
        /// </summary>
        /// <param name="index">position in list to get object for.</param>
        /// <returns>the event object or null if index > Count.</returns>
        public ObjectClass GetAt(TypeKeyOfObjectClass key)
        {
            if (!_objects.ContainsKey(key))
            {
                return default(ObjectClass);
            }
            else
            {
                return _objects[key];
            }
        }

        public ReadOnlyCollection<ObjectClass> All
        {
            get
            {
                return _objects.Values.ToList().AsReadOnly();
            }
        }

        public ObjectClass Find(TypeKeyOfObjectClass key)
        {
            ObjectClass value;
            _objects.TryGetValue(key, out value);
            return value;
        }

        public bool AllDataAvailable
        {
            get { return _allDataAvailable; }
        }

        protected virtual void ClearData()
        {
            _allDataAvailable = false;
            _objects.Clear();
        }

        protected virtual void ClearNotifiers()
        {
            NotifyHandlers = null;
        }

        internal virtual void Clear()
        {
            UnRegisterNotifyDelegate();
            ClearData();
            ClearNotifiers();
        }

        protected virtual void HandleSelect(ObjectClass obj)
        {
            // empty default implementation
        }

        protected virtual void HandleInsert(ObjectClass obj)
        {
            // empty default implementation
        }

        protected virtual void HandleUpdate(ObjectClass obj)
        {
            // empty default implementation
        }

        protected virtual void HandleDelete(ObjectClass obj)
        {
            // empty default implementation
        }

        protected virtual void HandleClear()
        {
            // empty default implementation
        }
    }
}