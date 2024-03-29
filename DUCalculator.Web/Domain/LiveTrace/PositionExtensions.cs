using System.Numerics;
using System.Text;

namespace DUCalculator.Web.Domain.LiveTrace;

public static class PositionExtensions
{
    public static Vector3 PositionToVector3(this string position)
    {
        var replacedString = position.Replace("::pos{", string.Empty)
            .Replace("}", string.Empty);

        var pieces = replacedString.Split(',', StringSplitOptions.RemoveEmptyEntries);

        if (pieces.Length != 5)
        {
            throw new ArgumentException(
                $"Invalid DU Position format. Example: ::pos{{0,0,5236583.0860,-9051901.5198,-857517.7448}}. Param = {position}", 
                nameof(position)
            );
        }
        
        var queue = new Queue<string>(pieces);
        queue.Dequeue();
        queue.Dequeue();

        var x = float.Parse(queue.Dequeue());
        var y = float.Parse(queue.Dequeue());
        var z = float.Parse(queue.Dequeue());

        return new Vector3(x, y, z);
    }

    public static string Vector3ToPosition(this Vector3 vector3, int planetId = 0)
    {
        var sb = new StringBuilder();

        sb.Append("::pos{0,");
        sb.Append(planetId);
        sb.Append(',');
        sb.Append(vector3.X.ToString("F0"));
        sb.Append(',');
        sb.Append(vector3.Y.ToString("F0"));
        sb.Append(',');
        sb.Append(vector3.Z.ToString("F0"));
        sb.Append('}');
        
        return sb.ToString();
    }
}