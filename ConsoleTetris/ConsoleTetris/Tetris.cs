using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ConsoleTetris
{
    class Tetris
    {
        MtgClass doublebuffer;
        MtgClass stage = new MtgClass();

        Bricks brick = new Bricks();
        private int nowbnum = 0;
        private int nextbnum = 0;
        private int[,] nowBrick;
        private int[,] nextBrick;

        private int startY = Environment.TickCount;
        private int Checktime = 1000;
        private int PosX = 4; //  
        private int PosY = 0; // max = 16
        private bool Endflag = true;
        private ConsoleKeyInfo info;
        private int RotateCount = 0;

        private int Score = 0;
        private int Level = 1;
        private int LineCleared = 0;
        
        private void SetBricks()
        {
            int temp = nowbnum;
            Random random = new Random(DateTime.Now.Second);
            while(temp == nowbnum)
                nowbnum = random.Next(0, 7);

            nowBrick = brick.GetListItem(nowbnum);
        }
        private void RotateBrick()
        {
            int[,] temp;
            if (nowBrick.Equals(brick.GetListItem(5)))
                return;
            temp = new int[nowBrick.GetLength(1), nowBrick.GetLength(0)];
            for (int i = 0; i < nowBrick.GetLength(0); i++)
            {
                for (int j = 0; j < nowBrick.GetLength(1); j++)
                {
                    temp[nowBrick.GetLength(1) - 1 - j, i] = nowBrick[i, j];
                }
            }
            nowBrick = temp;
            return;
        }
        private void BrickMove()
        {
            Checktime = 500 - 50*(Level-1);
            if (Console.KeyAvailable)
            {
                info = Console.ReadKey(); 
                switch(info.Key)
                {
                    case ConsoleKey.D:
                        if(PosX < 13 - (nowBrick.GetLength(1)))
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
                    case ConsoleKey.W:
                            RotateBrick();
                        break;
                    case ConsoleKey.Spacebar:
                        int temp = PosY;
                        while (true)
                        {
                            if (stage.IsSomethingBelow(PosX, temp, nowBrick))
                                break;
                            temp += 1;
                        }
                        PosY = temp;
                        break;
                    case ConsoleKey.S:
                        Checktime = 100;
                        break;
                    case ConsoleKey.Escape:
                        Endflag = false;
                        break;

                }
            }
        }
        private void DropBrick()
        {
            int Timetemp = Environment.TickCount;
            if (Timetemp - startY > Checktime)
            {
                startY = Timetemp;

                if (stage.IsSomethingBelow(PosX, PosY, nowBrick))
                {
                    if (PosY <= 0)
                        Endflag = false;
                    stage.SaveStage(doublebuffer);
                    PosX = 4;
                    PosY = 0;
                    SetBricks();
                    return;
                }
                PosY++;
            }
        }
        private void CheckScore()
        {
            int Combo = 0;
            int[] FullLine = { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 9 };

            for (int i = 0; i < stage.Height - 1; i++)
            {
                int[] temp = stage.GetLine(i);
                if (FullLine.SequenceEqual(temp))
                {
                    LineCleared++;
                    Combo++;
                    stage.LineRemove(i);
                }
            }

            if (Combo == 1)
                Score += 10 * Level;
            else if (Combo == 2)
                Score += 30 * Level;
            else if (Combo == 3)
                Score += 70 * Level;
            else if (Combo == 4)
                Score += 100 * Level;
        }
        private void CheckLevel()
        {
            if (LineCleared < 5)
                Level = 1;
            else if (LineCleared < 10)
                Level = 2;
            else if (LineCleared < 20)
                Level = 3;
            else if (LineCleared < 30)
                Level = 4;
            else if (LineCleared < 45)
                Level = 5;
            else if (LineCleared < 60)
                Level = 6;
            else if (LineCleared < 75)
                Level = 7;
            else if (LineCleared < 100)
                Level = 8;
            else if (LineCleared < 150)
                Level = 9;
        }


        private void Render()
        {
            
            //doublebuffer = stage;
            doublebuffer = new MtgClass();
            
            doublebuffer.SetBuffer(PosX, PosY, nowBrick);
            doublebuffer.SaveStage(stage);

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 3);
            Console.Write(doublebuffer.Getbuffer());

            Console.SetCursorPosition(50, 5);
            Console.WriteLine("Score : {0}", string.Format("{0:####}", Score));
            Console.SetCursorPosition(50, 7);
            Console.WriteLine("Level : {0}", string.Format("{0:##}", Level));

            
        }
        public void GameStart()
        {
            //brick.MakeBrick();
            SetBricks();
            while(Endflag)
            {
                CheckScore();
                CheckLevel();
                DropBrick();
                BrickMove();
                Render();
                
            }
        }


    }
}
