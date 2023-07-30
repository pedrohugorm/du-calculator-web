namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class ResetShipCommand : IWeaponDamageCommand
{
    public string[] Commands => new[] { "resetship", "rs" };
    public string Description => "Resets Ship Damage State but keeps weapon loadout";
    public string Example => "resetship";

    public void Execute(WeaponDamageContext context)
    {
        context.ShipState.Reset();
        
        context.Console.WriteLine("Ship State Reset");
    }
}