using System;
using System.Collections.Generic;
using System.Drawing;

namespace PathPlanner
{
    /// <summary>
    /// Represents a graph to search
    /// </summary>
    public class UndirectedGraph
    {
        /// <summary>
        /// Creates a new graph with the specified nodes.
        /// </summary>
        public UndirectedGraph(Node[] nodes)
        {
            Nodes = nodes;
        }

        /// <summary>
        /// The nodes of the graph
        /// </summary>
        public readonly Node[] Nodes;

        /// <summary>
        /// Finds the shortest path between the specified nodes and returns it as a list of nodes.
        /// </summary>
        /// <param name="start">The starting node for the path</param>
        /// <param name="end">The ending node for the path</param>
        /// <returns>The path, represented as a list of nodes beginning with start and ending with end.</returns>
        public List<Node> FindPath(Node start, Node end)
        {
            BinaryHeap pq = new BinaryHeap(Nodes.Length);
            foreach (Node n in Nodes) {
                if (n == start)
                    n.NodeCost = 0;
                else
                    n.NodeCost = Double.PositiveInfinity;
                n.Predecessor = null;
                pq.Add(n, n.NodeCost);
            }
            while (pq.Count != 0) {
                Node n = pq.ExtractMin();
                if (n == end)
                    break;
                foreach (UndirectedEdge v in n.Edges) {
                    Node temp;
                    if (v.A == n)
                        temp = v.B;
                    else
                        temp = v.A;
                    double w = v.Cost;
                    double newCost = w + n.NodeCost;
                    if (newCost < temp.NodeCost) {
                        pq.DecreasePriority(temp, newCost);
                        temp.NodeCost = newCost;
                        temp.Predecessor = n;
                    }
                }
            }
            Stack<Node> s = new Stack<Node>();
            Node t = end;
            s.Push(t);
            while (t.Predecessor != null) {
                s.Push(t.Predecessor);
                t = t.Predecessor;
            }
            List<Node> l = new List<Node>();
            while (s.Count != 0) {
                l.Add(s.Pop());
            }
            return l;
        }

        #region Utility functions
        /// <summary>
        /// Reads a graph from a spreadsheet.
        /// </summary>
        public static UndirectedGraph FromSpreadsheet(string path)
        {
            object[][] data = Spreadsheet.ConvertAllNumbers(Spreadsheet.Read(path, ','));
            List<Node> nodes = new List<Node>();
            Dictionary<string, Node> nodeNames = new Dictionary<string, Node>();
            
            // Make all the nodes, one per row
            for (int rowNum = 1; rowNum<data.Length; rowNum++)
            {
                object[] row = data[rowNum];
                string name = (string)row[0];
                Node n = new Node(name, new PointF(Convert.ToSingle(row[1]), Convert.ToSingle(row[2])));
                nodes.Add(n);
                nodeNames[name] = n;
            }

            // Add all the edges
            for (int rowNum = 1; rowNum < data.Length; rowNum++)
            {
                object[] row = data[rowNum];
                string name = (string)row[0];
                Node n = nodeNames[name];
                for (int colNum = 3; colNum<row.Length; colNum++)
                {
                    string neighborName = (string)row[colNum];
                    if (neighborName != "")
                    {
                        Node neighbor = nodeNames[neighborName];
                        n.AddEdge(neighbor);
                    }
                }
            }

            // Finally, make the graph
            return new UndirectedGraph(nodes.ToArray());
        }

        /// <summary>
        /// Finds the node with a given name.
        /// </summary>
        /// <param name="nodeName">Name to search for</param>
        /// <returns>The node with that name, or null if there is no such node.</returns>
        public Node FindNode(String nodeName)
        {
            return Array.Find(Nodes, n => n.Name == nodeName);
        }
        #endregion
    }
}
