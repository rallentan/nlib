// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;
using System.IO;

namespace NLib.Net
{
    [ComVisible(true)]
    public interface ISerializationStream
    {
        //--- Methods ---

#pragma warning disable 3010

        void Close();

        void Flush();

        void Write(bool value);

        void Write(byte value);

        void Write(char value);

        void Write(DateTime value);

        void Write(decimal value);

        void Write(double value);

        void Write(short value);

        void Write(int value);

        void Write(long value);

        void Write(object value);
        
        [CLSCompliant(false)]
        void Write(sbyte value);

        void Write(float value);
        
        [CLSCompliant(false)]
        void Write(ushort value);
        
        [CLSCompliant(false)]
        void Write(uint value);
        
        [CLSCompliant(false)]
        void Write(ulong value);

        void Write(object value, Type type);
        
        bool ReadBoolean();
        
        byte ReadByte();
        
        char ReadChar();
        
        DateTime ReadDateTime();
        
        decimal ReadDecimal();
        
        double ReadDouble();
        
        short ReadInt16();
        
        int ReadInt32();
        
        long ReadInt64();
        
        [CLSCompliant(false)]
        sbyte ReadSByte();
        
        float ReadSingle();
        
        string ReadString();
        
        [CLSCompliant(false)]
        ushort ReadUInt16();
        
        [CLSCompliant(false)]
        uint ReadUInt32();
        
        [CLSCompliant(false)]
        ulong ReadUInt64();
        
        [SecuritySafeCritical]
        object ReadValue(Type type);

#pragma warning restore 3010
    }
}
