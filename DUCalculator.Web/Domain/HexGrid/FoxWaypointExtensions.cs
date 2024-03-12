using System.Text.Json;
using DUCalculator.Web.Domain.LiveTrace;

namespace DUCalculator.Web.Domain.HexGrid;

public static class FoxWaypointExtensions
{
    public static string ToFoxDataBankString(this List<HexGridGenerator.WaypointLine> waypointLines, string colorHex)
    {
        List<HexGridGenerator.Waypoint> waypoints = new();
        
        foreach (var line in waypointLines)
        {
            waypoints.Add(line.StartWaypoint);
            waypoints.Add(line.EndWaypoint);
        }

        var waypointJsonObj = waypoints
            .Select(x => new
            {
                name = x.Name,
                position = x.Position.PositionToVector3()
            })
            .Select(x => new
            {
                pos = new
                {
                    x = x.position.X,
                    y = x.position.Y,
                    z = x.position.Z,
                },
                color = colorHex,
                x.name
            })
            .ToArray();

        return JsonSerializer.Serialize(waypointJsonObj);
    }
}