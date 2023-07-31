using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class RailgunSmallPrefab : IContextPrefab
{
    public string Name => "Railgun S";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = Name;
        context.ReloadTime = 14.1;
        context.RateOfFire = 5.91;
        context.BaseDamage = 67649;
        context.WeaponCount = 4;
        context.MagazineSize = 22;
        context.DamageTypes = new List<IDamageType>
        {
            new AntimatterDamageType(),
            new ElectromagneticDamageType(),
        };
    }
}