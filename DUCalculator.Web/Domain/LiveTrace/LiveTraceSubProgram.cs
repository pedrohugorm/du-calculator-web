using DUCalculator.Web.Domain.Common;

namespace DUCalculator.Web.Domain.LiveTrace;

public class LiveTraceSubProgram
{
    public LiveTraceExecutionContext Context;
    
    public LiveTraceSubProgram(IConsoleWriter consoleWriter)
    {
        Context = new LiveTraceExecutionContext(
            consoleWriter,
            new PositionAggregator(),
            _ => {}
        );
    }

    public void ProcessCommand(string line)
    {
        ProcessCommand(line, command =>
        {
            try
            {
                command.Execute(Context);
            }
            catch (Exception e)
            {
                Context.Writer.WriteLine();
                Context.Writer.WriteLine("Failed to Parse Line");
                Context.Writer.WriteLine(e.ToString());
                Context.Writer.WriteLine();
            }
        });
    }

    private void ProcessCommand(string line, Action<ICommand> onCommand)
    {
        if (string.IsNullOrEmpty(line))
        {
            return;
        }
        
        if (line.StartsWith("::pos{") && line.EndsWith("}"))
        {
            onCommand.Invoke(
                new PositionStringCommand(
                    line,
                    new OutputTraceCommand()
                )
            );
        }
        else if (line is "exit" or "x")
        {
            Context.Writer.WriteLine("Exited!");
        }
        else if (line is "clear" or "c")
        {
            onCommand.Invoke(new ClearScreenCommand());
        }
        else if (line is "ct")
        {
            onCommand.Invoke(new ClearBufferCommand());
        }
        else if (line is "trace" or "t")
        {
            onCommand.Invoke(new OutputTraceCommand());
        }
        else if (line.StartsWith("su "))
        {
            onCommand.Invoke(new SetTraceOutputListCommand(line));
        }
        else if (line is "show")
        {
            onCommand.Invoke(new ShowContextDataCommand());
        }
        else if (line is "help")
        {
            onCommand.Invoke(new ShowHelpCommand());
        }
    }
}