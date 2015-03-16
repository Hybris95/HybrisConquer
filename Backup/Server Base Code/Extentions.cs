using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    static public class Extentions
    {
        static public void ThreadSafeAdd<T, T2>(this Dictionary<T, T2> dictionary, T Key, T2 Value)
        {
            lock (dictionary)
            {
                if (dictionary.ContainsKey(Key))
                    dictionary[Key] = Value;
                else
                    dictionary.Add(Key, Value);
            }
        }
        static public void ThreadSafeRemove<T, T2>(this Dictionary<T, T2> dictionary, T Key)
        {
            lock (dictionary)
            {
                dictionary.Remove(Key);
            }
        }
        static public T2[] ThreadSafeValueArray<T, T2>(this Dictionary<T, T2> dictionary)
        {
            lock (dictionary)
            {
                T2[] Values = new T2[dictionary.Count];
                dictionary.Values.CopyTo(Values, 0);
                return Values;
            }
        }
    }
}
