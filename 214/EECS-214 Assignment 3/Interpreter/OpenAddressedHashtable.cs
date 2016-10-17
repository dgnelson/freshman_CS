using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class OpenAddressedHashtable : Dictionary
    {
        int count = 0;
        int size = 0;
        string[] keys;
        object[] vals;
        public OpenAddressedHashtable(int s)
        {
            size = s;
            keys = new string[s];
            vals = new object[s];
        }

        private int hash(string s) {
            int k = 0;
            for (int i = 0; i < s.Length; i++) 
                k = k + s[i];
            double a = ((Math.Sqrt(5) - 1.0) / 2.0);
            return ((int)(size * ((k * a) % 1)));
        }

        public override void Store(string name, object value)
        {
            if(count == size)
                throw new HashtableFullException();
            int num = hash(name);
            bool done = false;
            for (int i = num; num < size; num++) {
                if (keys[i] == null) {
                    keys[i] = name;
                    vals[i] = value;
                    count++;
                    done = true;
                    break;
                }
                else if (keys[i] == name)
                {
                    vals[i] = value;
                    done = true;
                    break;
                }
            }
            if (!done){
                for (int i = 0; i < num; i++)
                {
                    if (keys[i] == null)
                    {
                        keys[i] = name;
                        vals[i] = value;
                        count++;
                        done = true;
                        break;
                    }
                    else if (keys[i] == name)
                    {
                        vals[i] = value;
                        done = true;
                        break;
                    }
                }
                if (!done)
                    throw new HashtableFullException();
            }
        }

        public override object Lookup(string name)
        {
            int num = hash(name);
            for (int i = num; i < size; i++) {
                if (keys[i] == null)
                    throw new DictionaryKeyNotFoundException(name);
                if (keys[i] == name)
                    return vals[i];
            }
            for (int i = 0; i < num; i++) {
                if (keys[i] == null)
                    throw new DictionaryKeyNotFoundException(name);
                if (keys[i] == name)
                    return vals[i];
            }
            throw new DictionaryKeyNotFoundException(name);
        }

        public override int Count { 
            get{
                return count;
            } 
        
        }

        public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}
