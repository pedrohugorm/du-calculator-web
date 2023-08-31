using DUCalculator.Web.Domain.WeaponDamage.Damage;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Size;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class CannonSmallPrefab : IContextPrefab
{
    public string Name => "Cannon S";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = new WeaponId(WeaponType.Cannon, SizeType.S);
        context.ReloadTime = 4.2;
        context.RateOfFire = 2.11;
        context.BaseDamage = 25866;
        context.WeaponCount = 6;
        context.MagazineSize = 37;
        context.DamageTypes = new List<DamageType>
        {
            new ThermicDamageType(),
            new KineticDamageType(),
        };
    }
}