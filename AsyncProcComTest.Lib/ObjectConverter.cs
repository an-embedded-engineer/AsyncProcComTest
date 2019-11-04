using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AsyncProcComTest.Lib
{
    public class ObjectConverter<T>
    {
        private IFormatter Formatter { get; }

        public ObjectConverter(IFormatter formatter = null)
        {
            this.Formatter = formatter ?? new BinaryFormatter();
        }

        public byte[] ToByteArray(T obj)
        {
            using (var stream = new MemoryStream())
            {
                this.Formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        public T FromByteArray(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return (T)this.Formatter.Deserialize(stream);
            }
        }
    }
}
