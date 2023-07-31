using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class CannonMediumPrefab : IContextPrefab
{
    public string Name => "Cannon M";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = Name;
        context.ReloadTime = 4.2;
        context.RateOfFire = 3.16;
        context.BaseDamage = 51731;
        context.WeaponCount = 6;
        context.MagazineSize = 37;
        context.DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new KineticDamageType(),
        };
    }
}