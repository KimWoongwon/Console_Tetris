using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace ConsoleTetris
{
    class MtgClass
    {
        private int[,] buffer = {
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 },
                                 { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 9 },
                                 //{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }
                               };
        //private char[,] buffer = null;
        private int m_Width = 13;
        private int m_Height = 17;
        private int m_size = 0;
        public int Width
        {
            get { return m_Width; }
        }
        public int Height
        {
            get { return m_Height; }
        }
        public int Size
        {
            get { return m_size; }
        }
       
        public void SaveStage(MtgClass p_cls)
        {
            for (int i = 0; i < m_Height; i++)
            {
                for (int j = 0; j < m_Width; j++)
                {
                    if (!(buffer[i, j] == 1 && p_cls.buffer[i,j] == 0))
                    buffer[i, j] = p_cls.buffer[i, j];
                }
            }

        }
        public void LineRemove(int p_index)
        {
            int[] EmptyLine = { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9 };
            for (int i = 0; i < m_Width; i++)
            {
                buffer[p_index, i] = EmptyLine[i];
            }
            for(int i = p_index; i > 0; i--)
            {
                for(int j = 0; j < m_Width; j++)
                {
                    if (buffer[i, j] == 0 || buffer[i, j] == 1)
                        buffer[i, j] = buffer[i - 1, j];
                }
            }
        }
        
        //public void SaveBrick(MtgClass p_cls)
        //{
        //    for (int i = 0; i < m_Height; i++)
        //    {
        //        for (int j = 0; j < m_Width; j++)
        //        {
        //            buffer[i, j] = p_cls.buffer[i, j];
        //        }
        //    }

        //}
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
                    switch(buffer[i, j])
                    {
                        case 1:
                        case 4:
                            returnbuf[(m_Width * i) + j] = '■';
                            break;
                        case 0:
                            returnbuf[(m_Width * i) + j] = '□';
                            break;
                        default:
                            returnbuf[(m_Width * i) + j] = '\n';
                            break;
                    }
                }
            }
            return returnbuf;
        }
        public int[] GetLine(int p_index)
        {
            int[] returnbuf = new int[m_Width];
            for (int i = 0; i < m_Width; i++)
            {
                returnbuf[i] = buffer[p_index, i];
            }
            return returnbuf;
        }
        //public bool IsSomethingBelow(int p_x, int p_y, int[,] p_str)
        //{
        //    size = p_str.Length / p_str.GetLength(1);

        //    if (buffer[p_y + size, p_x] == 4)
        //        return true;
           
        //    for (int i = 0; i < p_str.GetLength(1); i++)
        //    {
        //        if(p_str[p_str.GetLength(0)-1,i] == 1)
        //        {
        //            if (buffer[p_y + size, p_x + i] == 1)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}
        public bool IsSomethingBelow(int p_x, int p_y, int[,] p_str)
        {
            m_size = p_str.Length / p_str.GetLength(1);
            if (buffer[p_y + m_size, p_x] == 4)
                return true;
            for (int i = 0; i < p_str.GetLength(0); i++)
            {
                for (int j = 0; j < p_str.GetLength(1); j++)
                {
                    if (buffer[p_y + i +1, p_x + j] + p_str[i, j] == 2)
                        return true;
                }
            }
            return false;
        }
    }
}
