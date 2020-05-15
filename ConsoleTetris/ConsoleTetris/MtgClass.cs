using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleTetris
{
    class MtgClass
    {
        private int[,] buffer = {
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9 },
                                 { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9 },
                                 //{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }
                               };
        //private char[,] buffer = null;
        private int m_Width = 13;
        private int m_Height = 17;
        public MtgClass()
        {
            
        }
        public MtgClass(MtgClass p_cls)
        {
            buffer = p_cls.buffer;            
        }
        public void SaveBrick(MtgClass p_cls)
        {
            for (int i = 0; i < m_Height; i++)
            {
                for (int j = 0; j < m_Width; j++)
                {
                    buffer[i, j] = p_cls.buffer[i, j];
                }
            }

        }
        public void SetBuffer(int p_x, int p_y, int[,] str)
        {
            for (int i = 0; i < str.GetLength(0); i++)
            {
                for(int j = 0; j < str.GetLength(1); j++)
                {
                    buffer[p_y + i, p_x + j] = str[i, j];
                }
                
            }
        }
        public char[] Getbuffer()
        {
            char[] returnbuf = new char[m_Height * m_Width];
            for(int i = 0; i < m_Height; i++)
            {
                for(int j = 0; j< m_Width; j++)
                {
                    if(buffer[i,j] == 1)
                        returnbuf[(m_Width * i)  + j] = '■'; 
                    else if(buffer[i,j] == 0)
                        returnbuf[(m_Width * i) + j] = '□';
                    else if(buffer[i,j] == 5)
                        returnbuf[(m_Width * i) + j] = ' ';
                    else 
                        returnbuf[(m_Width * i) + j] = '\n';
                }
            }
            return returnbuf;
        }
        public bool IsSomethingBelow(int p_x, int p_y, int[,] p_str)
        {

            int size = p_str.Length / p_str.GetLength(0) - 1;
            if (p_str.Length == 4 && p_str.GetLength(0) == 2)
                size += 1;
            else if (p_str.Length == 4 && p_str.GetLength(1) == 4)
                size = 1;
            //Max_y = p_y - size;
            for (int i = 0; i < p_str.GetLength(1); i++)
            {
                if (buffer[(p_y + size), p_x + i] == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
