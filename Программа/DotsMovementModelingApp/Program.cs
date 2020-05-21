using System;
using System.Windows.Forms;

namespace DotsMovementModelingApp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
            }
            catch (InvalidCastException e)
            {
                var message = "An unexpected fatal error has occurred. Exception message: " + e.Message +
                              Environment.NewLine + "The application will be closed." +
                              Environment.NewLine + "We apologize for the inconvenience.";
                var caption = "Fatal error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
