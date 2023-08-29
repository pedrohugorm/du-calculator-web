namespace DUCalculator.Web.Domain.LiveTrace;

public interface ICommand
{
    void Execute(LiveTraceExecutionContext context);
}