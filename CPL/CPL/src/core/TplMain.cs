using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPL.src.core
{
    class TplMain
    {
        private string buf = "";
        private char ch;
        private enum States { S, SYMB, NUM, DEL, LST };
        private States state;
        private string[] Words = { "program", "var", "int", "real", "bool", "begin", "end", "if", "then", "else", "while", "do", "read", "write", "true", "false" };
        private char[] Delimiter = { '.', ';', ',', ':', '=', '(', ')', '+', '-', '*', '/', '=', '>', '<' };
        

        private void GetNext(char symb)
        {
            ch = symb;
        }

        private void ClearBuf()
        {
            buf = "";
        }

        private void AddBuf(char symb)
        {
            buf += symb;
        }

        private int SerchLex(string[] lexes)
        {
            var srh = lexes.AsEnumerable().Where(x => x == buf).ToString();
            if (srh != "")
                return Array.IndexOf(lexes, srh);
            else return 0;
        }

        private int PushLex(string[] lexes)
        {
            var srh = lexes.AsEnumerable().Where(x => x == buf).ToString();
            if (srh != "")
                return 0;
            else
            {
                Array.Resize(ref lexes, lexes.Length + 1);
                lexes[lexes.Length - 1] = srh;
                return lexes.Length - 1;
            }
        }

        private void AddLex(string[] lexes, string val)
        {
            Array.Resize(ref lexes, lexes.Length + 1);
            lexes[lexes.Length - 1] = val;
        }

        public void Analysis(string text)
        {
            foreach(char sm in text)
            {
                
            }

        }
    }
}
