using DUCalculator.Web.Domain.Common;

namespace DUCalculator.Web.Domain.LiveTrace;

public class LiveTraceExecutionContext
{
    public SpaceUnit ClipboardDistance = SpaceUnit.SU(10);
    
    public List<SpaceUnit> TraceOutputDistanceList = new()
    {
        new SpaceUnit(5),
        new SpaceUnit(10),
        new SpaceUnit(15),
        new SpaceUnit(20),
        new SpaceUnit(40),
    };

    public IConsoleWriter Writer { get; }
    public PositionAggregator PositionAggregator { get; }

    public Action<string> SetLastPosition;

    public LiveTraceExecutionContext(
        IConsoleWriter writer,
        PositionAggregator positionAggregator, 
        Action<string> setLastPosition
    )
    {
        SetLastPosition = setLastPosition;
        Writer = writer;
        PositionAggregator = positionAggregator;
    }
}