using System;

namespace Term_3
{
    public class StudentChangedEventArgs<TKey> : EventArgs
    {
        public string CollectionName { get; set; } // свойство с названием коллекции
        public Action Action { get; set; } // с информацией о том, чем вызвано событие
        public string PropertyName { get; set; } //с названием свойства класса Student, которое является источником изменения данных элемента
        public TKey Key { get; set; } //с ключом добавленного, удаленного или измененного элемента

        public StudentChangedEventArgs(string CollectionNameValue, Action ActionValue, string PropertyNameValue, TKey KeyValue)
        {
            CollectionName = CollectionNameValue;
            Action = ActionValue;
            PropertyName = PropertyNameValue;
            Key = KeyValue;
        }
        public override string ToString()
        {
            string outputString = "Collection Name: " + CollectionName + "\n";
            outputString += "Action: " + Action.ToString() + "\nProperty: " + PropertyName + "\nKey: "+Key.ToString();
            return outputString;
        }
    }
}
