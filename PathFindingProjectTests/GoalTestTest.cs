using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using PathFindingProject;
using PathFindingProject.Search.Domain;


namespace PathFindingProjectTests {
    [TestFixture]
    public class GoalTestTest {
        [Test]
        public void TestShouldReturnTrueAtGoalState() {
            int[,] map = new int[,] { { 0, 0 }, { 0, 2 } };
            Point goal = new Point( 1, 1 );
            var gt = new GoalTest( goal );

            bool result = gt.IsGoalState( map );

            Assert.IsTrue( result );
        }

        [Test]
        public void TestShouldReturnFalseNoRobot() {
            int[,] map = new int[,] { { 0, 0 }, { 0, 0 } };
            Point goal = new Point( 1, 1 );
            var gt = new GoalTest( goal );

            bool result = gt.IsGoalState( map );

            Assert.IsFalse( result );
        }

        [Test]
        public void TestShouldReturnFalseNotGoalState() {
            int[,] map = new int[,] { { 0, 0 }, { 2, 0 } };
            Point goal = new Point( 1, 1 );
            var gt = new GoalTest( goal );

            bool result = gt.IsGoalState( map );

            Assert.IsFalse( result );
        }

        [Test]
        public void TestShouldReturnFalseWithTwoRobotsOneAtGoalState() {
            int[,] map = new int[,] { { 0, 2, 0 }, { 0, 2, 1 }, { 0, 1, 0 } };
            Point goal = new Point( 1, 1 );
            var gt = new GoalTest( goal );

            bool result = gt.IsGoalState( map );

            Assert.IsFalse( result );
        }

        [Test]
        public void TestShouldReturnFalseWithTwoRobotsNoneAtGoalState() {
            int[,] map = new int[,] { { 0, 2, 0 }, { 0, 0, 1 }, { 2, 1, 0 } };
            Point goal = new Point( 1, 1 );
            var gt = new GoalTest( goal );

            bool result = gt.IsGoalState( map );

            Assert.IsFalse( result );
        }
    }
}
