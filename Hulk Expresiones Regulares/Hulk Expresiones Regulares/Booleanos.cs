using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HulkRE
{
    internal class Booleanos
    {
        internal static string Evaluar(string[] texto)
        {
            double a;
            double b;
            
            if (texto[1]=="==")
            {
                string left = Lexer.StartAnal(texto[0]);
                string right = Lexer.StartAnal(texto[2]);
                if (double.TryParse(left, out a))
                {
                    if (double.TryParse(right, out b))
                    {
                        if (a == b) { return "true"; }
                        else { return "false"; }
                    }
                    else { throw new Exception("No se puede comparar 2 tipos diferentes"); }
                }
                if (left.StartsWith('"') && left.EndsWith('"'))
                {
                    if (right.StartsWith('"') && right.EndsWith('"'))
                    {
                        if (left == right) { return "true"; }
                        else { return "false"; }
                    }
                    else { throw new Exception("No se puede comparar 2 tipos diferentes"); }
                }
                if (left == "true" || left == "false")
                {
                    if (right == "true" || right == "false")
                    {
                        if (left == right) { return "true"; }
                        else
                        {
                            return "false"; }
                    }
                    else { throw new Exception("No se puede comparar 2 tipos diferentes"); }
                }
                throw new Exception(left + " no coincide con ningun tipo");
              
            }


           
            if (texto[1]==">=")
            {
                string left = Lexer.StartAnal(texto[0]);
                string right = Lexer.StartAnal(texto[2]);
                if (double.TryParse(left, out a))
                {
                    if (double.TryParse(right, out b))
                    {
                        if (a >= b) { return "true"; }
                        else { return "false"; }
                    }
                    else { throw new Exception("No se puede aplicar el operador >= a tipos que no sean numeros"); }
                }
                throw new Exception("No se puede aplicar el operador >= a tipos que no sean numeros");
            }
              
            if (texto[1]=="<=")
            {
                string left = Lexer.StartAnal(texto[0]);
                string right = Lexer.StartAnal(texto[2]);
                if (double.TryParse(left, out a))
                {
                    if (double.TryParse(right, out b))
                    {
                        if (a <= b) { return "true"; }
                        else { return "false"; }
                    }
                    else { throw new Exception("No se puede aplicar el operador >= a tipos que no sean numeros"); }
                }
                throw new Exception("No se puede aplicar el operador >= a tipos que no sean numeros");
            }

            if (texto[1]==">")
            {
                string left = Lexer.StartAnal(texto[0]);
                string right = Lexer.StartAnal(texto[2]);
                if (double.TryParse(left, out a))
                {
                    if(double.TryParse(right, out b))
                    {
                        if (a > b) { return "true"; }
                        else { return "false"; }
                    }
                    else { throw new Exception("No se puede aplicar el operador >= a tipos que no sean numeros"); }
                }
                throw new Exception("No se puede aplicar el operador >= a tipos que no sean numeros");
            }
       
            if (texto[1]=="<")
            {
                string left = Lexer.StartAnal(texto[0]);
                string right = Lexer.StartAnal(texto[2]);
                if(double.TryParse(left, out a))
                {
                    if (double.TryParse(right, out b))
                    {
                        if (a < b) {return "true"; }
                        else
                        {
                            return "false";
                        }
                    }
                    else { throw new Exception("No se puede aplicar el operador >= a tipos que no sean numeros"); }
                }
                throw new Exception("No se puede aplicar el operador >= a tipos que no sean numeros");
            }
          
            if (texto[1]=="!=")
            {
                string left = Lexer.StartAnal(texto[0]);
                string right = Lexer.StartAnal(texto[2]);
                if (double.TryParse(left, out a))
                {
                    if (double.TryParse(right, out b))
                    {
                        if (a != b) { return "true"; }
                        else { return "false"; }
                    }
                    else { throw new Exception("No se puede comparar 2 tipos diferentes"); }
                }
                if (left.StartsWith('"') && left.EndsWith('"'))
                {
                    if (right.StartsWith('"') && right.EndsWith('"'))
                    {
                        if (left != right) { return "true"; }
                        else { return "false"; }
                    }
                    else { throw new Exception("No se puede comparar 2 tipos diferentes"); }
                }
                if (left == "true" || left == "false")
                {
                    if (right == "true" || right == "false")
                    {
                        if (left != right) { return "true"; }
                        else
                        {
                            return "false";
                        }
                    }
                    else { throw new Exception("No se puede comparar 2 tipos diferentes"); }
                }
                throw new Exception(left + " no coincide con ningun tipo");
            }
            throw new Exception("Error de sintaxis");
        }
    }
}
