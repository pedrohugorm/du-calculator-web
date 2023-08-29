namespace DUCalculator.Web.Domain.LiveTrace;

public class ClearScreenCommand : ICommand
{
    public void Execute(LiveTraceExecutionContext context)
    {
        context.Writer.WriteLine("Ready to receive new positions.");
    }
}