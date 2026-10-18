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

        private void RefreshGrid()
        {
            //try
            //{
            //    handler.EnsureFileExists(filePath);
            //    dgvData.DataSource = handler.LoadAnimals(filePath, out int skipped);
            //    dataLoadedOk = true;
            //    if (skipped > 0)
            //    {
            //        MessageBox.Show($"{skipped} records were skipped due to validation errors.");
            //    }
            //}
            //catch (UnauthorizedAccessException)
            //{
            //    MessageBox.Show("The program doesn't have permission to create or read the file.",
            //        "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    dgvData.DataSource = null;
            //}
            //catch (IOException)
            //{
            //    MessageBox.Show("The file could not be accessed. It may be open in another program.",
            //        "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    dgvData.DataSource = null;
            //}
            //catch (Exception ex)
            //{
            //    if (!dataLoadedOk)
            //    {
            //        MessageBox.Show("The data file is missing or corrupted. Please ensure 'animals.txt' exists in the application directory and is formatted correctly.",
            //            "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //}

            dataLoadedOk = false; //reset the data loaded flag to false before attempting to load data again
            selectedAnimalId = null; //reset the selected animal ID to null since no animal is selected after refresh

            txbAnimalID.ReadOnly = false; //make the AnimalID text box editable again for new entries

            //clear the text boxes to remove any previously displayed animal data
            txbAnimalID.Clear();
            txbName.Clear();
            txbSpecies.Clear();
            txbAge.Clear();
            txbRecScore.Clear();

            dgvData.DataSource = null;

            try //attempt to load the animal data from the file
            {
                handler.EnsureFileExists(filePath); //ensure the data file exists, creating it if necessary

                animals = handler.LoadAnimals(filePath, out int skipped); //load the animals from the file and get the count of skipped records due to validation errors

                //bind the loaded animals to the DataGridView for display
                dgvData.DataSource = animals;
                dgvData.ClearSelection();

                if (skipped > 0) //if there were any skipped records, inform the user and disable update and delete operations
                {
                    MessageBox.Show(
                        $"{skipped} invalid records were skipped. Update and Delete are disabled until the file is corrected.");
                    return;
                }

                dataLoadedOk = true; //set the data loaded flag to true since the data was loaded successfully
            }
            catch (Exception ex) //catch any exceptions that occur during the loading process and inform the user
            {
                MessageBox.Show("Could not load animals: " + ex.Message);
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

            try //attempt to save the updated list of animals back to the file
            {
                handler.SaveAnimals(filePath, animals);

                RefreshGrid();

                MessageBox.Show("Animal updated successfully.");
            }
            catch (Exception ex) //catch any exceptions that occur during the save operation and inform the user
            {
                MessageBox.Show("Update failed: " + ex.Message);
                RefreshGrid();
            }
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
                $"Are you sure you want to delete {animal.Name}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (answer != DialogResult.Yes) //if the user does not confirm the deletion, return without doing anything
            {
                return;
            }

            animals.Remove(animal); //remove the selected animal from the list of animals

            try //attempt to save the updated list of animals back to the file
            {
                handler.SaveAnimals(filePath, animals);

                RefreshGrid();

                MessageBox.Show("Animal deleted successfully.");
            }
            catch (Exception ex) //catch any exceptions that occur during the save operation and inform the user
            {
                MessageBox.Show("Delete failed: " + ex.Message);
                RefreshGrid();
            }
        }


    }
}
