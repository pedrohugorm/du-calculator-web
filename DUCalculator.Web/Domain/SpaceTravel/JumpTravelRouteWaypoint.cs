using System.Numerics;

namespace DUCalculator.Web.Domain.SpaceTravel;

public class JumpTravelRouteWaypoint : ITravelRouteWaypoint
{
    public JumpTravelRouteWaypoint(string name, Vector3 position)
    {
        Name = name;
        Position = position;
    }

    public string Type => "jump";
    public string Name { get; }
    public Vector3 Position { get; }
}