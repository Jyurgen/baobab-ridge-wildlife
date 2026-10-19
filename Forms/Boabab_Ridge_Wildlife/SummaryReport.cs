using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Boabab_Ridge_Wildlife.Data_Layer;
using Boabab_Ridge_Wildlife.Models;
using Boabab_Ridge_Wildlife.Services;

namespace Boabab_Ridge_Wildlife
{
    public partial class frmSummaryReport : Form
    {
        private readonly DataHandler handler = new DataHandler();
        private readonly SummaryService summaryService = new SummaryService();
        private readonly string animalsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "animals.txt");
        private readonly string summaryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "summary.txt");

        public frmSummaryReport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Generates the summary as soon as the form opens.
        /// </summary>
        private void frmSummaryReport_Load(object sender, EventArgs e)
        {
            GenerateSummary();
        }

        /// <summary>
        /// Loads animals, builds the report, shows it on screen, and writes summary.txt.
        /// </summary>
        private void GenerateSummary()
        {
            BindingList<Animal> animals;
            try
            {
                handler.EnsureFileExists(animalsPath);
                animals = handler.LoadAnimals(animalsPath, out int skipped);

                if (skipped > 0)
                {
                    MessageBox.Show(
                        $"{skipped} invalid records were skipped and are not included in this summary.",
                        "Skipped records", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("The program does not have permission to read animals.txt.",
                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (IOException)
            {
                MessageBox.Show("animals.txt could not be read because it is open in another program. Close it and try again.",
                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred while loading animals.txt: " + ex.Message,
                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string reportText = summaryService.BuildReport(animals, DateTime.Now);
            rtbReport.Text = reportText;

            if (!summaryService.WriteSummaryFile(summaryPath, reportText, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Could not save summary",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Closes the summary form and returns the user to the main form.
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}