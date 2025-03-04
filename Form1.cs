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
    public partial class search : Form
    {
        private List<string> items;
        public search()
        {
            InitializeComponent();
            items = new List<string>
        {
            "Apple",
            "Banana",
            "Cherry",
            "Date",
            "Fig",
            "Grape",
            "Kiwi",
            "Lemon",
            "Mango",
            "Orange"
        };

            // Configura il DataGridView
            //Search_results.Columns.Add("Item", "Item");
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cabaretToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void concertiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void altreManifestazioniToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void musicalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            //string searchTerm = txt_search.Text.ToLower();
          //  var results = items.Where(item => item.ToLower().Contains(searchTerm)).ToList();

            // Pulisci il DataGridView
          //  Search_results.Rows.Clear();

            // Aggiungi i risultati al DataGridView
           // foreach (var result in results)
            {
             //   Search_results.Rows.Add(result);
            }
        }
    }
}
