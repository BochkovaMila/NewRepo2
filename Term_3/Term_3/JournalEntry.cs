namespace Term_3
{
    public class JournalEntry
    {
        public string CollectionName { get; set; } //с названием коллекции
        public Action ActionType { get; set; } //c информацией о типе события
        public string ChangedProperty { get; set; } // с названием свойства класса Student, которое явилось причиной изменения данных элемента
        public string StrKey { get; set; } //с текстовым представлением ключа добавленного, удаленного или измененного элемента
        public JournalEntry(string CollectionNameValue, Action ActionTypeValue, string ChangedPropertyValue, string StrKeyValue)
        {
            CollectionName = CollectionNameValue;
            ActionType = ActionTypeValue;
            ChangedProperty = ChangedPropertyValue;
            StrKey = StrKeyValue;
        }
        public override string ToString()
        {
            string outputString = "Collection name: " + CollectionName + "\n Action: " + ActionType.ToString() + "\nChanged Property: " + ChangedProperty + "\nKey: " + StrKey;
            return outputString;
        }
    }
}
