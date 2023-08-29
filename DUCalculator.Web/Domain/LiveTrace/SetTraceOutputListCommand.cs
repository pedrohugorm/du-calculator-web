using DUCalculator.Web.Domain.Common;

namespace DUCalculator.Web.Domain.LiveTrace;

public class SetTraceOutputListCommand : ICommand
{
    private readonly string _line;

    public SetTraceOutputListCommand(string line)
    {
        _line = line;
    }

    public void Execute(LiveTraceExecutionContext context)
    {
        var suValues = _line.Replace("su ", "")
            .Split(new[] { ',', '|', ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(float.Parse);

        context.TraceOutputDistanceList = suValues.Select(v => new SpaceUnit(v)).ToList();

        context.Writer.WriteLine($"Trace Output Values updated to: {string.Join(", ", context.TraceOutputDistanceList)}");
    }
}