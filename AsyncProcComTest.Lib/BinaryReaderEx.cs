using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncProcComTest.Lib
{
    public static class BinaryReaderEx
    {
        public static T ReadObject<T>(this BinaryReader reader)
        {
            var length = reader.ReadInt32();

            var bytes = reader.ReadBytes(length);

            var converter = new ObjectConverter<T>();

            return converter.FromByteArray(bytes);
        }
    }
}
