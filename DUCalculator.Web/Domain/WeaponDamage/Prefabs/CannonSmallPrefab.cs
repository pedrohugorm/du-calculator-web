using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class CannonSmallPrefab : IContextPrefab
{
    public string Name => "Cannon S";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = Name;
        context.ReloadTime = 4.2;
        context.RateOfFire = 2.11;
        context.BaseDamage = 25866;
        context.WeaponCount = 6;
        context.MagazineSize = 37;
        context.DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new KineticDamageType(),
        };
    }
}