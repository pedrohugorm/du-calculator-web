using System.Text;
using DUCalculator.Web.Domain.HexGrid;
using Newtonsoft.Json.Linq;

namespace DUCalculator.Web.Tests;

[TestFixture]
public class ScriptGeneratorTests
{
    private Dictionary<string, Tuple<string, string>> _positionsByGridName;

    [SetUp]
    public void Setup()
    {
        _positionsByGridName = new Dictionary<string, Tuple<string, string>>
        {
            {
                "Gamma1",
                new("::pos{0,0,-68174736.6077067,55503695.3791812,-15517321.4627307}",
                    "::pos{0,0,-87378419.6462399,55412172.2750875,-21103928.7763843}")
            },
            {
                "Jago-Gamma",
                new("::pos{0,0,-62094091.4522484,58735727.4501911,-15209175.8607358}",
                    "::pos{0,0,-50894548.7134906,74804364.7011467,-19255055.164415}")
            },
            {
                "Alioth-Gamma",
                new("::pos{0,0,-67323447.5374155,58093513.182356,-15071210.3798439}",
                    "::pos{0,0,-74797066.3809542,64522296.1382458,-16749236.3294535}")
            },
            {
                "Alioth-Alpha",
                new("::pos{0,0,35565317.7809754,74772167.2829724,30222773.8030116}",
                    "::pos{0,0,40504237.0129503,85112207.9960382,34409733.9021968}")
            },
            {
                "Zeta-Teoma",
                new("::pos{0,0,106093893.523135,65890644.8408812,-3581732.35836106}",
                    "::pos{0,0,117189468.768697,70827511.6055688,-4746016.05136229}")
            },
            {
                "Zeta-Teoma-Ex",
                new("::pos{0,0,117189468.768697,70827511.6055688,-4746016.05136229}",
                    "::pos{0,0,124283361.138811,73983869.0452872,-5490394.15016636}")
            },
            {
                "Kappa-Alioth",
                new("::pos{0,0,58209317.4747415,25545572.6956672,57408367.6935394}",
                    "::pos{0,0,60924976.2121123,26737359.0435008,60092551.540309}")
            }
        };
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
        
        var sb = new StringBuilder();
        sb.AppendLine("local sagaMap = {}");
        sb.AppendLine("local foxMap = {}");
        sb.AppendLine("local commandMap = {}");
        sb.AppendLine();

        var commandNames = new List<string>();
        var commands = new List<object>();
        var generator = new OffsetBasedHexGridGenerator();

        var key = 0;
        foreach (var kvp in _positionsByGridName)
        {
            var result = generator.GenerateGrid(
                new IHexGridGenerator.Settings(
                    kvp.Value.Item1,
                    kvp.Value.Item2,
                    true,
                    5
                )
            );

            var commandName = kvp.Key.Replace(" ", "")
                .ToLower();
            var foxCode = result.WaypointLines.ToFoxDataBankString("FF10F0", "10FFF0");
            var sagaCode = result.WaypointLines.ToSagaDataBankString();
            
            sb.AppendLine($"sagaMap[\"{kvp.Key}\"] = '{sagaCode}'");
            sb.AppendLine($"foxMap[\"{kvp.Key}\"] = '{foxCode}'");

            var foxCodeSb = new StringBuilder();
            foxCodeSb.AppendLine($"local value = '{foxCode}'");
            foxCodeSb.AppendLine("db.setStringValue('SagaRoutes', '')");
            foxCodeSb.AppendLine("db.setStringValue('savemarks', value)");
            foxCodeSb.AppendLine($"system.print('FOX {kvp.Key} SET')");
            
            var sagaCodeSb = new StringBuilder();
            sagaCodeSb.AppendLine($"local value = '{sagaCode}'");
            sagaCodeSb.AppendLine("db.setStringValue('savemarks', '')");
            sagaCodeSb.AppendLine("db.setStringValue('SagaRoutes', value)");
            sagaCodeSb.AppendLine($"system.print('SAGA {kvp.Key} SET')");
            
            commands.Add(codeHandler($"set fox {commandName}", foxCodeSb.ToString(), key, -4));
            commands.Add(codeHandler($"set saga {commandName}", sagaCodeSb.ToString(), key + 1, -4));

            commandNames.Add($"set fox {commandName}");
            commandNames.Add($"set saga {commandName}");

            key += 2;
        }

        commands.Add(new
        {
            code = $"system.print('AVAILABLE COMMANDS:')\n{string.Join("\n", commandNames.Select(x => $"system.print('{x}')"))}",
            filter = new
            {
                args = Array.Empty<object>(),
                signature = "onStart()",
                slotKey = -1
            },
            key = $"{key + 1}"
        });
        
        sb.AppendLine("commandMap[\"fox\"] = foxMap");
        sb.AppendLine("commandMap[\"saga\"] = sagaMap");

        var scriptString = sb.ToString();
        // Console.WriteLine(scriptString);
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


        var luaScriptString = JToken.FromObject(scriptObj).ToString();
        Console.WriteLine(luaScriptString);
    }
}