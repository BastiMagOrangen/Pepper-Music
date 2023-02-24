using System;

namespace Pepper_Music
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();

            Form1 MainForm;

            if (args != null && args.Length > 0)
            {
                List<string> t = new();
                for (int i = 0; i < args.Length; i++)
                {
                    t.Add(args[i]);
                }
                MainForm = new Form1(t);
            }
            else
            {
                MainForm = new Form1();
            }

            Application.Run(MainForm);

        }
    }
}