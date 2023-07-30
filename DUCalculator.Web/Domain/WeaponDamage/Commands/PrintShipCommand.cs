namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class PrintShipCommand : IWeaponDamageCommand
{
    public string[] Commands => new[] { "printship", "ps", "ship" };
    public string Description => "Prints the current Ship State";
    public string Example => "ship";

    public void Execute(WeaponDamageContext context)
    {
        context.Console.Write(context.ShipState.ToString());
    }
}