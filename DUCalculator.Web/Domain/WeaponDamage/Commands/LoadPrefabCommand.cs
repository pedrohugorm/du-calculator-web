using DUCalculator.Web.Domain.WeaponDamage.Prefabs;

namespace DUCalculator.Web.Domain.WeaponDamage.Commands;

public class LoadPrefabCommand : IWeaponDamageCommand
{
    public string[] Commands => new[] { "load", "loadprefab" };
    public string Description => "Loads a prefab configuration";
    public string Example => "load cm; load cannon_m; load rl; load laser_l";
    
    public void Execute(WeaponDamageContext context)
    {
        var prefabName = context.RawCommand
            .Replace("load ", "")
            .Replace("loadprefab ", "")
            .ToLower()
            .Trim();

        switch (prefabName)
        {
            case "cl":
            case "cannon_l":
                new CannonLargePrefab().Load(context);
                break;
            case "cm":
            case "cannon_m":
                new CannonMediumPrefab().Load(context);
                break;
            case "cs":
            case "cannon_s":
                new CannonSmallPrefab().Load(context);
                break;
            case "cxs":
            case "cannon_xs":
                new CannonXSmallPrefab().Load(context);
                break;
            case "rl":
            case "rail_l":
                new RailgunLargePrefab().Load(context);
                break;
            case "rm":
            case "rail_m":
                new RailgunMediumPrefab().Load(context);
                break;
            case "rs":
            case "rail_s":
                new RailgunSmallPrefab().Load(context);
                break;
            case "rxs":
            case "rail_xs":
                new RailgunXSmallPrefab().Load(context);
                break;
            case "ml":
            case "missile_l":
                new MissileLargePrefab().Load(context);
                break;
            case "mm":
            case "missile_m":
                new MissileMediumPrefab().Load(context);
                break;
            case "ms":
            case "missile_s":
                new MissileSmallPrefab().Load(context);
                break;
            case "mxs":
            case "missile_xs":
                new MissileXSmallPrefab().Load(context);
                break;
            case "ll":
            case "laser_l":
                new LaserLargePrefab().Load(context);
                break;
            case "lm":
            case "laser_m":
                new LaserMediumPrefab().Load(context);
                break;
            case "ls":
            case "laser_s":
                new LaserSmallPrefab().Load(context);
                break;
            case "lxs":
            case "laser_xs":
                new LaserXSmallPrefab().Load(context);
                break;
            default:
                context.Console.WriteLine($"{prefabName} NOT FOUND");
                return;
        }
        
        context.Console.WriteLine($"{context.WeaponCount} {context.WeaponId} Loaded");
    }
}