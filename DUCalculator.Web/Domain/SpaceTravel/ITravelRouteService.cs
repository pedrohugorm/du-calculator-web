namespace DUCalculator.Web.Domain.SpaceTravel;

public interface ITravelRouteService
{
    IEnumerable<ITravelRouteWaypoint> SolveTravelPath(
        NamedPosition initialPosition,
        IEnumerable<NamedPosition> positions,
        IEnumerable<NamedPosition> beacons
    );
}