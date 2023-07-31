using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class CannonXSmallPrefab : IContextPrefab
{
    public string Name => "Cannon XS";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = Name;
        context.ReloadTime = 4.2;
        context.RateOfFire = 1.41;
        context.BaseDamage = 12933;
        context.WeaponCount = 6;
        context.MagazineSize = 37;
        context.DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new KineticDamageType(),
        };
    }
}