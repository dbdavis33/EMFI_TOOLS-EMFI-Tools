using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMFI_Tools
{
    public class oPort
    {
        public string Id { get; set; }
        public string Port { get; set; }
        public string Name { get; set; }
        public string Env { get; set; } 
    }
    public class Ports
    {
        public List<oPort> ports;
    }
}
