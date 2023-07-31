using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class MissileSmallPrefab : IContextPrefab
{
    public string Name => "Missile S";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = "Missile S";
        context.ReloadTime = 33.8;
        context.RateOfFire = 2.81;
        context.BaseDamage = 73717;
        context.WeaponCount = 4;
        context.MagazineSize = 15;
        context.DamageTypes = new List<IDamageType>
        {
            new AntimatterDamageType(),
            new KineticDamageType(),
        };
    }
}