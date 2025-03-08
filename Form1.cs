﻿using System;
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
        static List<string> DateUSateLoad = new List<string>();

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

        static List<Button> TempPostiSel = new List<Button>();

        static Dictionary<string, Dictionary<(string luogo, string data), List<Button>>> PostiEvento = new Dictionary<string, Dictionary<(string, string), List<Button>>>();

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
        List<(string Titolo, string Artista, string Descrizione, Dictionary<string, (List<string> luoghi, List<string> date)> Evento)> Spettacoli = new List<(string, string, string, Dictionary<string, (List<string>, List<string>)>)>
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

        private void ComuniITA()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources", "T4_codicicatastali_comuni_20_01_2020.xlsx");
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

        private void PostiEventoFirstLoad()
        {
            foreach (var spettacolo in Spettacoli)
            {
                Dictionary<(string, string), List<Button>> postiEvento = new Dictionary<(string, string), List<Button>>();
                var luoghi = spettacolo.Evento[spettacolo.Titolo].luoghi;
                var date = spettacolo.Evento[spettacolo.Titolo].date;

                for (int i = 0; i < luoghi.Count; i++)
                {
                    var luogo = luoghi[i];
                    var data = date[i];

                    List<Button> posti = new List<Button>();
                    foreach (var settore in Pgn_SelezionePosti.Controls.OfType<Panel>())
                    {
                        foreach (Button posto in settore.Controls.OfType<Button>())
                        {
                            if (posto.Tag.ToString()[0] == '0')
                            {
                                posti.Add(posto);
                            }
                        }
                    }
                    postiEvento.Add((luogo, data), posti);
                }
                PostiEvento.Add(spettacolo.Titolo, postiEvento);
            }
        }

        private void PostiEventoShow(string titolo, string luogo, string data)
        {
            if (PostiEvento.ContainsKey(titolo) && PostiEvento[titolo].ContainsKey((luogo, data)))
            {
                var postiOccupati = PostiEvento[titolo][(luogo, data)];

                foreach (var posto in Pannello_Posti.Controls.OfType<Button>())
                {
                    if (postiOccupati.Any(p => p.Location == posto.Location && p.Tag.ToString()[0] == '1'))
                    {
                        posto.BackColor = Color.Gray;
                        posto.Enabled = false;
                    }
                    else
                    {
                        posto.BackColor = Color.LightSalmon;
                        posto.Enabled = true;
                    }
                }
            }
        }


        public Form1()
        {
            InitializeComponent();
            ComuniITA();
            PostiEventoFirstLoad();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1148, 488);
            Pannello_Principale.Size = new Size(1151, 434);
            Pannello_Principale.Location = new Point(0, 54);
            Pannello_Posti.Size = new Size(1151, 434);
            Pannello_Posti.Location = new Point(0, 54);
            Pannello_Posti.Visible = false;
            Pannello_Login.Size = new Size(1151, 434);
            Pannello_Login.Location = new Point(0, 54);
            Pannello_Login.Visible = false;
            Pannello_Pagamento.Size = new Size(1151, 434);
            Pannello_Pagamento.Location = new Point(0, 54);
            Pannello_Pagamento.Visible = false;
        }


        private void Spettacolo_Click(object sender, EventArgs e)
        {
            PostiSelezionati = 0;
            PictureBox pb = (PictureBox)sender;

            foreach (var spettacolo in Spettacoli)
            {
                if (spettacolo.Titolo == pb.Tag.ToString())
                {
                    Descrizione_Lbl.Text = spettacolo.Descrizione;
                    Artista_Lbl.Text = spettacolo.Artista;
                    TitoloSpettacolo_Lbl.Text = spettacolo.Titolo;
                    Luogo_Lst.Items.Clear();
                    Luogo_Lst.Items.AddRange(spettacolo.Evento[spettacolo.Titolo].luoghi.ToArray());
                    Data_Lst.Items.Clear();
                    Data_Lst.Items.AddRange(spettacolo.Evento[spettacolo.Titolo].date.ToArray());
                    break;
                }
            }
            Pannello_Principale.Visible = false;
            Pannello_Posti.Location = new Point(0, 54);
            Pannello_Posti.Visible = true;

            Img_Info.Image = pb.Image;
            Img_Info.SizeMode = PictureBoxSizeMode.Zoom;
        }


        int PostiSelezionati;
        int posti;
        private void PostoSelezionato_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.BackColor == Color.Yellow)
            {
                switch (btn.Tag.ToString())
                {
                    case "0Normal":
                        btn.BackColor = Color.LightSalmon;
                        PostiSelezionati--;
                        break;
                    case "0Senior":
                        btn.BackColor = Color.Violet;
                        PostiSelezionati--;
                        break;
                    case "Prato":
                        btn.BackColor = Color.PaleGreen;
                        PostiSelezionati -= posti;
                        PratoPiu.Visible = false;
                        PratoMeno.Visible = false;
                        PratoPostiNum_Lbl.Visible = false;
                        break;
                    case "VIP":
                        btn.BackColor = Color.Gold;
                        break;
                    case null:
                        return;

                }
                if (TempPostiSel.Contains(btn))
                {
                    TempPostiSel.Remove(btn);
                }
            }
            else
            {
                if (PostiSelezionati < 4)
                {
                    if(btn.Tag.ToString() == "Prato")
                    {
                        posti = 0;
                        PSel = PostiSelezionati;
                        PratoPostiNum_Lbl.Visible = true;
                        PratoPiu.Visible = true;
                        PratoMeno.Visible = true;
                        PratoPostiNum_Lbl.Text = posti.ToString(); 
                        btn.BackColor = Color.Yellow;
                    }
                    else
                    {
                        TempPostiSel.Add(btn);
                        PostiSelezionati++;
                        btn.BackColor = Color.Yellow;
                        string test = $"{btn.Parent.Name}\n\nPosto: {btn.Text}";
                        MessageBox.Show(test);
                    }
                }
                else
                {
                    MessageBox.Show("Hai già raggiunto il numero massimo di posti selezionabili");
                }
            }
        }

        int PSel;
        private void PratoPosti(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            
            if (btn.Text == "+1")
            {
                if (PSel + posti < 4)
                {
                    posti++;
                    PostiSelezionati++;
                    PratoPostiNum_Lbl.Text = posti.ToString();
                }
                else
                {
                    MessageBox.Show("Hai già raggiunto il numero massimo di posti selezionabili");
                }
            }
            else
            {
                PSel = PostiSelezionati;
                if (posti > 0)
                {
                    posti--;
                    PostiSelezionati--;
                    PratoPostiNum_Lbl.Text = posti.ToString();
                }
            }
        }


        private void TickeTlon_Click(object sender, EventArgs e)
        {
            Pannello_Principale.Visible = true;
            Pannello_Principale.Location = new Point(0, 54);
            Pannello_Posti.Visible = false;
            Pannello_Login.Visible = false;
            Pannello_Pagamento.Visible = false;
            Tab_Info_Posti.SelectedIndex = 0;
            TempPostiSel.Clear();
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
            //if(Login_Register-0)
            Login = true;
            Pannello_Login.Visible = false;
            Pannello_Login.Enabled = false;
            Pannello_Posti.Visible = false;
            Pannello_Principale.Visible = true;
        }
        private void Pannello_Posti_MouseMove(object sender, MouseEventArgs e)
        {
            if (Login)
            {
                Pannello_Login.Visible = false;
                Pannello_Principale.Visible = false;
                Pannello_Posti.Visible = true;
                Tab_Info_Posti.Enabled = true;
                if (Luogo_Lst.SelectedIndex != -1 && Data_Lst.SelectedIndex != -1 && Tab_Info_Posti.SelectedIndex == 1)
                {
                    Pgn_SelezionePosti.Enabled = true;
                    PostiEventoShow(TitoloSpettacolo_Lbl.Text, Luogo_Lst.SelectedItem.ToString(), Data_Lst.SelectedItem.ToString());
                }
                else Pgn_SelezionePosti.Enabled = false;
            }
            else
            {
                Tab_Info_Posti.Enabled = false;
            }
        }

        private void Pgn_Register_Click(object sender, EventArgs e)
        {

        }

        private void FormClose_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
        }

        private void Btn_ConfemaPosti_Click(object sender, EventArgs e)
        {
            if(PostiSelezionati > 0)
            {
                Pannello_Pagamento.Visible = true;
                Pannello_Posti.Visible = false;
            }
            else
            {
                MessageBox.Show("Seleziona almeno un posto");
            }
        }

        private void Btn_Pagamento_Click(object sender, EventArgs e)
        {
            foreach (var btn in TempPostiSel)
            {
                btn.Tag = "1" + btn.Tag.ToString().Substring(1);
                PostiEvento[TitoloSpettacolo_Lbl.Text][(Luogo_Lst.SelectedItem.ToString(), Data_Lst.SelectedItem.ToString())].Add(btn);
            }
            TempPostiSel.Clear();
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
