namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class SetRateOfFireCommand : SetDoubleValueCommand
{
    public override string[] Commands => new[] { "rof", "cycletime", "ct" };
    public override string Description => "Sets the Rate of Fire / Cycle Time of the loaded weapon";
    public override string Example => "rof 2.6";

    public override void SetValue(WeaponDamageContext context, double value)
    {
        context.RateOfFire = value;
    }
}