using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Size;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

namespace DUCalculator.Web.Domain.WeaponDamage;

public class WeaponId
{
    public WeaponType WeaponType { get; }
    public SizeType SizeType { get; }
    
    public string Code => $"{WeaponType.Code}{SizeType.Code}".ToUpper();
    
    public WeaponId(WeaponType weaponType, SizeType sizeType)
    {
        WeaponType = weaponType;
        SizeType = sizeType;
    }

    public override string ToString()
    {
        return $"{WeaponType.Name} {SizeType}";
    }

    protected bool Equals(WeaponId other)
    {
        return WeaponType.Equals(other.WeaponType) && SizeType.Equals(other.SizeType);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((WeaponId)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(WeaponType, SizeType);
    }
}