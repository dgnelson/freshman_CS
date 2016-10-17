using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment_5;

namespace UnitTests
{
    [TestClass]
    public class PersonGraphTests
    {
        private static PersonGraph Graph = new PersonGraph();

        /// <summary>
        /// This sets up the graph that will be used in the tests
        /// </summary>
        [ClassInitialize]
        public static void MakeTestGraph(TestContext ignore)
        {
            Graph.AddPerson("Mr. Lonely");
            Graph.AddConnection("Kevin Bacon", "Julia Roberts");
            Graph.AddConnection("Kevin Bacon", "Paul McCartney");
            Graph.AddConnection("Paul McCartney", "Ringo Starr");
            Graph.AddConnection("Julia Roberts", "Barack Obama");
            Graph.AddConnection("Julia Roberts", "David Letterman");
            Graph.AddConnection("Barack Obama", "David Letterman");
            Graph.AddConnection("Barack Obama", "Xi Jinping");
            Graph.AddConnection("Barack Obama", "Eric Cartman");
            Graph.AddConnection("Eric Cartman", "Stan Marsh");
            Graph.AddConnection("Eric Cartman", "Kyle Broflovski");
            Graph.AddConnection("Eric Cartman", "Kenny McCormick");
            Graph.AddConnection("Stan Marsh", "Kenny McCormick");
            Graph.AddConnection("Kyle Broflovski", "Kenny McCormick");
            Graph.AddConnection("Kyle Broflovski", "Stan Marsh");
            Graph.AddConnection("Eric Cartman", "Cthulhu");
        }

        [TestMethod]
        public void GraphConstructionTest()
        {
            // This test doesn't actually run anything
            // But if MakeTestGraph above fails, then this will fail too
            // making it clear that there's a problem with constructing the
            // graph.
            return;
        }

        /// <summary>
        /// Make sure the distance from a node to a neighbor is 1
        /// </summary>
        [TestMethod]
        public void KevinToJuliaTest()
        {
            Assert.AreEqual(1, Graph.Distance("Kevin Bacon", "Julia Roberts"));
        }

        /// <summary>
        /// Make sure the distance from a node to a neighbor is 1
        /// </summary>
        [TestMethod]
        public void JuliaKevinTest()
        {
            Assert.AreEqual(1, Graph.Distance("Julia Roberts", "Kevin Bacon"));
        }

        /// <summary>
        /// Test a longer distance
        /// </summary>
        [TestMethod]
        public void KevinToCthulhuTest()
        {
            Assert.AreEqual(4, Graph.Distance("Kevin Bacon", "Cthulhu"));
        }

        /// <summary>
        /// Test a longer distance
        /// </summary>
        [TestMethod]
        public void KennyRingoTest()
        {
            Assert.AreEqual(6, Graph.Distance("Kenny McCormick", "Ringo Starr"));
        }
    }
}
