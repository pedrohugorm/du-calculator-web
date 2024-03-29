using System.Numerics;

namespace DUCalculator.Web.Domain.SpaceTravel;

public interface ITravelRouteWaypoint
{
    string Type { get; }
    string Name { get; }
    Vector3 Position { get; }

    void AddOffset(Vector3 offset);
}