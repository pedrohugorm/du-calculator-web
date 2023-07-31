using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class LaserMediumPrefab : IContextPrefab
{
    public string Name => "Laser M";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = Name;
        context.ReloadTime = 8.4;
        context.RateOfFire = 4.43;
        context.BaseDamage = 67649;
        context.WeaponCount = 5;
        context.MagazineSize = 52;
        context.DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new ElectromagneticDamageType(),
        };
    }
}