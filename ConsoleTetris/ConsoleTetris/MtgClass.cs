using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleTetris
{
    class MtgClass
    {
        private char[,] buffer;
        private int m_x = 13;
        private int m_y = 18;
        public MtgClass(string str)
        {
            buffer = new char[m_y, m_x];

            int count = 0;
            for(int i = 0; i<m_y; i++)
            {
                for(int j = 0; j<m_x; j++)
                {
                    buffer[i,j] = str.ToCharArray()[count++];
                }
            }
        }
        public void SetBuffer(int p_x, int p_y, string str)
        {
            for(int i = p_x; i < p_x+str.Length; i++)
            {
                buffer[p_y, i] = str[i - p_x];
            }
        }
        public char[] Getbuffer()
        {
            char[] returnbuf = new char[m_y * m_x];
            for(int i = 0; i < m_y; i++)
            {
                for(int j = 0; j<m_x; j++)
                {
                    returnbuf[m_y * i + j] = buffer[i,j];
                }
            }
            return returnbuf;
        }

    }
}
