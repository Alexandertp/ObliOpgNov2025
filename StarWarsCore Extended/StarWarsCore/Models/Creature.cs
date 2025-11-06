using StarWarsCore.Helpers;

namespace StarWarsCore.Models;

public abstract class Creature
{
    public string Name;

    public enum DamageLevel
    {
        Healthy = 7,
        LightlyInjured = 6,
        Injured = 5,
        BadlyInjured = 4,
        Bloodied = 3,
        NearDeath = 2,
        Unconscious = 1,
        Dead = 0
    }
    public DamageLevel CurrentDamageLevel;
    
    public enum AttackAction
    {
        
    }

    public AttackRecorder ActionComment;
    public string Greeting;
    public string LastWords;
    public bool isDead = false;
    
    
}