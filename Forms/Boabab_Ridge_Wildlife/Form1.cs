using System.ComponentModel;
using Boabab_Ridge_Wildlife.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using Boabab_Ridge_Wildlife.Data_Layer;

namespace Boabab_Ridge_Wildlife
{
    public partial class Form1 : Form
    {
        DataHandler handler = new DataHandler();
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "animals.txt");
        private BindingList<Animal> animals = new BindingList<Animal>();
        private bool dataLoadedOk = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void ConfigureGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.ReadOnly = true;
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Animal ID",
                DataPropertyName = "AnimalID"
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Name",
                DataPropertyName = "Name"
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Species",
                DataPropertyName = "Species"
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Age",
                DataPropertyName = "Age"
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Recovery Score",
                DataPropertyName = "RecoveryScore"
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Status",
                DataPropertyName = "Status"
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Housing Unit",
                DataPropertyName = "HousingUnit"
            });
        }

        private void RefreshGrid()
        {
            try
            {
                handler.EnsureFileExists(filePath);
                dgvData.DataSource = handler.LoadAnimals(filePath, out int skipped);
                dataLoadedOk = true;
                if (skipped > 0)
                {
                    MessageBox.Show($"{skipped} records were skipped due to validation errors.");
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("The program doesn't have permission to create or read the file.",
                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvData.DataSource = null;
            }
            catch (IOException)
            {
                MessageBox.Show("The file could not be accessed. It may be open in another program.",
                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvData.DataSource = null;
            }
            catch (Exception ex)
            {
                if (!dataLoadedOk)
                {
                    MessageBox.Show("The data file is missing or corrupted. Please ensure 'animals.txt' exists in the application directory and is formatted correctly.",
                        "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        { 
            ConfigureGrid();
            RefreshGrid();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void btnSummaryReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSummaryReport summaryReportForm = new frmSummaryReport();
            summaryReportForm.Show();
        }
    }
}
