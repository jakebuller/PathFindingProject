using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Domain {
    public class GoalTest : IGoalTest {
        private Point m_rendevous;

        public GoalTest( Point rendevous ) {
            this.m_rendevous = rendevous;
        }

        public bool IsGoalState( object state ) {
            int[,] map = ( int[,] )state;
            if( map[m_rendevous.XCoord, m_rendevous.YCoord] != 2 ) {
                return false;
            }
            for( int i = 0; i < map.GetLength( 0 ); i++ ) {
                for( int j = 0; j < map.GetLength( 1 ); j++ ) {
                    if( map[i, j] == 2 && ( i != m_rendevous.XCoord || j != m_rendevous.YCoord ) ) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
