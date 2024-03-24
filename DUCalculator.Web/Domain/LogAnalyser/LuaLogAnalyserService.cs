using System.Text;
using System.Text.Json;

namespace DUCalculator.Web.Domain.LogAnalyser;

public class LuaLogAnalyserService : ILuaLogAnalyserService
{
    public LogAnalysisResult Analyse(string log)
    {
        var entries = new List<ILuaLogEntry>();

        try
        {
            var luaLog = JsonSerializer.Deserialize<DULuaLog>(log);

            if (luaLog is null or { messageList: null })
            {
                return new LogAnalysisResult(
                    false,
                    "Failed to obtain data - the format may be incorrect",
                    new List<ILuaLogEntry>()
                );
            }

            ConstructEntry? constructEntry = null;

            foreach (var message in luaLog.messageList)
            {
                if (message.content.Contains("CONTACT:"))
                {
                    if (!TryParseConstruct(message.content, out constructEntry))
                    {
                        constructEntry = null;
                    }

                    continue;
                }

                if (constructEntry != null)
                {
                    if (TryParsePosition(message.content, out var position))
                    {
                        entries.Add(
                            new RadarContactLogEntry(
                                constructEntry,
                                position,
                                message.messageDate
                            )
                        );
                    }

                    constructEntry = null;
                    continue;
                }
            }
        }
        catch (Exception e)
        {
            return new LogAnalysisResult(
                false,
                e.ToString(),
                new List<ILuaLogEntry>()
            );
        }

        return new LogAnalysisResult(
            true,
            "",
            entries
        );
    }

    public static DateTime UnixTimeStampToDateTime(long duUnixTimeStamp)
    {
        var unixTimestamp = duUnixTimeStamp / 1000;
        
        // Unix timestamp is seconds past epoch
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimestamp).ToLocalTime();
        
        return dateTime;
    }

    private bool TryParsePosition(string value, out string position)
    {
        var pieces = value.Split(" at ");

        if (pieces.Length != 2)
        {
            position = string.Empty;
            return false;
        }

        position = pieces[0].Trim();
        return position.StartsWith("::pos{");
    }

    private bool TryParseConstruct(string value, out ConstructEntry? constructEntry)
    {
        var tags = new List<string>();

        if (value.Contains("ABANDONED"))
        {
            tags.Add("abandoned");
        }

        var pieces = value.Split("CONTACT:");

        if (pieces.Length != 2)
        {
            constructEntry = null;
            return false;
        }

        var piecesQueue = new Queue<string>(pieces);
        piecesQueue.Dequeue();

        var contactDetails = piecesQueue.Dequeue();

        var nameBuffer = new StringBuilder();
        var sizeBuffer = new StringBuilder();
        var size = "XS";

        foreach (var @char in contactDetails)
        {
            switch (@char)
            {
                case '[':
                    sizeBuffer.Clear();
                    sizeBuffer.Append(@char);
                    break;
                case ']':
                    sizeBuffer.Append(@char);
                    size = sizeBuffer.ToString();
                    sizeBuffer.Clear();
                    break;
                default:
                    nameBuffer.Append(@char);
                    if (sizeBuffer.Length > 0)
                    {
                        sizeBuffer.Append(@char);
                    }

                    break;
            }
        }

        constructEntry = new ConstructEntry(
            size.Replace("[", "").Replace("]", ""),
            nameBuffer.ToString().Trim().Substring(0, nameBuffer.Length - 3),
            tags.ToArray()
        );
        return true;
    }
}