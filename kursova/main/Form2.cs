using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace main
{
    public partial class Form2 : Form
    {
        static class Game
        {
            public static bool first { get; set; }

            public static int empty { get; set; }

            public static int size { get; set; }
        }


        public int[,] map = new int[Settings.mapHeight, Settings.mapWidth];

        public Button[,] buttons = new Button[Settings.mapHeight, Settings.mapWidth];
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Closing(Object sender, FormClosingEventArgs e)
        {
            Form main = Application.OpenForms[0];
            main.Show();
        }

        private void Form2_Load(object sender, EventArgs e) 
        {
            Game.empty = 10;
            Game.size = 49;

            DoubleBuffered = true;
            if (Settings.mapWidth < 5)
            {
                textBox1.Visible = false;
                textBox2.Visible = false;
            }
            if (Settings.mapWidth < 1)
                button1.Visible = false;
            button1.Image = Image.FromFile("dobre.png");
            Settings.gameTime = 0;
            Settings.gameWin = 0;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            this.FormClosing += Form2_Closing;

            Game.first = true;
            sizeGame();
            addButtons();
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                                      (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);

            textBox2.Text = Settings.mapMin.ToString();


        }


        public Image imgFind(int xPos, int yPos)
        {
            Bitmap png_out = new Bitmap(Game.size, Game.size);

            Graphics gr = Graphics.FromImage(png_out);

            gr.DrawImage(Image.FromFile("saper.png"), new Rectangle(new Point(0, 0), new Size(Game.size, Game.size)), (xPos - 1) * 100 - 7, (yPos - 1) * 100 - 7, 116, 116, GraphicsUnit.Pixel);

            return png_out;
        }



        private void mapGenerate(int first_i, int first_j)
        {
            Settings.mapFlag = Settings.mapMin;
            Random rnd = new Random();
            for (int i = 0; i < Settings.mapHeight; i++)
            {
                for (int j = 0; j < Settings.mapWidth; j++)
                {
                    map[i, j] = 0;
                    buttons[i, j].Image = imgFind(5, 3);
                }
            }
            textBox2.Text = Settings.mapFlag.ToString();
            map[first_i, first_j] = 100;
            for (int k = 0; k < Settings.mapMin;)
            {
                int i = rnd.Next(0, Settings.mapHeight);
                int j = rnd.Next(0, Settings.mapWidth);
                if (map[i, j] != 10 && map[i, j] != 100)
                {
                    map[i, j] = 10;
                    k++;
                }
                if(Settings.mapHeight + Settings.mapWidth == 2)
                {
                    map[0, 0] = 10;
                    k++;

                }
            }
            map[first_i, first_j] = 0;
            if (Settings.mapHeight + Settings.mapWidth == 2)
            {
                map[0, 0] = 10;
            }
            for (int i = 0; i < Settings.mapHeight; i++)
            {
                for (int j = 0; j < Settings.mapWidth; j++)
                {
                    if (map[i, j] == 10)
                    {
                        if(i + 1 < Settings.mapHeight && map[i + 1, j] != 10)
                            map[i + 1, j]++;//1
                        if (i - 1 >= 0 && map[i - 1, j] != 10)
                            map[i - 1, j]++;//2
                        if (j + 1 < Settings.mapWidth && map[i, j + 1] != 10)
                            map[i, j + 1]++;//3
                        if (j - 1 >= 0 && map[i, j - 1] != 10)
                            map[i, j - 1]++;//4
                        if (i + 1 < Settings.mapHeight && j + 1 < Settings.mapWidth && map[i + 1, j + 1] != 10)
                            map[i + 1, j + 1]++;//5
                        if (i - 1 >= 0 && j + 1 < Settings.mapWidth && map[i - 1, j + 1] != 10)
                            map[i - 1, j + 1]++;//6
                        if (i + 1 < Settings.mapHeight && j - 1 >= 0 && map[i + 1, j - 1] != 10)
                            map[i + 1, j - 1]++;//7
                        if (i - 1 >= 0 && j - 1 >= 0 && map[i - 1, j - 1] != 10)
                            map[i - 1, j - 1]++;//8
                    }
                }
            }
            Settings.gameTime = 0;
        }
            private void sizeGame()
        {
                this.Width = Settings.mapWidth * Game.size + Game.empty * 3 + Game.empty / 2;
                this.Height = Settings.mapHeight * Game.size + Game.empty * 2 + 86;
        }



        private void addButtons()
        {
            for (int i = 0; i < Settings.mapHeight; i++)
            {
                for(int j = 0; j < Settings.mapWidth; j++)
                {
                    Button button = new Button();
                    button.Location = new Point(Game.size * j + Game.empty, Game.size * i + Game.empty);
                    button.Size = new Size(Game.size, Game.size);
                    button.Image = imgFind(5, 3);
                    button.MouseUp += new MouseEventHandler(pressed);
                    this.Controls.Add(button);
                    buttons[i, j] = button;
                }
            }
        }
        private void pressed(object sender, MouseEventArgs e)
        {
            Button pressedButton = (Button)sender;
            switch (e.Button.ToString())
            {
                case "Right":
                    pressedRight(pressedButton);
                    break;
                case "Left":
                    pressedLeft(pressedButton);
                    break;
            }
        }

        private void pressedRight(Button pressedButton)
        {
            int k = 0;
            int i = pressedButton.Location.Y / Game.size;
            int j = pressedButton.Location.X / Game.size;
            if (Settings.mapFlag > 0)
            {
                if (map[i, j] >= 0)
                {
                    switch (map[i, j])
                    {
                        case 0:
                            map[i, j] = -11;
                            buttons[i, j].Image = imgFind(4, 3);
                            Settings.mapFlag--;
                            k++;
                            break;

                        case 1:
                            map[i, j] = -1;
                            buttons[i, j].Image = imgFind(4, 3);
                            Settings.mapFlag--;
                            k++;
                            break;
                        case 2:
                            map[i, j] = -2;
                            buttons[i, j].Image = imgFind(4, 3);
                            Settings.mapFlag--;
                            k++;
                            break;
                        case 3:
                            map[i, j] = -3;
                            buttons[i, j].Image = imgFind(4, 3);
                            Settings.mapFlag--;
                            k++;
                            break;
                        case 4:
                            map[i, j] = -4;
                            buttons[i, j].Image = imgFind(4, 3);
                            Settings.mapFlag--;
                            k++;
                            break;
                        case 5:
                            map[i, j] = -5;
                            buttons[i, j].Image = imgFind(4, 3);
                            Settings.mapFlag--;
                            k++;
                            break;
                        case 6:
                            map[i, j] = -6;
                            buttons[i, j].Image = imgFind(4, 3);
                            Settings.mapFlag--;
                            k++;
                            break;
                        case 7:
                            map[i, j] = -7;
                            buttons[i, j].Image = imgFind(4, 3);
                            Settings.mapFlag--;
                            k++;
                            break;
                        case 8:
                            map[i, j] = -8;
                            buttons[i, j].Image = imgFind(4, 3);
                            Settings.mapFlag--;
                            k++;
                            break;
                        case 10:
                            map[i, j] = -10;
                            buttons[i, j].Image = imgFind(4, 3);
                            Settings.mapFlag--;
                            k++;
                            break;
                    }
                }
            }

            if (map[i, j] < 0 && map[i, j] >= -11 && k==0)
            {
                switch (map[i, j])
                {
                    case -11:
                        map[i, j] = 0;
                        buttons[i, j].Image = imgFind(2, 3);
                        Settings.mapFlag++;
                        break;
                    case -10:
                        map[i, j] = 10;
                        buttons[i, j].Image = imgFind(2, 3);
                        Settings.mapFlag++;
                        break;
                    case -8:
                        map[i, j] = 8;
                        buttons[i, j].Image = imgFind(2, 3);
                        Settings.mapFlag++;
                        break;
                    case -7:
                        map[i, j] = 7;
                        buttons[i, j].Image = imgFind(2, 3);
                        Settings.mapFlag++;
                        break;
                    case -6:
                        map[i, j] = 6;
                        buttons[i, j].Image = imgFind(2, 3);
                        Settings.mapFlag++;
                        break;
                    case -5:
                        map[i, j] = 5;
                        buttons[i, j].Image = imgFind(2, 3);
                        Settings.mapFlag++;
                        break;
                    case -4:
                        map[i, j] = 4;
                        buttons[i, j].Image = imgFind(2, 3);
                        Settings.mapFlag++;
                        break;
                    case -3:
                        map[i, j] = 3;
                        buttons[i, j].Image = imgFind(2, 3);
                        Settings.mapFlag++;
                        break;
                    case -2:
                        map[i, j] = 2;
                        buttons[i, j].Image = imgFind(2, 3);
                        Settings.mapFlag++;
                        break;
                    case -1:
                        map[i, j] = 1;
                        buttons[i, j].Image = imgFind(2, 3);
                        Settings.mapFlag++;
                        break;
                }


            }
            textBox2.Text = Settings.mapFlag.ToString();
        }



        private void stepStart(int i, int j)
        {
            int t = 0;
            Queue coordXY = new Queue();
            coordXY.Enqueue(i);
            coordXY.Enqueue(j);

            map[i, j] = 11;

            while(coordXY.Count > 0)
            {
                t++;
                if (t % 2 != 0)
                {
                    i = (int)coordXY.Dequeue();
                }
                if (t % 2 == 0)
                {
                    j = (int)coordXY.Dequeue();

                    if (map[i, j] == 11)
                    {
                        visibility(i, j);

                        if (i - 1 >= 0 && (map[i - 1, j] == 0 || map[i - 1, j] == -11)) {
                            if (map[i - 1, j] == -11)
                            {
                                Settings.mapFlag++;
                            }
                            coordXY.Enqueue(i - 1);
                            coordXY.Enqueue(j);
                            map[i - 1, j] = 11;
                        }

                        if (i + 1 < Settings.mapHeight && (map[i + 1, j] == 0 || map[i + 1, j] == -11))
                        {
                            if (map[i + 1, j] == -11)
                            {
                                Settings.mapFlag++;
                            }
                            coordXY.Enqueue(i + 1);
                            coordXY.Enqueue(j);
                            map[i + 1, j] = 11;
                        }
                        if (j - 1 >= 0 && (map[i, j - 1] == 0 || map[i, j - 1] == -11))
                        {
                            if (map[i, j - 1] == -11)
                            {
                                Settings.mapFlag++;
                            }
                            coordXY.Enqueue(i);
                            coordXY.Enqueue(j - 1);
                            map[i, j - 1] = 11;
                        }
                        if (j + 1 < Settings.mapWidth && (map[i, j + 1] == 0 || map[i, j + 1] == -11))
                        {
                            if (map[i, j + 1] == -11)
                            {
                                Settings.mapFlag++;
                            }
                            coordXY.Enqueue(i);
                            coordXY.Enqueue(j + 1);
                            map[i, j + 1] = 11;
                        }
                        if (i + 1 < Settings.mapHeight && j + 1 < Settings.mapWidth && (map[i + 1, j + 1] == 0 || map[i + 1, j + 1] == -11))
                        {
                            if (map[i + 1, j + 1] == -11)
                            {
                                Settings.mapFlag++;
                            }
                            coordXY.Enqueue(i + 1);
                            coordXY.Enqueue(j + 1);
                            map[i + 1, j + 1] = 11;
                        }
                        if (j + 1 < Settings.mapWidth && i - 1 >= 0 && (map[i - 1, j + 1] == 0 || map[i - 1, j + 1] == -11))
                        {
                            if (map[i - 1, j + 1] == -11)
                            {
                                Settings.mapFlag++;
                            }
                            coordXY.Enqueue(i - 1);
                            coordXY.Enqueue(j + 1);
                            map[i - 1, j + 1] = 11;
                        }
                        if (j - 1 >= 0 && i + 1 < Settings.mapHeight && (map[i + 1, j - 1] == 0 || map[i + 1, j - 1] == -11))
                        {
                            if (map[i + 1, j - 1] == -11)
                            {
                                Settings.mapFlag++;
                            }
                            coordXY.Enqueue(i + 1);
                            coordXY.Enqueue(j - 1);
                            map[i + 1, j - 1] = 11;
                        }
                        if (i - 1 >= 0 && j - 1 >= 0 && (map[i - 1, j - 1] == 0 || map[i - 1, j - 1] == -11))
                        {
                            if (map[i - 1, j - 1] == -11)
                            {
                                Settings.mapFlag++;
                            }
                            coordXY.Enqueue(i - 1);
                            coordXY.Enqueue(j - 1);
                            map[i - 1, j - 1] = 11;
                        }


                        if (i - 1 >= 0 && map[i - 1, j] != 0 && map[i - 1, j] != 11)
                        {
                            visibility(i - 1, j);
                        }
                        if (i + 1 < Settings.mapHeight && map[i + 1, j] != 0 && map[i + 1, j] != 11)
                            visibility(i + 1, j);

                        if (j - 1 >= 0 && map[i, j - 1] != 0 && map[i, j - 1] != 11)
                            visibility(i, j - 1);

                        if (j + 1 < Settings.mapWidth && map[i, j + 1] != 0 && map[i, j + 1] != 11)
                            visibility(i, j + 1);

                        if (i + 1 < Settings.mapHeight && j + 1 < Settings.mapWidth && map[i + 1, j + 1] != 0 && map[i + 1, j + 1] != 11)
                            visibility(i + 1, j + 1);

                        if (j + 1 < Settings.mapWidth && i - 1 >= 0 && map[i - 1, j + 1] != 0 && map[i - 1, j + 1] != 11)
                            visibility(i - 1, j + 1);

                        if (j - 1 >= 0 && i + 1 < Settings.mapHeight && map[i + 1, j - 1] != 0 && map[i + 1, j - 1] != 11)
                            visibility(i + 1, j - 1);

                        if (i - 1 >= 0 && j - 1 >= 0 && map[i - 1, j - 1] != 0 && map[i - 1, j - 1] != 11)
                            visibility(i - 1, j - 1);

                    }
                }
            }
            textBox2.Text = Settings.mapFlag.ToString();

            for (i = 0; i < Settings.mapHeight; i++)
            {
                for (j = 0; j < Settings.mapWidth; j++)
                {
                    if (map[i, j] == 11)
                        map[i, j] = 0;
                }
            }
        }



        private void pressedLeft(Button pressedButton)
        {
            int i = pressedButton.Location.Y / Game.size;
            int j = pressedButton.Location.X / Game.size;
            if (map[i, j] >= 0)
            {
                if (Game.first)
            {
                mapGenerate(i, j);
                    Game.first = false;
            }
                if (map[i, j] != 0)
                    visibility(i, j);
                else
                    stepStart(i, j);
            }
        }
        private void visibility(int i, int j) {
            if (map[i, j] >= 0)
            {
                if (map[i, j] == 0 || map[i, j] == 11)
                {
                    if (map[i, j] == -11)
                        map[i, j] = 11;
                    buttons[i, j].Enabled = false;
                    buttons[i, j].Image = imgFind(1, 4);
                    Settings.gameWin++;
                }
                if (map[i, j] == 1)
                {
                    buttons[i, j].Enabled = false;
                    buttons[i, j].Image = imgFind(1, 1);
                    Settings.gameWin++;
                    map[i, j] = 99;
                }
                if (map[i, j] == 2)
                {
                    buttons[i, j].Enabled = false;
                    buttons[i, j].Image = imgFind(2, 1);
                    Settings.gameWin++;
                    map[i, j] = 99;
                }

                if (map[i, j] == 3)
                {
                    buttons[i, j].Enabled = false;
                    buttons[i, j].Image = imgFind(3, 1);
                    Settings.gameWin++;
                    map[i, j] = 99;
                }

                if (map[i, j] == 4)
                {
                    buttons[i, j].Enabled = false;
                    buttons[i, j].Image = imgFind(4, 1);
                    Settings.gameWin++;
                    map[i, j] = 99;
                }

                if (map[i, j] == 5)
                {
                    buttons[i, j].Enabled = false;
                    buttons[i, j].Image = imgFind(5, 1);
                    Settings.gameWin++;
                    map[i, j] = 99;
                }
                if (map[i, j] == 6)
                {
                    buttons[i, j].Enabled = false;
                    buttons[i, j].Image = imgFind(1, 2);
                    Settings.gameWin++;
                    map[i, j] = 99;
                }
                if (map[i, j] == 7)
                {
                    buttons[i, j].Enabled = false;
                    buttons[i, j].Image = imgFind(2, 2);
                    Settings.gameWin++;
                    map[i, j] = 99;
                }
                if (map[i, j] == 8)
                {
                    buttons[i, j].Enabled = false;
                    buttons[i, j].Image = imgFind(3, 2);
                    Settings.gameWin++;
                    map[i, j] = 99;
                }
                if (map[i, j] == 10)
                {
                    buttons[i, j].Enabled = false;
                    buttons[i, j].Image = imgFind(5, 2);
                    map[i, j] = 100;
                    endGame(0);
                }
                if ((Settings.gameWin == (Settings.mapWidth * Settings.mapHeight) - Settings.mapMin) && (Settings.mapHeight + Settings.mapWidth != 2))
                    endGame(1);

                if (map[i, j] < 0 || map[i, j] >= -11)
                {
                    switch (map[i, j])
                    {
                        case -11:
                            buttons[i, j].Enabled = false;
                            map[i, j] = 0;
                            buttons[i, j].Image = imgFind(1, 4);
                            Settings.mapFlag++;
                            break;
                        case -10:
                            buttons[i, j].Enabled = false;
                            map[i, j] = 10;
                            buttons[i, j].Image = imgFind(5, 2);
                            Settings.mapFlag++;
                            break;
                        case -8:
                            buttons[i, j].Enabled = false;
                            map[i, j] = 8;
                            buttons[i, j].Image = imgFind(3, 2);
                            Settings.mapFlag++;
                            break;
                        case -7:
                            buttons[i, j].Enabled = false;
                            map[i, j] = 7;
                            buttons[i, j].Image = imgFind(2, 2);
                            Settings.mapFlag++;
                            break;
                        case -6:
                            buttons[i, j].Enabled = false;
                            map[i, j] = 6;
                            buttons[i, j].Image = imgFind(1, 2);
                            Settings.mapFlag++;
                            break;
                        case -5:
                            buttons[i, j].Enabled = false;
                            map[i, j] = 5;
                            buttons[i, j].Image = imgFind(5, 1);
                            Settings.mapFlag++;
                            break;
                        case -4:
                            buttons[i, j].Enabled = false;
                            map[i, j] = 4;
                            buttons[i, j].Image = imgFind(4, 1);
                            Settings.mapFlag++;
                            break;
                        case -3:
                            buttons[i, j].Enabled = false;
                            map[i, j] = 3;
                            buttons[i, j].Image = imgFind(3, 1);
                            Settings.mapFlag++;
                            break;
                        case -2:
                            buttons[i, j].Enabled = false;
                            map[i, j] = 2;
                            buttons[i, j].Image = imgFind(2, 1);
                            Settings.mapFlag++;
                            break;
                        case -1:
                            buttons[i, j].Enabled = false;
                            map[i, j] = 1;
                            buttons[i, j].Image = imgFind(1, 1);
                            Settings.mapFlag++;
                            break;
                    }
                    textBox2.Text = Settings.mapFlag.ToString();
                }
                }
        }
        private void endGame(int game)
        {
            timer1.Enabled = false;
            switch (game)
            {
                case 0:
                    button1.Image = Image.FromFile("zle.png");
                    MessageBox.Show("Поразка -_-");
                    for (int i = 0; i < Settings.mapHeight; i++)
                    {
                        for (int j = 0; j < Settings.mapWidth; j++)
                        {
                            buttons[i, j].Enabled = false;
                            if (map[i,j] == 10 || map[i, j] == -10)
                            {
                                buttons[i, j].Image = imgFind(4, 2);
                            }
                        }
                    }
                            break;
                    case 1:
                    recordWrite();
                    MessageBox.Show("Перемога ^_^");
                    for (int i = 0; i < Settings.mapHeight; i++)
                    {
                        for (int j = 0; j < Settings.mapWidth; j++)
                        {
                            if (map[i, j] == 10 || map[i, j] == -10)
                            {
                                buttons[i, j].Enabled = false;
                                buttons[i, j].Image = imgFind(4, 3);
                            }
                        }
                    }
                    break;
            }
        }

        private void recordWrite()
        {
            if (Settings.gameLevel == 1)
            {
                StreamWriter writer = new StreamWriter("easy.txt", true);
                writer.WriteLineAsync(Settings.gameName.ToString());
                writer.WriteLineAsync(Settings.gameTime.ToString());
                writer.Close();
            }
            if (Settings.gameLevel == 2)
            {
                StreamWriter writer = new StreamWriter("medium.txt", true);
                writer.WriteLineAsync(Settings.gameName.ToString());
                writer.WriteLineAsync(Settings.gameTime.ToString());
                writer.Close();
            }
            if (Settings.gameLevel == 3)
            {
                StreamWriter writer = new StreamWriter("hard.txt", true);
                writer.WriteLineAsync(Settings.gameName.ToString());
                writer.WriteLineAsync(Settings.gameTime.ToString());
                writer.Close();
            }

        }   
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            Settings.gameTime++;
            textBox1.Text = Settings.gameTime.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form main = Application.OpenForms[0];
            main.Hide();
            Form2 game = new Form2();
            game.Show();
        }
    }
}
