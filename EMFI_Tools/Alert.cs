using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMFI_Tools
{
    public class Alert
    {
        public string Id { get; set; }
        public string Disabled { get; set; }
    }
    public class Alerts
    {
        public List<Alert> alerts { get; set; }
    }
}
