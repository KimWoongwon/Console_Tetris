using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTetris
{
    class Tetris
    {
        //string[] bricks = new string[] { "****", "**^**", "** ^ **", " **^** ", " * ^***", "**^ *^ *", "**^* ^* " };
        char[,] bricks = new char[,]
            {
                 {'*', '*', ' '},
                 {' ', '*', '*'}

            };

        //0,0 -> 0,3

        int startY = Environment.TickCount;
        int Checktime = 1000;
        int PosX = 1; //  
        int PosY = 0; // max = 16
        bool Endflag = true;
        ConsoleKeyInfo info;
        public void BrickMove()
        {
            Checktime = 1000;
            if (Console.KeyAvailable)
            {
                info = Console.ReadKey(); 
                switch(info.Key)
                {
                    case ConsoleKey.D:
                        if(PosX < 11 - bricks.GetLength(1))
                            PosX++;
                        break;
                    case ConsoleKey.A:
                        if(PosX > 1)
                            PosX--;
                        break;
                    case ConsoleKey.Spacebar:
                        PosY = 17 - bricks.GetLength(0);
                        break;
                    case ConsoleKey.S:
                        Checktime = 300;
                        break;
                    case ConsoleKey.Escape:
                        Endflag = false;
                        break;

                }
            }
        }
        public void DropBrick()
        {
            int Timetemp = Environment.TickCount;
            if (Timetemp - startY > Checktime)
            {
                startY = Timetemp;
                if (PosY < 17 - bricks.GetLength(0))
                    PosY++;
            }
        }
        //public char[,] turn(char [,] p_arr)
        //{
            
        //    char[,] temp = new char[,];

        //}

        public void Render()
        {
            
            MtgClass doublebuffer = new MtgClass();
            doublebuffer.SetBuffer(PosX, PosY, bricks);

            Console.CursorVisible = false;
            Console.Write(doublebuffer.Getbuffer());
            Console.SetCursorPosition(0, 0);

        }
        public void GameStart()
        {
            while(Endflag)
            {
                DropBrick();
                BrickMove();

                Render();
            }
        }


    }
}
