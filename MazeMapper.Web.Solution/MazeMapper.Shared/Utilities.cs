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
    }
}
