using StarWarsCore.Helpers;

namespace StarWarsCore.Models;

public abstract class Creature
{
    public string Name;

    public enum DamageLevel
    {
        Healthy = 8,
        LightlyInjured = 7,
        Injured = 6,
        BadlyInjured = 5,
        Bloodied = 4,
        NearDeath = 3,
        Unconscious = 2,
        Dead = 1
    }
    public DamageLevel CurrentDamageLevel;
    
    public enum AttackAction
    {
        
    }

    public AttackRecorder ActionComment;
    public string Greeting;
    public string LastWords;
    public bool isDead = false;

    public void Fight(Creature attacker, Creature defender)
    {
        int damage = RandomGenerator.Rand.Next((int)defender.CurrentDamageLevel);
        switch (damage)
        {
            case 1 :
                defender.CurrentDamageLevel = DamageLevel.Dead;
                break;
            case 2 :
                defender.CurrentDamageLevel = DamageLevel.Unconscious;
                break;
            case 3 :
                defender.CurrentDamageLevel = DamageLevel.NearDeath;
                break;
            case 4 :
                defender.CurrentDamageLevel = DamageLevel.Bloodied;
                break;
            case 5 :
                defender.CurrentDamageLevel = DamageLevel.BadlyInjured;
                break;
            case 6 :
                defender.CurrentDamageLevel = DamageLevel.Injured;
                break;
            case 7 :
                defender.CurrentDamageLevel = DamageLevel.LightlyInjured;
                break;
            case 8 :
                defender.CurrentDamageLevel = DamageLevel.Healthy;
                break;
            default:
                break;
            
        }
    }
}