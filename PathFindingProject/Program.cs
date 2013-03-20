using System;
using System.Collections.Generic;
using System.Linq;

using PathFindingProject.Search.Domain;

namespace PathFindingProject {
	public class Program {

        private static int DimX;
        private static int DimY;       
        private static List<Point> Robots;
        private static Point Rendevous;
        private static int[,] Map;

		public static int Main( string[] args ) {
			string[] lines = System.IO.File.ReadAllLines( @"../../Maps/map_1.txt" );
            string[] coords;  
            // Display the file contents by using a foreach loop.
            if (lines.Length < 6) {
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
            for (int i = 0; i < NumRobot;i++) {
				coords = lines[i + 2].Split( ' ' );
				int x = int.Parse( coords[0] );
				int y = int.Parse( coords[1] );
				Point NextPoint = new Point( x, y );
				Robots.Add( NextPoint );
            }

            coords = lines[Robots.Count + 2].Split(' ');
			Rendevous = new Point( int.Parse( coords[0] ), int.Parse( coords[1] ) );

            int depth = 3 + Robots.Count();
            Map = new int[DimX,DimY];
            for (int i = 0; i < DimX; i++) {
               // Console.WriteLine(lines[i + depth]);
                char[] line = lines[i + depth].ToCharArray();
                for (int j = 0; j < line.Length; j++) {
					Map[i, j] = ( int )Char.GetNumericValue( line[j] );
					Console.Write( Map[i, j] );                 
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
