namespace DUCalculator.Web.Domain.WeaponDamage.Prefabs;

public interface IContextPrefab
{
    string Name { get; }
    
    void Load(WeaponDamageContext context);
}