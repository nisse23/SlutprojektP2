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
        public Form1()
        {
            InitializeComponent();

        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        board myboard;
        public void buttonOmstart_Click(object sender, EventArgs e)
        {
            //startar om spelet ifall det är startat och startar det annars
            if (myboard != null)
            {
                myboard.RemoveAllpieces();
                myboard.GeneratePieces();
            }

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            //skapar en spelplan och initierar spelet
            if(myboard == null)
                myboard = new board(panel1);

        }
    }
    public class move
    {
        //pjäsen som ska flyttas
        public piece SPiece { get; set; }

        //brädet som pjäsen flyttas på
        public board Myboard { get; set; }
        public move(board _Myboard, piece _SPiece)
        {
            SPiece = _SPiece;
            Myboard = _Myboard;
        }
        //flyttar pjäsen
        public void MovePiece(Button b)
        {
            string[] sa = Myboard.getBtntag(b);

            //ändra i array
            int newWidth = int.Parse(sa[1]);
            int newHeight = int.Parse(sa[0]);



            //frigör föregående cell
            Myboard.TheGrid[SPiece.currentCell.Columnnumber, SPiece.currentCell.Rownumber].Occupied = false;
            Myboard.TheGrid[SPiece.currentCell.Columnnumber, SPiece.currentCell.Rownumber].OccupiedBy = null;

            //ändrar cell i piecen
            SPiece.currentCell = new cell(newHeight, newWidth);

            //okuperar den nya cellen
            Myboard.TheGrid[newHeight, newWidth].Occupied = true;
            Myboard.TheGrid[newHeight, newWidth].OccupiedBy = SPiece;


            //change location of radiobutton
            SPiece.rb.Location = b.Location;
            SPiece.rb.Checked = false;
            SPiece.HasMoved = true;
            Myboard.SelectedPiece = null;

            if (((SPiece.rb.Location.X / 65) + (SPiece.rb.Location.Y / 65)) % 2 == 0)
            {
                SPiece.rb.BackColor = Color.White;
            }
            else
            {
                SPiece.rb.BackColor = Color.Black;
            }
            Myboard.IsSchack(SPiece);
        }       
    }
    public class piece
    {
        //namnet på pjäsen
        public string name { get; set; }

        //cellen som pjäsen står på just nu
        public cell currentCell { get; set; }
        
        //är pjäsen vit
        public bool IsWhite { get; set; }

        //radiobuttonen som pjäsen är kopplad till
        public RadioButton rb { get; set; }

        //specifierar ifall pjäsen har flyttats detta spelet
        public bool HasMoved { get; set; }

        public piece(string _name, cell _currentCell, board Myboard)
        {
            name = _name;
            currentCell = _currentCell;
            currentCell.Occupied = true;
            Myboard.TheGrid[currentCell.Columnnumber, currentCell.Rownumber].Occupied = true;
            IsWhite = true;
            HasMoved = false;
        }
    }
    public class board
    {

        //storleken på spelplanen
        public int size { get; set; }
        //en 2d array som motsvarar spelplanen
        public cell[,] TheGrid { get; set; }
        //den pjäsen som används just nu
        public piece SelectedPiece { get; set; }
        // lista med alla spelpjäser i form av radiobuttons
        public RadioButton[] pieceGrid;
        //panelen i UIn
        public Panel panel1 { get; set; }
        //en 2d array med alla knappar som motsvarar spelrutorna
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
            CreateButtons();
            GeneratePieces();
        }
        #region Metoder
        #region getcell-metoder
        //kollar och markerar  ifall cellen nedanför är tillåten
        private cell Down(cell newcell)
        {
            if (newcell.Rownumber + 1 < 8)
            {
                if (!TheGrid[newcell.Columnnumber, newcell.Rownumber + 1].Occupied)
                    TheGrid[newcell.Columnnumber, newcell.Rownumber + 1].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber, newcell.Rownumber + 1]);
                return new cell(newcell.Columnnumber, newcell.Rownumber + 1);
            }
            return null;
        }

        //kollar och markerar  ifall cellen ovanför är tillåten
        private cell Up(cell newcell)
        {
            if (newcell.Rownumber - 1 > -1)
            {
                if (!TheGrid[newcell.Columnnumber, newcell.Rownumber - 1].Occupied)
                    TheGrid[newcell.Columnnumber, newcell.Rownumber - 1].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber, newcell.Rownumber - 1]);
                return new cell(newcell.Columnnumber, newcell.Rownumber - 1);
            }
            return null;
        }

        //kollar och markerar ifall cellen till vänster är tillåten
        private cell Left(cell newcell)
        {
            if (newcell.Columnnumber - 1 > -1)
            {
                if (!TheGrid[newcell.Columnnumber - 1, newcell.Rownumber].Occupied)
                    TheGrid[newcell.Columnnumber - 1, newcell.Rownumber].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber - 1, newcell.Rownumber]);
                return new cell(newcell.Columnnumber - 1, newcell.Rownumber);

            }
            return null;
        }

        //kollar och markerar  ifall cellen till höger är tillåten
        private cell Right(cell newcell)
        {
            if (newcell.Columnnumber + 1 < 8)
            {
                if (!TheGrid[newcell.Columnnumber + 1, newcell.Rownumber].Occupied)
                    TheGrid[newcell.Columnnumber + 1, newcell.Rownumber].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber + 1, newcell.Rownumber]);
                return new cell(newcell.Columnnumber + 1, newcell.Rownumber);

            }
            return null;
        }

        //kollar och markerar ifall cellen uppe till vänster är tillåten
        private cell Up_left(cell newcell)
        {
            if (newcell.Columnnumber - 1 > -1 && newcell.Rownumber - 1 > -1)
            {
                if (!TheGrid[newcell.Columnnumber - 1, newcell.Rownumber - 1].Occupied)
                    TheGrid[newcell.Columnnumber - 1, newcell.Rownumber - 1].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber - 1, newcell.Rownumber - 1]);
                return new cell(newcell.Columnnumber - 1, newcell.Rownumber - 1);

            }
            return null;
        }

        //kollar och markerar ifall cellen upep till höger är tillåten
        private cell Up_right(cell newcell)
        {
            if (newcell.Columnnumber + 1 < 8 && newcell.Rownumber - 1 > -1)
            {
                if (!TheGrid[newcell.Columnnumber + 1, newcell.Rownumber - 1].Occupied)
                    TheGrid[newcell.Columnnumber + 1, newcell.Rownumber - 1].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber + 1, newcell.Rownumber - 1]);
                return new cell(newcell.Columnnumber + 1, newcell.Rownumber - 1);

            }
            return null;
        }

        //kollar och markerar ifall cellen nere till vänster är tillåten
        private cell Down_left(cell newcell)
        {
            if (newcell.Columnnumber + 1 < 8 && newcell.Rownumber + 1 < 8)
            {
                if (!TheGrid[newcell.Columnnumber + 1, newcell.Rownumber + 1].Occupied)
                    TheGrid[newcell.Columnnumber + 1, newcell.Rownumber + 1].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber + 1, newcell.Rownumber + 1]);
                return new cell(newcell.Columnnumber + 1, newcell.Rownumber + 1);
            }
            return null;
        }

        //kollar och markerar ifall cellen nere till höger är tillåten
        private cell Down_right(cell newcell)
        {

            if (newcell.Columnnumber - 1 > -1 && newcell.Rownumber + 1 < 8)
            {
                if (!TheGrid[newcell.Columnnumber - 1, newcell.Rownumber + 1].Occupied)
                    TheGrid[newcell.Columnnumber - 1, newcell.Rownumber + 1].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber - 1, newcell.Rownumber + 1]);
                return new cell(newcell.Columnnumber - 1, newcell.Rownumber + 1);
            }
            return null;
        }

        #region Hästmetoder
        //kollar och markerar ifall cellen ett steg vänster och två steg ner är tillåten       
        private void Left1Down2(cell currentCell, bool v)
        {
            if (currentCell.Columnnumber - 1 >= 0 && currentCell.Rownumber - 2 >= 0)
            {
                if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 2].Occupied)
                {
                    //ifall det är motståndarens pjäs markerar metoden den som allowed annars inte
                    Occupied(TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 2]);
                }
                else
                    TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber - 2].AllowedMove = true;
            }

        }

        //kollar och markerar ifall cellen ett steg vänster och två steg upp är tillåten
        private void Left1Up2(cell currentCell, bool v)
        {
            if (currentCell.Columnnumber - 1 >= 0 && currentCell.Rownumber + 2 < size)
            {
                if (TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2].Occupied)
                {
                    //ifall det är motståndarens pjäs markerar metoden den som allowed annars inte
                    Occupied(TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2]);
                }
                else
                    TheGrid[currentCell.Columnnumber - 1, currentCell.Rownumber + 2].AllowedMove = true;
            }

        }

        //kollar och markerar ifall cellen två steg vänster och ett steg ner är tillåten
        private void Left2Down1(cell currentCell, bool v)
        {
            if (currentCell.Columnnumber - 2 >= 0 && currentCell.Rownumber - 1 >= 0)
            {

                if (TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1].Occupied)
                {
                    //ifall det är motståndarens pjäs markerar metoden den som allowed annars inte
                    Occupied(TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1]);
                }
                else
                    TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber - 1].AllowedMove = true;
            }

        }

        //kollar och markerar ifall cellen två steg vänster och ett steg upp är tillåten
        private void Left2Up1(cell currentCell, bool v)
        {
            if (currentCell.Columnnumber - 2 >= 0 && currentCell.Rownumber + 1 < size)
            {
                if (TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1].Occupied)
                {
                    //ifall det är motståndarens pjäs markerar metoden den som allowed annars inte
                    Occupied(TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1]);
                }
                else
                    TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber + 1].AllowedMove = true;
            }

        }

        //kollar och markerar ifall cellen ett steg höger och två steg upp är tillåten
        private void Right1Up2(cell currentCell, bool v)
        {
            if (currentCell.Columnnumber + 1 < size && currentCell.Rownumber + 2 < size)
            {
                if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2].Occupied)
                {
                    //ifall det är motståndarens pjäs markerar metoden den som allowed annars inte
                    Occupied(TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2]);
                }
                else
                    TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber + 2].AllowedMove = true;
            }

        }

        //kollar och markerar ifall cellen ett steg höger och två steg ner är tillåten
        private void Right1Down2(cell currentCell, bool v)
        {
            if (currentCell.Columnnumber + 1 < size && currentCell.Rownumber - 2 >= 0)
            {
                if (TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].Occupied)
                {
                    //ifall det är motståndarens pjäs markerar metoden den som allowed annars inte
                    Occupied(TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2]);
                }
                else
                    TheGrid[currentCell.Columnnumber + 1, currentCell.Rownumber - 2].AllowedMove = true;
            }

        }

        //kollar och markerar ifall cellen två steg höger och ett steg ner är tillåten
        private void Right2Down1(cell currentCell, bool v)
        {
            if (currentCell.Columnnumber + 2 < size && currentCell.Rownumber - 1 >= 0)
            {
                if (TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1].Occupied)
                {
                    //ifall det är motståndarens pjäs markerar metoden den som allowed annars inte
                    Occupied(TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1]);
                }
                else
                    TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber - 1].AllowedMove = true;
            }

        }

        //kollar och markerar ifall cellen två steg höger och ett steg upp är tillåten
        private void Right2Up1(cell currentCell, bool v)
        {
            if (currentCell.Columnnumber + 2 < size && currentCell.Rownumber + 1 < size)
            {
                if (TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1].Occupied)
                {
                    //ifall det är motståndarens pjäs markerar metoden den som allowed annars inte
                    Occupied(TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1]);
                }
                else
                    TheGrid[currentCell.Columnnumber + 2, currentCell.Rownumber + 1].AllowedMove = true;
            }

        }
        #endregion
        #endregion

        #region pjäsmetoder
        //hämtar de tillåtna dragen för tornet
        void Torn(cell cell)
        {
            bool right = true;
            bool left = true;
            bool down = true;
            bool up = true;
            cell newcell = SelectedPiece.currentCell;

            while (right)
            {
                newcell = Right(newcell);
                if ((newcell == null || !TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove) || TheGrid[newcell.Columnnumber, newcell.Rownumber].Occupied)
                    break;                   
            }
            newcell = SelectedPiece.currentCell;
            while (left)
            {
                newcell = Left(newcell);
                if ((newcell == null || !TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove) || TheGrid[newcell.Columnnumber, newcell.Rownumber].Occupied)
                    break;                
            }
            newcell = SelectedPiece.currentCell;

            while (down)
            {
                newcell = Down(newcell);
                if ((newcell == null || !TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove) || TheGrid[newcell.Columnnumber, newcell.Rownumber].Occupied)
                {
                    break;
                }              
            }
            newcell = SelectedPiece.currentCell;
            while (up)
            {             
                newcell = Up(newcell);
                if ((newcell == null || !TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove) || TheGrid[newcell.Columnnumber, newcell.Rownumber].Occupied)
                {
                    break;
                }               
            }


        }

        //hämtar de tillåtna dragen för löparen
        private void Lopare(cell currentCell)
        {
            bool upleft = true;
            bool upright = true;
            bool downleft = true;
            bool downright = true;
            cell newcell = SelectedPiece.currentCell;
            while (downright)
            {
                newcell = Down_right(newcell);
                if ((newcell == null || !TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove) || TheGrid[newcell.Columnnumber, newcell.Rownumber].Occupied)
                    break;
            }
            newcell = SelectedPiece.currentCell;
            while (downleft)
            {
                newcell = Down_left(newcell);
                if ((newcell == null || !TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove) || TheGrid[newcell.Columnnumber, newcell.Rownumber].Occupied)
                    break;
            }
            newcell = SelectedPiece.currentCell;
            while (upleft)
            {
                newcell = Up_left(newcell);
                if ((newcell == null || !TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove) || TheGrid[newcell.Columnnumber, newcell.Rownumber].Occupied)
                    break;
            }
            newcell = SelectedPiece.currentCell;
            while (upright)
            {

                newcell = Up_right(newcell);
                if ((newcell == null || !TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove) || TheGrid[newcell.Columnnumber, newcell.Rownumber].Occupied)
                    break;
            }
        }

        //hämtar de tillåtna dragen för kungen
        private void Kung(cell currentCell)
        {
            Down(currentCell);
            Up(currentCell);
            Left(currentCell);
            Right(currentCell);
            Up_left(currentCell);
            Up_right(currentCell);
            Down_left(currentCell);
            Down_right(currentCell);
        }

        //hämtar de tillåtna dragen för hästen
        private void Hast(cell currentCell, bool v)
        {
            Right2Up1(currentCell, v);
            Left2Up1(currentCell, v);
            Left2Down1(currentCell, v);
            Right1Down2(currentCell, v);
            Right2Down1(currentCell, v);
            Right1Up2(currentCell, v);
            Left1Up2(currentCell, v);
            Left1Down2(currentCell, v);
        }

        //hämtar de tillåtna dragen för vit bonde
        private void VBonde(cell newcell, piece p)
        {
            cell c;
            c = Up_left(newcell);
            if (c != null && !TheGrid[c.Columnnumber, c.Rownumber].Occupied)
                TheGrid[c.Columnnumber, c.Rownumber].AllowedMove = false;

            c = Up_right(newcell);
            if (c != null && !TheGrid[c.Columnnumber, c.Rownumber].Occupied)
                TheGrid[c.Columnnumber, c.Rownumber].AllowedMove = false;

            c = Up(newcell);
            if (c != null && TheGrid[c.Columnnumber, c.Rownumber].Occupied)
                TheGrid[c.Columnnumber, c.Rownumber].AllowedMove = false;
            if (c != null && !p.HasMoved)
            {
                Up(c);
            }
        }

        //hämtar de tillåtna dragen för svart bonde
        private void SBonde(cell newcell, piece p)
        {
            cell c;
            c = Down_left(newcell);
            if (c != null && !TheGrid[c.Columnnumber, c.Rownumber].Occupied)
                TheGrid[c.Columnnumber, c.Rownumber].AllowedMove = false;

            c = Down_right(newcell);
            if (c != null && !TheGrid[c.Columnnumber, c.Rownumber].Occupied)
                TheGrid[c.Columnnumber, c.Rownumber].AllowedMove = false;

            c = Down(newcell);
            if (c != null && TheGrid[c.Columnnumber, c.Rownumber].Occupied)
                TheGrid[c.Columnnumber, c.Rownumber].AllowedMove = false;
            if (c != null && !p.HasMoved)
            {
                Down(c);
            }
        }
        #endregion

        //kollar ifall rutan är tagen av motståndarens pjäs och markerar den isf
        private void Occupied(cell newcell)
        {
            if(TheGrid[newcell.Columnnumber, newcell.Rownumber].OccupiedBy.IsWhite != SelectedPiece.IsWhite)
            {
                TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove = true;
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
            int pieceSize = panel1.Width / size;
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
            piece s = (piece)r.Tag;
            bool t = false;

            if (SelectedPiece != null && SelectedPiece.IsWhite != s.IsWhite)
            {

                t = CapturePiece(s, SelectedPiece);
            }
            if(t == false)
            {
                resetbtns();

                SelectedPiece = s;
                Markallowedmove(s.currentCell, s);
                for (int x = 0; x < size; x++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        cell cell = TheGrid[j, x];

                        markbtns(cell);
                        
                    }
                }
            }

        }

        //markerar de tillåtna dragen
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
                case "STorn1":
                case "STorn2":
                    //kan gå vertikalt och horisontellt
                    Torn(currentCell);
                    break;
                case "VLöpare1":
                case "VLöpare2":
                case "SLöpare1":
                case "SLöpare2":
                    //löpare kan gå diagonalt
                    Lopare(currentCell);
                    break;
                case "VKung":
                case "SKung":

                    // kan gå ett steg åt alla håll
                    Kung(currentCell);
                    break;
                case "SDrottning":
                case "VDrottning":

                    //diagonal / löpare
                    Lopare(currentCell);
                    //vertikal o horisontelt / torn
                    Torn(currentCell);
                    break;
                case "VHäst1":
                case "VHäst2":
                    //hästen kan gå två steg fram och ett åt sidan
                    Hast(currentCell, false);
                    break;
                case "SHäst1":
                case "SHäst2":
                    //hästen kan gå två steg fram och ett åt sidan
                    Hast(currentCell, true);
                    break;
                default:
                    if (Selectedpiece.name.StartsWith("VBonde"))
                    {
                        VBonde(currentCell, Selectedpiece);

                    }
                    else if (Selectedpiece.name.StartsWith("SBonde"))
                    {
                        SBonde(currentCell, Selectedpiece);
                    }
                    else
                        MessageBox.Show("nu har nått gått väldigt fel!");
                    break;
            }
        }

        //markerar de btns som man får flytta till
        private void markbtns(cell cell)
        {
            
            if (cell.AllowedMove)
            {
                if (cell.Occupied)
                {
                    cell.OccupiedBy.rb.FlatAppearance.BorderSize = 4;
                    cell.OccupiedBy.rb.FlatAppearance.BorderColor = Color.Green;

                }
                buttonGrid[cell.Columnnumber, cell.Rownumber].FlatAppearance.BorderSize = 4;
                buttonGrid[cell.Columnnumber, cell.Rownumber].FlatAppearance.BorderColor = Color.Green;
            }
        }

        //tar motståndarens pjäs ifall det är tillåtet
        private bool CapturePiece(piece oldPiece, piece newPiece)
        {
            if (oldPiece.rb.FlatAppearance.BorderSize == 4)
            {
                panel1.Controls.Remove(oldPiece.rb);
                TheGrid[oldPiece.currentCell.Columnnumber, oldPiece.currentCell.Rownumber].Occupied = false;
                move move = new move(this, newPiece);
                move.MovePiece(buttonGrid[oldPiece.currentCell.Columnnumber, oldPiece.currentCell.Rownumber]);
                resetbtns();
                if(oldPiece.name.EndsWith("Kung"))
                {
                    if (newPiece.IsWhite)
                        MessageBox.Show("Vit vann");
                    else
                        MessageBox.Show("Svart vann");
                    RemoveAllpieces();
                    GeneratePieces();
                }
                return true;
            }
            return false;
        }

        //tar bort alla rbs och tar bort dem från arrayn
        public void RemoveAllpieces()
        {
            for (int i = 0; i < pieceGrid.Length; i++)
            {
                panel1.Controls.Remove(pieceGrid[i]);
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    TheGrid[j, i].Occupied = false;
                    TheGrid[j, i].OccupiedBy = null;
                }
            }
            resetbtns();
        }

        //när man klickar på en button triggas denna
        private void buttonClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string[] btag = getBtntag(b);
            //MessageBox.Show(btag[0] + " " + btag[1] + " occ: " + TheGrid[int.Parse(btag[0]), int.Parse(btag[1])].Occupied);
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

        //tar bort den tillåtna markeringen på knapparna och radiobtns
        private void resetbtns()
        {
            //tar bort indikatorn och ändrar statusen på cellen
            for (int z = 0; z < size; z++)
            {
                for (int j = 0; j < size; j++)
                {
                    buttonGrid[j, z].FlatAppearance.BorderSize = 0;
                    TheGrid[j, z].AllowedMove = false;
                    if (TheGrid[j, z].Occupied)
                        TheGrid[j, z].OccupiedBy.rb.FlatAppearance.BorderSize = 0;
                }
            }
        }

        //hämtar tagen från en knapp
        public string[] getBtntag(Button b)
        {
            string str = (string)b.Tag;

            return str.Split(',');
        }

        //kollar ifall det är schack
        internal void IsSchack(piece sPiece)
        {
            SelectedPiece = sPiece;
            Markallowedmove(sPiece.currentCell, sPiece);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (TheGrid[j, i].Occupied == true && TheGrid[j, i].OccupiedBy.name.EndsWith("Kung") && TheGrid[j, i].AllowedMove)
                    {
                        MessageBox.Show("schack");
                    }
                }
            }
            SelectedPiece = null;
            resetbtns();

        }
        #endregion
    }
    public class cell
    {
        //radnumret på cell
        public int Rownumber { get; set; }

        //kolumnnumret på cell
        public int Columnnumber { get; set; }

        //är cellen upptagen
        public bool Occupied { get; set; }

        //den pjäsen som ev okuperar cellen
        public piece OccupiedBy { get; set; }

        //får en pjäs gå hit
        public bool AllowedMove { get; set; }

        public cell(int y, int x)
        {
            Columnnumber = y;
            Rownumber = x;
        }
    }
}