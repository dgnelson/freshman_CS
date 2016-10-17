using System;

namespace Assignment_1
{
    public class Node
    {
        public object val;
        public Node next;
        public Node prev;
        public Node(object o) {
            val = o;
        }

    }

   


    /// <summary>
    /// A queue internally implemented as a linked list of objects
    /// </summary>
    public class LinkedListQueue : Queue
    {


        private Node head = null;
        private Node tail = null;
        private int num_nodes = 0;

        /// <summary>
        /// Add object to end of queue
        /// </summary>
        /// <param name="o">object to add</param>
        public override void Enqueue(object o)
        {
            Node temp = new Node(o);
            if (num_nodes == 0)
            {
                head = temp;
                tail = temp;
            }
            else {
                tail.next = temp;
                tail = temp;
            }
            num_nodes++;
        }

        /// <summary>
        /// Remove object from beginning of queue.
        /// </summary>
        /// <returns>Object at beginning of queue</returns>
        public override object Dequeue()
        {
            if(num_nodes==0)
                throw new QueueEmptyException();
            else
            {
                object o = head.val;
                head = head.next;
                num_nodes--;
                return o;
            }

        }

        /// <summary>
        /// The number of elements in the queue.
        /// </summary>
        public override int Count
        {
            get
            {
                return num_nodes;
            }
        }

        /// <summary>
        /// True if the queue is full and enqueuing of new elements is forbidden.
        /// Note: LinkedListQueues can be grown to arbitrary length, and so can
        /// never fill.
        /// </summary>
        public override bool IsFull
        {
            get
            {
                return false;
            }
        }
    }
}
