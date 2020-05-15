using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTetris
{
    class Tetris
    {
        MtgClass doublebuffer;
        MtgClass stage = new MtgClass();

        Brick brick = new Brick();
        private int bnum = 0;

        private int startY = Environment.TickCount;
        private int Checktime = 1000;
        private int PosX = 4; //  
        private int PosY = 0; // max = 16
        private bool Endflag = true;
        private ConsoleKeyInfo info;
        
        public void SetBricks()
        {
            Random random = new Random(DateTime.Now.Second);
            bnum = random.Next() % 7;
        }
        public void BrickMove()
        {
            Checktime = 1000;
            if (Console.KeyAvailable)
            {
                info = Console.ReadKey(); 
                switch(info.Key)
                {
                    case ConsoleKey.D:
                        if(PosX < 11 - (brick.GetListItem(bnum).GetLength(1)))
                        {
                            PosX++;
                        }
                        break;
                    case ConsoleKey.A:
                        if(PosX > 1)
                        {
                            PosX--;
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        int temp = PosY;
                        while (true)
                        {
                            if (stage.IsSomethingBelow(PosX, temp, brick.GetListItem(bnum)))
                                break;
                            temp += 1;
                        }
                        PosY = temp;
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

                if (stage.IsSomethingBelow(PosX, PosY, brick.GetListItem(bnum)))
                {
                    if (PosY <= 0)
                        Endflag = false;
                    stage.SaveBrick(doublebuffer);
                    PosX = 4;
                    PosY = 0;
                    SetBricks();
                    return;
                }
                if (PosY < 17 - (brick.GetListItem(bnum).GetLength(0)+1))
                {
                    PosY++;
                }
                
            }
            
        }

        
        public void Render()
        {
            //doublebuffer = stage;
            doublebuffer = new MtgClass();
            doublebuffer.SaveBrick(stage);
            doublebuffer.SetBuffer(PosX, PosY, brick.GetListItem(bnum));
            

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Write(doublebuffer.Getbuffer());

        }
        public void GameStart()
        {
            brick.MakeBrick();
            SetBricks();
            while(Endflag)
            {

                DropBrick();
                BrickMove();
                Render();
            }
        }


    }
}
