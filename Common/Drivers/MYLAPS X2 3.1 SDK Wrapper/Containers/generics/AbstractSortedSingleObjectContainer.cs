using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MylapsSDK.MylapsSDKLibrary;

namespace MylapsSDK.Containers.generics
{
    abstract public class AbstractSortedSingleObjectContainer<ObjectClass, TypeKeyOfObjectClass, HandleWrapperClass>
    {
        protected abstract void RegisterNotifyDelegate();
        protected abstract void UnRegisterNotifyDelegate();
        protected abstract TypeKeyOfObjectClass GetKeyOfObject(ObjectClass obj);

        public delegate ObjectClass FromNativePointerDelegate(IntPtr nativePtrToStruct, HandleWrapperClass handleObject);

        private readonly HandleWrapperClass _handleWrapper;
        private readonly IntPtr _nativeHandle;

        private readonly FromNativePointerDelegate _fromNativePointerDelegate;

        private SortedList<TypeKeyOfObjectClass, ObjectClass> _objects = new SortedList<TypeKeyOfObjectClass, ObjectClass>();
        private bool _allDataAvailable;
        private bool _cache;

        private NativeNotifySingleObjectDelegate _defaultNativeNotifySingleObjectDelegate; // keep a ref to this delegates or else it will be deleted by the GC
        public Action<MDP_NOTIFY_TYPE, ObjectClass, HandleWrapperClass> NotifyHandlers;

        internal AbstractSortedSingleObjectContainer(HandleWrapperClass handleObject, IntPtr nativeHandle, FromNativePointerDelegate fromNativePointerDelegate, bool cacheObjects)
        {
            _handleWrapper = handleObject;
            _nativeHandle = nativeHandle;
            _fromNativePointerDelegate = fromNativePointerDelegate;
            _defaultNativeNotifySingleObjectDelegate = DefaultNotifySingleObjectHandler;
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

        protected NativeNotifySingleObjectDelegate NativeNotifySingleObjectDelegate
        {
            get { return _defaultNativeNotifySingleObjectDelegate; }
        }

        private void DefaultNotifySingleObjectHandler(System.IntPtr handle, MDP_NOTIFY_TYPE nType, System.IntPtr ptrToStruct, System.IntPtr context)
        {
            Debug.Assert(_defaultNativeNotifySingleObjectDelegate != null);

            if (_nativeHandle != handle)
                return;

            var obj = _fromNativePointerDelegate(ptrToStruct, _handleWrapper);

            if (_cache)
            {
                switch (nType)
                {
                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT:
                        _objects[this.GetKeyOfObject(obj)] = obj;
                        this.HandleSelect(obj);
                        _allDataAvailable = true;
                        break;
                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT:
                        _objects[this.GetKeyOfObject(obj)] = obj;
                        this.HandleInsert(obj);
                        _allDataAvailable = true;
                        break;
                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_UPDATE:
                        _objects[this.GetKeyOfObject(obj)] = obj;
                        this.HandleUpdate(obj);
                        break;
                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_DELETE:
                        _objects.Remove(this.GetKeyOfObject(obj));
                        this.HandleDelete(obj);
                        break;
                    case MDP_NOTIFY_TYPE.MDP_NOTIFY_CLEAR:
                        Clear();
                        break;
                }
            }

            if (NotifyHandlers != null)
                NotifyHandlers(nType, obj, _handleWrapper);
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
