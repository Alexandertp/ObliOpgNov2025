using StarWarsCore.Helpers;

namespace StarWarsCore.Models;

public class Dean : Hunter
{
    public Dean()
    {
        Name = "Dean";
        isDead = false;
        
        Greeting = "Hey. Do you know who wears sunglasses inside? Blind people and douchebags.";
        LastWords = "I'm beyond saving. I know how my story is gonna end. It's at the edge of a blade or the barrel of a gun.";
        CurrentDamageLevel = Creature.DamageLevel.Healthy;
        imageURL = "../images/DeanAttack.gif";
        ActionComment = new AttackRecorder();
        ActionComment.FightEvents = new List<string>()
        {
            Name + ": You hold him down while we knife him, and then we'll all go out for ice cream and strippers.",
            Name + ": Life as an angel condom. That's real fun.",
            Name + ": What kind of douchebag names a character after himself?",
            Name + ": I'm not wearing any makeup. Oh crap. I'm a painted whore.",
            Name + ": What I'm good at... is slicing throats. I ain't a father. I'm a killer. And there's no changing that. I know that now.",
            Name + ": You're just as screwed up as I am, you're just... bigger.",
            Name + ": I'm going to go hit the poop deck.",
            Name + ": Karma's a bitch, bitch.",
            Name + ": Let me get dressed, Robo-Cop.",
            Name + ": What are you, the Hamburglar?",
            Name + ": Eat it, Twilight!",
            Name + ": I'm Batman.",
            Name + ": Dude, you're fugly."
        };
        
        
    }
    
}