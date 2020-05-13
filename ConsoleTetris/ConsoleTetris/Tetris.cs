using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTetris
{
    class Tetris
    {
        string[] bricks = new string[] { "****", "**\n**", "** \n **", " **\n** ", " * \n***", "**\n *\n *", "**\n* \n* " };
        int startY = Environment.TickCount;
        int Checktime = 1000;
        int PosX = 1; // 
        int PosY = 0; // max = 16
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
                        PosX++;
                        break;
                    case ConsoleKey.A:
                        PosX--;
                        break;
                    case ConsoleKey.S:
                        Checktime = 300;
                        break;
                }
            }
            
            int Timetemp = Environment.TickCount;
            if (Timetemp - startY > Checktime)
            {
                startY = Timetemp;
                if(PosY < 16)
                    PosY++;
            }
        }
        public void Render()
        {
            string Stage = "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "0          0\n" +
                           "000000000000" ;
            MtgClass doublebuffer = new MtgClass(Stage);
            

            Console.Write(doublebuffer.Getbuffer());

            Console.SetCursorPosition(0, 0);

        }
        public void GameStart()
        {
            while(true)
            {
                BrickMove();
                Render();
            }
        }


    }
}
