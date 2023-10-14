
using HulkRE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using HulkRE;

namespace HulkRE
{
    internal class Aritmetica
    {

        internal static string Evaluar(string expression)
        {
            // Se eliminan los espacios en blanco de la expresión
            expression = expression.Replace(" ", "");
            expression = expression.Replace("-(", "-1*(");
            expression = expression.Replace("-", "-1*");
            int error = Validar_Aritmetica.ValidarExpresion(expression);
            if (Validar_Aritmetica.ValidarExpresion(expression) != -1)
            {
                if (expression[error] == '(')
                {
                    return "! SYNTAX ERROR: Missing closing parenthesis";
                }
                if (expression[error] == ')')
                {
                    return "! SYNTAX ERROR: Missing open parenthesis";
                }
               
                if (expression[error].ToString().Any(Char.IsLetter))
                {
                    return "! LEXICAL ERROR: " + expression[error].ToString()+" is not valid token.";
                }
                if (char.IsDigit(expression[error])) { return "Se esperaba (*) antes del parentesis"; }
                if (expression[error] == '+' || expression[error] == '-' || expression[error] == '*' || expression[error] == '/' || expression[error] == '^' || expression[error] == '%') {return  " Operador en posicion incorreta" ; }
                if (expression[error]=='.')
                {
                    return "Punto y coma no valido";
                }
                else
                {
                    return "! SYNTAX ERROR: Missing operator";
                }

            }
            

            // Se crea una pila para almacenar los números
            Stack<double> numbers = new Stack<double>();

            // Se crea una pila para almacenar los operadores
            Stack<char> operators = new Stack<char>();

            // Se recorre la expresión caracter por caracter
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

                // Si el caracter es un dígito o un punto decimal, se forma el número completo y se añade a la pila de números
                if (char.IsDigit(c))
                {

                    string number = c.ToString();

                    while (i + 1 < expression.Length && (char.IsDigit(expression[i + 1]) || expression[i + 1] == '.'))
                    {
                        number += expression[i + 1];
                        i++;
                    }

                    numbers.Push(double.Parse(number));
                }
                // Si el caracter es un signo menos y el anterior no es un dígito ni un paréntesis derecho, se trata de un número negativo
                else if (c == '-' && (i == 0 || !char.IsDigit(expression[i - 1]) && expression[i - 1] != ')'))
                {
                    string number = c.ToString();

                    while (i + 1 < expression.Length && (char.IsDigit(expression[i + 1]) || expression[i + 1] == '.'))
                    {
                        number += expression[i + 1];
                        i++;
                    }

                    numbers.Push(double.Parse(number));
                }
                // Si el caracter es un paréntesis izquierdo, se añade a la pila de operadores
                else if (c == '(')
                {
                    operators.Push(c);
                }
                // Si el caracter es un paréntesis derecho, se resuelven las operaciones dentro del paréntesis
                else if (c == ')')
                {
                    while (operators.Count > 0 && operators.Peek() != '(')
                    {
                        PerformOperation(numbers, operators);
                    }

                    // Se elimina el paréntesis izquierdo de la pila de operadores
                    operators.Pop();
                }
                // Si el caracter es un operador (+, -, *, /, ^), se resuelven las operaciones anteriores con mayor o igual precedencia
                else if (c == '+' || c == '-' || c == '*' || c == '/' || c == '^'|| c=='%')
                {
                    while (operators.Count > 0 && HasPrecedence(c, operators.Peek()))
                    {
                        PerformOperation(numbers, operators);
                    }

                    // Se añade el operador actual a la pila de operadores
                    operators.Push(c);
                }
            }


            // Se resuelven las operaciones restantes en la pila
            while (operators.Count > 0)
            {
                PerformOperation(numbers, operators);
            }

            // Se devuelve el resultado final en forma de string
            return numbers.Pop().ToString();
        }

        // Método que realiza una operación aritmética entre dos números según el operador dado
        // Los números y el operador se extraen de las pilas correspondientes
        // El resultado se añade a la pila de números
        static void PerformOperation(Stack<double> numbers, Stack<char> operators)
        {
            // Se extrae el operador de la pila de operadores
            char op = operators.Pop();

            // Se extraen los dos números de la pila de números
            double b = numbers.Pop();
            double a = numbers.Pop();

            // Se realiza la operación según el operador y se almacena el resultado
            double result = 0;

            switch (op)
            {
                case '+':
                    result = a + b;
                    break;
                case '-':
                    result = a - b;
                    break;
                case '*':
                    result = a * b;
                    break;
                case '/':
                    result = a / b;
                    break;
                case '%':
                    result = a % b;
                    break;
                case '^':
                    result = Math.Pow(a, b);
                    break;
            }

            // Se añade el resultado a la pila de números
            numbers.Push(result);
        }

        // Método que determina si el operador 1 tiene mayor o igual precedencia que el operador 2
        // La precedencia es: paréntesis > potencia > multiplicación y división > suma y resta
        // El método devuelve true si el operador 1 tiene mayor o igual precedencia que el operador 2, y false en caso contrario
        static bool HasPrecedence(char op1, char op2)
        {
            if (op2 == '(' || op2 == ')')
            {
                return false;
            }

            if ((op1 == '*' || op1 == '/'|| op1=='%') && (op2 == '+' || op2 == '-'))
            {
                return false;
            }

            if (op1 == '^' && op2 != '^')
            {
                return false;
            }

            return true;
        }
    }

}

    




    

        
  
