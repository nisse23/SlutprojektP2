using System.Windows.Forms;


//TO DO
// promota bönder
// enpassent
// rockad
// fixa UI
namespace Chess_Forms
{
    public class Piece
    {
        //namnet på pjäsen
        public string Name { get; set; }

        //cellen som pjäsen står på just nu
        public Cell CurrentCell { get; set; }
        
        //är pjäsen vit
        public bool IsWhite { get; set; }

        //radiobuttonen som pjäsen är kopplad till
        public RadioButton Rb { get; set; }

        //specifierar ifall pjäsen har flyttats detta spelet
        public bool HasMoved { get; set; }
        //construktor som tar in info om namn, cell och vilket bräde för pjäsen
        public Piece(string _name, Cell _currentCell, Board Myboard)
        {
            Name = _name;
            CurrentCell = _currentCell;
            CurrentCell.Occupied = true;
            Myboard.TheGrid[CurrentCell.Columnnumber, CurrentCell.Rownumber].Occupied = true;
            IsWhite = true;
            HasMoved = false;
        }
    }
}