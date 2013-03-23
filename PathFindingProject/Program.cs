using System;
using System.Collections.Generic;
using System.Linq;

using PathFindingProject.Search.Domain;
using PathFindingProject.Environment.Map;

namespace PathFindingProject {
    public class Program {

        private static int DimX;
        private static int DimY;
        private static List<Point> Robots;
        private static Point Rendevous;
        private static ExtendableMap ProblemMap;

        public static int Main( string[] args ) {
            string[] lines = System.IO.File.ReadAllLines( @"../../Maps/map_1.txt" );
            string[] coords;
            if( lines.Length < 6 ) {
                //Insufficient parameters
                return -1;
            }
            //get the room dimensions
            string[] dims = lines[0].Split( ' ' );
            DimY = int.Parse( dims[1] );
            DimX = int.Parse( dims[0] );

            Robots = new List<Point>();
            //get the robots
            int NumRobot = int.Parse( lines[1] );
            for( int i = 0; i < NumRobot; i++ ) {
                coords = lines[i + 2].Split( ' ' );
                int x = int.Parse( coords[0] );
                int y = int.Parse( coords[1] );
                Point NextPoint = new Point( x, y );
                Robots.Add( NextPoint );
            }

            coords = lines[Robots.Count + 2].Split( ' ' );
            Rendevous = new Point( int.Parse( coords[0] ), int.Parse( coords[1] ) );

            int depth = 3 + Robots.Count();

            //Map = new int[DimX,DimY];
            ProblemMap = new ExtendableMap();
            int[,] Map = new int[DimX, DimY];
            for( int i = 0; i < DimX; i++ ) {
                char[] line = lines[i + depth].ToCharArray();
                for( int j = 0; j < line.Length; j++ ) {
                    Map[i, j] = ( int )Char.GetNumericValue( line[j] );
                    Console.Write( Map[i, j] );
                }
                Console.WriteLine();
            }


            for( int i = 0; i < DimX; i++ ) {
                for( int j = 0; j < DimY; j++ ) {
                    if( Map[i, j] == 0 ) {
                        Console.WriteLine( "Adding point for " + i + "," + j );
                        //Check the boundaries
                        //Then check the neighbour before adding the link
                        //if the neighbour is 1 don't add the link
                        if( j != 0 ) {
                            if( Map[i, j - 1] == 0 ) {
                                ProblemMap.AddBidirectionalLink( i + "," + j, i + "," + ( j - 1 ), 1 );
                                Console.WriteLine( "Added link to: " + i + "," + ( j - 1 ) );
                            }
                        }
                       if( j < DimY - 1 ) {
                            if( Map[i, j + 1] == 0 ) {
                                ProblemMap.AddBidirectionalLink( i + "," + j, i + "," + ( j + 1 ), 1 );
                                Console.WriteLine( "Added link to: " + i + "," + ( j + 1 ) );
                            }
                        }
                        if( i != 0 ) {
                            if( Map[i - 1, j] == 0 ) {
                                ProblemMap.AddBidirectionalLink( i + "," + j, ( i - 1 ) + "," + j, 1 );
                                Console.WriteLine( "Added link to: " + ( i - 1 ) + "," + j );
                            }
                        }
                        if( i < DimX - 1) {
                            if( Map[i + 1, j] == 0 ) {
                                ProblemMap.AddBidirectionalLink( i + "," + j, ( i + 1 ) + "," + j, 1 );
                                Console.WriteLine( "Added link to: " + ( i + 1 ) + "," + j );
                            }
                        }
                    }
                }
                Console.WriteLine();
            }


            Console.WriteLine( "Rendevous at x: " + Rendevous.XCoord + " y: " + Rendevous.YCoord );
            Console.WriteLine( "Robots: " + Robots.Count );
            // Keep the console window open in debug mode.
#if DEBUG
            Console.WriteLine( "Press any key to exit." );
            System.Console.ReadKey();
#endif
            return 0;
        }
    }

}
