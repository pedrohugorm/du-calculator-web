namespace DUCalculator.Web.Domain.WeaponDamage;

public class WeaponDamageSubProgram
{
    public void ProcessCommand(string commandText, WeaponDamageContext context)
    {
        context.AvailableCommands = WeaponDamageCommandList.CreateCommandDictionary(context.Console);
        
        try
        {
            ReadCommands(commandText, context);
        }
        catch (Exception e)
        {
            context.Console.WriteLine();
            context.Console.WriteLine("Failed to Parse Line");
            context.Console.WriteLine(e.ToString());
            context.Console.WriteLine();
        }
    }

    private static void ReadCommands(string commandText, WeaponDamageContext context)
    {
        if (string.IsNullOrEmpty(commandText))
        {
            return;
        }
        
        if (commandText is "exit" or "x")
        {
            context.Console.WriteLine("Exited!");
            return;
        }
        
        context.RawCommand = commandText;

        var commandPieces = context.GetCommandPieces();
        var commandPiecesQueue = new Queue<string>(commandPieces);
        var commandName = commandPiecesQueue.Dequeue().ToLower();

        if (!context.AvailableCommands.ContainsKey(commandName))
        {
            context.Console.WriteLine($"Invalid command '{commandName}'");
            return;
        }

        var command = context.AvailableCommands[commandName];
        
        command.Execute(context);
    }
}