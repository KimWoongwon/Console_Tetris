using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTetris
{
    
    class Bricks
    {
        int[,] brick1 = new int[,]
        {
            {1, 1, 0},
            {0, 1, 1}
        };
        int[,] brick2 = new int[,]
        {
            {0, 1, 1},
            {1, 1, 0}
        };
        int[,] brick3 = new int[,]
        {
            {0, 1, 0},
            {1, 1, 1}
        };
        int[,] brick4 = new int[,]
        {
            {1, 1, 1},
            {1, 0, 0}
        };
        int[,] brick5 = new int[,]
        {
            {1, 1, 1},
            {0, 0, 1}
        };
        int[,] brick6 = new int[,]
        {
            {1, 1},
            {1, 1}
        };
        int[,] brick7 = new int[1, 4]
        {
            { 1, 1, 1, 1 }
        };
            
        List<int[,]> bricklist = new List<int[,]>();
        
        public Bricks()
        {
            bricklist.Add(brick1);
            bricklist.Add(brick2);
            bricklist.Add(brick3);
            bricklist.Add(brick4);
            bricklist.Add(brick5);
            bricklist.Add(brick6);
            bricklist.Add(brick7);
        }
        public int[,] GetListItem(int p_index)
        {
            if(p_index < 7)
                return bricklist[p_index];
            return null;
        }
    }
}
