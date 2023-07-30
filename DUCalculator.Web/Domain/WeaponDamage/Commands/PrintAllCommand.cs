namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class PrintAllCommand : IWeaponDamageCommand
{
    public string[] Commands => new[] { "printall", "print", "getconfig" };
    public string Description => "Prints the current configuration - except ship state";
    public string Example => "print";

    public void Execute(WeaponDamageContext context)
    {
        context.Console.WriteLine(context.ToString());
    }
}