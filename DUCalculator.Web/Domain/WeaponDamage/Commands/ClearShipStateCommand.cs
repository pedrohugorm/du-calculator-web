namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class ClearShipStateCommand : IWeaponDamageCommand
{
    public string[] Commands => new[] { "clearship", "cs" };
    public string Description => "Clears the Ship State. Overriding with a new state that has no weapons.";
    public string Example => "cs";

    public void Execute(WeaponDamageContext context)
    {
        context.ShipState = new ShipState();
        context.Console.WriteLine("Ship State Cleared.");
    }
}