using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMFI_Tools
{
    public partial class EMDC_StatusForm : Form
    {

        public string UserId { get; set; }
        public string EmailAddr { get; set; }
        public string Cid { get; set; }
        public string Env { get; set; }
        public string User { get; set; }

        public System.Windows.Forms.Timer bcTimer;
        public Stopwatch bcsw;

        public System.Windows.Forms.Timer timer;
        public Stopwatch sw;
        public bool Complete = false;

        int refreshRate = 10;   // 10 secs
        int timeOut = 3;

        List<string> instances;

        public EMDC_StatusForm(string env, string cid, string emailAddr, string user)
        {
            InitializeComponent();
            Env = env;
            Cid = cid;
            User = user;
            EmailAddr = emailAddr;
            userLB.Text += (user + " - " + cid);

        }
        private void LoadInstances()
        {
            //Load instances
            string comm = Env.Substring(2);
            instances = new List<string>();
            instances.Add("CLPOC");
            instances.Add("AKPOC");
            instances.Add("OCPOC");
            instances.Add("WMPOC");
            instances.Add("CLTST");
            instances.Add("AKTST");
            instances.Add("OCTST");
            instances.Add("WMTST");
            instances.Add("CLBETA");
            instances.Add("AKBETA");
            instances.Add("OCBETA");
            instances.Add("WMBETA");
            instances.Add("CLPRD");
            instances.Add("AKPRD");
            instances.Add("OCPRD");
            instances.Add("WMPRD");
            instances.Add("CLMST");
            instances.Add("AKMST");
            instances.Add("OCMST");
            instances.Add("WMMST");
            instances.Add("CLREL");
            instances.Add("AKREL");
            instances.Add("OCREL");
            instances.Add("WMREL");
            instances.Add("CLSUP");
            instances.Add("AKSUP");
            instances.Add("OCSUP");
            instances.Add("WMSUP");
            instances.Add("AKREF");
            instances.Add("OCREF");
            instances.Add("WMREF");
        }
        public EMDC_StatusForm()
        {
            InitializeComponent();
        }

        private void emdcClosePB_Click(object sender, EventArgs e)
        {
            timer.Stop();
            sw.Stop();
            bcTimer.Stop();
            bcsw.Stop();
            this.Close();
        }

        private void EMDC_StatusForm_Load(object sender, EventArgs e)
        {
            // Start Cycling through all instance 
            LoadInstances();
            Complete = false;
            // Setup UI refresh timer
            bcsw = new Stopwatch();
            bcTimer = new System.Windows.Forms.Timer()
            {
                Interval = refreshRate * 1000,
            };

            sw = new Stopwatch();
            timer = new System.Windows.Forms.Timer()
            {
                Interval = refreshRate * 1000,
            };
            timer.Start();
            sw.Start();
            timer.Tick += timer_Tick;
            emdcClosePB.Enabled = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var ts = sw.Elapsed;
            if (ts.Minutes > timeOut)
            {
                timer.Stop();
                sw.Stop();
                emdcClosePB.BackColor = System.Drawing.Color.Red;
                emdcClosePB.Text = "Check EMDC Status";
            }
            else if (BuildCompare())
            {
                timer.Stop();
                sw.Stop();
                emdcClosePB.Enabled = true;
            }
        }
        public bool BuildCompare()
        {
            string uri;
            string comm;
            bool done = false;
            string cbName;
            Control[] cs;
            CheckBox thisCB;

            foreach (string ins in instances)
            {
                done = true;
                cbName = ins.ToLower() + "CB";
                cs = this.emdcLO.Controls.Find(cbName, true);
                thisCB = (CheckBox)cs[0];
                if (thisCB.Checked == false)
                {
                    done = false;
                    //comm = ins.Substring(0, ins.Length - 1);
                    comm = ins.Substring(2, ins.Length - 2);
                    uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/getItem?Ini=EMP&Item=150&Cid=" + Cid;
                    SameResult r = (SameResult)Interconnect.HttpClientManager.GetHttpData(uri, ins, typeof(SameResult));
                    if (r != null && r.Result != null)
                    {
                        if (r.Result == EmailAddr)
                        {
                            thisCB.Checked = true;
                        }
                    }
                }
            }
            return done;
        }
    }
}
