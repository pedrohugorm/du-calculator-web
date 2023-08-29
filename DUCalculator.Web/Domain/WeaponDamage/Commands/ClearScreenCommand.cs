namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class ClearScreenCommand : IWeaponDamageCommand
{
    public string[] Commands => new[] { "c", "clear" };
    public string Description => "Clears the console";
    public string Example => "clear";

    public void Execute(WeaponDamageContext context)
    {
    }
}