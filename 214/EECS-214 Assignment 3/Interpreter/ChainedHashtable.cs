using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Node
    {
        KeyValuePair<string, object> p;
        public Node next;
        public Node prev;
        public Node(string s, object o)
        {
            p = new KeyValuePair<string, object>(s, o);
        }

        public void setNext(Node n)
        {
            next = n;
        }

        public Node getNext()
        {
            return next;
        }

        public void setPrev(Node n) {
            prev = n;
        }

        public Node getPrev()
        {
            return prev;
        }

        public void setPair(string s, object o)
        {
            p = new KeyValuePair<string, object>(s, o);
        }

        public KeyValuePair<string, object> getPair()
        {
            return p;
        }

        public string getKey()
        {
            return p.Key;
        }

        public object getValue()
        {
            return p.Value;
        }

    }

    
        public class ChainedHashtable : Dictionary
    {
        int count = 0;
        int size = 0;
        //LinkedList<KeyValuePair<string, object>>[] vals;
        Node[] vals;
        public ChainedHashtable(int s) {
            size = s;
            //vals = new LinkedList<KeyValuePair<string, object>>[size];
            vals = new Node[size];
            //for (int i=0; i < size; i++){
                //vals[i] = new LinkedList<KeyValuePair<string, object>>();
            //}
        }

        private int hash(string s)
        {
            int k = 0;
            for (int i = 0; i < s.Length; i++)
                k = k + s[i];
            double a = ((Math.Sqrt(5) - 1.0) / 2.0);
            return ((int)(size * ((k * a) % 1)));
        }

        public override void Store(string name, object value) {
            int num = hash(name);
            if (vals[num] == null)
            {
                vals[num] = new Node(name, value);
                
            }
            else {
                Node temp = vals[num];
                Node prev = null;
                bool done = false;
                while (temp != null) {
                    if (temp.getKey() == name)
                    {
                        temp.setPair(name, value);
                        done = true;
                    }
                    prev = temp;
                    temp = temp.getNext();
                }
                if (!done) {
                    prev.setNext(new Node(name, value));
                }
            }
            count++;
        }
   
        public override object Lookup(string name) {
            int num = hash(name);
            Node temp = vals[num];
            while (temp != null) {
                if (temp.getKey() == name)
                    return temp.getValue();
                temp = temp.getNext();
            }
            throw new DictionaryKeyNotFoundException(name);
        }

        public override int Count
        {
            get
            {
                return count;
            }

        }

        public override IEnumerator<KeyValuePair<string, object>> GetEnumerator() {
            throw new NotImplementedException();
        }

    }
}
