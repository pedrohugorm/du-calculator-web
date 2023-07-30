namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public interface IWeaponDamageCommand
{
    string[] Commands { get; }
    string Description { get; }
    string Example { get; }
    
    void Execute(WeaponDamageContext context);
}