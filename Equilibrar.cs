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
        internal static string[] EquilibrarIf_Else(string input, string[] grupos)
        {
            int count = 0;
            bool bandera = true;
            int pos = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(' && bandera) { pos = i; bandera = false; }
                if (input[i] == '(') { count++; }
                if (input[i] == ')' && count == 0)
                {

                    grupos[0] = input.Substring(pos + 1, i - pos - 1).Trim();

                    for (int j = i; j < input.Length - 4; j++)
                    {
                        if (input[j] == '"')
                        {
                            int index = input.IndexOf('"', j + 1);

                            if (index == -1)
                            {
                                throw new Exception("Faltan comillas");
                            }
                            j = index + 1;
                            if (j >= input.Length - 4) { break; }
                        }
                        //incluir los guiones bajos
                        if (input.Substring(j, 2) == "if" && !char.IsLetterOrDigit(input[j + 2]) && !char.IsLetterOrDigit(input[j - 1])) { count++; }
                        if (input.Substring(j, 4) == "else" && count == 0 && input[j - 1] == ' ' && input[j + 4] == ' ')
                        {
                            grupos[1] = input.Substring(i + 1, j - i - 1).Trim();
                            grupos[2] = input.Substring(j + 4, input.Length - 1 - j - 3).Trim(); break;
                        }
                        if (input.Substring(j, 4) == "else" && input[j - 1] == ' ' && input[j + 4] == ' ') { count--; }
                    }
                    if (grupos[1] == "" || grupos[2] == "") { (grupos[1], grupos[2]) = ("\"Declare una instruccion valida\"", "\"Declare una instruccion valida\""); }
                    if ((grupos[1] == null || grupos[2] == null))
                    {

                        (grupos[1], grupos[2]) = ("\"La estructura if-else no esta equilibrada\"", "\"La estructura if-else no esta equilibrada\"");

                    }

                    break;

                }
                if (input[i] == ')') { count--; if (count == 0) i--; }
            }
            return grupos;
        }
        internal static string[] EquilibrarLet_In(string input, string[] grupos)
        {
            int count = 1;

            int pos1 = 4;

            for (int i = 4; i < input.Length - 2; i++)
            {
                if (input[i] == '"')
                {
                    int index = input.IndexOf('"', i + 1);
                    if (index == -1) { throw new Exception("Faltan comillas"); }
                    i = index + 1;
                    if (i >= input.Length - 2) { throw new Exception("La estructura let-in no esta equilibrada"); }
                }

                if (input.Substring(i, 3) == "let" && (i + 3) < input.Length && input[i + 3] == ' ' && !char.IsLetterOrDigit(input[i - 1])) { count++; }
                if (input.Substring(i, 2) == "in" && input[i + 2] == ' ' && input[i - 1] == ' ')
                {
                    count--;
                    if (count == 0)
                    {
                        int pos2 = i;
                        grupos[1] = input.Substring(pos1, pos2 - pos1).Trim();
                        grupos[2] = input.Substring(pos2 + 2, input.Length - pos2 - 2).Trim();
                        return grupos;
                    }

                }
            }
            if ((grupos[1] == null || grupos[2] == null))
            {
                throw new Exception("La estructura let-in no esta equilibrada");
            }
            if (grupos[1] == "" || grupos[2] == "") { throw new Exception("Declare una instruccion valida"); }
            return grupos;
        }
        internal static List<string> Equilibrar_Var(string input)
        {
            List<string> list = new List<string>();

            int pos1 = 0;
            for (int i = 0; i < input.Length-3; i++)
            {
                if (input[i] == '"')
                {
                    int index = input.IndexOf('"', i + 1);
                    if (index == -1) { throw new Exception("Faltan comillas"); }
                    i = index + 1;
                    if (i >= input.Length-3) { break; }
                }
                if (input.Substring(i, 3) == "let" && input[i + 3] == ' ' && (i + 3) < input.Length && i-1>=0 && !char.IsLetterOrDigit(input[i - 1]))
                {
                    int count = 1;

                    for (int j = i+4; j < input.Length - 2; j++)
                    {
                        if (input[j] == '"')
                        {
                            int index = input.IndexOf('"', j + 1);
                            if (index == -1) { throw new Exception("Faltan comillas"); }
                            j = index + 1;
                            if (j >= input.Length - 3) { throw new Exception("La estructura let-in no esta equilibrada"); }
                        }

                        if (input.Substring(j, 3) == "let" && input[j + 3] == ' ' && (j + 3) < input.Length && !char.IsLetterOrDigit(input[j - 1])) { count++; }
                        if (input.Substring(j, 2) == "in" && input[j + 2] == ' ' && input[j - 1] == ' ')
                        {
                            count--;
                            if (count == 0)
                            {
                                i = j + 2; break;
                            }

                        }
                    }
                    if (count != 0) { throw new Exception("La estructura let-in no esta equilibrada"); }
                }
                if (input[i] == ',') { list.Add(input.Substring(pos1,i-pos1).Trim());pos1 = i+1; }
            }
            list.Add(input.Substring(pos1, input.Length - pos1).Trim());
            if (list.Any(elemento => elemento == "")) { throw new Exception("Variables mal declaradas"); }
            return list;
        }

    }
}
       