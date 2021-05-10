using System.Windows.Forms;

namespace Chess_Forms
{
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
            string[] sa = Myboard.GetBtntag(b);

            //ändra i array
            int newWidth = int.Parse(sa[1]);
            int newHeight = int.Parse(sa[0]);

            //frigör föregående cell
            Myboard.TheGrid[SPiece.CurrentCell.Columnnumber, SPiece.CurrentCell.Rownumber].Occupied = false;
            Myboard.TheGrid[SPiece.CurrentCell.Columnnumber, SPiece.CurrentCell.Rownumber].OccupiedBy = null;

            //ändrar cell i piecen
            SPiece.CurrentCell = new Cell(newHeight, newWidth);

            //okuperar den nya cellen
            Myboard.TheGrid[newHeight, newWidth].Occupied = true;
            Myboard.TheGrid[newHeight, newWidth].OccupiedBy = SPiece;


            //change location of radiobutton
            SPiece.Rb.Location = b.Location;
            SPiece.Rb.Checked = false;
            SPiece.HasMoved = true;
            Myboard.SelectedPiece = null;

            SPiece.Rb.BackColor = Myboard.SetBackColor(SPiece.Rb.Location);
            
            Myboard.IsSchack(SPiece);

        }       
    }
}