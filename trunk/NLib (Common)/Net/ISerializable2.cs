// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

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
