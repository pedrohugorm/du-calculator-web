using System.Numerics;
using DUCalculator.Web.Domain.Common;
using DUCalculator.Web.Domain.LiveTrace;

namespace DUCalculator.Web.Domain.HexGrid;

public class OffsetBasedHexGridGenerator : IHexGridGenerator
{
    public IHexGridGenerator.Result GenerateGrid(IHexGridGenerator.Settings settings)
    {
        const int suInKm = 200000;
        
        var baseHexRings = HexRingGenerator.GenerateRings(
            new HexRingGenerator.HexRingOptions(
                RingCount: settings.NumRings
            )
        ).Select(p => p * suInKm);

        var start = settings.EndPosition.PositionToVector3();
        var end = settings.StartPosition.PositionToVector3();

        var direction = Vector3.Normalize(end - start);
        var distance = Math.Abs(Vector3.Distance(end, start));

        var rotatedStartRings = baseHexRings
            .Select(v => RotateVectorToMatchNormal(v, direction))
            .Select(v => v + start)
            .ToArray();

        var rotatedEndRings = rotatedStartRings
            .Select(v => v + direction * distance)
            .ToArray();

        var wplList = new List<IHexGridGenerator.WaypointLine>();

        for (var i = 0; i < rotatedStartRings.Length; i++)
        {
            var posA = rotatedStartRings[i];
            var posB = rotatedEndRings[i];

            wplList.Add(
                new IHexGridGenerator.WaypointLine(
                    new IHexGridGenerator.Waypoint($"{i + 1}A", posA.Vector3ToPosition(), "A"),
                    new IHexGridGenerator.Waypoint($"{i + 1}B", posB.Vector3ToPosition(), "B")
                )
            );
        }

        return new IHexGridGenerator.Result(
            wplList,
            default,
            wplList.Count(),
            default,
            default,
            default
        );
    }

    private static Vector3 RotateVectorToMatchNormal(Vector3 vector, Vector3 normal)
    {
        var forward = new Vector3(1, 0, 0);
        
        var rotationAxis = Vector3.Cross(forward, normal)
            .NormalizeSafe();
        var rotationAngle = (float)Math.Acos(
            Math.Clamp(Vector3.Dot(forward, normal), -1, 1)
        );

        var rotQ = Quaternion.CreateFromAxisAngle(rotationAxis, rotationAngle);

        return Vector3.Transform(vector, rotQ);
    }
}