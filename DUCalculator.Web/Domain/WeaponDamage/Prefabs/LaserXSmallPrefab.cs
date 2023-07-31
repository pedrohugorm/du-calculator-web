using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class LaserXSmallPrefab : IContextPrefab
{
    public string Name => "Laser XS";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = Name;
        context.ReloadTime = 8.4;
        context.RateOfFire = 1.97;
        context.BaseDamage = 16912;
        context.WeaponCount = 5;
        context.MagazineSize = 52;
        context.DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new ElectromagneticDamageType(),
        };
    }
}