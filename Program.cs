using BenchmarkDotNet.Running;

namespace PiCalculatorNet;

class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<Benchmarks>();
    }
}
