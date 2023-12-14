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
            string result = Lexer.StartAnal(input.Trim());
            if(result.StartsWith('"')&&result.EndsWith("\""))
            {
               result= result.Remove(0, 1);
               result= result.Remove(result.Length - 1, 1);
            }
            Console.WriteLine('>' + result);
        }
        catch (Exception ex) { Console.WriteLine('>' + ex.Message); }
    }
    else
    {
        Console.WriteLine('>'+"Falta el punto y coma");
    }
}