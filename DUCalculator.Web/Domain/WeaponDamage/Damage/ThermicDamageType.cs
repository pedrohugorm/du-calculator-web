namespace DUCalculator.Web.Domain.WeaponDamage.Damage;

public class ThermicDamageType : DamageType
{
    public override string Name => "Thermic";
    public override string Code => "TH";

    public override double CalculateDamage(DamageReceiver receiver, double damage)
        => damage * (1 - receiver.ThermicResistance);
}