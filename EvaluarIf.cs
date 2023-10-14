using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HulkRE
{
    internal class EvaluarIf
    {
        internal static string Evaluar(string texto)

            
        {
            double a = 0;
            double b = 0;
            // Expresión regular para reconocer la línea de código
            string patron1 = @"^(.*?)\s*==\s*(.*?)$";
            // Crear un objeto Regex con el patrón
            Regex igualnumpatron = new Regex(patron1);
            Match Matchstigualnum = igualnumpatron.Match(texto);
            if(Matchstigualnum.Success ) { if ( Lexer.StartAnal(Matchstigualnum.Groups[1].Value.Trim()) == Lexer.StartAnal(Matchstigualnum.Groups[2].Value.Trim() ) ) return "true"; }


            // Expresión regular para reconocer la línea de código
            string patron2 = @"^(.*?)\s*>=\s*(.*?)$";
            // Crear un objeto Regex con el patrón
            Regex mayorigualnumpatron = new Regex(patron2);
            Match Matchmayorigualnum = mayorigualnumpatron.Match(texto);
           

            if (Matchmayorigualnum.Success)
            {
                
                a = Double.Parse(Lexer.StartAnal(Matchmayorigualnum.Groups[1].Value.Trim()));
                b = Double.Parse(Lexer.StartAnal(Matchmayorigualnum.Groups[2].Value.Trim())); if (a>=b) return "true"; }

            // Expresión regular para reconocer la línea de código
            string patron3 = @"^(.*?)\s*<=\s*(.*?)$";
            // Crear un objeto Regex con el patrón
            Regex menorigualnumpatron = new Regex(patron3);
            Match Matchmenorigualnum = menorigualnumpatron.Match(texto);
             

            if (Matchmenorigualnum.Success) {
                a = Double.Parse(Lexer.StartAnal(Matchmenorigualnum.Groups[1].Value.Trim()));
                b = Double.Parse(Lexer.StartAnal(Matchmenorigualnum.Groups[2].Value.Trim())); if (a <= b) return "true"; }

            string patron4 = @"^(.*?)\s*>\s*(.*?)$";
            // Crear un objeto Regex con el patrón
            Regex mayornumpatron = new Regex(patron4);
            Match Matchmayornum = mayornumpatron.Match(texto);
           

            if (Matchmayornum.Success)
            {
                a = Double.Parse(Lexer.StartAnal(Matchmayornum.Groups[1].Value.Trim()));
                b = Double.Parse(Lexer.StartAnal(Matchmayornum.Groups[2].Value.Trim())); if (a > b) return "true"; }


            string patron5 = @"^(.*?)\s*<\s*(.*?)$";
            // Crear un objeto Regex con el patrón
            Regex menornumpatron = new Regex(patron5);
            Match Matchmenornum = menornumpatron.Match(texto);
           

            if (Matchmenornum.Success)
            {
                a = Double.Parse(Lexer.StartAnal(Matchmenornum.Groups[1].Value.Trim()));
                b = Double.Parse(Lexer.StartAnal(Matchmenornum.Groups[2].Value.Trim())); if (a < b) return "true"; }


            string patron6 = @"^(.*?)\s*!+\s*=\s*(.*?)$";

            // Crear un objeto Regex con el patrón
            Regex diferentenumpatron = new Regex(patron6);
            Match diferentenum = diferentenumpatron.Match(texto);
            

            if (diferentenum.Success) {
                a = Double.Parse(Lexer.StartAnal(diferentenum.Groups[1].Value.Trim()));
                b = Double.Parse(Lexer.StartAnal(diferentenum.Groups[2].Value.Trim())); if (a != b) return "true"; }
            return "false"; 







        }
    }
}
