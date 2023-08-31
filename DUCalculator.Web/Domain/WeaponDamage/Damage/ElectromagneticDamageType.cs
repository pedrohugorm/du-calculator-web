namespace DUCalculator.Web.Domain.WeaponDamage.Damage;

public class ElectromagneticDamageType : DamageType
{
    public override string Name => "Electromagnetic";
    public override string Code => "EM";

    public override double CalculateDamage(DamageReceiver receiver, double damage)
        => damage * (1 - receiver.ElectromagneticResistance);
}