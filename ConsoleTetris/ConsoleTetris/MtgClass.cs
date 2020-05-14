using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleTetris
{
    class MtgClass
    {
        private string Stage = "0          0\n" +
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
                               "000000000000\n";
        private char[,] buffer = null;
        private int m_Width = 13;
        private int m_Height = 18;
        public MtgClass()
        {
            buffer = new char[m_Height, m_Width+1];
            for(int i = 0; i < m_Height; i++)
            {
                for(int j = 0; j < m_Width; j++)
                {
                    buffer[i, j] = Stage[(m_Width * i) + j];
                }
            }
            
        }
        public void SaveBrick(int p_x, char[,] str)
        {
            for(int i = 0; i < str.GetLength(0); i++)
            {
                for(int j = 0; j<str.GetLength(1); j++)
                    buffer[buffer.GetLength(0) - str.GetLength(0)+i, p_x + j] = str[i, j];
            }
        }
        public void SetBuffer(int p_x, int p_y, char[,] str)
        {
            if(p_y == buffer.GetLength(0) - str.GetLength(0))
            {
                SaveBrick(p_x, str);
            }

            for (int i = 0; i < str.GetLength(0); i++)
            {
                for(int j = 0; j < str.GetLength(1); j++)
                buffer[p_y+i, p_x+j] = str[i,j];
            }
        }
        public char[] Getbuffer()
        {
            char[] returnbuf = new char[m_Height * m_Width + 1];
            for(int i = 0; i < m_Height; i++)
            {
                for(int j = 0; j< m_Width; j++)
                {
                    returnbuf[(m_Width * i) + j] = buffer[i,j]; 
                }
            }
            return returnbuf;
        }

    }
}
