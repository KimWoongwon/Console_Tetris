using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTetris
{
    class Brick
    {
        int[,] bricks1 = new int[,]
        {
            {1, 1, 0},
            {0, 1, 1}
        };
        int[,] bricks2 = new int[,]
        {
            {0, 1, 1},
            {1, 1, 0}
        };
        int[,] bricks3 = new int[,]
        {
            {0, 1, 0},
            {1, 1, 1}
        };
        int[,] bricks4 = new int[,]
        {
            {1, 1, 1},
            {1, 0, 0}
        };
        int[,] bricks5 = new int[,]
        {
            {1, 1, 1},
            {0, 0, 1}
        };
        int[,] bricks6 = new int[,]
        {
            {1, 1},
            {1, 1}
        };
        int[,] bricks7 = new int[1, 4]
        {
            { 1, 1, 1, 1 }
        };
            
        List<int[,]> bricklist = new List<int[,]>();
        
        public void MakeBrick()
        {
            bricklist.Add(bricks1);
            bricklist.Add(bricks2);
            bricklist.Add(bricks3);
            bricklist.Add(bricks4);
            bricklist.Add(bricks5);
            bricklist.Add(bricks6);
            bricklist.Add(bricks7);
        }
        public int[,] GetListItem(int p_index)
        {
            if(p_index < 7)
                return bricklist[p_index];
            return null;
        }
        
    }
}
