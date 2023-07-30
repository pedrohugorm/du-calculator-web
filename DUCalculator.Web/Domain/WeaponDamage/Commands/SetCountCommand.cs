namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class SetCountCommand : SetDoubleValueCommand
{
    public override string[] Commands => new[] { "count" };
    public override string Description => "Sets the weapon count to a given number";
    public override string Example => "count 4";

    public override void SetValue(WeaponDamageContext context, double value)
    {
        context.WeaponCount = value;
    }
}