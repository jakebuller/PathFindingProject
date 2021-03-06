﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        private static Point Rendezvous;
        private static ExtendableMap ProblemMap = new ExtendableMap();
        private static double Distance = 1;
        private static Stopwatch stopWatch = new Stopwatch();
        private static Stopwatch execWatch = new Stopwatch();

        public static int Main( string[] args ) {
			if( args.Length != 1 ) {
				ShowParams();
                Console.WriteLine( "Press any key to exit." );
                System.Console.ReadKey();
				return -1;
			}
			var path = args[0];
			if( !File.Exists( path ) ) {
				ShowParams();
				ShowFileSetup();
                Console.WriteLine( "Press any key to exit." );
                System.Console.ReadKey();
				return -2;
			}
            execWatch = Stopwatch.StartNew();
            Console.WriteLine( "building map..." );

            string[] lines = File.ReadAllLines( path );
            if( lines.Length < 6 ) {
				ShowFileSetup();
                return -3;
            }
            // first line has the x and then y size
            SetRoomDimensions( lines[0] );

            SetRobots( lines );

            // 2 + Robots.Count lines appear before the rendevous point
            SetRendevousPoint( lines[2 + Robots.Count] );

            // where the floor plan starts in the text file
            int depth = 3 + Robots.Count();

            for( int j = DimY - 1; j >= 0; j-- ) {
                char[] line = lines[depth].ToCharArray();

                for( int i = 0; i < DimX; i++ ) {
                    int pointValue = ( int )Char.GetNumericValue( line[i] );


                    if( pointValue == 1 ) {
                        continue;
                    }

                    var label = i + "," + j;
                    ProblemMap.AddVertex( label, i, j );

                    AddLinks( i, j, label );
                }
                depth++;
            }

            Console.WriteLine( "Finished building." );

			var fileNameStore = new ConcurrentBag<string>();
            Parallel.ForEach( Robots, Robot => {

                var start = string.Format(
                    "{0},{1}",
                    Robot.XCoord,
                    Robot.YCoord
                );
                Problem problem = new Problem(
                    start,
                    new ActionsFunction( ProblemMap ),
                    new ResultFunction(),
                    new GoalTest( Rendezvous ),
                    new SimpleStepCostFunction()
                );

                IHeuristicFunction hf =
                    new DirectPathHeuristicFunction( Rendezvous );
                ISearch search = new AStarSearch( problem, hf );

                stopWatch = Stopwatch.StartNew();
                var fileName = string.Format(
                    "robot-Guid.-{0}.txt",
                    Guid.NewGuid()
                );
                fileNameStore.Add( fileName );
                File.WriteAllText(
                    fileName,
                    string.Format(
                        "Robot starting at x: {0} and y: {1} ...\n",
                        Robot.XCoord,
                        Robot.YCoord
                    )
                );
                Console.WriteLine( string.Format(
                        "Robot starting at x: {0} and y: {1} ...\n",
                        Robot.XCoord,
                        Robot.YCoord
                    ) );
                File.AppendAllText(
                    fileName,
                    string.Format(
                        "Solution path for robot starting at ({0},{1}):\n",
                        Robot.XCoord,
                        Robot.YCoord
                    )
                );
                Console.WriteLine( string.Format(
                        "Solution path for robot starting at ({0},{1}):\n",
                        Robot.XCoord,
                        Robot.YCoord
                    ) );
                if( Robot.Equals( Rendezvous ) ) {
                    File.AppendAllText(
                        fileName,
                        "This robot is starting at the goal state."
                    );
                } else {

                    var results = search.Search( problem );

                    if( results.Any() ) {
                        File.AppendAllText(
                            fileName,
                            string.Format(
                                "({0},{1}) -> ({2})\n",
                                Robot.XCoord,
                                Robot.YCoord,
                                string.Join(
                                    ") -> (",
                                    results.Select(
                                        m => m.TargetLocation
                                    )
                                )
                            )
                        );
                        Console.WriteLine( "finished for robot starting at: " + Robot.XCoord + ", " + Robot.YCoord );
                    } else {
                        File.AppendAllText(
                            fileName,
                            "A path could not be found for this robot. " +
                            "It must be trapped!"
                        );
                    }
                }

                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value. 
                string elapsedTime = string.Format(
                    "{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours,
                    ts.Minutes,
                    ts.Seconds,
                    ts.Milliseconds
                );
                File.AppendAllText(
                    fileName,
                    string.Format( "RunTime: {0}\n", elapsedTime )
                );

            } );

			MergeAndDeleteFiles( fileNameStore );

            execWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan t = execWatch.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsed = string.Format( 
				"{0:00}:{1:00}:{2:00}.{3:00}",
                t.Hours,
				t.Minutes,
				t.Seconds,
                t.Milliseconds
			);
			Console.WriteLine( 
				string.Format( "Total runtime: {0}", elapsed ) 
			);
#if DEBUG
            Console.WriteLine( "Press any key to exit." );
            System.Console.ReadKey();
#endif

            return 0;
        }

		private static void MergeAndDeleteFiles( 
			ConcurrentBag<string> fileNames 
		) {
			var outputFile = "output.txt";
			File.WriteAllText( outputFile, "Results:" );
			foreach( var fileName in fileNames ) {
				File.AppendAllText( outputFile, "\n" );
				File.AppendAllLines( 
					outputFile, 
					File.ReadAllLines( fileName ) 
				);
				File.Delete( fileName );
			}
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
            Rendezvous = new Point( 
				int.Parse( coords[0] ),
				int.Parse( coords[1] ) 
			);
        }

        private static void SetRobots( string[] lines ) {
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
            DimY = int.Parse( dims[0] );
            DimX = int.Parse( dims[1] );
        }

		private static void ShowParams() {
			Console.WriteLine( 
				"Expected parameter is a file path to the input file" 
			);
		}

		private static void ShowFileSetup() {
			Console.WriteLine( 
				"Input file should be setup in the following manner:"
			);
			Console.WriteLine( "Room dimensions as 'XMax YMax'" );
			Console.WriteLine( "Number of robots in the room" );
			Console.WriteLine( 
				"A line stating the starting point of each robot, as 'X Y'"
			);
			Console.WriteLine( 
				"The coordinates of the rendezvous point, as 'X Y'" );
			Console.WriteLine( 
				"Room points (0, YMax - 1), (1, YMax - 1), ... , " +
				"(XMax, YMax - 1)"
			);
			Console.WriteLine("...");
			Console.WriteLine(
				"Room points (0, 0), (1, 0), ... , (XMax, 0)"
			);
		}
    }
}
