using DUCalculator.Web.Domain.WeaponDamage.Damage;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Size;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class CannonLargePrefab : IContextPrefab
{
    public string Name => "Cannon L";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = new WeaponId(WeaponType.Cannon, SizeType.L);
        context.ReloadTime = 4.2;
        context.RateOfFire = 4.75;
        context.BaseDamage = 103463;
        context.WeaponCount = 6;
        context.MagazineSize = 37;
        context.DamageTypes = new List<DamageType>
        {
            new ThermicDamageType(),
            new KineticDamageType(),
        };
    }
}