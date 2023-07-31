using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class RailgunXSmallPrefab : IContextPrefab
{
    public string Name => "Railgun XS";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = Name;
        context.ReloadTime = 14.1;
        context.RateOfFire = 3.94;
        context.BaseDamage = 33824;
        context.WeaponCount = 4;
        context.MagazineSize = 22;
        context.DamageTypes = new List<IDamageType>
        {
            new AntimatterDamageType(),
            new ElectromagneticDamageType(),
        };
    }
}