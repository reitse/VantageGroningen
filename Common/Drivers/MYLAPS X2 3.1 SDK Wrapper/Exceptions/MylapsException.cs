using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MylapsSDK.Exceptions
{
    public class MylapsException : ApplicationException
    {
        public MylapsException(string msg) :
            base(msg)
        {
        }
    }
}
