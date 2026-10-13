using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boabab_Ridge_Wildlife.Models
{
    class Animal
    {
        // Seven fields
        public int AnimalID;
        public string Name;
        public string Species;
        public string Breed;
        public int Age;
        public string Gender;
        public double Weight;

        // Constructor
        public Animal(int animalID, string name, string species,
                      string breed, int age, string gender, double weight)
        {
            AnimalID = animalID;
            Name = name;
            Species = species;
            Breed = breed;
            Age = age;
            Gender = gender;
            Weight = weight;
        }
    }
}
