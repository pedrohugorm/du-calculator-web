using System.Numerics;
using System.Text;
using DUCalculator.Web.Domain.HexGrid;
using DUCalculator.Web.Domain.LiveTrace;
using Newtonsoft.Json.Linq;

namespace DUCalculator.Web.Tests;

[TestFixture]
public class ScriptGeneratorTests
{
    private Dictionary<string, HexGridSettings> _positionsByGridName;

    public record Entry(
        string Name,
        string StartPos,
        string EndPos,
        int MinSu,
        int MaxSu,
        int RingCount
    );

    public record TraceResult(
        Vector3 Start,
        Vector3 End
    );

    public record HexGridSettings(
        TraceResult TraceResult,
        int RingCount
    );

    private TraceResult CalculateTrace(Entry entry)
    {
        const int SuInKm = 200000;
        
        var last = entry.EndPos.PositionToVector3();
        var start = entry.StartPos.PositionToVector3();

        var direction = Vector3.Normalize(last - start);

        var minSuPos = last + direction * entry.MinSu * SuInKm;
        var maxSuPos = last + direction * entry.MaxSu * SuInKm;

        return new TraceResult(
            minSuPos,
            maxSuPos
        );
    }

    [SetUp]
    public void Setup()
    {
        var entries = new List<Entry>
        {
            new(
                "Alioth-Gamma",
                "::pos{0,0,-8.0000,-8.0000,-126303.0000}",
                "::pos{0,0,-64334000.0000,55522000.0000,-14400000.0000}",
                20,
                70,
                5
            ),
            new(
                "Talemai-Gamma",
                "::pos{0,0,-13234464.0000,55765536.0000,465536.0000}",
                "::pos{0,0,-64334000.0000,55522000.0000,-14400000.0000}",
                20,
                120,
                5
            ),
            new(
                "Jago-Gamma",
                "::pos{0,0,-94134464.0000,12765536.0000,-3634464.0000}",
                "::pos{0,0,-64334000.0000,55522000.0000,-14400000.0000}",
                20,
                120,
                5
            ),
            new(
                "Alioth-Alpha",
                "::pos{0,0,-8.0000,-8.0000,-126303.0000}",
                "::pos{0,0,33946000.0000,71381990.0000,28850000.0000}",
                20,
                100,
                5
            ),
            new(
                "Talemai-Alpha",
                "::pos{0,0,-13234464.0000,55765536.0000,465536.0000}",
                "::pos{0,0,33946000.0000,71381990.0000,28850000.0000}",
                20,
                120,
                5
            ),
            new(
                "Teoma-Alpha",
                "::pos{0,0,80865536.0000,54665536.0000,-934464.0000}",
                "::pos{0,0,33946000.0000,71381990.0000,28850000.0000}",
                20,
                120,
                5
            ),
            new(
                "Aegis-Alpha",
                "::pos{0,0,13856701.7693,7386301.6554,-258251.0307}",
                "::pos{0,0,33946000.0000,71381990.0000,28850000.0000}",
                20,
                120,
                5
            ),
            new(
                "Alioth-Kappa",
                "::pos{0,0,-8.0000,-8.0000,-126303.0000}",
                "::pos{0,0,52778000.0000,23162000.0000,52040000.0000}",
                20,
                120,
                5
            ),
            new(
                "SinnenM1-Kappa",
                "::pos{0,0,52416510.6689,26795577.1597,51893332.6527}",
                "::pos{0,0,52778000.0000,23162000.0000,52040000.0000}",
                20,
                70,
                6
            ),
            new(
                "Talemai-Kappa",
                "::pos{0,0,-13234464.0000,55765536.0000,465536.0000}",
                "::pos{0,0,52778000.0000,23162000.0000,52040000.0000}",
                20,
                50,
                5
            ),
            new(
                "Aegis-Kappa",
                "::pos{0,0,13856701.7693,7386301.6554,-258251.0307}",
                "::pos{0,0,52778000.0000,23162000.0000,52040000.0000}",
                20,
                120,
                5
            ),
            new(
                "Jago-Beta",
                "::pos{0,0,-94134464.0000,12765536.0000,-3634464.0000}",
                "::pos{0,0,-117534000.0000,8122000.0000,-4000000.0000}",
                20,
                120,
                5
            ),
            new(
                "Teoma-Zeta",
                "::pos{0,0,80865536.0000,54665536.0000,-934464.0000}",
                "::pos{0,0,102456000.0000,64272000.0000,-3200000.0000}",
                20,
                120,
                5
            ),
            new(
                "Thades-Zeta",
                "::pos{0,0,29165536.0000,10865536.0000,65536.0000}",
                "::pos{0,0,102456000.0000,64272000.0000,-3200000.0000}",
                20,
                120,
                5
            ),
        };

        _positionsByGridName = entries
            .ToDictionary(k => k.Name, v => new { Trace = CalculateTrace(v), v.RingCount })
            .ToDictionary(
                k => k.Key,
                v => new HexGridSettings(
                    v.Value.Trace,
                    v.Value.RingCount
                )
            );
    }

    [Test]
    public void Should_Generate_Script()
    {
        var emptySlot = delegate(string name)
        {
            return new
            {
                name,
                type = new
                {
                    events = Array.Empty<object>(),
                    methods = Array.Empty<object>()
                }
            };
        };

        var codeHandler = delegate(string command, string code, int key, int slotKey)
        {
            return new
            {
                code,
                filter = new
                {
                    args = new[]
                    {
                        new { value = command }
                    },
                    signature = "onInputText(text)",
                    slotKey = $"{slotKey}"
                },
                key = $"{key}"
            };
        };

        var commandNames = new List<string>();
        var commands = new List<object>();
        var generator = new OffsetBasedHexGridGenerator();

        var key = 0;
        foreach (var kvp in _positionsByGridName)
        {
            var result = generator.GenerateGrid(
                new IHexGridGenerator.Settings(
                    kvp.Value.TraceResult.Start.Vector3ToPosition(),
                    kvp.Value.TraceResult.End.Vector3ToPosition(),
                    true,
                    kvp.Value.RingCount
                )
            );

            var commandName = kvp.Key.Replace(" ", "")
                .ToLower();
            var foxCode = result.WaypointLines.ToFoxSetPositionsScript("FF10F0", "10FFF0");
            var sagaCode = result.WaypointLines.ToSagaDataBankString();

            var foxCodeSb = new StringBuilder();
            foxCodeSb.AppendLine("db.setStringValue('SagaRoutes', '')");
            foxCodeSb.AppendLine(foxCode);
            foxCodeSb.AppendLine($"system.print('FOX {kvp.Key} SET')");

            var sagaCodeSb = new StringBuilder();
            sagaCodeSb.AppendLine($"local value = '{sagaCode}'");
            sagaCodeSb.AppendLine("db.setStringValue('savemarks', '')");
            sagaCodeSb.AppendLine("db.setStringValue('SagaRoutes', value)");
            sagaCodeSb.AppendLine($"system.print('SAGA {kvp.Key} SET')");

            commands.Add(codeHandler($"set fox {commandName}", foxCodeSb.ToString(), key, -4));
            // commands.Add(codeHandler($"set saga {commandName}", sagaCodeSb.ToString(), key + 1, -4));

            commandNames.Add($"set fox {commandName}");
            // commandNames.Add($"set saga {commandName}");

            key += 2;
        }

        commands.Add(new
        {
            code =
                $"system.print('AVAILABLE COMMANDS:')\n{string.Join("\n", commandNames.Select(x => $"system.print('{x}')"))}",
            filter = new
            {
                args = Array.Empty<object>(),
                signature = "onStart()",
                slotKey = -1
            },
            key = $"{key + 1}"
        });

        dynamic scriptObj = new
        {
            slots = new Dictionary<string, dynamic>
            {
                { "0", emptySlot("db") },
                { "1", emptySlot("slot2") },
                { "2", emptySlot("slot3") },
                { "3", emptySlot("slot4") },
                { "4", emptySlot("slot5") },
                { "5", emptySlot("slot6") },
                { "6", emptySlot("slot7") },
                { "7", emptySlot("slot8") },
                { "8", emptySlot("slot9") },
                { "9", emptySlot("slot10") },
                { "-1", emptySlot("unit") },
                { "-2", emptySlot("construct") },
                { "-3", emptySlot("player") },
                { "-4", emptySlot("system") },
                { "-5", emptySlot("library") },
            },
            handlers = commands,
            methods = Array.Empty<object>(),
            events = Array.Empty<object>()
        };

        var luaScriptString = JToken.FromObject((object)scriptObj).ToString();
        
        
        Console.WriteLine(luaScriptString);

        var bytes = Encoding.Default.GetBytes(luaScriptString);
        var kb = bytes.Length / 1024;
        Console.WriteLine($"{kb} KB");
    }
}