using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace PiCalculatorNet;

[SimpleJob(RuntimeMoniker.Net80)]
[RPlotExporter]
[HtmlExporter]
public class Benchmarks
{
    [Params(100_000, 1_000_000, 10_000_000)]
    public int Iters { get; set; }

    [Benchmark]
    public double CalcSeq() => new PiCalculator().CalculateSequentially(Iters);

    [Benchmark]
    public double CalcParallel() => new PiCalculator().CalculateParallel(Iters);

    [Benchmark]
    public async Task<double> CalcTask() => await new PiCalculator().CalculateConcurrentTask(Iters);
}