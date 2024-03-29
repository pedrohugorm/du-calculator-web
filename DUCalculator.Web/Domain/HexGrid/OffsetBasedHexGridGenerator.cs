using System.Numerics;
using DUCalculator.Web.Domain.LiveTrace;

namespace DUCalculator.Web.Domain.HexGrid;

public class OffsetBasedHexGridGenerator : IHexGridGenerator
{
    public IHexGridGenerator.Result GenerateGrid(IHexGridGenerator.Settings settings)
    {
        var baseHexRings = HexRingGenerator.GenerateRings(
            new HexRingGenerator.HexRingOptions(
                RingCount: settings.NumRings
            )
        );

        var start = settings.EndPosition.PositionToVector3();
        var end = settings.StartPosition.PositionToVector3();

        var direction = Vector3.Normalize(end - start);
        var distance = Math.Abs(Vector3.Distance(end, start));

        var rotatedStartRings = baseHexRings
            .Select(v => RotateVectorToMatchNormal(v, direction))
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

    private static Vector3 RotateVectorToMatchNormal(Vector3 initialVector, Vector3 targetNormal)
    {
        // Find the rotation quaternion that transforms the initial vector to align with the target normal
        var rotationQuaternion = Quaternion.CreateFromAxisAngle(Vector3.Cross(initialVector, targetNormal), (float)Math.Acos(Vector3.Dot(initialVector, targetNormal)));

        // Apply the rotation quaternion to the initial vector
        var rotatedVector = Vector3.Transform(initialVector, rotationQuaternion);

        return rotatedVector;
    }
}