using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Azure.Core.HttpHeader;

namespace EMFI_Tools
{
    public partial class EnvForm : Form
    {
        public ICShortList results;

        public EnvForm()
        {
            InitializeComponent();
        }
        public EnvForm(EnvPanel panel)
        {
            InitializeComponent();
            envTB.Text = panel.Env;
            runLevelTB.Text = panel.RunLevel;
            includeTB.Text = panel.Include;
            versionTB.Text = panel.Version;
            editLockCB.Checked = panel.EditLock;
            getECLDestination(panel.Env);
            getEGCInboundInterfaces(panel.Env);
        }

        private void closePB_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void getEGCInboundInterfaces(string env)
        {
            string userName = "emp$4000";  //emp$462"
            string key = "";
            string comm = env.Substring(2);
            string uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/GetEGCInboundInterfaces";
            var restResponse = Interconnect.HttpClientManager.SendAsync(HttpMethod.Get, uri, string.Empty, @"application/json", userName, key, env);
            dcinDGV.Columns.Clear();
            if (restResponse != null)
            {
                var importResult = restResponse.Result.ToString();
                var importStatus = restResponse.Result.StatusCode.ToString();
                if (importStatus != "NotFound")
                {
                    var bodyText = restResponse.Result.Content.ReadAsStringAsync().Result;
                    var a = Newtonsoft.Json.JsonConvert.DeserializeObject<ICShortList>(bodyText);
                    if (a != null)
                    {
                        results = a;

                        dcinDGV.ColumnCount = 3;
                        dcinDGV.Columns[0].Name = "#";
                        dcinDGV.Columns[1].Name = "Id";
                        dcinDGV.Columns[2].Name = "Name";

                        dcinDGV.Columns[0].Width = 35;
                        dcinDGV.Columns[1].Width = 60;
                        dcinDGV.Columns[2].Width = 250;

                        foreach (ICShort res in results.aips)
                        {
                            var index = dcinDGV.Rows.Add();
                            dcinDGV.Rows[index].Cells["#"].Value = index + 1;
                            dcinDGV.Rows[index].Cells["Id"].Value = res.Id;
                            dcinDGV.Rows[index].Cells["Name"].Value = res.Name;
                        }
                    }
                }
            }
        }

        private void getECLDestination(string env)
        {
            string userName = "emp$4000";  //emp$462"
            string key = "";
            string comm = env.Substring(2);
            string uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/GetECLDestinations";
            var restResponse = Interconnect.HttpClientManager.SendAsync(HttpMethod.Get, uri, string.Empty, @"application/json", userName, key, env);
            dcoutDGV.Columns.Clear();
            if (restResponse != null)
            {
                var importResult = restResponse.Result.ToString();
                var importStatus = restResponse.Result.StatusCode.ToString();
                if (importStatus != "NotFound")
                {
                    var bodyText = restResponse.Result.Content.ReadAsStringAsync().Result;
                    var a = Newtonsoft.Json.JsonConvert.DeserializeObject<ICShortList>(bodyText);
                    if (a != null)
                    {
                        results = a;

                        dcoutDGV.ColumnCount = 3;
                        dcoutDGV.Columns[0].Name = "#";
                        dcoutDGV.Columns[1].Name = "Id";
                        dcoutDGV.Columns[2].Name = "Name";

                        dcoutDGV.Columns[0].Width = 35;
                        dcoutDGV.Columns[1].Width = 60;
                        dcoutDGV.Columns[2].Width = 250;

                        foreach (ICShort res in results.aips)
                        {
                            var index = dcoutDGV.Rows.Add();
                            dcoutDGV.Rows[index].Cells["#"].Value = index + 1;
                            dcoutDGV.Rows[index].Cells["Id"].Value = res.Id;
                            dcoutDGV.Rows[index].Cells["Name"].Value = res.Name;
                        }
                    }
                }
            }
        }
    }
}
