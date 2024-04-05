using System.Numerics;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using DUCalculator.Web.Domain.LiveTrace;

namespace DUCalculator.Web.Domain.HexGrid;

public class SearchGridScriptGenerator : ISearchGridScriptGenerator
{
    public ScriptResult GenerateScript(ScriptGenerationOptions options)
    {
        var positionsByGridName = options.Entries
            .ToDictionary(k => k.Name, v => new { Trace = CalculateTrace(v), v.RingCount })
            .ToDictionary(
                k => k.Key,
                v => new HexGridSettings(
                    v.Value.Trace,
                    v.Value.RingCount
                )
            );
        
        var commandNames = new List<string>();
        var commandsFox = new List<object>();
        var commandsSaga = new List<object>();
        var generator = new OffsetBasedHexGridGenerator();

        var key = 0;
        foreach (var kvp in positionsByGridName)
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
            foxCodeSb.AppendLine("db.setStringValue('savemarks', '')");
            foxCodeSb.AppendLine(foxCode);
            foxCodeSb.AppendLine($"system.print('FOX {kvp.Key} SET')");

            var sagaCodeSb = new StringBuilder();
            sagaCodeSb.AppendLine($"local value = '{sagaCode}'");
            sagaCodeSb.AppendLine("db.setStringValue('SagaRoutes', value)");
            sagaCodeSb.AppendLine($"system.print('SAGA {kvp.Key} SET')");

            commandsFox.Add(CodeHandler($"set {commandName}", foxCodeSb.ToString(), key, -4));
            commandsSaga.Add(CodeHandler($"set {commandName}", sagaCodeSb.ToString(), key + 1, -4));

            commandNames.Add($"set {commandName}");

            key += 2;
        }

        commandsFox.Add(new
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

        dynamic foxScript = ScriptObject(commandsFox);
        dynamic sagaScript = ScriptObject(commandsSaga);

        var serializerOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        var luaFoxScript = JsonSerializer.Serialize((object)foxScript, serializerOptions);
        var luaSagaScript = JsonSerializer.Serialize((object)sagaScript, serializerOptions);

        return new ScriptResult(
            true,
            luaSagaScript,
            luaFoxScript
        );
    }

    private static object ScriptObject(IEnumerable<object> commands)
    {
        return new
        {
            slots = new Dictionary<string, dynamic>
            {
                { "0", EmptySlot("db") },
                { "1", EmptySlot("slot2") },
                { "2", EmptySlot("slot3") },
                { "3", EmptySlot("slot4") },
                { "4", EmptySlot("slot5") },
                { "5", EmptySlot("slot6") },
                { "6", EmptySlot("slot7") },
                { "7", EmptySlot("slot8") },
                { "8", EmptySlot("slot9") },
                { "9", EmptySlot("slot10") },
                { "-1", EmptySlot("unit") },
                { "-2", EmptySlot("construct") },
                { "-3", EmptySlot("player") },
                { "-4", EmptySlot("system") },
                { "-5", EmptySlot("library") },
            },
            handlers = commands,
            methods = Array.Empty<object>(),
            events = Array.Empty<object>()
        };
    }
    
    private static object EmptySlot(string name)
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
    }

    private static object CodeHandler(string command, string code, int key, int slotKey)
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
    }
    
    private static TraceResult CalculateTrace(Entry entry)
    {
        const int suInKm = 200000;
        
        var last = entry.EndPos.PositionToVector3();
        var start = entry.StartPos.PositionToVector3();

        var direction = Vector3.Normalize(last - start);

        var minSuPos = last + direction * entry.MinSu * suInKm;
        var maxSuPos = last + direction * entry.MaxSu * suInKm;

        return new TraceResult(
            minSuPos,
            maxSuPos
        );
    }
    
    public record Entry(
        string Name,
        string StartPos,
        string EndPos,
        int MinSu,
        int MaxSu,
        int RingCount
    )
    {
        public string StartPos { get; set; } = StartPos;
        public string EndPos { get; set; } = EndPos;
        public int MinSu { get; set; } = MinSu;
        public int MaxSu { get; set; } = MaxSu;
        public string Name { get; set; } = Name;
        public int RingCount { get; set; } = RingCount;

        public bool IsValid => IsNameValid && IsStartPosValid && IsEndPosValid && IsRingCountValid;

        public bool IsNameValid => !string.IsNullOrEmpty(Name);
        public bool IsStartPosValid => StartPos.StartsWith("::pos{0,0,") && StartPos.EndsWith("}");
        public bool IsEndPosValid => EndPos.StartsWith("::pos{0,0,") && EndPos.EndsWith("}");
        public bool IsRingCountValid => RingCount > 0;
    }

    private record TraceResult(
        Vector3 Start,
        Vector3 End
    );

    private record HexGridSettings(
        TraceResult TraceResult,
        int RingCount
    );

    public record ScriptGenerationOptions(
        IEnumerable<Entry> Entries
    );

    public record ScriptResult(
        bool Success,
        string SagaScript,
        string FoxScript
    )
    {
        public int GetSagaScriptSizeBytes()
        {
            var bytes = Encoding.Default.GetBytes(SagaScript);
            return bytes.Length;
        }
        
        public int GetFoxScriptSizeBytes()
        {
            var bytes = Encoding.Default.GetBytes(FoxScript);
            return bytes.Length;
        }
    }
}