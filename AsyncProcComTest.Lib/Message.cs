using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncProcComTest.Lib
{
    [Serializable]
    public class Message
    {
        public int ID { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            return $"{nameof(this.ID)} = {this.ID}, {nameof(this.Content)} = {this.Content} ";
        }
    }
}
