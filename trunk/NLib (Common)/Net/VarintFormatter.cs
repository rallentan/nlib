// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.IO;

namespace NLib.Net
{
    [ComVisible(true)]
    public class VarintFormatter : SerializationStream
    {
        //--- Constructors ---

        public VarintFormatter(Stream stream)
            : base(stream)
        {
        }

        //--- Public Methods ---

        public override void Write(bool value)
        {
            if (value)
                Stream.WriteByte(0x1);
            else
                Stream.WriteByte(0x0);
        }

        public override void Write(byte value)
        {
            throw new NotImplementedException();
        }

        public override void Write(char value)
        {
            throw new NotImplementedException();
        }

        public override void Write(DateTime value)
        {
            throw new NotImplementedException();
        }

        public override void Write(decimal value)
        {
            throw new NotImplementedException();
        }

        public override void Write(double value)
        {
            throw new NotImplementedException();
        }

        public override void Write(short value)
        {
            throw new NotImplementedException();
        }

        public override void Write(int value)
        {
            value = (value << 1) ^ (value >> 31);
            Write((uint)value);
        }

        public override void Write(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(object value)
        {
            if (value is Enum)
                throw new NotImplementedException();

            var compactSerializable = value as ISerializable2;
            if (compactSerializable != null)
            {
                compactSerializable.GetObjectData(this);
            }
        }
        
        [CLSCompliant(false)]
        public override void Write(sbyte value)
        {
            throw new NotImplementedException();
        }

        public override void Write(float value)
        {
            throw new NotImplementedException();
        }
        
        [CLSCompliant(false)]
        public override void Write(ushort value)
        {
            throw new NotImplementedException();
        }
        
        [CLSCompliant(false)]
        public override void Write(uint value)
        {
            byte[] result;

            if ((value >> 7) == 0)
            {
                result = new byte[1];

                result[0] = (byte)(value & 0x7F);

                Stream.Write(result, 0, 1);
                return;
            }
            else if ((value >> 14) == 0)
            {
                result = new byte[2];

                result[0] = (byte)((value & 0x7F) | 0x80);
                value >>= 7;
                result[1] = (byte)(value & 0x7F);

                Stream.Write(result, 0, 2);
                return;
            }
            else if ((value >> 21) == 0)
            {
                result = new byte[3];

                result[0] = (byte)((value & 0x7F) | 0x80);
                value >>= 7;
                result[1] = (byte)((value & 0x7F) | 0x80);
                value >>= 7;
                result[2] = (byte)(value & 0x7F);

                Stream.Write(result, 0, 3);
                return;
            }
            else if ((value >> 28) == 0)
            {
                result = new byte[4];

                result[0] = (byte)((value & 0x7F) | 0x80);
                value >>= 7;
                result[1] = (byte)((value & 0x7F) | 0x80);
                value >>= 7;
                result[2] = (byte)((value & 0x7F) | 0x80);
                value >>= 7;
                result[3] = (byte)(value & 0x7F);

                Stream.Write(result, 0, 4);
                return;
            }
            else
            {
                result = new byte[5];

                result[0] = (byte)((value & 0x7F) | 0x80);
                value >>= 7;
                result[1] = (byte)((value & 0x7F) | 0x80);
                value >>= 7;
                result[2] = (byte)((value & 0x7F) | 0x80);
                value >>= 7;
                result[3] = (byte)((value & 0x7F) | 0x80);
                value >>= 7;
                result[4] = (byte)(value & 0x7F);

                Stream.Write(result, 0, 5);
                return;
            }
        }
        
        [CLSCompliant(false)]
        public override void Write(ulong value)
        {
            throw new NotImplementedException();
        }

        public override void Write(object value, Type type)
        {
            throw new NotImplementedException();
        }
        
        public override bool ReadBoolean()
        {
            throw new NotImplementedException();
        }

        public override byte ReadByte()
        {
            throw new NotImplementedException();
        }

        public override char ReadChar()
        {
            throw new NotImplementedException();
        }

        public override DateTime ReadDateTime()
        {
            throw new NotImplementedException();
        }

        public override decimal ReadDecimal()
        {
            throw new NotImplementedException();
        }

        public override double ReadDouble()
        {
            throw new NotImplementedException();
        }

        public override short ReadInt16()
        {
            throw new NotImplementedException();
        }

        public override int ReadInt32()
        {
            throw new NotImplementedException();
        }

        public override long ReadInt64()
        {
            throw new NotImplementedException();
        }
        
        [CLSCompliant(false)]
        public override sbyte ReadSByte()
        {
            throw new NotImplementedException();
        }

        public override float ReadSingle()
        {
            throw new NotImplementedException();
        }

        public override string ReadString()
        {
            throw new NotImplementedException();
        }
        
        [CLSCompliant(false)]
        public override ushort ReadUInt16()
        {
            throw new NotImplementedException();
        }
        
        [CLSCompliant(false)]
        public override uint ReadUInt32()
        {
            throw new NotImplementedException();
        }
        
        [CLSCompliant(false)]
        public override ulong ReadUInt64()
        {
            throw new NotImplementedException();
        }
        
        [SecuritySafeCritical]
        public override object ReadValue(Type type)
        {
            throw new NotImplementedException();
        }

        //--- Private Methods ---

        private object GetElement(out Type foundType)
        {
            throw new NotImplementedException();
        }

        private void ExpandArrays()
        {
            throw new NotImplementedException();
        }

        private int FindElement()
        {
            throw new NotImplementedException();
        }

        [ComVisible(true)]
        private object GetElementNoThrow(out Type foundType)
        {
            throw new NotImplementedException();
        }
    }
}
