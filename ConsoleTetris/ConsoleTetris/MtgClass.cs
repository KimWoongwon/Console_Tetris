using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace ConsoleTetris
{
    class MTGClass                 // 스테이지 기초정보 
    {                              // 0 : 빈공간 / 1: 블럭으로 차있는 공간 / 4: 벽
        private int[,] buffer = {  // 7: 벽밖의 추가적인 빈공간 / 0: \n문자로 치환
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 7, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 9 },
                                 { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
                               };
                                // 7의 존재 이유는 I형 블럭을 제외한 나머지 블럭은 3by3이라 넘어가지 않기 때문이다.
                                // I형 블럭의 경우 4by4의 형태를 띄고 있어 넘어가져야 최대한 아래쪽까지 닿을수 있기 떄문이다.
        private int m_Width = 14;
        private int m_Height = 18;
        // 배열의 최대 인덱스 값.
        public int Height
        {
            get { return m_Height; }
        }
       
        public void SaveStage(MTGClass p_cls)
        {
            // 블럭과 블럭이 겹쳤을때 빈공간을 그대로 덮어씌울경우 구멍이 나기 때문에 그부분을 위한 저장해주는 부분
            for (int i = 0; i < m_Height; i++)
            {
                for (int j = 0; j < m_Width; j++)
                {
                    // 새로 내려오는 블럭의 빈공간이 기존 차있는 공간과 만났을경우 덮어 쓰지 않는다.
                    if (!(buffer[i, j] == 1 && p_cls.buffer[i,j] == 0)) 
                    buffer[i, j] = p_cls.buffer[i, j];
                }
            }

        }
        public int[] GetLine(int p_index)
        {
            // p_index번째 줄을 바깥에서 가득찼는지 검사하기 위해
            // 1차원 int형 배열로 반환해주는 함수
            // 검사는 바깥에서 진행하기 때문에 스테이지를 복사해 넘겨주는일만 진행한다.
            int[] returnbuf = new int[m_Width];
            for (int i = 0; i < m_Width; i++)
            {
                returnbuf[i] = buffer[p_index, i];
            }
            return returnbuf;
        }
        public void LineRemove(int p_index)
        {
            // 라인이 꽉찼을때 그 라인을 비워주기 위한 1차원 배열
            int[] EmptyLine = { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 };
            // 어느 라인이 지워져야 할지는 바깥에서 이미 p_index값으로 넘어오기때문에
            // 치환을 진행한다.
            for (int i = 0; i < m_Width; i++)
            {
                buffer[p_index, i] = EmptyLine[i];
            }
            // 그후 지워진 부분을 위에있는 블럭들로 채워주어야 하기 때문에 
            // 아래에서 부터 한칸씩 땡겨가며 복사.
            for(int i = p_index; i > 0; i--)
            {
                for(int j = 0; j < m_Width; j++)
                {
                    // 벽과 \n문자 빈공간은 제외하고 한칸씩 떙겨서 내려온다.
                    if (buffer[i, j] == 0 || buffer[i, j] == 1)
                        buffer[i, j] = buffer[i - 1, j];
                }
            }
        }
        public void SetBuffer(int p_x, int p_y, int[,] p_str)
        {
            // 움직이는 블럭을 위해 셋팅해주는 함수
            for (int i = 0; i < p_str.GetLength(0); i++)
            {
                for(int j = 0; j < p_str.GetLength(1); j++)
                {
                    buffer[p_y + i, p_x + j] = p_str[i, j];
                }
                
            }
        }
        public char[] Getbuffer()
        {
            // Console.Write으로 출력해주기 위해 
            // 2차원 int형 배열인 스테이지를 1차원 char형 배열로 변환과 함께 
            // UI를 위해 특수문자로 치환
            char[] returnbuf = new char[m_Height * m_Width];
            for(int i = 0; i < m_Height; i++)
            {
                for(int j = 0; j< m_Width; j++)
                {
                    switch(buffer[i, j])
                    {
                        case 1:
                        case 4:
                            returnbuf[(m_Width * i) + j] = '■';
                            break;
                        case 0:
                            returnbuf[(m_Width * i) + j] = '□';
                            break;
                        case 7:
                            returnbuf[(m_Width * i) + j] = ' ';
                            break;
                        default:
                            returnbuf[(m_Width * i) + j] = '\n';
                            break;
                    }
                }
            }
            return returnbuf;
        }
        
        
        public bool IsSomethingBelow(int p_x, int p_y, int[,] p_str)
        {
            // 현재 블럭의 아래쪽에 무언가 있는지 검사하는 메소드
            for (int i = 0; i <p_str.GetLength(0); i++)
            {
                for (int j = 0; j < p_str.GetLength(1); j++)
                {
                    // 현재 블럭의 최하단과 그다음것을 더했을때 2라면 아래에 전의 블럭이 쌓아진 형태이다.
                    if (buffer[p_y + i + 1, p_x + j] + p_str[i, j] == 2) 
                        return true;
                    // 현재 블럭의 최하단과 그다음것을 더했을떄 5이상 7미만이라면 그것은 벽이라고 판단한다.
                    if (buffer[p_y + i + 1, p_x + j] + p_str[i, j] >= 5 && 
                        buffer[p_y + i + 1, p_x + j] + p_str[i, j] < 7)
                        return true;
                }
            }
            return false;
        }
        public bool IsSomethingRight(int p_x, int p_y, int[,] p_str)
        {
            // 현재 블럭 오른쪽에 무언가 있는지 검사하는 메소드
            for (int i = 0; i < p_str.GetLength(0); i++)
            {
                for (int j = 0; j < p_str.GetLength(1); j++)
                {
                    // 현재 블럭의 가장 우측과 그다음것을 더했을때 2라면 기존의 블럭이 쌓아져 그곳으로 갈수 없다고 판단한다.
                    if (buffer[p_y + i, p_x + j + 1] + p_str[i, j] == 2)
                        return true;
                    // 현재 블럭의 가장 우측과 그다음것을 더했을때 5이상 9이하라면 벽과 함께 빈공간이 존재한다고 판단한다.
                    if (buffer[p_y + i, p_x + j + 1] + p_str[i, j] >= 5 &&
                        buffer[p_y + i, p_x + j + 1] + p_str[i, j] < 9)
                        return true;
                }
            }
            return false;
        }
        public bool IsSomethingLeft(int p_x, int p_y, int[,] p_str)
        {
            // 현재 블럭의 왼쪽에 무언가 있는지 검사하는 메소드
            for (int i = 0; i < p_str.GetLength(0); i++)
            {
                for (int j = 0; j < p_str.GetLength(1); j++)
                {
                    // 현재 블럭의 가장 왼쪽과 그전의 것을 더했을때 2라면 기존의 블럭이 왼쪽에 존재한다고 판단한다.
                    if (buffer[p_y + i, p_x + j - 1] + p_str[i, j] == 2)
                        return true;
                    // 현재 블럭의 가장 왼쪽과 그전의 것을 더했을때 5라면 그곳에는 벽이 있다고 판단한다.
                    // 오른쪽과 하단 검사같이 추가적인 조건이 없는 경우는 스테이지가 왼쪽끝에 붙어있기 때문이다.
                    if (buffer[p_y + i, p_x + j - 1] + p_str[i, j] == 5)
                        return true;
                }
            }
            return false;
        }
    }
}
