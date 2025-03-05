using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biglietti_concerto
{
    public partial class Form1 : Form
    {
        List<(string Titolo, string Artista)> Spettacoli = new List<(string, string)>
    {
        ("Intelligenza Naturale", "Andrea Pezzi"),
        ("Marcus Miller", "Marcus Miller"),
        ("LRDL Summer Tour 2025", "La Rappresentante Di Lista"),
        ("PalaJova", "Jovanotti"),
        ("Sophie and The Giants", "Sophie\nThe Giants"),
        ("Damme 'na mano", "Tony Effe"),
        ("Games in Concert", ""),
        ("Tim Burton's Labrynth", "Tim Burton"),
        ("Prova A Prendermi", "Alessandro Longebardi\nSimone Montedoro"),
        ("Vita Bassa", "Giorgia Fumo"),
        ("Estate 2025", "Morrissey"),
        ("Jimmy Sax and Symphonic Dance Orchestra", "Jimmy Sax"),
        ("2025 World Tour - Milano", "Black Pink"),
        ("ACDC - Powerup Tour", "ACDC"),
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
            string titolo = "", artista = "";
            PictureBox pb = (PictureBox)sender;

            foreach (var spettacolo in Spettacoli)
            {
                if (spettacolo.Titolo == pb.Tag.ToString())
                {
                    titolo = spettacolo.Titolo;
                    artista = spettacolo.Artista;
                    break;
                }
            }

            // Nascondi il pannello principale e mostra quello dei posti
            Pannello_Principale.Visible = false;
            Pannello_Posti.Location = new Point(0, 55);
            Pannello_Posti.Visible = true;

            // Imposta i valori sui controlli della UI
            Artista_Lbl.Text = artista;
            Img_Info.Image = pb.Image;
            Img_Info.SizeMode = PictureBoxSizeMode.Zoom;
            TitoloSpettacolo_Lbl.Text = titolo;
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
