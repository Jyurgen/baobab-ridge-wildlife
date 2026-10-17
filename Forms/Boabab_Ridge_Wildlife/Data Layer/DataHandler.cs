using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boabab_Ridge_Wildlife.Models;
using Boabab_Ridge_Wildlife.Services;
using static System.Reflection.Metadata.BlobBuilder;

namespace Boabab_Ridge_Wildlife.Data_Layer
{
    class DataHandler
    {
        private readonly AnimalValidator validator = new AnimalValidator();
        public DataHandler() { }

        public BindingList<Animal> LoadAnimals(string path, out int skipped)
        {
            var animals = new BindingList<Animal>();
            skipped = 0;

            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var parts = line.Split('|');

                if (parts.Length != 7)
                {
                    skipped++;
                    continue;
                }

                var result = validator.Validate(parts[0], parts[1], parts[2], parts[3], parts[4], animals, false);

                if (!result.IsValid)
                {
                    skipped++;
                    continue;
                }

                animals.Add(new Animal(result.AnimalID, result.Name, result.Species, result.Age, result.RecoveryScore));
            }
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
