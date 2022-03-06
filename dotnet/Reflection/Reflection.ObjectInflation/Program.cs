using BenchmarkDotNet.Running;
using Reflection.ObjectInflation.Comparisons;

namespace Reflection.ObjectInflation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            _ = BenchmarkRunner.Run<RealWorld_ReflectionVsUnreliable>();
        }
    }
}