namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class SetResistancesCommand : IWeaponDamageCommand
{
    public string[] Commands => new[] { "res" };
    public string Description => "Sets the resistance against damage";
    public string Example => "res am 0.5";

    public void Execute(WeaponDamageContext context)
    {
        SetValue(context);
        context.Console.WriteLine(context.DamageReceiver.ToString());
    }

    private void SetValue(WeaponDamageContext context)
    {
        var commandPieces = context.GetCommandPieces();
        var commandPiecesQueue = new Queue<string>(commandPieces);
        commandPiecesQueue.Dequeue();

        if (!commandPiecesQueue.Any())
        {
            context.DamageReceiver.Zero();
            context.Console.WriteLine("Resistances Zeroed");
            return;
        }
        
        var commandSecondPart = commandPiecesQueue.Dequeue()
            .ToLower();

        switch (commandSecondPart)
        {
            case "c":
            case "cannon":
            case "cannons":
                context.DamageReceiver.LoadCannonResistances();
                return;
            case "r":
            case "rail":
            case "rails":
                context.DamageReceiver.LoadRailgunResistances();
                return;
            case "m":
            case "missile":
            case "missiles":
                context.DamageReceiver.LoadMissileResistances();
                return;
            case "l":
            case "laser":
            case "lasers":
                context.DamageReceiver.LoadLaserResistances();
                return;
        }

        double resistanceValue;
        
        if (!commandPiecesQueue.Any())
        {
            resistanceValue = 1;
        }
        else
        {
            var resistanceValueString = commandPiecesQueue.Dequeue();
            if (!double.TryParse(resistanceValueString, out resistanceValue))
            {
                context.Console.WriteLine($"Invalid Resistance Value '{resistanceValueString}'");
                return;
            }
        }

        switch (commandSecondPart)
        {
            case "a":
            case "am":
                context.DamageReceiver.AntimatterResistance = resistanceValue;
                return;
            case "e":
            case "em":
                context.DamageReceiver.ElectromagneticResistance = resistanceValue;
                return;
            case "t":
            case "th":
                context.DamageReceiver.ThermicResistance = resistanceValue;
                return;
            case "k":
            case "kn":
                context.DamageReceiver.KineticResistance = resistanceValue;
                return;
        }
    }
    
}