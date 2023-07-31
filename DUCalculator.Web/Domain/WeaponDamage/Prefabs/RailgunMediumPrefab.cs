using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class RailgunMediumPrefab : IContextPrefab
{
    public string Name => "Railgun M";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = Name;
        context.ReloadTime = 14.1;
        context.RateOfFire = 8.86;
        context.BaseDamage = 135297;
        context.WeaponCount = 4;
        context.MagazineSize = 22;
        context.DamageTypes = new List<IDamageType>
        {
            new AntimatterDamageType(),
            new ElectromagneticDamageType(),
        };
    }
}