using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class LaserLargePrefab : IContextPrefab
{
    public string Name => "Laser L";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = Name;
        context.ReloadTime = 8.4;
        context.RateOfFire = 6.64;
        context.BaseDamage = 135297;
        context.WeaponCount = 5;
        context.MagazineSize = 52;
        context.DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new ElectromagneticDamageType(),
        };
    }
}