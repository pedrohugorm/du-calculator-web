namespace DUCalculator.Web.Domain.LogAnalyser;

public interface IChannelOutputAnalyserService
{
    LogAnalysisResult Analyse(string log);
}