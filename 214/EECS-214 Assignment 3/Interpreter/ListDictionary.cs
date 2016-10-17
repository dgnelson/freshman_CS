using System;
using System.Collections.Generic;

namespace Interpreter
{
    
    public class ListDictionary : Dictionary
    {
        private int length;
        private Node head;
        private Node tail;

        public ListDictionary()
        {
            head = null;
            tail = null;
            length = 0;
        }

        public override void Store(string key, object value)
        {
            Node temp = head;
            Node prev = temp;
            if (head == null) {
                head = new Node(key, value);
                length++;
                return;
            }
            while (temp != null) {
                if (temp.getKey() == key) {
                    temp.setPair(key, value);
                    return;
                }
                prev = temp;
                temp = temp.getNext();
            }
            prev.setNext(new Node(key, value));
            length++;
        }

        public override object Lookup(string key)
        {
            Node temp = head;
            while (temp != null) {
                if (temp.getKey() == key)
                    return temp.getValue();
                temp = temp.getNext();
            }
            throw new DictionaryKeyNotFoundException(key);
        }

        public override int Count
        {
            get
            {
                return length;
            }
        }

        public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            for (Node temp = head; temp != null; temp = temp.getNext())
                 yield return new KeyValuePair<string, object>(temp.getKey(), temp.getValue());
        }
    }
}
