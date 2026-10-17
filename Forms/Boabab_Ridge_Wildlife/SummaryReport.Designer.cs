namespace Boabab_Ridge_Wildlife
{
    partial class frmSummaryReport
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
            panel1 = new Panel();
            label1 = new Label();
            rtbReport = new RichTextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ScrollBar;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(rtbReport);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(792, 475);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(273, 48);
            label1.Name = "label1";
            label1.Size = new Size(199, 31);
            label1.TabIndex = 1;
            label1.Text = "Summary Report";
            // 
            // rtbReport
            // 
            rtbReport.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbReport.Location = new Point(68, 99);
            rtbReport.Name = "rtbReport";
            rtbReport.ReadOnly = true;
            rtbReport.Size = new Size(676, 329);
            rtbReport.TabIndex = 0;
            rtbReport.Text = "";
            // 
            // frmSummaryReport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(816, 499);
            Controls.Add(panel1);
            Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "frmSummaryReport";
            Text = "Summary Report";
            Load += frmSummaryReport_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private RichTextBox rtbReport;
    }
}