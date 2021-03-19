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
            int i = 0;

            //nästade loopar för att gå igenom arrayn thegrid.
            for (int z = 0; z < Myboard.size; z++)
            {
                for (int j = 0; j < Myboard.size; j++)
                {
                    
                    // skapar en cell för varje ruta
                    cell cell = Myboard.TheGrid[z, j];
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
                                
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.BondeI;
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
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.Rook;
                                pieceGrid[i].Tag = "VTorn1" + " " + i;
                                pieceGrid[i].BackColor = Color.Black;
                                i++;
                                break;
                            case "VTorn2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.Rook;
                                pieceGrid[i].Tag = "VTorn2" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "VHäst1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.KnightI;
                                pieceGrid[i].Tag = "VHäst1" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "VHäst2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.KnightI;
                                pieceGrid[i].Tag = "VHäst2" + " " + i;
                                i++;
                                break;
                            case "VLöpare1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.BishopIfylld;
                                pieceGrid[i].Tag = "VLöpare1" + " " + i;
                                i++;
                                break;
                            case "VLöpare2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.BishopIfylld;
                                pieceGrid[i].Tag = "VLöpare2" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "VKung":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.KingI;
                                pieceGrid[i].Tag = "VKung" + " " + i;
                                i++;
                                break;
                            case "VDrottning":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.QueenI;
                                pieceGrid[i].Tag = "VDrottning" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            //svart pjäser
                            case "SBonde":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Tag = "SBonde" + " " + i;
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
                                pieceGrid[i].Location = new Point(pieceSize * 0, pieceSize * 0);
                                pieceGrid[i].Image = Properties.Resources.Rook;
                                pieceGrid[i].Tag = "STorn1" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                pieceGrid[i].BringToFront();
                                i++;
                                break;
                            case "STorn2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.Rook;
                                pieceGrid[i].Tag = "STorn2" + " " + i;
                                pieceGrid[i].BackColor = Color.Black;
                                i++;
                                break;
                            case "SHäst1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.KnightI;
                                pieceGrid[i].Tag = "SHäst1" + " " + i;
                                pieceGrid[i].BackColor = Color.Black;
                                i++;
                                break;
                            case "SHäst2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.KnightI;
                                pieceGrid[i].Tag = "SHäst2" + " " + i;
                                pieceGrid[i].BackColor = Color.White;

                                i++;
                                break;
                            case "SLöpare1":

                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.BishopIfylld;
                                pieceGrid[i].Tag = "SLöpare1" + " " + i;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "SLöpare2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.BishopIfylld;
                                pieceGrid[i].Tag = "SLöpare2" + " " + i;
                                i++;
                                break;
                            case "SKung":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.KingI;
                                pieceGrid[i].Tag = "SKung" + " " + i;
                                i++;
                                break;
                            case "SDrottning":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.QueenI;
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
            int i = int.Parse(tag[1]);
            cell c = new cell(pieceGrid[i].Location.Y / 65, pieceGrid[i].Location.X / 65);
            Myboard.Markallowedmove(c, piece);
            for (int x = 0; x < Myboard.size; x++)
            {
                for (int j = 0; j < Myboard.size; j++)
                {
                    cell cell = Myboard.TheGrid[x, j];

                    if (cell.AllowedMove)
                    {
                        MessageBox.Show(x.ToString() +"," +  j.ToString() + "y,x allowed");
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
                    buttonGrid[i, j].Location = new Point(i * buttonGrid[i, j].Height, buttonGrid[i, j].Width * j);
                    if (((j+ i) % 2) == 0) 
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
           
           
                for (int j = 7; j >= 0; j--)
                {
                    TheGrid[6, j].Occupied = true;
                    TheGrid[6, j].OccupiedBy = "VBonde";

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
                for (int j = 7; j >= 0; j--)
                {
                    TheGrid[1, j].Occupied = true;
                    TheGrid[1, j].OccupiedBy = "SBonde";
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
                        MessageBox.Show((currentCell.Columnnumber - 2) + " " + (currentCell.Rownumber - 1));
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
                    if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].Occupied)
                    {
                        char c = GetChar(TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].OccupiedBy);
                        if (c == 'S')
                        {
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].AllowedMove = true;
                        }
                    }
                    else
                    {
                        TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].AllowedMove = true;
                        if (TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber].Occupied)
                        {
                            char c = GetChar(TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber].OccupiedBy);
                            if (c == 'S')
                            {
                                TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber].AllowedMove = true;
                            }

                        }
                        else
                            TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber].AllowedMove = true;
                    }
                    //den kan oxå ta snett fram om det står någon där
                    if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].Occupied)
                    {
                        char c = GetChar(TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].OccupiedBy);
                        if (c =='S')
                        {
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].AllowedMove = true;
                        }
                        
                    }
                    if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].Occupied)
                    {
                        char c = GetChar(TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].OccupiedBy);
                        if (c == 'S')
                        {
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].AllowedMove = true;
                        }
                    }
                    break;
                case "SBonde":
                    //bonde kan gå ett elr två steg fram
                    if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].Occupied)
                    {
                        char c = GetChar(TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].OccupiedBy);
                        if (c == 'V')
                        {
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].AllowedMove = true;
                        }
                    }
                    else
                    {
                        TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].AllowedMove = true;
                        if (TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber].Occupied)
                        {
                            char c = GetChar(TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber].OccupiedBy);
                            if (c == 'V')
                            {
                                TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber].AllowedMove = true;
                            }

                        }
                        else
                            TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber].AllowedMove = true;
                    }
                    //den kan oxå ta snett fram om det står någon där
                    if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].Occupied)
                    {
                        char c = GetChar(TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].OccupiedBy);
                        if (c == 'V')
                        {
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].AllowedMove = true;
                        }

                    }
                    if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].Occupied)
                    {
                        char c = GetChar(TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].OccupiedBy);
                        if (c == 'V')
                        {
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].AllowedMove = true;
                        }
                    }
                    break;
                default:
                    MessageBox.Show("nu har nått gått väldigt fel!");
                    break;
            }
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

        public cell(int x, int y)
        {
            Rownumber = x;
            Columnnumber = y;
        }
    }
}