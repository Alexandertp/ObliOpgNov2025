using StarWarsCore.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsCore.Models


{
    public class Sam : Hunter
    {
        public Sam()
        {
            Name = "Sam";
            isDead = false;
            CurrentDamageLevel = Creature.DamageLevel.Healthy;
            Greeting = "I've been tortured by the Devil himself. So you, you're just an accent in a pantsuit. What can you do to me?";
            LastWords = "Dude I just got whaled on by Paris Hilton";
            FightLog = new AttackRecorder();
            FightLog.FightEvents = new List<string>()
            {
                Name + "Dude, you're confusing reality with porn again.",
                Name + "And you know what? After we kill it, we can go to Disneyland!",
                Name + "What kind of house doesn't have salt? Low sodium FREAKS.",
                Name + "Dude, you're not going to poke her with a stick.",
                Name + "You're bossy, and short.",
                Name + "Wait, there's no such things as unicorns?"
            };

        }

    }
}