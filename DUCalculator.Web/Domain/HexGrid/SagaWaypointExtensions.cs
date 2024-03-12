using System.Text;

namespace DUCalculator.Web.Domain.HexGrid;

public static class SagaWaypointExtensions
{
    public static string ToSagaDataBankString(this List<HexGridGenerator.WaypointLine> waypointLines)
    {
        List<HexGridGenerator.Waypoint> waypoints = new();
        
        foreach (var line in waypointLines)
        {
            waypoints.Add(line.StartWaypoint);
            waypoints.Add(line.EndWaypoint);
        }

        var sb = new StringBuilder();

        sb.Append("{{p={");
        sb.Append(string.Join("", waypoints.Select(x => x.ToSagaWaypointString())));
        sb.Append("},n=\"Route 1\"},}");
        
        return sb.ToString();
    }
}