using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DUCalculator.Web.Domain.LogAnalyser;

public partial class ChannelOutputAnalyserService : IChannelOutputAnalyserService
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

            if (luaLog.channelName == "Lua")
            {
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

            if (luaLog.channelName == "Combat")
            {
                foreach (var message in luaLog.messageList)
                {
                    var isMiss = message.content.Contains("missed");
                    var isHit = message.content.Contains("hit");

                    if (isMiss)
                    {
                        var piecesShipTarget = message.content.Split("missed")
                            .Select(x => x.Trim())
                            .ToArray();
                        var piecesShipWeapon = piecesShipTarget[0].Split("(")
                            .Select(x => x.Trim())
                            .ToArray();
                        var shipName = SanitizeName(piecesShipWeapon[0]);
                        var weaponName = SanitizeName(piecesShipWeapon[1].Replace(")", ""));
                        var targetName = SanitizeName(piecesShipTarget[1].Trim());

                        entries.Add(
                            new MissEntry(
                                shipName,
                                weaponName,
                                targetName,
                                message.messageDate
                            )
                        );
                    }
                    else if (isHit)
                    {
                        var piecesShipDamage = message.content.Split("hit")
                            .Select(x => x.Trim())
                            .ToArray();
                        var piecesTargetDamage = piecesShipDamage[1].Split("(")
                            .Select(x => x.Trim())
                            .ToArray();

                        var targetName = SanitizeName(piecesTargetDamage[0].Trim());
                        var damage = piecesTargetDamage[1].Replace("damage)", "")
                            .Trim();

                        if (!double.TryParse(damage, out var damageValue))
                        {
                            continue;
                        }

                        var piecesWeaponShip = piecesShipDamage[0].Split("aboard")
                            .Select(x => x.Trim())
                            .ToArray();

                        var weaponName = SanitizeName(piecesWeaponShip[0]);
                        var shipName = SanitizeName(piecesWeaponShip[1]);

                        entries.Add(new HitEntry(
                            message.messageDate,
                            shipName,
                            weaponName,
                            damageValue,
                            targetName
                        ));
                    }
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

    public static string SanitizeName(string name)
    {
        return NonWordRegex().Replace(
            name.Replace("&quot;", ""),
            ""
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

    [GeneratedRegex("[^\\w ]")]
    private static partial Regex NonWordRegex();
}