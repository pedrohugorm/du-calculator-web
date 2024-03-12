using DUCalculator.Web.Domain.Common;

namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class SetContextNumberCommand : SetIntValueCommand
{
    public override string[] Commands => new[] { "ctx" };
    public override string Description => "Set the context number to another number";
    public override string Example => "ctx 1";

    public override void SetValue(WeaponDamageContext context, int value)
    {
        if (!AppState.WeaponDamageContextDictionary.ContainsKey(value))
        {
            AppState.WeaponDamageContextDictionary.Add(value, new WeaponDamageContext(new WebConsoleOutputWriter()));
        }
        
        AppState.ContextNumber = value;
    }
}