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


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
            cell c;
            int i = 0;

            //nästade loopar för att gå igenom arrayn thegrid.
            for (int z = 0; z < Myboard.size; z++)
            {
                for (int j = 0; j < Myboard.size; j++)
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
                    //pieceGrid[i].Click += MarkallowedTiles;
                    // skapar en cell för varje ruta
                    cell cell = Myboard.TheGrid[z, j];
                    if (cell.Occupied)
                    {
                        switch (cell.OccupiedBy)
                        {
                            //vita pjäser
                            case "VBonde":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 6);
                                pieceGrid[i].Image = Properties.Resources.BondeI;
                                c = new cell(pieceSize * i, pieceSize * 6);
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
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Image = Properties.Resources.Rook;
                                c = new cell(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Tag = "VBonde" + " " + i;
                                pieceGrid[i].BackColor = Color.Black;
                                i++;
                                break;
                            case "VTorn2":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Image = Properties.Resources.Rook;
                                c = new cell(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Tag = "VBonde" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "VHäst1":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Image = Properties.Resources.KnightI;
                                c = new cell(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Tag = "VBonde" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "VHäst2":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Image = Properties.Resources.KnightI;
                                c = new cell(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Tag = "VBonde" + " " + i;
                                i++;
                                break;
                            case "VLöpare1":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Image = Properties.Resources.BishopIfylld;
                                c = new cell(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Tag = "VBonde" + " " + i;
                                i++;
                                break;
                            case "VLöpare2":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Image = Properties.Resources.BishopIfylld;
                                c = new cell(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Tag = "VBonde" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "VKung":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Image = Properties.Resources.KingI;
                                c = new cell(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Tag = "VBonde" + " " + i;
                                i++;
                                break;
                            case "VDrottning":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Image = Properties.Resources.QueenI;
                                c = new cell(pieceSize * j, pieceSize * 7);
                                pieceGrid[i].Tag = "VBonde" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            //svart pjäser
                            case "SBonde":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 1);
                                c = new cell(pieceSize * j, pieceSize * 1);
                                pieceGrid[i].Tag = "VBonde" + " " + i;
                                pieceGrid[i].Image = Properties.Resources.BondeI;
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
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Image = Properties.Resources.Rook;
                                c = new cell(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Tag = "STorn1" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                MessageBox.Show((pieceSize * j, pieceSize * 0).ToString());
                                pieceGrid[i].BringToFront();

                                i++;
                                break;
                            case "STorn2":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Image = Properties.Resources.Rook;
                                c = new cell(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Tag = "STorn2" + " " + i;
                                pieceGrid[i].BackColor = Color.Black;
                                i++;
                                break;
                            case "SHäst1":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Image = Properties.Resources.KnightI;
                                c = new cell(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Tag = "SHäst1" + " " + i;
                                pieceGrid[i].BackColor = Color.Black;
                                i++;
                                break;
                            case "SHäst2":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Image = Properties.Resources.KnightI;
                                c = new cell(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Tag = "SHäst2" + " " + i;
                                pieceGrid[i].BackColor = Color.White;

                                i++;
                                break;
                            case "SLöpare1":

                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Image = Properties.Resources.BishopIfylld;
                                c = new cell(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Tag = "SLöpare1" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "SLöpare2":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Image = Properties.Resources.BishopIfylld;
                                c = new cell(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Tag = "SLöpare2" + " " + i;
                                i++;
                                break;
                            case "SKung":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Image = Properties.Resources.KingI;
                                c = new cell(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Tag = "SKung" + " " + i;
                                i++;
                                break;
                            case "SDrottning":
                                pieceGrid[i].Location = new Point(pieceSize * j, pieceSize * 0);
                                pieceGrid[i].Image = Properties.Resources.QueenI;
                                c = new cell(pieceSize * j, pieceSize * 0);
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
            RadioButton r = (RadioButton)sender;
            string s = (string)r.Tag;
            string[] tag = s.Split(' '); 
            string piece = tag[0];
            MessageBox.Show(piece);
            int i = int.Parse(tag[1]);
            cell c = new cell(pieceGrid[i].Width/65, pieceGrid[i].Height/65);

            Myboard.Markallowedmove(c, piece);
            for (int x = 0; x < Myboard.size; x++)
            {
                for (int j = 0; j < Myboard.size; j++)
                {
                    cell cell = Myboard.TheGrid[x, j];

                    if (cell.AllowedMove)
                    {
                        MessageBox.Show("tjena");
                        buttonGrid[x, j].BackColor = Color.Green;
                    }
                }
            }
        }

        private void CreateButtons()
        {
            int buttonSize = panel1.Width / Myboard.size;
            for (int i = 0; i < Myboard.size; i++)
            {
                for (int j = 0; j < Myboard.size; j++)
                {
                    buttonGrid[i, j] = new Button();

                    buttonGrid[i, j].Width = buttonSize;
                    buttonGrid[i, j].Height = buttonSize;
                    buttonGrid[i, j].Click += buttonClick;

                    panel1.Controls.Add(buttonGrid[i, j]);
                    buttonGrid[i, j].Location = new Point(i * buttonGrid[i, j].Width, buttonGrid[i, j].Height * j);
                    if (((i + j) % 2) == 0) 
                    {
                        buttonGrid[i, j].BackColor = Color.White;
                    }
                    else
                        buttonGrid[i, j].BackColor = Color.Black;
                    buttonGrid[i, j].Tag = i +"," + j;
                    buttonGrid[i, j].FlatStyle = FlatStyle.Flat;
                    buttonGrid[i, j].FlatAppearance.BorderSize = 0;


                }
            }
        }

        private void buttonClick(object sender, EventArgs e)
        {

            Button b = (Button)sender;
            //hämtar den checkade radiobuttonen
            var rb = panel1.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
            string s = (string)rb.Tag;
            string[] tag = s.Split(' ');

            //if (b.BackColor == Color.Green)
            //{
                MovePiece(rb, b, tag[0]);
            


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
                    TheGrid[i, j] = new cell(i, j);

                }
            }
        }
        public void placePiecesInStart()
        {
            //svart
            for (int i = 1; i < 2; i++)
            {
                for (int j = 7; j >= 0; j--)
                {
                    TheGrid[i, j].Occupied = true;
                    TheGrid[i, j].OccupiedBy = "VBonde";

                }
            }
            TheGrid[7, 0].OccupiedBy = "VTorn1";
            TheGrid[7, 0].Occupied = true;
            TheGrid[7, 7].OccupiedBy = "VTorn2";
            TheGrid[7, 7].Occupied = true;

            TheGrid[7, 1].OccupiedBy = "VHäst1";
            TheGrid[7, 1].Occupied = true;
            TheGrid[7, 6].OccupiedBy = "VHäst2";
            TheGrid[7, 6].Occupied = true;

            TheGrid[7, 2].OccupiedBy = "VLöpare1";
            TheGrid[7, 2].Occupied = true;
            TheGrid[7, 5].OccupiedBy = "VLöpare2";
            TheGrid[7, 5].Occupied = true;

            TheGrid[7, 4].OccupiedBy = "VKung";
            TheGrid[7, 4].Occupied = true;

            TheGrid[7, 3].OccupiedBy = "VDrottning";
            TheGrid[7, 3].Occupied = true;
            //svart
            for (int i = 6; i > 5; i--)
            {
                for (int j = 7; j >= 0; j--)
                {
                    TheGrid[i, j].Occupied = true;
                    TheGrid[i, j].OccupiedBy = "SBonde";
                }
            }
            TheGrid[0, 0].OccupiedBy = "STorn1";
            TheGrid[0, 0].Occupied = true;
            TheGrid[0, 7].OccupiedBy = "STorn2";
            TheGrid[0, 7].Occupied = true;

            TheGrid[0, 1].OccupiedBy = "SHäst1";
            TheGrid[0, 1].Occupied = true;
            TheGrid[0, 6].OccupiedBy = "SHäst2";
            TheGrid[0, 6].Occupied = true;

            TheGrid[0, 2].OccupiedBy = "SLöpare1";
            TheGrid[0, 2].Occupied = true;
            TheGrid[0, 5].OccupiedBy = "SLöpare2";
            TheGrid[0, 5].Occupied = true;

            TheGrid[0, 3].OccupiedBy = "SKung";
            TheGrid[0, 3].Occupied = true;

            TheGrid[0, 4].OccupiedBy = "SDrottning";
            TheGrid[0, 4].Occupied = true;

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
                case "STorn1":
                case "STorn2":
                    //kan gå vertikalt och horisontellt
                    for (int i = 0; i < size; i++)
                    {
                        if (currentCell.Columnnumber + i < size)
                            TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;
                        if (currentCell.Rownumber + i < size)
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
                        if (currentCell.Columnnumber - i >= 0)
                            TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                        if (currentCell.Rownumber - i >= 0)
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                    }
                    break;
                case "VKung":
                case "SKung":
                    // kan gå ett steg åt alla håll
                    TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].AllowedMove = true;

                    break;
                case "SDrottning":
                case "VDrottning":

                    //diagonal / löpare
                    for (int i = 0; i < size; i++)
                    {
                        if (currentCell.Columnnumber + i < size && currentCell.Rownumber + i < size)
                        {
                            TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if (currentCell.Columnnumber + i < size && currentCell.Rownumber - i >= 0)
                        {
                            TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].AllowedMove = true;
                        }
                        if (currentCell.Columnnumber - i >= 0 && currentCell.Rownumber + i < size)
                        {
                            TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if (currentCell.Columnnumber - i >= 0 && currentCell.Rownumber - i >= 0)
                        {
                            TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].AllowedMove = true;
                        }

                    }
                    //vertikal o horisontelt / torn
                    for (int i = 0; i < size; i++)
                    {
                        if (currentCell.Columnnumber + i < size)
                            TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;
                        if (currentCell.Rownumber + i < size)
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
                        if (currentCell.Columnnumber - i >= 0)
                            TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                        if (currentCell.Rownumber - i >= 0)
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                    }
                    break;
                case "VLöpare1":
                case "VLöpare2":
                case "SLöpare1":
                case "SLöpare2":

                    //löpare kan gå diagonalt
                    for (int i = 0; i < size; i++)
                    {
                        if (currentCell.Columnnumber + i < size && currentCell.Rownumber + i < size)
                        {
                            TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if (currentCell.Columnnumber + i < size && currentCell.Rownumber - i >= 0)
                        {
                            TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].AllowedMove = true;
                        }
                        if (currentCell.Columnnumber - i >= 0 && currentCell.Rownumber + i < size)
                        {
                            TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if (currentCell.Columnnumber - i >= 0 && currentCell.Rownumber - i >= 0)
                        {
                            TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].AllowedMove = true;
                        }
                    }
                    break;
                case "VHäst1":
                case "VHäst2":
                case "SHäst1":
                case "SHäst2":

                    //hästen kan gå två steg fram och ett åt sidan
                    TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 2].AllowedMove = true;
                    break;
                case "VBonde":
                case "SBonde":

                    MessageBox.Show(currentCell.Columnnumber.ToString() +" " + currentCell.Rownumber.ToString());
                    //bonde kan gå ett elr två steg fram
                    TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].AllowedMove = true;

                    TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber].AllowedMove = true;

                    //den kan oxå ta snett fram om det står någon där
                    if (TheGrid[currentCell.Rownumber + 1, currentCell.Columnnumber + 1].Occupied)
                    {
                        TheGrid[currentCell.Rownumber + 1, currentCell.Columnnumber + 1].AllowedMove = true;
                    }
                    if (TheGrid[currentCell.Rownumber + 1, currentCell.Columnnumber - 1].Occupied)
                    {
                        TheGrid[currentCell.Rownumber + 1, currentCell.Columnnumber - 1].AllowedMove = true;
                    }
                    break;
                default:
                    MessageBox.Show("nu har nått gått väldigt fel!");
                    break;
            }
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