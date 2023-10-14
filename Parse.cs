
using HulkRE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HulkRE
{
    internal class Parse
    {
        internal static string ParsePrint(string expre)
        {
            return expre;
        }
        internal static string ParseVar(string[] vars,string input)
        {
            char caracter = '"';
            int count=input.Count(c => c== caracter);
            if(!(count%2==0) ) { throw new Exception("Las comillas no estan equilibradas"); }
            int pos = 0;
           for (int i = 0;i<input.Length;i++) 
            {
                if (input[i] == '"') 
                {
                    string sub=input.Substring(pos,i-pos);
                    string sub1 = sub;
                    if(sub.Contains())

                }
            
            }
        }

        internal static string ParseLetin( string cuerpo) { return Lexer.StartAnal(cuerpo); }

        internal static string ParseFunction(string expre, string cuerpo) { throw new NotImplementedException(); }

        internal static string ParseString(string cuerpo) { cuerpo = cuerpo.Substring(1, cuerpo.Length - 2); return cuerpo; }

        internal static string ParseConcatString(string expre1, string expre2)
        {

            return expre1 + expre2;
        }

        internal static string ParseArimetica(string input)
        {

            return Aritmetica.Evaluar(input);

        }

        internal static string ParseIf(string input, string input2,string condicion)
        {
            if (condicion == "true") { return Lexer.StartAnal(input); }
            else { return Lexer.StartAnal(input2); }
            

        }

    }
}
            


            

      
       
