using System;
using System.Collections.Generic;

namespace Assignment_5
{
    /// <summary>
    /// Implements an undirected graph of "connections" between named people
    /// a la LinkedIn or Facebook.
    /// </summary>
    /// 

    public class Person {
        public string name;
        public List<Person> neighbors = new List<Person>();
        public Person prev;

        public Person(string n) {
            name = n;
        }

    }

    public class PersonGraph
    {
        Dictionary<string, Person> graph = new Dictionary<string, Person>();


        /// <summary>
        /// Adds a new person (node) to the graph
        /// </summary>
        /// <param name="name">Name of the person</param>
        public void AddPerson(string name)
        {
            graph.Add(name, new Person(name));
        }

        /// <summary>
        /// Adds a new edge to the graph
        /// </summary>
        /// <param name="person1">Name of first person</param>
        /// <param name="person2">Name of second person</param>
        public void AddConnection(string person1, string person2)
        {
            if (!graph.ContainsKey(person1))
                graph.Add(person1, new Person(person1));
            if (!graph.ContainsKey(person2))
                graph.Add(person2, new Person(person2));
            graph[person1].neighbors.Add(new Person(person2));
            graph[person2].neighbors.Add(new Person(person1));

        }

        /// <summary>
        /// Returns the length of the shortest path between two people in the graph
        /// For example, the distance from a node to itself is 0, from a node to a
        /// neighbor is 1, etc.
        /// </summary>
        /// <param name="person1">Name of the first person</param>
        /// <param name="person2">Name of the second person</param>
        /// <returns>Length of the path</returns>
        public int Distance(string person1, string person2)
        {
            Queue<Person> q = new Queue<Person>();
            bool isPath = false;
            int sPath=0;
            Dictionary<string, Person> visited = new Dictionary<string, Person>();
            Person start = graph[person1];
            Person end = graph[person2];
            Person p;
            int n;
            q.Enqueue(start);
            visited.Add(person1, start);
            while (q.Count != 0) {
                p = q.Dequeue();
                if (p.name == person2)
                {
                    isPath = true;
                    n = 0;
                    Person temp = p;
                    while (temp.prev != null)
                    {
                        n++;
                        temp = temp.prev;
                    }
                    sPath = n;
                    break;
                }
                
                List<Person>.Enumerator e = p.neighbors.GetEnumerator();
                while (e.MoveNext()) {
                    if (!visited.ContainsKey(e.Current.name)) {
                        visited.Add(e.Current.name, e.Current);
                        e.Current.prev = p;
                        q.Enqueue(e.Current);
                    }
                }
                    
            }
        
            if (isPath)
                return sPath;
            return -1;
        }
    }
}
