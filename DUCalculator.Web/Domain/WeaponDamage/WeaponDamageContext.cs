using System.Text;
using DUCalculator.Web.Domain.Common;
using DUCalculator.Web.Domain.WeaponDamage.Commands;
using DUCalculator.Web.Domain.WeaponDamage.Damage;

namespace DUCalculator.Web.Domain.WeaponDamage;

public class WeaponDamageContext
{
    public IConsoleWriter Console;

    public WeaponDamageContext(IConsoleWriter console)
    {
        Console = console;
    }

    public Dictionary<string, IWeaponDamageCommand> AvailableCommands { get; set; } = new();
    public string RawCommand { get; set; } = "";
    public double BaseDamage { get; set; }
    public double RateOfFire { get; set; } = 1;
    public double ReloadTime { get; set; } = 1;
    public double WeaponCount { get; set; } = 1;
    public double Multiplier { get; set; } = 1;
    public int MagazineSize { get; set; } = 0;
    public double TargetDamage { get; set; } = 200000000;
    public double DeltaTime { get; set; } = 0.1;
    public string WeaponId { get; set; } = "null";
    public DamageReceiver DamageReceiver { get; set; } = new();
    public List<IDamageType> DamageTypes { get; set; } = new();

    public ShipState ShipState { get; set; } = new();

    public string[] GetCommandPieces() => RawCommand.Split(' ', ';', '\t');
    
    public void LoadMissileLarge()
    {
        WeaponId = "Missile L";
        ReloadTime = 33.8;
        RateOfFire = 2.81;
        BaseDamage = 149691;
        WeaponCount = 4;
        MagazineSize = 15;
        DamageTypes = new List<IDamageType>
        {
            new AntimatterDamageType(),
            new KineticDamageType(),
        };
    }
    
    public void LoadMissileMedium()
    {
        WeaponId = "Missile M";
        ReloadTime = 33.8;
        RateOfFire = 2.81;
        BaseDamage = 105047;
        WeaponCount = 4;
        MagazineSize = 15;
        DamageTypes = new List<IDamageType>
        {
            new AntimatterDamageType(),
            new KineticDamageType(),
        };
    }
    
    public void LoadMissileSmall()
    {
        WeaponId = "Missile S";
        ReloadTime = 33.8;
        RateOfFire = 2.81;
        BaseDamage = 73717;
        WeaponCount = 4;
        MagazineSize = 15;
        DamageTypes = new List<IDamageType>
        {
            new AntimatterDamageType(),
            new KineticDamageType(),
        };
    }
    
    public void LoadMissileXSmall()
    {
        WeaponId = "Missile XS";
        ReloadTime = 33.8;
        RateOfFire = 2.81;
        BaseDamage = 51731;
        WeaponCount = 4;
        MagazineSize = 15;
        DamageTypes = new List<IDamageType>
        {
            new AntimatterDamageType(),
            new KineticDamageType(),
        };
    }

    public void LoadCannonLarge()
    {
        WeaponId = "Cannon L";
        ReloadTime = 4.2;
        RateOfFire = 4.75;
        BaseDamage = 103463;
        WeaponCount = 6;
        MagazineSize = 37;
        DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new KineticDamageType(),
        };
    }
    
    public void LoadCannonMedium()
    {
        WeaponId = "Cannon M";
        ReloadTime = 4.2;
        RateOfFire = 3.16;
        BaseDamage = 51731;
        WeaponCount = 6;
        MagazineSize = 37;
        DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new KineticDamageType(),
        };
    }
    
    public void LoadCannonSmall()
    {
        WeaponId = "Cannon S";
        ReloadTime = 4.2;
        RateOfFire = 2.11;
        BaseDamage = 25866;
        WeaponCount = 6;
        MagazineSize = 37;
        DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new KineticDamageType(),
        };
    }
    
    public void LoadCannonXSmall()
    {
        WeaponId = "Cannon XS";
        ReloadTime = 4.2;
        RateOfFire = 1.41;
        BaseDamage = 12933;
        WeaponCount = 6;
        MagazineSize = 37;
        DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new KineticDamageType(),
        };
    }

    public void LoadRailGunLarge()
    {
        WeaponId = "Railgun L";
        ReloadTime = 14.1;
        RateOfFire = 13.29;
        BaseDamage = 270595;
        WeaponCount = 4;
        MagazineSize = 22;
        DamageTypes = new List<IDamageType>
        {
            new AntimatterDamageType(),
            new ElectromagneticDamageType(),
        };
    }
    
    public void LoadRailGunMedium()
    {
        WeaponId = "Railgun M";
        ReloadTime = 14.1;
        RateOfFire = 8.86;
        BaseDamage = 135297;
        WeaponCount = 4;
        MagazineSize = 22;
        DamageTypes = new List<IDamageType>
        {
            new AntimatterDamageType(),
            new ElectromagneticDamageType(),
        };
    }
    
    public void LoadRailGunSmall()
    {
        WeaponId = "Railgun S";
        ReloadTime = 14.1;
        RateOfFire = 5.91;
        BaseDamage = 67649;
        WeaponCount = 4;
        MagazineSize = 22;
        DamageTypes = new List<IDamageType>
        {
            new AntimatterDamageType(),
            new ElectromagneticDamageType(),
        };
    }
    
    public void LoadRailGunXSmall()
    {
        WeaponId = "Railgun XS";
        ReloadTime = 14.1;
        RateOfFire = 3.94;
        BaseDamage = 33824;
        WeaponCount = 4;
        MagazineSize = 22;
        DamageTypes = new List<IDamageType>
        {
            new AntimatterDamageType(),
            new ElectromagneticDamageType(),
        };
    }
    
    public void LoadLaserLarge()
    {
        WeaponId = "Laser L";
        ReloadTime = 8.4;
        RateOfFire = 6.64;
        BaseDamage = 135297;
        WeaponCount = 5;
        MagazineSize = 52;
        DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new ElectromagneticDamageType(),
        };
    }
    
    public void LoadLaserMedium()
    {
        WeaponId = "Laser M";
        ReloadTime = 8.4;
        RateOfFire = 4.43;
        BaseDamage = 67649;
        WeaponCount = 5;
        MagazineSize = 52;
        DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new ElectromagneticDamageType(),
        };
    }
    
    public void LoadLaserSmall()
    {
        WeaponId = "Laser S";
        ReloadTime = 8.4;
        RateOfFire = 2.95;
        BaseDamage = 33824;
        WeaponCount = 5;
        MagazineSize = 52;
        DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new ElectromagneticDamageType(),
        };
    }
    
    public void LoadLaserXSmall()
    {
        WeaponId = "Laser XS";
        ReloadTime = 8.4;
        RateOfFire = 1.97;
        BaseDamage = 16912;
        WeaponCount = 5;
        MagazineSize = 52;
        DamageTypes = new List<IDamageType>
        {
            new ThermicDamageType(),
            new ElectromagneticDamageType(),
        };
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("Configuration:");
        sb.AppendLine("-----------------------------------------------------------------------------");
        sb.AppendLine($"{nameof(WeaponId)} = {WeaponId}");
        sb.AppendLine($"{nameof(BaseDamage)} = {BaseDamage:N2}");
        sb.AppendLine($"{nameof(RateOfFire)} = {RateOfFire:N2}");
        sb.AppendLine($"{nameof(WeaponCount)} = {WeaponCount}");
        sb.AppendLine($"{nameof(MagazineSize)} = {MagazineSize}");
        sb.AppendLine($"{nameof(ReloadTime)} = {ReloadTime:N2}");
        sb.AppendLine("-----------------------------------------------------------------------------");
        sb.AppendLine("");
        sb.AppendLine("-----------------------------------------------------------------------------");
        sb.AppendLine($"{nameof(TargetDamage)} = {TargetDamage:N2}");
        sb.AppendLine($"{nameof(DeltaTime)} = {DeltaTime:N2}");
        sb.AppendLine($"{nameof(Multiplier)} = {Multiplier:N2}");
        sb.AppendLine($"{nameof(DamageTypes)}:");
        foreach (var damageType in DamageTypes)
        {
            sb.AppendLine($"{damageType}");
        }
        sb.AppendLine($"{nameof(Damage.DamageReceiver)}:");
        sb.AppendLine($"{DamageReceiver}");
        sb.AppendLine("-----------------------------------------------------------------------------");
        
        return sb.ToString();
    }
}