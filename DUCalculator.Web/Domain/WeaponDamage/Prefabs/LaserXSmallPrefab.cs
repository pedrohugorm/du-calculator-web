using DUCalculator.Web.Domain.WeaponDamage.Damage;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Size;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class LaserXSmallPrefab : IContextPrefab
{
    public string Name => "Laser XS";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = new WeaponId(WeaponType.Laser, SizeType.XS);
        context.ReloadTime = 8.4;
        context.RateOfFire = 1.97;
        context.BaseDamage = 16912;
        context.WeaponCount = 5;
        context.MagazineSize = 52;
        context.DamageTypes = new List<DamageType>
        {
            new ThermicDamageType(),
            new ElectromagneticDamageType(),
        };
    }
}