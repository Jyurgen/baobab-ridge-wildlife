using System.ComponentModel;
using Boabab_Ridge_Wildlife.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using Boabab_Ridge_Wildlife.Data_Layer;
using Boabab_Ridge_Wildlife.Services;
using System.Linq;

namespace Boabab_Ridge_Wildlife
{
    public partial class Form1 : Form
    {
        DataHandler handler = new DataHandler();
        private string? selectedAnimalId = null; // Variable to hold the selected animal ID
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "animals.txt");
        private BindingList<Animal> animals = new BindingList<Animal>();
        private bool dataLoadedOk = false;

        public Form1()
        {
            InitializeComponent();

            dgvData.CellClick += dgvData_CellClick; // Subscribe to the CellClick event
            btnUpdate.Click += btnUpdate_Click; // Subscribe to the Update button click event
            btnDelete.Click += btnDelete_Click; // Subscribe to the Delete button click event
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

        /// <summary>
        /// Saves the current animal list to animals.txt and reports friendly errors for common file problems.
        /// </summary>
        private bool TrySaveAnimals()
        {
            try
            {
                handler.SaveAnimals(filePath, animals);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("The program does not have permission to write animals.txt. Set the file to read-write and try again.",
                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("The folder that should contain animals.txt could not be found.",
                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                MessageBox.Show("animals.txt could not be written. It may be open in another program. Close it and try again.",
                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred while saving animals.txt: " + ex.Message,
                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        /// <summary>
        /// Reloads all valid records from animals.txt into the grid and resets the form to add mode.
        /// </summary>
        private void RefreshGrid()
        {
            dataLoadedOk = false;
            selectedAnimalId = null;
            txbAnimalID.ReadOnly = false;
            txbAnimalID.Clear();
            txbName.Clear();
            txbSpecies.Clear();
            txbAge.Clear();
            txbRecScore.Clear();
            dgvData.DataSource = null;

            try
            {
                handler.EnsureFileExists(filePath);
                animals = handler.LoadAnimals(filePath, out int skipped);

                dgvData.DataSource = animals;
                dgvData.ClearSelection();

                if (skipped > 0)
                {
                    MessageBox.Show(
                        $"{skipped} invalid records were skipped. Update and Delete are disabled until the file is corrected.");
                    return;
                }

                dataLoadedOk = true;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("The program does not have permission to read or create animals.txt.",
                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("The folder that should contain animals.txt could not be found.",
                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                MessageBox.Show("animals.txt could not be accessed. It may be open in another program. Close it and try again.",
                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred while loading animals.txt: " + ex.Message,
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

        private void btnSummaryReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSummaryReport summaryReportForm = new frmSummaryReport();
            // When the summary form is closed, show Form1 again.
            summaryReportForm.FormClosed += (s, args) => this.Show();
            summaryReportForm.Show();
        }

        private void dgvData_CellClick(object? sender, DataGridViewCellEventArgs e) //method to handle the CellClick event of the DataGridView
        {
            if (e.RowIndex < 0) //if the clicked row index is less than 0, it means the header row was clicked, so we return without doing anything
            {
                return;
            }

            Animal? animal = dgvData.Rows[e.RowIndex].DataBoundItem as Animal; //get the Animal object from the clicked row

            if (animal == null) //if the Animal object is null, it means the row is empty or not bound to an Animal object, so we return without doing anything
            {
                return;
            }

            selectedAnimalId = animal.AnimalID; //store the selected animal ID for later use in update or delete operations

            //populate the text boxes with the selected animal's data
            txbAnimalID.Text = animal.AnimalID;
            txbName.Text = animal.Name;
            txbSpecies.Text = animal.Species;
            txbAge.Text = animal.Age.ToString();
            txbRecScore.Text = animal.RecoveryScore.ToString();

            txbAnimalID.ReadOnly = true; //make the AnimalID text box read-only to prevent changes to the unique identifier
        }

        private void btnUpdate_Click(object? sender, EventArgs e) //method to handle the Update button click event
        {
            if (!dataLoadedOk) //if the data has not loaded correctly, inform the user and return without doing anything
            {
                MessageBox.Show("Animal data has not loaded correctly.");
                return;
            }

            if (selectedAnimalId == null) //if no animal is selected, inform the user and return without doing anything
            {
                MessageBox.Show("Please select an animal to update.");
                return;
            }

            Animal? animal = animals.FirstOrDefault(a => a.AnimalID == selectedAnimalId); //find the selected animal in the list of animals using the stored selectedAnimalId

            if (animal == null) //if the animal is not found in the list, inform the user and return without doing anything
            {
                MessageBox.Show("Animal not found.");
                return;
            }

            AnimalValidator validator = new AnimalValidator(); //create an instance of the AnimalValidator class to validate the input data before updating the animal

            // Validate the input data using the validator and get the result
            var result = validator.Validate(
                animal.AnimalID,
                txbName.Text,
                txbSpecies.Text,
                txbAge.Text,
                txbRecScore.Text,
                animals,
                true
            );

            if (!result.IsValid) //if the validation fails, inform the user of the validation errors and return without updating the animal
            {
                MessageBox.Show(result.Message);
                return;
            }

            // Update the animal's properties with the validated data
            animal.Name = result.Name;
            animal.Species = result.Species;
            animal.Age = result.Age;
            animal.RecoveryScore = result.RecoveryScore;

            animal.CalculateClassification(); // Recalculate the animal's classification based on the updated data

            if (!TrySaveAnimals())
            {
                RefreshGrid();
                return;
            }

            RefreshGrid();
            MessageBox.Show("Animal updated successfully.");
        }
        private void btnDelete_Click(object? sender, EventArgs e) //method to handle the Delete button click event
        {
            if (!dataLoadedOk) //if the data has not loaded correctly, inform the user and return without doing anything
            {
                MessageBox.Show("Animal data has not loaded correctly.");
                return;
            }

            if (selectedAnimalId == null) //if no animal is selected, inform the user and return without doing anything
            {
                MessageBox.Show("Please select an animal to delete.");
                return;
            }

            //find the selected animal in the list of animals using the stored selectedAnimalId
            Animal? animal = animals.FirstOrDefault(a => a.AnimalID == selectedAnimalId);

            if (animal == null) //if the animal is not found in the list, inform the user and return without doing anything
            {
                MessageBox.Show("Animal not found.");
                return;
            }

            DialogResult answer = MessageBox.Show(
                $"Delete {animal.AnimalID} ({animal.Name})? This cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (answer != DialogResult.Yes) //if the user does not confirm the deletion, return without doing anything
            {
                return;
            }

            animals.Remove(animal); //remove the selected animal from the list of animals

            if (!TrySaveAnimals())
            {
                animals.Add(animal);   // put it back so the screen matches the file
                RefreshGrid();
                return;
            }

            RefreshGrid();
            MessageBox.Show("Animal deleted successfully.");
        }


    }
}
