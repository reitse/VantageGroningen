using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Objects;

namespace MylapsSDK.Containers.Generics
{
    abstract public class AbstractSortedGenericWithModifierContainer<ObjectClass, TypeKeyOfObjectClass, ModifyObjectClass, HandleWrapperClass> : AbstractSortedGenericContainer<ObjectClass, TypeKeyOfObjectClass, HandleWrapperClass>
        where ModifyObjectClass : GenericModifier<ObjectClass>
    {
        private List<ModifyObjectClass> _activeModifiers = new List<ModifyObjectClass>();

        internal AbstractSortedGenericWithModifierContainer(HandleWrapperClass handleObject, IntPtr nativeHandle, FromNativePointerArrayDelegate fromNativePointerArrayDelegate, bool cacheObjects)
            : base(handleObject, nativeHandle, fromNativePointerArrayDelegate, cacheObjects)
        {
        }

        protected abstract ModifyObjectClass NewModifier(IntPtr nativePointer);
        protected abstract IntPtr NativeUpdate(TypeKeyOfObjectClass ID);
        protected abstract IntPtr NativeInsert(TypeKeyOfObjectClass ID);
        protected abstract bool NativeDelete(TypeKeyOfObjectClass ID);

        private ModifyObjectClass AddModifier(IntPtr p)
        {
            if (p == IntPtr.Zero)
                return null;

            var modifier = NewModifier(p);
            _activeModifiers.Add(modifier);
            return modifier;
        }

        /// <summary>
        /// Request a modifier to update a TransponderGroup
        /// </summary>
        /// <param name="transpondergroup">
        /// Instance to be updated/changed
        /// </param>
        /// <returns>a Modifier, or null if unable to get update for object.</returns>
        /// <remarks>
        /// To commit the changes, use MTA.CommitChanges()
        /// </remarks>
        public ModifyObjectClass Update(ObjectClass obj)
        {
            IntPtr p = NativeUpdate( GetKeyOfObject(obj) );
            return AddModifier(p);
        }

        /// <summary>
        /// Request a modifier to insert a new TransponderGroup
        /// </summary>
        /// <param name="id">
        /// transpondergroup ID of the new TransponderGroup
        /// </param>
        /// <returns>A Modifier, or null if unable to insert object.</returns>
        /// <remarks>
        /// object type specific method (with or without id)
        /// To commit the changes, use MTA.CommitChanges()
        /// </remarks>
        protected ModifyObjectClass InternalInsert(TypeKeyOfObjectClass id)
        {
            IntPtr p = NativeInsert(id);
            return AddModifier(p);
        }

        public bool Delete(ObjectClass obj)
        {
            return NativeDelete( GetKeyOfObject(obj) );
        }

        internal void MarkChangesAsApplied()
        {
            foreach (Modifier m in _activeModifiers)
            {
                m.MarkApplied();
            }
        }

        internal void ClearChanges()
        {
            _activeModifiers.Clear();
        }

        protected override void ClearData()
        {
            base.ClearData();
            ClearChanges();
        }

        internal override void Clear()
        {
            base.Clear();
            ClearChanges();
        }
    }
}
