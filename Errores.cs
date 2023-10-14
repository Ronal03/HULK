using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HulkRE
{
    internal class Errores
    {
        internal static void Error(string input)
        {
            List<string>[] errors = new List<string>[10];
            bool BanderaMadre=false;
            bool bandera = false;
            int count_open_parentesis=0;
            for(int i = 0; i < input.Length; i++) 
            {
                if(i==0)if (i==0&input.Substring(i, 5) != "print") { errors[0].Add("print"); }
                if (input[i] == ' ') { continue; }
                if (input[i] == '(') { count_open_parentesis++;bandera = true; }
                if (input[i] == ')') {  count_open_parentesis--; }
                if (input[i] == ')' && count_open_parentesis == 0) { if (!bandera) errors[0].Add("("); }
               

            }
            if(count_open_parentesis > 0) { errors[0] = new List<string> { };  errors[0].Add(")"); }
            if (errors[0].Count == 0) { BanderaMadre = true; }
            for(int i=0; i < errors[0].Count; i++)
            {
                if(i==0) { Console.Write("Falta el parametro"); }
                else { Console.Write(" " + errors[0][i-1]); }
            }

        }
    }
}
