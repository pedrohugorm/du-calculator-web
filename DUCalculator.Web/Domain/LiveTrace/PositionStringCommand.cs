namespace DUCalculator.Web.Domain.LiveTrace;

public class PositionStringCommand : ICommand
{
    private readonly OutputTraceCommand _outputTraceCommand;
    public string Value { get; }

    public PositionStringCommand(string value, OutputTraceCommand outputTraceCommand)
    {
        _outputTraceCommand = outputTraceCommand;
        Value = value;
    }

    public void Execute(LiveTraceExecutionContext context)
    {
        if (context.PositionAggregator.PushPosition(Value.Trim().PositionToVector3()))
        {
            context.Writer.WriteLine($"Captured = {Value}");
            context.SetLastPosition(Value);
            _outputTraceCommand.Execute(context);
        }
    }
}