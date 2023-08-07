namespace EMFI_Tools
{
    partial class StatusForm
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
            insFloLO = new FlowLayoutPanel();
            pkgStatusTB = new TextBox();
            label1 = new Label();
            buildComparePB = new Button();
            SuspendLayout();
            // 
            // insFloLO
            // 
            insFloLO.FlowDirection = FlowDirection.TopDown;
            insFloLO.Location = new Point(10, 63);
            insFloLO.Name = "insFloLO";
            insFloLO.Size = new Size(784, 123);
            insFloLO.TabIndex = 107;
            // 
            // pkgStatusTB
            // 
            pkgStatusTB.Location = new Point(145, 20);
            pkgStatusTB.Name = "pkgStatusTB";
            pkgStatusTB.Size = new Size(320, 27);
            pkgStatusTB.TabIndex = 106;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 22);
            label1.Name = "label1";
            label1.Size = new Size(110, 20);
            label1.TabIndex = 105;
            label1.Text = "Package Status:";
            // 
            // buildComparePB
            // 
            buildComparePB.DialogResult = DialogResult.OK;
            buildComparePB.Location = new Point(471, 19);
            buildComparePB.Name = "buildComparePB";
            buildComparePB.Size = new Size(136, 29);
            buildComparePB.TabIndex = 104;
            buildComparePB.Text = "Build Compare";
            buildComparePB.UseVisualStyleBackColor = true;
            buildComparePB.Click += buildComparePB_Click;
            // 
            // StatusForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 204);
            Controls.Add(insFloLO);
            Controls.Add(pkgStatusTB);
            Controls.Add(label1);
            Controls.Add(buildComparePB);
            Name = "StatusForm";
            Text = "StatusForm";
            FormClosing += StatusForm_FormClosing;
            Load += StatusForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel insFloLO;
        private TextBox pkgStatusTB;
        private Label label1;
        private Button buildComparePB;
    }
}