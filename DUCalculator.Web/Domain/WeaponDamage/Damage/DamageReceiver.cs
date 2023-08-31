namespace DUCalculator.Web.Domain.WeaponDamage.Damage;

public class DamageReceiver
{
    private double _am = 0.25;
    private double _em = 0.25;
    private double _kn = 0.25;
    private double _th = 0.25;
    
    public double AntimatterResistance
    {
        set => _am = Math.Clamp(value, 0, 1);
        get => _am;
    }

    public double ElectromagneticResistance
    {
        set => _em = Math.Clamp(value, 0, 1);
        get => _em;
    }
    public double KineticResistance
    {
        set => _kn = Math.Clamp(value, 0, 1);
        get => _kn;
    }
    
    public double ThermicResistance
    {
        set => _th = Math.Clamp(value, 0, 1);
        get => _th;
    }

    public double ResistDamage(IDamageType damageType, double damage) 
        => damageType.CalculateDamage(this, damage);

    public void LoadCannonResistances()
    {
        KineticResistance = 0.5;
        ThermicResistance = 0.5;
    }
    
    public void LoadRailgunResistances()
    {
        AntimatterResistance = 0.5;
        ElectromagneticResistance = 0.5;
    }
    
    public void LoadMissileResistances()
    {
        AntimatterResistance = 0.5;
        KineticResistance = 0.5;
    }
    
    public void LoadLaserResistances()
    {
        ElectromagneticResistance = 0.5;
        ThermicResistance = 0.5;
    }

    public void Zero()
    {
        KineticResistance = 0;
        ThermicResistance = 0;
        AntimatterResistance = 0;
        ElectromagneticResistance = 0;
    }

    public override string ToString()
    {
        return $"Resistances AM: {AntimatterResistance} EM: {ElectromagneticResistance} KN: {KineticResistance} TH: {ThermicResistance}";
    }
}