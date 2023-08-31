namespace DUCalculator.Web.Domain.WeaponDamage.Damage;

public abstract class DamageType
{
    public abstract string Name { get; }
    public abstract string Code { get; }
    
    public abstract double CalculateDamage(DamageReceiver receiver, double damage);

    public static readonly IEnumerable<DamageType> AllDamageTypes = new List<DamageType>
    {
        new AntimatterDamageType(),
        new ElectromagneticDamageType(),
        new KineticDamageType(),
        new ThermicDamageType(),
    };

    protected bool Equals(DamageType other)
    {
        return Code == other.Code;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((DamageType)obj);
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }
    
    public override string ToString() => Code;
}