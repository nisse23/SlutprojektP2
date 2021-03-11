using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace schack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class chessPiece
        {
            public string position { get; set; }
            public string coordinates { get; set; }
            public chessPiece(string _position, string _coordinates)
            {
                position = _position;
                coordinates = _coordinates;
            }
           
        }

        public class Rook : chessPiece
        {
            public Rook(string position, string coordinates) : base (position, coordinates)
            {
                
            }
            public void PossibleMoves()
            {
                string[] lista = position.Split(',');
                int width = int.Parse(lista[0]);


                while (width < 544 && width > 194)
                {
                    if (width == 544)
                    {
                        width -= 50;
                        string letter = GetCoordinateLetter(width);
                         
                    }
                }
            }
        }
        private void radioButtonWRook2_CheckedChanged(object sender, EventArgs e)
        {
            //gets the square
            Control parent = radioButtonWRook2.Parent;
            //converts the position
            //string position = CheckPosition(parent);
            string position = parent.Location.X + "," + parent.Location.Y;
            string coordinates = CheckPosition(parent);
            Rook Wrook1 = new Rook(position, coordinates);
            Wrook1.PossibleMoves();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //gets the square
            Control parent = radioButtonWRook1.Parent;
            //converts the position
            string positition = CheckPosition(parent);

        }
        string CheckPosition(Control piece)
        {
            //variables
            int height = piece.Location.Y;
            int width = piece.Location.X;
            string letter;
            string number;
            string coordinate;
            //gets the equivelant letter of the position
            letter = GetCoordinateLetter(width);
           
            //gets the equivelant number of the position
            number = GetCoordinateNumber(height);
            

            MessageBox.Show(letter + number);

            //returns the position
            return coordinate = letter +" " + number;

            
        }
        public static string GetCoordinateLetter(int width)
        {
            //gets the equivelant letter
            switch (width)
            {
                case 194:
                    return "a";

                case 244:
                    return "b";

                case 294:
                    return "c";

                case 344:
                    return "d";

                case 394:
                    return "e";

                case 444:
                    return "f";

                case 494:
                    return "g";

                case 544:
                    return "h";

                default:
                    MessageBox.Show("FEL");
                    return "";
            }
        }

        public static string GetCoordinateNumber(int height)
        {
            //gets the equivelant number
            switch (height)
            {
                case 420:
                    return "1";
                   
                case 370:
                    return "2";
                    
                case 320:
                    return "3";
                    
                case 270:
                    return "4";
                    
                case 220:
                    return "5";
                    
                case 170:
                    return "6";
                    
                case 120:
                    return "7";
                    
                case 70:
                    return "8";
                    
                default:
                    MessageBox.Show("FEL");
                    return "";
            }
        }


        private void button22_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

       
        private void radioButton1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel1_Click(object sender, EventArgs e)
        {

        }

        private void paneld3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void paneld1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
