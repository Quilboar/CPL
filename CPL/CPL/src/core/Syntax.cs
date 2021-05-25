using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPL.src.core
{
    public class Syntax 
    {

        private char[] sm = new char[1];
        private StringReader sr;
        public List<Lex> lexemes;

        private Lex lex;
        public Syntax(List<Lex> _lexemes)
        {
            lexemes = _lexemes;
        }

        public void SE()
        {
            throw new Exception("Синтаксическая ошибка");
        }

        public void SyntaxAnalysis()
        {
            for(int i = 0; i < lexemes.Count; i++)
            {
                if (lexemes[0].val != "program")
                    SE();
               

            }
        }

        
    }
}
