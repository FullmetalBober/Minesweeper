using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main
{
    static class Settings
    {
        public static int mapHeight { get; set; }
        public static int mapWidth { get; set; }
        public static int mapMin { get; set; }
        public static int mapFlag { get; set; }
        public static int gameWin { get; set; }
        public static int gameTime { get; set; }
        public static int gameLevel { get; set; }
        public static string gameName { get; set; }
        public static int recordList { get; set; }
        public static int recordCount { get; set; }
    }

    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }

    }
}
