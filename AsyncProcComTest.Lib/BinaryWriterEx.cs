using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncProcComTest.Lib
{
    public static class BinaryWriterEx
    {
        public static void WriteObject<T>(this BinaryWriter writer, T obj)
        {
            var converter = new ObjectConverter<T>();

            var bytes = converter.ToByteArray(obj);

            writer.Write(bytes.Length);

            writer.Write(bytes);
        }
    }
}
