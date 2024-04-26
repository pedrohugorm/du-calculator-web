using System.Text;
using DUCalculator.Web.Domain.LiveTrace;
using DUCalculator.Web.Domain.SpaceTravel;

namespace DUCalculator.Web.Domain.HexGrid;

public static class DeadRankWaypointExtensions
{
    public static string ToDeadRankRouteFile(this IEnumerable<ITravelRouteWaypoint> waypoints)
    {
        var sb = new StringBuilder();

        sb.AppendLine("return {");
        sb.AppendLine("\troute = {");
        var wpList = waypoints.ToList();

        var posNum = 0;
        for (var i = 0; i < wpList.Count; i++)
        {
            var wp = wpList[i];

            posNum++;
            sb.AppendLine($"\t\t[{posNum}] = '{wp.Position.Vector3ToPosition()}',");
            posNum++;
            sb.AppendLine($"\t\t[{posNum}] = '{wp.Position.Vector3ToPosition()}',");
        }

        sb.AppendLine("\t}");
        sb.AppendLine("}");

        return sb.ToString();
    }

    public static string ToDeadRankRouteByName(this IEnumerable<IHexGridGenerator.WaypointLine> waypointLines,
        string name)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"\t{name} = {{");

        var wpList = waypointLines.ToList();

        var posNum = 0;
        for (var i = 0; i < wpList.Count; i++)
        {
            var wp = wpList[i];

            posNum++;
            sb.AppendLine($"\t\t[{posNum}] = '{wp.StartWaypoint.Position}',");
            posNum++;
            sb.AppendLine($"\t\t[{posNum}] = '{wp.EndWaypoint.Position}',");
        }

        sb.AppendLine("\t},");
        return sb.ToString();
    }

    public static string ToDeadRankSingleRouteFileByName(
        this IEnumerable<IHexGridGenerator.WaypointLine> waypointLines,
        string name
    )
    {
        var sb = new StringBuilder();
        sb.AppendLine("return {");
        sb.AppendLine(waypointLines.ToDeadRankRouteByName(name));
        sb.AppendLine("}");
        
        return sb.ToString();
    }
}