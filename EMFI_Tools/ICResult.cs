using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace EMFI_Tools
{
    public class ICResults : IEquatable<ICResults>
    {
        private string _queue;
        public string Queue
        {
            get { return _queue; }
            set
            {
                if (value == "") _queue = "0";
                else _queue = value;
            }
        }
        public int PurgeDays { get; set; }
        public string Port { get; set; }
        public string IP { get; set; }
        public string HL7 { get; set; }
        public string AutoStart { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Cid { get; set; }
        public bool Flagged { get; set; }

        public ICResults(string id, string cid, string queue, string name, int purgeDays, string port, string ip, string hl7, string autostart)
        {

            Id = id;
            Cid = cid;
            Queue = queue;
            Name = name;
            PurgeDays = purgeDays;
            Port = port;
            IP = ip;
            HL7 = hl7;
            AutoStart = autostart;
            Flagged = false;

        }

        public string Print()
        {
            return (Id + "," + Queue + "," + Name + "," + Port + "," + IP + "," + PurgeDays + "," + HL7 + "," + AutoStart);
        }
        public bool Equals(ICResults other)
        {
            if (this.Id == other.Id && this.Queue == other.Queue && this.Name == other.Name && this.Port == other.Port &&
                this.IP == other.IP && this.HL7 == other.HL7 && this.PurgeDays == other.PurgeDays)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class ICResultsList
    {
        public List<ICResults> AIPIs { get; set; }
        public List<ICResults> AIPIsOld { get; set; }

        public bool IsEqual()
        {
            bool same = true;
            int i = 0;
            foreach (var item in AIPIs)
            {
                if (i < AIPIsOld.Count)
                    same = (AIPIs[i].Equals(AIPIsOld[i]));
                if (!same) break;
            }
            return same;
        }

 
        public int Fill(Microsoft.Data.SqlClient.SqlDataReader reader)
        {
            string id, queue, name, port, ip = "", hl7, autostart, cid;
            int purgeDays;
            AIPIsOld = new List<ICResults>();
            while (reader.Read())
            {

                id = reader["Aip_id"].ToString();
                cid = reader["Aip_cid"].ToString();
                queue = reader["Aip_queue"].ToString();
                name = reader["Aip_name"].ToString();
                purgeDays = Int32.Parse(reader["Purge_days"].ToString());
                port = reader["Aip_port"].ToString();
                ip = reader["Aip_IP"].ToString();
                hl7 = reader["HL7"].ToString();

                //autostart = reader.IsDBNull(15) ? "0" : (reader.GetBoolean(15) == true ? "1" : "0");
                //cid = reader.GetInt64(4).ToString();
                ICResults saveResults = new ICResults(id, cid, queue, name, purgeDays, port, ip, hl7, "0");
                AIPIsOld.Add(saveResults);
            }
            return 1;
        }
    }
}
