using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boabab_Ridge_Wildlife.Services
{
    public class AnimalValidationResults
    {
        // Stores the result of validating an animal's input.
        public class ValidationResult
        {
            public bool IsValid { get; private set; }
            public string FieldName { get; private set; } = string.Empty;
            public string Message { get; private set; } = string.Empty;

            public string AnimalID { get; private set; } = string.Empty;
            public string Name { get; private set; } = string.Empty;
            public string Species { get; private set; } = string.Empty;
            public int Age { get; private set; }
            public int RecoveryScore { get; private set; }

            // Creates a failed validation result.
            private ValidationResult(string fieldName, string message)
            {
                IsValid = false;
                FieldName = fieldName;
                Message = message;
            }

            // Creates a successful validation result.
            private ValidationResult(
                string animalID,
                string name,
                string species,
                int age,
                int recoveryScore)
            {
                IsValid = true;
                AnimalID = animalID;
                Name = name;
                Species = species;
                Age = age;
                RecoveryScore = recoveryScore;
            }

            // Returns a failed validation result.
            public static ValidationResult Failure(
                string fieldName, string message)
            {
                return new ValidationResult(fieldName, message);
            }

            // Returns a successful validation result.
            public static ValidationResult Success(
                string animalID,
                string name,
                string species,
                int age,
                int recoveryScore)
            {
                return new ValidationResult(
                    animalID, name, species, age, recoveryScore);
            }
        }
    }
}
