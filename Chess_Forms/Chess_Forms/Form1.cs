﻿using System;
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
            GeneratePieces();


            

    }

    private void GeneratePieces()
        {
            int pieceSize = panel1.Width / Myboard.size;
            for (int i = 0; i < 17; i++)
            {
                pieceGrid[i] = new RadioButton();
                pieceGrid[i].Width = pieceSize;
                pieceGrid[i].Height = pieceSize;
                pieceGrid[i].Appearance = Appearance.Button;
                pieceGrid[i].Image = Properties.Resources.Rook;
                pieceGrid[i].FlatStyle = FlatStyle.Flat;
                panel1.Controls.Add(pieceGrid[i]);
                pieceGrid[i].BringToFront();

                if (i < 8)
                {
                    //pieceGrid[i].Click += MarkallowedTiles();

                    pieceGrid[i].Location = new Point(pieceGrid[i].Width * i, pieceGrid[i].Height * 1);
                    if (i % 2 == 0)
                    {
                        pieceGrid[i].BackColor = Color.Black;
                    }
                    else
                    {
                        pieceGrid[i].BackColor = Color.White;
                    }
                }
                else if (i < 16)
                {
                    pieceGrid[i].Location = new Point(pieceGrid[i].Width * (i - 8), pieceGrid[i].Height * 6);
                    if (i % 2 == 0)
                    {
                        pieceGrid[i].BackColor = Color.White;
                    }
                    else
                    {
                        pieceGrid[i].BackColor = Color.Black;
                    }
                }
                else if(i <18)
                {
                    
                    pieceGrid[i].Location = new Point(0, 0);
                }
                    

            }
        }

        //private EventHandler MarkallowedTiles()
        //{
        //   // Myboard.Markallowedmove();
        //}

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
                    TheGrid[i, j].OccupiedBy = "Bonde";

                }
            }
            for (int i = 6; i > 5; i--)
            {
                for (int j = 7; j >= 0; j--)
                {
                    TheGrid[i, j].Occupied = true;
                    TheGrid[i, j].OccupiedBy = "Bonde";
                }
            }
            TheGrid[7, 0].OccupiedBy = "Torn";
            TheGrid[7, 0].Occupied = true;
            TheGrid[7, 7].OccupiedBy = "Torn";
            TheGrid[7, 7].Occupied = true;

            TheGrid[7, 1].OccupiedBy = "Häst";
            TheGrid[7, 1].Occupied = true;
            TheGrid[7, 6].OccupiedBy = "Häst";
            TheGrid[7, 6].Occupied = true;

            TheGrid[7, 2].OccupiedBy = "Löpare";
            TheGrid[7, 2].Occupied = true;
            TheGrid[7, 5].OccupiedBy = "Löpare";
            TheGrid[7, 5].Occupied = true;

            TheGrid[7, 3].OccupiedBy = "Kung";
            TheGrid[7, 3].Occupied = true;

            TheGrid[7, 4].OccupiedBy = "Drottning";
            TheGrid[7, 4].Occupied = true;
            //svart
            TheGrid[0, 0].OccupiedBy = "Torn";
            TheGrid[0, 0].Occupied = true;
            TheGrid[0, 7].OccupiedBy = "Torn";
            TheGrid[0, 7].Occupied = true;

            TheGrid[0, 1].OccupiedBy = "Häst";
            TheGrid[0, 1].Occupied = true;
            TheGrid[0, 6].OccupiedBy = "Häst";
            TheGrid[0, 6].Occupied = true;

            TheGrid[0, 2].OccupiedBy = "Löpare";
            TheGrid[0, 2].Occupied = true;
            TheGrid[0, 5].OccupiedBy = "Löpare";
            TheGrid[0, 5].Occupied = true;

            TheGrid[0, 3].OccupiedBy = "Kung";
            TheGrid[0, 3].Occupied = true;

            TheGrid[0, 4].OccupiedBy = "Drottning";
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
