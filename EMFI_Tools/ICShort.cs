using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMFI_Tools
{
    public class ICShort: IEquatable<ICShort>
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Default { get; set; }

        public ICShort(string id, string name, string def)
        {

            Id = id;
            Name = name;
            Default = def;
        }

        public bool Equals(ICShort other)
        {
            if (this.Id == other.Id && this.Name == other.Name && this.Default == other.Default)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class ICShortList
    {
        public List<ICShort> aips { get; set; }
    }
}
