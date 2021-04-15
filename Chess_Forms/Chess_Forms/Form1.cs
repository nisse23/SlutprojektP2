using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//TO DO
// promota bönder
// enpassent
// rockad
// fixa UI
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
        Board myboard;
        #region Actions
        //ändrar färgen på tiles
        private void Färg1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (myboard != null && colorDialog1.Color != myboard.backcolor2)
                {
                    myboard.backcolor1 = colorDialog1.Color;
                    myboard.ChangeBackColor();
                    myboard.ChangeRbBackColor();
                }
                else if (myboard == null)
                {
                    MessageBox.Show("Du måste starta spelet innan du ändrar färg");
                }
                else if (colorDialog1.Color == myboard.backcolor2)
                {
                    MessageBox.Show("Du valde samma färg på rutorna och då blir det svårt");
                }
            }
        }

        //ändrar färgen på tiles
        private void Färg2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                if (myboard != null && colorDialog2.Color != myboard.backcolor1)
                {
                    myboard.backcolor2 = colorDialog2.Color;
                    myboard.ChangeBackColor();
                    myboard.ChangeRbBackColor();

                }
                else if(myboard != null)
                {
                    MessageBox.Show("Du måste starta spelet innan du ändrar färg");
                }
                else if (colorDialog2.Color == myboard.backcolor1)
                {
                    MessageBox.Show("du valde samma färg på rutorna och då blir det svårt");
                }
            }
        }

        //startar spelet
        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //skapar en spelplan och initierar spelet
            if (myboard == null)
                myboard = new Board(panel1);
        }

        //startar om spelet
        private void StartaOmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //startar om spelet ifall det är startat och startar det annars
            if (myboard != null)
            {
                myboard.RemoveAllpieces();
                myboard.GeneratePieces();
            }
        }

        //resetar tilesfärgerna
        private void StandardFärgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myboard != null)
            {
                myboard.backcolor2 = Color.White;
                myboard.backcolor1 = Color.Black;
                myboard.ChangeBackColor();
                myboard.ChangeRbBackColor();
            }
        }
        #endregion
    }
    public class Move
    {
        //pjäsen som ska flyttas
        public Piece SPiece { get; set; }

        //brädet som pjäsen flyttas på
        public Board Myboard { get; set; }
        public Move(Board _Myboard, Piece _SPiece)
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
            SPiece.currentCell = new Cell(newHeight, newWidth);

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
                SPiece.rb.BackColor = Myboard.backcolor2;
            }
            else
            {
                SPiece.rb.BackColor = Myboard.backcolor1;
            }
            Myboard.IsSchack(SPiece);
            if (SPiece.name.StartsWith("SBonde") || SPiece.name.StartsWith("VBonde"))
                Myboard.Promote(SPiece);
        }       
    }
    public class Piece
    {
        //namnet på pjäsen
        public string name { get; set; }

        //cellen som pjäsen står på just nu
        public Cell currentCell { get; set; }
        
        //är pjäsen vit
        public bool IsWhite { get; set; }

        //radiobuttonen som pjäsen är kopplad till
        public RadioButton rb { get; set; }

        //specifierar ifall pjäsen har flyttats detta spelet
        public bool HasMoved { get; set; }

        public Piece(string _name, Cell _currentCell, Board Myboard)
        {
            name = _name;
            currentCell = _currentCell;
            currentCell.Occupied = true;
            Myboard.TheGrid[currentCell.Columnnumber, currentCell.Rownumber].Occupied = true;
            IsWhite = true;
            HasMoved = false;
        }
    }
    public class Board
    {
        #region Properties
        //storleken på spelplanen
        public int size { get; set; }
        //en 2d array som motsvarar spelplanen
        public Cell[,] TheGrid { get; set; }
        //den pjäsen som används just nu
        public Piece SelectedPiece { get; set; }
        // lista med alla spelpjäser i form av radiobuttons
        public RadioButton[] pieceGrid;
        //panelen i UIn
        public Panel panel1 { get; set; }
        //en 2d array med alla knappar som motsvarar spelrutorna
        Button[,] buttonGrid;
        //en prop för den första bakgrundsfärgen
        public Color backcolor1 { get; set; }
        //en prop för den andra bakgrundsfärgen
        public Color backcolor2 { get; set; }
        #endregion 
        public Board(Panel _panel1)
        {
            panel1 = _panel1;
            size = 8;
            pieceGrid = new RadioButton[32];
            buttonGrid = new Button[size, size];
            TheGrid = new Cell[size, size];
            backcolor1 = Color.Black;
            backcolor2 = Color.White;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    TheGrid[y, x] = new Cell(y, x);

                }
            }
            CreateButtons();
            GeneratePieces();
        }
        #region Metoder
        #region getcell-metoder
        //kollar och markerar  ifall cellen nedanför är tillåten
        private Cell Down(Cell newcell)
        {
            if (newcell.Rownumber + 1 < 8)
            {
                if (!TheGrid[newcell.Columnnumber, newcell.Rownumber + 1].Occupied)
                    TheGrid[newcell.Columnnumber, newcell.Rownumber + 1].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber, newcell.Rownumber + 1]);
                return new Cell(newcell.Columnnumber, newcell.Rownumber + 1);
            }
            return null;
        }

        //kollar och markerar  ifall cellen ovanför är tillåten
        private Cell Up(Cell newcell)
        {
            if (newcell.Rownumber - 1 > -1)
            {
                if (!TheGrid[newcell.Columnnumber, newcell.Rownumber - 1].Occupied)
                    TheGrid[newcell.Columnnumber, newcell.Rownumber - 1].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber, newcell.Rownumber - 1]);
                return new Cell(newcell.Columnnumber, newcell.Rownumber - 1);
            }
            return null;
        }

        //kollar och markerar ifall cellen till vänster är tillåten
        private Cell Left(Cell newcell)
        {
            if (newcell.Columnnumber - 1 > -1)
            {
                if (!TheGrid[newcell.Columnnumber - 1, newcell.Rownumber].Occupied)
                    TheGrid[newcell.Columnnumber - 1, newcell.Rownumber].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber - 1, newcell.Rownumber]);
                return new Cell(newcell.Columnnumber - 1, newcell.Rownumber);

            }
            return null;
        }

        //kollar och markerar  ifall cellen till höger är tillåten
        private Cell Right(Cell newcell)
        {
            if (newcell.Columnnumber + 1 < 8)
            {
                if (!TheGrid[newcell.Columnnumber + 1, newcell.Rownumber].Occupied)
                    TheGrid[newcell.Columnnumber + 1, newcell.Rownumber].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber + 1, newcell.Rownumber]);
                return new Cell(newcell.Columnnumber + 1, newcell.Rownumber);

            }
            return null;
        }

        //kollar och markerar ifall cellen uppe till vänster är tillåten
        private Cell Up_left(Cell newcell)
        {
            if (newcell.Columnnumber - 1 > -1 && newcell.Rownumber - 1 > -1)
            {
                if (!TheGrid[newcell.Columnnumber - 1, newcell.Rownumber - 1].Occupied)
                    TheGrid[newcell.Columnnumber - 1, newcell.Rownumber - 1].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber - 1, newcell.Rownumber - 1]);
                return new Cell(newcell.Columnnumber - 1, newcell.Rownumber - 1);

            }
            return null;
        }

        //kollar och markerar ifall cellen upep till höger är tillåten
        private Cell Up_right(Cell newcell)
        {
            if (newcell.Columnnumber + 1 < 8 && newcell.Rownumber - 1 > -1)
            {
                if (!TheGrid[newcell.Columnnumber + 1, newcell.Rownumber - 1].Occupied)
                    TheGrid[newcell.Columnnumber + 1, newcell.Rownumber - 1].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber + 1, newcell.Rownumber - 1]);
                return new Cell(newcell.Columnnumber + 1, newcell.Rownumber - 1);

            }
            return null;
        }

        //kollar och markerar ifall cellen nere till vänster är tillåten
        private Cell Down_left(Cell newcell)
        {
            if (newcell.Columnnumber + 1 < 8 && newcell.Rownumber + 1 < 8)
            {
                if (!TheGrid[newcell.Columnnumber + 1, newcell.Rownumber + 1].Occupied)
                    TheGrid[newcell.Columnnumber + 1, newcell.Rownumber + 1].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber + 1, newcell.Rownumber + 1]);
                return new Cell(newcell.Columnnumber + 1, newcell.Rownumber + 1);
            }
            return null;
        }

        //kollar och markerar ifall cellen nere till höger är tillåten
        private Cell Down_right(Cell newcell)
        {

            if (newcell.Columnnumber - 1 > -1 && newcell.Rownumber + 1 < 8)
            {
                if (!TheGrid[newcell.Columnnumber - 1, newcell.Rownumber + 1].Occupied)
                    TheGrid[newcell.Columnnumber - 1, newcell.Rownumber + 1].AllowedMove = true;
                else
                    Occupied(TheGrid[newcell.Columnnumber - 1, newcell.Rownumber + 1]);
                return new Cell(newcell.Columnnumber - 1, newcell.Rownumber + 1);
            }
            return null;
        }

        #region Hästmetoder
        //kollar och markerar ifall cellen ett steg vänster och två steg ner är tillåten       
        private void Left1Down2(Cell currentCell)
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
        private void Left1Up2(Cell currentCell)
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
        private void Left2Down1(Cell currentCell)
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
        private void Left2Up1(Cell currentCell)
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
        private void Right1Up2(Cell currentCell)
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
        private void Right1Down2(Cell currentCell)
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
        private void Right2Down1(Cell currentCell)
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
        private void Right2Up1(Cell currentCell)
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
        void Torn(Cell cell)
        {
            bool right = true;
            bool left = true;
            bool down = true;
            bool up = true;
            Cell newcell = SelectedPiece.currentCell;

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
        private void Lopare(Cell currentCell)
        {
            bool upleft = true;
            bool upright = true;
            bool downleft = true;
            bool downright = true;
            Cell newcell = SelectedPiece.currentCell;
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
        private void Kung(Cell currentCell)
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
        private void Hast(Cell currentCell)
        {
            Right2Up1(currentCell);
            Left2Up1(currentCell);
            Left2Down1(currentCell);
            Right1Down2(currentCell);
            Right2Down1(currentCell);
            Right1Up2(currentCell);
            Left1Up2(currentCell);
            Left1Down2(currentCell);
        }

        //hämtar de tillåtna dragen för vit bonde
        private void VBonde( Piece p)
        {
            Cell newcell = p.currentCell;
            Cell c;
            c = Up_left(newcell);
            if (c != null && !TheGrid[c.Columnnumber, c.Rownumber].Occupied)
                TheGrid[c.Columnnumber, c.Rownumber].AllowedMove = false;

            c = Up_right(newcell);
            if (c != null && !TheGrid[c.Columnnumber, c.Rownumber].Occupied)
                TheGrid[c.Columnnumber, c.Rownumber].AllowedMove = false;

            c = Up(newcell);
            if (c != null && TheGrid[c.Columnnumber, c.Rownumber].Occupied)
                TheGrid[c.Columnnumber, c.Rownumber].AllowedMove = false;
            if (c != null && !p.HasMoved && !TheGrid[c.Columnnumber, c.Rownumber].Occupied)
            {
                Up(c);
            }
            EnpassantV(p);

        }

        //hämtar de tillåtna dragen för svart bonde
        private void SBonde( Piece p)
        {
            Cell newcell = p.currentCell;
            Cell c;
            c = Down_left(newcell);
            if (c != null && !TheGrid[c.Columnnumber, c.Rownumber].Occupied)
                TheGrid[c.Columnnumber, c.Rownumber].AllowedMove = false;

            c = Down_right(newcell);
            if (c != null && !TheGrid[c.Columnnumber, c.Rownumber].Occupied)
                TheGrid[c.Columnnumber, c.Rownumber].AllowedMove = false;

            c = Down(newcell);
            if (c != null && TheGrid[c.Columnnumber, c.Rownumber].Occupied)
                TheGrid[c.Columnnumber, c.Rownumber].AllowedMove = false;
            if (c != null && !p.HasMoved && !TheGrid[c.Columnnumber, c.Rownumber].Occupied)
            {
                Down(c);
            }
            EnpassantS(p);
        }

        private void EnpassantS(Piece p)
        {
            //throw new NotImplementedException();
        }
        private void EnpassantV(Piece p)
        {
           // throw new NotImplementedException();
        }
        #endregion

        //kollar ifall rutan är tagen av motståndarens pjäs och markerar den isf
        private void Occupied(Cell newcell)
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
            Piece SBonde1 = new Piece("SBonde1", new Cell(0, 1), this);
            this.TheGrid[0, 1].OccupiedBy = SBonde1;
            SBonde1.IsWhite = false;

            Piece SBonde2 = new Piece("SBonde2", new Cell(1, 1), this);
            this.TheGrid[1, 1].OccupiedBy = SBonde2;
            SBonde2.IsWhite = false;

            Piece SBonde3 = new Piece("SBonde3", new Cell(2, 1), this);
            this.TheGrid[2, 1].OccupiedBy = SBonde3;
            SBonde3.IsWhite = false;

            Piece SBonde4 = new Piece("SBonde4", new Cell(3, 1), this);
            this.TheGrid[3, 1].OccupiedBy = SBonde4;
            SBonde4.IsWhite = false;

            Piece SBonde5 = new Piece("SBonde5", new Cell(4, 1), this);
            this.TheGrid[4, 1].OccupiedBy = SBonde5;
            SBonde5.IsWhite = false;

            Piece SBonde6 = new Piece("SBonde6", new Cell(5, 1), this);
            this.TheGrid[5, 1].OccupiedBy = SBonde6;
            SBonde6.IsWhite = false;

            Piece SBonde7 = new Piece("SBonde7", new Cell(6, 1), this);
            this.TheGrid[6, 1].OccupiedBy = SBonde7;
            SBonde7.IsWhite = false;

            Piece SBonde8 = new Piece("SBonde8", new Cell(7, 1), this);
            this.TheGrid[7, 1].OccupiedBy = SBonde8;
            SBonde8.IsWhite = false;

            Piece STorn1 = new Piece("STorn1", new Cell(0, 0), this);
            this.TheGrid[0, 0].OccupiedBy = STorn1;
            STorn1.IsWhite = false;

            Piece STorn2 = new Piece("STorn2", new Cell(7, 0), this);
            this.TheGrid[7, 0].OccupiedBy = STorn2;
            STorn2.IsWhite = false;

            Piece SHäst1 = new Piece("SHäst1", new Cell(1, 0), this);
            this.TheGrid[1, 0].OccupiedBy = SHäst1;
            SHäst1.IsWhite = false;

            Piece SHäst2 = new Piece("SHäst2", new Cell(6, 0), this);
            this.TheGrid[6, 0].OccupiedBy = SHäst2;
            SHäst2.IsWhite = false;

            Piece SLöpare1 = new Piece("SLöpare1", new Cell(2, 0), this);
            this.TheGrid[2, 0].OccupiedBy = SLöpare1;
            SLöpare1.IsWhite = false;

            Piece SLöpare2 = new Piece("SLöpare2", new Cell(5, 0), this);
            this.TheGrid[5, 0].OccupiedBy = SLöpare2;
            SLöpare2.IsWhite = false;

            Piece SKung = new Piece("SKung", new Cell(4, 0), this);
            this.TheGrid[4, 0].OccupiedBy = SKung;
            SKung.IsWhite = false;

            Piece SDrottning = new Piece("SDrottning", new Cell(3, 0), this);
            this.TheGrid[3, 0].OccupiedBy = SDrottning;
            SDrottning.IsWhite = false;
            #endregion
            #region vita pjäser
            Piece VBonde1 = new Piece("VBonde1", new Cell(0, 6), this);
            this.TheGrid[0, 6].OccupiedBy = VBonde1;

            Piece VBonde2 = new Piece("VBonde2", new Cell(1, 6), this);
            this.TheGrid[1, 6].OccupiedBy = VBonde2;

            Piece VBonde3 = new Piece("VBonde3", new Cell(2, 6), this);
            this.TheGrid[2, 6].OccupiedBy = VBonde3;

            Piece VBonde4 = new Piece("VBonde4", new Cell(3, 6), this);
            this.TheGrid[3, 6].OccupiedBy = VBonde4;

            Piece VBonde5 = new Piece("VBonde5", new Cell(4, 6), this);
            this.TheGrid[4, 6].OccupiedBy = VBonde5;

            Piece VBonde6 = new Piece("VBonde6", new Cell(5, 6), this);
            this.TheGrid[5, 6].OccupiedBy = VBonde6;

            Piece VBonde7 = new Piece("VBonde7", new Cell(6, 6), this);
            this.TheGrid[6, 6].OccupiedBy = VBonde7;

            Piece VBonde8 = new Piece("VBonde8", new Cell(7, 6), this);
            this.TheGrid[7, 6].OccupiedBy = VBonde8;

            Piece VTorn1 = new Piece("VTorn1", new Cell(0, 7), this);
            this.TheGrid[0, 7].OccupiedBy = VTorn1;

            Piece VTorn2 = new Piece("VTorn2", new Cell(7, 7), this);
            this.TheGrid[7, 7].OccupiedBy = VTorn2;

            Piece VHäst1 = new Piece("VHäst1", new Cell(1, 7), this);
            this.TheGrid[1, 7].OccupiedBy = VHäst1;

            Piece VHäst2 = new Piece("VHäst2", new Cell(6, 7), this);
            this.TheGrid[6, 7].OccupiedBy = VHäst2;

            Piece VLöpare1 = new Piece("VLöpare1", new Cell(2, 7), this);
            this.TheGrid[2, 7].OccupiedBy = VLöpare1;

            Piece VLöpare2 = new Piece("VLöpare2", new Cell(5, 7), this);
            this.TheGrid[5, 7].OccupiedBy = VLöpare2;

            Piece VKung = new Piece("VKung", new Cell(4, 7), this);
            this.TheGrid[4, 7].OccupiedBy = VKung;

            Piece VDrottning = new Piece("VDrottning", new Cell(3, 7), this);
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
                    buttonGrid[j, i].Tag = j + "," + i;
                    buttonGrid[j, i].FlatStyle = FlatStyle.Flat;
                    buttonGrid[j, i].FlatAppearance.BorderSize = 0;
                }
            }
            ChangeBackColor();

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
                    Cell cell = this.TheGrid[j, z];
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
                            #region vita pjäser
                            case "VTorn1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitRook;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor1;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "VTorn2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitRook;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor2;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "VHäst1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitKnight;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor2;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "VHäst2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitKnight;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor1;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "VLöpare1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitBishop1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor1;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "VLöpare2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitBishop1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor2;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "VKung":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitKung;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor1;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "VDrottning":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitQueen;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor2;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            #endregion
                            #region Svarta pjäser
                            //svart pjäser
                            case "STorn1":
                                pieceGrid[i].Location = new Point(pieceSize * 0, pieceSize * 0);
                                pieceGrid[i].Image = Properties.Resources.SvartRook1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor2;
                                cell.OccupiedBy.rb = pieceGrid[i];

                                i++;
                                break;
                            case "STorn2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartRook1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor1;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "SHäst1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKnight1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor1;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "SHäst2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKnight1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor2;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "SLöpare1":

                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartBishop2;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor2;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "SLöpare2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartBishop2;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor1;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "SKung":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKung1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor2;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            case "SDrottning":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartQueen1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = backcolor1;
                                cell.OccupiedBy.rb = pieceGrid[i];
                                i++;
                                break;
                            #endregion
                            default:
                                if (cell.OccupiedBy.name.StartsWith("VBonde"))
                                {
                                    pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                    pieceGrid[i].Image = Properties.Resources.VitPawn;
                                    pieceGrid[i].Tag = cell.OccupiedBy;
                                    cell.OccupiedBy.rb = pieceGrid[i];

                                    if (i % 2 == 0)
                                    {
                                        pieceGrid[i].BackColor = backcolor2;
                                    }
                                    else
                                    {
                                        pieceGrid[i].BackColor = backcolor1;
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
                                        pieceGrid[i].BackColor = backcolor1;
                                    }
                                    else
                                    {
                                        pieceGrid[i].BackColor = backcolor2;
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
            Piece s = (Piece)r.Tag;
            bool t = false;

            if (SelectedPiece != null && SelectedPiece.IsWhite != s.IsWhite)
            {

                t = CapturePiece(s, SelectedPiece);
            }
            if(t == false)
            {
                resetbtns();

                SelectedPiece = s;
                Markallowedmove( s);
                for (int x = 0; x < size; x++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Cell cell = TheGrid[j, x];

                        markbtns(cell);
                        
                    }
                }
            }

        }

        //markerar de tillåtna dragen
        public void Markallowedmove( Piece Selectedpiece)
        {
            Cell currentCell = SelectedPiece.currentCell;
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
                    Hast(currentCell);
                    break;
                case "SHäst1":
                case "SHäst2":
                    //hästen kan gå två steg fram och ett åt sidan
                    Hast(currentCell);
                    break;
                default:
                    if (Selectedpiece.name.StartsWith("VBonde"))
                    {
                        VBonde(Selectedpiece);

                    }
                    else if (Selectedpiece.name.StartsWith("SBonde"))
                    {
                        SBonde(Selectedpiece);
                    }
                    else
                        MessageBox.Show("nu har nått gått väldigt fel!");
                    break;
            }
        }

        //markerar de btns som man får flytta till
        private void markbtns(Cell cell)
        {
            
            if (cell.AllowedMove)
            {
                if (cell.Occupied)
                {
                    cell.OccupiedBy.rb.FlatAppearance.BorderSize = 4;
                    cell.OccupiedBy.rb.FlatAppearance.BorderColor = Color.Blue;

                }
                buttonGrid[cell.Columnnumber, cell.Rownumber].FlatAppearance.BorderSize = 4;
                buttonGrid[cell.Columnnumber, cell.Rownumber].FlatAppearance.BorderColor = Color.Blue;
            }
        }

        //tar motståndarens pjäs ifall det är tillåtet
        private bool CapturePiece(Piece oldPiece, Piece newPiece)
        {
            if (oldPiece.rb.FlatAppearance.BorderSize == 4)
            {
                panel1.Controls.Remove(oldPiece.rb);
                TheGrid[oldPiece.currentCell.Columnnumber, oldPiece.currentCell.Rownumber].Occupied = false;
                Move move = new Move(this, newPiece);
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
            MessageBox.Show(btag[0] + " " + btag[1] + " occ: " + TheGrid[int.Parse(btag[0]), int.Parse(btag[1])].Occupied);
            if (SelectedPiece != null)
            {
                Piece s = SelectedPiece;
                if (b.FlatAppearance.BorderSize == 4)
                {
                    Move m = new Move(this, s);
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

        //uppdaterar bakgrundsfärgen på btns
        public void ChangeBackColor()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (((j + i) % 2) == 0)
                    {
                        buttonGrid[j, i].BackColor = backcolor2;
                    }
                    else
                        buttonGrid[j, i].BackColor = backcolor1;
                }
            }
            
        }

        //uppdaterar bakgrundsfärgen på rbs
        public void ChangeRbBackColor()
        {
            for (int i = 0; i < 32; i++)
            {
                if (((pieceGrid[i].Location.X / 65) + (pieceGrid[i].Location.Y / 65)) % 2 == 0)
                {
                    pieceGrid[i].BackColor = backcolor2;
                }
                else
                {
                    pieceGrid[i].BackColor = backcolor1;
                }
            }
        }

        //kollar ifall det är schack
        internal void IsSchack(Piece sPiece)
        {
            SelectedPiece = sPiece;
            Markallowedmove(sPiece);
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

        internal void Promote(Piece sPiece)
        {
            // Create a new instance of the Form2 class
            Form Promoteform = new Form();

            // Show the settings form
            Promoteform.Show();
            if (sPiece.IsWhite)
            {
                if(sPiece.currentCell.Rownumber == 0)
                {

                }

            }
            if(!sPiece.IsWhite)
            {
                if (sPiece.currentCell.Rownumber == 7)
                {

                }
            }
        }
        #endregion
    }
    public class Cell
    {
        //radnumret på cell
        public int Rownumber { get; set; }

        //kolumnnumret på cell
        public int Columnnumber { get; set; }

        //är cellen upptagen
        public bool Occupied { get; set; }

        //den pjäsen som ev okuperar cellen
        public Piece OccupiedBy { get; set; }

        //får en pjäs gå hit
        public bool AllowedMove { get; set; }

        public Cell(int y, int x)
        {
            Columnnumber = y;
            Rownumber = x;
        }
    }
}