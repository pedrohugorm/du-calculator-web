using System.Numerics;

namespace DUCalculator.Web.Domain.Common;

public struct SpaceUnit
{
    public float Value { get; set; }

    public SpaceUnit(float value)
    {
        Value = value;
    }

    public float ToMeters() => Value * 200000;

    public bool Equals(SpaceUnit other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is SpaceUnit other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(SpaceUnit lhs, SpaceUnit rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(SpaceUnit lhs, SpaceUnit rhs)
    {
        return !(lhs == rhs);
    }

    public static Vector3 operator *(Vector3 v, SpaceUnit su)
    {
        return v * su.ToMeters();
    }
        
    public static Vector3 operator /(Vector3 v, SpaceUnit su)
    {
        return v / su.ToMeters();
    }

    public static SpaceUnit SU(float su) => new(su);

    public override string ToString()
    {
        return $"{Value}su";
    }
}