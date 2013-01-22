// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace NLib.Net
{
    public abstract class SerializationStream : ISerializationStream
    {
        //--- Fields ---
        Stream _stream;

        //--- Constructors ---

        public SerializationStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            _stream = stream;
        }

        //--- Public Static Methods ---

        public static implicit operator Stream(SerializationStream serializationStream)
        {
            return serializationStream._stream;
        }

        //--- Public Methods ---

        public virtual void Close()
        {
            _stream.Close();
        }

        public virtual void Flush()
        {
            _stream.Flush();
        }

        public virtual void Write(bool value)
        {
            if (value)
                _stream.WriteByte(0x1);
            else
                _stream.WriteByte(0x0);
        }

        public virtual void Write(byte value)
        {
            _stream.WriteByte(value);
        }

        public virtual void Write(char value)
        {
            _stream.WriteByte((byte)(value & 0xFF));
            _stream.WriteByte((byte)(value >> 8));
        }

        public virtual void Write(DateTime value)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(decimal value)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(double value)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(short value)
        {
            _stream.WriteByte((byte)((ushort)value & 0xFF));
            _stream.WriteByte((byte)((ushort)value >> 8));
        }

        public virtual void Write(int value)
        {
            _stream.WriteByte((byte)((uint)value & 0xFF));
            _stream.WriteByte((byte)(((uint)value >> 8) & 0xFF));
            _stream.WriteByte((byte)(((uint)value >> 16) & 0xFF));
            _stream.WriteByte((byte)(((uint)value >> 24) & 0xFF));
        }

        public virtual void Write(long value)
        {
            _stream.WriteByte((byte)((ulong)value & 0xFF));
            _stream.WriteByte((byte)(((ulong)value >> 8) & 0xFF));
            _stream.WriteByte((byte)(((ulong)value >> 16) & 0xFF));
            _stream.WriteByte((byte)(((ulong)value >> 24) & 0xFF));
            _stream.WriteByte((byte)(((ulong)value >> 32) & 0xFF));
            _stream.WriteByte((byte)(((ulong)value >> 48) & 0xFF));
            _stream.WriteByte((byte)(((ulong)value >> 56) & 0xFF));
        }

        public virtual void Write(object value)
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
        public virtual void Write(sbyte value)
        {
            _stream.WriteByte((byte)value);
        }

        public virtual void Write(float value)
        {
            throw new NotImplementedException();
        }
        
        [CLSCompliant(false)]
        public virtual void Write(ushort value)
        {
            _stream.WriteByte((byte)(value & 0xFF));
            _stream.WriteByte((byte)((value >> 8) & 0xFF));
        }
        
        [CLSCompliant(false)]
        public virtual void Write(uint value)
        {
            _stream.WriteByte((byte)(value & 0xFF));
            _stream.WriteByte((byte)((value >> 8) & 0xFF));
            _stream.WriteByte((byte)((value >> 16) & 0xFF));
            _stream.WriteByte((byte)((value >> 24) & 0xFF));
        }
        
        [CLSCompliant(false)]
        public virtual void Write(ulong value)
        {
            _stream.WriteByte((byte)(value & 0xFF));
            _stream.WriteByte((byte)((value >> 8) & 0xFF));
            _stream.WriteByte((byte)((value >> 16) & 0xFF));
            _stream.WriteByte((byte)((value >> 24) & 0xFF));
            _stream.WriteByte((byte)((value >> 32) & 0xFF));
            _stream.WriteByte((byte)((value >> 48) & 0xFF));
            _stream.WriteByte((byte)((value >> 56) & 0xFF));
        }

        public virtual void Write(object value, Type type)
        {
            throw new NotImplementedException();
        }

        public virtual bool ReadBoolean()
        {
            int value = _stream.ReadByte();
            if (value == -1)
                throw new EndOfStreamException();
            else if (value == 0)
                return false;
            else
                return true;
        }

        public virtual byte ReadByte()
        {
            int result = _stream.ReadByte();
            if (result == -1)
                throw new EndOfStreamException();
            return (byte)result;
        }

        public virtual char ReadChar()
        {
            int byte0 = _stream.ReadByte();
            if (byte0 == -1) throw new EndOfStreamException();
            int byte1 = _stream.ReadByte();
            if (byte1 == -1) throw new EndOfStreamException();

            char result = (char)((byte1 << 8) & byte0);
            return result;
        }

        public virtual DateTime ReadDateTime()
        {
            throw new NotImplementedException();
        }

        public virtual decimal ReadDecimal()
        {
            throw new NotImplementedException();
        }

        public virtual double ReadDouble()
        {
            throw new NotImplementedException();
        }

        public virtual short ReadInt16()
        {
            int byte0 = _stream.ReadByte();
            if (byte0 == -1) throw new EndOfStreamException();
            int byte1 = _stream.ReadByte();
            if (byte1 == -1) throw new EndOfStreamException();

            short result = (short)((byte1 << 8) & byte0);
            return result;
        }

        public virtual int ReadInt32()
        {
            uint byte0 = (uint)_stream.ReadByte();
            if (byte0 == unchecked((uint)-1)) throw new EndOfStreamException();
            uint byte1 = (uint)_stream.ReadByte();
            if (byte1 == unchecked((uint)-1)) throw new EndOfStreamException();
            uint byte2 = (uint)_stream.ReadByte();
            if (byte2 == unchecked((uint)-1)) throw new EndOfStreamException();
            uint byte3 = (uint)_stream.ReadByte();
            if (byte3 == unchecked((uint)-1)) throw new EndOfStreamException();

            byte1 <<= 8;
            byte2 <<= 16;
            byte3 <<= 24;
            int result = (int)(byte3 & byte2 & byte1 & byte0);
            return result;
        }

        public virtual long ReadInt64()
        {
            uint byte0 = (uint)_stream.ReadByte();
            if (byte0 == unchecked((uint)-1)) throw new EndOfStreamException();
            uint byte1 = (uint)_stream.ReadByte();
            if (byte1 == unchecked((uint)-1)) throw new EndOfStreamException();
            uint byte2 = (uint)_stream.ReadByte();
            if (byte2 == unchecked((uint)-1)) throw new EndOfStreamException();
            uint byte3 = (uint)_stream.ReadByte();
            if (byte3 == unchecked((uint)-1)) throw new EndOfStreamException();
            ulong byte4 = (ulong)_stream.ReadByte();
            if (byte4 == unchecked((ulong)-1)) throw new EndOfStreamException();
            ulong byte5 = (ulong)_stream.ReadByte();
            if (byte5 == unchecked((ulong)-1)) throw new EndOfStreamException();
            ulong byte6 = (ulong)_stream.ReadByte();
            if (byte6 == unchecked((ulong)-1)) throw new EndOfStreamException();
            ulong byte7 = (ulong)_stream.ReadByte();
            if (byte7 == unchecked((ulong)-1)) throw new EndOfStreamException();

            byte1 <<= 8;
            byte2 <<= 16;
            byte3 <<= 24;
            byte4 <<= 32;
            byte5 <<= 40;
            byte6 <<= 48;
            byte7 <<= 56;
            long result = (long)(byte7 & byte6 & byte5 & byte4 & byte3 & byte2 & byte1 & byte0);
            return result;
        }
        
        [CLSCompliant(false)]
        public virtual sbyte ReadSByte()
        {
            int result = _stream.ReadByte();
            if (result == -1)
                throw new EndOfStreamException();
            return (sbyte)result;
        }

        public virtual float ReadSingle()
        {
            throw new NotImplementedException();
        }

        public virtual string ReadString()
        {
            throw new NotImplementedException();
        }
        
        [CLSCompliant(false)]
        public virtual ushort ReadUInt16()
        {
            int byte0 = _stream.ReadByte();
            if (byte0 == -1) throw new EndOfStreamException();
            int byte1 = _stream.ReadByte();
            if (byte1 == -1) throw new EndOfStreamException();

            ushort result = (ushort)((byte1 << 8) & byte0);
            return result;
        }
        
        [CLSCompliant(false)]
        public virtual uint ReadUInt32()
        {
            uint byte0 = (uint)_stream.ReadByte();
            if (byte0 == unchecked((uint)-1)) throw new EndOfStreamException();
            uint byte1 = (uint)_stream.ReadByte();
            if (byte1 == unchecked((uint)-1)) throw new EndOfStreamException();
            uint byte2 = (uint)_stream.ReadByte();
            if (byte2 == unchecked((uint)-1)) throw new EndOfStreamException();
            uint byte3 = (uint)_stream.ReadByte();
            if (byte3 == unchecked((uint)-1)) throw new EndOfStreamException();

            byte1 <<= 8;
            byte2 <<= 16;
            byte3 <<= 24;
            uint result = (uint)(byte3 & byte2 & byte1 & byte0);
            return result;
        }
        
        [CLSCompliant(false)]
        public virtual ulong ReadUInt64()
        {
            uint byte0 = (uint)_stream.ReadByte();
            if (byte0 == unchecked((uint)-1)) throw new EndOfStreamException();
            uint byte1 = (uint)_stream.ReadByte();
            if (byte1 == unchecked((uint)-1)) throw new EndOfStreamException();
            uint byte2 = (uint)_stream.ReadByte();
            if (byte2 == unchecked((uint)-1)) throw new EndOfStreamException();
            uint byte3 = (uint)_stream.ReadByte();
            if (byte3 == unchecked((uint)-1)) throw new EndOfStreamException();
            ulong byte4 = (ulong)_stream.ReadByte();
            if (byte4 == unchecked((ulong)-1)) throw new EndOfStreamException();
            ulong byte5 = (ulong)_stream.ReadByte();
            if (byte5 == unchecked((ulong)-1)) throw new EndOfStreamException();
            ulong byte6 = (ulong)_stream.ReadByte();
            if (byte6 == unchecked((ulong)-1)) throw new EndOfStreamException();
            ulong byte7 = (ulong)_stream.ReadByte();
            if (byte7 == unchecked((ulong)-1)) throw new EndOfStreamException();

            byte1 <<= 8;
            byte2 <<= 16;
            byte3 <<= 24;
            byte4 <<= 32;
            byte5 <<= 40;
            byte6 <<= 48;
            byte7 <<= 56;
            ulong result = (ulong)(byte7 & byte6 & byte5 & byte4 & byte3 & byte2 & byte1 & byte0);
            return result;
        }
        
        [SecuritySafeCritical]
        public virtual object ReadValue(Type type)
        {
            throw new NotImplementedException();
        }

        //--- Public Properties ---

        public Stream Stream
        {
            get { return _stream; }
        }
    }
}
