using System.Reflection;
using DUCalculator.Web.Domain.Common;
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
    
    public static WeaponDamageSubProgram Program = new(new WebConsoleOutputWriter());
    public static WeaponDamageContext Context => Program.Context;

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