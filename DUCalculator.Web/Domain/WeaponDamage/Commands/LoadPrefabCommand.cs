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
            case "rl":
            case "rail_l":
                context.LoadRailGunLarge();
                break;
            case "ml":
            case "missile_l":
                context.LoadMissileLarge();
                break;
            case "ll":
            case "laser_l":
                context.LoadLaserLarge();
                break;
            default:
                context.Console.WriteLine($"{prefabName} NOT FOUND");
                return;
        }
        
        context.Console.WriteLine($"{context.WeaponCount} {context.WeaponId} Loaded");
    }
}