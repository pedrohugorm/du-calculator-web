using System.Numerics;
using DUCalculator.Web.Domain.Common;
using DUCalculator.Web.Domain.LiveTrace;

namespace DUCalculator.Web.Domain.HexGrid;

public class OffsetBasedHexGridGenerator : IHexGridGenerator
{
    public IHexGridGenerator.Result GenerateGrid(IHexGridGenerator.Settings settings)
    {
        const float suKm = 200000;
        
        var fromPos = settings.StartPosition.PositionToVector3();
        var toPos = settings.EndPosition.PositionToVector3();
        var distance = Math.Abs(Vector3.Distance(fromPos, toPos));
        var forward = Vector3.Normalize(toPos - fromPos);

        var result = new List<IHexGridGenerator.WaypointLine>
        {
            new(
                new IHexGridGenerator.Waypoint("A1", fromPos.Vector3ToPosition()),
                new IHexGridGenerator.Waypoint("B1", toPos.Vector3ToPosition())
            )
        };

        for (var ring = 1; ring <= settings.NumRings; ring++)
        {
            var ringResult = GenerateHexagonRing(
                fromPos,
                ring, 
                settings.HexSizeSu * suKm,
                forward,
                distance
            );
        
            result.AddRange(ringResult);
        }

        var totalHexes = CalculateTotalHexagons(settings.NumRings);

        return new IHexGridGenerator.Result(
            result,
            distance,
            totalHexes,
            distance * totalHexes,
            default,
            default
        );
    }
    
    private IEnumerable<IHexGridGenerator.WaypointLine> GenerateHexagonRing(
        Vector3 refPosition,
        int ringNumber, 
        float hexSize, 
        Vector3 forward,
        float distance
    )
    {
        var refUp = new Vector3(0, 0, 1);
        var right = Vector3.Cross(forward, refUp);
        right = right.NormalizeSafe(new Vector3(1, 0, 0));
        var up = Vector3.Cross(right, forward);
        up = up.NormalizeSafe(new Vector3(0, 0, 1));

        var minHexNumber = CalculateTotalHexagons(ringNumber - 1);
        var result = new List<IHexGridGenerator.WaypointLine>();

        var totalHexagons = 6 * ringNumber;

        for (var i = 1; i <= totalHexagons; i++)
        {
            var angle = 2 * Math.PI / totalHexagons * i;
            var offsetRight = (int)(hexSize * ringNumber * Math.Cos(angle));
            var offsetUp = (int)(hexSize * ringNumber * Math.Sin(angle));

            var positionA = refPosition + right * offsetRight + up * offsetUp;
            var positionB = positionA + forward * distance;

            if (i % 2 == 0)
            {
                result.Add(
                    new IHexGridGenerator.WaypointLine(
                        new IHexGridGenerator.Waypoint($"A{minHexNumber + i}", positionA.Vector3ToPosition()),
                        new IHexGridGenerator.Waypoint($"B{minHexNumber + i}", positionB.Vector3ToPosition())
                    )
                );
            }
            else
            {
                result.Add(
                    new IHexGridGenerator.WaypointLine(
                        new IHexGridGenerator.Waypoint($"B{minHexNumber + i}", positionB.Vector3ToPosition()),
                        new IHexGridGenerator.Waypoint($"A{minHexNumber + i}", positionA.Vector3ToPosition())
                    )
                );
            }
        }

        return result;
    }
    
    private int CalculateTotalHexagons(int ringNumber)
    {
        // Formula: Number of hexagons = 1 + 6 * (1 + 2 + ... + radius)
        int totalHexagons = 1;

        for (int i = 1; i <= ringNumber; i++)
        {
            totalHexagons += 6 * i;
        }

        return totalHexagons;
    }
}