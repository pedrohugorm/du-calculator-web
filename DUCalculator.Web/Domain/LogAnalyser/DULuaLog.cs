namespace DUCalculator.Web.Domain.LogAnalyser;

public record DULuaLog(
    string channelName,
    List<DULuaLog.DULuaMessage> messageList
)
{
    public record DULuaMessage(
        long messageDate,
        string senderName,
        string content
    );
}