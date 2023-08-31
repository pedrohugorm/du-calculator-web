using DUCalculator.Web.Domain.WeaponDamage.Damage;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Size;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

namespace DUCalculator.Web.Domain.WeaponDamage;

public class AmmoType
{
    public WeaponId WeaponId { get; }
    public DamageType DamageType { get; }

    public string Code => $"{WeaponId.Code}{DamageType}";
    
    public AmmoType(WeaponId weaponId, DamageType damageType)
    {
        WeaponId = weaponId;
        DamageType = damageType;
    }

    private static readonly List<AmmoType> _allAmmoTypes = new();

    public static IEnumerable<AmmoType> AllAmmoTypes
    {
        get
        {
            if (_allAmmoTypes.Any())
            {
                return _allAmmoTypes;
            }

            foreach (var wt in WeaponType.AllWeaponTypes)
            {
                foreach (var st in SizeType.AllSizeTypes)
                {
                    foreach (var dt in DamageType.AllDamageTypes)
                    {
                        if (!wt.IsCompatibleWith(dt))
                        {
                            continue;
                        }
                        
                        _allAmmoTypes.Add(
                            new AmmoType(
                                new WeaponId(wt, st),
                                dt
                            )
                        );
                    }
                }
            }

            return _allAmmoTypes;
        }
    }

    public static bool TryParse(string code, out AmmoType? ammoType)
    {
        var ammoTypeMap = AllAmmoTypes.ToDictionary(k => k.Code, v => v);

        return ammoTypeMap.TryGetValue(code, out ammoType);
    }

    protected bool Equals(AmmoType other)
    {
        return WeaponId.Equals(other.WeaponId) && DamageType.Equals(other.DamageType);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AmmoType)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(WeaponId, DamageType);
    }

    public override string ToString()
    {
        return $"{WeaponId} {DamageType}";
    }
}