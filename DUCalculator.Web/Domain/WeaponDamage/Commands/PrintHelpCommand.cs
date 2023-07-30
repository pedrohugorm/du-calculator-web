using System.Text;

namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class PrintHelpCommand : IWeaponDamageCommand
{
    public string[] Commands => new[] { "help", "h" };
    public string Description => "Prints the help with list of commands, examples and descriptions";
    public string Example => "help";

    public void Execute(WeaponDamageContext context)
    {
        var clearCommand = new ClearScreenCommand();
        clearCommand.Execute(context);
        
        var sb = new StringBuilder();

        sb.AppendLine("HELP:");
        sb.AppendLine("----------------------------------------------------------------------------------------------");

        var commandByType = context.AvailableCommands.Values
            .GroupBy(g => g.GetType().Name)
            .Select(x => x.First())
            .ToDictionary(k => k.GetType().Name, v => v);
        var orderedCommands = commandByType.OrderBy(kvp => kvp.Key);
        foreach (var kvp in orderedCommands)
        {
            var command = kvp.Value;
            sb.AppendLine(string.Join(", ", command.Commands));
            sb.AppendLine(command.Description);
            sb.AppendLine($"Example: {command.Example}");
            sb.AppendLine("");
        }

        sb.AppendLine("----------------------------------------------------------------------------------------------");
        sb.AppendLine("");

        context.Console.WriteLine(sb.ToString());
    }
}