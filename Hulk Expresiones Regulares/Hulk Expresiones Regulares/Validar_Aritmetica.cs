using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HulkRE
{
    internal class Validar_Aritmetica
    {
        internal static int ValidarExpresion(string expresion)
        {
            // Un arreglo de los operadores válidos
            char[] operadores = { '+','*', '/', '^', '%' };

            // Una pila para guardar los paréntesis abiertos
            Stack<int> pila = new Stack<int>();

            // Un bucle para recorrer la expresión de izquierda a derecha
            for (int i = 0; i < expresion.Length; i++)
            {
                // El caracter actual
                char c = expresion[i];
                if (c=='.')
                {
                    if (i==0 || i==expresion.Length-1)
                    {
                        return i;
                    }
                    if (!(char.IsDigit(expresion[i + 1]) && char.IsDigit(expresion[i - 1])))
                    {
                        return i;
                    }
                    else continue;
                }
               

                // Si el caracter es un dígito comprobar 
                if (char.IsDigit(c))
                {
                    if ((i+1 != expresion.Length&& expresion[i + 1] == '(') ||( i - 1 >= 0 && expresion[i-1]==')'))
                    {
                        return i;
                    }
                    continue;
                
                }

                // Si el caracter es un paréntesis abierto, lo guardamos en la pila
                if (c == '(')
                {
                    pila.Push(i);
                    continue;
                }

                // Si el caracter es un paréntesis cerrado, verificamos que haya un paréntesis abierto correspondiente en la pila
                if (c == ')')
                {
                    // Si la pila está vacía, significa que hay un paréntesis cerrado de más
                    if (pila.Count == 0)
                    {
                        return i; // Devolvemos la posición del error
                    }

                    // Si no, sacamos el paréntesis abierto de la pila
                    pila.Pop();
                    continue;
                }
                // Si el caracter es un signo menos, verificamos que sea parte de un número negativo o una resta válida
                if (c == '-')
                {
                    // Si el signo menos está al final de la expresión, es un error
                    if (i == expresion.Length - 1)
                    {
                        return i; // Devolvemos la posición del error
                    }

                    // El caracter siguiente
                    char next = expresion[i + 1];

                    // Si el caracter siguiente no es un dígito o un paréntesis abierto, es un error
                    if (!char.IsDigit(next) && next != '(')
                    {
                        return i; // Devolvemos la posición del error
                    }

                    continue;
                }

                // Si el caracter es un operador, verificamos que haya un operando válido a la izquierda y a la derecha
                if (operadores.Contains(c))
                {
                    // Si el operador está al principio o al final de la expresión, es un error
                    if (i == 0 || i == expresion.Length - 1)
                    {
                        return i; // Devolvemos la posición del error
                    }
                    // El caracter anterior y el siguiente
                    char prev = expresion[i - 1];
                    char next = expresion[i + 1];

                    // Si el caracter anterior o el siguiente es otro operador o un paréntesis cerrado o abierto respectivamente, es un error
                    if (operadores.Contains(prev) || prev == '(' || operadores.Contains(next) || next == ')')
                    {
                        return i; // Devolvemos la posición del error
                    }

                    continue;
                }
                // Si el caracter no es ninguno de los anteriores, es un error
                return i; // Devolvemos la posición del error
            }

            // Al finalizar el bucle, verificamos que la pila esté vacía, lo que significa que todos los paréntesis están balanceados
            if (pila.Count > 0)
            {
                return pila.Pop(); // Devolvemos la posición del primer paréntesis abierto sin cerrar
            }

            // Si no hay ningún error, devolvemos -1
            return -1;
        }
    }
}
