using DUCalculator.Web.Domain.WeaponDamage.Damage;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Size;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class MissileXSmallPrefab : IContextPrefab
{
    public string Name => "Missile XS";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = new WeaponId(WeaponType.Missile, SizeType.XS);
        context.ReloadTime = 33.8;
        context.RateOfFire = 2.81;
        context.BaseDamage = 51731;
        context.WeaponCount = 4;
        context.MagazineSize = 15;
        context.DamageTypes = new List<DamageType>
        {
            new AntimatterDamageType(),
            new KineticDamageType(),
        };
    }
}