using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPL.src.core
{
    class TplMain
    {
        private string buf = "";
        private char ch;
        private int dt = 0;
        private enum States { S, SYMB, NUM, DLM, FIN, ID, ER, ASGN, COM};
        private States state;
        private string[] Words = { "program", "var", "int", "real", "bool", "begin", "end", "if", "then", "else", "while", "do", "read", "write", "true", "false" };
        private string[] Delimiter = { ".", ";", ",", ":", "=", "(", ")", "+", "-", "*", "/", "=", ">", "<" };
        List<Lex> Lexemes = new List<Lex>();
        private string[] TID;
        private string[] TNUM;
        private string[] TD;


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
                            state = States.ID;
                            continue;
                        }
                        else if (char.IsDigit(sm))
                        {
                            dt = (int)sm;
                            state = States.NUM;
                            continue;
                        }
                        else if (sm == '{')
                        {
                            state = States.COM;
                            continue;
                        }
                        else if (sm == ':')
                        {
                            state = States.ASGN;
                            continue;
                        }
                        else if(sm == '.')
                        {
                            AddLex(Lexemes, 2, 0);
                            state = States.FIN;
                        }
                        else
                        {
                            state = States.DLM;
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
                                AddLex(Lexemes, 1, srch);
                            else
                            {
                                var j = PushLex(TID);
                                AddLex(Lexemes, 4, j);
                            }
                            state = States.S;
                        }
                        break;

                    case States.NUM:
                        if (Char.IsDigit(sm))
                        {
                            dt = dt * 10 + (int)sm;
                            continue;
                        }
                        else
                        {
                            var j = PushLex(TNUM);
                            AddLex(Lexemes, 3, j);
                            state = States.S;
                        }
                        break;
                    case States.DLM:
                        ClearBuf();
                        AddBuf(sm);
                        var r = SerchLex(TD);
                        if (r != 0)
                        {
                            AddLex(Lexemes, 2, r);
                            state = States.S;
                            continue;
                        }
                        else
                            state = States.ER;
                        break;
                    case States.ASGN:
                        if (sm == '=')
                            AddLex(Lexemes, 2, 4);
                        else
                            AddLex(Lexemes, 2, 3);
                        state = States.S;
                        
                        break;
                }
            }

        }
    }
}
