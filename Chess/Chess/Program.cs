using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        static board board = new board();

        static void Main(string[] args)
        {
            cell c = new cell(4, 4);
            //board.Markallowedmove(c, "Bonde");
            //board.placePiecesInStart();
            //PrintBoard(board);
            //Console.WriteLine("x: ");
            //int x = int.Parse(Console.ReadLine());
            //Console.WriteLine("y: ");
            //int y = int.Parse(Console.ReadLine());
            //if ((x + 2 < 8 && x + 2 >= 0) && (y + 1 < 8 && y + 1 >= 0))
            //{
            //    Console.WriteLine("tjena");
            //}

                Console.ReadLine();

        }

        private static void PrintBoard(board board)
        {
            Console.WriteLine("  0  1  2  3  4  5  6  7");
            //Console.WriteLine("  1  2  3  4  5  6  7  8");
            //Console.WriteLine("  a  b  c  d  e  f  g  h");

            //skriver ut spelplanen
            for (int i = 0; i < board.size; i++)
            {
                Console.Write(i);
                //Console.Write(i + 1);

                for (int j = 0; j < board.size; j++)
                {
                    //Console.Write(j);
                    cell c = board.TheGrid[i, j];
                    if(c.OccupiedBy == "Bonde")
                    {
                        Console.Write(" B ");
                    }
                    else if (c.OccupiedBy == "Drottning")
                    {
                        Console.Write(" D ");

                    }
                    else if (c.OccupiedBy == "Kung")
                    {
                        Console.Write(" K ");

                    }
                    else if (c.OccupiedBy == "Häst")
                    {
                        Console.Write(" H ");

                    }
                    else if (c.OccupiedBy == "Löpare")
                    {
                        Console.Write(" L ");

                    }
                    else if (c.OccupiedBy == "Torn")
                    {
                        Console.Write(" T ");

                    }
                    else if(c.Occupied)
                    {
                        Console.Write(" X ");

                    }
                    else if (c.AllowedMove)
                    {
                        Console.Write(" + ");
                    }

                    else
                    {
                        Console.Write(" . ");
                    }
                }
                Console.WriteLine();
            }
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
                    TheGrid[i,j] = new cell(i,j);

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
            TheGrid[7,0].Occupied = true;
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
        public void Markallowedmove (cell currentCell, string piece)
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
                        if(currentCell.Columnnumber + i < size)
                        TheGrid[currentCell.Columnnumber + i, currentCell.Rownumber ].AllowedMove = true;
                        if(currentCell.Rownumber + i < size)
                        TheGrid[currentCell.Columnnumber , currentCell.Rownumber + i].AllowedMove = true;
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
                    TheGrid[currentCell.Columnnumber , currentCell.Rownumber + 1].AllowedMove = true;
                    TheGrid[currentCell.Columnnumber , currentCell.Rownumber - 1].AllowedMove = true;

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
                    Console.WriteLine(currentCell.Columnnumber - 2+" " + currentCell.Rownumber);
                    TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber].AllowedMove = true;
                    Console.WriteLine(TheGrid[currentCell.Columnnumber - 2, currentCell.Rownumber].AllowedMove);
                    //den kan oxå ta snett fram om det står någon där
                    if(TheGrid[currentCell.Rownumber + 1,currentCell.Columnnumber +1].Occupied)
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
