using System.Reflection;
using DUCalculator.Web.Domain.Common;
using DUCalculator.Web.Domain.LiveTrace;
using DUCalculator.Web.Domain.WeaponDamage;
using DUCalculator.Web.Domain.WeaponDamage.Prefabs;

namespace DUCalculator.Web.Domain;

public static class AppState
{
    public static event Action OnUpdate;

    public static void NotifyUpdated()
    {
        OnUpdate?.Invoke();
    }

    public static int ContextNumber = 0;
    
    public static WeaponDamageSubProgram WeaponDamage = new();
    public static LiveTraceSubProgram LiveTrace = new(new WebConsoleOutputWriter());

    public static WeaponDamageContext WeaponDamageContext => WeaponDamageContextDictionary[ContextNumber];
    public static Dictionary<int, WeaponDamageContext> WeaponDamageContextDictionary = new()
    {
        {0, new WeaponDamageContext(new WebConsoleOutputWriter())}
    };
    
    public static LiveTraceExecutionContext LiveTraceContext => LiveTrace.Context;

    public static Dictionary<string, IContextPrefab> Prefabs
        => Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.IsAssignableTo(typeof(IContextPrefab)))
            .Where(r => r != typeof(IContextPrefab))
            .Where(r => !r.IsAbstract)
            .Select(x => x.GetConstructor(Type.EmptyTypes)?.Invoke(Array.Empty<object>()) as IContextPrefab)
            .Where(x => x != null)
            .ToDictionary(k => k!.Name, v => v!);
}