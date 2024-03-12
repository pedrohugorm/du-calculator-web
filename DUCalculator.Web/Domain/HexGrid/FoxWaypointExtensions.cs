﻿using System.Text.Json;
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
            .Select(wp => new
            {
                name = wp.Name,
                position = wp.Position.PositionToVector3()
            })
            .Select(wp => new
            {
                pos = new
                {
                    x = wp.position.X,
                    y = wp.position.Y,
                    z = wp.position.Z,
                },
                colour = colorHex,
                wp.name
            })
            .ToArray();

        return JsonSerializer.Serialize(waypointJsonObj);
    }
}