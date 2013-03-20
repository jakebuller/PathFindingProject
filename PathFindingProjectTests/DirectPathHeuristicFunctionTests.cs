//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using NUnit.Framework;

//using PathFindingProject;
//using PathFindingProject.Search.Domain;


//namespace PathFindingProjectTests {

//    [TestFixture]
//    public class DirectPathHeuristicFunctionTests {

//        [Test]
//        public void CalculateShouldReturnZeroWhenGivenGoalState() {
//            var point = new Point( 0, 0 );
//            var state = new int[,] { { 2, 0 }, { 1, 1 } };
//            var function = new PathFindingProject.Search.Domain.DirectPathHeuristicFunction(point);

//            var result = function.Calculate( state );

//            Assert.AreEqual( 0, result );
//        }

//        [Test]
//        public void CalculateShouldReturnPathCostForOneRobot() {
//            var point = new Point( 0, 1 );
//            var state = new int[,] { { 1, 0 }, { 2, 1 } };
//            var function = new PathFindingProject.Search.Domain.DirectPathHeuristicFunction(point);

//            var result = function.Calculate( state );

//            Assert.AreEqual( 2, result );
//        }

//        [Test]
//        public void CalculateShouldReturnPathCostForTwoRobots() {
//            var point = new Point( 2, 1 );
//            var state = new int[,] { 
//                { 1, 0, 2 }, 
//                { 2, 1, 0 }, 
//                { 0, 0, 1 } 
//            };
//            var function = new PathFindingProject.Search.Domain.DirectPathHeuristicFunction(point);

//            var result = function.Calculate( state );

//            Assert.AreEqual( 5, result );
//        }

//        [Test]
//        public void CalculateShouldReturnPathCostOfOneRobotWhenOneIsAtGoalAndOtherIsNot() {
//            var point = new Point( 1, 0 );
//            var state = new int[,] { 
//                { 1, 0, 2 }, 
//                { 2, 1, 0 }, 
//                { 0, 0, 1 } 
//            };
//            var function = new PathFindingProject.Search.Domain.DirectPathHeuristicFunction(point);

//            var result = function.Calculate( state );

//            Assert.AreEqual( 3, result );
//        }

//        [Test]
//        public void CalculateShouldReturnZeroWhenThereAreNoRobots() {
//            var point = new Point( 1, 0 );
//            var state = new int[,] { 
//                { 1, 0, 0 }, 
//                { 0, 1, 0 }, 
//                { 0, 0, 1 } 
//            };
//            var function = new PathFindingProject.Search.Domain.DirectPathHeuristicFunction( point );

//            var result = function.Calculate( state );

//            Assert.AreEqual( 0, result );
//        }
//    }
//}
