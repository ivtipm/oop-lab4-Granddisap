using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD
{
    public partial class SearchForm : Form
    {
        dataWork data = new dataWork();
        public SearchForm()
        {
            InitializeComponent();        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();

            if ((data.SpFiles.Count == 0) || (textBox1.Text == ""))
                return;
            frm.dataGridView1.ClearSelection();
            List<int> foundElements = data.SearchSpravFile(textBox1.Text);
            if (foundElements[0] == -1)
            {
                MessageBox.Show("Ничего не удалось найти!");
                return;
            }
            frm.dataGridView1.CurrentCell = frm.dataGridView1[0, foundElements[0]];
            for (int i = 0; i < foundElements.Count; i++)
            {
                frm.dataGridView1.Rows[foundElements[i]].Selected = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
