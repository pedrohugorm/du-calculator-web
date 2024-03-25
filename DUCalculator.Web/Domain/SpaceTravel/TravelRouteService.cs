using System.Numerics;

namespace DUCalculator.Web.Domain.SpaceTravel;

public class TravelRouteService : ITravelRouteService
{
    public IEnumerable<ITravelRouteWaypoint> SolveTravelPath(
        NamedPosition initialPosition,
        IEnumerable<NamedPosition> positions,
        IEnumerable<NamedPosition> beacons
    )
    {
        var route = new List<ITravelRouteWaypoint>();
        var unvisited = new List<NamedPosition>(positions);
        var current = initialPosition;

        route.Add(new BurnTravelRouteWaypoint(current.Name, current.Position));
        unvisited.Remove(current);

        var counter = 1;
        while (unvisited.Count > 0)
        {
            var refPosition = current;

            var nearestIndex = FindNearestNeighbor(current, unvisited);
            current = unvisited[nearestIndex];

            if (beacons != null && beacons.Any() && TryFindAlignedBeacon(
                    beacons,
                    refPosition,
                    current,
                    out var beacon
                ))
            {
                route.Add(new JumpTravelRouteWaypoint(beacon!.Name, beacon.Position));
            }

            route.Add(new BurnTravelRouteWaypoint($"{current.Name}_{counter}", current.Position));
            unvisited.RemoveAt(nearestIndex);

            counter++;
        }

        return route;
    }

    private static bool TryFindAlignedBeacon(
        IEnumerable<NamedPosition> beacons,
        NamedPosition refPos,
        NamedPosition destinationPos,
        out NamedPosition? beacon
    )
    {
        var alignmentScore = new Dictionary<NamedPosition, float>();

        foreach (var namedPosition in beacons)
        {
            var refPosDot = Vector3.Dot(refPos.Position, namedPosition.Position);
            var refPosDestinationDot = Vector3.Dot(refPos.Position, destinationPos.Position);

            var dotDiff = Math.Abs(refPosDot - refPosDestinationDot);

            alignmentScore.Add(namedPosition, dotDiff);
        }

        var smallestAlignment = alignmentScore.MinBy(x => x.Value);

        if (smallestAlignment.Value <= 0.2)
        {
            beacon = smallestAlignment.Key;

            return true;
        }

        beacon = null;
        return false;
    }

    private static int FindNearestNeighbor(NamedPosition current, IReadOnlyList<NamedPosition> unvisited)
    {
        var minDistance = double.MaxValue;
        var nearestIndex = -1;

        for (var i = 0; i < unvisited.Count; i++)
        {
            var distance = Distance(current, unvisited[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestIndex = i;
            }
        }

        return nearestIndex;
    }

    private static double Distance(NamedPosition a, NamedPosition b)
    {
        return Vector3.Distance(a.Position, b.Position);
    }
}