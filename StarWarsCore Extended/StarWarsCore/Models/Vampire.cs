using StarWarsCore.Helpers;

namespace StarWarsCore.Models;

public class Vampire : Monster
{
    public Vampire(string name)
    {
        Name = name;
        isDead = false;
        Greeting = "Prepare to join my nest, Blah BBBlahh";
        LastWords = "BLAHHHHHHHHHHHH";
        CurrentDamageLevel = Creature.DamageLevel.Healthy;
        imageURL = "../images/VampireAttack.gif";
        ActionComment = new AttackRecorder();
        ActionComment.FightEvents = new List<string>()
        {
            Name + ": Blah, Blah, Blah",
            Name + ": Jeg slikker på din pulsåre, Blah BBBLAHHH"

        };
        
        AddWeakness("Dead Man's Blood");
        AddWeakness("Sunlight");
        AddWeakness("Colt");
        AddWeakness("Machete");
        AddWeakness("Silver Blade");
    }
    /// <summary>
    /// Add a string to a list of weakness strings
    /// </summary>
    /// <param name="weakness"></param>
    public void AddWeakness(string weakness)
    {
        Weaknesses.Add(weakness);
    }
}