using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class LaserSmallPrefab : IContextPrefab
{
    public string Name => "Laser S";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = Name;
        context.ReloadTime = 8.4;
        context.RateOfFire = 2.95;
        context.BaseDamage = 33824;
        context.WeaponCount = 5;
        context.MagazineSize = 52;
        context.DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new ElectromagneticDamageType(),
        };
    }
}