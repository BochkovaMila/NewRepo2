using System;
using System.Collections.Generic;
using System.Text;

namespace Term_3
{
    [Serializable]
    public class Person
    {
        protected string name;
        protected string surname;
        protected DateTime birthday;
        public Person(string nameValue, string surnameValue, DateTime birthValue)
        {
            name = nameValue;
            surname = surnameValue;
            birthday = birthValue;
        }
        public Person() : this("Ivan", "Ivanov", new DateTime(2000, 1, 1)) { }
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
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }
        public DateTime Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                birthday = value;
            }
        }
        public int BirthYear
        { 
            get
            {
                return birthday.Year;
            }
            set
            {
                birthday = new DateTime(value, birthday.Month, birthday.Day);
            }
        }

        //public DateTime Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override string ToString() //формирование строки со значениями всех полей класса
        {
            return name + " " + surname + " " + birthday.ToShortDateString();
        }
        public virtual string ToShortString()// возвращает строку, содержащую только имя и фамилию
        {
            return name + " " + surname;
        }
        public override bool Equals(object obj) // объекты считались равными, если равны все данные объектов
        {
            //Check for null and compare run-time types
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Person p = (Person) obj;
                return (name == p.name) && (surname == p.surname) && (birthday == p.birthday);
            }
        }
        public static bool operator ==(Person p1, Person p2) // определение операции ==
            => p1.Equals(p2);
        public static bool operator !=(Person p1, Person p2) // определение операции !=
            => ! p1.Equals(p2);
        public override int GetHashCode() 
        {
            return name.GetHashCode() + surname.GetHashCode() + birthday.GetHashCode();
        }
        public virtual object DeepCopy() //создает полную копию объекта
        {
            return new Person { name = this.name, surname = this.surname, birthday = this.birthday };
        }

    }
}
