namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class SetTargetDamageCommand : SetDoubleValueCommand
{
    public override string[] Commands => new[] { "td" };
    public override string Description => "Sets the target the damage the simulation will try to reach";
    public override string Example => "td 200000000";

    public override void SetValue(WeaponDamageContext context, double value)
    {
        context.TargetDamage = value;
    }
}