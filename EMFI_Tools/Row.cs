using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMFI_Tools
{
    public class Row
    {
        public int rowCount { get; set; }
        public bool use { get; set; }
        public string id { get; set; }
        public string newId { get; set; }
        public string name { get; set; }
        public string port { get; set; }

        public Row(int i, bool use, string id, string name, string port)
        {
            rowCount = i;
            this.use = use;
            this.id = id;
            this.name = name;   
            this.port = port;   
        }
    }
}
