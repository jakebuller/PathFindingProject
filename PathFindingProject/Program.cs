using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using PathFindingProject.Agent;
using PathFindingProject.Environment.Map;
using PathFindingProject.Search.Domain;
using PathFindingProject.Search.Framework;
using PathFindingProject.Search.Informed;

namespace PathFindingProject {
    public class Program {

        private static int DimX;
        private static int DimY;
        private static List<Point> Robots = new List<Point>();
        private static Point Rendevous;
        private static ExtendableMap ProblemMap = new ExtendableMap();
		private static double Distance = 1;

        public static int Main( string[] args ) {
            string[] lines = System.IO.File.ReadAllLines( @"../../Maps/OneRobotWithObstaclesSmall.txt" );
            if( lines.Length < 6 ) {
                //Insufficient parameters
                return -1;
            }
			// first line has the x and then y size
			SetRoomDimensions( lines[0] );

            SetRobots(lines);

			// 2 + Robots.Count lines appear before the rendevous point
			SetRendevousPoint( lines[ 2 + Robots.Count] );

			// where the floor plan starts in the text file
            int depth = 3 + Robots.Count();

            for( int i = 0; i < DimX; i++ ) {
                char[] line = lines[i + depth].ToCharArray();

                for( int j = 0; j < line.Length; j++ ) {
					int pointValue = ( int )Char.GetNumericValue( line[j] );
					Console.Write( pointValue );

					if( pointValue == 1 ) {
						continue;
					}
					var label = i + "," + j;
					ProblemMap.AddVertex( label, i, j );

					AddLinks( i, j, label );
                }
                Console.WriteLine();
            }

            Console.WriteLine( "Rendevous at x: " + Rendevous.XCoord + " y: " + Rendevous.YCoord );
            Console.WriteLine( "Robots: " + Robots.Count );
			var start = string.Format(
				"{0},{1}",
				Robots.First().XCoord,
				Robots.First().YCoord
			);
			Problem problem = new Problem( 
				start,
				new ActionsFunction( ProblemMap ),
				new StringStateResultFunction(),
				new GoalTest( Rendevous ),
				new SimpleStepCostFunction()
			);

			IHeuristicFunction hf = new DirectPathHeuristicFunction( Rendevous );
			ISearch search = new AStarSearch( problem, hf );

			var results = search.Search( problem );

			// Keep the console window open in debug mode.

			Console.WriteLine();
			Console.WriteLine( "Actions:" );
			Console.WriteLine( string.Format(
				"\t{0}",
				string.Join( "\n\t", results.Select( a => {
					var m = (MoveToAction) a;
					return m.TargetLocation;
				} ) )
			) );
#if DEBUG
			Console.WriteLine( "Press any key to exit." );
			System.Console.ReadKey();
#endif

            return 0;
        }

		private static void AddLinks( int x, int y, string currentLabel ) {
			var topNeighbourLabel = x + "," + ( y + 1 );
			if( ProblemMap.IsVertexLabel( topNeighbourLabel ) ) {
				ProblemMap.AddBidirectionalLink( 
					currentLabel, 
					topNeighbourLabel,
					Distance
				);
			}

			var leftNeighbourLabel = ( x - 1 ) + "," + y;
			if( ProblemMap.IsVertexLabel( leftNeighbourLabel ) ) {
				ProblemMap.AddBidirectionalLink(
					currentLabel,
					leftNeighbourLabel,
					Distance
				);
			}

			var rightNeighbourLabel = ( x + 1 ) + "," + y;
			if( ProblemMap.IsVertexLabel( rightNeighbourLabel ) ) {
				ProblemMap.AddBidirectionalLink(
					currentLabel,
					rightNeighbourLabel,
					Distance
				);
			}

			var bottomNeighbourLabel = x + "," + ( y - 1 );
			if( ProblemMap.IsVertexLabel( bottomNeighbourLabel ) ) {
				ProblemMap.AddBidirectionalLink(
					currentLabel,
					bottomNeighbourLabel,
					Distance
				);
			}
		}

		private static void SetRendevousPoint( string line ) {
			string[] coords = line.Split( ' ' );
			Rendevous = new Point( int.Parse( coords[0] ), int.Parse( coords[1] ) );
		}

		private static void SetRobots(string[] lines) {
			string[] coords;
			// second line has the number of robots
			int numRobots = int.Parse( lines[1] );
			for( int i = 0; i < numRobots; i++ ) {
				// + 2 to always skip the first two lines of the file
				coords = lines[i + 2].Split( ' ' );
				int x = int.Parse( coords[0] );
				int y = int.Parse( coords[1] );
				Point point = new Point( x, y );
				Robots.Add( point );
			}
		}

		private static void SetRoomDimensions( string line ) {
			string[] dims = line.Split( ' ' );
			DimX = int.Parse( dims[0] );
			DimY = int.Parse( dims[1] );
		}
    }

}
