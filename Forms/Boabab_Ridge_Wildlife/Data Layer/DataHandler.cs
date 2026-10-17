using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boabab_Ridge_Wildlife.Models;

namespace Boabab_Ridge_Wildlife.Data_Layer
{
    class DataHandler
    {
        public DataHandler()
        {
            //
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
                    var parts = line.Split('|');
                    if (parts.Length == 7 &&
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
            //MessageBox.Show(animals.Count.ToString());
            return animals;
        }

        public void EnsureFileExists(string path)
        {
            if (!File.Exists(path))
            {
                // Creates a new empty file; Dispose closes it straight away
                File.Create(path).Dispose();
            }
        }
    }
}
