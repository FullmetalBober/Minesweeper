using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace main
{
    public partial class Form3 : Form
    {
        public class RecordEasy
        {
            public string easyName { get; set; }
            public int easyTime { get; set; }
        }
        public class RecordMedium
        {
            public string mediumName { get; set; }
            public int mediumTime { get; set; }
        }
        public class RecordHard
        {
            public string hardName { get; set; }
            public int hardTime { get; set; }
        }





        public Form3()
        {
            InitializeComponent();
        }

        private void Form2_Closing(Object sender, FormClosingEventArgs e)
        {
            Form main = Application.OpenForms[0];
            main.Show();
        }
        private void Form3_Load(object sender, EventArgs e)
        {

            Settings.recordCount = 0;
            this.FormClosing += Form2_Closing;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Settings.recordList = 0;
            read_Easy();
            read_Medium();
            read_Hard();
        }


        private async Task read_Hard()
        {
            richTextBox3.Text = null;
            textBox6.Text = null;
            List<RecordHard> recordHard = new List<RecordHard>();

            string line;
            StreamReader readerHard = File.OpenText("hard.txt");
            for (int i = 0; (line = await readerHard.ReadLineAsync()) != null; i++)
            {
                recordHard.Add(new RecordHard() { hardName = line, hardTime = int.Parse(await readerHard.ReadLineAsync()) });
                if (i > Settings.recordCount)
                    Settings.recordCount = i;
            }
            readerHard.Close();

            richTextBox3.Text += "\t\tВажкий";

            recordHard.Sort(delegate (RecordHard a, RecordHard b)
            { return a.hardTime.CompareTo(b.hardTime); });
            for (int i = 14 * Settings.recordList; i < 14 * (Settings.recordList + 1) || i < recordHard.Count; i++)
            {
                richTextBox3.Text += "\n" + (i + 1) + "." + recordHard[i].hardName;
                textBox6.Text += "\t" + recordHard[i].hardTime;

            }
        }


        private async Task read_Medium()
        {
            richTextBox2.Text = null;
            textBox5.Text = null;

            List<RecordMedium> recordMedium = new List<RecordMedium>();

            string line;
            StreamReader readerMedium = File.OpenText("medium.txt");
            for (int i = 0; (line = await readerMedium.ReadLineAsync()) != null; i++)
            {
                recordMedium.Add(new RecordMedium() { mediumName = line, mediumTime = int.Parse(await readerMedium.ReadLineAsync()) });
                if (i > Settings.recordCount)
                    Settings.recordCount = i;
            }
            readerMedium.Close();

            richTextBox2.Text += "\t\tСередній";

            recordMedium.Sort(delegate (RecordMedium a, RecordMedium b)
            { return a.mediumTime.CompareTo(b.mediumTime); });
            for (int i = 14 * Settings.recordList; i < 14 * (Settings.recordList + 1) || i < recordMedium.Count; i++)
            {
                richTextBox2.Text += "\n" + (i + 1) + "." + recordMedium[i].mediumName;
                textBox5.Text += "\t" + recordMedium[i].mediumTime;

            }
        }




        private async Task read_Easy()
        {
            richTextBox1.Text = null;
            textBox4.Text = null;

            List<RecordEasy> recordEasy = new List<RecordEasy>();

            string line;
            StreamReader readerEasy = File.OpenText("easy.txt");
            for (int i = 0; (line = await readerEasy.ReadLineAsync()) != null; i++)
            {
                recordEasy.Add(new RecordEasy() { easyName = line, easyTime = int.Parse(await readerEasy.ReadLineAsync()) });
                if (i > Settings.recordCount)
                    Settings.recordCount = i;
            }
            readerEasy.Close();

            richTextBox1.Text += "\t\tЛегкий";

            recordEasy.Sort(delegate (RecordEasy a, RecordEasy b)
             { return a.easyTime.CompareTo(b.easyTime); });
            //for (int i = 0; i < 15; i++)
            //    richTextBox1.Text += "\n3";
            for (int i = 14 * Settings.recordList; i < 14 * (Settings.recordList + 1) && i < recordEasy.Count; i++)
            {
                richTextBox1.Text += "\n"+ (i + 1) + "." + recordEasy[i].easyName;
                textBox4.Text += "\t" + recordEasy[i].easyTime;

            }

        }

      

 


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Settings.recordList >= 1)
            {
                Settings.recordList--;
                read_Easy();
                read_Medium();
                read_Hard();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (14 * (Settings.recordList + 1) < Settings.recordCount + 1)
            {
                Settings.recordList++;
                read_Easy();
                read_Medium();
                read_Hard();
            }
        }
    }
}
