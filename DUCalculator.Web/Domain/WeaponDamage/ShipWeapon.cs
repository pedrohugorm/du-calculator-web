using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage;

public class ShipWeapon
{
    public string Id { get; }
    public double BaseDamage { get; }
    public double RateOfFire { get; }
    public double ReloadTime { get; }
    public int MagazineSize { get; }
    public int CurrentMagazine { get; set; }
    public IDamageType DamageType { get; }
    public int AmmoSpent { get; set; }
    public int ReloadCount { get; set; }

    public double TickDamage { private set; get; }
    public double TotalDamage { private set; get; }
    private bool IsReloading { set; get; }
    private bool IsCycling { set; get; }
    private double AccumulatedTime { get; set; }

    private double DpsAccumulatedTime { get; set; }
    private double DpsDamage { get; set; }
    private List<double> DamagePerSecondList { get; set; } = new();

    private double NoReloadDpsAccumulatedTime { get; set; }
    private double NoReloadDpsDamage { get; set; }
    private List<double> NoReloadDamagePerSecondList { get; set; } = new();

    public ShipWeapon(
        string id,
        double baseDamage,
        double rateOfFire,
        double reloadTime,
        int magazineSize,
        int currentMagazine,
        IDamageType damageType
    )
    {
        Id = id;
        BaseDamage = baseDamage;
        RateOfFire = rateOfFire;
        ReloadTime = reloadTime;
        MagazineSize = magazineSize;
        CurrentMagazine = currentMagazine;
        DamageType = damageType;
    }

    public void Tick(WeaponDamageContext context)
    {
        TickDamage = 0;

        AccumulatedTime += context.DeltaTime;
        DpsAccumulatedTime += context.DeltaTime;

        if (DpsAccumulatedTime >= 1)
        {
            DpsAccumulatedTime -= 1;
            DamagePerSecondList.Add(DpsDamage);
            DpsDamage = 0;
        }

        if (NoReloadDpsAccumulatedTime >= 1)
        {
            NoReloadDpsAccumulatedTime -= 1;
            NoReloadDamagePerSecondList.Add(NoReloadDpsDamage);
            NoReloadDpsDamage = 0;
        }

        if (IsReloading)
        {
            if (AccumulatedTime >= ReloadTime)
            {
                AccumulatedTime = 0;
                CurrentMagazine = MagazineSize;
                IsReloading = false;
            }
            else
            {
                return;
            }
        }

        // only accumulates time if it's not reloading
        NoReloadDpsAccumulatedTime += context.DeltaTime;

        if (CurrentMagazine <= 0)
        {
            IsReloading = true;
            ReloadCount++;
            AccumulatedTime = 0;

            return;
        }

        if (IsCycling)
        {
            if (AccumulatedTime >= RateOfFire)
            {
                IsCycling = false;
                AccumulatedTime = 0;
            }
            else
            {
                return;
            }
        }

        CurrentMagazine--;
        AmmoSpent++;
        TickDamage = GetDamage(context);
        TotalDamage += TickDamage;
        DpsDamage += TickDamage;
        NoReloadDpsDamage += TickDamage;
        IsCycling = true;
        AccumulatedTime = 0;
    }

    public double GetAverageDpsWithReload()
    {
        if (!DamagePerSecondList.Any())
        {
            return 0;
        }

        return DamagePerSecondList.Average();
    }

    public double GetAverageDpsWithoutReload()
    {
        if (!NoReloadDamagePerSecondList.Any())
        {
            return 0;
        }

        return NoReloadDamagePerSecondList.Average();
    }

    public string GetGroupId() => $"{Id} ({DamageType})";

    private double GetDamage(WeaponDamageContext context)
    {
        var rawDamage = BaseDamage * context.Multiplier;
        var damageDealt = context.DamageReceiver.ResistDamage(DamageType, rawDamage);

        return damageDealt;
    }

    public void Reset()
    {
        CurrentMagazine = MagazineSize;
        TotalDamage = 0;
        TickDamage = 0;
        DpsDamage = 0;
        NoReloadDpsDamage = 0;
        AccumulatedTime = 0;
        DpsAccumulatedTime = 0;
        NoReloadDpsAccumulatedTime = 0;
        IsCycling = false;
        IsReloading = false;
        AmmoSpent = 0;
        ReloadCount = 0;
    }
}