
namespace Chess_Forms
{
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