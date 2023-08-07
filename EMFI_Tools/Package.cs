using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMFI_Tools
{
    public class Package
    {
        public string Env { get; set; }
        public string Filename { get; set; }
        public string Cid { get; set; }
        public List<AIP> AIPs { get; set; }
        public Package(string env, string f, string cid)
        {
            Env = env;
            Filename = f;
            AIPs = new List<AIP>();
            Cid = cid;
        }
        public string AIPString()
        {
            string str = "";
            foreach (AIP aip in AIPs)
            {
                str += aip.Id + "^";
            }
            return str.Substring(0, str.Length - 1);
        }
    }
}
