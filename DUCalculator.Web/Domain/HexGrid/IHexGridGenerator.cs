namespace DUCalculator.Web.Domain.HexGrid;

public interface IHexGridGenerator
{
    HexGridGenerator.Result GenerateGrid(HexGridGenerator.Settings settings);
}