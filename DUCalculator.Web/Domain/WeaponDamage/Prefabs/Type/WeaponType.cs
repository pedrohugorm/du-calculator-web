using System.Reflection;
using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs.Type;

public abstract class WeaponType
{
    public abstract string Code { get; }
    public abstract string Name { get; }
    
    public abstract HashSet<DamageType> AllowedAmmoTypes { get; }

    public override string ToString() => Name;

    protected bool Equals(WeaponType other)
    {
        return Code == other.Code;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((WeaponType)obj);
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }

    public bool IsCompatibleWith(DamageType damageType) => AllowedAmmoTypes.Contains(damageType);

    public static WeaponType Cannon => new CannonType();
    public static WeaponType Laser => new LaserType();
    public static WeaponType Missile => new MissileType();
    public static WeaponType Railgun => new RailgunType();

    public static IEnumerable<WeaponType> AllWeaponTypes
        = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(WeaponType)) && !t.IsAbstract)
            .Select(t => t.GetConstructor(Array.Empty<System.Type>()))
            .Select(c => (WeaponType)c.Invoke(Array.Empty<object>()));
}