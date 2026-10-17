using System.ComponentModel;
using Boabab_Ridge_Wildlife.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace Boabab_Ridge_Wildlife
{
    public partial class Form1 : Form
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "animals.txt");
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
                DataPropertyName = "animalID"
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
                DataPropertyName = "recoveryScore"
            });
        }
        public BindingList<Animal> LoadAnimals(string path, out int skipped)
        {
            var animals = new BindingList<Animal>();
            skipped = 0;

            try
            {
                var lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 5 &&
                        int.TryParse(parts[3], out int age) &&
                        int.TryParse(parts[4], out int recoveryScore))
                    {
                        var animal = new Animal(parts[0], parts[1], parts[2], age, recoveryScore);
                        animals.Add(animal);
                    }
                    else
                    {
                        skipped++;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file not found, access denied)
                Console.WriteLine($"Error loading animals: {ex.Message}");
            }

            return animals;
        }
        private void EnsureFileExists(string path)
        {
            if (!File.Exists(path))
            {
                // Creates a new empty file; Dispose closes it straight away
                File.Create(path).Dispose();
            }
        }
        private void RefreshGrid()
        {
            EnsureFileExists(filePath);
            try
            {
                EnsureFileExists(filePath);
                dgvData.DataSource = LoadAnimals(filePath, out int skipped);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("The program doesn't have permission to create or read the file.",
                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                MessageBox.Show("The file could not be accessed. It may be open in another program.",
                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
