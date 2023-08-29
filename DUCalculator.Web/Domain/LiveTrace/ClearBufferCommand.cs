namespace DUCalculator.Web.Domain.LiveTrace;

public class ClearBufferCommand : ICommand
{
    public void Execute(LiveTraceExecutionContext context)
    {
        context.PositionAggregator.Clear();
        
        context.Writer.WriteLine("Cleared Buffer. Ready to receive new positions.");
    }
}