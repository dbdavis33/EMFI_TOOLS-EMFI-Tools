using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EMFI_Tools
{
    public class EnvPanel : Panel
    {
        public string Env { get; set; }
        public string RunLevel { get; set; }
        public string Version { get; set; }
        public bool EditLock { get; set; }
        public string Include { get; set; }
        public AIPs InAIPs { get; set; }
        public AIPs OutAIPs { get; set; }

        public EnvPanel(string env,EnvStat es, bool localEdit)
        {
            if (es != null)
            {
                RunLevel = es.RunLevel;
                Version = es.Version;
                Include = es.Include;
                EditLock = localEdit;
            }

            Label envLB = new Label();
            Label runLevelLB = new Label();
            Label versionLB = new Label();
            Label editLockLB = new Label();
            Label includeLB = new Label();

            this.Size = new System.Drawing.Size(160, 150);
            Font font = new Font("Times New Roman", 12.0f, FontStyle.Bold);
            envLB.Size = new Size(120, 20);
            envLB.Font = font;
            envLB.ForeColor = Color.White;
            envLB.Text = env;
            Controls.Add(envLB);

            // Run Level
            runLevelLB.Text = "RunLevel: " + RunLevel;
            runLevelLB.Location = new Point(2, 30);
            runLevelLB.ForeColor = Color.White;
            Controls.Add(runLevelLB);

            // Version
            versionLB.Text = "Ver: " + Version;
            versionLB.Location = new Point(2, 60);
            versionLB.Size = new Size(150, 20);
            versionLB.ForeColor = Color.White;
            Controls.Add(versionLB);

            // Edit locked
            editLockLB.Text = "Local Edits?: " + (localEdit!=true);
            editLockLB.Location = new Point(2, 90);
            editLockLB.Size = new Size(150, 20);
            editLockLB.ForeColor = Color.White;
            Controls.Add(editLockLB);

            // Include/Exclude
            includeLB.Text = "Incl/Excl: " + Include;
            includeLB.Location = new Point(2, 120);
            includeLB.ForeColor = Color.White;
            Controls.Add(includeLB);

            BackColor = Color.Green;
            if (RunLevel!="UA")
                BackColor = Color.Blue;
            if (EditLock==false)
                BackColor = Color.Red;

            if (Version!="November 2022")
                BackColor = Color.Blue;
            BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;


        }
        public EnvPanel(string env, string runlevel, string version, bool editLock, string include, AIPs ins, AIPs outs)
        {
            Env = env;
            RunLevel = runlevel;
            Version = version;
            EditLock = editLock;
            Include = include;

            Label envLB = new Label();
            Label runLevelLB = new Label();
            Label versionLB = new Label();
            Label editLockLB = new Label();  
            Label includeLB = new Label();

            this.Size = new System.Drawing.Size(160, 150);
            Font font = new Font("Times New Roman", 12.0f, FontStyle.Bold);
            envLB.Size = new Size(120, 20);
            envLB.Font = font;
            envLB.ForeColor = Color.White;
            envLB.Text = env;
            Controls.Add(envLB);

            // Run Level
            runLevelLB.Text = "RunLevel: " + runlevel;
            runLevelLB.Location = new Point(2, 30);
            runLevelLB.ForeColor = Color.White;
            Controls.Add(runLevelLB);

            // Version
            versionLB.Text = "Ver: "+ version;
            versionLB.Location = new Point(2, 60);
            versionLB.ForeColor = Color.White;
            Controls.Add(versionLB);

            // Edit locked
            editLockLB.Text = "Locked: " + editLock;
            editLockLB.Location = new Point(2, 90);
            editLockLB.ForeColor = Color.White;
            Controls.Add(editLockLB);

            // Include/Exclude
            includeLB.Text = "Incl/Excl: " + include;
            includeLB.Location = new Point(2, 120);
            includeLB.ForeColor = Color.White;
            Controls.Add(includeLB);

            if (env == "SANDBOX2")
                BackColor = Color.Red;
            else
                BackColor = Color.Green;
            BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        }
    }
}
