using DUCalculator.Web.Domain.WeaponDamage.Damage;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Size;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public class RailgunLargePrefab : IContextPrefab
{
    public string Name => "Railgun L";
    
    public void Load(WeaponDamageContext context)
    {
        context.WeaponId = new WeaponId(WeaponType.Railgun, SizeType.L);
        context.ReloadTime = 14.1;
        context.RateOfFire = 13.29;
        context.BaseDamage = 270595;
        context.WeaponCount = 4;
        context.MagazineSize = 22;
        context.DamageTypes = new List<DamageType>
        {
            new AntimatterDamageType(),
            new ElectromagneticDamageType(),
        };
    }
}