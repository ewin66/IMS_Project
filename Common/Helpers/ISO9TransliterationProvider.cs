using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers
{
    //public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
    //{
    //    TValue value;
    //    return dictionary.TryGetValue(key, out value) ? value : defaultValue;
    //}

    public class ISO9TransliterationProvider
    {
        private readonly Dictionary<Char, Char> cyrilicCharMapping = new Dictionary<char, char>() {
            { 'q', 'љ' }, 
            { 'w', 'њ' }, 
            { 'e', 'е' },
            { 'r', 'р' },
            { 't', 'т' },
            { 'y', 'ѕ' },
            { 'u', 'у' },
            { 'i', 'и' },
            { 'o', 'о' },
            { 'p', 'п' },
            { '[', 'ш' },
            { ']', 'ѓ' },
            { 'a', 'а' },
            { 's', 'с' },
            { 'd', 'д' },
            { 'f', 'ф' },
            { 'g', 'г' },
            { 'h', 'х' },
            { 'j', 'ј' },
            { 'k', 'к' },
            { 'l', 'л' },
            { ';', 'ч' },
            { '\'', 'ќ' },
            { '\\', 'ж' },
            { 'z', 'з' },
            { 'x', 'џ' },
            { 'c', 'ц' },
            { 'v', 'в' },
            { 'b', 'б' },
            { 'n', 'н' },
            { 'm', 'м' },
            { ',', ',' },
            { '.', '.' }
        //enc.
        };



        public string ToLatin(string cyrillic)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in cyrillic)
                result.Append(cyrilicCharMapping[c]);
            return result.ToString();
        }

        public string ToMK(string latin)
        {
            StringBuilder result = new StringBuilder();
            bool success = false;
            char outChar;
            foreach (char c in latin){
                success = cyrilicCharMapping.TryGetValue(c, out outChar);
                result.Append(success ? outChar : c);
            }
            return result.ToString();
        }
    }
}
