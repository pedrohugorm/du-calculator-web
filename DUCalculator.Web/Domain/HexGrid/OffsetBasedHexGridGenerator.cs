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

        var start = settings.StartPosition.PositionToVector3();
        var end = settings.EndPosition.PositionToVector3();

        var direction = Vector3.Normalize(end - start);
        var distance = Math.Abs(Vector3.Distance(end, start));

        var rotatedStartRings = baseHexRings
            .Select(v => RotateVectorToMatchNormal(v, direction) + start)
            .ToArray();

        var rotatedEndRings = rotatedStartRings
            .Select(v => v + direction * distance)
            .ToArray();

        var waypointMap = new Dictionary<string, IHexGridGenerator.Waypoint>();
        for (var i = 0; i < rotatedStartRings.Length; i++)
        {
            var name = $"{i + 1}A";
            waypointMap.Add(
                name,
                new IHexGridGenerator.Waypoint(name, rotatedStartRings[i].Vector3ToPosition(), "A")
            );
        }

        for (var i = 0; i < rotatedEndRings.Length; i++)
        {
            var name = $"{i + 1}B";
            waypointMap.Add(
                name,
                new IHexGridGenerator.Waypoint(name, rotatedEndRings[i].Vector3ToPosition(), "B")
            );
        }

        var wplList = new List<IHexGridGenerator.WaypointLine>
        {
            new(
                waypointMap["1A"],
                waypointMap["1B"]
            )
        };

        var currentId = 1;
        var previousType = "A";
        var currentType = "B";

        var typeMap = new Dictionary<int, string>
        {
            { 0, "A" },
            { 1, "B" }
        };
        var typeVal = 1;

        var orderedWaypoints = new Queue<IHexGridGenerator.Waypoint>();
        
        for (;;)
        {
            var different = previousType != currentType;

            if (different)
            {
                currentId++;
            }
            else
            {
                typeVal = (typeVal + 1) % 2;
            }
            
            previousType = currentType;
            currentType = typeMap[typeVal];

            if (!waypointMap.TryGetValue($"{currentId}{currentType}", out var wp))
            {
                break;
            }
            
            orderedWaypoints.Enqueue(wp);
        }

        while (orderedWaypoints.Any())
        {
            wplList.Add(
                new IHexGridGenerator.WaypointLine(
                    orderedWaypoints.Dequeue(),
                    orderedWaypoints.Dequeue()
                ).Reversed(settings.ReverseOrder)
            );
        }

        if (settings.ReverseOrder)
        {
            wplList.Reverse();
        }

        return new IHexGridGenerator.Result(
            wplList,
            default,
            wplList.Count,
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