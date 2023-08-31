using DUCalculator.Web.Domain.WeaponDamage.Damage;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Size;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class LaserSmallPrefab : IContextPrefab
{
    public string Name => "Laser S";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = new WeaponId(WeaponType.Laser, SizeType.S);
        context.ReloadTime = 8.4;
        context.RateOfFire = 2.95;
        context.BaseDamage = 33824;
        context.WeaponCount = 5;
        context.MagazineSize = 52;
        context.DamageTypes = new List<DamageType>
        {
            new ThermicDamageType(),
            new ElectromagneticDamageType(),
        };
    }
}