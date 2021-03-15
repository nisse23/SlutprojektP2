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
        static board Myboard = new board();
        public Button[,] buttonGrid = new Button[Myboard.size, Myboard.size];
        public RadioButton[] pieceGrid = new RadioButton[32];


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateButtons();
            Myboard.placePiecesInStart();

            GeneratePieces();

            

    }

    private void GeneratePieces()
        {
            int pieceSize = panel1.Width / Myboard.size;
            cell c;
            int i = 0;

            for (int z = 0; z < Myboard.size; z++)
            {
                for (int j = 0; j < Myboard.size; j++)
                {
                    
                    pieceGrid[i] = new RadioButton();
                    pieceGrid[i].Width = pieceSize;
                    pieceGrid[i].Height = pieceSize;
                    pieceGrid[i].Appearance = Appearance.Button;
                    pieceGrid[i].Image = Properties.Resources.BondeI;
                    pieceGrid[i].FlatStyle = FlatStyle.Flat;
                    panel1.Controls.Add(pieceGrid[i]);
                    pieceGrid[i].BringToFront();
                    switch (Myboard.TheGrid[z,j].OccupiedBy )
                    {
                        //vita pjäser
                        case "VBonde":
                            for (int x = 0; x < Myboard.size; x++)
                            {
                                pieceGrid[x].Location = new Point(pieceGrid[x].Width * j , pieceGrid[x].Height * z);
                                c = new cell(pieceGrid[x].Width * j, pieceGrid[x].Height * z);
                                pieceGrid[x].Tag = c;
                                pieceGrid[x].Click += MarkallowedTiles;
                                if (i % 2 == 0)
                                {
                                    pieceGrid[x].BackColor = Color.White;
                                }
                                else
                                {
                                    pieceGrid[x].BackColor = Color.Black;
                                }
                            }
                            i++;
                            break;
                        case "VTorn1":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.Rook;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            pieceGrid[i].BackColor = Color.Black;
                            i++;
                            break;
                        case "VTorn2":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.Rook;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            pieceGrid[i].BackColor = Color.Black;
                            i++;
                            break;
                        case "VHäst1":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.KnightI;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            pieceGrid[i].BackColor = Color.White;
                            i++;
                            break;
                        case "VHäst2":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.KnightI;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            i++;
                            break;
                        case "VLöpare1":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.BishopIfylld;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            i++;
                            break;
                        case "VLöpare2":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.BishopIfylld;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            pieceGrid[i].BackColor = Color.White;
                            i++;
                            break;
                        case "VKung":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.KingI;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            i++;
                            break;
                        case "VDrottning":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.QueenI;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            pieceGrid[i].BackColor = Color.White;
                            i++;
                            break;
                        //svart pjäser
                        case "SBonde":
                            for (int x = 0; x < Myboard.size; x++)
                            {
                                pieceGrid[x].Location = new Point(pieceGrid[x].Width * z, pieceGrid[x].Height * j);
                                c = new cell(pieceGrid[x].Width * z, pieceGrid[x].Height * j);
                                pieceGrid[x].Tag = c;
                                pieceGrid[x].Click += MarkallowedTiles;
                                if (i % 2 == 0)
                                {
                                    pieceGrid[x].BackColor = Color.White;
                                }
                                else
                                {
                                    pieceGrid[x].BackColor = Color.Black;
                                }
                            }
                            i++;
                            break;
                        case "STorn1":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.Rook;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            pieceGrid[i].BackColor = Color.White;

                            i++;
                            break;
                        case "STorn2":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.Rook;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            pieceGrid[i].BackColor = Color.Black;
                            i++;
                            MessageBox.Show((pieceGrid[i].Width * j).ToString(), (pieceGrid[i].Height * z).ToString());

                            break;
                        case "SHäst1":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.KnightI;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            pieceGrid[i].BackColor = Color.White;
                            i++;
                            break;
                        case "SHäst2":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.KnightI;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            i++;
                            break;
                        case "SLöpare1":

                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);

                            pieceGrid[i].Image = Properties.Resources.BishopIfylld;
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            i++;
                            break;
                        case "SLöpare2":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.BishopIfylld;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            pieceGrid[i].BackColor = Color.White;
                            i++;
                            break;
                        case "SKung":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.KingI;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            i++;
                            break;
                        case "SDrottning":
                            pieceGrid[i].Location = new Point(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Image = Properties.Resources.QueenI;
                            c = new cell(pieceGrid[i].Width * j, pieceGrid[i].Height * z);
                            pieceGrid[i].Tag = c;
                            pieceGrid[i].Click += MarkallowedTiles;
                            pieceGrid[i].BackColor = Color.White;
                            i++;
                            break;
                    }
                    
                }
            }
            //for (int i = 0; i < 32; i++)
            //{
            //        //placerar ut torn, häst, löpare, kung och drottning
            //        switch (i)
            //        {
            //            case 16:
            //                pieceGrid[i].Location = new Point(pieceGrid[i].Width * 0, pieceGrid[i].Height * 0);
            //                pieceGrid[i].Image = Properties.Resources.Rook;
            //                c = new cell(pieceGrid[i].Width * i, pieceGrid[i].Height * 1);
            //                pieceGrid[i].Tag = c;
            //                pieceGrid[i].Click += MarkallowedTiles;
            //                pieceGrid[i].BackColor = Color.White;

            //                break;
            //            case 17:
            //                pieceGrid[i].Location = new Point(pieceGrid[i].Width * 7, pieceGrid[i].Height * 0);
            //                pieceGrid[i].Image = Properties.Resources.Rook;
            //                c = new cell(pieceGrid[i].Width * i, pieceGrid[i].Height * 1);
            //                pieceGrid[i].Tag = c;
            //                pieceGrid[i].Click += MarkallowedTiles;
            //                pieceGrid[i].BackColor = Color.Black;

            //                break;
            //            case 22:
            //                pieceGrid[i].Location = new Point(pieceGrid[i].Width * 6, pieceGrid[i].Height * 0);
            //                pieceGrid[i].Image = Properties.Resources.KnightI;
            //                c = new cell(pieceGrid[i].Width * i, pieceGrid[i].Height * 1);
            //                pieceGrid[i].Tag = c;
            //                pieceGrid[i].Click += MarkallowedTiles;
            //                pieceGrid[i].BackColor = Color.White;

            //                break;
            //            case 23:
            //                pieceGrid[i].Location = new Point(pieceGrid[i].Width * 1, pieceGrid[i].Height * 0);
            //                pieceGrid[i].Image = Properties.Resources.KnightI;
            //                c = new cell(pieceGrid[i].Width * i, pieceGrid[i].Height * 1);
            //                pieceGrid[i].Tag = c;
            //                pieceGrid[i].Click += MarkallowedTiles;
            //                break;

            //            case 26:
            //                pieceGrid[i].Location = new Point(pieceGrid[i].Width * 5, pieceGrid[i].Height * 0);
            //                pieceGrid[i].Image = Properties.Resources.BishopS;
            //                c = new cell(pieceGrid[i].Width * i, pieceGrid[i].Height * 1);
            //                pieceGrid[i].Tag = c;
            //                pieceGrid[i].Click += MarkallowedTiles;
            //                break;
            //            case 27:
            //                pieceGrid[i].Location = new Point(pieceGrid[i].Width * 2, pieceGrid[i].Height * 0);
            //                pieceGrid[i].Image = Properties.Resources.BishopS;
            //                c = new cell(pieceGrid[i].Width * i, pieceGrid[i].Height * 1);
            //                pieceGrid[i].Tag = c;
            //                pieceGrid[i].Click += MarkallowedTiles;
            //                pieceGrid[i].BackColor = Color.White;

            //                break;
            //            case 28:
                            
            //                break;
            //            case 29:
                            
            //            case 30:
            //                pieceGrid[i].Location = new Point(pieceGrid[i].Width * 4, pieceGrid[i].Height * 0);
            //                pieceGrid[i].Image = Properties.Resources.KingI;
            //                c = new cell(pieceGrid[i].Width * i, pieceGrid[i].Height * 1);
            //                pieceGrid[i].Tag = c;
            //                pieceGrid[i].Click += MarkallowedTiles;
            //                pieceGrid[i].BackColor = Color.White;

            //                break;
            //            case 31:
            //                pieceGrid[i].Location = new Point(pieceGrid[i].Width * 3, pieceGrid[i].Height * 0);
            //                pieceGrid[i].Image = Properties.Resources.QueenS;
            //                c = new cell(pieceGrid[i].Width * i, pieceGrid[i].Height * 1);
            //                pieceGrid[i].Tag = c;
            //                pieceGrid[i].Click += MarkallowedTiles;

            //                break;
            //        }
                
                    

            //}
        }

        private void MarkallowedTiles(object sender, EventArgs e)
        {
            //RadioButton r = (RadioButton)sender;
            //pieceGrid
            //string piece;
            //if (i < 8)
            //{
            //    //svart bonde
            //    piece = "Bonde";
            //}
            //else if (i < 16)
            //{
            //    //vit bonde
            //    piece = "Bonde";
            //}
            //else
            //{
            //    piece = "Bonde";
            //    switch (i)
            //    {
            //        case 16:
            //            //svart torn
            //            piece = "Torn";
            //            break;
            //        case 17:
            //            //svart torn
            //            piece = "Torn";
            //            break;
            //        case 18:
            //            //vit torn
            //            piece = "Torn";
            //            break;
            //        case 19:
            //            //vit torn
            //            piece = "Torn";
            //            break;
            //        case 20:
            //            //vit häst
            //            piece = "Häst";
            //            break;
            //        case 21:
            //            //vit häst
            //            piece = "Häst";
            //            break;
            //        case 22:
            //            //svart häst
            //            piece = "Häst";

            //            break;
            //        case 23:
            //            //svart häst
            //            piece = "Häst";
            //            break;
            //        case 24:
            //            //vit löpare
            //            piece = "Löpare";
            //            break;
            //        case 25:
            //            //vit löpare
            //            piece = "Löpare";
            //            break;
            //        case 26:
            //            //svart löpare
            //            piece = "löpare";
            //            break;
            //        case 27:
            //            //svart löpare
            //            piece = "löpare";
            //            break;
            //        case 28:
            //            //vit drottning
            //            piece = "Drottning";
            //            break;
            //        case 29:
            //            //vit kung
            //            piece = "Kung";
            //            break;
            //        case 30:
            //            //svart drottning
            //            piece = "Drottning";
            //            break;
            //        case 31:
            //            //svart kung
            //            piece = "Kung";
            //            break;
            //        default:
            //            MessageBox.Show("error");
            //            piece = "Bonde";
            //            break;
            //    }

            //}
            //Myboard.Markallowedmove(pieceGrid[i].Tag, piece);
            //for (int x = 0; x < Myboard.size; x++)
            //{
            //    for (int j = 0; j < Myboard.size; j++)
            //    {
            //        if (Myboard.TheGrid[x, j].AllowedMove)
            //        {
            //            buttonGrid[i, j].BackColor = Color.Green;
            //        }
            //    }
            //}
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
            int x = 4;
            int y = 4;
            cell currentcell = new cell(x, y);
            Myboard.Markallowedmove(currentcell, "Bonde");


            RadioButton piece = new RadioButton();
            panel1.Controls.Add(piece);
            piece.Location = new Point(x* 65, 65 * y);
            piece.Image = Properties.Resources.Rook;
            piece.Width = panel1.Width / Myboard.size;
            piece.Height = panel1.Width / Myboard.size;
            piece.Appearance = Appearance.Button;
            piece.BackColor = Color.Black;
            piece.FlatStyle = FlatStyle.Flat;
            piece.BringToFront();
            if ((x + y) % 2 == 0)
            {
                piece.BackColor = Color.White;
            }
            else
            {
                piece.BackColor = Color.Black;
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
                    TheGrid[i, j] = new cell(i, j);

                }
            }
        }
        public void placePiecesInStart()
        {
            for (int i = 1; i < 2; i++)
            {
                for (int j = 7; j >= 0; j--)
                {
                    TheGrid[i, j].Occupied = true;
                    TheGrid[i, j].OccupiedBy = "VBonde";

                }
            }
            for (int i = 6; i > 5; i--)
            {
                for (int j = 7; j >= 0; j--)
                {
                    TheGrid[i, j].Occupied = true;
                    TheGrid[i, j].OccupiedBy = "SBonde";
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

            TheGrid[7, 3].OccupiedBy = "VKung";
            TheGrid[7, 3].Occupied = true;

            TheGrid[7, 4].OccupiedBy = "VDrottning";
            TheGrid[7, 4].Occupied = true;
            //svart
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
            TheGrid[currentCell.Columnnumber, currentCell.Rownumber].Occupied = true;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    TheGrid[i, j].AllowedMove = false;
                }
            }
            switch (piece)
            {
                case "Torn":
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
                case "Kung":
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
                case "Drottning":
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
                case "Löpare":
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
                case "Häst":
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
                case "Bonde":
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
