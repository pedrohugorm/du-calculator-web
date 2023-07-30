using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class SetDamageTypeCommand : IWeaponDamageCommand
{
    public string[] Commands => new[] { "dt" };

    public string Description =>
        "Sets the damage type of a loaded weapon. Useful to set it to only one damage type instead of the standard two";

    public string Example => "dt am";
    public void Execute(WeaponDamageContext context)
    {
        var commandPieces = context.GetCommandPieces();
        var commandPiecesQueue = new Queue<string>(commandPieces);
        commandPiecesQueue.Dequeue();

        if (!commandPiecesQueue.Any())
        {
            context.Console.WriteLine("No Damage Type in command. Aborting.");
            
            return;
        }

        var damageType = commandPiecesQueue.Dequeue().ToLower();

        switch (damageType)
        {
            case "a":
            case "am":
                context.DamageTypes.Clear();
                context.DamageTypes.Add(new AntimatterDamageType());
                break;
            case "e":
            case "em":
                context.DamageTypes.Clear();
                context.DamageTypes.Add(new ElectromagneticDamageType());
                break;
            case "t":
            case "th":
                context.DamageTypes.Clear();
                context.DamageTypes.Add(new ThermicDamageType());
                break;
            case "k":
            case "kn":
                context.DamageTypes.Clear();
                context.DamageTypes.Add(new KineticDamageType());
                break;
        }

        foreach (var dt in context.DamageTypes)
        {
            context.Console.WriteLine($"{dt}");
        }
    }
}