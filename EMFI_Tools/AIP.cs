using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMFI_Tools
{
    public class AIP
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Default { get; set; }

        public AIP(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class AIPs
    {
        public List<AIP> aips { get; set; }
    }
    public class lAIP
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Port { get; set; }    
        public lAIP(string name, string id)
        {
            Name = name;
            Id = id;
        }
    }
    public class lAIPs
    {
        public List<lAIP> aips { get; set; }
    }
}
