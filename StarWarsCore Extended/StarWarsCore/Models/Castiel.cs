using StarWarsCore.Helpers;

namespace StarWarsCore.Models
{
    public class Castiel : Hunter
    {
        public Castiel()
        {
            Name = "Castiel";
            isDead = false;
            CurrentDamageLevel = DamageLevel.Healthy;
            Greeting = "I'm an angel, you ass!";
            LastWords = "Pull my finger *Dies of cringe*";
            AddWeapon("Angel Blade");
            ActionComment = new AttackRecorder();
            ActionComment.FightEvents = new List<string>()
            {
                Name + ": It's funnier in enochian.",
                Name + ": Hey, assbutt!",
                Name + ": What's a netflix?",
                Name + ": I don't sweat under any circumstances.",
                Name + ": Why is 6 afraid of 7? I assume it's because 7 is a prime number and prime numbers can be intimidating.",
                Name + ": I'll interrogate the cat.",
                Name + ": I like texting. Emoticons!",
            };
        }
        public void AddWeapon(string input)
        {
            Weapons.Add(input);
        }
    }
}
