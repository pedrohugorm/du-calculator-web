using System.Diagnostics;
using DUCalculator.Web.Domain.WeaponDamage.Commands;

namespace DUCalculator.Web.Domain.WeaponDamage;

public class TickCommand : IWeaponDamageCommand
{
    public string[] Commands => new[] { "tick", "simulate", "sim" };
    public string Description => "Starts the simulation with the Ship State and configuration you setup";
    public string Example => "sim";

    public void Execute(WeaponDamageContext context)
    {
        var sw = new Stopwatch();
        sw.Start();

        if (context.Multiplier == 0 || !context.ShipState.Weapons.Any())
        {
            context.Console.WriteLine("Will never do Damage: Resistances are 100% (>=1) OR Multiplier is Zero OR No Weapons added");
            return;
        }

        var iterations = 0;
        while (true)
        {
            context.ShipState.Tick(context);
            iterations++;

            if (context.IsSimulationDone())
            {
                break;
            }

            if (sw.Elapsed > TimeSpan.FromSeconds(3))
            {
                context.Console.WriteLine("Simulation Timeout");
                break;
            }
        }
        
        sw.Stop();

        context.Console.WriteLine($"Finished Simulating. Took = {sw.Elapsed}. Iterations = {iterations}");
        
        var printReport = new PrintShipCommand();
        printReport.Execute(context);
    }
}