namespace DUCalculator.Web.Domain.HexGrid;

public static class OrderedWaypointExtensions
{
    public static List<IHexGridGenerator.Waypoint> ToWaypointList(this IEnumerable<IHexGridGenerator.WaypointLine> waypointLines)
    {
        List<IHexGridGenerator.Waypoint> waypoints = new();

        foreach (var line in waypointLines)
        {
            waypoints.Add(line.StartWaypoint);
            waypoints.Add(line.EndWaypoint);
        }
        
        return waypoints;
    }
}