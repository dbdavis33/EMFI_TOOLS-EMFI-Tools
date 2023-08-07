using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMFI_Tools
{
    public class PackageResult
    {
        public string returnval { get; set; }
        public string egaic { get; set; }

        public PackageResult()
        {
        }
        public PackageResult(string rv, string id)
        {
            returnval = rv;
            egaic = id;
        }
    }
}
