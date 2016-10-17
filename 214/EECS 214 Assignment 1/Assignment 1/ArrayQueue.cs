using System;
using System.Collections.Generic;
using System;

namespace Assignment_1
{
    /// <summary>
    /// A queue internally implemented as an array
    /// </summary>
    public class ArrayQueue : Queue
    {
        private readonly int max_size = 100;
        private object[] queue = new object[100];
        private int index = 0; //indicates the last added item in the queue 

        public ArrayQueue() {
        
        }


        /// <summary>
        /// Add object to end of queue
        /// </summary>
        /// <param name="o">object to add</param>
        public override void Enqueue(object o)
        {
            if (index >= queue.Length - 1)
                throw new QueueFullException();
            else if (index==0 && queue[index] == null){
                queue[index] = o;
            }
            else
            {
                index++;
                queue[index] = o;
            }
        }

        /// <summary>
        /// Remove object from beginning of queue.
        /// </summary>
        /// <returns>Object at beginning of queue</returns>
        public override object Dequeue()
        {
            if (index == 0 && queue[index] == null)
                throw new QueueEmptyException();
            else {
                object obj = queue[0];
                for (int i = 1; i <= index; i++) {
                    queue[i - 1] = queue[i];
                }
                index--;
                return obj;
            }
        }

        /// <summary>
        /// The number of elements in the queue.
        /// </summary>
        public override int Count
        {
            get {
                if (queue[0] == null)
                    return 0;
                else {
                    return index + 1;
                }
            }
        }

        /// <summary>
        /// True if the queue is full and enqueuing of new elements is forbidden.
        /// </summary>
        public override bool IsFull
        {
            get {
                return (index + 1) == max_size;
            }
        }
    }
}
