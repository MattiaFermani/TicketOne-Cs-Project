using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biglietti_concerto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1167, 528);
        }


        private void Spettacolo_Click(object sender, EventArgs e)
        {
            Pannello_Principale.Visible = false;
            Pannello_Posti.Location = new Point(0, 55);
            Pannello_Posti.Visible = true;
        }

        private void PostoSelezionato_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if(btn.BackColor == Color.Yellow)
            {
                btn.BackColor = Color.White;
            }
            else
            {
                btn.BackColor = Color.Yellow;
                string test = $"{btn.Parent.Name}\n\nPosto: {btn.Text}";
                MessageBox.Show(test);
            }
        }

        private void TickeTlon_Click(object sender, EventArgs e)
        {
            Pannello_Principale.Visible = true;
            Pannello_Principale.Location = new Point(0, 54);
            Pannello_Posti.Visible = false;
        }
    }
}
