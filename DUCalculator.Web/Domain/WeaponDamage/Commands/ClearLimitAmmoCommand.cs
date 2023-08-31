namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class ClearLimitAmmoCommand : IWeaponDamageCommand
{
    public string[] Commands => new[] { "cla" };
    public string Description => "Clears the limit of ammo of a type";
    public string Example => "cla all; cla mmam";
    public void Execute(WeaponDamageContext context)
    {
        var commandPieces = context.GetCommandPieces();
        var commandPiecesQueue = new Queue<string>(commandPieces);
        commandPiecesQueue.Dequeue();
        
        if (!commandPiecesQueue.Any())
        {
            context.ShipState.AmmoContainer.Clear();
            context.Console.WriteLine("All Ammo Limits Cleared");
        }
        
        var type = commandPiecesQueue.Dequeue().ToUpper();
        
        if (!AmmoType.TryParse(type, out var ammoType))
        {
            var validTypes = string.Join(", ", AmmoType.AllAmmoTypes.Select(x => x.Code));
            context.Console.WriteLine($"'{type}' is an invalid ammo type. Valid types are: {validTypes}");
            return;
        }

        Execute(context, ammoType!);
    }

    public void Execute(WeaponDamageContext context, AmmoType ammoType)
    {
        context.ShipState.AmmoContainer.Remove(ammoType);
        
        context.Console.WriteLine($"Ammo Limit for '{ammoType}' Cleared");
    }
}