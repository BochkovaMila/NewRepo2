using System;

namespace Term_3
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Создать объект типа T с непустым списком элементов, для которого
            //предусмотрен ввод данных с консоли.Создать полную копию объекта с
            //помощью метода, использующего сериализацию, и вывести исходный
            //объект и его копию.
            Console.WriteLine("-----------------1ST ASSIGNMENT-----------------");
            Student firststudent = new Student(new Person("Masha", "Petrova", new DateTime(2001, 5, 5)), Education.Вachelor, 111);
            Exam[] exams = new Exam[3];
            exams[0] = new Exam("English", 4, new DateTime(2021, 9, 4));
            exams[1] = new Exam("Chemistry", 3, new DateTime(2021, 9, 7));
            exams[2] = new Exam("C#", 5, new DateTime(2021, 9, 19));
            firststudent.AddExams(exams);
            Student secondstudent = new Student();
            secondstudent = (Student)firststudent.TDeepCopy();
            Console.WriteLine("First student: ");
            Console.WriteLine(firststudent.ToString());
            Console.WriteLine("Second student: ");
            Console.WriteLine(secondstudent.ToString());
            Console.WriteLine("-----------------2ND ASSIGNMENT-----------------");
            /**
             *  Предложить пользователю ввести имя файла:
                если файла с введенным именем нет, приложение должно 
                сообщить об этом и создать файл;
                если файл существует, вызвать метод Load(string filename) для 
                инициализации объекта T данными из файла. 
            **/
            Console.WriteLine("Enter file name (students.bin)");
            string filename = Console.ReadLine();
            secondstudent.Load(filename);
            Console.WriteLine("-----------------3RD ASSIGNMENT-----------------");
            //Вывести объект
            Console.WriteLine("Loaded student:");
            Console.WriteLine(secondstudent.ToString());
            Console.WriteLine("-----------------4TH ASSIGNMENT-----------------");
            //Для этого же объекта T сначала вызвать метод AddFromConsole(), затем 
            //метод Save(string filename). Вывести объект T.
            secondstudent.AddFromConsole();
            secondstudent.Save(filename);
            Console.WriteLine("Saved student:");
            Console.WriteLine(secondstudent.ToString());
            Console.WriteLine("-----------------5TH ASSIGNMENT-----------------");
            /**Вызвать последовательно 
                статический метод Load( string filename, T obj), передав как параметры ссылку на тот же самый объект T и введенное ранее имя файла;
                метод AddFromConsole();
                статический метод Save (string filename, T obj).
                Вывести объект T.
            **/
            Student.Load(filename, firststudent);
            firststudent.AddFromConsole();
            Student.Save(filename, firststudent);
            Console.WriteLine(firststudent.ToString());
        }
    }
}
