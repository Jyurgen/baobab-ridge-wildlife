using System;
using System.Windows.Forms;

namespace Boabab_Ridge_Wildlife
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Global safety net: show a friendly message instead of a raw crash dialog.
            Application.ThreadException += (s, e) =>
                MessageBox.Show("An unexpected error occurred: " + e.Exception.Message,
                    "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    MessageBox.Show("A fatal error occurred: " + ex.Message,
                        "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}