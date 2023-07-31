﻿using System.Text;
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