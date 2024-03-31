using System.Numerics;

namespace DUCalculator.Web.Domain.HexGrid;

public static class HexRingGenerator
{
    public static IEnumerable<Vector3> GenerateRings(HexRingOptions options)
    {
        var hexFaceRadius = HexagonFaceDistance(options.SphereRadius);
        var hexDiameter = hexFaceRadius * 2;
        var hexBaseSize = CalculateUnknownTriangleSide(hexDiameter, hexFaceRadius);

        var hexPositions = new List<Vector3> { Vector3.Zero };
        
        var offsetBySide = new Dictionary<int, Vector3>
        {
            {1, new Vector3(0, hexBaseSize, hexFaceRadius)},
            {2, new Vector3(0, hexBaseSize, -hexFaceRadius)},
            {3, new Vector3(0, 0, -hexDiameter)},
            {4, new Vector3(0, -hexBaseSize, -hexFaceRadius)},
            {5, new Vector3(0, -hexBaseSize, hexFaceRadius)},
            {6, new Vector3(0, 0, hexDiameter)},
        };

        for (var ring = 1; ring <= options.RingCount; ring++)
        {
            var hexesPerSide = ring + 1;
            var currentHexPos = new Vector3(0, -hexBaseSize, hexFaceRadius) * ring;
            
            for (var side = 1; side <= 6; side++)
            {
                var offset = offsetBySide[side];
                
                for (var sideCount = 1; sideCount < hexesPerSide; sideCount++)
                {
                    var hexPos = currentHexPos + offset * sideCount;
                    hexPositions.Add(hexPos);
                }
                
                currentHexPos = hexPositions.Last();
            }
        }

        return hexPositions;
    }
    
    private static float HexagonFaceDistance(float radius)
    {
        return (float)(Math.Sqrt(3) / 2 * radius);
    }

    private static float CalculateUnknownTriangleSide(double hypotenuse, double knownSide)
    {
        var unknownSideSquared = hypotenuse * hypotenuse - knownSide * knownSide;
        var unknownSide = Math.Sqrt(unknownSideSquared);

        return (float)unknownSide;
    }

    public record HexRingOptions(
        float SphereRadius = 2,
        float RingCount = 5
    );
}