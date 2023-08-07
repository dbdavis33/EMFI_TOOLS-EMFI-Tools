namespace EMFI_Tools
{
    partial class EnvForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            envTB = new TextBox();
            versionTB = new TextBox();
            label2 = new Label();
            runLevelTB = new TextBox();
            label3 = new Label();
            includeTB = new TextBox();
            label4 = new Label();
            label5 = new Label();
            editLockCB = new CheckBox();
            label6 = new Label();
            dcoutDGV = new DataGridView();
            dcinDGV = new DataGridView();
            label7 = new Label();
            closePB = new Button();
            ((System.ComponentModel.ISupportInitialize)dcoutDGV).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dcinDGV).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(35, 20);
            label1.TabIndex = 0;
            label1.Text = "Env:";
            // 
            // envTB
            // 
            envTB.Location = new Point(45, 15);
            envTB.Name = "envTB";
            envTB.ReadOnly = true;
            envTB.Size = new Size(98, 27);
            envTB.TabIndex = 1;
            // 
            // versionTB
            // 
            versionTB.Location = new Point(209, 15);
            versionTB.Name = "versionTB";
            versionTB.ReadOnly = true;
            versionTB.Size = new Size(125, 27);
            versionTB.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(146, 18);
            label2.Name = "label2";
            label2.Size = new Size(60, 20);
            label2.TabIndex = 2;
            label2.Text = "Version:";
            // 
            // runLevelTB
            // 
            runLevelTB.Location = new Point(422, 15);
            runLevelTB.Name = "runLevelTB";
            runLevelTB.ReadOnly = true;
            runLevelTB.Size = new Size(65, 27);
            runLevelTB.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(341, 18);
            label3.Name = "label3";
            label3.Size = new Size(75, 20);
            label3.TabIndex = 4;
            label3.Text = "Run Level:";
            // 
            // includeTB
            // 
            includeTB.Location = new Point(565, 15);
            includeTB.Name = "includeTB";
            includeTB.ReadOnly = true;
            includeTB.Size = new Size(125, 27);
            includeTB.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(494, 18);
            label4.Name = "label4";
            label4.Size = new Size(67, 20);
            label4.TabIndex = 6;
            label4.Text = "Incl/Excl:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(696, 18);
            label5.Name = "label5";
            label5.Size = new Size(68, 20);
            label5.TabIndex = 8;
            label5.Text = "EditLock:";
            // 
            // editLockCB
            // 
            editLockCB.AutoSize = true;
            editLockCB.Location = new Point(769, 21);
            editLockCB.Name = "editLockCB";
            editLockCB.Size = new Size(18, 17);
            editLockCB.TabIndex = 9;
            editLockCB.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 57);
            label6.Name = "label6";
            label6.Size = new Size(66, 20);
            label6.TabIndex = 10;
            label6.Text = "DC Outs:";
            // 
            // dcoutDGV
            // 
            dcoutDGV.AllowUserToAddRows = false;
            dcoutDGV.AllowUserToDeleteRows = false;
            dcoutDGV.AllowUserToResizeRows = false;
            dcoutDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dcoutDGV.Location = new Point(16, 80);
            dcoutDGV.Name = "dcoutDGV";
            dcoutDGV.RowHeadersWidth = 51;
            dcoutDGV.RowTemplate.Height = 29;
            dcoutDGV.Size = new Size(375, 242);
            dcoutDGV.TabIndex = 11;
            // 
            // dcinDGV
            // 
            dcinDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dcinDGV.Location = new Point(413, 80);
            dcinDGV.Name = "dcinDGV";
            dcinDGV.RowHeadersWidth = 51;
            dcinDGV.RowTemplate.Height = 29;
            dcinDGV.Size = new Size(374, 242);
            dcinDGV.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(413, 57);
            label7.Name = "label7";
            label7.Size = new Size(54, 20);
            label7.TabIndex = 12;
            label7.Text = "DC Ins:";
            // 
            // closePB
            // 
            closePB.Location = new Point(693, 345);
            closePB.Name = "closePB";
            closePB.Size = new Size(94, 29);
            closePB.TabIndex = 14;
            closePB.Text = "Close";
            closePB.UseVisualStyleBackColor = true;
            closePB.Click += closePB_Click;
            // 
            // EnvForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(811, 386);
            Controls.Add(closePB);
            Controls.Add(dcinDGV);
            Controls.Add(label7);
            Controls.Add(dcoutDGV);
            Controls.Add(label6);
            Controls.Add(editLockCB);
            Controls.Add(label5);
            Controls.Add(includeTB);
            Controls.Add(label4);
            Controls.Add(runLevelTB);
            Controls.Add(label3);
            Controls.Add(versionTB);
            Controls.Add(label2);
            Controls.Add(envTB);
            Controls.Add(label1);
            Name = "EnvForm";
            Text = "EnvForm";
            ((System.ComponentModel.ISupportInitialize)dcoutDGV).EndInit();
            ((System.ComponentModel.ISupportInitialize)dcinDGV).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox envTB;
        private TextBox versionTB;
        private Label label2;
        private TextBox runLevelTB;
        private Label label3;
        private TextBox includeTB;
        private Label label4;
        private Label label5;
        private CheckBox editLockCB;
        private Label label6;
        private DataGridView dcoutDGV;
        private DataGridView dcinDGV;
        private Label label7;
        private Button closePB;
    }
}