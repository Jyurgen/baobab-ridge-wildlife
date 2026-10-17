using Boabab_Ridge_Wildlife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Boabab_Ridge_Wildlife.Services
{
    public class AnimalValidator
    {
        // Validates raw form input and returns the failed field and message,
        // or the normalized values and parsed numbers when validation succeeds.
        public ValidationResult Validate(
            string animalID,
            string name,
            string species,
            string ageText,
            string recoveryScoreText,
            IEnumerable<Animal> existingAnimals,
            bool isUpdate = false)
        {
            // A missing collection must never silently bypass duplicate checks.
            if (existingAnimals == null)
            {
                return ValidationResult.Failure(
                    "General",
                    "The existing animals collection is required.");
            }

            // The validator is the single owner of trimming and ID normalization.
            animalID = (animalID ?? string.Empty)
                .Trim().ToUpperInvariant();
            name = (name ?? string.Empty).Trim();
            species = (species ?? string.Empty).Trim();
            ageText = (ageText ?? string.Empty).Trim();
            recoveryScoreText =
                (recoveryScoreText ?? string.Empty).Trim();

            // Validate the animal ID.
            if (string.IsNullOrWhiteSpace(animalID))
            {
                return ValidationResult.Failure(
                    "AnimalID", "Animal ID is required.");
            }

            if (!Regex.IsMatch(animalID, @"^WR-[0-9]{4}$"))
            {
                return ValidationResult.Failure(
                    "AnimalID",
                    "Animal ID must contain WR- followed by exactly 4 digits, for example WR-0007.");
            }

            // An ID must be unique when adding a new animal.
            if (!isUpdate && existingAnimals.Any(existing =>
                existing != null &&
                string.Equals(
                    existing.AnimalID?.Trim(),
                    animalID,
                    StringComparison.OrdinalIgnoreCase)))
            {
                return ValidationResult.Failure(
                    "AnimalID",
                    "Animal ID already exists. Enter a unique Animal ID.");
            }

            // Validate the animal's name.
            if (string.IsNullOrWhiteSpace(name))
            {
                return ValidationResult.Failure(
                    "Name", "Name is required.");
            }

            if (name.Length > 30)
            {
                return ValidationResult.Failure(
                    "Name", "Name must not exceed 30 characters.");
            }

            if (name.Contains('|'))
            {
                return ValidationResult.Failure(
                    "Name", "Name must not contain the pipe character |.");
            }

            // Validate the animal's species.
            if (string.IsNullOrWhiteSpace(species))
            {
                return ValidationResult.Failure(
                    "Species", "Species is required.");
            }

            if (species.Length > 30)
            {
                return ValidationResult.Failure(
                    "Species", "Species must not exceed 30 characters.");
            }

            if (species.Contains('|'))
            {
                return ValidationResult.Failure(
                    "Species", "Species must not contain the pipe character |.");
            }

            // Parse and validate age from the raw text box value.
            if (!int.TryParse(ageText, out int age))
            {
                return ValidationResult.Failure(
                    "Age", "Age must be a whole number between 0 and 100.");
            }

            if (age < 0 || age > 100)
            {
                return ValidationResult.Failure(
                    "Age", "Age must be a whole number between 0 and 100.");
            }

            // Parse and validate the recovery score from raw text.
            if (!int.TryParse(recoveryScoreText, out int recoveryScore))
            {
                return ValidationResult.Failure(
                    "RecoveryScore",
                    "Recovery Score must be a whole number between 0 and 100.");
            }

            if (recoveryScore < 0 || recoveryScore > 100)
            {
                return ValidationResult.Failure(
                    "RecoveryScore",
                    "Recovery Score must be a whole number between 0 and 100.");
            }

            // Return normalized values and parsed numbers for the form to use.
            return ValidationResult.Success(
                animalID, name, species, age, recoveryScore);
        }
    }

    // Carries validation status, the field to focus, the error message,
    // and parsed data when validation succeeds.
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

        // Creates a successful validation result with normalized data.
        private ValidationResult(
            string animalID, string name, string species,
            int age, int recoveryScore)
        {
            IsValid = true;
            AnimalID = animalID;
            Name = name;
            Species = species;
            Age = age;
            RecoveryScore = recoveryScore;
        }

        // Builds a result describing the invalid field.
        public static ValidationResult Failure(
            string fieldName, string message)
        {
            return new ValidationResult(fieldName, message);
        }

        // Builds a result containing the validated animal data.
        public static ValidationResult Success(
            string animalID, string name, string species,
            int age, int recoveryScore)
        {
            return new ValidationResult(
                animalID, name, species, age, recoveryScore);
        }
    }
}


