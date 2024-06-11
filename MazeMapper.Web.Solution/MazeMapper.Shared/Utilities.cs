using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MazeMapper.Shared
{
    public class Utilities
    {
        public const string SelectClause = @"SELECT * FROM {0};";
        public const string SelectWithWhereClause = @"SELECT * FROM {0} WHERE {1} = {2};";

        /// <summary>
        /// Ejemplo con clientes y pedidos.
        /// Devuelve solo las filas que tienen correspondencia en ambas tablas.
        /// </summary>
        public const string SelectWithInnerJoinClause = @"SELECT clientes.nombre, pedidos.producto
FROM clientes
INNER JOIN pedidos ON clientes.id_cliente = pedidos.id_cliente;";

        /// <summary>
        /// Ejemplo con clientes y pedidos.
        /// Devuelve todas las filas de la tabla izquierda (clientes), junto con las filas de la derecha (pedidos) que tienen una clave coincidente en la tabla izquierda.
        /// Incluso si un cliente no ha realizado ningún pedido, su información será devuelta con valores NULL.
        /// </summary>
        public const string SelectWithLeftJoinClause = @"SELECT clientes.nombre, pedidos.producto
FROM clientes
LEFT JOIN pedidos ON clientes.id_cliente = pedidos.id_cliente;";

        /// <summary>
        /// Ejemplo con clientes y pedidos.
        /// Devuelve todas las filas de la tabla derecha (pedidos), junto con las filas de la izquierda (clientes) que tienen una clave coincidente en la tabla derecha.
        /// Incluso si no hay coincidencias en la tabla clientes, todas las filas de la tabla pedidos serán devueltas, con valores NULL .
        /// </summary>
        public const string SelectWithRightJoinClause = @"SELECT clientes.nombre, pedidos.producto
FROM clientes
LEFT JOIN pedidos ON clientes.id_cliente = pedidos.id_cliente;";


        public static int ParallelSum(int start, int end)
        {
            int totalSum = 0;
            object lockObj = new object();

            Parallel.For(start, end + 1, () => 0, (i, state, localSum) =>
                {
                    localSum += i;
                    return localSum;
                },
                localSum =>
                {
                    lock (lockObj)
                    {
                        totalSum += localSum;
                    }
                });

            return totalSum;
        }

        public static string MaskString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return input.Replace('a', '*')
                .Replace('e', '*')
                .Replace('i', '*')
                .Replace('o', '*')
                .Replace('u', '*')
                .Replace('A', '*')
                .Replace('E', '*')
                .Replace('I', '*')
                .Replace('O', '*')
                .Replace('U', '*');
        }

        public static string MaskStringWithRegex(string input)
        {
            // Define the regular expression pattern for vowels
            string pattern = "[aeiouAEIOU]";

            // Use Regex.Replace to replace all vowels with '*'
            string result = Regex.Replace(input, pattern, "*");

            return result;
        }
    }
}
