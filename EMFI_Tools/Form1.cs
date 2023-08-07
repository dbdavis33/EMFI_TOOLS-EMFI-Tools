using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace EMFI_Tools
{
    public partial class Form1 : Form
    {
        bool strict = false;
        bool hasSecurity = false;
        public List<AIP> aips;

        public ICResultsList results;
        public string[] instances = { "CL", "EF", "AK", "OC", "WM" };
        public string[] icomms;
        public string[] pcomms = {
            "CLPOC","EFPOC","AKPOC","OCPOC","WMPOC",
            "CLBETA","EFBETA","AKBETA","OCBETA","WMBETA",
            "CLTST","EFTST","AKTST","OCTST","WMTST",
            "CLPRD","EFPRD","AKPRD","OCPRD","WMPRD",
            "CLMCK","EFMCK","AKMCK","OCMCK","WMMCK",
            "CLMST","EFMST","AKMST","OCMST","WMMST",
            "CLREL","EFREL","AKREL","OCREL","WMREL",
            "CLSUP","EFSUP","AKSUP","OCSUP","WMSUP",
            "CLSKEW","EFSKEW","AKSKEW","OCSKEW","WMSKEW"
            };
        public string[] tcomms = {
            "CLPOC","EFPOC","AKPOC","OCPOC","WMPOC",
            "CLBETA","EFBETA","AKBETA","OCBETA","WMBETA",
            "CLTST","EFTST","AKTST","OCTST","WMTST",
            "CLPRD","EFPRD","AKPRD","OCPRD","WMPRD",
            "CLMCK","EFMCK","AKMCK","OCMCK","WMMCK",
            "CLMST","EFMST","AKMST","OCMST","WMMST",
            "CLREL","EFREL","AKREL","OCREL","WMREL",
            "CLSUP","EFSUP","AKSUP","OCSUP","WMSUP",
            "CLSKEW","EFSKEW","AKSKEW","OCSKEW","WMSKEW",
            "CLMPE","EFMPE","AKMPE","OCMPE","WMMPE",
            "CLNEXT","EFNEXT","AKNEXT","OCNEXT","WMNEXT",
            "CLMDR","EFMDR","AKMDR","OCMDR","WMMDR",
            "CLVAL","EFVAL","AKVAL","OCVAL","WMVAL",
            "CLMPE","EFMPE","AKMPE","OCMPE","WMMPE"
            };
        public string[] acomms = {
            "CLPOC","EFPOC","AKPOC","OCPOC","WMPOC",
            "CLBETA","EFBETA","AKBETA","OCBETA","WMBETA",
            "CLTST","EFTST","AKTST","OCTST","WMTST",
            "CLPRD","EFPRD","AKPRD","OCPRD","WMPRD",
            "CLMCK","EFMCK","AKMCK","OCMCK","WMMCK",
            "CLMST","EFMST","AKMST","OCMST","WMMST",
            "CLREL","EFREL","AKREL","OCREL","WMREL",
            "CLSUP","EFSUP","AKSUP","OCSUP","WMSUP",
            "CLSKEW","EFSKEW","AKSKEW","OCSKEW","WMSKEW",
            "CLCTD","EFCTD","AKCTD","OCCTD","WMCTD",
            "CLMPE","EFMPE","AKMPE","OCMPE","WMMPE",
            "CLNEXT","EFNEXT","AKNEXT","OCNEXT","WMNEXT",
            "CLMDR","EFMDR","AKMDR","OCMDR","WMMDR",
            "CLVAL","EFVAL","AKVAL","OCVAL","WMVAL",
            "CLMPE","EFMPE","AKMPE","OCMPE","WMMPE",
            "SANDBOX1","SANDBOX2","SANDBOX3"
            };

        public Form1()
        {
            InitializeComponent();
            InitialDGVs();
            envPanelCB.SelectedIndex = 0;
        }

        public void InitialDGVs()
        {
            // #
            srcAIPsDGV.Columns.Add("num", "#");
            srcAIPsDGV.Columns["num"].DataPropertyName = "num";
            srcAIPsDGV.Columns["num"].Width = 60;
            srcAIPsDGV.Columns["num"].SortMode = DataGridViewColumnSortMode.Automatic;
            srcAIPsDGV.Columns["num"].ReadOnly = true;

            // Use?
            DataGridViewColumn use = new DataGridViewCheckBoxColumn()
            {
                HeaderText = "Use",
                Name = "use",
                FlatStyle = FlatStyle.Standard,
                ThreeState = true,
                Width = 60,
                CellTemplate = new DataGridViewCheckBoxCell(),
            };

            srcAIPsDGV.Columns.Add(use);
            //srcAIPsDGV.Columns["use"].Width = 60;
            //srcAIPsDGV.Columns["use"].SortMode = DataGridViewColumnSortMode.Automatic;

            // ID
            srcAIPsDGV.Columns.Add("aip", "AIP");
            srcAIPsDGV.Columns["aip"].DataPropertyName = "Id";
            srcAIPsDGV.Columns["aip"].Width = 60;
            srcAIPsDGV.Columns["aip"].SortMode = DataGridViewColumnSortMode.Automatic;
            srcAIPsDGV.Columns["aip"].ReadOnly = true;

            srcAIPsDGV.Columns.Add("newAip", "New AIP");
            srcAIPsDGV.Columns["newAip"].DataPropertyName = "NewId";
            srcAIPsDGV.Columns["newAip"].Width = 60;
            srcAIPsDGV.Columns["newAip"].SortMode = DataGridViewColumnSortMode.Automatic;
            //srcAIPsDGV.Columns["newAip"].ReadOnly = true;

            // Name
            srcAIPsDGV.Columns.Add("name", "Name");
            srcAIPsDGV.Columns["name"].DataPropertyName = "Name";
            srcAIPsDGV.Columns["name"].Width = 240;
            srcAIPsDGV.Columns["name"].SortMode = DataGridViewColumnSortMode.Automatic;
            srcAIPsDGV.Columns["name"].ReadOnly = true;


            srcAIPsDGV.Columns.Add("port", "PORT");
            srcAIPsDGV.Columns["port"].DataPropertyName = "Port";
            srcAIPsDGV.Columns["port"].Width = 60;
            srcAIPsDGV.Columns["port"].SortMode = DataGridViewColumnSortMode.Automatic;
            srcAIPsDGV.Columns["port"].ReadOnly = true;

            // Port
            destAIPsDGV.Columns.Add("port", "Port");
            destAIPsDGV.Columns["port"].DataPropertyName = "Port";
            destAIPsDGV.Columns["port"].Width = 120;
            destAIPsDGV.Columns["port"].SortMode = DataGridViewColumnSortMode.Automatic;
            destAIPsDGV.Columns["port"].ReadOnly = true;

        }
        private bool saveConfigs(string instance)
        {
            // Cycle through selected environments
            //loadSaveResultsLB.Items.Clear();
            string comm = commCB.SelectedItem.ToString();
            string uri;
            //string key = "";
            //string env;
            //string userName = "emp$4000";  //emp$462"
            string env = instance + comm;
            loadSaveResultsLB.Items.Add("Saving configuration for : " + env);
            uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/SaveEMFIEnv?SaveDirectory=" + fileTB.Text + "&FileTag=" + tagTB.Text;
            Result r = (Result)Interconnect.HttpClientManager.GetHttpData(uri, env, typeof(Result));
            //Result r = (Result)HttpClientMgr.GetHttpData(uri, env, typeof(Result));
            if (r != null && r.result != null)
            {
                loadSaveResultsLB.Items.Add(r.result);
                return true;
            }
            else
            {
                loadSaveResultsLB.Items.Add("Error saving config for: " + env);
            }
            return false;
        }

        private void savePB_Click(object sender, EventArgs e)
        {

            if (commCB.Text == "")
            {
                MessageBox.Show("Please select a community");
                return;
            }
            if (instanceCB.Text == "")
            {
                MessageBox.Show("Please selected an instance");
                return;
            }

            if (fileTB.Text == "/nfs/prov/emfi/")
            {
                MessageBox.Show("Please enter subdirectory for save");
                return;
            }
            if (fileTB.Text[fileTB.Text.Length - 1] != '/')
                fileTB.Text += "/";

            loadSaveResultsLB.Items.Clear();
            if (instanceCB.Text == "ALL")
            {
                foreach (string s in instances)
                {
                    saveConfigs(s);
                }
            }
            else
                saveConfigs(instanceCB.Text);
        }
        private void saveETAN(string instance, string filename)
        {
            string key = "";
            string userName = "emp$4000";  //emp$462"
            string env = instance + commCB.Text;
            string uri = "https://interconn.providence.org/Interconnect-" + commCB.Text + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/SaveGoldenECI?Filename=" + filename;
            Uri myUri = new Uri(uri);
            goldenECIResultLB.Items.Add("Working on " + env);
            //var restResponse = HttpClientMgr.SendAsync(HttpMethod.Get, uri, String.Empty, @"application/json", userName, key, env);
            var restResponse = Interconnect.HttpClientManager.SendAsync(HttpMethod.Get, uri, String.Empty, @"application/json", userName, key, env);
            if (restResponse != null)
            {
                var importResult = restResponse.Result.ToString();
                var importStatus = restResponse.Result.StatusCode.ToString();
                if (importStatus != "NotFound")
                {
                    var bodyText = restResponse.Result.Content.ReadAsStringAsync().Result;
                    var r = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(bodyText);
                    if (r != null) goldenECIResultLB.Items.Add(r.result);
                }
                else
                {
                    goldenECIResultLB.Items.Add("Interconnect: SaveETAN failed.");
                }
            }

        }
        private void goldenECIPB_Click(object sender, EventArgs e)
        {
            goldenECIResultLB.Items.Clear();
            if (packFilenameTB.Text == "/nfs/prov/emfi/")
            {
                goldenECIResultLB.Items.Add("Please enter subdirectory for pack");
                return;
            }
            if ((commCB.Text == "NEXT" || commCB.Text == "POC") &&
                instanceCB.Text == "CL")
            {
                saveETAN(instanceCB.Text, packFilenameTB.Text);
            }
            else if (instanceCB.Text == "ALL")
            {
                goldenECIResultLB.Items.Add("Cannot select 'ALL' for this function");
            }
            else
            {
                goldenECIResultLB.Items.Add("Can only pack CLNEXT or CLPOC at this time.");
            }
        }

        private void setPB_Click(object sender, EventArgs e)
        {

            // Setup and call setAutoStart IC

            autoStartResultLB.Items.Clear();
            if (commCB.Text == "" || instanceCB.Text == "")
            {
                autoStartResultLB.Items.Add("Please select a Comm And Env");
                return;
            }
            else if (instanceCB.Text == "ALL")
            {
                foreach (string instance in instanceCB.Items)
                {
                    if (instance != "ALL")
                    {
                        setAutoStarts(instance);
                    }
                }
            }
            else
            {
                setAutoStarts(instanceCB.Text);
            }
        }
        private void setAutoStarts(string instance)
        {
            string key = "";
            string userName = "emp$4000";  //emp$462"
            string env = instance + commCB.Text;
            //autoStartResultLB.Items.Clear();
            int enable = 0, restart = 0;
            if (enableCKB.Checked) enable = 1;
            if (restartCKB.Checked) restart = 1;
            string uri = "https://interconn.providence.org/Interconnect-" + commCB.Text + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/setAutoStarts?Enable=" + enable + "&Restart=" + restart;
            Uri myUri = new Uri(uri);
            autoStartResultLB.Items.Add("Working on " + env);
            //var restResponse = HttpClientMgr.SendAsync(HttpMethod.Put, uri, String.Empty, @"application/json", userName, key, env);
            var restResponse = Interconnect.HttpClientManager.SendAsync(HttpMethod.Put, uri, String.Empty, @"application/json", userName, key, env);
            if (restResponse != null)
            {
                var importResult = restResponse.Result.ToString();
                var importStatus = restResponse.Result.StatusCode.ToString();
                if (importStatus != "NotFound")
                {
                    var bodyText = restResponse.Result.Content.ReadAsStringAsync().Result;
                    var r = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(bodyText);
#pragma warning disable CS8604 // Possible null reference argument.
                    if (r != null) autoStartResultLB.Items.Add(r.result);
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
        }

        private bool updateInterfaceAlerts(string env, string action)
        {
            string key = "";
            string userName = "emp$4000";  //emp$462"
            //string action = "D";
            //if (onRB.Checked) action = "E";
            string comm = env.Substring(2);

            string uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/UpdateInterfaceAlerts?Action=" + action;
            Uri myUri = new Uri(uri);
            //var restResponse = HttpClientMgr.SendAsync(HttpMethod.Get, uri, String.Empty, @"application/json", userName, key, env);
            var restResponse = Interconnect.HttpClientManager.SendAsync(HttpMethod.Get, uri, String.Empty, @"application/json", userName, key, env);
            if (restResponse != null)
            {
                var importResult = restResponse.Result.ToString();
                var importStatus = restResponse.Result.StatusCode.ToString();
                if (importStatus != "NotFound")
                {
                    var bodyText = restResponse.Result.Content.ReadAsStringAsync().Result;
                    var r = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(bodyText);
                    if (r == null)
                    {
                        MessageBox.Show("Alert changed failed");
                    }
                    //else
                    //{
                    //    //alertsLB.Items.Add(r.result.ToString());
                    //}
                }
            }
            return false;
        }
        private void changeAlerts(string action, string env)
        {
            //if (instanceCB.Text == "" || commCB.Text == "" || commCB.Text == "EF")
            //{
            //    MessageBox.Show("Please select a non-EF community");
            //    return;
            //}
            //else if (instanceCB.Text == "ALL")
            //{
            //    foreach (string s in instances)
            //    {
            //        updateInterfaceAlerts(s, action);
            //    }
            //}
            //else
            //updateInterfaceAlerts(instanceCB.Text + commCB.Text, action);
            updateInterfaceAlerts(env, action);
        }
        private void onPB_Click(object sender, EventArgs e)
        {
            string action = "E";

            alertDGV.Rows.Clear();
            if (instanceCB.Text == "ALL")
            {
                foreach (string instance in instanceCB.Items)
                {
                    if (instance != "ALL")
                    {
                        changeAlerts(action, instance + commCB.Text);
                        alertStatus(instance + commCB.Text);
                    }
                }
            }
            else
            {
                changeAlerts(action, instanceCB.Text + commCB.Text);
                alertStatus(instanceCB.Text + commCB.Text);
            }

        }

        private void alertsLB_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void offPB_Click(object sender, EventArgs e)
        {
            string action;
            if (oneRB.Checked)
                action = "D1";
            else
                action = "D";

            alertDGV.Rows.Clear();
            if (instanceCB.Text == "ALL")
            {
                foreach (string instance in instanceCB.Items)
                {
                    if (instance != "ALL")
                    {
                        changeAlerts(action, instance + commCB.Text);
                        alertStatus(instance + commCB.Text);
                    }
                }
            }
            else
            {
                changeAlerts(action, instanceCB.Text + commCB.Text);
                alertStatus(instanceCB.Text + commCB.Text);
            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {
        }

        private void esPage_Enter(object sender, EventArgs e)
        {
            /*
                        //for(int i=0; i<66;  i++)

                        envFLOPanel.Controls.Clear();

                        Cursor.Current = Cursors.WaitCursor;

                        foreach (string env in icomms)
                        {
                            createEnv(env);
                        }
                        Cursor.Current = Cursors.Default;
            */
        }
        public Panel createEnv(string env)
        {
            AIPs outs = new AIPs();
            AIPs ins = new AIPs();

            /*
            AIP a = new AIP("182000", "EFPRD <- CLPRD");
            AIPs outs = new AIPs();
            outs.aips.Add(a);
            a = new AIP("182001", "EFPRD -> CLPRD");
            outs.aips.Add(a);
            AIPs ins = new AIPs();
            */
            // get panel stats; runlevel,version
            // 

            //EnvPanel thisPanel = new EnvPanel(env, "UC", "Feb 2022", true, "Include", ins, outs);
            EnvPanel thisPanel = new EnvPanel(env, getEnvStats(env), getLocalEdits(env));

            thisPanel.DoubleClick += panel1_Click;
            envFLOPanel.Controls.Add(thisPanel);
            return thisPanel;
        }

        public EnvStat? getEnvStats(string env)
        {
            EnvStat es;
            string userName = "emp$4000";  //emp$462"
            string key = "";
            //if (commCB.Text == "" || instanceCB.Text == "")
            //{
            //    MessageBox.Show("Please select a Comm And Env");
            //    return null;
            //}
            // Get Allow local edit status

            string comm = env.Substring(2);
            string uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/getEnvStats";
            var restResponse = Interconnect.HttpClientManager.SendAsync(HttpMethod.Get, uri, string.Empty, @"application/json", userName, key, env);
            if (restResponse != null)
            {
                var importResult = restResponse.Result.ToString();
                var importStatus = restResponse.Result.StatusCode.ToString();
                if (importStatus != "NotFound")
                {
                    var bodyText = restResponse.Result.Content.ReadAsStringAsync().Result;
                    var a = Newtonsoft.Json.JsonConvert.DeserializeObject<EnvStat>(bodyText);
                    es = a;
                    return es;
                }
            }
            return null;
        }
        private void goPB_Click(object sender, EventArgs e)
        {
            if (instanceCB.Text != "ALL")
                runCheck(instanceCB.Text + commCB.Text, preCurrentDGV, preSavedDGV, preCompareLB);
            else
                MessageBox.Show("Cannot select ALL for this function");
        }

        private void GetSavedAips(string env, DataGridView dgv)
        {
            //string cstr;
            Microsoft.Data.SqlClient.SqlConnection sqlCon;
            Microsoft.Data.SqlClient.SqlConnectionStringBuilder builder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder()
            {
                DataSource = "sqldb-epicx-test-004.database.windows.net",
                UserID = "servEMFI_SQL_DB1@providence.org",
                InitialCatalog = "EMFI_DEV",
                Pooling = true,
                Authentication = SqlAuthenticationMethod.ActiveDirectoryInteractive,
            };
            //cstr = builder.ConnectionString;

            // Clear grid
            dgv.Columns.Clear();

            using (sqlCon = new Microsoft.Data.SqlClient.SqlConnection(builder.ConnectionString))
            {

                Microsoft.Data.SqlClient.SqlCommand command = new Microsoft.Data.SqlClient.SqlCommand("SELECT * FROM AIP_MODEL where Env_name='" + env + "' ORDER BY Aip_id", sqlCon);
                sqlCon.Open();
                Microsoft.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
                results.Fill(reader);
                //lastLB.Items.Clear();
                dgv.ColumnCount = 10;
                dgv.Columns[0].Name = "#";
                dgv.Columns[1].Name = "Id";
                dgv.Columns[2].Name = "Cid";
                dgv.Columns[3].Name = "Queue";
                dgv.Columns[4].Name = "Name";
                dgv.Columns[5].Name = "PurgeDays";
                dgv.Columns[6].Name = "Port";
                dgv.Columns[7].Name = "IP";
                dgv.Columns[8].Name = "HL7";

                dgv.Columns[0].Width = 35;
                dgv.Columns[1].Width = 60;
                dgv.Columns[2].Width = 60;
                dgv.Columns[3].Width = 60;
                dgv.Columns[4].Width = 200;
                dgv.Columns[5].Width = 100;
                dgv.Columns[6].Width = 100;
                dgv.Columns[7].Width = 150;
                dgv.Columns[8].Width = 60;

                foreach (ICResults res in results.AIPIsOld)
                {
                    var index = dgv.Rows.Add();
                    dgv.Rows[index].Cells["#"].Value = index + 1;
                    dgv.Rows[index].Cells["Id"].Value = res.Id;
                    dgv.Rows[index].Cells["Cid"].Value = res.Cid;
                    dgv.Rows[index].Cells["Queue"].Value = res.Queue;
                    dgv.Rows[index].Cells["Name"].Value = res.Name;
                    dgv.Rows[index].Cells["PurgeDays"].Value = res.PurgeDays;
                    dgv.Rows[index].Cells["Port"].Value = res.Port;
                    dgv.Rows[index].Cells["IP"].Value = res.IP;
                    dgv.Rows[index].Cells["HL7"].Value = res.HL7;
                    //dgv.Rows[index].Cells["AutoStart"].Value = (res.AutoStart == "1" ? true : false);
                }

            }
            sqlCon.Close();
        }

        private void runCheck(string env, DataGridView currentDGV, DataGridView savedDGV, Label compareLB)
        {
            string userName = "emp$4000";  //emp$462"
            string key = "";
            if (commCB.Text == "" || instanceCB.Text == "")
            {
                MessageBox.Show("Please select a Comm And Env");
                return;
            }
            string comm = env.Substring(2);
            string uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/InterfaceCheck";
            //var restResponse = HttpClientMgr.SendAsync(HttpMethod.Get, uri, string.Empty, @"application/json", userName, key, env);
            var restResponse = Interconnect.HttpClientManager.SendAsync(HttpMethod.Get, uri, string.Empty, @"application/json", userName, key, env);
            //ICResults res = null;
            //currentLB.Items.Clear();
            currentDGV.Columns.Clear();
            if (restResponse != null)
            {
                var importResult = restResponse.Result.ToString();
                var importStatus = restResponse.Result.StatusCode.ToString();
                if (importStatus != "NotFound")
                {
                    var bodyText = restResponse.Result.Content.ReadAsStringAsync().Result;
                    var a = Newtonsoft.Json.JsonConvert.DeserializeObject<ICResultsList>(bodyText);

                    results = a;

                    //currentDGV.DataSource = results.AIPIs;
                    currentDGV.ColumnCount = 10;
                    currentDGV.Columns[0].Name = "#";
                    currentDGV.Columns[1].Name = "Id";
                    currentDGV.Columns[2].Name = "Cid";
                    currentDGV.Columns[3].Name = "Queue";
                    currentDGV.Columns[4].Name = "Name";
                    currentDGV.Columns[5].Name = "PurgeDays";
                    currentDGV.Columns[6].Name = "Port";
                    currentDGV.Columns[7].Name = "IP";
                    currentDGV.Columns[8].Name = "HL7";
                    //DataGridViewCheckBoxColumn asCol = new DataGridViewCheckBoxColumn();
                    //{
                    //    asCol.Name = "AutoStart";
                    //    asCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    //    asCol.FlatStyle = FlatStyle.Standard;
                    //    asCol.ThreeState = true;
                    //    asCol.CellTemplate = new DataGridViewCheckBoxCell();
                    //    //asCol.CellTemplate.Style.BackColor = Color.Beige;
                    //}
                    //currentDGV.Columns.Insert(9, asCol);
                    //currentDGV.Columns[8].Name = "AutoStart";

                    currentDGV.Columns[0].Width = 35;
                    currentDGV.Columns[1].Width = 60;
                    currentDGV.Columns[2].Width = 60;
                    currentDGV.Columns[3].Width = 60;
                    currentDGV.Columns[4].Width = 200;
                    currentDGV.Columns[5].Width = 100;
                    currentDGV.Columns[6].Width = 100;
                    currentDGV.Columns[7].Width = 150;
                    currentDGV.Columns[8].Width = 60;
                    //currentDGV.Columns[8].Width = 60;


                    foreach (ICResults res in results.AIPIs)
                    {
                        var index = currentDGV.Rows.Add();
                        currentDGV.Rows[index].Cells["#"].Value = index + 1;
                        currentDGV.Rows[index].Cells["Id"].Value = res.Id;
                        currentDGV.Rows[index].Cells["Cid"].Value = res.Cid;
                        currentDGV.Rows[index].Cells["Queue"].Value = res.Queue;
                        currentDGV.Rows[index].Cells["Name"].Value = res.Name;
                        currentDGV.Rows[index].Cells["PurgeDays"].Value = res.PurgeDays;
                        currentDGV.Rows[index].Cells["Port"].Value = res.Port;
                        currentDGV.Rows[index].Cells["IP"].Value = res.IP;
                        currentDGV.Rows[index].Cells["HL7"].Value = res.HL7;
                        //currentDGV.Rows[index].Cells["AutoStart"].Value = (res.AutoStart == "1" ? true : false);
                    }
                }
            }
            // Now fill saved config

            // ---> Insert new Saved routine call here 

            GetSavedAips(env, savedDGV);

            // Compare current to saved aips
            if (!results.IsEqual())
            {
                compareLB.Text = "Differences found";
                // Select the rows that are different
                int cnt = 0;
                foreach (ICResults res in results.AIPIs)
                {
                    currentDGV.Rows[cnt].Selected = res.Flagged;
                    cnt++;
                }
                cnt = 0;
                foreach (ICResults res in results.AIPIsOld)
                {
                    savedDGV.Rows[cnt].Selected = res.Flagged;
                }
            }
            else compareLB.Text = "No Differences found";
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            EnvPanel panel = (EnvPanel)sender;
            EnvForm thisForm = new EnvForm(panel);
            thisForm.ShowDialog();

        }
        private string setDestinations(string name)
        {
            string[] parts = name.Split("-> ");
            string dest = parts[1];
            if (parts[0].Contains("ALT 1"))
            {
                dest += " ALT 1";
            }
            else if (parts[0].Contains("ALT 2"))
            {
                dest += " ALT 2";
            }
            return (dest);

        }
        private void GetInterfaces()
        {
            if (commCB.Text == "" || instanceCB.Text == "")
            {
                aipResultLB.Items.Add("Select comm and instance.");
                return;
            }
            string comm = commCB.Text;
            string env = instanceCB.Text + commCB.Text;
            string dest;
            string uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/DBD/getEGCInterfaces";
            AIPs r = (AIPs)Interconnect.HttpClientManager.GetHttpData(uri, env, typeof(AIPs));
            int i = 0;
            if (r != null && r.aips != null)
            {
                ///aips = new List<AIP>();
                foreach (AIP a in r.aips)
                {
                    //dest = setDestinations(a.Name);
                    aipCB.Items.Add(a.Id + " - " + a.Name);
                    //aips.Add(a);
                    i++;
                }
            }
            else
            {
                aipResultLB.Items.Add("Interconnect not responding to env: " + env);
            }
        }
        /*
                private void srcCB_SelectedIndexChanged(object sender, EventArgs e)
                {
                    tgtCKB.Items.Clear();
                    string src = ((ComboBox)sender).Text;
                    string wqfName = wqfTagTB.Text;
                    string dest;
                    string comm;
                    if (src.Substring(2, 2) == "SK")
                        comm = src.Substring(2, 4);
                    else
                        comm = src.Substring(2, 3);
                    string uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/DBD/getEGCInterfaces";
                    AIPs r = (AIPs)Interconnect.HttpClientManager.GetHttpData(uri, src, typeof(AIPs));
                    int i = 0;
                    if (r != null && r.aips != null)
                    {
                        aips = new List<AIP>();
                        foreach (AIP a in r.aips)
                        {
                            if (a.Default != "3")  // Hidden 
                            {
                                dest = setDestinations(a.Name);
                                tgtCKB.Items.Add(dest);
                                tgtCKB.SetItemChecked(i, a.Default == "1");
                                aips.Add(a);
                                i++;
                            }
                        }
                    }
                    else
                    {
                        resultLB.Items.Add("Interconnect not responding to env: " + src);
                    }
                }

                private bool getWQFFilename(AIP aip, string env, ref string name, ref long cid)
                {
                    SqlConnection sqlCon;
                    SqlDataReader rdr;
                    bool rtn = true;


                    if (aip.Name.Contains("ALT 1")) env += " ALT 1";
                    else if (aip.Name.Contains("ALT 2")) env += " ALT 2";
                    //string filename = "";

                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    //builder.DataSource = "sqldb-emfi-dev-001.database.windows.net";
                    builder.DataSource = "sqldb-epicx-test-004.database.windows.net";
                    builder.UserID = "servEMFI_SQL_DB1@providence.org";
                    builder.InitialCatalog = "EMFI_TEST";
                    builder.Pooling = true;
                    //builder.Authentication = SqlAuthenticationMethod.ActiveDirectoryPassword;
                    builder.Authentication = SqlAuthenticationMethod.ActiveDirectoryInteractive;

                    using (sqlCon = new Microsoft.Data.SqlClient.SqlConnection(builder.ConnectionString))
                    {
                        sqlCon.Open();
                        SqlCommand sql_cmnd = new SqlCommand("sp_getWqfFile", sqlCon);
                        sql_cmnd.Parameters.AddWithValue("@SendingEnv", SqlDbType.NVarChar).Value = env;
                        sql_cmnd.Parameters.AddWithValue("@DestinationAip", SqlDbType.Int).Value = Int32.Parse(aip.Id);
                        sql_cmnd.CommandType = CommandType.StoredProcedure;
                        rdr = sql_cmnd.ExecuteReader();
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            name = rdr.GetString(0);
                            cid = rdr.GetInt64(1);
                            rtn = true;
                        }
                        else
                        {
                            MessageBox.Show("Selected dest has no config file");
                            rtn = false;
                        }

                        sqlCon.Close();
                    }
                    return rtn;
                }

                private void autoDCPB_Click(object sender, EventArgs e)
                {
                    // Check Inputs
                    if (wqfTagTB.Text == "")
                    {
                        resultLB.Items.Add("Please enter a WQF Tag/Comment.");
                        return;
                    }
                    if (tgtCKB.CheckedIndices == null)
                    {
                        resultLB.Items.Add("Please select destination(s).");
                        return;
                    }
                    // Get Check envs
                    List<string> inputs = new List<string>();

                    // Get File for this environment
                    string aipString = "";
                    string dest;
                    string filename = "";
                    long cid = 0;
                    List<Package> pkgs = new List<Package>();
                    Package thisPkg = null;
                    StatusForm thisStatus;
                    bool gotFile = false;
                    bool found = false;
                    List<string> destinations = new List<string>();

                    this.Cursor = Cursors.WaitCursor;

                    foreach (int indexChecked in tgtCKB.CheckedIndices)
                    {
                        // Determine how many sends we will do based on WQFFile cal
                        foreach (AIP a in aips)
                        {
                            dest = setDestinations(a.Name);

                            if (dest == tgtCKB.Items[indexChecked].ToString())
                            {
                                gotFile = getWQFFilename(a, srcCB.Text, ref filename, ref cid);

                                found = false;
                                if (filename != "")
                                {
                                    destinations.Add(dest);
                                    // Search current packages for this filename
                                    foreach (Package pk in pkgs)
                                    {
                                        if (filename == pk.Filename)
                                        {
                                            pk.AIPs.Add(new AIP(a.Id, dest));
                                            found = true;
                                            break;
                                        }
                                    }
                                    if (!found)
                                    {
                                        thisPkg = new Package(srcCB.Text, filename, cid.ToString());
                                        thisPkg.AIPs.Add(new AIP(a.Id, dest));
                                        pkgs.Add(thisPkg);
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    //Remove version of the same destination
                    for (int d = destinations.Count - 1; d >= 0; d--)
                    {
                        string[] eP1 = destinations[d].Split(" ");
                        for (int dd = 0; dd < destinations.Count; dd++)
                        {
                            string[] eP2 = destinations[dd].Split(" ");
                            if (eP1[0] == eP2[0] && d != dd)
                            {
                                destinations.RemoveAt(d);
                                break;
                            }
                        }
                    }
                    // One last time thru to trim possible ALTs off the survivors
                    for (int d = 0; d < destinations.Count; d++)
                    {
                        string[] delim = { "ALT", "-" };
                        string[] eP1 = destinations[d].Split(delim, StringSplitOptions.None);
                        destinations[d] = eP1[0];
                    }

                    string alt;
                    String uri;
                    string comm = srcCB.Text.Substring(2);
                    List<PackageResult> prs = new List<PackageResult>();
                    foreach (Package pkg in pkgs)
                    {
                        //alt = "Main";
                        //if (pkg.AIPs[0].Name.Contains("ALT 1")) alt = "ALT 1";
                        //if (pkg.AIPs[0].Name.Contains("ALT 2")) alt = "ALT 2";
                        aipString = pkg.AIPString();
                        // Call AutoDCAipTest with pkg.AIPs, pkg.Filename, wqfTagTB.Text
                        // Testing bypass DC /////////////////
                        uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/tools/AutoDCAipTest?aips=" + aipString + "&wqffile=" + pkg.Filename + "&wqfcmt=" + wqfTagTB.Text;
                        PackageResult r = (PackageResult)Interconnect.HttpClientManager.GetHttpData(uri, srcCB.Text, typeof(PackageResult));
                        //PackageResult r = new PackageResult();
                        //r.egaic = "12345";
                        /////////////////////////////////////
                        prs.Add(r);
                    }
                    int cnt = 0;
                    foreach (Package pkg in pkgs)
                    {
                        if (prs[cnt].egaic != null)
                        {
                            alt = "Main";
                            if (pkg.AIPs[0].Name.Contains("ALT 1")) alt = "ALT 1";
                            if (pkg.AIPs[0].Name.Contains("ALT 2")) alt = "ALT 2";
                            thisStatus = new StatusForm(srcCB.Text, prs[cnt].egaic, pkg.Cid, wqfTagTB.Text, destinations, alt);
                            thisStatus.Show();
                        }
                        cnt++;
                    }
                    string src = srcCB.Text.ToString();

                    resultLB.Items.Clear();
                    resultLB.Items.Add("DCing from: " + src);
                    int i = 0;
                    foreach (object itm in tgtCKB.CheckedItems)
                    {
                        resultLB.Items.Add("Tgt[" + i + "]= " + itm.ToString());
                        i++;
                    }
                    this.Cursor = Cursors.Default;
                }

                private void autoEMPPB_Click(object sender, EventArgs e)
                {
                    // Call service to update EMP 150 in the appropriate PRD env
                    string comm = srcCB.Text.Substring(2);
                    string emailAddr = "";
                    string user = "";
                    string cid = "";
                    EMDC_StatusForm thisStatus;
                    string ds = DateTime.Now.ToString("MMddyyyy h:mm tt");
                    switch (srcCB.Text)
                    {
                        case "CLPRD":
                            emailAddr = "clprd_emdc@" + ds;
                            //id = "P372435"; // Christine
                            cid = "1221775";
                            user = "Tamayo, Christine";
                            break;
                        case "AKPRD":
                            emailAddr = "akprd_emdc@" + ds;
                            //id = "P445058"; //COX, JAMES J
                            cid = "15262172";
                            user = "Cox, James J";
                            break;
                        case "OCPRD":
                            emailAddr = "ocprd_emdc@" + ds;
                            //id = "P361509"; // Ray
                            cid = "1224062";
                            user = "Ryuta, Sasaki";
                            break;
                        case "WMPRD":
                            emailAddr = "wmprd_emdc@" + ds;
                            //id = "P516362"; // Aaron
                            cid = "1202522";
                            user = "Cunningham, Aaron";
                            break;

                    }
                    if (emailAddr == "")
                    {
                        resultLB.Items.Add("Please select a PRD environment!");
                    }
                    else
                    {
                        // Create setEMPEmailAddr service

                        string uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/setItem?Ini=EMP&Item=150&Cid=" + cid + "&Value=" + emailAddr;
                        AResult r = (AResult)Interconnect.HttpClientManager.GetHttpData(uri, srcCB.Text, typeof(AResult));
                        if (r.status == "Complete")
                        {
                            thisStatus = new EMDC_StatusForm(srcCB.Text, cid, emailAddr, user);
                            thisStatus.Show();
                        }
                    }
                }

                private void wqfEditPB_Click(object sender, EventArgs e)
                {
                    string cids;

                    // Check Inputs
                    if (srcCB.Text == "")
                    {
                        resultLB.Items.Add("Please select a source.");
                        return;
                    }
                    if (wqfTagTB.Text == "")
                    {
                        resultLB.Items.Add("Please enter a WQF Tag/Comment.");
                        return;
                    }
                    // Get CIDs of all related WQFs
                    cids = get_WQF_CID(srcCB.Text);
                    UpdateWQFs(cids, wqfTagTB.Text);
                }

                private string get_WQF_CID(string name)
                {
                    SqlConnection sqlCon;
                    SqlDataReader rdr;
                    bool rtn = true;
                    string sql;
                    string cidString = "";

                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    //builder.DataSource = "sqldb-emfi-dev-001.database.windows.net";
                    builder.DataSource = "sqldb-epicx-test-004.database.windows.net";
                    builder.UserID = "servEMFI_SQL_DB1@providence.org";
                    builder.InitialCatalog = "EMFI_TEST";
                    builder.Pooling = true;
                    //builder.Authentication = SqlAuthenticationMethod.ActiveDirectoryPassword;
                    builder.Authentication = SqlAuthenticationMethod.ActiveDirectoryInteractive;

                    using (sqlCon = new Microsoft.Data.SqlClient.SqlConnection(builder.ConnectionString))
                    {
                        sqlCon.Open();
                        sql = "select distinct WQF_cid from dc_test_plan where Send_env like '" + srcCB.Text + "%'";
                        SqlCommand sql_cmnd = new SqlCommand(sql, sqlCon);
                        sql_cmnd.CommandType = CommandType.Text;
                        rdr = sql_cmnd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                cidString += rdr.GetInt64(0).ToString() + ",";
                            }
                        }
                        else
                        {
                            MessageBox.Show("No WQFs found for this Source.");
                        }
                        sqlCon.Close();
                    }
                    return cidString.Substring(0, cidString.Length - 1);

                }
                private void UpdateWQFs(string cids, string comment)
                {
                    string comm = srcCB.Text.Substring(2);
                    string uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/UpdateWQFs?Comment=" + comment + "&Cids=" + cids;
                    SameResult r = (SameResult)Interconnect.HttpClientManager.GetHttpData(uri, srcCB.Text, typeof(SameResult));
                    if (r.Result == "1")
                    {
                        resultLB.Items.Add("WQFs set to " + wqfTagTB.Text);
                    }
                    else
                    {
                        resultLB.Items.Add("WQF set failed");
                    }
                }
        */
        private void listPB_Click(object sender, EventArgs e)
        {
            GetInterfaces();
        }

        private bool getLocalEdits(string instance)
        {
            bool cEdit = false;
            string key = "";
            string userName = "emp$4000";  //emp$462"
            //string env = instance + commCB.Text;
            string comm = instance.Substring(2);
            string uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/CheckECIEdits";
            Uri myUri = new Uri(uri);
            var restResponse = Interconnect.HttpClientManager.SendAsync(HttpMethod.Get, uri, String.Empty, @"application/json", userName, key, instance);
            if (restResponse != null)
            {
                var importResult = restResponse.Result.ToString();
                var importStatus = restResponse.Result.StatusCode.ToString();
                if (importStatus != "NotFound")
                {
                    var bodyText = restResponse.Result.Content.ReadAsStringAsync().Result;
                    var r = Newtonsoft.Json.JsonConvert.DeserializeObject<ECIResult>(bodyText);
                    if (r != null && r.Result != null)
                    {
                        //eciResultsLB.Items.Add(r.Result);
                        string s = r.Result;
                        cEdit = (s.Substring(s.Length - 2) == "No");
                        //localEditsCKB.Checked = cEdit;
                        return cEdit;
                    }
                    //else
                    //{
                    //    return null;
                    //    //eciResultsLB.Items.Add("No results found for: " + env + ". Contact the admin.");
                    //}
                }
            }
            return false;
        }

        private void unpackECIPB_Click(object sender, EventArgs e)
        {
            eciResultsLB.Items.Clear();
            if (commCB.Text == "" || instanceCB.Text == "")
            {
                autoStartResultLB.Items.Add("Please select a Comm And Env");
                return;
            }
            else if (instanceCB.Text == "ALL")
            {
                DialogResult res = MessageBox.Show("Are you sure you want to overwrite the ECI in " + instanceCB.Text + commCB.Text, "Unpack ECI", MessageBoxButtons.OKCancel);
                if (res == DialogResult.OK)
                {
                    foreach (string instance in instanceCB.Items)
                    {
                        if (instance != "ALL")
                        {
                            loadETAN(instance, fileNameTB.Text);
                        }
                    }
                }
            }
            else if (commCB.Text == "PRD")
            {
                MessageBox.Show("You cannot currently LoadECIs in PRDs. Please select another community");
            }
            else
            {
                DialogResult res = MessageBox.Show("Are you sure you want to overwrite the ECI in " + instanceCB.Text + commCB.Text, "Unpack ECI", MessageBoxButtons.OKCancel);
                if (res == DialogResult.OK)
                    loadETAN(instanceCB.Text, fileNameTB.Text);
            }

        }
        private void loadETAN(string instance, string filename)
        {

            string key = "";
            string userName = "emp$4000";  //emp$462"
            string env = instance + commCB.Text;
            string uri = "https://interconn.providence.org/Interconnect-" + commCB.Text + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/loadGoldenECI?Filename=" + filename;
            Uri myUri = new Uri(uri);
            eciResultsLB.Items.Add("Working on " + env);
            //var restResponse = HttpClientMgr.SendAsync(HttpMethod.Get, uri, String.Empty, @"application/json", userName, key, env);
            var restResponse = Interconnect.HttpClientManager.SendAsync(HttpMethod.Get, uri, String.Empty, @"application/json", userName, key, env);
            if (restResponse != null)
            {
                var importResult = restResponse.Result.ToString();
                var importStatus = restResponse.Result.StatusCode.ToString();
                if (importStatus != "NotFound")
                {
                    var bodyText = restResponse.Result.Content.ReadAsStringAsync().Result;
                    var r = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(bodyText);
                    if (r != null) eciResultsLB.Items.Add(r.result);
                }
            }
        }

        private bool getSetLocalEdits(string instance, string onOff)
        {
            bool cEdit = false;
            string key = "";
            string userName = "emp$4000";  //emp$462"
            string env = instance + commCB.Text;
            string uri = "https://interconn.providence.org/Interconnect-" + commCB.Text + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/CheckECIEdits?OnOff=" + onOff;
            Uri myUri = new Uri(uri);
            //var restResponse = HttpClientMgr.SendAsync(HttpMethod.Get, uri, String.Empty, @"application/json", userName, key, env);
            var restResponse = Interconnect.HttpClientManager.SendAsync(HttpMethod.Get, uri, String.Empty, @"application/json", userName, key, env);
            if (restResponse != null)
            {
                var importResult = restResponse.Result.ToString();
                var importStatus = restResponse.Result.StatusCode.ToString();
                if (importStatus != "NotFound")
                {
                    var bodyText = restResponse.Result.Content.ReadAsStringAsync().Result;
                    var r = Newtonsoft.Json.JsonConvert.DeserializeObject<ECIResult>(bodyText);
                    if (r != null && r.Result != null)
                    {
                        eciResultLB.Items.Add(r.Result);
                        string s = r.Result;
                        cEdit = (s.Substring(s.Length - 3) == "Yes");
                        localEditsCKB.Checked = cEdit;
                        //localEditsCKB.Checked = (r.canEdit == 1);
                        //return (r.canEdit == 1);
                        return cEdit;
                    }
                    else
                    {
                        eciResultLB.Items.Add("No results found for: " + env + ". Contact the admin.");
                    }
                }
            }
            return false;
        }

        private void InstanceNameSubmitPB_Click(object sender, EventArgs e)
        {
            //string name = "";
            eciResultLB.Items.Clear();
            if (commCB.Text == "" || instanceCB.Text == "")
            {
                eciResultLB.Items.Add("Please select a Comm And Env");
                return;
            }
            Button thisButton = (Button)sender;

            if (instanceCB.Text == "ALL")
            {
                foreach (string instance in instanceCB.Items)
                {
                    if (instance != "ALL")
                    {
                        if (thisButton.Name == instanceNameStatusPB.Name)
                        {
                            getSetInstanceName(instance, "");
                        }
                        else
                        {
                            if (getSetLocalEdits(instance, ""))
                                getSetInstanceName(instance, instanceTB.Text);
                            else
                            {
                                MessageBox.Show("Unlock local edits for " + instance + commCB.Text);
                                instanceTB.Clear();
                            }
                        }
                    }
                }
            }
            else
            {
                if (thisButton.Name == instanceNameStatusPB.Name)
                {
                    getSetInstanceName(instanceCB.Text, "");
                }
                else
                {
                    if (getSetLocalEdits(instanceCB.Text, ""))
                        getSetInstanceName(instanceCB.Text, instanceTB.Text);
                    else
                    {
                        MessageBox.Show("Unlock local edits");
                        instanceTB.Clear();
                    }
                }
            }
        }

        private void getSetInstanceName(string instance, string name)
        {
            string key = "";
            string userName = "emp$4000";  //emp$462"
            string env = instance + commCB.Text;
            string uri = "https://interconn.providence.org/Interconnect-" + commCB.Text + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/GetSetInstanceName?Name=" + name;
            Uri myUri = new Uri(uri);
            //var restResponse = HttpClientMgr.SendAsync(HttpMethod.Get, uri, String.Empty, @"application/json", userName, key, env);
            var restResponse = Interconnect.HttpClientManager.SendAsync(HttpMethod.Get, uri, String.Empty, @"application/json", userName, key, env);
            if (restResponse != null)
            {
                var importResult = restResponse.Result.ToString();
                var importStatus = restResponse.Result.StatusCode.ToString();
                if (importStatus != "NotFound")
                {
                    var bodyText = restResponse.Result.Content.ReadAsStringAsync().Result;
                    var r = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(bodyText);
                    if (r != null)
                    {
                        eciResultLB.Items.Add(r.result);
                        //instanceTB.Text = r.result.Substring(25);
                    }
                }
            }
        }
        private void AllowLocalEditPB_Click(object sender, EventArgs e)
        {
            string onOff = "";
            Button thisButton = (Button)sender;

            eciResultLB.Items.Clear();
            if (commCB.Text == "" || instanceCB.Text == "")
            {
                eciResultLB.Items.Add("Please select a Comm And Env");
                return;
            }
            if (thisButton.Name == eciLockPB.Name)
            {
                onOff = "0";
            }
            else if (thisButton.Name == eciUnlockPB.Name)
            {
                onOff = "1";
            }
            if (instanceCB.Text == "ALL")
            {
                foreach (string instance in instanceCB.Items)
                {
                    if (instance != "ALL")
                    {
                        getSetLocalEdits(instance, onOff);
                    }
                }
                localEditsCKB.Checked = (onOff == "1");
            }
            else
            {
                getSetLocalEdits(instanceCB.Text, onOff);
                localEditsCKB.Checked = (onOff == "1");
            }
        }

        private void instanceNameStatusPB_Click(object sender, EventArgs e)
        {
            eciResultLB.Items.Clear();
            if (commCB.Text == "" || instanceCB.Text == "")
            {
                eciResultLB.Items.Add("Please select a Comm And Env");
                return;
            }
            if (instanceCB.Text == "ALL")
            {
                foreach (string instance in instanceCB.Items)
                {
                    if (instance != "ALL")
                    {
                        getSetInstanceName(instance, "");
                    }
                }
            }
            else
            {
                getSetInstanceName(instanceCB.Text, "");
            }

        }

        private void label100_Click(object sender, EventArgs e)
        {

        }
        private void alertStatus(string env)
        {
            string key = "";
            int oneMany = 0;
            string userName = "emp$4000";  //emp$462"

            //if (instanceCB.Text == "" || commCB.Text == "")
            //{
            //    MessageBox.Show("Please select an environment.");
            //    return;
            //}

            string comm = env.Substring(2);


            string uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/alertStatus";
            Uri myUri = new Uri(uri);
            var restResponse = Interconnect.HttpClientManager.SendAsync(HttpMethod.Get, uri, String.Empty, @"application/json", userName, key, env);
            if (restResponse != null)
            {
                var importResult = restResponse.Result.ToString();
                var importStatus = restResponse.Result.StatusCode.ToString();
                if (importStatus != "NotFound")
                {
                    var bodyText = restResponse.Result.Content.ReadAsStringAsync().Result;
                    var r = Newtonsoft.Json.JsonConvert.DeserializeObject<Alerts>(bodyText);


                    if (r == null || r.alerts == null)
                    {
                        MessageBox.Show("Alert status failed");
                    }
                    else
                    {
                        //alertDGV.Rows.Clear();
                        foreach (Alert a in r.alerts)
                        {
                            var index = alertDGV.Rows.Add();
                            alertDGV.Rows[index].Cells["alertNm"].Value = env + " - " + a.Id;
                            alertDGV.Rows[index].Cells["disabled"].Value = (a.Disabled == "Y");
                            if (a.Disabled == "Y") oneMany += 1;
                        }
                        if (oneMany == r.alerts.Count) allRB.Checked = true;
                        else if (oneMany == 1) oneRB.Checked = true;
                        else
                        {
                            allRB.Checked = false;
                            oneRB.Checked = false;
                        }
                    }
                }
            }
        }

        private void statusPB_Click(object sender, EventArgs e)
        {
            if (instanceCB.Text == "" || commCB.Text == "")
            {
                MessageBox.Show("Please select an environment.");
                return;
            }
            alertDGV.Rows.Clear();
            if (instanceCB.Text == "ALL")
            {
                foreach (string instance in instanceCB.Items)
                {
                    if (instance != "ALL")
                    {
                        alertStatus(instance + commCB.Text);
                    }
                }
            }
            else
            {
                alertStatus(instanceCB.Text + commCB.Text);
            }
        }

        private void leapSubmitPB_Click(object sender, EventArgs e)
        {
            string key = "";
            string userName = "emp$4000";  //emp$462"
            if (instanceCB.Text == "" || commCB.Text == "")
            {
                MessageBox.Show("Please select an environment.");
                return;
            }

            if (ibAipTB.Text == "" || ibIniTB.Text == "")
            {
                MessageBox.Show("Please select an inbound interface and INI.");
                return;
            }

            string env = instanceCB.Text + commCB.Text;
            int commit = previewCKB.Checked ? 0 : 1;
            string uri = "https://interconn.providence.org/Interconnect-" + commCB.Text + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/leapfrog?Commit=" + commit + "&Aip=" + ibAipTB + "&Ini=" + ibIniTB.Text;
            Uri myUri = new Uri(uri);
            var restResponse = Interconnect.HttpClientManager.SendAsync(HttpMethod.Get, uri, String.Empty, @"application/json", userName, key, env);
            if (restResponse != null)
            {
                var importResult = restResponse.Result.ToString();
                var importStatus = restResponse.Result.StatusCode.ToString();
                if (importStatus != "NotFound")
                {
                    var bodyText = restResponse.Result.Content.ReadAsStringAsync().Result;
                    var r = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageResult>(bodyText);
                    if (r != null && r.Result != null)
                    {
                        resultLB.Items.Add("Result: " + r.Result);
                        resultLB.Items.Add("Count: " + r.Count);
                    }
                }
            }
        }

        private void envPanelGoPB_Click(object sender, EventArgs e)
        {
            envFLOPanel.Controls.Clear();
            Cursor.Current = Cursors.WaitCursor;
            if (envPanelCB.SelectedIndex == 0) icomms = pcomms;
            else if (envPanelCB.SelectedIndex == 1) icomms = tcomms;
            else icomms = acomms;
            foreach (string env in icomms)
            {
                createEnv(env);
            }
            Cursor.Current = Cursors.Default;
        }

        private void shoveAheadSubmitPB_Click(object sender, EventArgs e)
        {
            string key = "";
            string userName = "emp$4000";  //emp$462"
            if (instanceCB.Text == "" || commCB.Text == "")
            {
                MessageBox.Show("Please select an environment.");
                return;
            }

            if (obAipTB.Text == "" || obIniTB.Text == "")
            {
                MessageBox.Show("Please select an inbound interface and INI.");
                return;
            }

            string env = instanceCB.Text + commCB.Text;
            int commit = obPreviewCKB.Checked ? 0 : 1;
            string uri = "https://interconn.providence.org/Interconnect-" + commCB.Text + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/shoveAhead?Commit=" + commit + "&Aip=" + ibAipTB + "&Ini=" + ibIniTB.Text;
            Uri myUri = new Uri(uri);
            var restResponse = Interconnect.HttpClientManager.SendAsync(HttpMethod.Get, uri, String.Empty, @"application/json", userName, key, env);
            if (restResponse != null)
            {
                var importResult = restResponse.Result.ToString();
                var importStatus = restResponse.Result.StatusCode.ToString();
                if (importStatus != "NotFound")
                {
                    var bodyText = restResponse.Result.Content.ReadAsStringAsync().Result;
                    var r = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageResult>(bodyText);
                    if (r != null && r.Result != null)
                    {
                        resultLB.Items.Add("Result: " + r.Result);
                        resultLB.Items.Add("Count: " + r.Count);
                    }
                }
            }
        }

        private void aipCreatePB_Click(object sender, EventArgs e)
        {

        }

        private void srcEnvCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            //            Row row = new Row(1,false,"182000","EFPRD->CLPRD","61567");

            //srcAIPsDGV.Rows.Add(row);

            string comm = srcEnvCB.Text.Substring(2);
            string uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/DBD/CheckQueues";

            lAIPs a = (lAIPs)Interconnect.HttpClientManager.GetHttpData(uri, srcEnvCB.Text, typeof(lAIPs));

            uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/ListPorts?emfiOnly=1";
            Ports ports = (Ports)Interconnect.HttpClientManager.GetHttpData(uri, srcEnvCB.Text, typeof(Ports));

            int count = 0;
            foreach (lAIP aip in a.aips)
            {
                for (int i = 0; i < ports.ports.Count; i++)
                {
                    if (aip.Id == ports.ports[i].Id)
                    {
                        aip.Port = ports.ports[i].Port; break;
                    }
                }
                srcAIPsDGV.Rows.Add();
                DataGridViewRow dr = srcAIPsDGV.Rows[count];
                dr.Cells[0].Value = count + 1;
                dr.Cells[1].Value = false;
                dr.Cells[2].Value = aip.Id;
                dr.Cells[4].Value = aip.Name;
                dr.Cells[5].Value = aip.Port;
                count++;
            }
            //srcAIPsDGV.DataSource = a.aips;


            //if (a != null)
            //    return a.aips;
            //else
            //    return null;
        }

        private void destEnvLB_Click(object sender, EventArgs e)
        {

        }


        /*
      //private void aipCB_SelectedIndexChanged(object sender, EventArgs e)
      //{
      //    GetInterfaces();
      //}

      //private void dequePg_Enter(object sender, EventArgs e)
      //{
      //    GetInterfaces();
      //}
*/
        /*
private void clearStats()
{
  empTB.Text = "";
  cidTB.Text = "";
  nameTB.Text = "";
  rehomePB.Enabled = false;
  resultLB.Items.Clear();
  snIdTB.Text = "";
}
private void validatePB_Click(object sender, EventArgs e)
{
  // Get emp, Get cid;
  string id = empTB.Text;
  string cid = cidTB.Text;
  //string env = "CLMCK";
  string env = "CLPRD";
  string uri;
  // Bypassing security for now. Set optionToolStripMenu item to Checked in the designer to default set security
  //if (hasSecurity) strict = !optionsToolStripMenuItem.Checked;
  string comm = env.Substring(2);
  if (env != "")
  {
      if (id != null && id != "")
          uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/userLookup?Id=" + id;
      else
          uri = "https://interconn.providence.org/Interconnect-" + comm + "MEC-GK/api/PSJH/2020/EMFI/TOOLS/userLookup?Cid=" + cid;
      //string userName = "emp$4000";  //emp$462"
      //string key = "";
      EmpHome r = (EmpHome)Interconnect.HttpClientManager.GetHttpData(uri, env, typeof(EmpHome));
      if (r != null && (r.Ido != null || r.Cido != null))
      {
          if (strict)
          {
              if (r.Ido == empTB.Text &&
                  r.Cido == cidTB.Text &&
                  r.Name == nameTB.Text)
              {
                  fromCB.SelectedValue = r.Home;
                  resultLB.Items.Clear();
                  rehomePB.Enabled = true;
              }
              else
              {
                  resultLB.Items.Add("Inputs do not match");
              }
          }
          else
          {
              empTB.Text = r.Ido;
              cidTB.Text = r.Cido;
              nameTB.Text = r.Name;
              fromCB.SelectedValue = r.Home;
              resultLB.Items.Clear();
              rehomePB.Enabled = true;
          }
      }
      else
      {
          clearStats();
          resultLB.Items.Add(r.Status);
      }
  }
  else
      resultLB.Items.Add("Select destination home");
}
*/
    }
}