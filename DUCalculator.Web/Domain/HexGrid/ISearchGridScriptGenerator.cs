namespace DUCalculator.Web.Domain.HexGrid;

public interface ISearchGridScriptGenerator
{
    SearchGridScriptGenerator.ScriptResult GenerateScript(SearchGridScriptGenerator.ScriptGenerationOptions options);
}