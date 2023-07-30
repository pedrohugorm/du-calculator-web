namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class SetDamageCommand : SetDoubleValueCommand
{
    public override string[] Commands => new[] { "damage", "dmg" };
    public override string Description => "Sets the base damage of the loaded weapon";
    public override string Example => "dmg 1000";

    public override void SetValue(WeaponDamageContext context, double value)
    {
        context.BaseDamage = value;
    }
}