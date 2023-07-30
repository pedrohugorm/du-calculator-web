namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class SetMultiplierCommand : SetDoubleValueCommand
{
    public override string[] Commands => new[] { "mul" };
    public override string Description => "Sets a multiplier that will be applied to damage. Default = 1";
    public override string Example => "mul 0.5";

    public override void SetValue(WeaponDamageContext context, double value)
    {
        context.Multiplier = value;
    }
}