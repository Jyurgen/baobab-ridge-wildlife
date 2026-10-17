namespace Boabab_Ridge_Wildlife
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txbAnimalID = new TextBox();
            txbName = new TextBox();
            txbSpecies = new TextBox();
            txbAge = new TextBox();
            txbRecScore = new TextBox();
            btnAdd = new Button();
            btnView = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnSummaryReport = new Button();
            dgvData = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 51);
            label1.Name = "label1";
            label1.Size = new Size(68, 16);
            label1.TabIndex = 0;
            label1.Text = "Animal ID:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 96);
            label2.Name = "label2";
            label2.Size = new Size(43, 16);
            label2.TabIndex = 1;
            label2.Text = "Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(28, 133);
            label3.Name = "label3";
            label3.Size = new Size(53, 16);
            label3.TabIndex = 2;
            label3.Text = "Species:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(28, 170);
            label4.Name = "label4";
            label4.Size = new Size(32, 16);
            label4.TabIndex = 3;
            label4.Text = "Age:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(28, 209);
            label5.Name = "label5";
            label5.Size = new Size(105, 16);
            label5.TabIndex = 4;
            label5.Text = "Recovery Score: ";
            // 
            // txbAnimalID
            // 
            txbAnimalID.Location = new Point(139, 48);
            txbAnimalID.Name = "txbAnimalID";
            txbAnimalID.Size = new Size(100, 23);
            txbAnimalID.TabIndex = 5;
            // 
            // txbName
            // 
            txbName.Location = new Point(139, 93);
            txbName.Name = "txbName";
            txbName.Size = new Size(100, 23);
            txbName.TabIndex = 7;
            // 
            // txbSpecies
            // 
            txbSpecies.Location = new Point(139, 130);
            txbSpecies.Name = "txbSpecies";
            txbSpecies.Size = new Size(100, 23);
            txbSpecies.TabIndex = 8;
            // 
            // txbAge
            // 
            txbAge.Location = new Point(139, 167);
            txbAge.Name = "txbAge";
            txbAge.Size = new Size(100, 23);
            txbAge.TabIndex = 9;
            // 
            // txbRecScore
            // 
            txbRecScore.Location = new Point(139, 202);
            txbRecScore.Name = "txbRecScore";
            txbRecScore.Size = new Size(100, 23);
            txbRecScore.TabIndex = 10;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(62, 254);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 32);
            btnAdd.TabIndex = 11;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnView
            // 
            btnView.Location = new Point(483, 445);
            btnView.Name = "btnView";
            btnView.Size = new Size(127, 32);
            btnView.TabIndex = 12;
            btnView.Text = "View All";
            btnView.UseVisualStyleBackColor = true;
            btnView.Click += btnView_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(62, 322);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(127, 32);
            btnUpdate.TabIndex = 13;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(62, 384);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(127, 32);
            btnDelete.TabIndex = 14;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnSummaryReport
            // 
            btnSummaryReport.Location = new Point(62, 445);
            btnSummaryReport.Name = "btnSummaryReport";
            btnSummaryReport.Size = new Size(127, 32);
            btnSummaryReport.TabIndex = 15;
            btnSummaryReport.Text = "Summary Report";
            btnSummaryReport.UseVisualStyleBackColor = true;
            btnSummaryReport.Click += btnSummaryReport_Click;
            // 
            // dgvData
            // 
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Location = new Point(268, 22);
            dgvData.Name = "dgvData";
            dgvData.Size = new Size(536, 394);
            dgvData.TabIndex = 16;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(816, 499);
            Controls.Add(dgvData);
            Controls.Add(btnSummaryReport);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnView);
            Controls.Add(btnAdd);
            Controls.Add(txbRecScore);
            Controls.Add(txbAge);
            Controls.Add(txbSpecies);
            Controls.Add(txbName);
            Controls.Add(txbAnimalID);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Times New Roman", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "Form1";
            Text = "Main ";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txbAnimalID;
        private TextBox txbName;
        private TextBox txbSpecies;
        private TextBox txbAge;
        private TextBox txbRecScore;
        private Button btnAdd;
        private Button btnView;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnSummaryReport;
        private DataGridView dgvData;
    }
}
