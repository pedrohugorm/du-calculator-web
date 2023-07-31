using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class CannonLargePrefab : IContextPrefab
{
    public string Name => "Cannon L";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = Name;
        context.ReloadTime = 4.2;
        context.RateOfFire = 4.75;
        context.BaseDamage = 103463;
        context.WeaponCount = 6;
        context.MagazineSize = 37;
        context.DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new KineticDamageType(),
        };
    }
}