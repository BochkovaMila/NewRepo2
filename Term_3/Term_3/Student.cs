using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Term_3
{
    [Serializable]
    public class Student : Person, INotifyPropertyChanged
    {
        private Education educat;
        private int group;
        private List<Exam> examstaken = new List<Exam>(); //для списка экзаменов
        private List<Test> tests = new List<Test>(); //хранится список зачетов
        public event PropertyChangedEventHandler PropertyChanged; // событие

        public Student(Person newstudent, Education neweducation, int newgroup)//конструктор c параметрами
        {
            name = newstudent.Name;
            surname = newstudent.Surname;
            birthday = newstudent.Birthday;
            educat = neweducation;
            group = newgroup;
            examstaken.Add(new Exam());
            tests.Add(new Test());
        }
        public Student() : this(new Person(), Education.Specialist, 123) { }
        public Person StudentData
        {
            get
            {
                return new Person(name, Surname, birthday);
            }
            set
            {
                name = value.Name;
                surname = value.Surname;
                birthday = value.Birthday;
            }
        }

        public double GetAverageScore //средний балл как среднее значение оценок в списке сданных экзаменов
        {
            get
            {
                double averagescore = 0;
                foreach (object i in examstaken)
                {
                    Exam exam = i as Exam;
                    averagescore += exam.Grade;
                }
                return averagescore / examstaken.Count;
            }
        }

        public List<Exam> ExamsList //для доступа к полю со списком экзаменов
        {
            get => examstaken;
            set
            {
                examstaken = value;
            }
        }
        public void AddExams(params Exam[] addedexams)//для добавления элементов в список экзаменов
        {
            if (examstaken == null)
            {
                examstaken = new List<Exam>();
            }
            examstaken.AddRange(addedexams);
        }
        public void AddTests(params Test[] addedtests)//для добавления элементов в список экзаменов
        {
            if (tests == null)
            {
                tests = new List<Test>();
            }
            tests.AddRange(addedtests);
        }

        public Education StudentEducat
        {
            get
            {
                return educat;
            }
            set
            {
                educat = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Education changed")); 
            }
        }
        public int Group
        {
            get
            {
                return group;
            }
            set
            {
                if (value <=100 || value >599)
                {
                    throw new ArgumentException("Group number can be > 100 or <= 599");
                }
                group = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Group changed"));
            }
        }
        public bool this[Education if_matches] //индексатор булевского типа
        {
            get
            {
                return educat == if_matches;
            }
        }

        public override string ToString() // строкa со значениями всех полей класса, включая список зачетов и экзаменов
        {
            string examsstring = "";
            foreach (var i in examstaken)
            {
                Exam exam = i as Exam;
                examsstring += exam.Subject + " " + exam.Grade.ToString() + " " + exam.ExamDate.ToShortDateString() + " ";
            }
            string teststring = "";
            foreach (var i in tests)
            {
                Test test = i as Test;
                if (test.pass_or_fail == true)
                {
                    teststring += test.subject_name + " is passed; ";
                }
                else
                {
                    teststring += test.subject_name + " is failed; ";
                }
                
            }
            return name.ToString() + " " + surname.ToString() + " " + birthday.ToShortDateString() + " " + educat.ToString() + " " + group.ToString() + " " + examsstring + teststring;
        }
        public virtual string ToShortString() //со значением среднего балла
        {
            return name.ToString() + " " + surname.ToString() + " " + birthday.ToString() + " " + educat.ToString() + " " + group.ToString() + " " + GetAverageScore.ToString();
        }
        public override object DeepCopy()
        {
            Student student = new Student();
            student.name = name;
            student.surname = surname;
            student.birthday = birthday;
            student.educat = educat;
            student.group = group;
            student.examstaken = new List<Exam>();
            foreach (Exam ex in examstaken)
            {
                student.examstaken.Add((Exam)ex.DeepCopy());
            }
            student.tests = new List<Test>();
            foreach (Test test in tests)
            {
                student.tests.Add((Test)test.DeepCopy());
            }
            return student;
        }
        public void PrintExams()
        {
            foreach(var ex in examstaken)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public object TDeepCopy() //для создания полной копии объекта с использованием сериализации
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;
                return formatter.Deserialize(ms);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Student();
            }
            finally
            {
                ms?.Close();
            }
        }
        public bool Save(string filename) //для сохранения данных объекта в файле с помощью сериализации
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, this);
                }
                return true;
            }
            catch
            {
                Console.WriteLine("Save to file " + filename + " failed");
                return false;
            }
            finally
            {
                Console.WriteLine("Finished saving");
            }
        }
        public bool Load(string filename) //для инициализации объекта данными из файла с помощью десериализации
        {
            try
            {
                Student student;
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    student = (Student)formatter.Deserialize(fs);
                }
                educat = student.StudentEducat;
                group = student.Group;
                examstaken = new List<Exam>(student.examstaken); 
                tests = new List<Test>(student.tests); 
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                Console.WriteLine("Finished loading");
            }
        }
        public bool AddFromConsole() // для добавления в один из списков класса нового элемента, данные для которого вводятся с консоли
        {
            Console.Write("Enter exam info in format:\nSubject, Grade, Exam Date (year month day) \nusing these separators '-' ' ' ',' ';'\n");
            string output = Console.ReadLine();
            string[] results = output.Split('-', ' ', ';', ',');
            if (results.Length != 5)
            {
                Console.WriteLine("Invalid input");
                return false;
            }
            else
            {
                try
                {
                    Exam exam = new Exam();
                    exam.Subject = results[0];
                    exam.Grade = Int32.Parse(results[1]);
                    exam.ExamDate = new DateTime(Int32.Parse(results[2]), Int32.Parse(results[3]), Int32.Parse(results[4]));
                    this.AddExams(exam);
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        public static bool Save(string filename, Student obj) // для сохранения объекта в файле с помощью сериализации
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, obj);
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                Console.WriteLine("Finished static saving");
            }
        }
        public static bool Load(string filename, Student obj) //для восстановления объекта из файла с помощью десериализации
        {
            try
            {
                Student student;
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    student = (Student)formatter.Deserialize(fs);
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                Console.WriteLine("Finished static loading");
            }
        }
        public void SortExamsBySubject() //по названию предмета
        {
            examstaken.Sort();
        }
        public void SortExamsByGrade() //по оценке
        {
            examstaken.Sort(new Exam());
        }
        public void SortExamsByExamDate() //по дате экзамена
        {
            examstaken.Sort(new ExamComparerByExamDate());
        }
        public IEnumerable Iterations(object[] obj) //последовательный перебор всех элементов из списков зачетов и экзаменов
        {
            foreach (var o in obj)
            {
                yield return o;
            }
        }
        public IEnumerable ExamsWithHigherGrades(int grade) //перебор экзаменов с оценкой больше заданного значения
        {
            foreach (object exam in examstaken)
            {
                var ex = exam as Exam;
                if (ex.Grade > grade)
                {
                    yield return ex;
                }
            }
        }
        public IEnumerable ExamsAndTests()//перебор сданных зачетов и экзаменов
        {
            HashSet<string> test_subjects = new HashSet<string>();
            foreach (var test in tests)
            {
                var t = test as Test;
                test_subjects.Add(t.subject_name);
            }
            HashSet<string> exam_subjects = new HashSet<string>();
            foreach (var exam in examstaken)
            {
                var e = exam as Exam;
                exam_subjects.Add(e.Subject);
            }
            test_subjects.IntersectWith(exam_subjects);
            foreach (object subject in test_subjects)
            {
                yield return subject;
            }
        }
        public IEnumerable PassedExamsAndTests()//перебор всех сданных зачетов, для которых сдан и экзамен
        {
            HashSet<string> test_subjects = new HashSet<string>();
            foreach (var test in tests)
            {
                var t = test as Test;
                if (t.pass_or_fail == true)
                {
                    test_subjects.Add(t.subject_name);
                }
            }
            HashSet<string> exam_subjects = new HashSet<string>();
            foreach (var exam in examstaken)
            {
                var e = exam as Exam;
                if (e.Grade > 2)
                {
                    exam_subjects.Add(e.Subject);
                }
            }
            test_subjects.IntersectWith(exam_subjects);
            foreach (object subject  in test_subjects)
            {
                yield return subject;
            }

        }
        public IEnumerable PassedExamsOrTests()//перебор сданных зачетов и экзаменов
        {
            HashSet<string> test_subjects = new HashSet<string>();
            foreach (var test in tests)
            {
                var t = test as Test;
                if (t.pass_or_fail == true)
                {
                    test_subjects.Add(t.subject_name);
                }
            }
            HashSet<string> exam_subjects = new HashSet<string>();
            foreach (var exam in examstaken)
            {
                var e = exam as Exam;
                if (e.Grade > 2)
                {
                    exam_subjects.Add(e.Subject);
                }
            }
            test_subjects.UnionWith(exam_subjects);
            foreach (object subject in test_subjects)
            {
                yield return subject;
            }
        }
        public IEnumerator GetEnumerator()
        {
            return new StudentEnumenator(examstaken, tests);
        }
    }

    class StudentEnumenator : IEnumerator
    {
        private int position;
        private List<Exam> examstaken;
        private List<Test> tests;

        public StudentEnumenator(List<Exam> new_exams, List<Test> new_tests)
        {
            position = -1;
            examstaken = new_exams;
            tests = new_tests;
        }
        public object Current
        {
            get
            {
                if (position == -1 || position >= examstaken.Count)
                {
                    throw new InvalidOperationException();
                }
                return examstaken[position];
            }
        }

        public bool MoveNext()
        {
            if (position < examstaken.Count - 1)
            {
                position++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
