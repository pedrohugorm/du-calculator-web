using System.Numerics;

namespace DUCalculator.Web.Domain.Common;

public static class VectorExtensions
{
    public static Vector3 NormalizeSafe(this Vector3 vector3, Vector3? @default = null)
    {
        if (@default == null)
        {
            @default = Vector3.Zero; 
        }
        
        if (vector3.Length() < double.Epsilon)
        {
            return @default.Value;
        }

        return Vector3.Normalize(vector3);
    }
}