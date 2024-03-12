namespace DUCalculator.Web.Domain.HexGrid;

public static class OrderedWaypointExtensions
{
    public static List<HexGridGenerator.Waypoint> ToWaypointList(this List<HexGridGenerator.WaypointLine> waypointLines)
    {
        List<HexGridGenerator.Waypoint> waypoints = new();

        foreach (var line in waypointLines)
        {
            waypoints.Add(line.StartWaypoint);
            waypoints.Add(line.EndWaypoint);
        }
        
        return waypoints;
    }
}