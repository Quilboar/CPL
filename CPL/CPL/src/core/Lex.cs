using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPL.src.core
{
    public class Lex
    {
        public readonly int id;
        public readonly int lex;
        public readonly string val;

        public Lex(int id, int lex, string val)
        {
            this.id = id;
            this.lex = lex;
            this.val = val;
        }
    }
}
