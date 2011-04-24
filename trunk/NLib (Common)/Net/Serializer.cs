using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace NLib.Net
{
    static class Serializer
    {
        public static void Serialize(Stream outputStream, object objectToSerialize)
        {
            var encoder = new VarintFormatter(outputStream);
            Serialize(objectToSerialize, encoder);
        }

        public static void Serialize(ISerializationStream encoder, object objectToSerialize)
        {
            var serializable = objectToSerialize as ISerializable2;
            if (serializable != null)
            {
                serializable.GetObjectData(encoder);
            }
            else
            {
                AutoSerialize(objectToSerialize, encoder);
            }
        }
        
        public static void Serialize<T>(Stream outputStream, T objectToSerialize) where T : struct
        {
            var encoder = new VarintFormatter(outputStream);
            Serialize(objectToSerialize, encoder);
        }

        public static void Serialize(object objectToSerialize, ISerializationStream encoder)
        {
            var serializable = objectToSerialize as ISerializable2;
            if (serializable != null)
            {
                serializable.GetObjectData(encoder);
            }
            else
            {
                AutoSerialize(objectToSerialize, encoder);
            }
        }

        static void AutoSerialize(object objectToSerialize, ISerializationStream encoder)
        {
            var fieldInfos = GetSerializableMembers(objectToSerialize.GetType());
            SortFieldInfoArray(fieldInfos);

            int fieldInfosLength = fieldInfos.Length;
            for (int i = 0; i < fieldInfosLength; i++)
            {
                FieldInfo fieldInfo = fieldInfos[i];
                if (fieldInfo.FieldType == typeof(bool)) encoder.Write((bool)fieldInfo.GetValue(objectToSerialize));
                else if (fieldInfo.FieldType == typeof(byte)) encoder.Write((byte)fieldInfo.GetValue(objectToSerialize));
                else if (fieldInfo.FieldType == typeof(char)) encoder.Write((char)fieldInfo.GetValue(objectToSerialize));
                else if (fieldInfo.FieldType == typeof(DateTime)) encoder.Write((DateTime)fieldInfo.GetValue(objectToSerialize));
                else if (fieldInfo.FieldType == typeof(decimal)) encoder.Write((decimal)fieldInfo.GetValue(objectToSerialize));
                else if (fieldInfo.FieldType == typeof(double)) encoder.Write((double)fieldInfo.GetValue(objectToSerialize));
                else if (fieldInfo.FieldType == typeof(short)) encoder.Write((short)fieldInfo.GetValue(objectToSerialize));
                else if (fieldInfo.FieldType == typeof(long)) encoder.Write((long)fieldInfo.GetValue(objectToSerialize));
                else if (fieldInfo.FieldType == typeof(object)) encoder.Write((object)fieldInfo.GetValue(objectToSerialize));
                else if (fieldInfo.FieldType == typeof(sbyte)) encoder.Write((sbyte)fieldInfo.GetValue(objectToSerialize));
                else if (fieldInfo.FieldType == typeof(float)) encoder.Write((float)fieldInfo.GetValue(objectToSerialize));
                else if (fieldInfo.FieldType == typeof(ushort)) encoder.Write((ushort)fieldInfo.GetValue(objectToSerialize));
                else if (fieldInfo.FieldType == typeof(uint)) encoder.Write((uint)fieldInfo.GetValue(objectToSerialize));
                else if (fieldInfo.FieldType == typeof(ulong)) encoder.Write((ulong)fieldInfo.GetValue(objectToSerialize));
            }
        }

        static void SortFieldInfoArray(FieldInfo[] fieldInfos)
        {
            Array.Sort(fieldInfos,
            (FieldInfo fieldInfo1, FieldInfo fieldInfo2) =>
            {
                return fieldInfo1.ToString().CompareTo(fieldInfo2.ToString());
            });
        }

        static FieldInfo[] GetSerializableMembers(Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            int index = 0;
            for (int i = 0; i < fields.Length; i++)
            {
                if ((fields[i].Attributes & FieldAttributes.NotSerialized) != FieldAttributes.NotSerialized)
                {
                    index++;
                }
            }
            if (index == fields.Length)
            {
                return fields;
            }
            FieldInfo[] infoArray2 = new FieldInfo[index];
            index = 0;
            for (int j = 0; j < fields.Length; j++)
            {
                if ((fields[j].Attributes & FieldAttributes.NotSerialized) != FieldAttributes.NotSerialized)
                {
                    infoArray2[index] = fields[j];
                    index++;
                }
            }

            return infoArray2;
        }
    }
}
