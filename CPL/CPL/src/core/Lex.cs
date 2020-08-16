using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPL.src.core
{
    public class Lex
    {
        public int id;
        public int lex;
        public string val;

        public Lex(int _id, int _lex, string _val)
        {
            id = _id;
            lex = _lex;
            val = _val;
        }
    }
}
