using System;
using System.Collections.Generic;
using System.Text;

namespace Term_3
{
    [Serializable]
    public class Exam : IDateAndCopy, IComparable, IComparer<Exam>
    {
        public string Subject;
        public int Grade;
        public DateTime ExamDate;

        public DateTime Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Exam(string subject, int grade, DateTime date)
        {
            Subject = subject;
            Grade = grade;
            ExamDate = date;
        }
        public Exam() : this("Maths", 5, new DateTime(2021, 1, 1)) { }
        public override string ToString() // формирование строки со значениями всех свойств класса
        {
            return Subject + " " + Grade + " " + ExamDate.ToShortDateString();
        }
        internal object DeepCopy()
        {
            return new Exam { Subject = this.Subject, Grade = this.Grade, ExamDate = this.ExamDate };
        }

        object IDateAndCopy.DeepCopy()
        {
            throw new NotImplementedException();
        }
        public int CompareTo(object obj) //для сравнения объектов типа Exam по названию предмета
        {
            var exam = obj as Exam;
            if (exam.Subject != null) return Subject.CompareTo(exam.Subject);
            throw new Exception("Subject is null");
        }
        public int Compare(Exam ex1, Exam ex2) // для сравнения объектов типа Exam по оценке
        { 
            return ex1.Grade.CompareTo(ex2.Grade);
        }
    }
    // сравнениe объектов типа Exam по дате экзамена
    class ExamComparerByExamDate : IComparer<Exam>
    {
        public int Compare(Exam x, Exam y)
        {
            if (x == null && y == null) return 0;
            else if (x == null) return -1;
            else if (y == null) return -1;
            if (x.ExamDate > y.ExamDate) return 1;
            if (y.ExamDate > x.ExamDate) return -1;
            return 0;
        }
    }
}
