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

    public AttackRecorder ActionComment;
    public string Greeting;
    public string LastWords;
    public bool isDead = false;
    public string imageURL = "";

    public virtual void Fight(Creature attacker, Creature defender, int bonusDamage = 0)
    {
        int damage = RandomGenerator.Rand.Next((int)defender.CurrentDamageLevel);
        if (bonusDamage != 0)
        {
            damage -= bonusDamage;
            if (damage < 1)
            {
                damage = 1;
            }
        }

        switch (damage)
        {
            case 1 :
                defender.CurrentDamageLevel = DamageLevel.Dead;
                defender.isDead = true;
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