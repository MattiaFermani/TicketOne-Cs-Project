using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
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

        static Random rnd;
        static List<DateTime> dateUsate = new List<DateTime>();


        //NOT WORKING PROPERLY
        /*static List<string> GeneraDate(int numeroDate, DateTime dataIniziale)
        {
            List<string> dateGenerates = new List<string>();

            for (int i = 0; i < numeroDate; i++)
            {
                rnd = new Random();
                DateTime nuovadata = dataIniziale.AddDays(rnd.Next(1, 30));
                while (dateUsate.Contains(nuovadata))
                {
                    nuovadata = dataIniziale.AddDays(rnd.Next(1, 30));
                }
                dateUsate.Add(nuovadata);
                dateGenerates.Add(nuovadata.ToString("dd/MM/yyyy"));
            }



            return dateGenerates;
        }*/

        static Dictionary<string, (List<string> luoghi, List<string> date)> Eventi = new Dictionary<string, (List<string>, List<string>)>
    {
        { "Intelligenza Naturale",
            (new List<string> { "Firenze - Teatro Verdi", "Roma - Stadio Olimpico", "Roma - Auditorium Parco della Musica", "Torino - Teatro Regio" },
                new List<string> {    "25/04/2025", "05/06/2025", "09/11/2025", "21/03/2026"})
             //GeneraDate(4, new DateTime(2025, 3, 25)))
        },

        { "Marcus Miller",
            (new List<string> { "Verona - Arena di Verona", "Milano - Teatro La Scala", "Milano - Blue Note", "Torino - Pala Alpitour" },
                new List<string> {    "19/05/2025", "25/10/2025", "09/12/2025", "30/02/2026"})
             //GeneraDate(rnd.Next(3, 6), new DateTime(2025, 3, 25)))
        },

        { "LRDL Summer Tour 2025",
            (new List<string> { "Roma - Palazzetto dello Sport", "Napoli - Teatro Centrale", "Firenze - Stadio Artemio Franchi", "Roma - Ippodromo delle Capannelle" },
                new List<string> {    "25/07/2025", "10/08/2025", "31/08/2025", "11/09/2025"})
             //GeneraDate(rnd.Next(2, 5), new DateTime(2025, 3, 25)))
        },

        { "PalaJova",
            (new List<string> { "Torino - Pala Alpitour", "Milano - Mediolanum Forum", "Bologna - Unipol Arena", "Roma - Stadio Olimpico" },
                new List<string> {    "06/10/2025", "12/12/2025", "15/01/2025", "22/02/2026"})
             //GeneraDate(rnd.Next(3, 7), new DateTime(2025, 3, 25)))
        },

        { "Sophie and The Giants",
            (new List<string> { "Milano - Alcatraz", "Roma - Atlantico", "Firenze - Teatro Verdi", "Bologna - Estragon Club" },
                new List<string> {    "06/04/2025", "12/06/2025", "19/07/2025", "06/11/2025"})
             //GeneraDate(rnd.Next(2, 6), new DateTime(2025, 3, 25)))
        },

        { "Damme na mano Roma e Milano",
            (new List<string> { "Milano - Ippodromo Snai", "Roma - Circo Massimo", "Napoli - Palapartenope", "Bologna - Unipol Arena" },
                new List<string> {    "22/09/2025", "04/10/2025", "07/10/2025", "29/10/2025"})
             //GeneraDate(rnd.Next(3, 7), new DateTime(2025, 3, 25)))
        },

        { "Games in Concert",
            (new List<string> { "Roma - Auditorium della Musica", "Torino - Teatro Regio", "Firenze - Palazzo dei Congressi", "Palermo - Teatro Massimo" },
                new List<string> {    "10/09/2025", "11/09/2025", "12/09/2025", "19/09/2025"})
             //GeneraDate(rnd.Next(2, 5), new DateTime(2025, 3, 25)))
        },

        { "FASK tour estivo 2025",
            (new List<string> { "Roma - Villa Ada", "Bologna - Estragon Club", "Milano - Fabrique", "Milano - Alcatraz" },
                new List<string> {    "18/08/2025", "24/08/2025", "25/08/2025", "07/09/2025"})
             //GeneraDate(rnd.Next(3, 6), new DateTime(2025, 3, 25)))
        },

        { "Prova A Prendermi",
            (new List<string> { "Roma - Teatro Brancaccio", "Milano - Teatro Nazionale", "Genova - Politeama Genovese", "Bologna - Teatro Comunale" },
                new List<string> {    "05/08/2025", "08/08/2025", "09/08/2025", "14/08/2025"})
             //GeneraDate(rnd.Next(2, 4), new DateTime(2025, 3, 25)))
        },

        { "Vita Bassa",
            (new List<string> { "Milano - Teatro Manzoni", "Milano - Teatro Elfo Puccini", "Milano - Auditorium San Fedele", "Torino - Teatro Gobetti" },
                new List<string> {    "16/07/2025", "21/07/2025", "03/08/2025", "03/08/2025"})
             //GeneraDate(rnd.Next(3, 5), new DateTime(2025, 3, 25)))
        },

        { "Estate 2025",
            (new List<string> { "Milano - Ippodromo Snai San Siro", "Roma - Circo Massimo", "Firenze - Stadio Artemio Franchi", "Bari - Stadio San Nicola" },
                new List<string> {    "06/07/2025", "06/07/2025", "07/07/2025", "08/07/2025"})
             //GeneraDate(rnd.Next(4, 8), new DateTime(2025, 3, 25)))
        },

        { "Jimmy Sax and Symphonic Dance Orchestra",
            (new List<string> { "Milano - Teatro degli Arcimboldi", "Roma - Auditorium Parco della Musica", "Firenze - Palazzo della Musica", "Verona - Arena di Verona" },
                new List<string> {    "08/06/2025", "21/06/2025", "27/06/2025", "28/06/2025"})
             //GeneraDate(rnd.Next(3, 6), new DateTime(2025, 3, 25)))
        },

        { "2025 World Tour - Milano",
            (new List<string> { "Milano - Mediolanum Forum", "Bologna - Unipol Arena", "Torino - Pala Alpitour", "Roma - Palazzo dello Sport" },
                new List<string> {    "04/05/2025", "24/05/2025", "30/05/2025", "01/06/2025"})
             //GeneraDate(rnd.Next(2, 5), new DateTime(2025, 3, 25)))
        },

        { "AC/DC - Powerup Tour",
            (new List<string> { "Milano - Stadio San Siro", "Roma - Stadio Olimpico", "Verona - Arena di Verona", "Roma - Ippodromo delle Capannelle" },
                new List<string> {    "10/04/2025", "11/04/2025", "12/04/2025", "28/04/2025"})
             //GeneraDate(rnd.Next(4, 7), new DateTime(2025, 3, 25)))
        }
    };

        List<(string Titolo, string Artista, string Descrizione, Dictionary<string, (List<string> luoghi, List<string> date)> Dizionario)> Spettacoli = new List<(string, string, string, Dictionary<string, (List<string>, List<string>)>)>
    {
        ("Intelligenza Naturale", "Andrea Pezzi", "Uno spettacolo sull'intelligenza umana", Eventi),
        ("Marcus Miller", "Marcus Miller", "Il maestro del basso funk torna in Italia", Eventi),
        ("LRDL Summer Tour 2025", "La Rappresentante di Lista", "Musica e performance incredibili", Eventi),
        ("PalaJova", "Jovanotti", "Annunciate nuove date. Scopri i dettagli e acquista il tuo biglietto!", Eventi),
        ("Sophie and The Giants", "Sophie\nThe Giants", "Annunciato un nuovo appuntamento. Scopri i dettagli e acquista il tuo biglietto!", Eventi),
        ("Damme na mano Roma e Milano", "Tony Effe", "Annunciati nuovi appuntamenti live. Scopri i dettagli e acquista il tuo biglietto!", Eventi),
        ("Games in Concert", "Autore Sconosciuto", "Le colonne sonore dei più celebri videogames. Scopri i dettagli e acquista il tuo biglietto!", Eventi),
        ("FASK tour estivo 2025", "Fast Animals and Slow Kids", "Annunciate nuove date estive. Scopri i dettagli!", Eventi),
        ("Prova A Prendermi", "Guido Castrogiovanni\nTommaso Cassissa", "Arriva il musical basato sul film di Steven Spielberg con Leonardo DiCaprio e Tom Hanks.\nScopri i dettagli e acquista il tuo biglietto!", Eventi),
        ("Vita Bassa", "Giorgia Fumo", "Scopri i dettagli e acquista il tuo biglietto!", Eventi),
        ("Estate 2025", "Morrissey", "Scopri i dettagli e acquista il tuo biglietto!", Eventi),
        ("Jimmy Sax and Symphonic Dance Orchestra", "Jimmy Sax", "Scopri i dettagli e acquista il tuo biglietto!", Eventi),
        ("2025 World Tour - Milano", "Black Pink", "Le Blackpink, star mondiali del K-POP, arrivano in Italia per la prima volta.\nScopri i dettagli!", Eventi),
        ("AC/DC - Powerup Tour", "AC/DC", "Annunciata una data estiva del POWER UP Tour. Scopri i dettagli!", Eventi),
        };



        public Form1()
        {
            InitializeComponent();
            //dateTimePicker1.Value = DateTime.Today;

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

            foreach (var spettacolo in Spettacoli)
            {
                if (spettacolo.Titolo == pb.Tag.ToString())
                {
                    Descrizione_Lbl.Text = spettacolo.Descrizione;
                    Artista_Lbl.Text = spettacolo.Artista;
                    TitoloSpettacolo_Lbl.Text = spettacolo.Titolo;
                    Luogo_Lst.Items.Clear();
                    Luogo_Lst.Items.AddRange(spettacolo.Dizionario[spettacolo.Titolo].luoghi.ToArray());
                    Data_Lst.Items.Clear();
                    Data_Lst.Items.AddRange(spettacolo.Dizionario[spettacolo.Titolo].date.ToArray());
                    break;
                }
            }
            Pannello_Principale.Visible = false;
            Pannello_Posti.Location = new Point(0, 55);
            Pannello_Posti.Visible = true;

            Img_Info.Image = pb.Image;
            Img_Info.SizeMode = PictureBoxSizeMode.Zoom;
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

        private void Data_Lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            Luogo_Lst.SelectedIndex = Data_Lst.SelectedIndex;
        }

        private void Luogo_Lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data_Lst.SelectedIndex = Luogo_Lst.SelectedIndex;
        }

        /*private void txt_nascita_TextChanged(object sender, EventArgs e)
        {
            if(txt_nascita.SelectedText ==true)
            if (txt_nascita.Text.Length == 10)
            {
                if (txt_nascita.Text[2] == '/' && txt_nascita.Text[5] == '/')
                {
                    string[] data = txt_nascita.Text.Split('/');
                    if (int.TryParse(data[0], out int giorno) && int.TryParse(data[1], out int mese) && int.TryParse(data[2], out int anno))
                    {
                        if (giorno > 0 && giorno <= 31 && mese > 0 && mese <= 12 && anno > 1900 && anno < 2025)
                        {
                            txt_nascita.BackColor = Color.White;
                        }
                        else
                        {
                            txt_nascita.BackColor = Color.Red;
                        }
                    }
                    else
                    {
                        txt_nascita.BackColor = Color.Red;
                    }
                }
                else
                {
                    txt_nascita.BackColor = Color.Red;
                }
            }
            else
            {
                txt_nascita.BackColor = Color.Red;
            }
        }*/
    }
}
