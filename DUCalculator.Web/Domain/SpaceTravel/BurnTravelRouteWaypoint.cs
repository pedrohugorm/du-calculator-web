using System.Numerics;

namespace DUCalculator.Web.Domain.SpaceTravel;

public class BurnTravelRouteWaypoint : ITravelRouteWaypoint
{
    public BurnTravelRouteWaypoint(string name, Vector3 position)
    {
        Name = name;
        Position = position;
    }

    public string Type => "burn";
    public string Name { get; }
    public Vector3 Position { get; }
}