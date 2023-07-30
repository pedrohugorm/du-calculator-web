using DUCalculator.Web.Domain.Common;
using DUCalculator.Web.Domain.WeaponDamage;

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
}