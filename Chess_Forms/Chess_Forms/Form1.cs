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

        }
        Board myboard;
        #region Events
        //ändrar färgen på tiles
        private void Färg1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (myboard != null && colorDialog1.Color != myboard.Backcolor2)
                {
                    myboard.Backcolor1 = colorDialog1.Color;
                    myboard.ChangeBackColor();
                    myboard.ChangeRbBackColor();
                }
                else if (myboard == null)
                {
                    MessageBox.Show("Du måste starta spelet innan du ändrar färg");
                }
                else if (colorDialog1.Color == myboard.Backcolor2)
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
                if (myboard != null && colorDialog2.Color != myboard.Backcolor1)
                {
                    myboard.Backcolor2 = colorDialog2.Color;
                    myboard.ChangeBackColor();
                    myboard.ChangeRbBackColor();

                }
                else if(myboard != null)
                {
                    MessageBox.Show("Du måste starta spelet innan du ändrar färg");
                }
                else if (colorDialog2.Color == myboard.Backcolor1)
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
                myboard.Backcolor2 = Color.White;
                myboard.Backcolor1 = Color.Black;
                myboard.ChangeBackColor();
                myboard.ChangeRbBackColor();
            }
        }
        #endregion
    }
}