namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class SetReloadTimeCommand : SetDoubleValueCommand
{
    public override string[] Commands=> new[] { "reload" };
    public override string Description => "Set the reload time to a given value in seconds";
    public override string Example => "reload 5";

    public override void SetValue(WeaponDamageContext context, double value)
    {
        context.ReloadTime = value;
    }
}