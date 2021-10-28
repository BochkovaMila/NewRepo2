using System;
using System.Collections.Generic;
using System.Text;

namespace Term_3
{
    public class Journal
    {
        private List<JournalEntry> journalEntries = new List<JournalEntry>();
        public void JournalEntryHandler(object sender, EventArgs eventArgs) //обработчик события 
        {
            var info = eventArgs as StudentChangedEventArgs<string>;
            journalEntries.Add(new JournalEntry(info.CollectionName, info.Action, info.PropertyName, info.Key));
        }
        public override string ToString()
        {
            string outputString = "";
            foreach (var entry in journalEntries)
            {
                outputString += entry.ToString() + "\n";
            }
            return outputString;
        }
    }
}
