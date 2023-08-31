using DUCalculator.Web.Domain.WeaponDamage.Damage;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Size;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class CannonMediumPrefab : IContextPrefab
{
    public string Name => "Cannon M";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = new WeaponId(WeaponType.Cannon, SizeType.M);
        context.ReloadTime = 4.2;
        context.RateOfFire = 3.16;
        context.BaseDamage = 51731;
        context.WeaponCount = 6;
        context.MagazineSize = 37;
        context.DamageTypes = new List<DamageType>
        {
            new ThermicDamageType(),
            new KineticDamageType(),
        };
    }
}