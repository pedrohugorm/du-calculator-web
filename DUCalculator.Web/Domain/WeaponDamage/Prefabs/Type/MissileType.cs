using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

public class MissileType : WeaponType
{
    public override string Code => "M";
    public override string Name => "Missile";

    public override HashSet<DamageType> AllowedAmmoTypes
        => new(
            new List<DamageType>
            {
                new AntimatterDamageType(),
                new KineticDamageType()
            }
        );
}