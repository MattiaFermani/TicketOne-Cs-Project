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
using ClosedXML.Excel;
using System.Windows.Forms;

namespace Biglietti_concerto
{
    public partial class Form1 : Form
    {
        static Random rand = new Random();
        static List<string> Comuni = new List<string>();
        static List<string> CodiciBelfiore = new List<string>();
        readonly static List<string> Luoghi = new List<string>
        {
            "Firenze - Teatro Verdi",
            "Roma - Stadio Olimpico",
            "Roma - Auditorium Parco della Musica",
            "Torino - Teatro Regio",
            "Verona - Arena di Verona",
            "Milano - Teatro La Scala",
            "Milano - Blue Note",
            "Torino - Pala Alpitour",
            "Roma - Palazzetto dello Sport",
            "Napoli - Teatro Centrale",
            "Firenze - Stadio Artemio Franchi",
            "Roma - Ippodromo delle Capannelle",
            "Milano - Mediolanum Forum",
            "Bologna - Unipol Arena",
            "Milano - Alcatraz",
            "Roma - Atlantico",
            "Bologna - Estragon Club",
            "Napoli - Palapartenope",
            "Roma - Circo Massimo",
            "Milano - Ippodromo Snai",
            "Roma - Auditorium della Musica",
            "Firenze - Palazzo dei Congressi",
            "Palermo - Teatro Massimo",
            "Roma - Villa Ada",
            "Milano - Fabrique",
            "Genova - Politeama Genovese",
            "Milano - Teatro Manzoni",
            "Milano - Teatro Elfo Puccini",
            "Milano - Auditorium San Fedele",
            "Torino - Teatro Gobetti",
            "Bari - Stadio San Nicola",
            "Milano - Teatro degli Arcimboldi",
            "Roma - Palazzo dello Sport",
            "Milano - Stadio San Siro"
        };

        static List<string> Date_Luoghi_Random(int cosa, Random rand)
        {
            switch (cosa)
            {
                case 0:
                    List<string> luoghi = new List<string>();
                    for (int i = 0; i < 4; i++)
                    {
                        int index = rand.Next(0, Luoghi.Count);
                        luoghi.Add(Luoghi[index]);
                    }
                    return luoghi;
                case 1:
                    List<string> dates = new List<string>();
                    for (int i = 0; i < 4; i++)
                    {
                        int day = rand.Next(1, 29);
                        int month = rand.Next(4, 13);
                        int year = 2025 + rand.Next(0, 2);
                        dates.Add($"{year:D4}/{month:D2}/{day:D2}");
                    }

                    dates.Sort();
                    dates.Reverse();

                    for (int i = 0; i < dates.Count; i++)
                    {
                        DateTime dt = DateTime.ParseExact(dates[i], "yyyy/MM/dd", null);
                        dates[i] = dt.ToString("dd/MM/yyyy");
                    }

                    dates.Reverse();
                    return dates;
            }
            return null;
        }

        static Dictionary<string, (List<string> luoghi, List<string> date)> Eventi = new Dictionary<string, (List<string>, List<string>)>
        {
            { "Intelligenza Naturale", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand)) },
            { "Marcus Miller", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand)) },
            { "LRDL Summer Tour 2025", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand)) },
            { "PalaJova", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand)) },
            { "Sophie and The Giants", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand)) },
            { "Damme na mano Roma e Milano", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand)) },
            { "Games in Concert", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand)) },
            { "FASK tour estivo 2025", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand)) },
            { "Prova A Prendermi", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand)) },
            { "Vita Bassa", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand)) },
            { "Estate 2025", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand)) },
            { "Jimmy Sax and Symphonic Dance Orchestra", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand)) },
            { "2025 World Tour - Milano", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand)) },
            { "AC/DC - Powerup Tour", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand)) }
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

        private void ComuniITA(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rowCount = worksheet.RowsUsed();

                    foreach (var row in rowCount)
                    {
                        string comune = row.Cell(2).GetValue<string>();
                        string codiceBelfiore = row.Cell(1).GetValue<string>();

                        Comuni.Add(comune);
                        CodiciBelfiore.Add(codiceBelfiore);

                        Comuni_Lst.Items.Add($"{comune} - {codiceBelfiore} ");
                    }
                }
            }
            else
            {
                MessageBox.Show("Il file Excel non esiste!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Form1()
        {
            InitializeComponent();
            ComuniITA(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources", "T4_codicicatastali_comuni_20_01_2020.xlsx"));

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1167, 528);
            Pannello_Principale.Size = new Size(1151, 434);
            Pannello_Principale.Location = new Point(0, 54);
            Pannello_Posti.Size = new Size(1151, 434);
            Pannello_Login.Size = new Size(1151, 434);
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
            Pannello_Login.Visible = false;
        }
        private void Data_Lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            Luogo_Lst.SelectedIndex = Data_Lst.SelectedIndex;
        }
        private void Luogo_Lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data_Lst.SelectedIndex = Luogo_Lst.SelectedIndex;
        }




        bool Login = false;
        private void Account_Click(object sender, EventArgs e)
        {
            Pannello_Principale.Visible = false;
            Pannello_Posti.Visible = false;
            Pannello_Login.Visible = true;
            Pannello_Login.Location = new Point(0, 54);
        }

        private void Login_Register(object sender, EventArgs e)
        {
            Login = true;
            Pannello_Login.Visible = false;
            Pannello_Login.Enabled = false;
            Pannello_Posti.Visible = false;
            Pannello_Principale.Visible = true;
        }

        private void Tab_Info_Posti_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void Pannello_Posti_MouseMove(object sender, MouseEventArgs e)
        {
            if (Login == true)
            {
                Info_Error.Visible = false;
            }
            else
            {
                Info_Error.Visible = true;
            }
        }

        private void Pgn_Register_Click(object sender, EventArgs e)
        {

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
