using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace NLib.Net
{
    [ComVisible(true)]
    public interface ISerializable2
    {
        void GetObjectData(ISerializationStream info);
    }
}
