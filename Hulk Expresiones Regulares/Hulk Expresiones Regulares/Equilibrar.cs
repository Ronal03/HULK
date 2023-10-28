using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace HulkRE
{
    internal class Equilibrar
    {
        internal static string[] OperacionesBooleanas(string input)
        {
            string[] result = new string[4];
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '"')
                {
                    int index = input.IndexOf('"', i + 1);
                    if (index == -1) { throw new Exception("Faltan comillas"); }
                    i = index;
                }
                if (i+1<input.Length&&(input.Substring(i,2)=="<="||input.Substring(i,2)==">="|| input.Substring(i, 2) == "==" || input.Substring(i, 2) == "!="))
                {
                    result[0]=input.Substring(0,i).Trim();
                    if (i + 2 == input.Length) { throw new Exception("El operador "+input.Substring(i,2)+ " no puede estar al final de la cadena"); }
                    result[1]=input.Substring(i,2);
                    result[2]=input.Substring(i+2,input.Length-i-2).Trim();
                    if (result[0] == "") { throw new Exception("El operador "+input.Substring(i,2)+ " no puede estar al inicio de la cadena"); }
                    result[3] = "true";
                    return result;
                }
                if (input[i] == '<' || input[i] == '>')
                {
                    result[0]= input.Substring(0, i).Trim();
                    if (i + 1 == input.Length) { throw new Exception("El operador "+input[i]+" no puede estar al final de la cadena"); }
                    result[1] = input[i].ToString();
                    result[2]= input.Substring(i + 1, input.Length - i - 1).Trim();
                    if (result[0] == "") { throw new Exception("El operador " + input[i] + " no puede estar al inicio de la cadena"); }
                    result[3] = "true";
                    return result;
                }
            }
            result[3] = "false";
            return result;
        }
        internal static string[] ConcatString(string input)
        {
            string[] result = new string[3];
            for(int i=0; i<input.Length; i++)
            {
                if (input[i] == '"')
                {
                    int index = input.IndexOf('"', i + 1);
                    if (index == -1) { throw new Exception("Faltan comillas"); }
                    i = index;
                }
                if (input[i] == '@')
                {
                    result[0]= input.Substring(0, i).Trim();
                    if (i + 1 == input.Length) { throw new Exception("El operador @ no puede estar al final de la cadena"); }
                    result[1]= input.Substring(i + 1, input.Length - i - 1).Trim();
                    if (result[0] == "") { throw new Exception("El operador @ no puede estar al inicio de la cadena"); }
                    result[2] = "true";
                    return result;
                }
            }
            result[2] = "false";
            return result;
        }
        internal static string[] EquilibrarPrint(string input)
        {
            int count = 0;
            int pos=0;
            bool bandera = true;
            string[] result = new string[4];
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '"')
                {
                    int index = input.IndexOf('"', i + 1);
                    if (index == -1) { throw new Exception("Faltan comillas"); }
                    i = index;
                }
                if (i+5<input.Length && input.Substring(i, 5) == "print" && !char.IsLetterOrDigit(input[i+5]) && (i - 1 < 0 || !char.IsLetterOrDigit(input[i-1])))
                {
                    result[0]=input.Substring(0, i).Trim();
                    
                    for(int j = i+5;j< input.Length;j++)
                    {
                        if (input[j] == '('&& bandera) { bandera = false;pos = j; }
                        if (input[j] == '(') { count++; }
                        if (input[j] == ')')
                        { 
                            count--;
                            if (count == 0)
                            {
                                result[1] = input.Substring(pos + 1, j - pos - 1).Trim();
                                if (j + 1 < input.Length) { result[2] = input.Substring(j + 1, input.Length - j - 1).Trim(); }
                                else { result[2] = ""; }
                                result[3] = "true";
                                return result;
                            }
                        }
                    }
                    if (count != 0) { throw new Exception("Los parentesis no estan equilibrados"); }
                }
            }
            result[3] = "false";
            return result;
        }
        internal static string[] EquilibrarIf_Else(string input, string[] grupos)
        {
            int count = 0;
            bool bandera = true;
            int pos = 0;
            bool bandera1 = true;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '"')
                {
                    int index = input.IndexOf('"', i + 1);
                    if (index == -1) { throw new Exception("Faltan comillas"); }
                    i = index;
                }
                if (i+2<input.Length && input.Substring(i, 2) == "if" && !char.IsLetterOrDigit(input[i + 2]) && (i-1<0 || !char.IsLetterOrDigit(input[i - 1])) && bandera1) { grupos[0] = input.Substring(0, i).Trim();bandera1 = false; }
                if (input[i] == '(' && bandera&&!bandera1) { pos = i; bandera = false; }
                if (input[i] == '('&&!bandera1) { count++; }
                if (input[i] == ')'&&!bandera1 && count == 0)
                {
                    grupos[1] = input.Substring(pos + 1, i - pos - 1).Trim();

                    for (int j = i; j < input.Length - 4; j++)
                    {
                        if (input[j] == '"')
                        {
                            int index = input.IndexOf('"', j + 1);

                            if (index == -1)
                            {
                                throw new Exception("Faltan comillas");
                            }
                            j = index;
                            if (j >= input.Length - 4) { break; }
                        }
                        //incluir los guiones bajos
                        if (input.Substring(j, 2) == "if" && !char.IsLetterOrDigit(input[j + 2]) && !char.IsLetterOrDigit(input[j - 1])) { count++; }
                        if (input.Substring(j, 4) == "else" && count == 0 && input[j - 1] == ' ' && input[j + 4] == ' ')
                        {
                            grupos[2] = input.Substring(i + 1, j - i - 1).Trim();
                            grupos[3] = input.Substring(j + 4, input.Length - 1 - j - 3).Trim();
                            grupos[4] = "true";
                            if (grupos[2] == "" || grupos[1] == "") { throw new Exception("Declare una instruccion valida"); }
                            return grupos;  
                        }
                        if (input.Substring(j, 4) == "else" && input[j - 1] == ' ' && input[j + 4] == ' ') { count--; }
                    }
                    if (count!=0)
                    {
                        throw new Exception("La estructura if-else no es correcta");
                    }

                    break;

                }
                if (input[i] == ')' && !bandera1) { count--; if (count == 0) i--; }
            }
            if (count != 0) { throw new Exception("Los parentesis no estan balanceados");}
            grupos[4] = "false";
            return grupos;
        }
        internal static string[] EquilibrarLet_In(string input, string[] grupos)
        {
            int count = 0;

            int pos1 = 0;
            bool bandera= true;
            for (int i = 0; i < input.Length - 3; i++)
            {
                if (input[i] == '"')
                {
                    int index = input.IndexOf('"', i + 1);
                    if (index == -1) { throw new Exception("Faltan comillas"); }
                    i = index;
                    if (i >= input.Length - 3) { break; }
                }

                if (input.Substring(i, 3) == "let" && input[i + 3] == ' ' &&(i-1<0 || !char.IsLetterOrDigit(input[i - 1]))) 
                {
                    if (bandera) { pos1 = i + 4;bandera = false; }
                    count++;
                
                }
                if (input.Substring(i, 2) == "in" && input[i + 2] == ' ' && input[i - 1] == ' ')
                {
                    count--;
                    if (count == 0)
                    {
                        int pos2 = i;
                        grupos[0]=input.Substring(0,pos1-4).Trim();
                        grupos[1] = input.Substring(pos1, pos2 - pos1).Trim();
                        grupos[2] = input.Substring(pos2 + 2, input.Length - pos2 - 2).Trim();
                        grupos[3] = "true";
                        if (grupos[1] == "") { throw new Exception("Declare una instruccion valida"); }
                        return grupos;
                    }

                }
            }
            if (count!=0)
            {
                throw new Exception("La estructura let-in no es correcta");
            }
            grupos[3] = "false";
            return grupos;
        }
        internal static List<string> Equilibrar_Var(string input)
        {
            List<string> list = new List<string>();

            int pos1 = 0;
            for (int i = 0; i < input.Length - 3; i++)
            {
                if (input[i] == '"')
                {
                    int index = input.IndexOf('"', i + 1);
                    if (index == -1) { throw new Exception("Faltan comillas"); }
                    i = index;
                    if (i >= input.Length - 3) { break; }
                }
                string verificar = input.Substring(i, input.Length - i);
                foreach (KeyValuePair<string, string> item in Lexer.NameParameters)
                {
                    string clave = item.Key;
                    string patron = $@"^({clave})\s*(\(.*)";
                    Match match = Regex.Match(verificar, patron);
                    if (match.Success)
                    {
                        int count = 0;
                        for (int j = i + clave.Length; j < input.Length; j++)
                        {
                            if (input[j] == '(') { count++; }
                            if (input[j] == ')') { count--; if (count == 0) i = j; break; }
                           
                        }
                        if (count != 0) { throw new Exception("Los parentesis no estan balanceados"); }
                    }
                }
                if (i >= input.Length - 3) { break; }

                if (input.Substring(i, 3) == "let" && input[i + 3] == ' ' &&( i - 1 < 0 ||!char.IsLetterOrDigit(input[i - 1])))
                {
                    int count = 1;

                    for (int j = i + 4; j < input.Length - 3; j++)
                    {
                        if (input[j] == '"')
                        {
                            int index = input.IndexOf('"', j + 1);
                            if (index == -1) { throw new Exception("Faltan comillas"); }
                            j = index;
                            if (j >= input.Length - 3) { break; }
                        }

                        if (input.Substring(j, 3) == "let" && input[j + 3] == ' ' && !char.IsLetterOrDigit(input[j - 1])) { count++; }
                        if (input.Substring(j, 2) == "in" && input[j + 2] == ' ' && input[j - 1] == ' ')
                        {
                            count--;
                            if (count == 0)
                            {
                                i = j + 2; break;
                            }

                        }
                    }
                    if (count != 0) { throw new Exception("La estructura let-in no es correcta"); }
                }
                if (input[i] == ',') { list.Add(input.Substring(pos1, i - pos1).Trim()); pos1 = i + 1; }
            }
            list.Add(input.Substring(pos1, input.Length - pos1).Trim());
            if (list.Any(elemento => elemento == "")) { throw new Exception("Variables mal declaradas"); }
            return list;
        }
        //Parametros de la declaracion de funcion
        internal static List<string> Parameters(string input)
        {
            List<string> result = new List<string>();
            int pos = 0;
            if (input == "") { return result; }
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ',')
                {
                    result.Add(input.Substring(pos, i - pos).Trim());
                    pos = i + 1;
                }
            }
            if (result.Any(elemento => elemento == "")) { throw new Exception("Parametros incoherentes"); }
            if (pos < input.Length) { result.Add(input.Substring(pos, input.Length - pos).Trim()); return result; }
            throw new Exception("Parametros incoherentes");
        }
        //Parametros del llamado de funcion
        internal static string[] EquilibrarParametros(string input)
        {
            string[] result= new string[2];
            int count = 0;
            bool bandera = true;
            int pos = 0;
            for (int i = 0;i < input.Length; i++)
            {
                if (input[i] == '('&& bandera) { pos = i;bandera= false; }
                if (input[i] == '(') { count++; }
                if (input[i] == ')' && count == 0)
                {
                    result[0]= input.Substring(pos + 1, i - pos - 1).Trim();
                    if(i+1==input.Length) { result[1] = "";return result; }
                    else
                    {
                        pos = i;
                        result[1]= input.Substring(pos + 1, input.Length - pos - 1).Trim();
                        return result;
                    }
                    

                }
                if (input[i] == ')') { count--;if (count == 0)  i--;  }
            }
            throw new Exception("Los parentesis no estan equilibrados");
        }
        internal static List<string> Parameters2(string input)
        {
            List<string> result = new List<string>();
            if (input == "") {  return result; }
            int pos = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '"')
                {
                    int index = input.IndexOf('"', i + 1);
                    if (index == -1) { throw new Exception("Faltan comillas"); }
                    i = index;
                }
                string verificar=input.Substring(i, input.Length - i);
                foreach (KeyValuePair<string, string> item in Lexer.NameParameters)
                {
                    string clave = item.Key;
                    string patron = $@"^({clave})\s*(\(.*)";
                    Match match = Regex.Match(verificar, patron);
                    if (match.Success)
                    {
                        int count = 0;
                        for(int j = i + clave.Length; j < input.Length; j++)
                        {
                            if (input[j] == '(') { count++; }
                            if (input[j] == ')') { count--;if (count == 0) i = j;break; }
                        }
                        if (count != 0) { throw new Exception("Los parentesis no estan balanceados"); }
                    }

                }
                if (((i + 3) < input.Length && input.Substring(i, 3) == "let" && input[i + 3] == ' ' && i == 0) || ((i + 3) < input.Length && input.Substring(i, 3) == "let" && input[i + 3] == ' ' && i - 1 >= 0 && !char.IsLetterOrDigit(input[i - 1])))
                {
                    int count = 1;

                    for (int j = i + 4; j < input.Length - 3; j++)
                    {
                        if (input[j] == '"')
                        {
                            int index = input.IndexOf('"', j + 1);
                            if (index == -1) { throw new Exception("Faltan comillas"); }
                            j = index;
                            if (j >= input.Length - 3) { break; }
                        }

                        if (input.Substring(j, 3) == "let"  && input[j + 3] == ' ' && !char.IsLetterOrDigit(input[j - 1])) { count++; }
                        if (input.Substring(j, 2) == "in" && input[j + 2] == ' ' && input[j - 1] == ' ')
                        {
                            count--;
                            if (count == 0)
                            {
                                i = j + 2; break;
                            }
                        }
                    }
                    if (count != 0) { throw new Exception("La estructura let-in no es correcta"); }
                }
                if (input[i] == ',')
                {
                    result.Add(input.Substring(pos, i - pos).Trim());
                    pos = i + 1;
                }
            }
            if (result.Any(elemento => elemento == "")) { throw new Exception("Parametros incoherentes"); }
            if (pos < input.Length) { result.Add(input.Substring(pos, input.Length - pos).Trim()); return result; }
            throw new Exception("Parametros incoherentes");
        }
    }
}