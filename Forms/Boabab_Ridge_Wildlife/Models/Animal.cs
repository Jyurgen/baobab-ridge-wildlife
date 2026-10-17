using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Boabab_Ridge_Wildlife.Models
{
    public class Animal
    {
        // Stores the animal's unique identification code.
        public string AnimalID { get; set; } = string.Empty;

        // Stores the animal's name.
        public string Name { get; set; } = string.Empty;

        // Stores the animal's species.
        public string Species { get; set; } = string.Empty;

        // Stores the animal's age in whole years.
        public int Age { get; set; }

        // Stores the recovery score from 0 to 100.
        public int RecoveryScore { get; set; }

        // Stores the calculated health status.
        public string Status { get; private set; } = string.Empty;

        // Stores the calculated housing unit.
        public string HousingUnit { get; private set; } = string.Empty;

        // Creates an animal and calculates its initial classification.
        public Animal(string animalID, string name, string species,
                      int age, int recoveryScore)
        {
            AnimalID = animalID;
            Name = name;
            Species = species;
            Age = age;
            RecoveryScore = recoveryScore;

            CalculateClassification();
        }

        // Calculates status and housing from the recovery score.
        public void CalculateClassification()
        {
            if (RecoveryScore <= 19)
            {
                Status = "Critical";
                HousingUnit = "Intensive Care Unit";
            }
            else if (RecoveryScore <= 39)
            {
                Status = "Serious";
                HousingUnit = "High-Dependency Ward";
            }
            else if (RecoveryScore <= 59)
            {
                Status = "Stable";
                HousingUnit = "Recovery Ward";
            }
            else if (RecoveryScore <= 79)
            {
                Status = "Recovering";
                HousingUnit = "Outdoor Enclosure";
            }
            else
            {
                Status = "Release-Ready";
                HousingUnit = "Pre-Release Camp";
            }
        }

        
    }
}
