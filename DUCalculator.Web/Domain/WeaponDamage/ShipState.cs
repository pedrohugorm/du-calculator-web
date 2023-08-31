using System.Text;
using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage;

public class ShipState
{
    public List<ShipWeapon> Weapons { get; } = new();
    public Dictionary<AmmoType, int> AmmoContainer { get; private set; } = new();

    public double TotalDamage { get; set; }
    public double TickDamage { get; set; }
    private double TotalTime { get; set; }

    public void AddWeapons(WeaponDamageContext context)
    {
        if (context.WeaponId == null)
        {
            context.Console.WriteLine("No Weapon Loaded.");
            return;
        }
        
        for (var i = 0; i < context.WeaponCount; i++)
        {
            var damageTypeIndex = i % context.DamageTypes.Count;

            DamageType damageType;
            try
            {
                damageType = context.DamageTypes[damageTypeIndex];
            }
            catch (ArgumentOutOfRangeException)
            {
                context.Console.WriteLine($"Could not find DamageType at index: {damageTypeIndex}");
                throw;
            }

            Weapons.Add(
                new ShipWeapon(
                    context.WeaponId,
                    context.BaseDamage,
                    context.RateOfFire,
                    context.ReloadTime,
                    context.MagazineSize,
                    context.MagazineSize,
                    damageType
                )
            );
        }
    }

    /// <summary>
    /// Retrieves ammo int the container if there are limits or the mag size if no limits (no key)
    /// </summary>
    /// <param name="ammoType"></param>
    /// <param name="magazineSize"></param>
    /// <param name="ammoTaken"></param>
    /// <returns></returns>
    public bool TryToTakeAmmoTypeFromContainer(AmmoType ammoType, int magazineSize, out int ammoTaken)
    {
        if (AmmoContainer.TryGetValue(ammoType, out var availableAmmo))
        {
            var ammoToTake = Math.Min(magazineSize, availableAmmo);
            AmmoContainer[ammoType] -= ammoToTake;

            ammoTaken = ammoToTake;
            return ammoTaken > 0;
        }

        ammoTaken = magazineSize;
        return true;
    }

    public void Tick(WeaponDamageContext context)
    {
        TickDamage = 0;
        TotalTime += context.DeltaTime;

        foreach (var weapon in Weapons)
        {
            weapon.Tick(context);
        }

        TickDamage = Weapons.Sum(x => x.TickDamage);
        TotalDamage += TickDamage;
    }

    public void Reset()
    {
        TotalDamage = 0;
        TickDamage = 0;
        TotalTime = 0;

        foreach (var weapon in Weapons)
        {
            weapon.Reset();
        }
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder();

        var weaponByIdCount = Weapons.GroupBy(g => g.GetGroupId())
            .ToDictionary(k => k.Key, v => v.Count());

        sb.AppendLine("-----------------------------------------------------------------------------");
        sb.AppendLine("Weapons:");

        foreach (var kvp in weaponByIdCount)
        {
            sb.AppendLine($"{kvp.Key} = {kvp.Value}");
        }

        sb.AppendLine("-----------------------------------------------------------------------------");

        sb.AppendLine("Damage:");
        sb.AppendLine($"Total: {TotalDamage}");

        var weaponReportById = Weapons.GroupBy(g => g.GetGroupId())
            .ToDictionary(k => k.Key, v => new ShipWeaponReport(
                v.Count(),
                v.Average(x => x.ReloadCount),
                v.Sum(x => x.TotalDamage),
                v.Average(x => x.GetAverageDpsWithReload()),
                v.Average(x => x.GetAverageDpsWithoutReload()),
                v.Sum(x => x.GetAverageDpsWithReload()),
                v.Sum(x => x.GetAverageDpsWithoutReload()),
                v.Sum(x => x.AmmoSpent)
            ));

        sb.AppendLine("");

        foreach (var kvp in weaponReportById)
        {
            sb.AppendLine($"{kvp.Key}:");
            sb.AppendLine("");
            sb.AppendLine($"Avg DPS (per Weapon) = {kvp.Value.AvgPerWeaponDpsNoReload}");
            sb.AppendLine($"Avg DPS (per Weapon) (w/ Reload) = {kvp.Value.AvgPerWeaponDpsWithReload}");
            sb.AppendLine($"Avg Reload Count Per Weapon = {kvp.Value.ReloadCount}");
            sb.AppendLine("");
            sb.AppendLine($"All Weapons DPS = {kvp.Value.AllWeaponsDpsNoReload}");
            sb.AppendLine($"All Weapons DPS (w/ Reload) = {kvp.Value.AllWeaponsDpsWithReload}");
            sb.AppendLine($"Total Damage = {kvp.Value.TotalDamage}");
            sb.AppendLine($"Total Ammo Spent = {kvp.Value.AmmoSpent}");
            sb.AppendLine("");
        }

        sb.AppendLine("-----------------------------------------------------------------------------");

        var ts = TimeSpan.FromSeconds(TotalTime);
        sb.AppendLine($"Total Time = {ts}");

        return sb.ToString();
    }
}