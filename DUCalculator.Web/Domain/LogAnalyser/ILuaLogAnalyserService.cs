namespace DUCalculator.Web.Domain.LogAnalyser;

public interface ILuaLogAnalyserService
{
    LogAnalysisResult Analyse(string log);
}