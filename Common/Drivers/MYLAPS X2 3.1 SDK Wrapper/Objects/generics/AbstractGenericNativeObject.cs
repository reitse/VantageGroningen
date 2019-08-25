using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MylapsSDK.Objects.generics
{
    public class AbstractGenericNativeObject<StructType, ObjectClass, HandleClass>
        where ObjectClass : new()
    {
        protected readonly System.IntPtr _nativePointer;
        protected readonly StructType _data;
        protected HandleClass _context;

        private static ObjectClass fromNativePointerToObject(System.IntPtr pointerToNativeStruct, HandleClass handleObject)
        {
            //StructType data = (StructType)System.Runtime.InteropServices.Marshal.PtrToStructure(pointerToNativeStruct, typeof(StructType));
            return new ObjectClass();// { NativePointer = pointerToNativeStruct };
        }

        internal static System.Collections.Generic.List<ObjectClass> FromNativeDoublePointerToList(System.IntPtr pointerToNativeArray, uint count, HandleClass handleObject)
        {
            var ptrArray = new System.IntPtr[count];
            System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int)count);
            return new System.Collections.Generic.List<ObjectClass>(
                System.Array.ConvertAll<System.IntPtr, ObjectClass>(ptrArray,
                    ptr => AbstractGenericNativeObject<StructType, ObjectClass, HandleClass>.fromNativePointerToObject(ptr, handleObject)));
        }

        internal AbstractGenericNativeObject(HandleClass handle, System.IntPtr nativePointer)
        {
            _nativePointer = nativePointer;
            _data = (StructType)System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(StructType));
            _context = handle;
        }

        internal System.IntPtr NativePointer
        {
            get { return _nativePointer; }
        }

        internal HandleClass Handle
        {
            get { return _context; }
        }

        internal StructType Data
        {
            get { return _data; }
        }
    }
}
