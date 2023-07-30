namespace DUCalculator.Web.Domain.WeaponDamage;

public class ShipWeaponReport
{
    public int Count { get; set; }
    public double ReloadCount { get; set; }
    public double AmmoSpent { get; set; }
    public double TotalDamage { get; set; }
    public double AvgPerWeaponDpsWithReload { get; set; }
    public double AvgPerWeaponDpsNoReload { get; set; }
    public double AllWeaponsDpsWithReload { get; }
    public double AllWeaponsDpsNoReload { get; }

    public ShipWeaponReport(
        int count, 
        double reloadCount, 
        double totalDamage, 
        double avgPerWeaponDpsWithReload, 
        double avgPerWeaponDpsNoReload, 
        double allWeaponsDpsWithReload, 
        double allWeaponsDpsNoReload, 
        int ammoSpent
    )
    {
        Count = count;
        ReloadCount = reloadCount;
        TotalDamage = totalDamage;
        AvgPerWeaponDpsWithReload = avgPerWeaponDpsWithReload;
        AvgPerWeaponDpsNoReload = avgPerWeaponDpsNoReload;
        AllWeaponsDpsWithReload = allWeaponsDpsWithReload;
        AllWeaponsDpsNoReload = allWeaponsDpsNoReload;
        AmmoSpent = ammoSpent;
    }
}