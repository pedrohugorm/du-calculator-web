namespace DUCalculator.Web.Domain.WeaponDamage.Damage;

public interface IDamageType
{
    double CalculateDamage(DamageReceiver receiver, double damage);
}