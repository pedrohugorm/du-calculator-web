using System.Numerics;
using System.Text;
using System.Text.Json;
using DUCalculator.Web.Domain.LiveTrace;

namespace DUCalculator.Web.Domain.HexGrid;

public static class FoxWaypointExtensions
{
    public static string ToFoxDataBankString(
        this IEnumerable<Tuple<string, Vector3>> waypoints
    )
    {
        var waypointJsonObj = waypoints
            .Select(wp => new
            {
                name = wp.Item1.Replace(" ", ""),
                position = wp.Item2,
            })
            .Select(wp => new
            {
                pos = new
                {
                    x = wp.position.X,
                    y = wp.position.Y,
                    z = wp.position.Z,
                },
                colour = "FFFF00",
                wp.name
            })
            .ToArray();

        return JsonSerializer.Serialize(waypointJsonObj);
    }

    public static string ToFoxSetPositionsScript(
        this IEnumerable<IHexGridGenerator.WaypointLine> waypointLines,
        string colorAHex,
        string colorBHex,
        string dbVarName = "db"
    )
    {
        var sb = new StringBuilder();
        sb.AppendLine("local v = {}");
        sb.AppendLine("local r = {}");
        sb.AppendLine("local i = table.insert");
        sb.AppendLine($"local a = \"{colorAHex}\"");
        sb.AppendLine($"local b = \"{colorBHex}\"");

        foreach (var line in waypointLines)
        {
            var startV = line.StartWaypoint.Position.PositionToVector3();
            var endV = line.EndWaypoint.Position.PositionToVector3();

            sb.AppendLine($"i(v,{{n=\"{line.StartWaypoint.Name}\",p={{{startV.X:F0},{startV.Y:F0},{startV.Z:F0}}},c=a}})");
            sb.AppendLine($"i(v,{{n=\"{line.EndWaypoint.Name}\",p={{{endV.X:F0},{endV.Y:F0},{endV.Z:F0}}},c=b}})");
        }

        sb.AppendLine("for k,vp in ipairs(v) do");
        sb.AppendLine("\ti(r, {name=vp.n,pos=vec3(vp.p),colour=vp.c})");
        sb.AppendLine("end");

        sb.AppendLine($"{dbVarName}.setStringValue('savemarks', json.encode(r))");

        return sb.ToString()
            .Replace("\r", "");
    }

    public static string ToFoxDataBankString(
        this IEnumerable<IHexGridGenerator.WaypointLine> waypointLines,
        string colorAHex,
        string colorBHex
    )
    {
        List<IHexGridGenerator.Waypoint> waypoints = new();

        foreach (var line in waypointLines)
        {
            waypoints.Add(line.StartWaypoint);
            waypoints.Add(line.EndWaypoint);
        }

        var waypointJsonObj = waypoints
            .Select(wp => new
            {
                name = wp.Name.Replace(" ", ""),
                position = wp.Position.PositionToVector3(),
                type = wp.Type
            })
            .Select(wp => new
            {
                pos = new
                {
                    x = wp.position.X,
                    y = wp.position.Y,
                    z = wp.position.Z,
                },
                colour = wp.type == "A" ? colorAHex : colorBHex,
                wp.name
            })
            .ToArray();

        return JsonSerializer.Serialize(waypointJsonObj);
    }
}