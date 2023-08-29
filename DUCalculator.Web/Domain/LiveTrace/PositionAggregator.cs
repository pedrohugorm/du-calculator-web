using System.Numerics;
using DUCalculator.Web.Domain.Common;

namespace DUCalculator.Web.Domain.LiveTrace;

public class PositionAggregator
{
    private List<Vector3> Positions { get; set; } = new();

    public bool PushPosition(Vector3 vector3)
    {
        if (Positions.Any() && Positions.Last() == vector3)
        {
            return false;
        }
        
        Positions.Add(vector3);

        return true;
    }

    public bool CanDoTrace()
    {
        return Positions.Count >= 2;
    }

    public Vector3 TraceTo(SpaceUnit su)
    {
        if (!CanDoTrace())
        {
            return Vector3.Zero;
        }

        var positionsBuffer = Positions.ToList();
        var last = positionsBuffer.Last();
        positionsBuffer.Remove(last);
        var before = positionsBuffer.Last();

        var direction = Vector3.Normalize(last - before);

        var projection = last + direction * su;

        return projection;
    }

    public void ClearAndKeepLast()
    {
        Positions = new List<Vector3>
        {
            Positions.Last()
        };
    }

    public void Clear()
    {
        Positions.Clear();
    }
}