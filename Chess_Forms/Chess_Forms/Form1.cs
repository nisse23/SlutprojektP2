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



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //skapar en spelplan
            board Myboard = new board(panel1);


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

        private void buttonOmstart_Click(object sender, EventArgs e)
        {
            //placePiecesInStart();
        }

    }
    public class move
    {
        public piece SPiece { get; set; }
        public board Myboard { get; set; }
        public move(board _Myboard, piece _SPiece)
        {
            SPiece = _SPiece;
            Myboard = _Myboard;
        }
        public void MovePiece(Button b)
        {
            string[] sa = Myboard.getBtntag(b);

            //ändra i array
            int newWidth = int.Parse(sa[1]);
            int newHeight = int.Parse(sa[0]);



            //frigör föregående cell
            Myboard.TheGrid[SPiece.currentCell.Columnnumber, SPiece.currentCell.Rownumber].Occupied = false;
            Myboard.TheGrid[SPiece.currentCell.Columnnumber, SPiece.currentCell.Rownumber].OccupiedBy = null;

            SPiece.currentCell = new cell(newHeight, newWidth);

            //okuperar den nya cellen
            Myboard.TheGrid[newHeight, newWidth].Occupied = true;
            Myboard.TheGrid[newHeight, newWidth].OccupiedBy = SPiece;


            //change location of radiobutton
            SPiece.rb.Location = b.Location;
            SPiece.rb.Checked = false;

            if (((SPiece.rb.Location.X / 65) + (SPiece.rb.Location.Y / 65)) % 2 == 0)
            {
                SPiece.rb.BackColor = Color.White;
            }
            else
            {
                SPiece.rb.BackColor = Color.Black;
            }

            
        }

        
    }
    public class piece
    {
        public string name { get; set; }
        public cell currentCell { get; set; }
        public bool IsWhite { get; set; }
        public RadioButton rb { get; set; }
        public bool HasMoved { get; set; }

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
        // lista med alla spelpjäser i form av radiobuttons
        public RadioButton[] pieceGrid;
        public Panel panel1 { get; set; }
        Button[,] buttonGrid ;

        public board(Panel _panel1)
        {
            panel1 = _panel1;
            size = 8;
            pieceGrid = new RadioButton[32];
            buttonGrid = new Button[size, size];
            TheGrid = new cell[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    TheGrid[y, x] = new cell(y, x);

                }
            }
            placePiecesInStart();
            CreateButtons();
            GeneratePieces();
        }
        #region Metoder

        public void Markallowedmove(cell currentCell, piece Selectedpiece)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    TheGrid[j, i].AllowedMove = false;
                }
            }

            switch (Selectedpiece.name)
            {
                case "VTorn1":
                case "VTorn2":
                    //kan gå vertikalt och horisontellt
                    Torn(currentCell, false);

                    break;
                case "STorn1":
                case "STorn2":
                    //kan gå vertikalt och horisontellt
                    Torn(currentCell, true);
                    break;
                case "VLöpare1":
                case "VLöpare2":
                    //löpare kan gå diagonalt
                    bool top_right = true;
                    bool top_left = true;
                    bool bot_right = true;
                    bool bot_left = true;
                    Lopare(currentCell, false);           
                    break;
                case "SLöpare1":
                case "SLöpare2":
                    //löpare kan gå diagonalt
                    top_right = true;
                    top_left = true;
                    bot_right = true;
                    bot_left = true;
                    Lopare(currentCell, true);
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
                    if ((currentCell.Columnnumber - 1 >= 0 && currentCell.Columnnumber - 1 < size) && (currentCell.Rownumber + 1 < size && currentCell.Rownumber + 1 >= 0))
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
                                TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].AllowedMove = true;
                            }
                        }
                        else
                            TheGrid[currentCell.Columnnumber, currentCell.Rownumber + 1].AllowedMove = true;
                    }
                    if (currentCell.Rownumber - 1 >= 0 && currentCell.Rownumber - 1 < size)
                    {
                        if (TheGrid[currentCell.Columnnumber, currentCell.Rownumber - 1].Occupied)
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
                    if (currentCell.Columnnumber + 1 < size && currentCell.Rownumber + 1 < size)
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

                    Torn(currentCell, true);
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
                    Torn(currentCell, false);

                    break;
                case "VHäst1":
                case "VHäst2":
                    //hästen kan gå två steg fram och ett åt sidan
                    if (currentCell.Columnnumber + 2 < size && currentCell.Rownumber + 1 < size)
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
                        cell newcell = Up(SelectedPiece.currentCell);
                        if(newcell != null)
                        {
                            Up(newcell);
                        }
                    }
                    else if (Selectedpiece.name.StartsWith("SBonde"))
                    {
                        cell newcell = Down(SelectedPiece.currentCell);
                        if (newcell != null)
                        {
                            Down(newcell);
                        }
                    }
                    else
                        MessageBox.Show("nu har nått gått väldigt fel!");
                    break;
            }

        }

        private void Lopare(cell currentCell, bool v)
        {
            bool topleft = true;
            bool topright = true;
            bool downleft = true;
            bool downright = true;

            while(downright)
            {
                cell newcell = SelectedPiece.currentCell;
                if (newcell.Columnnumber + 1 <= 8 && newcell.Rownumber + 1 <= 8)
                    break;
                newcell = Down_right(SelectedPiece.currentCell);
                if (newcell == null)
                    break;
                
                MessageBox.Show("1");
            }
            while (downleft)
            {
                cell newcell = SelectedPiece.currentCell;
                if (newcell.Columnnumber - 1 >= 0 && newcell.Rownumber + 1 <= 8)
                    break;
                newcell = Down_left(SelectedPiece.currentCell);
                if (newcell == null)
                    break;
            }
            while (topleft)
            {
                cell newcell = SelectedPiece.currentCell;
                if (newcell.Columnnumber - 1 >= 0 && newcell.Rownumber - 1 >= 0)
                    break;
                newcell = Up_left(SelectedPiece.currentCell);
                if (newcell == null)
                    break;
            }
            while (topright)
            {
                cell newcell = SelectedPiece.currentCell;
                if (newcell.Columnnumber + 1 <= 0 && newcell.Rownumber - 1 >= 8)
                    break;
                newcell = Up_right(SelectedPiece.currentCell);
                if (newcell == null)
                    break;
            }
        }
        #region getcellmetoder
        private cell Down(cell newcell)
        {
            if (TheGrid[newcell.Columnnumber, newcell.Rownumber + 1].Occupied)
                return null;
            TheGrid[newcell.Columnnumber, newcell.Rownumber + 1].AllowedMove = true;
            return new cell(newcell.Columnnumber, newcell.Rownumber + 1);
        }

        private cell Up(cell newcell)
        {
            if (TheGrid[newcell.Columnnumber, newcell.Rownumber - 1].Occupied)
                return null;
            TheGrid[newcell.Columnnumber, newcell.Rownumber - 1].AllowedMove = true;
            return new cell(newcell.Columnnumber, newcell.Rownumber - 1);
        }
        private cell Left(cell newcell)
        {
            MessageBox.Show("left");
            if (TheGrid[newcell.Columnnumber - 1, newcell.Rownumber].Occupied)
            {
                MessageBox.Show("null");
                return null;
            }
            TheGrid[newcell.Columnnumber - 1, newcell.Rownumber].AllowedMove = true;
            return new cell(newcell.Columnnumber - 1, newcell.Rownumber);
        }
        private cell Right(cell newcell)
        {
            if (TheGrid[newcell.Columnnumber + 1, newcell.Rownumber].Occupied)
                return null;
            TheGrid[newcell.Columnnumber + 1, newcell.Rownumber].AllowedMove = true;
            return new cell(newcell.Columnnumber + 1, newcell.Rownumber);
        }

        private cell Up_left(cell newcell)
        {
            if (TheGrid[newcell.Columnnumber - 1, newcell.Rownumber - 1].Occupied)
                return null;
            TheGrid[newcell.Columnnumber - 1, newcell.Rownumber - 1].AllowedMove = true;
            return new cell(newcell.Columnnumber - 1, newcell.Rownumber - 1);
        }
        private cell Up_right(cell newcell)
        {
            if (TheGrid[newcell.Columnnumber + 1, newcell.Rownumber - 1].Occupied)
                return null;
            TheGrid[newcell.Columnnumber + 1, newcell.Rownumber - 1].AllowedMove = true;
            return new cell(newcell.Columnnumber + 1, newcell.Rownumber - 1);
        }
        private cell Down_left(cell newcell)
        {
            if (TheGrid[newcell.Columnnumber + 1, newcell.Rownumber + 1].Occupied)
                return null;
            TheGrid[newcell.Columnnumber + 1, newcell.Rownumber + 1].AllowedMove = true;
            return new cell(newcell.Columnnumber + 1, newcell.Rownumber + 1);
        }
        private cell Down_right(cell newcell)
        {
            if (TheGrid[newcell.Columnnumber - 1, newcell.Rownumber + 1].Occupied)
                return null;
            TheGrid[newcell.Columnnumber - 1, newcell.Rownumber + 1].AllowedMove = true;
            return new cell(newcell.Columnnumber - 1, newcell.Rownumber + 1);
        }
        #endregion
        void Torn(cell cell, bool v)
        {
            bool cplus = true;
            bool cmin = true;
            bool rplus = true;
            bool rmin = true;
            cell newcell = SelectedPiece.currentCell;

            while (cplus)
            {
                MessageBox.Show("right");

                if (newcell.Columnnumber + 1 == 8)
                {
                    MessageBox.Show("nej right" + (newcell.Columnnumber + 1));
                    break;
                }
                newcell = Right(SelectedPiece.currentCell);
                if (newcell == null)
                    break;                   
            }
            newcell = SelectedPiece.currentCell;
            while (cmin)
            {
                MessageBox.Show("left");

                if (newcell.Columnnumber - 1 == 0)
                {
                    MessageBox.Show("nej left" + (newcell.Columnnumber - 1));
                    break;
                }
                newcell = Left(SelectedPiece.currentCell);
                if (newcell == null)
                    break;                
            }
            newcell = SelectedPiece.currentCell;

            while (rplus)
            {
                MessageBox.Show("down");

                if (newcell.Rownumber + 1 == 8)
                {
                    MessageBox.Show("nej down" + (newcell.Rownumber + 1));
                    break;
                }
                newcell = Down(SelectedPiece.currentCell);
                if (newcell == null)
                {
                    break;
                }              
            }
            newcell = SelectedPiece.currentCell;
            while (rmin)
            {
                
                MessageBox.Show(newcell.Rownumber + "första ");

                if ((newcell.Rownumber - 1) == 0)
                {
                    MessageBox.Show("nej up" + (newcell.Rownumber - 1));
                    break;
                }
                newcell = Up(SelectedPiece.currentCell);
                MessageBox.Show(newcell.Rownumber + " ");
                if (newcell == null)
                {
                    break;
                }               
            }


        }
        //plaserar ut alla pjäser i arrayn
        public void placePiecesInStart()
        {
            #region svartapjäser
            piece SBonde1 = new piece("SBonde1", new cell(0, 1), this);
            this.TheGrid[0, 1].OccupiedBy = SBonde1;
            SBonde1.IsWhite = false;

            piece SBonde2 = new piece("SBonde2", new cell(1, 1), this);
            this.TheGrid[1, 1].OccupiedBy = SBonde2;
            SBonde2.IsWhite = false;

            piece SBonde3 = new piece("SBonde3", new cell(2, 1), this);
            this.TheGrid[2, 1].OccupiedBy = SBonde3;
            SBonde3.IsWhite = false;

            piece SBonde4 = new piece("SBonde4", new cell(3, 1), this);
            this.TheGrid[3, 1].OccupiedBy = SBonde4;
            SBonde4.IsWhite = false;

            piece SBonde5 = new piece("SBonde5", new cell(4, 1), this);
            this.TheGrid[4, 1].OccupiedBy = SBonde5;
            SBonde5.IsWhite = false;

            piece SBonde6 = new piece("SBonde6", new cell(5, 1), this);
            this.TheGrid[5, 1].OccupiedBy = SBonde6;
            SBonde6.IsWhite = false;

            piece SBonde7 = new piece("SBonde7", new cell(6, 1), this);
            this.TheGrid[6, 1].OccupiedBy = SBonde7;
            SBonde7.IsWhite = false;

            piece SBonde8 = new piece("SBonde8", new cell(7, 1), this);
            this.TheGrid[7, 1].OccupiedBy = SBonde8;
            SBonde8.IsWhite = false;

            piece STorn1 = new piece("STorn1", new cell(0, 0), this);
            this.TheGrid[0, 0].OccupiedBy = STorn1;
            STorn1.IsWhite = false;

            piece STorn2 = new piece("STorn2", new cell(7, 0), this);
            this.TheGrid[7, 0].OccupiedBy = STorn2;
            STorn2.IsWhite = false;

            piece SHäst1 = new piece("SHäst1", new cell(1, 0), this);
            this.TheGrid[1, 0].OccupiedBy = SHäst1;
            SHäst1.IsWhite = false;

            piece SHäst2 = new piece("SHäst2", new cell(6, 0), this);
            this.TheGrid[6, 0].OccupiedBy = SHäst2;
            SHäst2.IsWhite = false;

            piece SLöpare1 = new piece("SLöpare1", new cell(2, 0), this);
            this.TheGrid[2, 0].OccupiedBy = SLöpare1;
            SLöpare1.IsWhite = false;

            piece SLöpare2 = new piece("SLöpare2", new cell(5, 0), this);
            this.TheGrid[5, 0].OccupiedBy = SLöpare2;
            SLöpare2.IsWhite = false;

            piece SKung = new piece("SKung", new cell(3, 0), this);
            this.TheGrid[3, 0].OccupiedBy = SKung;
            SKung.IsWhite = false;

            piece SDrottning = new piece("SDrottning", new cell(4, 0), this);
            this.TheGrid[4, 0].OccupiedBy = SDrottning;
            SDrottning.IsWhite = false;
            #endregion
            #region vita pjäser
            piece VBonde1 = new piece("VBonde1", new cell(0, 6), this);
            this.TheGrid[0, 6].OccupiedBy = VBonde1;

            piece VBonde2 = new piece("VBonde2", new cell(1, 6), this);
            this.TheGrid[1, 6].OccupiedBy = VBonde2;

            piece VBonde3 = new piece("VBonde3", new cell(2, 6), this);
            this.TheGrid[2, 6].OccupiedBy = VBonde3;

            piece VBonde4 = new piece("VBonde4", new cell(3, 6), this);
            this.TheGrid[3, 6].OccupiedBy = VBonde4;

            piece VBonde5 = new piece("VBonde5", new cell(4, 6), this);
            this.TheGrid[4, 6].OccupiedBy = VBonde5;

            piece VBonde6 = new piece("VBonde6", new cell(5, 6), this);
            this.TheGrid[5, 6].OccupiedBy = VBonde6;

            piece VBonde7 = new piece("VBonde7", new cell(6, 6), this);
            this.TheGrid[6, 6].OccupiedBy = VBonde7;

            piece VBonde8 = new piece("VBonde8", new cell(7, 6), this);
            this.TheGrid[7, 6].OccupiedBy = VBonde8;

            piece VTorn1 = new piece("VTorn1", new cell(0, 7), this);
            this.TheGrid[0, 7].OccupiedBy = VTorn1;

            piece VTorn2 = new piece("VTorn2", new cell(7, 7), this);
            this.TheGrid[7, 7].OccupiedBy = VTorn2;

            piece VHäst1 = new piece("VHäst1", new cell(1, 7), this);
            this.TheGrid[1, 7].OccupiedBy = VHäst1;

            piece VHäst2 = new piece("VHäst2", new cell(6, 7), this);
            this.TheGrid[6, 7].OccupiedBy = VHäst2;

            piece VLöpare1 = new piece("VLöpare1", new cell(2, 7), this);
            this.TheGrid[2, 7].OccupiedBy = VLöpare1;

            piece VLöpare2 = new piece("VLöpare2", new cell(5, 7), this);
            this.TheGrid[5, 7].OccupiedBy = VLöpare2;

            piece VKung = new piece("VKung", new cell(4, 7), this);
            this.TheGrid[4, 7].OccupiedBy = VKung;

            piece VDrottning = new piece("VDrottning", new cell(3, 7), this);
            this.TheGrid[3, 7].OccupiedBy = VDrottning;
            #endregion

        }
        // skapar buttonsen som visas i formen
        public void CreateButtons()
        {
            int buttonSize = panel1.Width / size;

            for (int i = 0; i < size; i++)
            {
                //label[i] = new Label();
                //label[i].Text = "va fan e det som händer";
                //label[i].Location = label1.Location;
                //label[i].BringToFront();


                for (int j = 0; j < size; j++)
                {
                    buttonGrid[j, i] = new Button();

                    buttonGrid[j, i].Width = buttonSize;
                    buttonGrid[j, i].Height = buttonSize;
                    buttonGrid[j, i].Click += buttonClick;

                    panel1.Controls.Add(buttonGrid[j, i]);
                    buttonGrid[j, i].Location = new Point(j * buttonGrid[j, i].Height, buttonGrid[j, i].Width * i);
                    if (((j + i) % 2) == 0)
                    {
                        buttonGrid[j, i].BackColor = Color.White;
                    }
                    else
                        buttonGrid[j, i].BackColor = Color.Black;
                    buttonGrid[j, i].Tag = j + "," + i;
                    buttonGrid[j, i].FlatStyle = FlatStyle.Flat;
                    buttonGrid[j, i].FlatAppearance.BorderSize = 0;
                }
            }
        }

        //skapar och placerar ut alla spelpjäser
        public void GeneratePieces()
        {
            //placerar dem rätt i arrayen
            placePiecesInStart();

            //räknar ut storleken på btns. =65
            int pieceSize = 65;
            //variabler
            int i = 0;

            //nästade loopar för att gå igenom arrayn thegrid.
            for (int z = 0; z < this.size; z++)
            {
                for (int j = 0; j < this.size; j++)
                {

                    // skapar en cell för varje ruta
                    cell cell = this.TheGrid[j, z];
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
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitRook;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.Black;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "VTorn2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitRook;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "VHäst1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitKnight;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "VHäst2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitKnight;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "VLöpare1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitBishop1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "VLöpare2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitBishop1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "VKung":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitKung;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "VDrottning":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitQueen;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            //svart pjäser
                            case "STorn1":
                                pieceGrid[i].Location = new Point(pieceSize * 0, pieceSize * 0);
                                pieceGrid[i].Image = Properties.Resources.SvartRook1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                cell.OccupiedBy.rb = pieceGrid[i];

                                i++;
                                break;
                            case "STorn2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartRook1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.Black;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "SHäst1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKnight1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.Black;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "SHäst2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKnight1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "SLöpare1":

                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartBishop2;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "SLöpare2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartBishop2;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "SKung":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKung1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "SDrottning":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartQueen1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Color.White;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            default:
                                if (cell.OccupiedBy.name.StartsWith("VBonde"))
                                {
                                    pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                    pieceGrid[i].Image = Properties.Resources.VitPawn;
                                    pieceGrid[i].Tag = cell.OccupiedBy;
                                    cell.OccupiedBy.rb = pieceGrid[i];

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
                                    pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                    pieceGrid[i].Tag = cell.OccupiedBy;
                                    pieceGrid[i].Image = Properties.Resources.SvartPawn1;
                                    cell.OccupiedBy.rb = pieceGrid[i];

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
        //markerar de tillåtna buttons
        private void MarkallowedTiles(object sender, EventArgs e)
        {
            RadioButton r = (RadioButton)sender;


            for (int z = 0; z < this.size; z++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    buttonGrid[j, z].FlatAppearance.BorderSize = 0;


                }
            }

            piece s = (piece)r.Tag;
            MessageBox.Show(s.currentCell.Columnnumber + " " + s.currentCell.Rownumber);
            SelectedPiece = s;
            Markallowedmove(s.currentCell, s);
            for (int x = 0; x < this.size; x++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    cell cell = this.TheGrid[j, x];

                    if (cell.AllowedMove)
                    {
                        //MessageBox.Show(j.ToString() +"," +  x.ToString() + "y,x allowed");
                        buttonGrid[j, x].FlatAppearance.BorderSize = 4;
                        buttonGrid[j, x].FlatAppearance.BorderColor = Color.Green;
                    }
                }
            }

        }

        //när man klickar på en button triggas denna
        private void buttonClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string[] btag = getBtntag(b);
            MessageBox.Show(btag[0] + " " + btag[1] + " occ: " + TheGrid[int.Parse(btag[0]), int.Parse(btag[1])].Occupied);
            if (SelectedPiece != null)
            {
                piece s = SelectedPiece;
                if (b.FlatAppearance.BorderSize == 4)
                {
                    move m = new move(this, s);
                    m.MovePiece(b);
                    resetbtns();
                }
                    
                
            }
        }

        private void resetbtns()
        {
            //tar bort indikatorn och ändrar statusen på cellen
            for (int z = 0; z < size; z++)
            {
                for (int j = 0; j < size; j++)
                {
                    buttonGrid[j, z].FlatAppearance.BorderSize = 0;
                    TheGrid[j, z].AllowedMove = false;
                }
            }
        }
        public string[] getBtntag(Button b)
        {
            string str = (string)b.Tag;

            return str.Split(',');
        }
        #endregion
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
            Columnnumber = y;
            Rownumber = x;
        }
    }
}
