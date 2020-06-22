using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.IO;

namespace BD
{
    public class dataWork
    {
        ArrayList spFiles = new ArrayList();

        public ArrayList SpFiles
        {
            get
            {
                return spFiles;
            }
        }

        public void AddPeopleFile(SpravFile spFile)
        {
            spFiles.Add(spFile);
        }

        public void DeletePeople() => spFiles.Clear();

        public void DeleteSpravFile(int number) => spFiles.RemoveAt(number);

        public void ChangePeopleName(string nameC, int index)
        {
            SpravFile sprav = (SpravFile)SpFiles[index];
            sprav.Name = nameC;
        }

        public void ChangeEmail(string emailC, int index)
        {
            SpravFile sprav = (SpravFile)SpFiles[index];
            sprav.Email = emailC;
        }

        public void ChangePhone(ulong phoneC, int index)
        {
            SpravFile sprav = (SpravFile)SpFiles[index];
            if (phoneC>99999999999)
                throw new Exception("Неверно указан номер телефона!");
            sprav.Phone = phoneC;
        }

        public void ChangeCity(string cityC, int index)
        {
            SpravFile sprav = (SpravFile)SpFiles[index];
            sprav.City = cityC;
        }

        public void SaveFile(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.Unicode))
            {
                foreach (SpravFile s in spFiles)
                {
                    sw.WriteLine(s.ToString());
                }
            }
        }

        public void OpenFile(string filename)
        {
            if (!System.IO.File.Exists(filename))
                throw new Exception("Файл не существует");
            if (spFiles.Count != 0)
                DeletePeople();
            using (StreamReader sw = new StreamReader(filename))
            {
                while (!sw.EndOfStream)
                {
                    string str = sw.ReadLine();
                    String[] dataFromFile = str.Split(new String[] { "|" },
                        StringSplitOptions.RemoveEmptyEntries);
                    ushort id = (ushort)Convert.ToInt32(dataFromFile[0]);
                    string nameC = dataFromFile[1];
                    string emailC = dataFromFile[2];
                    ushort phoneC = (ushort)Convert.ToInt32(dataFromFile[3]);
                    string cityC = dataFromFile[4];
                    SpravFile spFile = new SpravFile(id, nameC, emailC, phoneC, cityC);
                    AddPeopleFile(spFile);
                }
            }
        }

        public List<int> SearchSpravFile(string query)
        {
            List<int> count = new List<int>();
            ushort num_query;
            if (ushort.TryParse(query, out num_query))
            {
                for (int i = 0; i < spFiles.Count; i++)
                {
                    SpravFile sprav = (SpravFile)spFiles[i];
                    if (sprav.Id == num_query)
                    {
                        count.Add(i);
                        break;
                    }
                    else
                    {
                        if (sprav.Phone == num_query)
                            count.Add(i);
                    }
                }
                if (count.Count == 0)
                    count.Add(-1);
                return count;
            }
            query = query.ToLower();
            query = query.Replace(" ", "");
            for (int i = 0; i < spFiles.Count; i++)
            {
                SpravFile sprav = (SpravFile)spFiles[i];
                if (sprav.Name.ToLower().Replace(" ", "").Contains(query))
                    count.Add(i);
                else
                    if (sprav.Email.ToLower().Replace(" ", "").Contains(query))
                    count.Add(i);
                else
                    if (sprav.City.ToLower().Replace(" ", "").Contains(query))
                    count.Add(i);
            }
            if (count.Count == 0)
                count.Add(-1);
            return count;
        }

        public void Sort(SortDirection sortDirection)
        {
            spFiles.Sort(new Comparison(sortDirection));
        }

        public static implicit operator dataWork(string v)
        {
            throw new NotImplementedException();
        }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public class Comparison : IComparer
    {
        private SortDirection m_direction = SortDirection.Ascending;

        public Comparison() : base() { }

        public Comparison(SortDirection direction)
        {
            this.m_direction = direction;
        }

        int IComparer.Compare(object x, object y)
        {
            SpravFile sprav1 = (SpravFile)x;
            SpravFile sprav2 = (SpravFile)y;

            return (this.m_direction == SortDirection.Ascending) ?
                sprav1.Id.CompareTo(sprav2.Id) :
                sprav2.Id.CompareTo(sprav1.Id);
        }
    }
}
