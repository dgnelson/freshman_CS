using System;

namespace Assignment_1
{
    /// <summary>
    /// A double-ended queue
    /// Implement this as a doubly-linked list
    /// </summary>
    public class Deque
    {
        private Node head;
        private Node tail;
        private int num_nodes;

        /// <summary>
        /// Add object to end of queue
        /// </summary>
        /// <param name="o">object to add</param>
        public void AddFront(object o)
        {
            Node temp_node = new Node(o);
            if (num_nodes == 0)
            {
                head = temp_node;
                tail = temp_node;
            }
            else
            {
                temp_node.next = head;
                head.prev = temp_node;
                head = temp_node;
            }
            num_nodes++;
        }

        /// <summary>
        /// Remove object from beginning of queue.
        /// </summary>
        /// <returns>Object at beginning of queue</returns>
        public object RemoveFront()
        {
            if (num_nodes == 0)
                throw new QueueEmptyException();
            else if (num_nodes == 1) {
                object v = head.val;
                head = null;
                tail = null;
                num_nodes--;
                return v;
            }
            else
            {
                Node temp = head;
                head = head.next;
                head.prev = null;
                num_nodes--;
                return temp.val;
            }
        }

        /// <summary>
        /// Add object to end of queue
        /// </summary>
        /// <param name="o">object to add</param>
        public void AddEnd(object o)
        {
            Node temp = new Node(o);
            if (num_nodes == 0)
            {
                head = temp;
                tail = temp;   
            }
            else
            {
               
                tail.next = temp;
                temp.prev = tail;
                tail = tail.next;
            }
            num_nodes++;
        }

        /// <summary>
        /// Remove object from beginning of queue.
        /// </summary>
        /// <returns>Object at beginning of queue</returns>
        public object RemoveEnd()
        {
            if (num_nodes == 0)
                throw new QueueEmptyException();
            else if (num_nodes == 1) {
                object o = head.val;
                head = null;
                tail = null;
                num_nodes--;
                return o;
            }
            else
            {
                Node temp = tail;
                tail = tail.prev;
                tail.next = null;
                num_nodes--;
                return temp.val;
            }
        }

        /// <summary>
        /// The number of elements in the queue.
        /// </summary>
        public int Count
        {
            get
            {
                return num_nodes;
            }
        }

        /// <summary>
        /// True if the queue is empty and dequeuing is forbidden.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return (num_nodes == 0);
            }
        }
    }
}
