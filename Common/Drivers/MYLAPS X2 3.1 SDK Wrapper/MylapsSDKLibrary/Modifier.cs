using System;
using MylapsSDK.Exceptions;

namespace MylapsSDK.MylapsSDKLibrary
{
    public class Modifier
    {
        private bool _applied = false;

        public Modifier(IntPtr modifyHandle)
        {
            ModifyHandle = modifyHandle;
        }

        /** Mark this modifier as _applied. */
        public void MarkApplied()
        {
            _applied = true;
        }

        protected void CheckApplied()
        {
            if (_applied)
            {
                throw new MylapsException("Modifier already applied or cleared.");
            }
        }

        public IntPtr ModifyHandle { get; private set; }

        public override bool Equals(object obj)
        {
            // If parameter is null return false:
            if (obj == null)
                return false;
            
            // If parameter cannot be cast return false.
            var m = obj as Modifier;
            if ((System.Object)m == null)
            {
                return false;
            }

            // Return true if the field match:
            return (ModifyHandle == m.ModifyHandle);
        }

        public override int GetHashCode()
        {
            return ModifyHandle.GetHashCode();
        }
    }

    public class GenericModifier<T> : Modifier
    {
        public GenericModifier(IntPtr modifyHandle) :
            base(modifyHandle) { }
    }
}
