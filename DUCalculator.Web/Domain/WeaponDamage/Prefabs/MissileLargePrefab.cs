using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class MissileLargePrefab : IContextPrefab
{
    public string Name => "Missile L";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = Name;
        context.ReloadTime = 33.8;
        context.RateOfFire = 2.81;
        context.BaseDamage = 149691;
        context.WeaponCount = 4;
        context.MagazineSize = 15;
        context.DamageTypes = new List<IDamageType>
        {
            new AntimatterDamageType(),
            new KineticDamageType(),
        };
    }
}