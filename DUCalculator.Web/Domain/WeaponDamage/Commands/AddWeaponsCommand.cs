namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class AddWeaponsCommand : IWeaponDamageCommand
{
    public string[] Commands => new[] { "add" };
    public string Description => "Adds the loaded prefab to a ship state";
    public string Example => "add";

    public void Execute(WeaponDamageContext context)
    {
        context.ShipState.AddWeapons(context);
        
        context.Console.WriteLine($"{context.WeaponCount} {context.WeaponId} Added");
    }
}