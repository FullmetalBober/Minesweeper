using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        //private void buttonDesign()
        //{
        //    GraphicsPath gp = new GraphicsPath();
        //    Graphics g = CreateGraphics();
        //    // Создадим новый прямоугольник с размерами кнопки 
        //    Rectangle smallRectangle = button1.ClientRectangle;
        //    // уменьшим размеры прямоугольника 
        //    smallRectangle.Inflate(5, 5);
        //    // создадим эллипс, используя полученные размеры 
        //    gp.AddEllipse(smallRectangle);
        //    button1.Region = new Region(gp);
        //    // рисуем окантовоку для круглой кнопки 
        //    g.DrawEllipse(new Pen(Color.Gray, 2),
        //    button1.Left + 1,
        //    button1.Top + 1,
        //    button1.Width - 3,
        //    button1.Height - 3);
        //    // освобождаем ресурсы 
        //    g.Dispose();
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Settings.gameLevel = 1;
                Settings.mapHeight = 9;
                Settings.mapWidth = 9;
                Settings.mapMin = 10;
                this.Hide();
                Form2 game = new Form2();
                game.Show();
            }

            if (radioButton2.Checked)
            {
                Settings.gameLevel = 2;
                Settings.mapHeight = 14;
                Settings.mapWidth = 14;
                Settings.mapMin = 35;
                this.Hide();
                Form2 game = new Form2();
                game.Show();
            }

            if (radioButton3.Checked)
            {
                Settings.gameLevel = 3;
                Settings.mapHeight = 19;
                Settings.mapWidth = 19;
                Settings.mapMin = 75;
                this.Hide();
                Form2 game = new Form2();
                game.Show();
            }

            if (radioButton4.Checked)
            {
                Settings.gameLevel = 0;
                int mh, mw, mm, min = 0;
                if (int.TryParse(textBox1.Text, out mh) && mh > min && mh < 20)
                {
                    mh =  int.Parse(textBox1.Text);
                    Settings.mapHeight = mh;
                }
                else
                {
                    MessageBox.Show("Помилка введення висоти!");
                    textBox1.Text = null;
                    mh = 0;
                }

                ////

                if (int.TryParse(textBox2.Text, out mw) && mw > min && mw < 40)
                {
                    mw = int.Parse(textBox2.Text);
                    Settings.mapWidth = mw;
                }
                else
                {
                    MessageBox.Show("Помилка введення ширини!");
                    textBox2.Text = null;
                    mw = 0;
                }
                
                ////

                if (int.TryParse(textBox3.Text, out mm) && mm > 0)
                {
                    if (mm > (mh * mw) * 0.9)
                        mm = Convert.ToInt32(Math.Round((mh * mw) * 0.9, 2));
                    else if (mh == 1 && mw == 1)
                        mm = 1;
                    else
                        mm = int.Parse(textBox3.Text);
                    Settings.mapMin = mm;
                }
                else
                {
                    MessageBox.Show("Помилка введення кількості мін!");
                    textBox3.Text = null;
                    mm = 0;
                }

                ////

                if (mh > 0 && mw > 0 && mm > 0)
                {
                    this.Hide();
                    Form2 game = new Form2();
                    game.Show();
                }
            }
            Settings.mapFlag = Settings.mapMin;

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
