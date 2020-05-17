using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTetris
{
    
    class Bricks
    {
        int[,] brick1 = new int[,]  //테트리스 블럭 Z
        {
            {1, 1, 0},
            {0, 1, 1},
            {0, 0, 0}
        };
        int[,] brick2 = new int[,]  //테트리스 블럭 S
        {
            {0, 1, 1},
            {1, 1, 0},
            {0, 0, 0}
        };
        int[,] brick3 = new int[,]  //테트리스 블럭 T
        {
            {0, 1, 0},
            {1, 1, 1},
            {0, 0, 0}
        };
        int[,] brick4 = new int[,]  //테트리스 블럭 L
        {
            {1, 1, 1},
            {1, 0, 0},
            {0, 0, 0}
        };
        int[,] brick5 = new int[,]  //테트리스 블럭 J
        {
            {1, 1, 1},
            {0, 0, 1},
            {0, 0, 0}
        };
        int[,] brick6 = new int[,]  //테트리스 블럭 O
        {
            {1, 1},
            {1, 1}
        };
        int[,] brick7 = new int[4, 4]  //테트리스 블럭 I
        {
            { 0, 0, 0, 0 },
            { 1, 1, 1, 1 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
            
        List<int[,]> bricklist = new List<int[,]>();
        // 랜덤으로 블럭을 뽑아주기 위한 리스트
        public Bricks() 
        {
            // 생성자에서 리스트에 블럭 추가, 한번만 실행된다.
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
            // 랜덤한 index를 받아서 리스트의 내용물을 반환하는 함수.
            if (p_index < 7)
                return bricklist[p_index];
            return null;
        }
    }
}
