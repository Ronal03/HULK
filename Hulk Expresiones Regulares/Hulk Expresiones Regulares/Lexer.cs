using HulkRE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HulkRE
{

    internal class Lexer
    {
        public static Dictionary<string, string> NameParameters = new Dictionary<string, string>(); 
        public static Dictionary<string,string> NameBody=new Dictionary<string,string>();
        internal static string StartAnal(string input)  
        {
            string[] grupos1 = RegularExp.RegularIf(input);
            string[] grupos2 = RegularExp.RegularLetin(input);
            string[] grupos3 = RegularExp.RegularLlamado(NameParameters, input);
            Match matchfunction = RegularExp.RegularFunction(input);
            Match matcharitmetica = RegularExp.RegularAritmetica(input);
           string[]concstring = RegularExp.RegularConcstring(input);
            string[] print = RegularExp.RegularPrint(input);


            if (matchfunction.Success)
            {
                NameParameters.Add(matchfunction.Groups[1].Value.Trim(), matchfunction.Groups[2].Value.Trim());
                NameBody.Add(matchfunction.Groups[1].Value.Trim(), matchfunction.Groups[3].Value.Trim());
                return "";

            }
            if (grupos2[3] == "true")
            {
                string result = grupos2[2];
                List<string> Variables = Equilibrar.Equilibrar_Var(grupos2[1]);
                string[] Var = new string[Variables.Count];
                List<int> indices = new List<int>();
                for (int i = 0; i < Variables.Count; i++)
                {
                    //Variables[i] = StartAnal(Variables[i]);
                    Match var = RegularExp.RegularVar(Variables[i]);
                    if (var.Success)
                    {
                        Var[i] = var.Groups[1].Value.Trim();
                    }
                    else { throw new Exception("Mal declaracion de variables"); }
                }
                for (int i = 0; i < Var.Length; i++)
                {
                    int index = Array.LastIndexOf(Var, Var[i]);
                    if (index != i) { indices.Add(i); }
                }
                indices.Sort();
                indices.Reverse();
                foreach (int index in indices)
                {
                    Variables.RemoveAt(index);
                }
                for (int i = 0; i < Variables.Count; i++)
                {
                    Match var = RegularExp.RegularVar(Variables[i]);

                    string parsevalue = StartAnal(var.Groups[2].Value.Trim());
                    double m;
                    if (double.TryParse(parsevalue, out m) || (parsevalue.StartsWith('"') && parsevalue.EndsWith('"')) || parsevalue == "true" || parsevalue == "false")
                    {
                        string[] vars = new string[2];
                        vars[0] = var.Groups[1].Value.Trim();
                        vars[1] = parsevalue;
                        string evaluacion = Parse.ParseVar(vars, result);
                        result = evaluacion;
                    }
                    else { throw new Exception("El valor de la variable no coincide con ningun tipo"); }


                }
                return StartAnal(grupos2[0] + result);
            }

            if (grupos1[4] == "true")
            {
                string result = Parse.ParseIf(grupos1[2], grupos1[3], Lexer.StartAnal(grupos1[1].Trim())); 
                return StartAnal(grupos1[0]+result);
            }
            if (grupos3[3] == "true")
            {
                string result = Lexer.NameBody[grupos3[1]];
                List<string> Parametros = Equilibrar.Parameters(NameParameters[grupos3[1]]);
                foreach (string item in Parametros)
                {
                    string patron = @"^([a-zA-Z_]\w*)";
                    Regex var = new Regex(patron);
                    Match verificar=var.Match(item);
                    if (!verificar.Success) { throw new Exception("Parametros incorrectos"); }
                }
                string[] ParametrosLLamado = Equilibrar.EquilibrarParametros(grupos3[2]);
                List<string> ValoresParametros = Equilibrar.Parameters2(ParametrosLLamado[0]);
                for(int i = 0;i<ValoresParametros.Count;i++)
                {
                    ValoresParametros[i] = StartAnal(ValoresParametros[i].Trim());
                }
                if (Parametros.Count != ValoresParametros.Count) { throw new Exception("La funcion " + grupos3[1]+" no acepta "+ValoresParametros.Count+" parametros"); }
                for(int i = 0; i<ValoresParametros.Count; i++)
                {
                    double m;
                    if (double.TryParse(ValoresParametros[i], out m) || (ValoresParametros[i].StartsWith('"') && ValoresParametros[i].EndsWith('"')) || ValoresParametros[i] == "true" || ValoresParametros[i] == "false") { continue; }
                    throw new Exception("El valor del parametro " + Parametros[i] + " no coincide con ningun tipo");
                }
                for (int i = 0; i<ValoresParametros.Count; i++)
                {
                    result = Parse.ParseLlamado(Parametros[i], ValoresParametros[i], result);
                }  
                return StartAnal(grupos3[0] + result + ParametrosLLamado[1]);
            }
            if (print[3] == "true")
            {
                string result = StartAnal(print[1]);
                if (result.StartsWith('"')&&result.EndsWith('"'))
                {
                    result = result.Remove(0, 1);
                    result=result.Remove(result.Length-1, 1);
                    if (print[0] == "" && print[2] == "") { return result; }
                    return StartAnal(print[0] + result + print[2]);
                }
                else { return StartAnal(print[0] + result + print[2]); }
            }
           
            if (input.Contains("==") || input.Contains("<=") || input.Contains(">=") || input.Contains("!=") || input.Contains("<") || input.Contains(">"))
            {

                string[] s=Equilibrar.OperacionesBooleanas(input);
                if (s[3] == "true")
                {
                    return Booleanos.Evaluar(s);
                }
            }
           
            if (concstring[2]=="true" )
            {
                double m;
                string left = StartAnal(concstring[0]);
                string right = StartAnal(concstring[1]);
                if(left.StartsWith('"')&&left.EndsWith('"'))
                {
                    left = left.Remove(left.Length - 1, 1);
                    if(right.StartsWith('"') && right.EndsWith('"'))
                    {
                       right = right.Remove(0, 1);
                        return left + right;
                    }
                    if(double.TryParse(right, out m))
                    {
                        return left + right+"\"";
                    }
                    throw new Exception("El operador @ solo se puede aplicar a strings y numeros");
                }
                if (double.TryParse(left,out m))
                {
                    if(right.StartsWith('"') && right.EndsWith('"'))
                    {
                        right = right.Remove(0, 1);
                        return "\""+left + right;
                    }
                    if(double.TryParse(right, out m))
                    {
                        return left+right;
                    }
                    throw new Exception("El operador @ solo se puede aplicar a strings y numeros");
                }
                throw new Exception("El operador @ solo se puede aplicar a strings y numeros");

            }
            if (input.StartsWith('"')&&input.EndsWith('"'))
            {
                char caracter = '"';
                int count = input.Count(c => c == caracter);
                if (!(count % 2 == 0)) { throw new Exception("Las comillas no estan equilibradas"); }
                else { return input; }

            }

            if (matcharitmetica.Success)
            {
                
                return Parse.ParseArimetica(input);
            }
             double n;     
            if (double.TryParse(input, out n))
            {
                return input;
            }

            throw new Exception("Error de sintaxis");
           
        }
    }
}

