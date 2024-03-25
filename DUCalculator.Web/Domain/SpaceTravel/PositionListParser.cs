using DUCalculator.Web.Domain.LiveTrace;

namespace DUCalculator.Web.Domain.SpaceTravel;

public static class PositionListParser
{
    public static IEnumerable<NamedPosition> ParseText(string value)
    {
        var result = new List<NamedPosition>();
        
        var lines = value.Split(Environment.NewLine);

        var counter = 1;
        foreach (var line in lines)
        {
            var pieces = line.Split("::");
            if (pieces.Length != 2)
            {
                continue;
            }

            var name = pieces[0];
            if (string.IsNullOrEmpty(name))
            {
                name = $"WP{counter}";
            }
            
            var position = $"::{pieces[1]}".PositionToVector3();
            
            result.Add(new NamedPosition(name, position));

            counter++;
        }

        return result;
    }
}