using DUCalculator.Web.Domain.LiveTrace;

namespace DUCalculator.Web.Domain.HexGrid;

public interface IHexGridGenerator
{
    Result GenerateGrid(Settings settings);
    
    public record Settings(
        string StartPosition,
        string EndPosition,
        bool ReverseOrder,
        int NumRings = 3,
        float HexSizeSu = 3.35f
    );

    public record Result(
        IEnumerable<WaypointLine> WaypointLines,
        double SingleLineDistance,
        int NumberOfLines,
        double TotalDistance,
        double Scanning,
        double Reposition
    );

    public record Waypoint(
        string Name,
        string Position,
        string Type
    )
    {
        public string ToSagaWaypointString()
        {
            var p = Position.PositionToVector3();
            
            return string.Join(
                "",
                "{n=\"",
                Name,
                "\",",
                "c={",
                $"x={p.X:F2},",
                $"y={p.Y:F2},",
                $"z={p.Z:F2}",
                "}},"
            );
        }
    };

    public record WaypointLine(
        Waypoint StartWaypoint,
        Waypoint EndWaypoint
    )
    {
        public WaypointLine Reversed(bool reverse)
        {
            if (reverse)
            {
                return new WaypointLine(
                    EndWaypoint,
                    StartWaypoint
                );
            }

            return this;
        }
    }
}