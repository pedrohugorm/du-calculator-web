using System.Numerics;
using System.Text;
using DUCalculator.Web.Domain.LiveTrace;
using DUCalculator.Web.Domain.SpaceTravel;

namespace DUCalculator.Web.Domain.HexGrid;

public static class SagaWaypointExtensions
{
    public static string ToSagaDataBankString(this IEnumerable<ITravelRouteWaypoint> waypoints)
    {
        var sb = new StringBuilder();

        sb.Append("{{p={");
        sb.Append(string.Join("", waypoints.Select(x => x.Position.ToSagaWaypointString(x.Name))));
        sb.Append("},n=\"Route 1\"},}");
        
        return sb.ToString();
    }
    
    public static string ToSagaDataBankString(this IEnumerable<IHexGridGenerator.WaypointLine> waypointLines)
    {
        List<IHexGridGenerator.Waypoint> waypoints = new();
        
        foreach (var line in waypointLines)
        {
            waypoints.Add(line.StartWaypoint);
            waypoints.Add(line.EndWaypoint);
        }

        var sb = new StringBuilder();

        sb.Append("{{p={");
        sb.Append(string.Join("", waypoints.Select(x => x.Position.PositionToVector3().ToSagaWaypointString(x.Name))));
        sb.Append("},n=\"Route 1\"},}");
        
        return sb.ToString();
    }
    
    private static string ToSagaWaypointString(this Vector3 position, string name)
    {
        return string.Join(
            "",
            "{n=\"",
            name,
            "\",",
            "c={",
            $"x={position.X:F2},",
            $"y={position.Y:F2},",
            $"z={position.Z:F2}",
            "}},"
        );
    }
}