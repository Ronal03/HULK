﻿using System;
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

        internal static string[] RegularPrint(string input)
        {
            string[] result= new string[4];  
            // Expresión regular para reconocer la línea de código
            string patron = @"print\s*(\(.*)";
            // Crear un objeto Regex con el patrón
            Regex print = new Regex(patron);
            Match MatchPrint = print.Match(input);
            if (MatchPrint.Success)
            {
                result = Equilibrar.EquilibrarPrint(input);
                return result;
            }
            result[3] = "false";
            return result;
        }
        internal static string[] RegularPI(string input)
        {
            string[] result = new string[3];
            string patron = @"(.*?)PI(.*)";
            Regex pi = new Regex(patron);
            Match matchpi = pi.Match(input);
            if (matchpi.Success)
            {
                result[0]= matchpi.Groups[1].Value;
                result[1]= matchpi.Groups[2].Value;
                if ((result[0]==""||!char.IsLetterOrDigit(result[0][result[0].Length - 1])) && (result[1]==""|| !char.IsLetterOrDigit(result[1][0]))) { result[2] = "true"; return result; }
            }
            result[2] = "false";    
            return result;  
        }
        internal static string[] RegularLog(string input)
        {
            string[] result= new string[3];
            string patron = @"(.*?)log\s*(\(.*)";
            Regex log = new Regex(patron);
            Match matchlog = log.Match(input);
            if (matchlog.Success) { result[0] = matchlog.Groups[1].Value.Trim(); result[1] = matchlog.Groups[2].Value.Trim(); result[2] = "true"; return result; }
            result[2] = "false";
            return result;
        }
        internal static string[] RegularSen(string input)
        {
            string[] result= new string[3];
            string patron = @"(.*?)sin\s*(\(.*)";
            Regex sen= new Regex(patron);
            Match matchsen = sen.Match(input);
            if (matchsen.Success) { result[0] = matchsen.Groups[1].Value.Trim(); result[1] = matchsen.Groups[2].Value.Trim(); result[2] = "true";return result; }
            result[2] = "false";
            return result;
        
         }
        internal static string[] RegularCos(string input)
        {
            string[] result = new string[3];
            string patron = @"(.*?)cos\s*(\(.*)";
            Regex cos = new Regex(patron);
            Match matchcos = cos.Match(input);
            if (matchcos.Success) { result[0] = matchcos.Groups[1].Value.Trim(); result[1] = matchcos.Groups[2].Value.Trim(); result[2] = "true"; return result; }
            result[2] = "false";
            return result;
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
            string[] grupos=new string[4];
            // Expresión regular para reconocer la línea de código
            string patron = @"let\s+.*\s+in\s+.*";
            // Crear un objeto Regex con el patrón
            Regex Letin = new Regex(patron);
            Match MatchLetin = Letin.Match(input);
            if (MatchLetin.Success) {
               grupos = Equilibrar.EquilibrarLet_In(input, grupos); 
            }
            else { grupos[3] = "false"; }
            return grupos;
        }

        internal static Match RegularFunction(string input)
        {
            // Expresión regular para reconocer la línea de código
            string patron = @"^function\s+([a-zA-Z_]\w*)\s*\((.*?)\)\s*=>\s*(.*)$";
            // Crear un objeto Regex con el patrón
            Regex Function = new Regex(patron);
            Match MatchFunction = Function.Match(input);
            return MatchFunction;
        }
        internal static string[] RegularLlamado(Dictionary<string, string> dicc,string input)
        {
            string[] result= new string[4];
            foreach (KeyValuePair<string,string> item in dicc)
            {
                string clave = item.Key;
                string patron = $@"(.*?)({clave})\s*(\(.*)";
                Match match = Regex.Match(input, patron);
                if (match.Success) { result[0] = match.Groups[1].Value.Trim(); result[3] = "true"; result[1] = match.Groups[2].Value; result[2] = match.Groups[3].Value.Trim();return result; }
            }
           
            result[3] = "false";
            return result;
        }  

        internal static Match RegularAritmetica(string input)
        {
            // Expresión regular para reconocer la línea de código
            string patron = @"[\+\-\*\/\^%]";
            // Crear un objeto Regex con el patrón
            Regex Aritmetica = new Regex(patron);
            Match MatchAritmetica = Aritmetica.Match(input);
            return MatchAritmetica;
        }

        internal static string[] RegularConcstring(string input)
        {
            string[] result= new string[3];
            // Expresión regular para reconocer la línea de código
            string patron = @".*\s*@\s*.*";
            // Crear un objeto Regex con el patrón
            Regex Concstring = new Regex(patron);
            Match MatchConcstring = Concstring.Match(input);
            if(MatchConcstring.Success) { result=Equilibrar.ConcatString(input);return result; }
            else { result[2] = "false";return result; }
           
        }

        internal static string[] RegularIf(string input)
        {
            string[] grupos = new string[6];
            // Expresión regular para reconocer la línea de código
            string patron = @"if\s*\(.*\)\s*.*\s+else\s+.*";

            // Crear un objeto Regex con el patrón
            Regex ifpatron = new Regex(patron);
            Match Matchif = ifpatron.Match(input);
            if (Matchif.Success) 
            {
                grupos = Equilibrar.EquilibrarIf_Else(input, grupos);
            }
            else
            {
                grupos[4] = "false";
            }
            return grupos ;
        }
    }

}

