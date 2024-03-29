namespace DUCalculator.Web.Domain.HexGrid;

public interface IHexGridGenerator
{
    Result GenerateGrid(Settings settings);
    
    public record Settings(
        string StartPosition,
        string EndPosition,
        bool ReverseOrder,
        int NumRings = 3
    );

    public record Result(
        IEnumerable<WaypointLine> WaypointLines,
        double SingleLineDistance,
        int NumberOfLines,
        double TotalDistance,
        double Scanning,
        double Reposition
    )
    {
        public static Result Null()
        {
            return new Result(
                new List<WaypointLine>(),
                default,
                default,
                default,
                default,
                default
            );
        }
    }

    public record Waypoint(
        string Name,
        string Position,
        string Type
    );

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