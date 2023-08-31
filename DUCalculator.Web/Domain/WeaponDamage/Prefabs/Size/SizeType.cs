using System.Reflection;

namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs.Size;

public abstract class SizeType
{
    public abstract string Code { get; }

    // ReSharper disable once InconsistentNaming
    public static SizeType XS => new ExtraSmall();
    public static SizeType S => new Small();
    public static SizeType M => new Medium();
    public static SizeType L => new Large();

    protected bool Equals(SizeType other)
    {
        return Code == other.Code;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SizeType)obj);
    }

    public override int GetHashCode() => Code.GetHashCode();

    public override string ToString() => Code;

    public static IEnumerable<SizeType> AllSizeTypes
        = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(SizeType)) && !t.IsAbstract)
            .Select(t => t.GetConstructor(Array.Empty<System.Type>()))
            .Select(c => (SizeType)c.Invoke(Array.Empty<object>()));
}