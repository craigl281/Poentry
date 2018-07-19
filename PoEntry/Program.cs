using System;
using System.Windows.Forms;

namespace PoEntry
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            args = new string[6];
            args[0] = "vmpersei";
            args[1] = "pinnacle";
            args[2] = "1084DD"; 
            args[3] = "POENTRY9.vshost.exe";
            args[4] = "Paul";
            args[5] = "Purchase Order Entry9";
            /*
            args = new string[6];
            args[0] = "vmpersei";
            args[1] = "pinnacle";
            args[2] = "DDD564DDADB2";//"1084DD"; 
             args[3] = "POENTRY9.vshost.exe";
            args[4] = "EHSMGR";// "CRAIG";
            args[5] = "Purchase Order Entry9";*/
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length < 1)
            {
                MessageBox.Show("Bad MainMenu please use newest MainMenu9, cannot open Poentry9");
                Application.Exit();
            }
            else
                Application.Run(new Form1(args));
        }
    }
}
