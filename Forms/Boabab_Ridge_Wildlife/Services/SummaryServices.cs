using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Boabab_Ridge_Wildlife.Models;

namespace Boabab_Ridge_Wildlife.Services
{
    /// <summary>
    /// Calculates and writes the Centre's summary report from the current animal data.
    /// </summary>
    public class SummaryService
    {
        /// <summary>
        /// Builds the full summary text used both on screen and in summary.txt.
        /// </summary>
        public string BuildReport(IList<Animal> animals, DateTime generatedAt)
        {
            int critical = animals.Count(a => a.Status == "Critical");
            int serious = animals.Count(a => a.Status == "Serious");
            int stable = animals.Count(a => a.Status == "Stable");
            int recovering = animals.Count(a => a.Status == "Recovering");
            int releaseReady = animals.Count(a => a.Status == "Release-Ready");

            // InvariantCulture forces a full stop as the decimal separator,
            // regardless of the PC's regional settings.
            string avgAge = animals.Count == 0
                ? "N/A"
                : animals.Average(a => a.Age).ToString("F2", CultureInfo.InvariantCulture);

            string avgScore = animals.Count == 0
                ? "N/A"
                : animals.Average(a => a.RecoveryScore).ToString("F2", CultureInfo.InvariantCulture);

            string timestamp = generatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            var sb = new StringBuilder();
            sb.AppendLine("BAOBAB RIDGE WILDLIFE REHABILITATION CENTRE");
            sb.AppendLine("ANIMAL SUMMARY REPORT");
            sb.AppendLine($"Generated: {timestamp}");
            sb.AppendLine();
            sb.AppendLine($"Total animals: {animals.Count}");
            sb.AppendLine($"Average age (years): {avgAge}");
            sb.AppendLine($"Average recovery score: {avgScore}");
            sb.AppendLine();
            sb.AppendLine("Animals per status:");
            sb.AppendLine($"Critical: {critical}");
            sb.AppendLine($"Serious: {serious}");
            sb.AppendLine($"Stable: {stable}");
            sb.AppendLine($"Recovering: {recovering}");
            sb.AppendLine($"Release-Ready: {releaseReady}");
            return sb.ToString();
        }

        /// <summary>
        /// Writes the summary text to summary.txt, overwriting the previous version.
        /// </summary>
        public bool WriteSummaryFile(string filePath, string reportText, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                File.WriteAllText(filePath, reportText, Encoding.UTF8);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                errorMessage = "You do not have permission to write summary.txt. Check the folder permissions and try again.";
                return false;
            }
            catch (IOException)
            {
                errorMessage = "summary.txt could not be written because it is open in another program. Close it and try again.";
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = "An unexpected error occurred while writing the summary: " + ex.Message;
                return false;
            }
        }
    }
}