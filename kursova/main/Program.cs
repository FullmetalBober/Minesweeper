using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main
{
    public class Music
    {
        Random random = new Random();
        public int sound { get; set; }
        public static WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();
        Timer tmr = new Timer();
        public void play_music_1()
        {
            tmr.Interval = 10;
            tmr.Stop();
            WMP.URL = @"1.mp3"; // файл музыкальный
            WMP.settings.volume = 100; // меняя значение можно регулировать громкость
            WMP.controls.play();
            tmr.Tick += new EventHandler(tmr_Tick);
            WMP.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(wplayer_PlayStateChange);
        }
        public void play_music_2()
        {
            tmr.Interval = 10;
            tmr.Stop();
            WMP.URL = @"2.mp3"; // файл музыкальный
            WMP.settings.volume = 100; // меняя значение можно регулировать громкость
            WMP.controls.play();
            tmr.Tick += new EventHandler(tmr_Tick);
            WMP.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(wplayer_PlayStateChange);
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            WMP.controls.stop();
            if (sound == 1)
                play_music_1();
            if (sound == 2)
                play_music_2();
            if (sound == 3)
                Random();
        }
        void wplayer_PlayStateChange(int NewState)
        {
            if (NewState == (int)WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                tmr.Start();
            }
        }
        public void Random()
        {
            if (random.Next(1, 3) == 1)
                play_music_1();
            else
                play_music_2();
        }
        public void stop()
        {
            WMP.controls.stop();
        }
    }
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
        public static int easterEgg { get; set; }
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
