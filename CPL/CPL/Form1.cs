using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CPL.src.core;

namespace CPL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            TplMain tpl = new TplMain();
            tpl.Analysis(textBox1.Text);
            
            foreach(var lex in tpl.Lexemes)
            {
                switch (lex.id)
                {
                    case 1:
                        textBox2.Text += "id: " + lex.id + " lex: " + lex.lex + " val: " + lex.val + " |" + " служебные слова "+ Environment.NewLine;
                        break;
                    case 2:
                        textBox2.Text += "id: " + lex.id + " lex: " + lex.lex + " val: " + lex.val + " |" + " ограничители " + Environment.NewLine;
                        break;
                    case 3:
                        textBox2.Text += "id: " + lex.id + " lex: " + lex.lex + " val: " + lex.val + " |" + " числа " + Environment.NewLine;
                        break;
                    case 4:
                        textBox2.Text += "id: " + lex.id + " lex: " + lex.lex + " val: " + lex.val + " |" + " идентификатор " + Environment.NewLine;
                        break;
                        
                }
                

               
            }
           
        }
    }
}
