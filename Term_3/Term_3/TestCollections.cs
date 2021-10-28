using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Term_3
{
    public delegate System.Collections.Generic.KeyValuePair<TKey, TValue>
        GenerateElement<TKey, TValue>(int k);

    public class TestCollections<TKey,TValue>
    {
        private List<TKey> tKeyList = new List<TKey>(); 
        private List<string> strList = new List<string>();
        private Dictionary<TKey, TValue> tKeyDictionary = new Dictionary<TKey, TValue>();
        private Dictionary<string, TValue> strDictionary = new Dictionary<string, TValue>();
        private GenerateElement<TKey, TValue> generateElement;

        public TestCollections(int size, GenerateElement<TKey, TValue> example) // конструктор для автоматической генерации элементов коллекций
        {
            generateElement = example;
            for (int i = 0; i < size; i++)
            {
                var element = generateElement(i);
                tKeyDictionary.Add(element.Key, element.Value);
                strDictionary.Add(element.Key.ToString(), element.Value);
                tKeyList.Add(element.Key);
                strList.Add(element.Key.ToString());
            }
        }
        public void findKeyList()
        {
            var first = tKeyList[0];
            var middle = tKeyList[tKeyList.Count / 2];
            var last = tKeyList[tKeyList.Count - 1];
            var notfound = generateElement(tKeyList.Count + 1).Key;

            var watch = Stopwatch.StartNew();
            tKeyList.Contains(first);
            watch.Stop();
            Console.WriteLine("In key list");
            Console.WriteLine("Time for the first element is " + watch.ElapsedTicks);

            watch.Restart();
            tKeyList.Contains(middle);
            watch.Stop();
            Console.WriteLine("Time for the middle element is " + watch.ElapsedTicks);

            watch.Restart();
            tKeyList.Contains(last);
            watch.Stop();
            Console.WriteLine("Time for the last element is " + watch.ElapsedTicks);

            watch.Restart();
            tKeyList.Contains(notfound);
            watch.Stop();
            Console.WriteLine("Time for the element not in list is " + watch.ElapsedTicks);
        }
        public void findStrList()
        {
            var first = strList[0];
            var middle = strList[strList.Count / 2];
            var last = strList[strList.Count - 1];
            var notfound = generateElement(strList.Count + 1).Key.ToString();

            var watch = Stopwatch.StartNew();
            strList.Contains(first);
            watch.Stop();
            Console.WriteLine("In string list");
            Console.WriteLine("Time for the first element is " + watch.ElapsedTicks);

            watch.Restart();
            strList.Contains(middle);
            watch.Stop();
            Console.WriteLine("Time for the middle element is " + watch.ElapsedTicks);

            watch.Restart();
            strList.Contains(last);
            watch.Stop();
            Console.WriteLine("Time for the last element is " + watch.ElapsedTicks);

            watch.Restart();
            strList.Contains(notfound);
            watch.Stop();
            Console.WriteLine("Time for the element not in list is " + watch.ElapsedTicks);
        }
        public void findTKeyDictionaryByKeys()
        {
            var first = tKeyDictionary.ElementAt(0).Key;
            var middle = tKeyDictionary.ElementAt(tKeyDictionary.Count/2).Key; 
            var last = tKeyDictionary.ElementAt(tKeyDictionary.Count-1).Key; 
            var notfound = generateElement(tKeyDictionary.Count + 1).Key;

            var watch = Stopwatch.StartNew();
            tKeyDictionary.ContainsKey(first);
            watch.Stop();
            Console.WriteLine("TKeyDictionary by Keys");
            Console.WriteLine("Time for the first element is " + watch.ElapsedTicks);

            watch.Restart();
            tKeyDictionary.ContainsKey(middle);
            watch.Stop();
            Console.WriteLine("Time for the middle element is " + watch.ElapsedTicks);

            watch.Restart();
            tKeyDictionary.ContainsKey(last);
            watch.Stop();
            Console.WriteLine("Time for the last element is " + watch.ElapsedTicks);

            watch.Restart();
            tKeyDictionary.ContainsKey(notfound);
            watch.Stop();
            Console.WriteLine("Time for the element not in list is " + watch.ElapsedTicks);
        }
        public void findTKeyDictionaryByValues()
        {
            var first = tKeyDictionary.ElementAt(0).Value;
            var middle = tKeyDictionary.ElementAt(tKeyDictionary.Count / 2).Value; 
            var last = tKeyDictionary.ElementAt(tKeyDictionary.Count - 1).Value; 
            var notfound = generateElement(tKeyDictionary.Count + 1).Value;

            var watch = Stopwatch.StartNew();
            tKeyDictionary.ContainsValue(first);
            watch.Stop();
            Console.WriteLine("TKeyDictionary by Values");
            Console.WriteLine("Time for the first element is " + watch.ElapsedTicks);

            watch.Restart();
            tKeyDictionary.ContainsValue(middle);
            watch.Stop();
            Console.WriteLine("Time for the middle element is " + watch.ElapsedTicks);

            watch.Restart();
            tKeyDictionary.ContainsValue(last);
            watch.Stop();
            Console.WriteLine("Time for the last element is " + watch.ElapsedTicks);

            watch.Restart();
            tKeyDictionary.ContainsValue(notfound);
            watch.Stop();
            Console.WriteLine("Time for the element not in list is " + watch.ElapsedTicks);
        }
    }
}
