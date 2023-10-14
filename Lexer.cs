using HulkRE;
using System;
using System.Collections.Generic;
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
        internal static string StartAnal(string input)
            
        {
            //Errores.Error(input);
            

            
            
            Match matchprint = RegularExp.RegularPrint(input);
            Match matchvar = RegularExp.RegularVar(input);
            string[] grupos2 = RegularExp.RegularLetin(input);
            Match matchfunction = RegularExp.RegularFunction(input);
            Match matcharitmetica = RegularExp.RegularAritmetica(input);
            Match matchconcstring = RegularExp.RegularConcstring(input);
            Match matchstring = RegularExp.RegularString(input);
            Match Match_Varias_Variables = RegularExp.Regular_Varias_Var(input);
            string[]grupos1 = RegularExp.RegularIf(input);
           
            if (matchfunction.Success)
            {
                
                return Parse.ParseFunction(StartAnal(matchfunction.Groups[1].Value.Trim()), StartAnal(matchfunction.Groups[2].Value.Trim()));
                
            }
            if (grupos2[0] == "true") 
            {
                List<string> Variables = Equilibrar.Equilibrar_Var(grupos2[1]);
                for(int i = 0; i < Variables.Count; i++) 
                {
                    Match var = RegularExp.RegularVar(Variables[i]);
                    if (var.Success) 
                    {
                        string parsevalue = StartAnal(var.Groups[2].Value.Trim());
                        double m;
                        if (double.TryParse(parsevalue,out m) || (parsevalue.StartsWith("\"") && parsevalue.EndsWith("\"")) || parsevalue == "true" || parsevalue == "false") 
                        {
                            string[] vars=new string[2];
                            vars[0] = var.Groups[1].Value.Trim();
                            vars[1] = parsevalue;



                        }

                    
                    }
                
                
                }


            }

           
            if (grupos1[3]=="verda" )

            {
               
                 return Parse.ParseIf(grupos1[1], grupos1[2], Lexer.StartAnal(grupos1[0].Trim())); 
            }
           
           
            if (input.Contains("==") || input.Contains("<=") || input.Contains(">=") || input.Contains("!=") || input.Contains("<") || input.Contains(">"))
            {

                if (EvaluarIf.Evaluar(input) == "true") { return "true"; } else { return "false"; }
            }
            if (matchvar.Success )
            {

                
                Parse.ParseVar(matchvar.Groups[1].Value.Trim(), matchvar.Groups[2].Value.Trim(),VariableDiccionario);
                return "";




            }
            if (matchconcstring.Success )
            {
                
                return Parse.ParseConcatString(StartAnal(matchconcstring.Groups[1].Value.Trim()), StartAnal(matchconcstring.Groups[2].Value.Trim()));
            }
            if (matchstring.Success)
            {

                return Parse.ParseString(input);
            }

            if (matcharitmetica.Success)
            {
                
                return Parse.ParseArimetica(input);
            }
             double n;
        
        marca:
            
            if (double.TryParse(input, out n))
                
            {
                
                return input;
            }

            throw new Exception("Error de sintaxis");
            









        }







    }
}

