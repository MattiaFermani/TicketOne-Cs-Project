using ClosedXML.Excel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Biglietti_concerto
{


    public partial class Form1 : Form
    {

        static string loggedNome = "";
        static string loggedTelefono = "";
        static string loggedEmail = "";
        static string loggedRole = "";

        static string adminFilePath = "admin.dat";
        static string encryptionKey = "abcabc"; // 🔑 Chiave di cifratura (NON salvarla nel codice in produzione!)


        static readonly string filePath = "accounts.json";

        private Button button;
        private Panel panel;
        static Random rand = new Random();
        static List<string> Comuni = new List<string>();
        static List<string> CodiciBelfiore = new List<string>();


        readonly static List<string> Luoghi = new List<string>
        {
            "Milano - Stadio San Siro",
            "Roma - Stadio Olimpico",
            "Torino - Pala Alpitour",
            "Firenze - Stadio Artemio Franchi",
            "Napoli - Stadio Diego Armando Maradona",
            "Bologna - Unipol Arena",
            "Verona - Arena di Verona",
            "Genova - Stadio Luigi Ferraris",
            "Palermo - Stadio Renzo Barbera",
            "Bari - Stadio San Nicola",
            "Venezia - Teatro La Fenice",
            "Parma - Teatro Regio",
            "Trieste - Teatro Lirico Giuseppe Verdi",
            "Catania - Teatro Massimo Bellini",
            "Lucca - Piazza Napoleone",
            "Perugia - PalaEvangelisti",
            "Cagliari - Sardegna Arena",
            "Padova - Gran Teatro Geox",
            "Brescia - PalaGeorge",
            "Livorno - Stadio Armando Picchi",
            "Pisa - Verdi Theater",
            "Reggio Emilia - RCF Arena",
            "Rimini - 105 Stadium",
            "Ancona - Mole Vanvitelliana",
            "L'Aquila - Stadio Tommaso Fattori",
            "Udine - Dacia Arena",
            "Salerno - Stadio Arechi",
            "Taranto - Stadio Erasmo Iacovone",
            "Foggia - Stadio Pino Zaccheria",
            "Latina - Stadio Domenico Francioni",
            "Pescara - Stadio Adriatico",
            "Ascoli Piceno - Stadio Cino e Lillo Del Duca",
            "Terni - Stadio Libero Liberati",
            "Vicenza - Stadio Romeo Menti",
            "Treviso - Stadio Omobono Tenni",
            "Como - Stadio Giuseppe Sinigaglia",
            "Lecco - Stadio Rigamonti-Ceppi",
            "Bergamo - Gewiss Stadium",
            "Cremona - Stadio Giovanni Zini",
            "Mantova - Stadio Danilo Martelli",
            "Alessandria - Stadio Giuseppe Moccagatta",
            "Vercelli - Stadio Silvio Piola",
            "Novara - Stadio Silvio Piola",
            "Biella - Stadio Lamarmora",
            "Aosta - Stadio Mario Puchoz",
            "Cuneo - Stadio Fratelli Paschiero",
            "Savona - Stadio Valerio Bacigalupo",
            "La Spezia - Stadio Alberto Picco",
            "Piacenza - Stadio Leonardo Garilli",
            "Pavia - Stadio Pietro Fortunati",
            "Sondrio - Stadio Moretti Orobici",
            "Varese - Stadio Franco Ossola",
            "Monza - Stadio Brianteo",
            "Lodi - Stadio Dossenina",
            "Cremona - Stadio Giovanni Zini",
            "Siena - Stadio Artemio Franchi",
            "Arezzo - Stadio Città di Arezzo",
            "Grosseto - Stadio Carlo Zecchini",
            "Prato - Stadio Lungobisenzio",
            "Pistoia - Stadio Marcello Melani",
            "Lucca - Stadio Porta Elisa",
            "Massa - Stadio degli Oliveti",
            "Carrara - Stadio dei Marmi",
            "Livorno - Stadio Armando Picchi",
            "Pisa - Arena Garibaldi",
            "Ferrara - Stadio Paolo Mazza",
            "Ravenna - Stadio Bruno Benelli",
            "Forlì - Stadio Tullo Morgagni",
            "Cesena - Stadio Dino Manuzzi",
            "Rimini - Stadio Romeo Neri",
            "San Marino - Stadio Olimpico",
            "Pesaro - Stadio Tonino Benelli",
            "Ancona - Stadio Del Conero",
            "Macerata - Stadio Helvia Recina",
            "Ascoli Piceno - Stadio Cino e Lillo Del Duca",
            "Teramo - Stadio Gaetano Bonolis",
            "Pescara - Stadio Adriatico",
            "Chieti - Stadio Guido Angelini",
            "L'Aquila - Stadio Tommaso Fattori",
            "Rieti - Stadio Centro d'Italia",
            "Viterbo - Stadio Enrico Rocchi",
            "Frosinone - Stadio Benito Stirpe",
            "Latina - Stadio Domenico Francioni",
            "Caserta - Stadio Alberto Pinto",
            "Benevento - Stadio Ciro Vigorito",
            "Avellino - Stadio Partenio",
            "Salerno - Stadio Arechi",
            "Napoli - Stadio San Paolo",
            "Torre del Greco - Stadio Amerigo Liguori",
            "Castellammare di Stabia - Stadio Romeo Menti",
            "Nocera Inferiore - Stadio San Francesco d'Assisi",
            "Cava de' Tirreni - Stadio Simonetta Lamberti",
            "Agropoli - Stadio Raffaele Guariglia",
            "Battipaglia - Stadio Luigi Pastena",
            "Eboli - Stadio José Guimarães Dirceu",
            "Altamura - Stadio Tonino D'Angelo",
            "Andria - Stadio degli Ulivi",
            "Barletta - Stadio Cosimo Puttilli",
            "Bisceglie - Stadio Gustavo Ventura",
            "Canosa di Puglia - Stadio San Sabino",
            "Cerignola - Stadio Domenico Monterisi",
            "Foggia - Stadio Pino Zaccheria",
            "Lucera - Stadio Giuseppe Capozza",
            "Manfredonia - Stadio Miramare",
            "San Severo - Stadio Ricciardelli",
            "Trani - Stadio Comunale",
            "Brindisi - Stadio Franco Fanuzzi",
            "Francavilla Fontana - Stadio Giovanni Paolo II",
            "Lecce - Stadio Via del Mare",
            "Gallipoli - Stadio Antonio Bianco",
            "Manduria - Stadio Nino Dimitri",
            "Martina Franca - Stadio Giuseppe Domenico Tursi",
            "Mesagne - Stadio Gianni De Luca",
            "Monopoli - Stadio Vito Simone Veneziani",
            "Ostuni - Stadio Comunale",
            "San Vito dei Normanni - Stadio Comunale",
            "Taranto - Stadio Erasmo Iacovone",
            "Crotone - Stadio Ezio Scida",
            "Lamezia Terme - Stadio Guido D'Ippolito",
            "Reggio Calabria - Stadio Oreste Granillo",
            "Vibo Valentia - Stadio Luigi Razza",
            "Catanzaro - Stadio Nicola Ceravolo",
            "Cosenza - Stadio San Vito",
            "Rende - Stadio Marco Lorenzon",
            "Castrovillari - Stadio Mimmo Rende",
            "Corigliano Calabro - Stadio Comunale",
            "Rossano - Stadio Stefano Rizzo",
            "Soverato - Stadio Comunale",
            "Tropea - Stadio Comunale",
            "Villafranca Tirrena - Stadio Comunale",
            "Acireale - Stadio Tupparello",
            "Caltagirone - Stadio Comunale",
            "Catania - Stadio Angelo Massimino",
            "Gela - Stadio Vincenzo Presti",
            "Messina - Stadio San Filippo",
            "Milazzo - Stadio Grotta Polifemo",
            "Patti - Stadio Comunale",
            "Ragusa - Stadio Aldo Campo",
            "Siracusa - Stadio Nicola De Simone",
            "Taormina - Stadio Comunale",
            "Trapani - Stadio Polisportivo Provinciale",
            "Agrigento - Stadio Esseneto",
            "Caltanissetta - Stadio Marco Tomaselli",
            "Enna - Stadio Generale Gaeta",
            "Marsala - Stadio Antonino Lombardo Angotta",
            "Mazara del Vallo - Stadio Nino Vaccara",
            "Palermo - Stadio Renzo Barbera",
            "Termini Imerese - Stadio Comunale",
            "Bagheria - Stadio Comunale",
            "Partinico - Stadio Comunale",
            "Sciacca - Stadio Luigi Riccardo Gurrera",
            "Trapani - Stadio Polisportivo Provinciale",
            "Alghero - Stadio Mariotti",
            "Cagliari - Stadio Sant'Elia",
            "Carbonia - Stadio Comunale",
            "Iglesias - Stadio Comunale",
            "Nuoro - Stadio Franco Frogheri",
            "Olbia - Stadio Bruno Nespoli",
            "Oristano - Stadio Tharros",
            "Sassari - Stadio Vanni Sanna",
            "Tempio Pausania - Stadio Nino Manus",
            "Tortolì - Stadio Fra Locci",
            "Lanusei - Stadio Comunale",
            "Aosta - Stadio Mario Puchoz",
            "Ivrea - Stadio Gino Pistoni",
            "Biella - Stadio Lamarmora",
            "Verbania - Stadio Carlo Pedroli",
            "Domodossola - Stadio Comunale",
            "Novara - Stadio Silvio Piola",
            "Vercelli - Stadio Silvio Piola",
            "Alessandria - Stadio Giuseppe Moccagatta",
            "Casale Monferrato - Stadio Natale Palli",
            "Tortona - Stadio Fausto Coppi",
            "Acqui Terme - Stadio Jona Ottolenghi",
            "Ovada - Stadio Comunale",
            "Savona - Stadio Valerio Bacigalupo",
            "Albenga - Stadio Annibale Riva",
            "Finale Ligure - Stadio Comunale",
            "Imperia - Stadio Nino Ciccione",
            "Sanremo - Stadio Comunale",
            "Ventimiglia - Stadio Simone Rizzato",
            "Cuneo - Stadio Fratelli Paschiero",
            "Alba - Stadio Comunale",
            "Bra - Stadio Attilio Bravi",
            "Fossano - Stadio Angelo Pochissimo",
            "Savigliano - Stadio Comunale",
            "Saluzzo - Stadio Amedeo Damiano",
            "Pinerolo - Stadio Luigi Barbieri",
            "Torino - Stadio Olimpico Grande Torino",
            "Chivasso - Stadio Comunale",
            "Ivrea - Stadio Gino Pistoni",
            "Biella - Stadio Lamarmora",
            "Verbania - Stadio Carlo Pedroli",
            "Domodossola - Stadio Comunale",
            "Novara - Stadio Silvio Piola",
            "Vercelli - Stadio Silvio Piola",
            "Alessandria - Stadio Giuseppe Moccagatta",
            "Casale Monferrato - Stadio Natale Palli",
            "Tortona - Stadio Fausto Coppi",
            "Acqui Terme - Stadio Jona Ottolenghi",
            "Ovada - Stadio Comunale",
            "Savona - Stadio Valerio Bacigalupo",
            "Albenga - Stadio Annibale Riva",
            "Finale Ligure - Stadio Comunale",
            "Imperia - Stadio Nino Ciccione",
            "Sanremo - Stadio Comunale",
            "Ventimiglia - Stadio Simone Rizzato",
            "Cuneo - Stadio Fratelli Paschiero",
            "Alba - Stadio Comunale",
            "Bra - Stadio Attilio Bravi",
            "Fossano - Stadio Angelo Pochissimo",
            "Savigliano - Stadio Comunale",
            "Saluzzo - Stadio Amedeo Damiano",
            "Pinerolo - Stadio Luigi Barbieri",
            "Torino - Stadio Olimpico Grande Torino",
            "Chivasso - Stadio Comunale",
            "Ivrea - Stadio Gino Pistoni",
            "Biella - Stadio Lamarmora",
            "Verbania - Stadio Carlo Pedroli",
            "Domodossola - Stadio Comunale",
            "Novara - Stadio Silvio Piola",
            "Vercelli - Stadio Silvio Piola",
            "Alessandria - Stadio Giuseppe Moccagatta",
            "Casale Monferrato - Stadio Natale Palli",
            "Tortona - Stadio Fausto Coppi",
            "Acqui Terme - Stadio Jona Ottolenghi",
            "Ovada - Stadio Comunale",
            "Savona - Stadio Valerio Bacigalupo",
            "Albenga - Stadio Annibale Riva",
            "Finale Ligure - Stadio Comunale",
            "Imperia - Stadio Nino Ciccione",
            "Sanremo - Stadio Comunale",
            "Ventimiglia - Stadio Simone Rizzato",
            "Cuneo - Stadio Fratelli Paschiero",
            "Alba - Stadio Comunale",
            "Bra - Stadio Attilio Bravi",
            "Fossano - Stadio Angelo Pochissimo",
            "Savigliano - Stadio Comunale",
            "Saluzzo - Stadio Amedeo Damiano",
            "Pinerolo - Stadio Luigi Barbieri",
            "Torino - Stadio Olimpico Grande Torino",
            "Chivasso - Stadio Comunale",
            "Ivrea - Stadio Gino Pistoni",
            "Biella - Stadio Lamarmora",
            "Verbania - Stadio Carlo Pedroli",
            "Domodossola - Stadio Comunale",
            "Novara - Stadio Silvio Piola",
            "Vercelli - Stadio Silvio Piola",
            "Alessandria - Stadio Giuseppe Moccagatta",
            "Casale Monferrato - Stadio Natale Palli",
            "Tortona - Stadio Fausto Coppi",
            "Acqui Terme - Stadio Jona Ottolenghi",
            "Ovada - Stadio Comunale",
            "Savona - Stadio Valerio Bacigalupo",
            "Albenga - Stadio Annibale Riva",
            "Finale Ligure - Stadio Comunale",
            "Imperia - Stadio Nino Ciccione",
            "Sanremo - Stadio Comunale",
            "Ventimiglia - Stadio Simone Rizzato",
            "Cuneo - Stadio Fratelli Paschiero",
            "Alba - Stadio Comunale",
            "Bra - Stadio Attilio Bravi",
            "Fossano - Stadio Angelo Pochissimo",
            "Savigliano - Stadio Comunale",
            "Saluzzo - Stadio Amedeo Damiano",
            "Pinerolo - Stadio Luigi Barbieri",
            "Torino - Stadio Olimpico Grande Torino",
            "Chivasso - Stadio Comunale",
            "Ivrea - Stadio Gino Pistoni",
            "Biella - Stadio Lamarmora",
            "Verbania - Stadio Carlo Pedroli",
            "Domodossola - Stadio Comunale",
            "Novara - Stadio Silvio Piola",
            "Vercelli - Stadio Silvio Piola",
            "Alessandria - Stadio Giuseppe Moccagatta",
            "Casale Monferrato - Stadio Natale Palli",
            "Tortona - Stadio Fausto Coppi",
            "Acqui Terme - Stadio Jona Ottolenghi",
            "Ovada - Stadio Comunale",
            "Savona - Stadio Valerio Bacigalupo",
            "Albenga - Stadio Annibale Riva",
            "Finale Ligure - Stadio Comunale",
            "Imperia - Stadio Nino Ciccione",
            "Sanremo - Stadio Comunale",
            "Ventimiglia - Stadio Simone Rizzato",
            "Cuneo - Stadio Fratelli Paschiero",
            "Alba - Stadio Comunale",
            "Bra - Stadio Attilio Bravi",
            "Fossano - Stadio Angelo Pochissimo",
            "Savigliano - Stadio Comunale",
            "Saluzzo - Stadio Amedeo Damiano",
            "Pinerolo - Stadio Luigi Barbieri",
            "Torino - Stadio Olimpico Grande Torino",
            "Chivasso - Stadio Comunale",
            "Ivrea - Stadio Gino Pistoni",
            "Biella - Stadio Lamarmora",
            "Verbania - Stadio Carlo Pedroli",
            "Domodossola - Stadio Comunale",
            "Novara - Stadio Silvio Piola",
            "Vercelli - Stadio Silvio Piola",
            "Alessandria - Stadio Giuseppe Moccagatta",
            "Casale Monferrato - Stadio Natale Palli",
            "Tortona - Stadio Fausto Coppi",
            "Acqui Terme - Stadio Jona Ottolenghi",
            "Ovada - Stadio Comunale",
            "Savona - Stadio Valerio Bacigalupo",
            "Albenga - Stadio Annibale Riva",
            "Finale Ligure - Stadio Comunale",
            "Imperia - Stadio Nino Ciccione",
            "Sanremo - Stadio Comunale",
            "Ventimiglia - Stadio Simone Rizzato",
            "Cuneo - Stadio Fratelli Paschiero",
            "Alba - Stadio Comunale",
            "Bra - Stadio Attilio Bravi",
            "Fossano - Stadio Angelo Pochissimo",
            "Savigliano - Stadio Comunale",
            "Saluzzo - Stadio Amedeo Damiano",
            "Pinerolo - Stadio Luigi Barbieri",
            "Pinerolo - Stadio Luigi Barbieri",
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

        static Dictionary<string, (List<string> luoghi, List<string> date, Dictionary<(string luogo, string data), Dictionary<string, List<Button>>> buttons)> Eventi = new Dictionary<string, (List<string>, List<string>, Dictionary<(string, string), Dictionary<string, List<Button>>>)>
        {
            { "Intelligenza Naturale", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand), new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
            { "Marcus Miller", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand), new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
            { "LRDL Summer Tour 2025", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand), new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
            { "PalaJova", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand), new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
            { "Sophie and The Giants", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand), new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
            { "Damme na mano Roma e Milano", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand), new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
            { "Games in Concert", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand), new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
            { "FASK tour estivo 2025", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand), new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
            { "Prova A Prendermi", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand), new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
            { "Vita Bassa", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand), new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
            { "Estate 2025", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand), new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
            { "Jimmy Sax and Symphonic Dance Orchestra", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand), new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
            { "2025 World Tour - Milano", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand), new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
            { "AC/DC - Powerup Tour", (Date_Luoghi_Random(0, rand), Date_Luoghi_Random(1, rand), new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
            { "Default", (new List<string> { "Default" }, new List<string> { "Default" }, new Dictionary<(string, string), Dictionary<string, List<Button>>>()) },
        };
        List<(string Titolo, string Artista, string Descrizione, Dictionary<string, (List<string> luoghi, List<string> date, Dictionary<(string luogo, string data), Dictionary<string, List<Button>>> buttons)> Dizionario)> Spettacoli = new List<(string, string, string, Dictionary<string, (List<string> luoghi, List<string> date, Dictionary<(string luogo, string data), Dictionary<string, List<Button>>> buttons)>)>
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

                        Comuni_Lst.Items.Add($"{comune}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Il file Excel non esiste!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CalcCodiceFiscale(string nome, string cognome, string dataNascita, string sesso, string comune)
        {
            string cf = "";
            string consonanti = "", vocali = "";
            foreach (char c in cognome)

                if ("AEIOU".Contains(c)) vocali += c;
                else consonanti += c;
            cf += (consonanti + vocali + "XXX").Substring(0, 3);

            consonanti = "";
            vocali = "";

            foreach (char c in nome)
                if ("AEIOU".Contains(c)) vocali += c;
                else consonanti += c;
            cf += (consonanti.Length >= 4) ? $"{consonanti[0]}{consonanti[2]}{consonanti[3]}" :
                  (consonanti + vocali + "XXX").Substring(0, 3);

            DateTime dt = DateTime.Parse(dataNascita);
            string anno = dt.Year.ToString().Substring(2, 2);
            string mesi = "ABCDEHLMPRST";
            string mese = mesi[dt.Month - 1].ToString();
            int giorno = dt.Day + (sesso == "F" ? 40 : 0);
            cf += anno + mese + giorno.ToString("D2");


            cf += Comuni.Contains(comune) ? CodiciBelfiore[Comuni_Lst.SelectedIndex] : "Z999";

            Dictionary<char, int> Pari = new Dictionary<char, int>
            {
                {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
                {'A', 0}, {'B', 1}, {'C', 2}, {'D', 3}, {'E', 4}, {'F', 5}, {'G', 6}, {'H', 7}, {'I', 8}, {'J', 9},
                {'K', 10}, {'L', 11}, {'M', 12}, {'N', 13}, {'O', 14}, {'P', 15}, {'Q', 16}, {'R', 17}, {'S', 18}, {'T', 19},
                {'U', 20}, {'V', 21}, {'W', 22}, {'X', 23}, {'Y', 24}, {'Z', 25}
            };

            Dictionary<char, int> Dispari = new Dictionary<char, int>
            {
                {'0', 1}, {'1', 0}, {'2', 5}, {'3', 7}, {'4', 9}, {'5', 13}, {'6', 15}, {'7', 17}, {'8', 19}, {'9', 21},
                {'A', 1}, {'B', 0}, {'C', 5}, {'D', 7}, {'E', 9}, {'F', 13}, {'G', 15}, {'H', 17}, {'I', 19}, {'J', 21},
                {'K', 2}, {'L', 4}, {'M', 18}, {'N', 20}, {'O', 11}, {'P', 3}, {'Q', 6}, {'R', 8}, {'S', 12}, {'T', 14},
                {'U', 16}, {'V', 10}, {'W', 22}, {'X', 25}, {'Y', 24}, {'Z', 23}
            };

            Dictionary<int, char> Carattere = new Dictionary<int, char>
            {
                {0, 'A'}, {1, 'B'}, {2, 'C'}, {3, 'D'}, {4, 'E'}, {5, 'F'}, {6, 'G'}, {7, 'H'}, {8, 'I'}, {9, 'J'},
                {10, 'K'}, {11, 'L'}, {12, 'M'}, {13, 'N'}, {14, 'O'}, {15, 'P'}, {16, 'Q'}, {17, 'R'}, {18, 'S'}, {19, 'T'},
                {20, 'U'}, {21, 'V'}, {22, 'W'}, {23, 'X'}, {24, 'Y'}, {25, 'Z'}
            };
            cf = cf.ToUpper();
            int somma = 0;
            for (int i = 0; i < 15; i++)
            {
                char c = cf[i];
                if ((i + 1) % 2 == 0)
                    somma += Pari[c];
                else
                    somma += Dispari[c];
            }

            cf += Carattere[(somma % 26)];

            return cf;
        }

        public Form1()
        {
            InitializeComponent();
            CreaAdminPassword("Cisco123");
            ComuniITA();
        }



        private void CaricaPostiEventi()
        {
            foreach (var key in Eventi.Keys.ToList())
            {
                var evento = Eventi[key];
                Dictionary<(string, string), Dictionary<string, List<Button>>> NewPosti = new Dictionary<(string, string), Dictionary<string, List<Button>>>();
                int i = 0;
                foreach (var luogo in evento.luoghi)
                {
                    string data = evento.date[i];
                    Dictionary<string, List<Button>> Settori = new Dictionary<string, List<Button>>();
                    foreach (Control control in Panel_Seats.Controls)
                    {
                        if (control is Panel Settore)
                        {
                            List<Button> Posti = new List<Button>();
                            foreach (Button Posto in Settore.Controls)
                            {
                                Posti.Add(Posto);
                            }
                            Settori.Add(Settore.Name, Posti);
                        }
                    }
                    NewPosti.Add((luogo, data), Settori);
                    i++;
                }
                Eventi[key] = (evento.luoghi, evento.date, NewPosti);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(1150, 485);
            Pannello_Principale.Size = new System.Drawing.Size(1151, 434);
            Pannello_Principale.Location = new Point(0, 54);
            Pannello_Posti.Size = new System.Drawing.Size(1151, 434);
            Pannello_Login.Size = new System.Drawing.Size(1151, 434);
            Pannello_Login.Location = new Point(0, 54);
            Pannello_Login.Visible = false;
            Pannello_Acc_User.Size = new System.Drawing.Size(1151, 434);
            Pannello_Acc_User.Location = new Point(0, 54);
            Pannello_Acc_User.Visible = false;
            AggiornaDisponibilitaTooltip();
            spettacoliToolTip.AutoPopDelay = 5000;
            spettacoliToolTip.InitialDelay = 300;
            spettacoliToolTip.ReshowDelay = 500;
            CaricaPostiEventi();
        }


        private void CreaAdminPassword(string password)
        {
            byte[] encryptedData = EncryptData(password, encryptionKey);
            File.WriteAllBytes(adminFilePath, encryptedData);
        }
        private byte[] EncryptData(string text, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32)); // AES usa chiavi da 32 byte
                aes.IV = new byte[16]; // Inizializziamo IV a 16 byte nulli

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cryptoStream))
                    {
                        writer.Write(text);
                    }
                    return ms.ToArray();
                }
            }
        }
        private string LeggiAdminPassword()
        {
            if (!File.Exists(adminFilePath))
            {
                MessageBox.Show("File admin non trovato.");
                return null;
            }

            byte[] encryptedData = File.ReadAllBytes(adminFilePath);
            return DecryptData(encryptedData, encryptionKey);
        }
        private string DecryptData(byte[] data, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32));
                aes.IV = new byte[16];

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(data))
                using (var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(cryptoStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private void Spettacolo_Click(object sender, EventArgs e)
        {
            if (Login)
            {
                Tab_Info_Posti.Enabled = true;
            }
            else
            {
                Tab_Info_Posti.Enabled = false;
            }
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
                    Luogo_Lst.Items.AddRange(spettacolo.Dizionario[spettacolo.Titolo].luoghi.ToArray());
                    Data_Lst.Items.Clear();
                    Data_Lst.Items.AddRange(spettacolo.Dizionario[spettacolo.Titolo].date.ToArray());
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
        private Dictionary<(string luogo, string data), List<Button>> postiSelezionati = new Dictionary<(string, string), List<Button>>();

        private void PostoSelezionato_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            button = btn;
            if (btn.BackColor != Color.Yellow)
            {
                if (PostiSelezionati < 4)
                {
                    btn.BackColor = Color.Yellow;
                    PostiSelezionati++;
                }
                else
                {
                    MessageBox.Show("Hai già selezionato 4 posti");
                }
            }
            else
            {
                switch (btn.Tag)
                {
                    case "0Normal":
                        btn.BackColor = Color.LightSalmon;
                        break;
                    case "0Senior":
                        btn.BackColor = Color.Violet;
                        break;
                    case "0Vip":
                        btn.BackColor = Color.Gold;
                        break;
                }
                PostiSelezionati--;
            }

            string luogo = Luogo_Lst.SelectedItem.ToString();
            string data = Data_Lst.SelectedItem.ToString();
            var key = (luogo, data);

            if (!postiSelezionati.ContainsKey(key))
            {
                postiSelezionati[key] = new List<Button>();
            }

            if (btn.BackColor == Color.Yellow)
            {
                postiSelezionati[key].Add(btn);
            }
            else
            {
                postiSelezionati[key].Remove(btn);
            }
        }

        private void Btn_ConfemaPosti_Click(object sender, EventArgs e)
        {
            if (PostiSelezionati != 0)
            {
                foreach (var entry in postiSelezionati)
                {
                    var eventKey = entry.Key;
                    foreach (var btn in entry.Value)
                    {

                        string settore = ((Panel)btn.Parent).Name;

                        if (!Eventi[TitoloSpettacolo_Lbl.Text].buttons.ContainsKey(eventKey))
                        {
                            Eventi[TitoloSpettacolo_Lbl.Text].buttons[eventKey] = new Dictionary<string, List<Button>>();
                        }
                        if (!Eventi[TitoloSpettacolo_Lbl.Text].buttons[eventKey].ContainsKey(settore))
                        {
                            Eventi[TitoloSpettacolo_Lbl.Text].buttons[eventKey][settore] = new List<Button>();
                        }

                        foreach (Button b in Eventi[TitoloSpettacolo_Lbl.Text].buttons[eventKey][settore])
                        {
                            if (b.Name == btn.Name)
                            {
                                int index = Eventi[TitoloSpettacolo_Lbl.Text].buttons[eventKey][settore].IndexOf(b);
                                Eventi[TitoloSpettacolo_Lbl.Text].buttons[eventKey][settore][index].BackColor = Color.Gray;
                                Eventi[TitoloSpettacolo_Lbl.Text].buttons[eventKey][settore][index].Enabled = false;
                                break;
                            }
                        }
                        //var existingButton = Eventi[TitoloSpettacolo_Lbl.Text].buttons[eventKey][settore].FirstOrDefault(b => b.Name == btn.Name);

                        //int index = Eventi[TitoloSpettacolo_Lbl.Text].buttons[eventKey][settore].IndexOf(existingButton);
                        //Eventi[TitoloSpettacolo_Lbl.Text].buttons[eventKey][settore][index].BackColor = Color.Gray;
                        //Eventi[TitoloSpettacolo_Lbl.Text].buttons[eventKey][settore][index].Enabled = false;

                    }
                }
                CaricaPosti("Default", "Default", "Default");

                postiSelezionati.Clear();
                MessageBox.Show($"{PostiSelezionati} Posti acquistati con successo!");
                PostiSelezionati = 0;
                AggiornaDisponibilitaTooltip();
            }
            else
            {
                MessageBox.Show("Nessun posto selezionato");
            }

            Pannello_Principale.Visible = false;
            Pannello_Principale.Location = new Point(0, 54);
            Tab_Info_Posti.SelectedIndex = 0;
            Data_Lst.SelectedIndex = -1;
            Luogo_Lst.SelectedIndex = -1;
            Pannello_Posti.Visible = false;
            Pannello_Pagamento.Visible = true;
            ResetPostiColori();
        }

        private void ResetPostiColori()
        {
            foreach (Control settore in Panel_Seats.Controls)
            {
                if (settore is Panel panelSettore)
                {
                    foreach (Button posto in panelSettore.Controls)
                    {
                        if (posto.BackColor == Color.Gray)
                        {
                            posto.Enabled = true;
                            switch (posto.Tag)
                            {
                                case "0Normal":
                                    posto.BackColor = Color.LightSalmon;
                                    break;
                                case "0Senior":
                                    posto.BackColor = Color.Violet;
                                    break;
                                case "0Vip":
                                    posto.BackColor = Color.Gold;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void CaricaPosti(string eventTitle, string selectedLuogo, string selectedData)
        {

            if (!Eventi.ContainsKey(eventTitle))
                return;

            var eventData = Eventi[eventTitle];
            var key = (selectedLuogo, selectedData);

            if (!eventData.buttons.ContainsKey(key))
            {
                foreach (Control settore in Panel_Seats.Controls)
                {
                    if (settore is Panel panelSettore)
                    {
                        foreach (Button posto in panelSettore.Controls)
                        {
                            switch (posto.Tag)
                            {
                                case "0Normal":
                                    posto.BackColor = Color.LightSalmon;
                                    break;
                                case "0Senior":
                                    posto.BackColor = Color.Violet;
                                    break;
                                case "0Vip":
                                    posto.BackColor = Color.Gold;
                                    break;
                            }
                        }
                    }
                }
                return;
            }

            var savedSectors = eventData.buttons[key];


            foreach (Control settore in Panel_Seats.Controls)
            {
                if (settore is Panel panelSettore)
                {
                    if (savedSectors.TryGetValue(panelSettore.Name, out List<Button> savedButtons))
                    {
                        foreach (Button posto in panelSettore.Controls)
                        {

                            var savedButton = savedButtons.FirstOrDefault(b => b.Name == posto.Name);
                            if (savedButton != null)
                            {
                                posto.BackColor = savedButton.BackColor;
                                posto.Enabled = savedButton.Enabled;
                            }
                            else
                            {
                                // Se non trovato, assegna il colore di default in base al Tag
                                switch (posto.Tag)
                                {
                                    case "0Normal":
                                        posto.BackColor = Color.LightSalmon;
                                        break;
                                    case "0Senior":
                                        posto.BackColor = Color.Violet;
                                        break;
                                    case "0Vip":
                                        posto.BackColor = Color.Gold;
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (Button posto in panelSettore.Controls)
                        {
                            switch (posto.Tag)
                            {
                                case "0Normal":
                                    posto.BackColor = Color.LightSalmon;
                                    break;
                                case "0Senior":
                                    posto.BackColor = Color.Violet;
                                    break;
                                case "0Vip":
                                    posto.BackColor = Color.Gold;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private ToolTip spettacoliToolTip = new ToolTip();

        private void AggiornaDisponibilitaTooltip()
        {
            foreach (Control c in Pannello_Principale.Controls)
            {
                if (c is PictureBox pb && pb.Tag != null)
                {
                    var spettacolo = Spettacoli.FirstOrDefault(s => s.Titolo == pb.Tag.ToString());
                    int postiTotali = 0;
                    int postiOccupati = 0;

                    if (spettacolo.Dizionario != null)
                    {
                        var eventoTuple = spettacolo.Dizionario[spettacolo.Titolo];
                        for (int i = 0; i < eventoTuple.luoghi.Count; i++)
                        {
                            string luogo = eventoTuple.luoghi[i];
                            string data = eventoTuple.date[i];
                            var key = (luogo, data);
                            if (eventoTuple.buttons.ContainsKey(key))
                            {
                                var postiEvento = eventoTuple.buttons[key];
                                postiTotali += postiEvento.Values.Sum(list => list.Count);
                                postiOccupati += postiEvento.Values.Sum(list => list.Count(p => p.BackColor == Color.Gray));
                            }
                        }
                    }

                    string disponibilita = (postiTotali - postiOccupati) < 20 ?
                        "ULTIMI POSTI DISPONIBILI!" : "Disponibilità normale";

                    spettacoliToolTip.SetToolTip(pb,
                        $"{spettacolo.Titolo}\nPosti liberi: {postiTotali - postiOccupati}/{postiTotali}\n{disponibilita}");
                }
            }
        }
        private void TickeTlon_Click(object sender, EventArgs e)
        {
            Pannello_Principale.Visible = true;
            Pannello_Principale.Location = new Point(0, 54);
            Pannello_Posti.Visible = false;
            Pannello_Login.Visible = false;
            Pannello_Acc_User.Visible = false;
            Pannello_Pagamento.Visible = false;
        }
        private void Data_Luogo_Lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data_Lst.SelectedIndex = Luogo_Lst.SelectedIndex;
            if (Luogo_Lst.SelectedIndex == -1) return;
            CaricaPosti(TitoloSpettacolo_Lbl.Text, Luogo_Lst.SelectedItem.ToString(), Data_Lst.SelectedItem.ToString());
        }


        bool Login = false;
        private void Account_Click(object sender, EventArgs e)
        {
            if (Login)
            {
                Pannello_Acc_User.Visible = true;
                txt_A_Nome.Text = loggedNome;
                txt_A_Email.Text = loggedEmail;
                Lbl_Role.Text = loggedRole;
                txt_A_Telefono.Text =  string.IsNullOrEmpty(loggedTelefono) ? "Nessun Telefono Impostato" : loggedTelefono;

            }
            else
            {
                Pannello_Principale.Visible = false;
                Pannello_Posti.Visible = false;
                Pannello_Login.Visible = true;
                Pannello_Login.Enabled = true;
                Pannello_Login.Location = new Point(0, 54);
            }
        }
        private string MascheraPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
        private void Btn_Register(object sender, EventArgs e)
        {

            if (txt_codicefiscale.BackColor == Color.PaleGreen && txt_confermapsw.BackColor == Color.PaleGreen && !string.IsNullOrEmpty(txt_email.Text) && !string.IsNullOrWhiteSpace(txt_nome_visualizzato.Text))
            {
                if (!int.TryParse(txt_telefono.Text, out int n) && txt_telefono.Text.Length != 10 && !string.IsNullOrEmpty(txt_telefono.Text))
                {
                    MessageBox.Show("Il numero di telefono deve essere composto da 10 cifre");
                    return;
                }
                Login = true;
                Pannello_Login.Visible = false;
                Pannello_Login.Enabled = false;
                Pannello_Posti.Visible = false;
                Pannello_Principale.Visible = true;
                string filePath = "accounts.json";

                JArray jsonArray;
                if (File.Exists(filePath))
                {
                    string jsonContent = File.ReadAllText(filePath);
                    jsonArray = JArray.Parse(jsonContent);
                }
                else
                {
                    jsonArray = new JArray();
                }

                JObject newProfile = new JObject
                {
                    ["Nome"] = txt_nome.Text,
                    ["Cognome"] = txt_cognome.Text,
                    ["Nome Visualizzato"] = txt_nome_visualizzato.Text,
                    ["Data di nascita"] = dtp_nascita.Text,
                    ["Comune"] = Comuni_Lst.SelectedItem.ToString(),
                    ["Codice Fiscale"] = txt_codicefiscale.Text,
                    ["Telefono"] = txt_telefono.Text,
                    ["Email"] = txt_email.Text,
                    ["Role"] = Chk_IsAdmin.Checked ? (PswAdmin() ? "Admin" : "User") : "User",
                    ["Password"] = MascheraPassword(txt_password.Text),
                };
                jsonArray.Add(newProfile);

                File.WriteAllText(filePath, jsonArray.ToString());

                loggedNome = newProfile["Nome Visualizzato"].ToString();
                loggedTelefono = newProfile["Telefono"].ToString();
                loggedEmail = newProfile["Email"].ToString();
                loggedRole = newProfile["Role"].ToString();

                MessageBox.Show("Registrazione Completata");

                Login = true;
                txb_psw_admin.Clear();
                Pannello_Login.Visible = false;
                Pannello_Login.Enabled = false;
                Pannello_Posti.Visible = false;
                Pannello_Principale.Visible = true;
            }
            else
            {
                MessageBox.Show("Controlla i dati inseriti e riprova");
            }
        }
        private void Btn_Login(object sender, EventArgs e)
        {

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Nessun account registrato.");
                return;
            }

            string jsonContent = File.ReadAllText(filePath);
            JArray jsonArray = JArray.Parse(jsonContent);

            string Input = txt_L_Email.Text;
            string passwordHashInput = MascheraPassword(txt_L_Password.Text);

            JObject user = jsonArray
                .Children<JObject>()
                .FirstOrDefault(u =>
                    (u["Email"]?.ToString() == Input || u["Nome Visualizzato"]?.ToString() == Input || u["Telefono"]?.ToString() == Input) &&
                    u["Password"]?.ToString() == passwordHashInput);

            if (user != null)
            {
                try
                {
                    loggedNome = user["Nome Visualizzato"].ToString();
                }
                catch (Exception)
                {
                    loggedNome = "";
                }
                try
                {
                    loggedTelefono = user["Telefono"].ToString();
                }
                catch (Exception)
                {
                    loggedTelefono = "";
                }
                loggedEmail = user["Email"].ToString();
                loggedRole = user["Role"].ToString();

                MessageBox.Show($"Benvenuto, {user["Nome Visualizzato"]}!");
                txt_L_Email.Clear();
                txt_L_Password.Clear();
                Login = true;
                Pannello_Login.Visible = false;
                Pannello_Login.Enabled = false;
                Pannello_Posti.Visible = false;
                Pannello_Acc_User.Enabled = true;
                Pannello_Principale.Visible = true;
            }
            else
            {
                MessageBox.Show("Email/Username/Telefono o Password errati.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FormClose_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
        }

        private void txt_codicefiscale_TextChanged(object sender, EventArgs e)
        {
            string nome = txt_nome.Text;
            string cognome = txt_cognome.Text;
            string dataNascita = dtp_nascita.Text;
            string sesso = "";
            if (Female_Rdb.Checked)
            {
                sesso = "F";
            }
            else if (Male_Rdb.Checked)
            {
                sesso = "M";
            }
            string comune = Comuni_Lst.SelectedItem.ToString();


            if (string.Equals(txt_codicefiscale.Text, CalcCodiceFiscale(nome.ToUpper(), cognome.ToUpper(), dataNascita, sesso.ToUpper(), comune), StringComparison.OrdinalIgnoreCase))
            {
                txt_codicefiscale.BackColor = Color.PaleGreen;
            }
            else
            {
                if (string.IsNullOrEmpty(txt_codicefiscale.Text))
                    txt_codicefiscale.BackColor = Color.White;
                else
                    txt_codicefiscale.BackColor = Color.IndianRed;
            }
        }

        private void Pgn_Register_MouseMove(object sender, MouseEventArgs e)
        {
            if (Comuni_Lst.SelectedIndex == -1 || (!Female_Rdb.Checked && !Male_Rdb.Checked))
            {
                txt_codicefiscale.Enabled = false;
            }
            else
            {
                txt_codicefiscale.Enabled = true;
            }
        }

        private void txt_confermapsw_TextChanged(object sender, EventArgs e)
        {
            if (txt_password.Text == txt_confermapsw.Text)
            {
                txt_confermapsw.BackColor = Color.PaleGreen;
                Lbl_ConfermaPsw.Text = "Conferma Password";
            }
            else
            {
                txt_confermapsw.BackColor = Color.IndianRed;
                Lbl_ConfermaPsw.Text = "Conferma Password - Le Password non Corrispondono";
            }
        }

        private void txt_L_Password_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Btn_Login(sender, e);
            }
        }

        private void Modifica_Vis_Name_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Logout_Click(object sender, EventArgs e)
        {
            Login = false;
            Pannello_Acc_User.Visible = false;
            Pannello_Acc_User.Enabled = false;
            Pannello_Login.Visible = true;
            Pannello_Login.Enabled = true;

            loggedNome = "";
            loggedTelefono = "";
            loggedEmail = "";
            loggedRole = "";
        }

        private void Lbl_register_Click(object sender, EventArgs e)
        {
            Tab_Login_Register.SelectedIndex = 1;
        }

        private void Chk_IsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_IsAdmin.Checked)
            {
                lbl_admin.Visible = true;
                txb_psw_admin.Visible = true;
                btn_Admin_Check.Visible = true;
            }
            else
            {
                lbl_admin.Visible = false;
                txb_psw_admin.Visible = false;
                btn_Admin_Check.Visible = false;
            }
        }

        private void btn_Admin_Check_Click(object sender, EventArgs e)
        {
            PswAdmin();
        }

        private bool PswAdmin()
        {
            string savedPassword = LeggiAdminPassword();
            string inputPassword = txb_psw_admin.Text;

            if (savedPassword != null && savedPassword == inputPassword)
            {
                Chk_IsAdmin.Checked = true;
                lbl_admin.Visible = false;
                txb_psw_admin.Visible = false;
                btn_Admin_Check.Visible = false;
                return true;
            }
            else
            {
                Chk_IsAdmin.Checked = false;
                return false;
            }
        }

        private void Modifica_Info(object sender, EventArgs e)
        {
            string InfodaModicare = ((Button)sender).Parent.Name;

            JArray jsonArray = JArray.Parse(File.ReadAllText(filePath));
            JObject user = jsonArray
                .Children<JObject>()
                .FirstOrDefault(u =>
                    (u["Email"]?.ToString() == loggedEmail));

            switch (InfodaModicare)
            {
                case "Nome":
                    user["Nome"] = txt_A_Nome.Text;
                    loggedNome = txt_A_Nome.Text;
                    break;
                case "Email":
                    user["Email"] = txt_A_Email.Text;
                    loggedEmail = txt_A_Email.Text;
                    break;
                case "Telefono":
                    user["Telefono"] = txt_A_Telefono.Text;
                    loggedTelefono = txt_A_Telefono.Text;
                    break;
                case "Password":
                    Pnl_ModPswVer.Visible = true;
                    break;
            }

            File.WriteAllText(filePath, jsonArray.ToString());
        }

        private void Btn_CkPsw_Click(object sender, EventArgs e)
        {

            JArray jsonArray = JArray.Parse(File.ReadAllText(filePath));
            JObject user = jsonArray
                .Children<JObject>()
                .FirstOrDefault(u =>
                    (u["Email"]?.ToString() == loggedEmail));

            if (user["Password"]?.ToString() == MascheraPassword(txt_VerPsw.Text))
            {
                txt_VerPsw.BackColor = Color.PaleGreen;
                user["Password"] = MascheraPassword(txt_A_Password.Text);
                Thread.Sleep(200);
                Pnl_ModPswVer.Visible = false;
                txt_VerPsw.Clear();
                txt_VerPsw.BackColor = Color.White;
            }
            else
            {
                txt_VerPsw.BackColor = Color.IndianRed;
                txt_VerPsw.Clear();
            }

            File.WriteAllText(filePath, jsonArray.ToString());
        }

        private void button155_Click(object sender, EventArgs e)
        {

        }
    }
}
