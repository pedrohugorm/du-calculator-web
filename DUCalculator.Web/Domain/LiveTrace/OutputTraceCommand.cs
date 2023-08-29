namespace DUCalculator.Web.Domain.LiveTrace;

public class OutputTraceCommand : ICommand
{
    public void Execute(LiveTraceExecutionContext context)
    {
        if (!context.PositionAggregator.CanDoTrace())
        {
            return;
        }
        
        foreach (var unit in context.TraceOutputDistanceList)
        {
            var result = context.PositionAggregator.TraceTo(unit);
            var position = result.Vector3ToPosition();
            context.Writer.WriteLine($"{unit} = {position}");
        }
    }
}