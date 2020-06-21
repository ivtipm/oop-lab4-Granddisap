using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD
{
    public class SpravFile
    {
        string name; // имя человека
        string email; // почта
        ulong phone; // номер телефона
        string city; // город
        ushort id; // его id 

        public SpravFile(ushort id, string name, string email, ulong phone, string city)
        {
            if ((name == "") || (email == "") || (city == ""))
                throw new Exception("Не все поля заполнены!");
            this.name = name;
            this.email = email;
            this.city = city;
            if (phone > 99999999999)
                throw new Exception("Неверно указан номер телефона!");
            this.phone = phone;
            this.id = id;
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public ulong Phone
        {
            get
            {
                return phone;
            }

            set
            {
                phone = value;
            }
        }

        public string City
        {
            get
            {
                return city;
            }

            set
            {
                city = value;
            }
        }

        public ushort Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public override string ToString()
        {
            return id + "|" + name + "|" + email + "|" +
                phone + "|" + city;
        }
    }
}
