using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

public class RailgunType : WeaponType
{
    public override string Code => "R";
    public override string Name => "Railgun";

    public override HashSet<DamageType> AllowedAmmoTypes
        => new(
            new List<DamageType>
            {
                new AntimatterDamageType(),
                new ElectromagneticDamageType()
            }
        );
}