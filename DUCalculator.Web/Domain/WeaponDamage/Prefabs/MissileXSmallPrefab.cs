using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class MissileXSmallPrefab : IContextPrefab
{
    public string Name => "Missile XS";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = Name;
        context.ReloadTime = 33.8;
        context.RateOfFire = 2.81;
        context.BaseDamage = 51731;
        context.WeaponCount = 4;
        context.MagazineSize = 15;
        context.DamageTypes = new List<IDamageType>
        {
            new AntimatterDamageType(),
            new KineticDamageType(),
        };
    }
}