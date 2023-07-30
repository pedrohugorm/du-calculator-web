namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class LoadPrefabCommand : IWeaponDamageCommand
{
    public string[] Commands => new[] { "load", "loadprefab" };
    public string Description => "Loads a prefab configuration";
    public string Example => "load cm; load cannon_m; load rl; load laser_l";
    
    public void Execute(WeaponDamageContext context)
    {
        var prefabName = context.RawCommand.Replace("load ", "")
            .ToLower()
            .Trim();

        switch (prefabName)
        {
            case "cl":
            case "cannon_l":
                context.LoadCannonLarge();
                break;
            case "cm":
            case "cannon_m":
                context.LoadCannonMedium();
                break;
            case "cs":
            case "cannon_s":
                context.LoadCannonSmall();
                break;
            case "cxs":
            case "cannon_xs":
                context.LoadCannonXSmall();
                break;
            case "rl":
            case "rail_l":
                context.LoadRailGunLarge();
                break;
            case "rm":
            case "rail_m":
                context.LoadRailGunMedium();
                break;
            case "rs":
            case "rail_s":
                context.LoadRailGunSmall();
                break;
            case "rxs":
            case "rail_xs":
                context.LoadRailGunXSmall();
                break;
            case "ml":
            case "missile_l":
                context.LoadMissileLarge();
                break;
            case "mm":
            case "missile_m":
                context.LoadMissileMedium();
                break;
            case "ms":
            case "missile_s":
                context.LoadMissileSmall();
                break;
            case "mxs":
            case "missile_xs":
                context.LoadMissileXSmall();
                break;
            case "ll":
            case "laser_l":
                context.LoadLaserLarge();
                break;
            case "lm":
            case "laser_m":
                context.LoadLaserMedium();
                break;
            case "ls":
            case "laser_s":
                context.LoadLaserSmall();
                break;
            case "lxs":
            case "laser_xs":
                context.LoadLaserXSmall();
                break;
            default:
                context.Console.WriteLine($"{prefabName} NOT FOUND");
                return;
        }
        
        context.Console.WriteLine($"{context.WeaponCount} {context.WeaponId} Loaded");
    }
}