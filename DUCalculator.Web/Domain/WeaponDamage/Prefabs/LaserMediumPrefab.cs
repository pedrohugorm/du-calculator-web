using DUCalculator.Web.Domain.WeaponDamage.Damage;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Size;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class LaserMediumPrefab : IContextPrefab
{
    public string Name => "Laser M";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = new WeaponId(WeaponType.Laser, SizeType.M);
        context.ReloadTime = 8.4;
        context.RateOfFire = 4.43;
        context.BaseDamage = 67649;
        context.WeaponCount = 5;
        context.MagazineSize = 52;
        context.DamageTypes = new List<DamageType>
        {
            new ThermicDamageType(),
            new ElectromagneticDamageType(),
        };
    }
}