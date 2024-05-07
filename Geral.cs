using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsBD
{
    internal class Geral
    {
        public static string id_user = "";

        public static string TirarEspacos(string texto)
        {
            texto = texto.Trim();
            texto = Regex.Replace(texto, @"\s+", " ");
            return texto;
        }

        public static bool CheckDate(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

         private bool ContemApenasLetras(string str)
        {
            // Expressão regular para verificar se a string contém apenas letras
            Regex regex = new Regex(@"^[a-zA-Z]+$");
            return regex.IsMatch(str);
        }
    }
}
