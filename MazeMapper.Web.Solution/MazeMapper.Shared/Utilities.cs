using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MazeMapper.Shared
{
    public class Utilities
    {
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
