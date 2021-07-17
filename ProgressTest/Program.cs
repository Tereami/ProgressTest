using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgressTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 form = new Form1();
            Progress<(int, string)> progressReport = new Progress<(int, string)>(form.ReportProgress);
            LongProcessAsync(progressReport);


            Application.Run(form);
        }

        private static async void LongProcessAsync(IProgress<(int, string)> progress)
        {
            int count = 1000;
            await Task.Run(() =>
            {
                for (int i = 0; i < count; i++)
                {
                    int percent = 100 * i / count;
                    string taskname = "Начинаю...";
                    if (i > 500) taskname = "Скоро закончу...";

                    progress.Report((percent, taskname));
                    System.Threading.Thread.Sleep(1);
                }
            });

        }
    }
}
