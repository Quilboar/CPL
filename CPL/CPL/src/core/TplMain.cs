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
        private enum States { S, SYMB, NUM, DEL, LETT, ID };
        private States state;
        private string[] Words = { "program", "var", "int", "real", "bool", "begin", "end", "if", "then", "else", "while", "do", "read", "write", "true", "false" };
        private string[] Delimiter = { ".", ";", ",", ":", "=", "(", ")", "+", "-", "*", "/", "=", ">", "<" };
        List<Lex> Lexemes = new List<Lex>();


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

        private void AddLex(List<Lex> lexes, int key, int val)
        {
            lexes.Add(new Lex(key, val));
        }

        public void Analysis(string text)
        {
            foreach(char sm in text)
            {
                switch (state)
                {
                    case States.S:
                        //if(sm == ' ' || sm == '\n' || sm == '\t')
                        if (Char.IsLetter(sm))
                        {
                            ClearBuf();
                            AddBuf(sm);
                            state = States.LETT;
                            continue;
                        }

                            
                        break;
                    case States.ID:
                        if (Char.IsLetterOrDigit(sm))
                        {
                            AddBuf(sm);
                            continue;
                        }
                        else
                        {
                            var srch = SerchLex(Words);
                            if (srch != 0)
                            {
                                AddLex(Lexemes, 1, srch);
                                state = States.S;
                            }
                            else
                            {

                            }
                                
                        }
                        break;
                }
            }

        }
    }
}
