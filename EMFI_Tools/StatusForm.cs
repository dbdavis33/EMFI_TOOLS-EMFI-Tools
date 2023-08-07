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
    public partial class StatusForm : Form
    {
        public System.Windows.Forms.Timer bcTimer;
        public Stopwatch bcsw;

        public System.Windows.Forms.Timer timer;
        public Stopwatch sw;
        public bool EgaComplete = false;


        int refreshRate = 10;   // 10 secs
        int timeOut = 3;

        List<string> instances;

        public string Env { get; set; }
        public string EGAId { get; set; }
        public string Cid { get; set; }
        public string Comment { get; set; }
        public List<string> Destinations { get; set; }

        public string Alt { get; set; }


        public StatusForm()
        {
            InitializeComponent();
        }

        public StatusForm(string env, string egaId, string cid, string comment, List<string> dest, string alt)
        {
            InitializeComponent();
            Env = env;
            EGAId = egaId;
            Text = "Status - EGA: " + egaId + " from " + Env + " " + alt;
            Cid = cid;
            Alt = alt;
            Comment = comment;
            Destinations = dest;
        }

        private void buildComparePB_Click(object sender, EventArgs e)
        {
            sw.Stop();
            timer.Stop();

            bcTimer.Start();
            bcsw.Start();
            bcTimer.Tick += timer_Tick_BC;
            buildComparePB.Enabled = false;
        }
        private void timer_Tick_BC(object sender, EventArgs e)
        {
            var ts = bcsw.Elapsed;
            if (ts.Minutes > timeOut)
            {
                bcTimer.Stop();
                bcsw.Stop();
                buildComparePB.BackColor = System.Drawing.Color.Red;
                buildComparePB.Text = "Check BC";
                buildComparePB.Enabled = true;
            }
            else
            {
                if (BuildCompare())
                {
                    bcTimer.Stop();
                    bcsw.Stop();
                    buildComparePB.Enabled = true;
                    buildComparePB.BackColor = System.Drawing.Color.Green;
                    buildComparePB.Text = "Complete";

                }
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
            //string[] parts;

            foreach (string ins in instances)
            {
                done = true;
                int a;
                cbName = ins.ToLower() + "CKB";
                cs = this.insFloLO.Controls.Find(cbName, true);
                if (cbName.Contains("PRD"))
                    a = 1;

                thisCB = (CheckBox)cs[0];
                if (thisCB.Checked == false)
                {
                    done = false;
                    if (!ins.Contains("SANDBOX"))
                        comm = ins.Substring(2);
                    else
                        comm = ins.Substring(0, ins.Length - 1);

                    uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/getItem?Ini=WQF&Item=38&Cid=" + Cid;
                    SameResult r = (SameResult)Interconnect.HttpClientManager.GetHttpData(uri, ins, typeof(SameResult));
                    if (r != null && r.Result != null)
                    {
                        if (r.Result == Comment)
                        {
                            thisCB.Checked = true;
                        }
                    }
                }
            }
            return done;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var ts = sw.Elapsed;
            if (ts.Minutes > timeOut)
            {
                timer.Stop();
                sw.Stop();
                buildComparePB.BackColor = System.Drawing.Color.Red;
                buildComparePB.Text = "Check EGA status";
            }
            else if (CheckComplete())
            {
                timer.Stop();
                sw.Stop();
                //statusClosePB.BackColor = System.Drawing.Color.Green;
                //statusClosePB.Text = "Complete";
                buildComparePB.Enabled = true;
                //Complete = true;
            }
        }
        public bool CheckComplete()
        {
            // Loop thru all, removing those that match emp home
            string uri;
            string comm;
            bool done = false;
            comm = Env.Substring(2);
            if (!EgaComplete)
            {
                uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/AutoDCStatus?egaid=" + EGAId;

                AResult result = (AResult)Interconnect.HttpClientManager.GetHttpData(uri, Env, typeof(AResult));
                // Testing
                //Result result = new Result();
                //result.status = "Complete";
                //
                pkgStatusTB.Text = result.status;
                if (result.status == "Complete")
                {
                    EgaComplete = true;
                    done = true;
                }
            }
            return done;
        }

        private void LoadCheckBoxes()
        {
            instances = new List<string>();
            string ckbName = "";
            string comm = "";
            this.insFloLO.Controls.Clear();
            foreach (string dest in Destinations)
            {
                if (dest.Contains("REF"))
                {
                    if (Alt == "Main")
                    {
                        // Skip Refs for now. No IC servers
                        comm = dest;
                        ckbName = comm.ToLower() + "CKB";
                        this.insFloLO.Controls.Add(new CheckBox() { Name = ckbName, Text = dest.ToUpper(), Checked = true });
                        //instances.Add(dest);
                    }
                }
                else if (dest.Contains("SANDBOX"))
                {
                    if (Alt == "Main")
                    {
                        ckbName = dest.ToLower() + "CKB";
                        this.insFloLO.Controls.Add(new CheckBox() { Name = ckbName, Text = "SB2", Checked = false }); ;
                        //comm = dest.Substring(0, dest.Length - 1);
                        instances.Add(dest);
                    }
                }
                else
                {
                    comm = dest.Substring(2);
                    ckbName = comm.ToLower() + "CKB";
                    this.insFloLO.Controls.Add(new CheckBox() { Name = "cl" + ckbName, Text = "CL" + comm.ToUpper(), Checked = false }); instances.Add("CL" + comm);
                    this.insFloLO.Controls.Add(new CheckBox() { Name = "ak" + ckbName, Text = "AK" + comm.ToUpper(), Checked = false }); instances.Add("AK" + comm);
                    this.insFloLO.Controls.Add(new CheckBox() { Name = "oc" + ckbName, Text = "OC" + comm.ToUpper(), Checked = false }); instances.Add("OC" + comm);
                    this.insFloLO.Controls.Add(new CheckBox() { Name = "wm" + ckbName, Text = "WM" + comm.ToUpper(), Checked = false }); instances.Add("WM" + comm);
                }

            }
        }

        private void StatusForm_Load(object sender, EventArgs e)
        {
            // Start Cycling through all instance 
            EgaComplete = false;
            //LoadInstances();
            LoadCheckBoxes();
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
            buildComparePB.Enabled = false;

        }

        private void StatusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bcTimer.Stop();
            bcsw.Stop();
        }
    }
}
