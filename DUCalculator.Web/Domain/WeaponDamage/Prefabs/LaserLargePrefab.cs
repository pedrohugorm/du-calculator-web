using DUCalculator.Web.Domain.WeaponDamage.Damage;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Size;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class LaserLargePrefab : IContextPrefab
{
    public string Name => "Laser L";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = new WeaponId(WeaponType.Laser, SizeType.L);
        context.ReloadTime = 8.4;
        context.RateOfFire = 6.64;
        context.BaseDamage = 135297;
        context.WeaponCount = 5;
        context.MagazineSize = 52;
        context.DamageTypes = new List<DamageType>
        {
            new ThermicDamageType(),
            new ElectromagneticDamageType(),
        };
    }
}