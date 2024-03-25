using System.Numerics;

namespace DUCalculator.Web.Domain.SpaceTravel;

public record NamedPosition(
    string Name,
    Vector3 Position
);