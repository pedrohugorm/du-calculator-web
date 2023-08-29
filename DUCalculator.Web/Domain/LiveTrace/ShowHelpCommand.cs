using System.Text;

namespace DUCalculator.Web.Domain.LiveTrace;

public class ShowHelpCommand : ICommand
{
    public void Execute(LiveTraceExecutionContext context)
    {
        var sb = new StringBuilder();

        sb.AppendLine("----------------------------------------------------------------------------------------------");
        sb.AppendLine("help                 Shows this help text");
        sb.AppendLine("::pos{0,...}         Tracks the next Position. If two positions are provided, a trace is calculated");
        sb.AppendLine("ct                   Clears the Buffered Traces");
        sb.AppendLine("clear or c           Clears the screen - but doesn't clear the stored traces");
        sb.AppendLine("su 10,15,20          Sets the trace distances to the distances separated by comma.");
        sb.AppendLine("t                    Calculate trace positions.");
        sb.AppendLine("exit                 Closes the program");
        sb.AppendLine("----------------------------------------------------------------------------------------------");

        context.Writer.Write(sb.ToString());
    }
}