using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;

namespace Term_3
{
    public delegate TKey KeySelector<TKey>(Student st);
    public class StudentCollection<TKey>
    {
        private Dictionary<TKey, Student> collection = new Dictionary<TKey, Student>();
        private KeySelector<TKey> keys;

        public StudentChangedHandler<TKey> StudentsChanged; //происходит, когда в коллекцию добавляются элементы, из нее удаляется элемент или изменяются данные одного из ее элементов
        public string CollectionName { get; set; } //свойство с названием коллекции
        public StudentCollection(KeySelector<TKey> keysValue)
        {
            keys = keysValue;
        }
        private void StudentPropertyChanged(Action action, string Name, TKey key) //вызов события
        {
            StudentsChanged?.Invoke(this, new StudentChangedEventArgs<TKey>(CollectionName, action, Name, key));
        }
        private void HandleEvent(object sender, EventArgs eventArgs)
        {
            var it = (PropertyChangedEventArgs)eventArgs;
            var st = (Student)sender;
            var key = keys(st);
            StudentPropertyChanged(Action.Property, it.PropertyName, key);
        }
        public bool Remove(Student st)
        {
            //для удаления элемента со значением st из словаря Dictionary<TKey, Student>; если в словаре нет элемента st, метод возвращает значение false;
            bool flag = false;
            foreach (KeyValuePair<TKey, Student> pair in collection)
            {
                if (pair.Value == st)
                {
                    collection.Remove(pair.Key);
                    StudentPropertyChanged(Action.Remove, st.Name, pair.Key);
                    st.PropertyChanged -= HandleEvent;
                    flag = true;
                }
            }
            return flag;
            //var k = collection.FirstOrDefault(m => m.Value == st).Key;
            //if (k == null) return false;
            //collection.Remove(k);
            //StudentPropertyChanged(Action.Remove, "Removed", k);
            //st.PropertyChanged -= HandleEvent;
            //return true;
        }
        public void AddDefaults() //добавить некоторое число элементов типа Student для инициализации коллекции по умолчанию
        {
            var st = new Student();
            collection.Add(keys(st), st);
            st.PropertyChanged += HandleEvent;
        }
        public void AddStudents(params Student[] students) //для добавления элементов в коллекцию
        {
            foreach (var student in students)
            {
                collection.Add(keys(student), student);
                StudentPropertyChanged(Action.Add, student.Name, keys(student));
                student.PropertyChanged += HandleEvent;
            }
        }
        public double MaxAverageScore //возвращающее максимальное значение среднего балла для элементов
        {
            get
            {
                if (collection.Count == 0) return 0;
                return collection.Values.Max(mbox => mbox.GetAverageScore);
            }
        }
        public IEnumerable<KeyValuePair<TKey, Student>>EducationForm(Education value) //возвращающeт подмножество элементов коллекции с заданной формой обучения
        {
            return collection.Where(cElem => cElem.Value.StudentEducat == value);
        }
        public IEnumerable<IGrouping<Education, KeyValuePair<TKey, Student>>> GroupCollection //группировкa элементов коллекции в зависимости от формы обучения
        {
            get
            {
                return collection.GroupBy(st => st.Value.StudentEducat);
            }
        }
        public override string ToString() //формированиe строки, содержащей информацию обо всех элементах коллекции 
        {
            string str = $"This Collection has {collection.Count} students: \n";
            foreach (var st in collection.Values)
            {
                str += st.ToString();
            }
            return str;
        }
        public virtual string ToShortString() //без списка зачетов и экзаменов
        {
            string str = $"This Collection has {collection.Count} students: \n";
            foreach (var st in collection.Values)
            {
                str += st.ToShortString() + "\n";
            }
            return str;
        }

    }
}
