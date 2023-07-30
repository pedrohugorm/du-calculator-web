using System.Reflection;
using DUCalculator.Web.Domain.Common;
using DUCalculator.Web.Domain.WeaponDamage.Commands;

namespace DUCalculator.Web.Domain.WeaponDamage;

public class WeaponDamageSubProgram
{
    public WeaponDamageContext Context { get; }

    public WeaponDamageSubProgram(IConsoleWriter consoleWriter)
    {
        Context = new WeaponDamageContext(consoleWriter)
        {
            AvailableCommands = CreateCommandDictionary(consoleWriter)
        };
    }

    public void ProcessCommand(string commandText)
    {
        try
        {
            ReadCommands(commandText, Context);
        }
        catch (Exception e)
        {
            Context.Console.WriteLine();
            Context.Console.WriteLine("Failed to Parse Line");
            Context.Console.WriteLine(e.ToString());
            Context.Console.WriteLine();
        }
    }

    private static Dictionary<string, IWeaponDamageCommand> CreateCommandDictionary(IConsoleWriter consoleWriter)
    {
        var result = new Dictionary<string, IWeaponDamageCommand>();
        
        var commandTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(r => r.IsAssignableTo(typeof(IWeaponDamageCommand)))
            .Where(r => r != typeof(IWeaponDamageCommand))
            .Where(r => !r.IsAbstract)
            .ToList();

        consoleWriter.WriteLine($"Found {commandTypes.Count} Commands");
        
        foreach (var commandType in commandTypes)
        {
            var constructorInfo = commandType.GetConstructor(Type.EmptyTypes);
            if (constructorInfo == null)
            {
                consoleWriter.WriteLine(
                    $"WARN: Could not load Command Type '{commandType.Name}'. No Default Empty Constructor");
                continue;
            }

            var instance = (IWeaponDamageCommand)constructorInfo.Invoke(Array.Empty<object>());

            foreach (var command in instance.Commands)
            {
                if (result.TryGetValue(command, out var existingKey))
                {
                    consoleWriter.WriteLine(
                        $"WARN: Command Type '{commandType.Name}' conflicted with {existingKey.GetType().Name} on command '{command}'");
                    continue;
                }

                result.Add(command, instance);
            }
        }
        
        return result;
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