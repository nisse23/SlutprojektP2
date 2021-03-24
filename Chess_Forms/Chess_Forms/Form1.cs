using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess_Forms
{
    public partial class Form1 : Form
    {
        //skapar en spelplan
        static board Myboard = new board();
        //en lista med alla buttons som ska visa rutorna
        public Button[,] buttonGrid = new Button[Myboard.size, Myboard.size];
        // lista med alla spelpjäser i form av radiobuttons
        public RadioButton[] pieceGrid = new RadioButton[32];
        public Label[] label = new Label[8];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Myboard.placePiecesInStart();
            //skapar spelplanen
            CreateButtons();
            //placerar ut pjäserna
            GeneratePieces();

        }
        //skapar och placerar ut alla spelpjäser
        private void GeneratePieces()
        {
            //placerar dem rätt i arrayen
            Myboard.placePiecesInStart();

            //räknar ut storleken på btns. =65
            int pieceSize = panel1.Width / Myboard.size;
            //variabler
            int i = 0;

            //nästade loopar för att gå igenom arrayn thegrid.
            for (int z = 0; z < Myboard.size; z++)
            {
                for (int j = 0; j < Myboard.size; j++)
                {
                    
                    // skapar en cell för varje ruta
                    cell cell = Myboard.TheGrid[j, z];
                    if (cell.Occupied)
                    {
                        //skapar en ny radiobutton och stylar den
                        pieceGrid[i] = new RadioButton();
                        pieceGrid[i].Width = pieceSize;
                        pieceGrid[i].Height = pieceSize;
                        pieceGrid[i].Appearance = Appearance.Button;
                        pieceGrid[i].FlatStyle = FlatStyle.Flat;
                        panel1.Controls.Add(pieceGrid[i]);
                        pieceGrid[i].BringToFront();
                        //lägger till ett click event
                        pieceGrid[i].Click += MarkallowedTiles;
                        switch (cell.OccupiedBy)
                        {
                            //vita pjäser
                            case "VBonde":
                                
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitPawn;
                                pieceGrid[i].Tag = "VBonde" + " " + i;
                                if (i % 2 == 0)
                                {
                                    pieceGrid[i].BackColor = Color.White;
                                }
                                else
                                {
                                    pieceGrid[i].BackColor = Color.Black;
                                }
                                i++;
                                break;
                            case "VTorn1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitRook;
                                pieceGrid[i].Tag = "VTorn1" + " " + i;
                                pieceGrid[i].BackColor = Color.Black;
                                i++;
                                break;
                            case "VTorn2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitRook;
                                pieceGrid[i].Tag = "VTorn2" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "VHäst1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitKnight;
                                pieceGrid[i].Tag = "VHäst1" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "VHäst2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitKnight;
                                pieceGrid[i].Tag = "VHäst2" + " " + i;
                                i++;
                                break;
                            case "VLöpare1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitBishop1;
                                pieceGrid[i].Tag = "VLöpare1" + " " + i;
                                i++;
                                break;
                            case "VLöpare2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitBishop1;
                                pieceGrid[i].Tag = "VLöpare2" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "VKung":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitKung;
                                pieceGrid[i].Tag = "VKung" + " " + i;
                                i++;
                                break;
                            case "VDrottning":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitQueen;
                                pieceGrid[i].Tag = "VDrottning" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            //svart pjäser
                            case "SBonde":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Tag = "SBonde" + " " + i;
                                pieceGrid[i].Image = Properties.Resources.SvartPawn1;
                                if (i % 2 == 0)
                                {
                                    pieceGrid[i].BackColor = Color.Black;
                                }
                                else
                                {
                                    pieceGrid[i].BackColor = Color.White;
                                }  
                                i++;
                                break;
                            case "STorn1":
                                pieceGrid[i].Location = new Point(pieceSize * 0, pieceSize * 0);
                                pieceGrid[i].Image = Properties.Resources.SvartRook1;
                                pieceGrid[i].Tag = "STorn1" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                pieceGrid[i].BringToFront();
                                i++;
                                break;
                            case "STorn2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.SvartRook1;
                                pieceGrid[i].Tag = "STorn2" + " " + i;
                                pieceGrid[i].BackColor = Color.Black;
                                i++;
                                break;
                            case "SHäst1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKnight1;
                                pieceGrid[i].Tag = "SHäst1" + " " + i;
                                pieceGrid[i].BackColor = Color.Black;
                                i++;
                                break;
                            case "SHäst2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKnight1;
                                pieceGrid[i].Tag = "SHäst2" + " " + i;
                                pieceGrid[i].BackColor = Color.White;

                                i++;
                                break;
                            case "SLöpare1":

                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.SvartBishop2;
                                pieceGrid[i].Tag = "SLöpare1" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "SLöpare2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.SvartBishop2;
                                pieceGrid[i].Tag = "SLöpare2" + " " + i;
                                i++;
                                break;
                            case "SKung":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKung1;
                                pieceGrid[i].Tag = "SKung" + " " + i;
                                i++;
                                break;
                            case "SDrottning":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.SvartQueen1;
                                pieceGrid[i].Tag = "SDrottning" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                        }
                    }
                    
                }
            }

        }
        //markerar de ställen som pjäsen får gå
        private void MarkallowedTiles(object sender, EventArgs e)
        {
            for (int z = 0; z < Myboard.size; z++)
            {
                for (int j = 0; j < Myboard.size; j++)
                {
                    buttonGrid[j, z].FlatAppearance.BorderSize = 0;


                }
            }
            RadioButton r = (RadioButton)sender;
            string s = (string)r.Tag;
            string[] tag = s.Split(' '); 
            string piece = tag[0];
            int i = int.Parse(tag[1]);
            cell c = new cell(pieceGrid[i].Location.Y / 65, pieceGrid[i].Location.X / 65);
            MessageBox.Show(c.Columnnumber+ " "+ c.Rownumber);
            Myboard.Markallowedmove(c, piece);
            for (int x = 0; x < Myboard.size; x++)
            {
                for (int j = 0; j < Myboard.size; j++)
                {
                    cell cell = Myboard.TheGrid[j, x];

                    if (cell.AllowedMove)
                    {
                        MessageBox.Show(j.ToString() +"," +  x.ToString() + "y,x allowed");
                        buttonGrid[j, x].FlatAppearance.BorderSize = 4;
                        buttonGrid[j, x].FlatAppearance.BorderColor = Color.Green;
                    }
                }
            }
        }

        private void CreateButtons()
        {
            int buttonSize = panel1.Width / Myboard.size;

            for (int i = 0; i < Myboard.size; i++)
            {
                //label[i] = new Label();
                //label[i].Text = "va fan e det som händer";
                //label[i].Location = label1.Location;
                //label[i].BringToFront();
                

                for (int j = 0; j < Myboard.size; j++)
                {
                    buttonGrid[j, i] = new Button();

                    buttonGrid[j, i].Width = buttonSize;
                    buttonGrid[j, i].Height = buttonSize;
                    buttonGrid[j, i].Click += buttonClick;

                    panel1.Controls.Add(buttonGrid[j, i]);
                    buttonGrid[j, i].Location = new Point(i * buttonGrid[j, i].Height, buttonGrid[j, i].Width * j);
                    if (((j+ i) % 2) == 0) 
                    {
                        buttonGrid[j, i].BackColor = Color.White;
                    }
                    else
                        buttonGrid[j, i].BackColor = Color.Black;
                    buttonGrid[j, i].Tag = i +"," + j;
                    buttonGrid[j, i].FlatStyle = FlatStyle.Flat;
                    buttonGrid[j, i].FlatAppearance.BorderSize = 0;


                }
            }
        }

        private void buttonClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string str = (string)b.Tag;
            string[] btag = str.Split(',');
            MessageBox.Show(btag[0] + " " + btag[1]);

            if (b.FlatAppearance.BorderSize == 4)
            {


                //hämtar den checkade radiobuttonen
                var rb = panel1.Controls.OfType<RadioButton>()
                                          .FirstOrDefault(r => r.Checked);
                string s = (string)rb.Tag;
                string[] tag = s.Split(' ');



                MovePiece(rb, b, tag[0]);
                for (int z = 0; z < Myboard.size; z++)
                {
                    for (int j = 0; j < Myboard.size; j++)
                    {
                        buttonGrid[j, z].FlatAppearance.BorderSize = 0;


                    }
                }
            }


        }

        private void MovePiece(RadioButton rb, Button b, string piece)
        {
            //ändra i array
            int width = rb.Location.X / 65;
            int height = rb.Location.Y / 65;
            int newWidth = b.Location.X / 65;
            int newHeight = b.Location.Y / 65;
            Myboard.TheGrid[width, height].Occupied = false;
            Myboard.TheGrid[width, height].OccupiedBy = string.Empty;
            Myboard.TheGrid[newWidth, newHeight].Occupied = true;
            Myboard.TheGrid[newWidth, newHeight].OccupiedBy = piece;
            //MessageBox.Show("tjo");
            //change location of radiobutton
            rb.Location = b.Location;
            rb.Checked = false;
            if(((rb.Location.X/65) + (rb.Location.Y/65)) % 2 == 0)
            {
                rb.BackColor = Color.White;
            }
            else
            {
                rb.BackColor = Color.Black;
            }
        }

        private void buttonOmstart_Click(object sender, EventArgs e)
        {
            Myboard.placePiecesInStart();
        }

    }
    public class board
    {
        //storleken på spelplanen
        public int size { get; set; }
        //en 2d array som motsvarar spelplanen
        public cell[,] TheGrid { get; set; }
        public board()
        {
            size = 8;
            TheGrid = new cell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    TheGrid[j, i] = new cell(j, i);

                }
            }
        }
        public void placePiecesInStart()
        {
            //svart
           
           
            //for (int j = 7; j >= 0; j--)
            //{
            //    TheGrid[j, 6].Occupied = true;
            //    TheGrid[j, 6].OccupiedBy = "VBonde";

            //}
            
            TheGrid[0, 7].OccupiedBy = "VTorn1";
            TheGrid[0, 7].Occupied = true;
            TheGrid[7, 7].OccupiedBy = "VTorn2";
            TheGrid[7, 7].Occupied = true;

            TheGrid[1, 7].OccupiedBy = "VHäst1";
            TheGrid[1, 7].Occupied = true;
            TheGrid[6, 7].OccupiedBy = "VHäst2";
            TheGrid[6, 7].Occupied = true;

            TheGrid[2, 7].OccupiedBy = "VLöpare1";
            TheGrid[2, 7].Occupied = true;
            TheGrid[5, 7].OccupiedBy = "VLöpare2";
            TheGrid[5, 7].Occupied = true;

            TheGrid[4, 7].OccupiedBy = "VKung";
            TheGrid[4, 7].Occupied = true;

            TheGrid[3, 7].OccupiedBy = "VDrottning";
            TheGrid[3, 7].Occupied = true;
            //svart
            //for (int j = 7; j >= 0; j--)
            //{
            //    TheGrid[j, 1].Occupied = true;
            //    TheGrid[j, 1].OccupiedBy = "SBonde";
            //}
            TheGrid[0, 0].OccupiedBy = "STorn1";
            TheGrid[0, 0].Occupied = true;
            TheGrid[7, 0].OccupiedBy = "STorn2";
            TheGrid[7, 0].Occupied = true;

            TheGrid[1, 0].OccupiedBy = "SHäst1";
            TheGrid[1, 0].Occupied = true;
            TheGrid[6, 0].OccupiedBy = "SHäst2";
            TheGrid[6, 0].Occupied = true;

            TheGrid[2, 0].OccupiedBy = "SLöpare1";
            TheGrid[2, 0].Occupied = true;
            TheGrid[5, 0].OccupiedBy = "SLöpare2";
            TheGrid[5, 0].Occupied = true;

            TheGrid[3, 0].OccupiedBy = "SKung";
            TheGrid[3, 0].Occupied = true;
            
            TheGrid[4, 0].OccupiedBy = "SDrottning";
            TheGrid[4, 0].Occupied = true;

        }
        public void Markallowedmove(cell currentCell, string piece)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    TheGrid[i, j].AllowedMove = false;
                }
            }
            switch (piece)
            {
                case "VTorn1":
                case "VTorn2":
                    //kan gå vertikalt och horisontellt

                    //bools som säger ifall den kan gå åt respektive håll
                    bool right = true;
                    bool left = true;
                    bool up = true;
                    bool down = true;
                    MessageBox.Show(currentCell.Columnnumber + " " + currentCell.Rownumber + " mark cell");
                    for (int i = 0; i < size; i++)
                    {
                        if (currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >=0 && up)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].OccupiedBy.ToCharArray();
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;
                                }
                                
                                    up = false;
                            }
                            else
                            TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;
                        }
                        if (currentCell.Rownumber + i < size && currentCell.Rownumber + i >=0 && right)
                        {
                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].OccupiedBy.ToCharArray();
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
                                }
                                
                                    right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if (currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size && down)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].OccupiedBy.ToCharArray();
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                                }
                                
                                    down = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                        }
                        if (currentCell.Rownumber - i >= 0 && currentCell.Rownumber - i < size && left)
                        {
                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].OccupiedBy.ToCharArray();
                                
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                                }
                                
                                    left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                        }
                    }
                    break;
                case "STorn1":
                case "STorn2":
                    //kan gå vertikalt och horisontellt
                    //bools som säger ifall den kan gå åt respektive håll
                    right = true;
                    left = true;
                    up = true;
                    down = true;
                    for (int i = 0; i < size; i++)
                    {
                        if (currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >= 0 && up)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].OccupiedBy.ToCharArray();
                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;
                                }

                                up = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;
                        }
                        if (currentCell.Rownumber + i < size && currentCell.Rownumber + i >= 0 && right)
                        {
                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].OccupiedBy.ToCharArray();
                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
                                }

                                right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if (currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size && down)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].OccupiedBy.ToCharArray();
                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                                }

                                down = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                        }
                        if (currentCell.Rownumber - i >= 0 && currentCell.Rownumber - i < size && left)
                        {
                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].OccupiedBy.ToCharArray();

                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                        }
                    }
                    break;
                case "VLöpare1":
                case "VLöpare2":
                    //löpare kan gå diagonalt
                    bool top_right = true;
                    bool top_left = true;
                    bool bot_right = true;
                    bool bot_left = true;
                    for (int i = 0; i < size; i++)
                    {
                        if ((currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >=0) && (currentCell.Rownumber + i < size && currentCell.Rownumber + i >=0) && bot_right)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].OccupiedBy.ToCharArray();
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                                }

                                bot_right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >= 0)  && (currentCell.Rownumber - i >= 0 && currentCell.Rownumber - i < size) && bot_left)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].OccupiedBy.ToCharArray();
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                bot_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber - i >= 0 && currentCell.Rownumber < size) && top_left)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].OccupiedBy.ToCharArray();
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                bot_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber + i < size && currentCell.Rownumber >=0 ) && top_right)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].OccupiedBy.ToCharArray();
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].AllowedMove = true;
                                }

                                top_right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                    }
                    break;
                case "SLöpare1":
                case "SLöpare2":
                    //löpare kan gå diagonalt
                    top_right = true;
                    top_left = true;
                    bot_right = true;
                    bot_left = true;
                    for (int i = 0; i < size; i++)
                    {
                        if ((currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >= 0) && (currentCell.Rownumber + i < size && currentCell.Rownumber + i >= 0) && bot_right)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].OccupiedBy.ToCharArray();
                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                                }

                                bot_right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >= 0) && (currentCell.Rownumber - i >= 0 && currentCell.Rownumber - i < size) && bot_left)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].OccupiedBy.ToCharArray();
                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                bot_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber - i >= 0 && currentCell.Rownumber < size) && top_left)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].OccupiedBy.ToCharArray();
                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                bot_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber + i < size && currentCell.Rownumber >= 0) && top_right)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].OccupiedBy.ToCharArray();
                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].AllowedMove = true;
                                }

                                top_right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                    }
                    break;
                case "VKung":
                    // kan gå ett steg åt alla håll
                    if (currentCell.Columnnumber + 1 < size && currentCell.Columnnumber + 1 >= 0) 
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].AllowedMove = true;
                    }
                    if (currentCell.Columnnumber - 1 >= 0 && currentCell.Columnnumber - 1 >= 0)
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber - 1 >= 0 && currentCell.Columnnumber - 1 < size) && (currentCell.Rownumber + 1 < size && currentCell.Rownumber + 1 >=0))
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber - 1 >= 0 && currentCell.Columnnumber - 1 < size) && (currentCell.Rownumber - 1 < size && currentCell.Rownumber - 1 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber + 1 >= 0 && currentCell.Columnnumber + 1 < size) && (currentCell.Rownumber + 1 < size && currentCell.Rownumber + 1 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber + 1 >= 0 && currentCell.Columnnumber + 1 < size) && (currentCell.Rownumber - 1 < size && currentCell.Rownumber - 1 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].AllowedMove = true;
                    }
                    if (currentCell.Rownumber + 1 < size && currentCell.Rownumber + 1 >= 0)
                    {
                        if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber , currentCell.Rownumber + 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber , currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if (currentCell.Rownumber - 1 >= 0 && currentCell.Rownumber - 1 < size)
                    {
                        if (TheGrid[currentCell.Columnnumber , currentCell.Rownumber - 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].AllowedMove = true;
                    }
                    break;
                case "SKung":
                    // kan gå ett steg åt alla håll
                    if (currentCell.Columnnumber + 1 < size || currentCell.Columnnumber + 1 >= 0)
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].AllowedMove = true;
                    }
                    if (currentCell.Columnnumber - 1 >= 0 || currentCell.Columnnumber - 1 >= 0)
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber - 1 >= 0 || currentCell.Columnnumber - 1 < size) && (currentCell.Rownumber + 1 < size || currentCell.Rownumber + 1 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber - 1 >= 0 || currentCell.Columnnumber - 1 < size) && (currentCell.Rownumber - 1 < size || currentCell.Rownumber - 1 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber + 1 >= 0 || currentCell.Columnnumber + 1 < size) && (currentCell.Rownumber + 1 < size || currentCell.Rownumber + 1 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber + 1 >= 0 || currentCell.Columnnumber + 1 < size) && (currentCell.Rownumber - 1 < size || currentCell.Rownumber - 1 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].AllowedMove = true;
                    }
                    if (currentCell.Rownumber + 1 < size || currentCell.Rownumber + 1 >= 0)
                    {
                        if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if (currentCell.Rownumber - 1 >= 0 || currentCell.Rownumber - 1 < size)
                    {
                        if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].AllowedMove = true;
                    }
                    break;
                case "SDrottning":
                    //diagonal / löpare
                    top_right = true;
                    top_left = true;
                    bot_right = true;
                    bot_left = true;
                    for (int i = 0; i < size; i++)
                    {
                        if ((currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >= 0) && (currentCell.Rownumber + i < size && currentCell.Rownumber + i >= 0) && bot_right)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].OccupiedBy.ToCharArray();
                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                                }

                                bot_right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >= 0) && (currentCell.Rownumber - i >= 0 && currentCell.Rownumber - i < size) && bot_left)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].OccupiedBy.ToCharArray();
                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                bot_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber - i >= 0 && currentCell.Rownumber < size) && top_left)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].OccupiedBy.ToCharArray();
                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                bot_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber + i < size && currentCell.Rownumber >= 0) && top_right)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].OccupiedBy.ToCharArray();
                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].AllowedMove = true;
                                }

                                top_right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                    }

                    //vertikal o horisontelt / torn
                    //bools som säger ifall den kan gå åt respektive håll
                    right = true;
                    left = true;
                    up = true;
                    down = true;
                    for (int i = 0; i < size; i++)
                    {
                        if (currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >= 0 && up)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].OccupiedBy.ToCharArray();
                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;
                                }

                                up = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;
                        }
                        if (currentCell.Rownumber + i < size && currentCell.Rownumber + i >= 0 && right)
                        {
                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].OccupiedBy.ToCharArray();
                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
                                }

                                right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if (currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size && down)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].OccupiedBy.ToCharArray();
                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                                }

                                down = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                        }
                        if (currentCell.Rownumber - i >= 0 && currentCell.Rownumber - i < size && left)
                        {
                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].OccupiedBy.ToCharArray();

                                if (c[0] == 'V')
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                        }
                    }
                    break;
                case "VDrottning":
                    //diagonal / löpare
                    //löpare kan gå diagonalt
                    top_right = true;
                    top_left = true;
                    bot_right = true;
                    bot_left = true;
                    for (int i = 0; i < size; i++)
                    {
                        if ((currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >= 0) && (currentCell.Rownumber + i < size && currentCell.Rownumber + i >= 0) && bot_right)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].OccupiedBy.ToCharArray();
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                                }

                                bot_right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >= 0) && (currentCell.Rownumber - i >= 0 && currentCell.Rownumber - i < size) && bot_left)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].OccupiedBy.ToCharArray();
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                bot_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber - i >= 0 && currentCell.Rownumber < size) && top_left)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].OccupiedBy.ToCharArray();
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                bot_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber + i < size && currentCell.Rownumber >= 0) && top_right)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].OccupiedBy.ToCharArray();
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].AllowedMove = true;
                                }

                                top_right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                    }
                    //vertikal o horisontelt / torn
                    //bools som säger ifall den kan gå åt respektive håll
                    right = true;
                    left = true;
                    up = true;
                    down = true;
                    MessageBox.Show(currentCell.Columnnumber + " " + currentCell.Rownumber + " mark cell");
                    for (int i = 0; i < size; i++)
                    {
                        if (currentCell.Columnnumber + i < size || currentCell.Columnnumber + i >= 0 && up)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].OccupiedBy.ToCharArray();
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;
                                }

                                up = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;
                        }
                        if (currentCell.Rownumber + i < size || currentCell.Rownumber + i >= 0 && right)
                        {
                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].OccupiedBy.ToCharArray();
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
                                }

                                right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if (currentCell.Columnnumber - i >= 0 || currentCell.Columnnumber - i < size && down)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].OccupiedBy.ToCharArray();
                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                                }

                                down = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                        }
                        if (currentCell.Rownumber - i >= 0 || currentCell.Rownumber - i < size && left)
                        {
                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].Occupied)
                            {
                                char[] c = TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].OccupiedBy.ToCharArray();

                                if (c[0] == 'S')
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                        }
                    }
                    break;
                case "VHäst1":
                case "VHäst2":
                    //hästen kan gå två steg fram och ett åt sidan
                    if ((currentCell.Columnnumber + 2 < size && currentCell.Columnnumber + 2 >= 0) && (currentCell.Rownumber + 1 < size && currentCell.Rownumber + 1 >=0))
                    {
                        MessageBox.Show(currentCell.Columnnumber + " " + currentCell.Rownumber + " col + row + häst");
                        if (TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber - 2 >= 0 && currentCell.Columnnumber - 2 < size) && (currentCell.Rownumber + 1 < size && currentCell.Rownumber + 1 >= 0 ))
                    {
                        if (TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber - 2 < size && currentCell.Columnnumber - 2 >= 0) && (currentCell.Rownumber - 1 >= 0 && currentCell.Rownumber - 1 < size))
                    {

                        if (TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber + 1 < size && currentCell.Columnnumber + 1 >=0) && (currentCell.Rownumber - 2 < size && currentCell.Rownumber - 2 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber + 2 < size && currentCell.Columnnumber + 2 >=0) && (currentCell.Rownumber - 1 < size && currentCell.Rownumber - 1 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber + 1 < size && currentCell.Columnnumber + 1 >=0) && (currentCell.Rownumber + 2 < size && currentCell.Rownumber + 2 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber - 1 >= 0 && currentCell.Columnnumber - 1 < size ) && (currentCell.Rownumber + 2 < size && currentCell.Rownumber + 2 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber - 1 >= 0 && currentCell.Columnnumber - 1 < size) && (currentCell.Rownumber - 2 < size && currentCell.Rownumber - 2 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 2].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 2].OccupiedBy.ToCharArray();
                            if (c[0] == 'S')
                            {
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 2].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 2].AllowedMove = true;
                    }
                    break;
                case "SHäst1":
                case "SHäst2":

                    //hästen kan gå två steg fram och ett åt sidan
                    if ((currentCell.Columnnumber + 2 < size && currentCell.Columnnumber + 2 >= 0) && (currentCell.Rownumber + 1 < size && currentCell.Rownumber + 1 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber - 2 >= 0 && currentCell.Columnnumber - 2 < size) && (currentCell.Rownumber + 1 < size && currentCell.Rownumber + 1 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber + 2 < size && currentCell.Columnnumber + 2 >= 0) && (currentCell.Rownumber - 1 >= 0 && currentCell.Rownumber - 1 < size))
                    {
                        if (TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber + 1 < size && currentCell.Columnnumber + 1 >= 0) && (currentCell.Rownumber - 1 < size && currentCell.Rownumber - 1 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber + 2 < size && currentCell.Columnnumber + 2 >= 0) && (currentCell.Rownumber - 1 < size && currentCell.Rownumber - 1 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber + 1 < size && currentCell.Columnnumber + 1 >= 0) && (currentCell.Rownumber + 2 < size && currentCell.Rownumber + 2 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber - 1 >= 0 && currentCell.Columnnumber - 1 < size) && (currentCell.Rownumber + 2 < size && currentCell.Rownumber + 2 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber - 1 >= 0 && currentCell.Columnnumber - 1 < size) && (currentCell.Rownumber - 2 < size && currentCell.Rownumber - 2 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 2].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 2].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 2].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 2].AllowedMove = true;
                    }
                    if ((currentCell.Columnnumber + 1 < size && currentCell.Columnnumber + 1 >= 0) && (currentCell.Rownumber - 2 < size && currentCell.Rownumber - 2 >= 0))
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].Occupied)
                        {
                            char[] c = TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].OccupiedBy.ToCharArray();
                            if (c[0] == 'V')
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].AllowedMove = true;
                    }

                    break;
                case "VBonde":
                    //bonde kan gå ett elr två steg fram
                    if (TheGrid[currentCell.Columnnumber , currentCell.Rownumber - 1].Occupied)
                    {
                        char c = GetChar(TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].OccupiedBy);

                        if (c == 'S')
                        {
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].AllowedMove = true;
                        }
                    }
                    else
                    {
                        TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].AllowedMove = true;
                        if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 2].Occupied)
                        {
                            char c = GetChar(TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 2].OccupiedBy);
                            if (c == 'S')
                            {
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 2].AllowedMove = true;
                            }

                        }
                        else
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 2].AllowedMove = true;
                    }
                    if (currentCell.Columnnumber + 1 <= size || currentCell.Rownumber - 1 <= 0)
                    {

                    }
                    
                    else
                    {
                        //den kan oxå ta snett fram om det står någon där
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].Occupied)
                        {
                            char c = GetChar(TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].OccupiedBy);
                            if (c == 'S')
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].AllowedMove = true;
                            }

                        }
                    }
                    if (currentCell.Columnnumber + 1 >= size || currentCell.Rownumber + 1 >= size)
                    {

                    }
                    else
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].Occupied)
                        {
                            char c = GetChar(TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].OccupiedBy);
                            if (c == 'S')
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                    }
                    break;
                case "SBonde":
                    //bonde kan gå ett elr två steg fram
                    if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].Occupied)
                    {
                        char c = GetChar(TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].OccupiedBy);
                        if (c == 'V')
                        {
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].AllowedMove = true;
                        }
                    }
                    else
                    {
                        TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].AllowedMove = true;
                        if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 2].Occupied)
                        {
                            char c = GetChar(TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 2].OccupiedBy);
                            if (c == 'V')
                            {
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 2].AllowedMove = true;
                            }

                        }
                        else
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 2].AllowedMove = true;
                    }
                    //den kan oxå ta snett fram om det står någon där
                    if (currentCell.Columnnumber - 1 <= 0 || currentCell.Rownumber + 1 <= size)
                    {

                    }
                    else
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].Occupied)
                        {
                            char c = GetChar(TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].OccupiedBy);
                            if (c == 'V')
                            {
                                MessageBox.Show("ajdå +");
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].AllowedMove = true;
                            }

                        }
                    }
                    if (currentCell.Columnnumber - 1 <= 0 || currentCell.Rownumber - 1 <= 0)
                    {

                    }
                    else
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].Occupied)
                        {
                            char c = GetChar(TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].OccupiedBy);
                            if (c == 'V')
                            {
                                MessageBox.Show("ajdå -");

                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                    }
                    break;
                default:
                    MessageBox.Show("nu har nått gått väldigt fel!");
                    break;
            }
            //for (int i = 0; i < size; i++)
            //{
            //    for (int j = 0; j < size; j++)
            //    {
            //        if (TheGrid[i, j].Occupied)
            //        {
            //            MessageBox.Show(i.ToString() + " " + j.ToString());

            //        }
            //        else
            //        {
            //            if (TheGrid[i, j].OccupiedBy == TheGrid[2,3].OccupiedBy)
            //            {
            //                MessageBox.Show(i.ToString() + " " + j.ToString());


            //            }
            //        }
            //    }
            //}
        }

        private char GetChar(string occupiedBy)
        {
            char[] c = occupiedBy.ToCharArray();
            return c[0];
        }
    }
    public class cell
    {
        public int Rownumber { get; set; }
        public int Columnnumber { get; set; }
        public bool Occupied { get; set; }
        public string OccupiedBy { get; set; }

        public bool AllowedMove { get; set; }

        public cell(int y, int x)
        {
            Rownumber = x;
            Columnnumber = y;
        }
    }
}