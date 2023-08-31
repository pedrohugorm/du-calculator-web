using DUCalculator.Web.Domain.WeaponDamage.Damage;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Size;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class MissileMediumPrefab : IContextPrefab
{
    public string Name => "Missile M";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = new WeaponId(WeaponType.Missile, SizeType.M);
        context.ReloadTime = 33.8;
        context.RateOfFire = 2.81;
        context.BaseDamage = 105047;
        context.WeaponCount = 4;
        context.MagazineSize = 15;
        context.DamageTypes = new List<DamageType>
        {
            new AntimatterDamageType(),
            new KineticDamageType(),
        };
    }
}