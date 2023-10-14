using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HulkRE
{
    internal class RegularExp
    {

        internal static Match RegularPrint(string input)
        {
            // Expresión regular para reconocer la línea de código
            string patron = @"(print)\s*(\()(.*)(\))";
            // Crear un objeto Regex con el patrón
            Regex print = new Regex(patron);
            Match MatchPrint = print.Match(input);
       

            return MatchPrint;

        }

        internal static Match RegularVar(string input)
        {
            // Expresión regular para reconocer la línea de código
            string patron = @"^([a-zA-Z_]\w*)\s*=\s*(.*)$";
            // Crear un objeto Regex con el patrón
            Regex var = new Regex(patron);
            Match Matchvar = var.Match(input);

            return Matchvar;
        }

        internal static string[] RegularLetin(string input)
        {
            string[] grupos=new string[3];
            // Expresión regular para reconocer la línea de código
            //falta verificar que despues del in no este vacio
            string patron = @"^let\s+(.*?)\s+in\s+(.*?)\s*";
            // Crear un objeto Regex con el patrón
            Regex Letin = new Regex(patron);
            Match MatchLetin = Letin.Match(input);
            if (MatchLetin.Success) { grupos = Equilibrar.EquilibrarLet_In(input, grupos); grupos[0] = "true"; }
            else { grupos[0] = "false"; }
            return grupos;
        }

        internal static Match RegularFunction(string input)
        {
            // Expresión regular para reconocer la línea de código
            string patron  = @"^function\s+([a-zA-Z]\w*)\s*\(.*?\)\s*=>\s*(.*?)\s*";
            // Crear un objeto Regex con el patrón
            Regex Function = new Regex(patron);
            Match MatchFunction = Function.Match(input);
            return MatchFunction;
        }

        internal static Match RegularAritmetica(string input)
        {
            // Expresión regular para reconocer la línea de código
            string patron = @"[+\-*/^]";
            // Crear un objeto Regex con el patrón
            Regex Aritmetica = new Regex(patron);
            Match MatchAritmetica = Aritmetica.Match(input);
            return MatchAritmetica;
        }

        internal static Match RegularConcstring(string input)
        {
            // Expresión regular para reconocer la línea de código
            string patron = @"^(.*?)\s*@\s*(.*)$";
            // Crear un objeto Regex con el patrón
            Regex Concstring = new Regex(patron);
            Match MatchConcstring = Concstring.Match(input);
            return MatchConcstring;
        }

        internal static Match RegularString(string input)
        {
            // Expresión regular para reconocer la línea de código
            string patron = @"\s*""(.*)""\s*";
            // Crear un objeto Regex con el patrón
            Regex stringpatron = new Regex(patron);
            Match Matchstringpatron = stringpatron.Match(input);
            return Matchstringpatron;
        }

        internal static Match Regular_Varias_Var(string input)
        {
            // Expresión regular para reconocer la línea de código
            string patron = @"^(.*?)\s*,\s*(.*)$";
            // Crear un objeto Regex con el patrón
            Regex Varias = new Regex(patron);
            Match MatchVarias = Varias.Match(input);
            return MatchVarias;
        }
        //falta coger los espacios en blanco despues del else
        internal static string[] RegularIf(string input)
        {
            string[] grupos = new string[4];
            //falta verificar q despues del else no este vacio
            // Expresión regular para reconocer la línea de código
            string patron = @"^if\s*\((.*?)\)\s*(.*?)\s+else\s+(.*?)\s*";

            // Crear un objeto Regex con el patrón
            Regex ifpatron = new Regex(patron);
            Match Matchif = ifpatron.Match(input);
            if (Matchif.Success) 
            {
                grupos = Equilibrar.EquilibrarIf_Else(input, grupos);
                grupos[3] = "verda";
            }
            else
            {
                grupos[3] = "ment";
            }
        
            
            return grupos ;
        }
    }

}

