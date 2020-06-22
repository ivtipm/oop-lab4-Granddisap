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

namespace BD
{
    public partial class Form1 : Form
    {
        dataWork data = new dataWork();
        string filename = "";
        string resultfn;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Rows[0].ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            ToolTip tip = new ToolTip();
            tip.SetToolTip(button1, "Сортировать по ID.");
            tip.SetToolTip(button3, "Поиск человека по ID, имени, почте, телефону или городу.");
            tip.SetToolTip(textBox1, "Поле ввода имени.");
            tip.SetToolTip(textBox2, "Поле ввода почты.");
            tip.SetToolTip(textBox3, "Поле ввода мобильного телефона.");
            tip.SetToolTip(textBox4, "Поле ввода города.");
            tip.SetToolTip(button2, "Добавить человека.");
            tip.SetToolTip(dataGridView1, "Ваш справочник.");
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

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        public void BanChangeColumn(int index) =>
            dataGridView1.Rows[index].Cells[0].ReadOnly = true;

        

        public void WriteToDataGrid()
        {
            for (int i = 0; i < data.SpFiles.Count; i++)
            {
                SpravFile sprav = (SpravFile)data.SpFiles[i];
                dataGridView1.Rows.Add(sprav.Id, sprav.Name,
                    sprav.Email, sprav.Phone, sprav.City);
                BanChangeColumn(i);
            }
            dataGridView1.Rows[data.SpFiles.Count].ReadOnly = true;
        }

        private void удалитьВыделинноеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = dataGridView1.Rows.Count;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int index = row.Index;
                if (index == count - 1) return;
                data.DeleteSpravFile(index);
                dataGridView1.Rows.RemoveAt(index);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename == "")
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                filename = saveFileDialog1.FileName;
                resultfn = Path.GetFileName(filename);
                this.Text = resultfn + " - Справочник";
            }
            data.SaveFile(filename);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filename = openFileDialog1.FileName;
            resultfn = Path.GetFileName(filename);
            this.Text = resultfn + " - Справочник";
            dataGridView1.Rows.Clear();
            data.OpenFile(filename);
            WriteToDataGrid();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((data.SpFiles.Count != 0) || (filename != ""))
            {
                DialogResult dialogResult = MessageBox.Show("Уверены," +
                    "что хотите создать новый файл?" + "\r\n" +
                    "Изменения в текущем не сохранятся!", "Подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    this.Text = "Справочник";
                    filename = "";
                    data.DeletePeople();
                    dataGridView1.Rows.Clear();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (data.SpFiles.Count != 0)
            {
                dataGridView1.Rows.Clear();
                if (comboBox1.SelectedIndex == 0)
                {
                    data.Sort(SortDirection.Ascending);
                }
                else
                {
                    data.Sort(SortDirection.Descending);
                }
                WriteToDataGrid();
            }
        }

        public void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (data.SpFiles.Count != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Уверены," +
                    "что хотите удалить все элементы?", "Подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    data.DeletePeople();
                    dataGridView1.Rows.Clear();
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                string nameC = textBox1.Text;
                string emailC = textBox2.Text;
                ulong phoneC = (ulong)Convert.ToInt32(textBox3.Text);
                string cityC = textBox4.Text;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                SpravFile spFile = new SpravFile(generateID(), nameC, emailC, phoneC, cityC);
                data.AddPeopleFile(spFile);
                int n = data.SpFiles.Count;
                SpravFile sprav = (SpravFile)data.SpFiles[n - 1];
                
                dataGridView1.Rows.Add(sprav.Id, nameC, emailC, phoneC, cityC);
                BanChangeColumn(n - 1);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка: {exception.Message}");
            }
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string info = "Выполнил: А.А. Долгов" + "\n\n" + "2020 г.";
            MessageBox.Show(info, "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((data.SpFiles.Count == 0) || (textBox5.Text == ""))
                return;
            dataGridView1.ClearSelection();
            List<int> foundElements = data.SearchSpravFile(textBox5.Text);
            if (foundElements[0] == -1)
            {
                MessageBox.Show("Ничего не удалось найти!");
                return;
            }
            dataGridView1.CurrentCell = dataGridView1[0, foundElements[0]];
            for (int i = 0; i < foundElements.Count; i++)
            {
                dataGridView1.Rows[foundElements[i]].Selected = true;
            }
        }
    }
}
