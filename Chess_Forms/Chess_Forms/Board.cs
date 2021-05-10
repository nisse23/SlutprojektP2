using System;
using System.Drawing;
using System.Windows.Forms;


namespace Chess_Forms
{
    public class Board
    {
        #region Properties
        //storleken på spelplanen
        public int Size { get; set; }
        //en 2d array som motsvarar spelplanen
        public Cell[,] TheGrid { get; set; }
        //den pjäsen som används just nu
        public Piece SelectedPiece { get; set; }
        // lista med alla spelpjäser i form av radiobuttons
        public RadioButton[] pieceGrid;
        //panelen i UIn
        public Panel Panel1 { get; set; }
        //en 2d array med alla knappar som motsvarar spelrutorna
        public Button[,] buttonGrid;
        //en prop för den första bakgrundsfärgen
        public Color Backcolor1 { get; set; }
        //en prop för den andra bakgrundsfärgen
        public Color Backcolor2 { get; set; }
        #endregion 
        public Board(Panel _panel1)
        {
            Panel1 = _panel1;
            Size = 8;
            pieceGrid = new RadioButton[32];
            buttonGrid = new Button[Size, Size];
            TheGrid = new Cell[Size, Size];
            Backcolor1 = Color.Black;
            Backcolor2 = Color.White;

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
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
            if (currentCell.Columnnumber - 1 >= 0 && currentCell.Rownumber + 2 < Size)
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
            if (currentCell.Columnnumber - 2 >= 0 && currentCell.Rownumber + 1 < Size)
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
            if (currentCell.Columnnumber + 1 < Size && currentCell.Rownumber + 2 < Size)
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
            if (currentCell.Columnnumber + 1 < Size && currentCell.Rownumber - 2 >= 0)
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
            if (currentCell.Columnnumber + 2 < Size && currentCell.Rownumber - 1 >= 0)
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
            if (currentCell.Columnnumber + 2 < Size && currentCell.Rownumber + 1 < Size)
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
            Cell newcell = SelectedPiece.CurrentCell;

            while (right)
            {
                newcell = Right(newcell);
                if ((newcell == null || !TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove) || TheGrid[newcell.Columnnumber, newcell.Rownumber].Occupied)
                    break;                   
            }
            newcell = SelectedPiece.CurrentCell;
            while (left)
            {
                newcell = Left(newcell);
                if ((newcell == null || !TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove) || TheGrid[newcell.Columnnumber, newcell.Rownumber].Occupied)
                    break;                
            }
            newcell = SelectedPiece.CurrentCell;

            while (down)
            {
                newcell = Down(newcell);
                if ((newcell == null || !TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove) || TheGrid[newcell.Columnnumber, newcell.Rownumber].Occupied)
                {
                    break;
                }              
            }
            newcell = SelectedPiece.CurrentCell;
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
            Cell newcell = SelectedPiece.CurrentCell;
            while (downright)
            {
                newcell = Down_right(newcell);
                if ((newcell == null || !TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove) || TheGrid[newcell.Columnnumber, newcell.Rownumber].Occupied)
                    break;
            }
            newcell = SelectedPiece.CurrentCell;
            while (downleft)
            {
                newcell = Down_left(newcell);
                if ((newcell == null || !TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove) || TheGrid[newcell.Columnnumber, newcell.Rownumber].Occupied)
                    break;
            }
            newcell = SelectedPiece.CurrentCell;
            while (upleft)
            {
                newcell = Up_left(newcell);
                if ((newcell == null || !TheGrid[newcell.Columnnumber, newcell.Rownumber].AllowedMove) || TheGrid[newcell.Columnnumber, newcell.Rownumber].Occupied)
                    break;
            }
            newcell = SelectedPiece.CurrentCell;
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
            Cell newcell = p.CurrentCell;
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
        }

        //hämtar de tillåtna dragen för svart bonde
        private void SBonde( Piece p)
        {
            Cell newcell = p.CurrentCell;
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
        public void PlacePiecesInStart()
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
            int buttonSize = Panel1.Width / Size;

            for (int i = 0; i < Size; i++)
            {
                //label[i] = new Label();
                //label[i].Text = "va fan e det som händer";
                //label[i].Location = label1.Location;
                //label[i].BringToFront();


                for (int j = 0; j < Size; j++)
                {
                    buttonGrid[j, i] = new Button();

                    buttonGrid[j, i].Width = buttonSize;
                    buttonGrid[j, i].Height = buttonSize;
                    buttonGrid[j, i].Click += ButtonClick;

                    Panel1.Controls.Add(buttonGrid[j, i]);
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
            PlacePiecesInStart();

            //räknar ut storleken på btns. =65
            int pieceSize = Panel1.Width / Size;
            //variabler
            int i = 0;

            //nästade loopar för att gå igenom arrayn thegrid.
            for (int z = 0; z < this.Size; z++)
            {
                for (int j = 0; j < this.Size; j++)
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
                        Panel1.Controls.Add(pieceGrid[i]);
                        pieceGrid[i].BringToFront();
                        //lägger till ett click event
                        pieceGrid[i].Click += MarkallowedTiles;
                        string s = cell.OccupiedBy.Name;
                        switch (cell.OccupiedBy.Name)
                        {
                            //vita pjäser
                            #region vita pjäser
                            case "VTorn1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitRook;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor1;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            case "VTorn2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitRook;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor2;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            case "VHäst1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitKnight;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor2;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            case "VHäst2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitKnight;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor1;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            case "VLöpare1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitBishop1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor1;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            case "VLöpare2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitBishop1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor2;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            case "VKung":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitKung;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor1;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            case "VDrottning":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.VitQueen;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor2;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            #endregion
                            #region Svarta pjäser
                            //svart pjäser
                            case "STorn1":
                                pieceGrid[i].Location = new Point(pieceSize * 0, pieceSize * 0);
                                pieceGrid[i].Image = Properties.Resources.SvartRook1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor2;
                                cell.OccupiedBy.Rb = pieceGrid[i];

                                i++;
                                break;
                            case "STorn2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartRook1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor1;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            case "SHäst1":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKnight1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor1;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            case "SHäst2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKnight1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor2;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            case "SLöpare1":

                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartBishop2;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor2;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            case "SLöpare2":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartBishop2;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor1;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            case "SKung":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartKung1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor2;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            case "SDrottning":
                                pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                pieceGrid[i].Image = Properties.Resources.SvartQueen1;
                                pieceGrid[i].Tag = cell.OccupiedBy;
                                pieceGrid[i].BackColor = Backcolor1;
                                cell.OccupiedBy.Rb = pieceGrid[i];
                                i++;
                                break;
                            #endregion
                            default:
                                if (cell.OccupiedBy.Name.StartsWith("VBonde"))
                                {
                                    pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                    pieceGrid[i].Image = Properties.Resources.VitPawn;
                                    pieceGrid[i].Tag = cell.OccupiedBy;
                                    cell.OccupiedBy.Rb = pieceGrid[i];
                                    cell.OccupiedBy.Rb.BackColor = SetBackColor(pieceGrid[i].Location);
                                    i++;
                                }
                                else if (cell.OccupiedBy.Name.StartsWith("SBonde"))
                                {
                                    pieceGrid[i].Location = new Point(pieceSize * cell.Columnnumber, pieceSize * cell.Rownumber);
                                    pieceGrid[i].Tag = cell.OccupiedBy;
                                    pieceGrid[i].Image = Properties.Resources.SvartPawn1;
                                    cell.OccupiedBy.Rb = pieceGrid[i];
                                    pieceGrid[i].BackColor = SetBackColor(pieceGrid[i].Location);
                                  
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
        //returnerar bakgrundsfärgen för en location
        public Color SetBackColor(Point location)
        {
            //bestämmer bakgrunden
            if (((location.X / 65) + (location.Y / 65)) % 2 == 0)
            {
                return Backcolor2;
            }
            else
            {
                return Backcolor1;
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
                Resetbtns();

                SelectedPiece = s;
                Markallowedmove( s);
                for (int x = 0; x < Size; x++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        Cell cell = TheGrid[j, x];

                        Markbtns(cell);
                        
                    }
                }
            }

        }

        //markerar de tillåtna dragen
        public void Markallowedmove( Piece Selectedpiece)
        {
            Cell currentCell = SelectedPiece.CurrentCell;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    TheGrid[j, i].AllowedMove = false;
                }
            }
            switch (Selectedpiece.Name)
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
                    if (Selectedpiece.Name.StartsWith("VBonde"))
                    {
                        VBonde(Selectedpiece);

                    }
                    else if (Selectedpiece.Name.StartsWith("SBonde"))
                    {
                        SBonde(Selectedpiece);
                    }
                    else
                        MessageBox.Show("nu har nått gått väldigt fel!");
                    break;
            }
        }

        //markerar de btns som man får flytta till
        private void Markbtns(Cell cell)
        {
            
            if (cell.AllowedMove)
            {
                if (cell.Occupied)
                {
                    cell.OccupiedBy.Rb.FlatAppearance.BorderSize = 4;
                    cell.OccupiedBy.Rb.FlatAppearance.BorderColor = Color.Blue;

                }
                buttonGrid[cell.Columnnumber, cell.Rownumber].FlatAppearance.BorderSize = 4;
                buttonGrid[cell.Columnnumber, cell.Rownumber].FlatAppearance.BorderColor = Color.Blue;
            }
        }

        //tar motståndarens pjäs ifall det är tillåtet
        private bool CapturePiece(Piece oldPiece, Piece newPiece)
        {
            if (oldPiece.Rb.FlatAppearance.BorderSize == 4)
            {
                Panel1.Controls.Remove(oldPiece.Rb);
                TheGrid[oldPiece.CurrentCell.Columnnumber, oldPiece.CurrentCell.Rownumber].Occupied = false;
                Move move = new Move(this, newPiece);
                move.MovePiece(buttonGrid[oldPiece.CurrentCell.Columnnumber, oldPiece.CurrentCell.Rownumber]);
                Resetbtns();
                if(oldPiece.Name.EndsWith("Kung"))
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
                Panel1.Controls.Remove(pieceGrid[i]);
            }
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    TheGrid[j, i].Occupied = false;
                    TheGrid[j, i].OccupiedBy = null;
                }
            }
            Resetbtns();
        }

        //när man klickar på en button triggas denna
        private void ButtonClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string[] btag = GetBtntag(b);
            if (SelectedPiece != null)
            {
                Piece s = SelectedPiece;
                if (b.FlatAppearance.BorderSize == 4)
                {
                    Move m = new Move(this, s);
                    m.MovePiece(b);
                    Resetbtns();
                }
                    
                
            }
        }

        //tar bort den tillåtna markeringen på knapparna och radiobtns
        private void Resetbtns()
        {
            //tar bort indikatorn och ändrar statusen på cellen
            for (int z = 0; z < Size; z++)
            {
                for (int j = 0; j < Size; j++)
                {
                    buttonGrid[j, z].FlatAppearance.BorderSize = 0;
                    TheGrid[j, z].AllowedMove = false;
                    if (TheGrid[j, z].Occupied)
                        TheGrid[j, z].OccupiedBy.Rb.FlatAppearance.BorderSize = 0;
                }
            }
        }

        //hämtar tagen från en knapp
        public string[] GetBtntag(Button b)
        {
            string str = (string)b.Tag;

            return str.Split(',');
        }

        //uppdaterar bakgrundsfärgen på btns
        public void ChangeBackColor()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (((j + i) % 2) == 0)
                    {
                        buttonGrid[j, i].BackColor = Backcolor2;
                    }
                    else
                        buttonGrid[j, i].BackColor = Backcolor1;
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
                    pieceGrid[i].BackColor = Backcolor2;
                }
                else
                {
                    pieceGrid[i].BackColor = Backcolor1;
                }
            }
        }

        //kollar ifall det är schack
        internal void IsSchack(Piece sPiece)
        {
            SelectedPiece = sPiece;
            Markallowedmove(sPiece);
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (TheGrid[j, i].Occupied == true && TheGrid[j, i].OccupiedBy.Name.EndsWith("Kung") && TheGrid[j, i].AllowedMove)
                    {
                        MessageBox.Show("schack");
                    }
                }
            }
            SelectedPiece = null;
            Resetbtns();

        }

        #endregion
    }
}