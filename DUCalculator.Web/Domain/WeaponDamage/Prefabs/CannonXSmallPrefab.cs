using DUCalculator.Web.Domain.WeaponDamage.Damage;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Size;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class CannonXSmallPrefab : IContextPrefab
{
    public string Name => "Cannon XS";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = new WeaponId(WeaponType.Cannon, SizeType.XS);
        context.ReloadTime = 4.2;
        context.RateOfFire = 1.41;
        context.BaseDamage = 12933;
        context.WeaponCount = 6;
        context.MagazineSize = 37;
        context.DamageTypes = new List<DamageType>
        {
            new ThermicDamageType(),
            new KineticDamageType(),
        };
    }
}