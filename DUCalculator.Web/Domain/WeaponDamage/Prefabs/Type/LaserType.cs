using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

public class LaserType : WeaponType
{
    public override string Code => "L";
    public override string Name => "Laser";

    public override HashSet<DamageType> AllowedAmmoTypes
        => new(
            new List<DamageType>
            {
                new ThermicDamageType(),
                new ElectromagneticDamageType()
            }
        );
}