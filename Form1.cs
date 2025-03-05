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
        List<string> Titoli = new List<string>
        {
            "Intelligenza Naturale", "Art of Play - Mostra Immersiva"
        };
        List<string> Artisti = new List<string>
        {
            "Andrea Pezzi"
        };

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1167, 528);
            Pannello_Principale.Size = new Size(1151, 434);
            Pannello_Principale.Location = new Point(0, 54);
            Pannello_Posti.Size = new Size(1151, 434);
        }


        private void Spettacolo_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            Pannello_Principale.Visible = false;
            Pannello_Posti.Location = new Point(0, 55);
            Pannello_Posti.Visible = true;

            Artista_Lbl.Text = pb.Tag?.ToString() ?? "Sconosciuto";
            Img_Info.Image = pb.Image;
            Img_Info.SizeMode = PictureBoxSizeMode.Zoom;
            TitoloSpettacolo_Lbl.Text = pb.Name.Substring(4);
        }

        private void PostoSelezionato_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.BackColor == Color.Yellow)
            {

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
