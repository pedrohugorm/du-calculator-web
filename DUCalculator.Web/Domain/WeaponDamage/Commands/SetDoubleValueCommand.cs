namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public abstract class SetDoubleValueCommand : IWeaponDamageCommand
{
    public virtual string[] Commands { get; } = Array.Empty<string>();
    public virtual string Description => "";
    public virtual string Example => "";

    public void Execute(WeaponDamageContext context)
    {
        var commandPieces = context.GetCommandPieces();
        var commandPieceQueue = new Queue<string>(commandPieces);
        var commandName = commandPieceQueue.Dequeue();
        var commandValue = commandPieceQueue.Dequeue();
        
        if (double.TryParse(commandValue, out var value))
        {
            SetValue(context, value);
            
            context.Console.WriteLine($"{GetType().Name} Changed to {value:N}");
            return;
        }
        
        context.Console.WriteLine($"Invalid {GetType().Name} Value");
    }

    public virtual void SetValue(WeaponDamageContext context, double value)
    {
        
    }
}