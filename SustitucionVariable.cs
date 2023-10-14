using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HulkRE
{
    internal class SustitucionVariable
    {
        public static string Sustitucion(string cadena, Dictionary<string, string> diccionario)
        {
            // Crear una expresión regular que coincida con las variables entre comillas o con letras a su lado
            Regex regex = new Regex("\"[^\"]*\"|\\b[a-zA-Z]+\\b");

            // Crear una variable para almacenar el resultado
            string resultado = cadena;

            // Recorrer cada par clave-valor del diccionario
            foreach (KeyValuePair<string, string> par in diccionario)
            {
                // Reemplazar todas las ocurrencias de la clave por el valor, excepto las que coincidan con la expresión regular
                resultado = regex.Replace(resultado, m => m.Value == par.Key ? par.Value : m.Value);
            }

            // Devolver el resultado
            return resultado;
        }
    }
}


       





