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
        static public board Myboard = new board();

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
            placePiecesInStart(Myboard);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    
                    if (Myboard.TheGrid[j, i].Occupied)
                    {
                        //MessageBox.Show(j + " " + i + Myboard.TheGrid[j, i].OccupiedBy.name);
                        
                    }
                }

            }

        }
        //skapar och placerar ut alla spelpjäser
        private void GeneratePieces()
        {
            //placerar dem rätt i arrayen
            placePiecesInStart(Myboard);

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
                        string s = cell.OccupiedBy.name;


                       
                        switch (cell.OccupiedBy.name)
                        {
                            //vita pjäser

                            case "VTorn1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitRook;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.Black;
                                i++;
                                break;
                            case "VTorn2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitRook;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "VHäst1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitKnight;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "VHäst2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitKnight;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                i++;
                                break;
                            case "VLöpare1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitBishop1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                i++;
                                break;
                            case "VLöpare2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitBishop1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "VKung":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitKung;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                i++;
                                break;
                            case "VDrottning":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.VitQueen;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            //svart pjäser
                            case "STorn1":
                                pieceGrid[i].Location = new Point(pieceSize * 0, pieceSize * 0);
                                pieceGrid[i].Image = Properties.Resources.SvartRook1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                pieceGrid[i].BringToFront();
                                i++;
                                break;
                            case "STorn2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.SvartRook1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.Black;
                                i++;
                                break;
                            case "SHäst1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKnight1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.Black;
                                i++;
                                break;
                            case "SHäst2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKnight1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;

                                i++;
                                break;
                            case "SLöpare1":

                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.SvartBishop2;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            case "SLöpare2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.SvartBishop2;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                i++;
                                break;
                            case "SKung":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKung1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                i++;
                                break;
                            case "SDrottning":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                pieceGrid[i].Image = Properties.Resources.SvartQueen1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                i++;
                                break;
                            default:
                                if (cell.OccupiedBy.name.StartsWith("VBonde"))
                                {
                                    pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                    pieceGrid[i].Image = Properties.Resources.VitPawn;
                                    pieceGrid[i].Tag = cell.OccupiedBy;
                                    MessageBox.Show("tjo");
                                    if (i % 2 == 0)
                                    {
                                        pieceGrid[i].BackColor = Color.White;
                                    }
                                    else
                                    {
                                        pieceGrid[i].BackColor = Color.Black;
                                    }
                                    i++;
                                }
                                else if (cell.OccupiedBy.name.StartsWith("SBonde"))
                                {
                                    pieceGrid[i].Location = new Point(pieceSize * cell.Rownumber, pieceSize * cell.Columnnumber);
                                    pieceGrid[i].Tag = cell.OccupiedBy;
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
                                }
                                else
                                    MessageBox.Show("genererade inte en pjäs");
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
            
            //RadioButton r1 = new RadioButton();
            
            //int y = r.Location.Y / 65;
            //int a = r.Location.X / 65;
            
            
            //if (buttonGrid[a, y].FlatAppearance.BorderSize == 4)
            //{
            //    for (int i = 0; i < 32; i++)
            //    {
            //        if (pieceGrid[i].Location == buttonGrid[a, y].Location)
            //        {
            //            r1 = pieceGrid[i];
            //            string s1 = (string)r1.Tag;
            //            string[] tag1 = s1.Split(' ');
            //            string piece1 = tag1[0];
            //            MovePiece(r1, buttonGrid[a, y], piece1);
            //            panel1.Controls.Remove(r);
            //        }

            //    }
                
            //}
            //else
            //{
                for (int z = 0; z < Myboard.size; z++)
                {
                    for (int j = 0; j < Myboard.size; j++)
                    {
                        buttonGrid[j, z].FlatAppearance.BorderSize = 0;


                    }
                }
                piece s = (piece)r.Tag;
                MessageBox.Show(s.currentCell.Columnnumber + " " + s.currentCell.Rownumber);
                Myboard.SelectedPiece = s;
                Myboard.Markallowedmove(s.currentCell, s);
                for (int x = 0; x < Myboard.size; x++)
                {
                    for (int j = 0; j < Myboard.size; j++)
                    {
                        cell cell = Myboard.TheGrid[j, x];

                        if (cell.AllowedMove)
                        {
                            //MessageBox.Show(j.ToString() +"," +  x.ToString() + "y,x allowed");
                            buttonGrid[j, x].FlatAppearance.BorderSize = 4;
                            buttonGrid[j, x].FlatAppearance.BorderColor = Color.Green;
                        }
                    }
                }
           //}
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
            MessageBox.Show(btag[0] + " " + btag[1] + " occ: " + Myboard.TheGrid[int.Parse(btag[0]), int.Parse(btag[1])].Occupied);
            if (Myboard.SelectedPiece != null)
            {
                piece p = Myboard.SelectedPiece;
                for (int i = 0; i < pieceGrid.Length; i++)
                {
                    if(p == pieceGrid[i].Tag)
                    {
                        RadioButton rb = pieceGrid[i];
                        if (b.FlatAppearance.BorderSize == 4)
                        {
                            MovePiece(rb, b, p);
                            for (int z = 0; z < Myboard.size; z++)
                            {
                                for (int j = 0; j < Myboard.size; j++)
                                {
                                    buttonGrid[j, z].FlatAppearance.BorderSize = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void MovePiece(RadioButton rb, Button b, piece Spiece)
        {
            //ändra i array
            int width = rb.Location.X / 65;
            int height = rb.Location.Y / 65;
            int newWidth = b.Location.X / 65;
            int newHeight = b.Location.Y / 65;
            Spiece.currentCell = new cell(newHeight, newWidth);
            Myboard.TheGrid[width, height].Occupied = false;
            Myboard.TheGrid[width, height].OccupiedBy = null;
            Myboard.TheGrid[newWidth, newHeight].Occupied = true;
            Myboard.TheGrid[newWidth, newHeight].OccupiedBy = Spiece;
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
            placePiecesInStart(Myboard);
        }
        private void placePiecesInStart(board myboard)
        {
            //vita pjäser
            PlaceWhitepieces(myboard);
            //svart pjäser
            PlaceBlackpieces(myboard);
        }

        private void PlaceBlackpieces(board myboard)
        {
            piece SBonde1 = new piece("SBonde1", new cell(0, 1), myboard);
            myboard.TheGrid[0, 1].OccupiedBy = SBonde1;
            SBonde1.IsWhite = false;

            piece SBonde2 = new piece("SBonde2", new cell(1, 1), myboard);
            myboard.TheGrid[1, 1].OccupiedBy = SBonde2;
            SBonde2.IsWhite = false;

            piece SBonde3 = new piece("SBonde3", new cell(2, 1), myboard);
            myboard.TheGrid[2, 1].OccupiedBy = SBonde3;
            SBonde3.IsWhite = false;

            piece SBonde4 = new piece("SBonde4", new cell(3, 1), myboard);
            myboard.TheGrid[3, 1].OccupiedBy = SBonde4;
            SBonde4.IsWhite = false;

            piece SBonde5 = new piece("SBonde5", new cell(4, 1), myboard);
            myboard.TheGrid[4, 1].OccupiedBy = SBonde5;
            SBonde5.IsWhite = false;

            piece SBonde6 = new piece("SBonde6", new cell(5, 1), myboard);
            myboard.TheGrid[5, 1].OccupiedBy = SBonde6;
            SBonde6.IsWhite = false;

            piece SBonde7 = new piece("SBonde7", new cell(6, 1), myboard);
            myboard.TheGrid[6, 1].OccupiedBy = SBonde7;
            SBonde7.IsWhite = false;

            piece SBonde8 = new piece("SBonde8", new cell(7, 1), myboard);
            myboard.TheGrid[7, 1].OccupiedBy = SBonde8;
            SBonde8.IsWhite = false;

            piece STorn1 = new piece("STorn1", new cell(0, 0), myboard);
            myboard.TheGrid[0, 0].OccupiedBy = STorn1;
            STorn1.IsWhite = false;

            piece STorn2 = new piece("STorn2", new cell(7, 0), myboard);
            myboard.TheGrid[7, 0].OccupiedBy = STorn2;
            STorn2.IsWhite = false;

            piece SHäst1 = new piece("SHäst1", new cell(1, 0), myboard);
            myboard.TheGrid[1, 0].OccupiedBy = SHäst1;
            SHäst1.IsWhite = false;

            piece SHäst2 = new piece("SHäst2", new cell(6, 0), myboard);
            myboard.TheGrid[6, 0].OccupiedBy = SHäst2;
            SHäst2.IsWhite = false;

            piece SLöpare1 = new piece("SLöpare1", new cell(2, 0), myboard);
            myboard.TheGrid[2, 0].OccupiedBy = SLöpare1;
            SLöpare1.IsWhite = false;

            piece SLöpare2 = new piece("SLöpare2", new cell(5, 0), myboard);
            myboard.TheGrid[5, 0].OccupiedBy = SLöpare2;
            SLöpare2.IsWhite = false;

            piece SKung = new piece("SKung", new cell(3, 0), myboard);
            myboard.TheGrid[3, 0].OccupiedBy = SKung;
            SKung.IsWhite = false;

            piece SDrottning = new piece("SDrottning", new cell(4, 0), myboard);
            myboard.TheGrid[4, 0].OccupiedBy = SDrottning;
            SDrottning.IsWhite = false;
        }

        private void PlaceWhitepieces(board myboard)
        {
            piece VBonde1 = new piece("VBonde1", new cell(0, 6), myboard);
            myboard.TheGrid[0, 6].OccupiedBy = VBonde1;

            piece VBonde2 = new piece("VBonde2", new cell(1, 6), myboard);
            myboard.TheGrid[1, 6].OccupiedBy = VBonde2;

            piece VBonde3 = new piece("VBonde3", new cell(2, 6), myboard);
            myboard.TheGrid[2, 6].OccupiedBy = VBonde3;

            piece VBonde4 = new piece("VBonde4", new cell(3, 6), myboard);
            myboard.TheGrid[3, 6].OccupiedBy = VBonde4;

            piece VBonde5 = new piece("VBonde5", new cell(4, 6), myboard);
            myboard.TheGrid[4, 6].OccupiedBy = VBonde5;

            piece VBonde6 = new piece("VBonde6", new cell(5, 6), myboard);
            myboard.TheGrid[5, 6].OccupiedBy = VBonde6;

            piece VBonde7 = new piece("VBonde7", new cell(6, 6), myboard);
            myboard.TheGrid[6, 6].OccupiedBy = VBonde7;

            piece VBonde8 = new piece("VBonde8", new cell(7, 6), myboard);
            myboard.TheGrid[7, 6].OccupiedBy = VBonde8;

            piece VTorn1 = new piece("VTorn1", new cell(0, 7), myboard);
            myboard.TheGrid[0, 7].OccupiedBy = VTorn1;

            piece VTorn2 = new piece("VTorn2", new cell(7, 7), myboard);
            myboard.TheGrid[7, 7].OccupiedBy = VTorn2;

            piece VHäst1 = new piece("VHäst1", new cell(1, 7), myboard);
            myboard.TheGrid[1, 7].OccupiedBy = VHäst1;

            piece VHäst2 = new piece("VHäst2", new cell(6, 7), myboard);
            myboard.TheGrid[6, 7].OccupiedBy = VHäst2;

            piece VLöpare1 = new piece("VLöpare1", new cell(2, 7), myboard);
            myboard.TheGrid[2, 7].OccupiedBy = VLöpare1;

            piece VLöpare2 = new piece("VLöpare2", new cell(5, 7), myboard);
            myboard.TheGrid[5, 7].OccupiedBy = VLöpare2;

            piece VKung = new piece("VKung", new cell(4, 7), myboard);
            myboard.TheGrid[4, 7].OccupiedBy = VKung;

            piece VDrottning = new piece("VDrottning", new cell(3, 7), myboard);
            myboard.TheGrid[3, 7].OccupiedBy = VDrottning;


        }
    }
    public class piece
    {
        public string name { get; set; }
        public cell currentCell { get; set; }
        public bool IsWhite { get; set; }

        public piece(string _name, cell _currentCell, board Myboard)
        {
            name = _name;
            currentCell = _currentCell;
            currentCell.Occupied = true;
            Myboard.TheGrid[currentCell.Columnnumber, currentCell.Rownumber].Occupied = true;
            IsWhite = true;
        }
    }
    public class board
    {
        //storleken på spelplanen
        public int size { get; set; }
        //en 2d array som motsvarar spelplanen
        public cell[,] TheGrid { get; set; }
        public piece SelectedPiece { get; set; }
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
        
        public void Markallowedmove(cell currentCell, piece Selectedpiece)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    TheGrid[i, j].AllowedMove = false;
                }
            }
            
            switch (Selectedpiece.name)
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
                    for (int i = 1; i < size; i++)
                    {

                        if (currentCell.Columnnumber + i < size && up)
                        {
                            //MessageBox.Show((currentCell.Columnnumber + i) + " " + (currentCell.Rownumber + i) + " up");

                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;
                                }

                                up = false;
                            }
                            else
                            {
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;

                            }
                        }
                        if (currentCell.Columnnumber - i >= 0 && right)
                        {
                            //MessageBox.Show((currentCell.Columnnumber + i) + " " + (currentCell.Rownumber - i) + " right");

                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                                }
                                
                                    right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                        }
                        if (currentCell.Rownumber - i >= 0 && down)
                        {
                           // MessageBox.Show((currentCell.Columnnumber - i) + " " + (currentCell.Rownumber - i) + " down");

                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                                }
                                
                                    down = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber , currentCell.Rownumber - i].AllowedMove = true;
                        }
                        if (currentCell.Rownumber + i < size && left)
                        {
                            //MessageBox.Show((currentCell.Columnnumber - i) + " " + (currentCell.Rownumber + i) + " left");

                            if (TheGrid[currentCell.Columnnumber , currentCell.Rownumber + i].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber , currentCell.Rownumber + i].AllowedMove = true;
                                }                                
                                left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
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
                    for (int i = 1; i < size; i++)
                    {
                        if (currentCell.Columnnumber + i < size && up)
                        {
                            MessageBox.Show((currentCell.Columnnumber + i) + " " + (currentCell.Rownumber + i) + " up");

                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].Occupied)
                            {
                                if (Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;
                                }

                                up = false;
                            }
                            else
                            {
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;

                            }
                        }
                        if (currentCell.Columnnumber - i >= 0 && right)
                        {
                            //MessageBox.Show((currentCell.Columnnumber + i) + " " + (currentCell.Rownumber - i) + " right");

                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].Occupied)
                            {
                                if (Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                                }

                                right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                        }
                        if (currentCell.Rownumber - i >= 0 && down)
                        {
                            // MessageBox.Show((currentCell.Columnnumber - i) + " " + (currentCell.Rownumber - i) + " down");

                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].Occupied)
                            {
                                if (Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                down = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                        }
                        if (currentCell.Rownumber + i < size && left)
                        {
                            //MessageBox.Show((currentCell.Columnnumber - i) + " " + (currentCell.Rownumber + i) + " left");

                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].Occupied)
                            {
                                if (Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
                                }
                                left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
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
                    for (int i = 1; i < size; i++)
                    {
                        if ((currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >=0) && (currentCell.Rownumber + i < size && currentCell.Rownumber + i >=0) && bot_right)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                                }

                                bot_right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                        if (currentCell.Columnnumber + i < size  && currentCell.Rownumber - i >= 0  && bot_left)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].AllowedMove = true;
                                }
                                bot_left = false;
                            }
                            else
                            {
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].AllowedMove = true;

                            }

                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber - i >= 0 && currentCell.Rownumber - i< size) && top_left)
                        {

                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].AllowedMove = true;
                                }
                                top_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].AllowedMove = true;
                        }
                        if (currentCell.Columnnumber - i >= 0 && currentCell.Rownumber + i < size && top_right)
                        {
                            MessageBox.Show("top_right");

                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].AllowedMove = true;
                                }
                                MessageBox.Show((currentCell.Columnnumber - i) + " " + (currentCell.Rownumber + i));

                                top_right = false;
                            }
                            else
                            {
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].AllowedMove = true;
                                MessageBox.Show("ajdå");
                            }
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
                    for (int i = 1; i < size; i++)
                    {
                        if ((currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >= 0) && (currentCell.Rownumber + i < size && currentCell.Rownumber + i >= 0) && bot_right)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
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
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                bot_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber - i >= 0 && currentCell.Rownumber - i < size) && top_left)
                        {

                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].Occupied)
                            {
                                if (Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                top_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber + i < size && currentCell.Rownumber + i >= 0) && top_right)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].Occupied)
                            {
                                if (Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].AllowedMove = true;
                                }
                                top_right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                    }
                    break;
                case "VKung":
                    // kan gå ett steg åt alla håll
                    if (currentCell.Columnnumber + 1 < size) 
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].Occupied)
                        {
                            if (!Selectedpiece.IsWhite)
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
                            if (!Selectedpiece.IsWhite)
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
                            if (!Selectedpiece.IsWhite)
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
                            if (!Selectedpiece.IsWhite)
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
                            if (!Selectedpiece.IsWhite)
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
                            if (!Selectedpiece.IsWhite)
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
                            if (!Selectedpiece.IsWhite)
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
                            if (!Selectedpiece.IsWhite)
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
                    if (currentCell.Columnnumber + 1 < size)
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].Occupied)
                        {
                            if (Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber].AllowedMove = true;
                    }
                    if (currentCell.Columnnumber - 1 >= 0)
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].Occupied)
                        {
                            if (Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber].AllowedMove = true;
                    }
                    if (currentCell.Columnnumber - 1 >= 0 && currentCell.Rownumber + 1 < size)
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].Occupied)
                        {
                            if (Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if (currentCell.Columnnumber - 1 >= 0 && currentCell.Rownumber - 1 >= 0)
                    {
 
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].Occupied)
                        {
                            if (Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].AllowedMove = true;
                    }
                    if ( currentCell.Columnnumber + 1 < size && currentCell.Rownumber + 1 < size)
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].Occupied)
                        {
                            if (Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if (currentCell.Columnnumber + 1 < size && currentCell.Rownumber - 1 >= 0)
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 1].Occupied)
                        {
                            if (Selectedpiece.IsWhite)
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
                            if (Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if (currentCell.Rownumber - 1 >= 0)
                    {
                        if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].Occupied)
                        {
                            if (Selectedpiece.IsWhite)
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
                    for (int i = 1; i < size; i++)
                    {
                        if ((currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >= 0) && (currentCell.Rownumber + i < size && currentCell.Rownumber + i >= 0) && bot_right)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].Occupied)
                            {
                                if (Selectedpiece.IsWhite)
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
                                if (Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                bot_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber - i >= 0 && currentCell.Rownumber - i < size) && top_left)
                        {

                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].Occupied)
                            {
                                if (Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                top_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber + i < size && currentCell.Rownumber + i >= 0) && top_right)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].Occupied)
                            {
                                if (Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].AllowedMove = true;
                                }
                                top_right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                    }

                    //vertikal o horisontelt / torn
                    //bools som säger ifall den kan gå åt respektive håll
                    right = true;
                    left = true;
                    up = true;
                    down = true;
                    for (int i = 1; i < size; i++)
                    {
                        if (currentCell.Columnnumber + i < size && up)
                        {
                            MessageBox.Show((currentCell.Columnnumber + i) + " " + (currentCell.Rownumber + i) + " up");

                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].Occupied)
                            {


                                if (Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;
                                }

                                up = false;
                            }
                            else
                            {
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;

                            }
                        }
                        if (currentCell.Columnnumber - i >= 0 && right)
                        {
                            //MessageBox.Show((currentCell.Columnnumber + i) + " " + (currentCell.Rownumber - i) + " right");

                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].Occupied)
                            {
                                if (Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                                }

                                right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                        }
                        if (currentCell.Rownumber - i >= 0 && down)
                        {
                            // MessageBox.Show((currentCell.Columnnumber - i) + " " + (currentCell.Rownumber - i) + " down");

                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].Occupied)
                            {
                                if (Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                down = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                        }
                        if (currentCell.Rownumber + i < size && left)
                        {
                            //MessageBox.Show((currentCell.Columnnumber - i) + " " + (currentCell.Rownumber + i) + " left");

                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].Occupied)
                            {
                                if (Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
                                }
                                left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
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
                    for (int i = 1; i < size; i++)
                    {
                        if ((currentCell.Columnnumber + i < size && currentCell.Columnnumber + i >= 0) && (currentCell.Rownumber + i < size && currentCell.Rownumber + i >= 0) && bot_right)
                        {
                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber + i].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
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
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                bot_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber - i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber - i >= 0 && currentCell.Rownumber - i < size) && top_left)
                        {

                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                top_left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber - i].AllowedMove = true;
                        }
                        if ((currentCell.Columnnumber - i >= 0 && currentCell.Columnnumber - i < size) && (currentCell.Rownumber + i < size && currentCell.Rownumber + i >= 0) && top_right)
                        {
                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].AllowedMove = true;
                                }
                                top_right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber + i].AllowedMove = true;
                        }
                    }
                    
                    //vertikal o horisontelt / torn
                    //bools som säger ifall den kan gå åt respektive håll
                    right = true;
                    left = true;
                    up = true;
                    down = true;
                    for (int i = 1; i < size; i++)
                    {
                        if (currentCell.Columnnumber + i < size && up)
                        {
                            MessageBox.Show((currentCell.Columnnumber + i) + " " + (currentCell.Rownumber + i) + " up");

                            if (TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;
                                }

                                up = false;
                            }
                            else
                            {
                                TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber].AllowedMove = true;

                            }
                        }
                        if (currentCell.Columnnumber - i >= 0 && right)
                        {
                            //MessageBox.Show((currentCell.Columnnumber + i) + " " + (currentCell.Rownumber - i) + " right");

                            if (TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                                }

                                right = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber - i, currentCell.Rownumber].AllowedMove = true;
                        }
                        if (currentCell.Rownumber - i >= 0 && down)
                        {
                            // MessageBox.Show((currentCell.Columnnumber - i) + " " + (currentCell.Rownumber - i) + " down");

                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                                }

                                down = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber - i].AllowedMove = true;
                        }
                        if (currentCell.Rownumber + i < size && left)
                        {
                            //MessageBox.Show((currentCell.Columnnumber - i) + " " + (currentCell.Rownumber + i) + " left");

                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
                                }
                                left = false;
                            }
                            else
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber + i].AllowedMove = true;
                        }
                    }
                    break;
                case "VHäst1":
                case "VHäst2":
                    //hästen kan gå två steg fram och ett åt sidan
                    if (currentCell.Columnnumber + 2 < size && currentCell.Rownumber + 1 < size )
                    {
                        MessageBox.Show(currentCell.Columnnumber + " " + currentCell.Rownumber + " col + row + häst");
                        if (TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1].Occupied)
                        {
                            if (!Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if (currentCell.Columnnumber - 2 >= 0 && currentCell.Rownumber + 1 < size)
                    {
                        if (TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1].Occupied)
                        {
                            if (!Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if (currentCell.Columnnumber - 2 >= 0 && currentCell.Rownumber - 1 >= 0)
                    {

                        if (TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1].Occupied)
                        {
                            if (!Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1].AllowedMove = true;
                    }
                    if (currentCell.Columnnumber + 1 < size && currentCell.Rownumber - 2 >= 0)
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].Occupied)
                        {
                            if (!Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].AllowedMove = true;
                    }
                    if (currentCell.Columnnumber + 2 < size && currentCell.Rownumber - 1 >= 0)
                    {
                        if (TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1].Occupied)
                        {
                            if (!Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1].AllowedMove = true;
                    }
                    if (currentCell.Columnnumber + 1 < size && currentCell.Rownumber + 2 < size)
                    {
                        if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2].Occupied)
                        {
                            if (!Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2].AllowedMove = true;
                    }
                    if (currentCell.Columnnumber - 1 >= 0 && currentCell.Rownumber + 2 < size)
                    {
                        MessageBox.Show((currentCell.Columnnumber - 1) + " " + (currentCell.Rownumber + 2));
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2].Occupied)
                        {
                            if (!Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2].AllowedMove = true;
                    }
                    if (currentCell.Columnnumber - 1 >= 0 && currentCell.Rownumber - 2 >= 0)
                    {
                        if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 2].Occupied)
                        {
                            if (!Selectedpiece.IsWhite)
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
                            if (Selectedpiece.IsWhite)
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
                            if (Selectedpiece.IsWhite)
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
                            if (Selectedpiece.IsWhite)
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
                            if (Selectedpiece.IsWhite)
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
                            if (Selectedpiece.IsWhite)
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
                            if (Selectedpiece.IsWhite)
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
                            if (Selectedpiece.IsWhite)
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
                            if (Selectedpiece.IsWhite)
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
                            if (Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].AllowedMove = true;
                    }

                    break;
                default:
                    if (Selectedpiece.name.StartsWith("VBonde"))
                    {
                        //bonde kan gå ett elr två steg fram
                        if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].Occupied)
                        {
                            if (!Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].AllowedMove = true;
                            }
                        }
                        else
                        {
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].AllowedMove = true;
                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 2].Occupied)
                            {
                                if (!Selectedpiece.IsWhite)
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
                                if (!Selectedpiece.IsWhite)
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
                                if (!Selectedpiece.IsWhite)
                                {
                                    TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 1].AllowedMove = true;
                                }
                            }
                        }
                    }
                    else if (Selectedpiece.name.StartsWith("SBonde"))
                    {
                        //bonde kan gå ett elr två steg fram
                        if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].Occupied)
                        {
                            if (Selectedpiece.IsWhite)
                            {
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                        {
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].AllowedMove = true;
                            if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 2].Occupied)
                            {
                                if (Selectedpiece.IsWhite)
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
                                if (Selectedpiece.IsWhite)
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
                                if (Selectedpiece.IsWhite)
                                {
                                    MessageBox.Show("ajdå -");

                                    TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 1].AllowedMove = true;
                                }
                            }
                        }
                    }
                    else
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
        public piece OccupiedBy { get; set; }

        public bool AllowedMove { get; set; }

        public cell(int y, int x)
        {
            Rownumber = x;
            Columnnumber = y;
        }
    }
}