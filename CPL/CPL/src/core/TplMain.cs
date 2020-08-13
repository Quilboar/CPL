﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPL.src.core
{
    class TplMain
    {
        private string buf = "";
        private char[] sm = new char[1];
        private int dt = 0;
        private enum States { S, SYMB, NUM, DLM, FIN, ID, ER, ASGN, COM };
        private States state;
        private string[] Words = { "program", "var", "integer", "real", "bool", "begin", "end", "if", "then", "else", "while", "do", "read", "write", "true", "false" };
        private string[] Delimiter = { ".", ";", ",", ":=", "(", ")", "+", "-", "*", "/", "=", ">", "<" };
        public List<Lex> Lexemes = new List<Lex>();
        public string[] TID = { "" };
        public string[] TNUM = { "" };
       
        StringReader sr;

        private void GetNext()
        {
            sr.Read(sm, 0, 1);
        }

        private void ClearBuf()
        {
            buf = "";
        }

        private void AddBuf(char symb)
        {
            buf += symb;
        }

        private (int, string) SerchLex(string[] lexes)
        {
            var srh = Array.FindIndex(lexes, s => s.Equals(buf)); 
            if (srh != -1)
                return (srh, buf);             
            else return (-1, "");
        }

        private (int, string) PushLex(string[] lexes, string buf)
        {
            var srh = Array.FindIndex(lexes, s => s.Equals(buf));
            if (srh != -1)
                return (-1, "");
            else
            {
                Array.Resize(ref lexes, lexes.Length + 1);
                lexes[lexes.Length - 1] = buf;
                return (lexes.Length - 1, buf);
            }
        }

        private void AddLex(List<Lex> lexes, int key, int val, string lex)
        {
            lexes.Add(new Lex(key, val, lex));
        }

        public void Analysis(string text)
        {
            sr = new StringReader(text);
            while (state != States.FIN)
            {
                switch (state)
                {

                    case States.S:
                        if (sm[0] == ' ' || sm[0] == '\n' || sm[0] == '\t' || sm[0] == '\0' || sm[0] == '\r' )
                            GetNext();
                        else if (Char.IsLetter(sm[0]))
                        {
                            ClearBuf();
                            AddBuf(sm[0]);
                            state = States.ID;
                            GetNext();
                        }
                        else if (char.IsDigit(sm[0]))
                        {
                            dt = (int)(sm[0]-'0');
                            GetNext();
                            state = States.NUM;
                            
                        }
                        else if (sm[0] == '{')
                        {
                            state = States.COM;
                            GetNext();
                        }
                        else if (sm[0] == ':')
                        {
                            state = States.ASGN;
                            ClearBuf();
                            AddBuf(sm[0]);
                            GetNext();
                        }
                        else if (sm[0] == '.')
                        {
                            AddLex(Lexemes, 2, 0, sm[0].ToString());
                            state = States.FIN;
                        }
                        else
                        {
                            state = States.DLM;

                        }

                        break;
                    case States.ID:
                        if (Char.IsLetterOrDigit(sm[0]))
                        {
                            AddBuf(sm[0]);
                            GetNext();
                        }
                        else
                        {
                            var srch = SerchLex(Words);
                            if (srch.Item1 != -1)
                                AddLex(Lexemes, 1, srch.Item1, srch.Item2);
                            else
                            {
                                var j = PushLex(TID, buf);
                                AddLex(Lexemes, 4, j.Item1, j.Item2);
                            }
                            state = States.S;
                        }
                        break;

                    case States.NUM:
                        if (Char.IsDigit(sm[0]))
                        {
                            dt = dt * 10 + (int)(sm[0]-'0');
                            GetNext();
                        }
                        else
                        {

                            var j = PushLex(TNUM, dt.ToString());
                            AddLex(Lexemes, 3, j.Item1, j.Item2);
                            state = States.S;
                        }
                        break;
                    case States.DLM:
                        ClearBuf();
                        AddBuf(sm[0]);
                       
                        var r = SerchLex(Delimiter);
                        if (r.Item1 != -1)
                        {
                            AddLex(Lexemes, 2, r.Item1, r.Item2);
                            state = States.S;
                            GetNext();
                        }
                        else
                            state = States.ER;
                        break;
                    case States.ASGN:
                        if (sm[0] == '=')
                        {
                            AddBuf(sm[0]);
                            AddLex(Lexemes, 2, 4, buf);
                            ClearBuf();
                            GetNext();
                        }
                        else
                            AddLex(Lexemes, 2, 3, buf);
                        state = States.S;

                        break;
                    case States.ER:
                        MessageBox.Show("Ошибка в программе");
                        state = States.FIN;
                        break;
                    case States.FIN:
                        MessageBox.Show("Лексический анализ закончен");
                        break;
                }
               
            }
            


        }
    }
}
