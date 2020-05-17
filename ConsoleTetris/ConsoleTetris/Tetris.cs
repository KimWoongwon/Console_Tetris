using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ConsoleTetris
{
    public enum BrickNumber { Zbrick = 0, Sbrick = 1, Tbrick, Lbrick, Jbrick, Obrick, Ibrick };// 가독성을 위한 enum형 변수
    class Tetris
    {
        MTGClass doublebuffer;              // 더블버퍼 구현을 위한 MTGclass Render()메소드에서 할당.
        MTGClass stage = new MTGClass();    // 계속 초기화되는 더블버퍼와는 달리 테트리스 현황을 저장할 변수
                                            // 최초 1회만 할당 및 생성.
        Bricks brick = new Bricks();        // 테트로미노를 위한 클래스 생성.
        private int nowbnum = 0;            // 어떤 모양의 블럭인지 정해줄 index값.
        private int[,] nowBrick;            // 현재 출력되고 있는 블럭

        private int startY = Environment.TickCount; // 블럭드랍을 구현하기 위한 최초 시간.
        private int Checktime = 500;                // 블럭 드랍의 주기 0.5초
        private int PosX = 4; // 움직이는 블럭의 좌상단 좌표 
        private int PosY = 0; // 
        private bool Endflag = true; // 게임의 종료 시점을 나타내는 변수
        private ConsoleKeyInfo info; // 플레이어의 키값을 받기 위한 변수
        private bool Iturn = false;  // I자 블럭의 예외처리를 위한 bool형 변수 true = ㅣ false = ㅡ 모양
        private int TturnCount = 0;  // T자 블럭의 예외처리를 위한 int형 변수 
                                     // T자의 경우 4가지 모양중 2가지를 예외처리 하기위해 int형으로 선언
        private int Score = 0;      //점수
        private int Level = 1;      //레벨
        private int LineCleared = 0;//레벨을 결정하는 줄을 지운 횟수

        
        private void SetBricks()    //처음과 블럭이 멈췄을때 새로운 블럭을 만들어 주기위한 메소드
        {
            int temp = nowbnum;     // 블럭이 중복되어 나오지 않도록 하기위한 임시 int형 변수
            Random random = new Random(DateTime.Now.Second);    // 랜덤한 값을 설정. DataTime으로 시드값을 넘겨준다.
            while (temp == nowbnum) // 블럭 중복 방지 조건
                nowbnum = random.Next(0, 7);    // 블럭이 7가지 이기 때문에 0~6까지의 값이 나오도록 설정

            nowBrick = brick.GetListItem(nowbnum); // 현재 블럭에 리스트에서 랜덤하게 뽑아 할당.
            if(nowbnum == (int)BrickNumber.Ibrick) // I 모양 일경우
                Iturn = false;
            if(nowbnum == (int)BrickNumber.Tbrick) // T 모양 일경우
                TturnCount = 0;
        }
        private void RotateBrick() // 블럭 회전 함수
        {
            int[,] temp;    // 회전된 블럭을 담을 임시 2차원배열 변수

            if (nowbnum == (int)BrickNumber.Obrick) // O모양 블럭의 경우 회전이 필요하지 않기에 그냥 리턴
                return;
                
            if (nowbnum == (int)BrickNumber.Tbrick) // T자형 블럭의 경우 회전은 4개 0,1,2,3 으로 회전된 상태를 나타낸다.
                TturnCount = ++TturnCount % 4;

            // O형 블럭을 제외한 나머지 블럭의 경우 임시 변수에 그 크기만큼 할당을 해준다.
            temp = new int[nowBrick.GetLength(0), nowBrick.GetLength(1)];
            if (nowbnum == (int)BrickNumber.Ibrick) // I형 블럭의 경우
            {
                if (stage.IsSomethingLeft(PosX, PosY, nowBrick)) 
                {
                    //왼쪽 벽에 닿았을 경우의 예외처리
                    if (Iturn)  
                    {
                        // 항상 1,1 기준으로 회전하기 때문에 
                        Iturn = false; // ㅣ모양에서 ㅡ모양으로 바뀔때
                        PosX++;  // 왼쪽 벽을 뚫어서 끼이는 경우를 강제로 한칸 이동
                        for (int i = 0; i < 4; i++)
                            temp[1, i] = nowBrick[i, 1];
                        
                    }
                    else
                    {
                        Iturn = true; // ㅡ모양에서 ㅣ모양으로 바뀔때 
                        PosX--; // 원래의 자리로 돌아가게끔 설정.
                        for (int i = 0; i < 4; i++)
                            temp[i, 1] = nowBrick[1, i];
                    }
                }
                else
                {
                    // 왼쪽벽이나 오른쪽 벽에 닿지 않았을 경우의 처리.
                    // 왼쪽이나 오른쪽에 아무것도 없기에 그냥 회전 처리
                    if (Iturn)
                    {
                        Iturn = false;
                        for (int i = 0; i < 4; i++)
                            temp[1, i] = nowBrick[i, 1];
                    }
                    else
                    {
                        Iturn = true;
                        for (int i = 0; i < 4; i++)
                            temp[i, 1] = nowBrick[1, i];
                    }
                }
                if (stage.IsSomethingRight(PosX, PosY, nowBrick))
                {
                    // 오른쪽 벽에 완전히 붙어있을때의 예외처리
                    // 오른쪽 벽에 아무것도 없는 경우로 회전한뒤에 강제로 x좌표를 이동하여 구현.
                    if (Iturn)
                        PosX += 2;
                    else
                        PosX -= 2;
                }
                else if (stage.IsSomethingRight(PosX + 1, PosY, nowBrick))
                {
                    // 만약 오른쪽 벽으로부터 1칸 떨어져 있을경우
                    // ㅣ모양에서 ㅡ모양으로 회전시 끼이는 경우가 발생해 예외처리
                    // 1칸만 이동시켜주면 된다.
                    if (Iturn)
                        PosX += 1;
                    else
                        PosX -= 1;
                }

            }
            else
            {
                // I형 O형 블럭을 제외한 나머지 블럭의 회전 구현
                // 3 by 3 배열으로 인해 항상 1,1을 기준으로 회전
                for (int i = 0; i < nowBrick.GetLength(0); i++)
                {
                    for (int j = 0; j < nowBrick.GetLength(1); j++)
                    {
                        temp[nowBrick.GetLength(1) - 1 - j, i] = nowBrick[i, j];
                    }
                }
                // 아래는 T형 블럭의 예외 처리.
                if(TturnCount == 2)
                {
                    // ㅓ모양일경우 오른쪽 벽에 딱 붙어 회전시 ㅜ모양이 될때
                    if(stage.IsSomethingRight(PosX - 1, PosY, temp))
                    {
                        // 오른쪽에 무언가 있다면 한칸 왼쪽으로 이동.
                        PosX -= 1;
                    }
                    
                }
                else if (TturnCount == 0)
                {
                    // ㅏ모양일 경우 왼쪽 벽에 딱 붙어 회전시 ㅗ모양이 될때
                    if (stage.IsSomethingLeft(PosX + 1, PosY, temp))
                    {
                        // 왼쪽에 무언가 있다면 한칸 오른쪽으로 이동
                        PosX += 1;
                    }
                }
            }
            // 회전이 된 최종 모형을 현재 블럭으로 설정.
            nowBrick = temp;
        }
        private void BrickMove()
        {
            Checktime = 500 - 50 * (Level - 1); // 레벨에 따른 속도 조절, 레벨당 0.05초씩 주기 감소
            if (Console.KeyAvailable) // 키가 눌렸을 경우
            {
                info = Console.ReadKey(); // 눌린 키값을 받아서
                switch(info.Key) // 비교
                {
                    case ConsoleKey.D: // 오른쪽에 무언가 없다면 오른쪽으로 한칸 이동
                        if (!stage.IsSomethingRight(PosX, PosY, nowBrick))
                            PosX++;
                        break;
                    case ConsoleKey.A: // 왼쪽에 무언가 없다면 왼쪽으로 한칸 이동 
                        if(!stage.IsSomethingLeft(PosX, PosY, nowBrick))
                            PosX--;
                        break;
                    case ConsoleKey.W: // w키 입력시 블럭 회전
                            RotateBrick();
                        break;
                    case ConsoleKey.Spacebar: // spacebar 입력시 최대한 아래로 블럭이 이동
                        int temp = PosY;
                        while (true)
                        {
                            if (stage.IsSomethingBelow(PosX, temp, nowBrick))
                                break;
                            temp += 1;
                        }
                        PosY = temp;
                        // 임시 변수를 만들어 temp 다음에 무엇이 있나 체크후 없다면 증가
                        // 있다면 PosY를 temp로 설정
                        break;
                    case ConsoleKey.S: // s키 꾹 입력시 블럭이 떨어지는 주기가 0.1초로 감소
                        Checktime = 100;
                        break;
                    case ConsoleKey.Escape: // esc버튼 입력시 게임 종료
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
                // 현재시간 - 시작점이 주기보다 크다면 다음을 실행하고
                startY = Timetemp; // 시작점을 다시 현재시간으로 초기화
                
                if (stage.IsSomethingBelow(PosX, PosY, nowBrick))
                {
                    // 아래에 무엇이 있다면 더이상 블럭이 내려가지 않고
                    // 스테이지에 저장후 블럭의 좌표 및 새로운 블럭으로 재설정
                    if (PosY <= 0)
                        Endflag = false;
                    stage.SaveStage(doublebuffer);
                    PosX = 4;
                    PosY = 0;
                    SetBricks();
                    return;
                }
                // 주기마다 좌표값을 증가시켜 블럭이 떨어지는것 처럼 구현
                PosY++;
            }
        }
        private void CheckScore()
        {
            // 점수계산 메소드
            int Combo = 0;
            int[] FullLine = { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 9 };
            // 한줄이 가득찼을때의 정보를 가지는 1차원 배열

            for (int i = 0; i < stage.Height - 2; i++) // -2의 경우 스테이지에서 아래 두줄은 벽과 빈공간으로 이루어져있기에 그부분은 제외하고 비교
            {
                int[] temp = stage.GetLine(i); // 저장된 스테이지에서 한줄씩 가져와서
                if (FullLine.SequenceEqual(temp)) // 한줄이 가득찼는지 비교
                {
                    LineCleared++;  
                    Combo++;             // 한번에 여러개의 줄을 지울경우 추가점수를 위한 콤보
                    stage.LineRemove(i); // 줄을 지워주는 함수
                }
            }

            if (Combo == 1)     // 점수 계산 부분
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
            // 레벨 설정을 위한 단순한 메소드
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
            // 최대 9레벨
        }


        private void Render()
        {
            // 화면을 그려주는 메소드
            doublebuffer = new MTGClass();
            
            doublebuffer.SetBuffer(PosX, PosY, nowBrick);
            doublebuffer.SaveStage(stage);
            // 새로운 스테이지를 만들고 거기에 블럭이 내려오는 부분을 그린후에
            // SaveStage메소드에서 추가적으로 기존에 있던 블럭들의 정보를 추가 연산

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 3);
            Console.Write(doublebuffer.Getbuffer());
            // 출력부분

            Console.SetCursorPosition(50, 5);
            Console.WriteLine("Score : {0}", string.Format("{0:####}", Score));
            Console.SetCursorPosition(50, 7);
            Console.WriteLine("Level : {0}", string.Format("{0:##}", Level));
            // 점수출력
            
        }
        public void GameStart()
        {
            SetBricks(); // 초기에 블럭의 정보가 없기에 1회 호출
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
