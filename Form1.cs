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
            cf += (consonanti.Length >= 4) ? $"{consonanti[0]}{consonanti[2]}{consonanti[3]}" :
                  (consonanti + vocali + "XXX").Substring(0, 3);

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

            // CODICE BELFIORE
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
                {'K', 1}, {'L', 0}, {'M', 5}, {'N', 7}, {'O', 9}, {'P', 13}, {'Q', 15}, {'R', 17}, {'S', 19}, {'T', 21},
                {'U', 1}, {'V', 0}, {'W', 5}, {'X', 7}, {'Y', 9}, {'Z', 13}
            };
            cf = cf.ToUpper();
            int somma = 0;
            for (int i = 0; i < 15; i++)
            {
                char c = cf[i];
                if (i % 2 == 0)
                    somma += Pari[c];
                else
                    somma += Dispari[c];
            }

            cf += (char)('A' + (somma % 26));
            cf = cf.ToLower();

            return cf;
        }

        public Form1()
        {
            InitializeComponent();
            ComuniITA();
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
        int posti;
        private void PostoSelezionato_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.BackColor == Color.Yellow)
            {
                switch (btn.Tag.ToString())
                {
                    case "Normal":
                        btn.BackColor = Color.LightSalmon;
                        PostiSelezionati--;
                        break;
                    case "Senior":
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

        private void Tab_Info_Posti_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void Pannello_Posti_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Pgn_Register_Click(object sender, EventArgs e)
        {

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
            else if(Male_Rdb.Checked)
            {
                sesso = "M";
            }
            string comune = Comuni_Lst.SelectedItem.ToString();

            txt_codicefiscale.Text = CalcCodiceFiscale(nome, cognome, dataNascita, sesso, comune);

            //if(string.Equals(txt_codicefiscale.Text, CalcCodiceFiscale(nome, cognome, dataNascita, sesso, comune), StringComparison.OrdinalIgnoreCase))
            //{
            //    txt_codicefiscale.BackColor = Color.Green;
            //}
            //else
            //{
            //    txt_codicefiscale.BackColor = Color.Red;
            //}
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
