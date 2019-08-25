using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Exceptions;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;

namespace MylapsSDK.Containers
{
    public abstract class GenericWithModifierContainer<T, M>: GenericContainer<T>
        where T : IObjectWithID
        where M : GenericModifier<T>
    {
        public GenericWithModifierContainer(MTA mta) :
            base(mta) { }

        private M AddModifier(IntPtr p)
        {
            if (p == IntPtr.Zero)
                throw new MylapsException("failed to create a TransponderGroupModifier");
            var modifier = NewModifier(p);
            MTA.AddModifier(modifier);
            return modifier;
        }

        /// <summary>
        /// Request a modifier to update a TransponderGroup
        /// </summary>
        /// <param name="transpondergroup">
        /// Instance to be updated/changed
        /// </param>
        /// <exception cref="MylapsException">
        /// Throws an exception whenever it failed to create a TransponderGroupModifier instance
        /// </exception>
        /// <remarks>
        /// To commit the changes, use MTA.CommitChanges()
        /// </remarks>
        public M Update(T obj)
        {
            var p = NativeUpdate(obj.ID);
            return AddModifier(p);
        }

        protected abstract M NewModifier(IntPtr nativePointer);
        protected abstract IntPtr NativeUpdate(uint ID);
        protected abstract IntPtr NativeInsert(uint ID);
        protected abstract bool NativeDelete(uint ID);

        /// <summary>
        /// Request a modifier to insert a new TransponderGroup
        /// </summary>
        /// <param name="id">
        /// transpondergroup ID of the new TransponderGroup
        /// </param>
        /// <exception cref="MylapsException">
        /// Throws an exception whenever it failed to create a TransponderGroupModifier instance
        /// </exception>
        /// <remarks>
        /// To commit the changes, use MTA.CommitChanges()
        /// </remarks>
        public M Insert(UInt32 id)
        {
            var p = NativeInsert(id);
            return AddModifier(p);
        }

        public bool Delete(T obj)
        {
            return NativeDelete(obj.ID);
        }
    }
}
