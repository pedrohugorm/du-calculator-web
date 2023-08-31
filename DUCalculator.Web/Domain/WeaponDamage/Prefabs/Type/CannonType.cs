using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

public class CannonType : WeaponType
{
    public override string Code => "C";
    public override string Name => "Cannon";
    
    public override HashSet<DamageType> AllowedAmmoTypes => new(
        new List<DamageType>
        {
            new KineticDamageType(),
            new ThermicDamageType()
        }
    );
}