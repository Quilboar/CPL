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
        private string cr = "";
        private enum States { S, SYMB, NUM, DEL, LST };
        private States state;
        private string[] Words = { "program", "var", "int", "real", "bool", "begin", "end", "if", "then", "else", "while", "do", "read", "write", "true", "false" };
        private char[] Delimiter = { '.', ';', ',', ':', '=', '(', ')', '+', '-', '*', '/', '=', '>', '<' };
    }
}
