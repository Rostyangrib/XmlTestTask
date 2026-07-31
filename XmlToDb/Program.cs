// Точка входа: открывает базу данных и показывает главную форму.

using System;
using System.Windows.Forms;

namespace XmlToDb
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Database.Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось открыть базу данных:" + Environment.NewLine + ex.Message,
                    "XmlToDb", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new MainForm());
        }
    }
}
