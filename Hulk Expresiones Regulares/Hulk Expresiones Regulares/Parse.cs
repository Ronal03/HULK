﻿
using HulkRE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HulkRE
{
    internal class Parse
    {

        internal static string ParseVar(string[] vars,string input)
        {
            string result = "";
            char caracter = '"';
            int count=input.Count(c => c== caracter);
            if(!(count%2==0) ) { throw new Exception("Las comillas no estan equilibradas"); }
            int pos = 0;
            bool bandera = true;
           for (int i = 0;i<input.Length;i++) 
            {
                if (input[i] == '"'&& bandera) 
                {
                   
                    bandera = false;
                    string sub=input.Substring(pos,i-pos);
                    if (sub == "") { continue; }
                    
                    int indice = sub.IndexOf(vars[0], StringComparison.Ordinal);
                    while (indice != -1)
                    {

                        if ((indice + vars[0].Length == input.Length || !char.IsLetterOrDigit(sub[indice + vars[0].Length])) && (indice - 1 < 0 || !char.IsLetterOrDigit(sub[indice - 1])))
                        {
                            string parteanterior = sub.Substring(0, indice);
                            string parteposterior = sub.Substring(indice + vars[0].Length);
                            sub = parteanterior + vars[1] + parteposterior;
                        }
                        indice = sub.IndexOf(vars[0], StringComparison.Ordinal);

                    }
                    pos = i;
                    result += sub;
                    continue;
                }
                if (input[i]=='"'&&!bandera)
                {
                    bandera = true;
                    string sub = input.Substring(pos, i + 1 - pos);
                    result += sub;
                    pos = i + 1;
                }
            }
          if(pos<input.Length) 
            {
                string pedazofaltante=input.Substring(pos,input.Length-pos);
               
                int indice = pedazofaltante.IndexOf(vars[0], StringComparison.Ordinal);
                while (indice != -1)
                {

                    if ((indice + vars[0].Length == input.Length || !char.IsLetterOrDigit(pedazofaltante[indice + vars[0].Length])) && (indice - 1 < 0 || !char.IsLetterOrDigit(pedazofaltante[indice - 1])))
                    {
                        string parteanterior = pedazofaltante.Substring(0, indice);
                        string parteposterior = pedazofaltante.Substring(indice + vars[0].Length);
                        pedazofaltante = parteanterior + vars[1] + parteposterior;
                    }
                    indice = pedazofaltante.IndexOf(vars[0], StringComparison.Ordinal);

                }
               
                
                return result+=pedazofaltante;

            }
          return result;
        }
        internal static string ParseLlamado(string oldvalue, string newvalue,string body) 
        {
            string result = "";
            char caracter = '"';
            int count = body.Count(c => c == caracter);
            if (!(count % 2 == 0)) { throw new Exception("Las comillas no estan equilibradas"); }
            int pos = 0;
            bool bandera = true;
            for (int i = 0; i < body.Length; i++)
            {
                if (body[i] == '"' && bandera)
                {

                    bandera = false;
                    string sub = body.Substring(pos, i - pos);
                    if (sub == "") { continue; }
                   
                    int indice = sub.IndexOf(oldvalue, StringComparison.Ordinal);
                    while (indice != -1)
                    {
                        if ((indice + oldvalue.Length == body.Length || !char.IsLetterOrDigit(sub[indice + oldvalue.Length])) && (indice - 1 < 0 || !char.IsLetterOrDigit(sub[indice - 1])))
                        {
                            string parteanterior = sub.Substring(0, indice  );
                            string parteposterior = sub.Substring(indice + oldvalue.Length);
                            sub = parteanterior + newvalue + parteposterior;
                        }
                        indice = sub.IndexOf(oldvalue, StringComparison.Ordinal);

                    }
                    pos = i;
                    result += sub;
                    continue;
                }
                if (body[i] == '"' && !bandera)
                {
                    bandera = true;
                    string sub = body.Substring(pos, i + 1 - pos);
                    result += sub;
                    pos = i + 1;
                }
            }
            if (pos < body.Length)
            {
                string pedazofaltante = body.Substring(pos, body.Length - pos);
               
                int indice = pedazofaltante.IndexOf(oldvalue, StringComparison.Ordinal);
                while (indice != -1)
                {
                    
                        if ((indice + oldvalue.Length == body.Length || !char.IsLetterOrDigit(pedazofaltante[indice + oldvalue.Length])) && (indice - 1 < 0 || !char.IsLetterOrDigit(pedazofaltante[indice - 1])))
                        {
                            string parteanterior = pedazofaltante.Substring(0, indice);
                            string parteposterior = pedazofaltante.Substring(indice + oldvalue.Length);
                            pedazofaltante = parteanterior + newvalue + parteposterior;
                        }
                    indice = pedazofaltante.IndexOf(oldvalue,StringComparison.Ordinal);

                }
                return result += pedazofaltante;

            }
            return result;


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
            


            

      
       
