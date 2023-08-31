namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class LimitAmmoCommand : IWeaponDamageCommand
{
    public string[] Commands => new[] { "la", "limitammo" };
    public string Description => "Limits the ammo of based on the type";
    public string Example => $"la mmam: Limits the ammo of a Missile M anti-matter. Available Types {GetAmmoTypesString()}";

    public void Execute(WeaponDamageContext context)
    {
        var commandPieces = context.GetCommandPieces();
        var commandPiecesQueue = new Queue<string>(commandPieces);
        commandPiecesQueue.Dequeue();

        if (commandPiecesQueue.Count != 2)
        {
            context.Console.WriteLine("Invalid Command");
            return;
        }
        
        var type = commandPiecesQueue.Dequeue().ToUpper();
        var ammoSizeString = commandPiecesQueue.Dequeue();

        if (!int.TryParse(ammoSizeString, out var ammoSize))
        {
            context.Console.WriteLine("Invalid Ammo Size");
            return;
        }

        if (!AmmoType.TryParse(type, out var ammoType))
        {
            var validTypes = GetAmmoTypesString();
            context.Console.WriteLine($"'{type}' is an invalid ammo type. Valid types are: {validTypes}");
            return;
        }

        if (context.ShipState.AmmoContainer.ContainsKey(ammoType!))
        {
            context.ShipState.AmmoContainer.Remove(ammoType!);
        }
        
        context.ShipState.AmmoContainer.Add(ammoType!, ammoSize);
        context.Console.WriteLine($"Ammo Limits for '{ammoType}' set to {ammoSize} rounds");
    }
    
    private string GetAmmoTypesString()
    {
        return string.Join(", ", AmmoType.AllAmmoTypes.Select(x => x.Code));
    }
}