using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HulkRE;

while (true)
{
    Console.Write(">");
    string input = Console.ReadLine()!;
    if (input[input.Length - 1] == ';')
    {
        input = input.Remove(input.Length - 1, 1);
        try
        {
            Console.WriteLine('>' + Lexer.StartAnal(input.Trim()));
        }
        catch (Exception ex) { Console.WriteLine('>' + ex.Message); }
    }
    else
    {
        Console.WriteLine('>'+"Falta el punto y coma");
    }
}