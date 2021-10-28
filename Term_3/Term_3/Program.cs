using System;

namespace Term_3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создать две коллекции StudentCollection<string> с разными названиями
            KeySelector<String> selector = delegate (Student student)
            {
                return student.GetHashCode().ToString();
            };
            StudentCollection<string> studentCollection1 = new StudentCollection<string>(selector);
            studentCollection1.CollectionName = "Student Collection 1";
            StudentCollection<string> studentCollection2 = new StudentCollection<string>(selector);
            studentCollection2.CollectionName = "Student Collection 2";

            Student st1 = new Student();
            Student st2 = new Student(new Person("Anna", "Fischer", new DateTime(1998, 6, 6)), Education.Specialist, 155);
            Student st3 = new Student(new Person("Holly", "Weber", new DateTime(1999, 7, 7)), Education.Вachelor, 156);
            Student st4 = new Student(new Person("Emily", "Meyer", new DateTime(2000, 8, 8)), Education.SecondEducation, 157);

            //  Создать объект Journal и подписать его на события StudentsChanged из обеих коллекций StudentCollection<string>
            Journal journal = new Journal();
            studentCollection1.StudentsChanged = journal.JournalEntryHandler;
            studentCollection2.StudentsChanged = journal.JournalEntryHandler;
            //st1.PropertyChanged += journal.JournalEntryHandler;
            //st2.PropertyChanged += journal.JournalEntryHandler;
            // добавить элементы Student в коллекции
            studentCollection1.AddStudents(st1, st3);
            studentCollection2.AddStudents(st2, st4);
            // изменить значения разных свойств элементов, входящих в коллекцию
            st1.StudentEducat = Education.SecondEducation;
            st2.Group = 150;
            // удалить элемент из коллекции
            studentCollection1.Remove(st1);
            // изменить данные в удаленном элементе
            st1.Group = 154;
            // Вывести данные объекта Journal
            Console.WriteLine(journal);
         }
    }
}
