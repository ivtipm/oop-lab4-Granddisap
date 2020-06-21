using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections;

namespace BD
{
    public partial class AddForm : Form
    {

        dataWork data = new dataWork();
        public AddForm()
        {
            InitializeComponent();
        }

        public ushort generateID()
        {
            Random r = new Random();
            int id = r.Next(0, 1000);
            for (int i = 0; i < data.SpFiles.Count; i++)
            {
                SpravFile sprav = (SpravFile)data.SpFiles[i];
                if (sprav.Id == id)
                {
                    i = 0;
                    id = r.Next(0, 1000);
                }
            }
            return (ushort)id;
        }

        private void AddForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string nameC = textBox1.Text;
                string emailC = textBox2.Text;
                ushort phoneC = (ushort)Convert.ToInt32(textBox3.Text);
                string cityC = textBox4.Text;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                SpravFile spFile = new SpravFile(generateID(), nameC, emailC, phoneC, cityC);
                data.AddPeopleFile(spFile);
                int n = data.SpFiles.Count;
                SpravFile sprav = (SpravFile)data.SpFiles[n - 1];
                Form1 frm = this.Owner as Form1;
                frm.dataGridView1.Rows.Add(sprav.Id, nameC, emailC, phoneC, cityC);
                frm.BanChangeColumn(n - 1);              
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка: {exception.Message}");
            }
        }
    }
}
