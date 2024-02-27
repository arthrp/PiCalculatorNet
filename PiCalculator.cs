namespace PiCalculatorNet;

public class PiCalculator
{
    public double CalculateSequentially(int iters)
    {
        var pointsInCircle = GetPointsInCircle(iters);

        var ratio = (double) pointsInCircle / (double) iters;

        return ratio * 4d;
    }

    public double CalculateParallel(int iters)
    {
        const int pieces = 5;
        var itersPerPiece = iters / pieces;

        var pointsInCircle = Enumerable.Range(0, pieces).AsParallel().Select(_ => GetPointsInCircle(itersPerPiece)).Sum();
        var ratio = (double) pointsInCircle / (double) iters;
        return ratio * 4d;
    }
    
    public async Task<double> CalculateConcurrentTask(int iters)
    {
        const int taskCount = 5;
        var itersPerTask = iters / taskCount;
        List<Task<int>> tasks = new();

        foreach (var _ in Enumerable.Range(0,taskCount))
        {
            Task<int> t = Task.Run(() =>
            {
                var pointsInCircle = GetPointsInCircle(itersPerTask);
                return pointsInCircle;
            });
            tasks.Add(t);
        }

        var results = await Task.WhenAll(tasks);
        var total = results.Sum();
        
        var ratio = (double) total / (double) iters;
        return ratio * 4d;
    }

    private int GetPointsInCircle(int iters)
    {
        var pointsInCircle = 0;
        var r = new Random();

        foreach (var _ in Enumerable.Range(0,iters))
        {
            var x = r.NextDouble();
            var y = r.NextDouble();

            if (IsPointInCircle(x, y)) pointsInCircle++;
        }

        return pointsInCircle;
    }

    private bool IsPointInCircle(double x, double y)
    {
        return x * x + y * y < 1d;
    }
}