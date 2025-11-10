using StarWarsCore.Helpers;
using System.Xml.Linq;

namespace StarWarsCore.Models
{
    public class Ghost : Monster
    {
        public Ghost(string name)
        {
            Name = name;
            isDead = false;
            Greeting = "Wooo, I am a ghost.";
            LastWords = "Wooo, I'm going to the nether.";
            CurrentDamageLevel = Creature.DamageLevel.Healthy;
            ActionComment = new AttackRecorder();
            ActionComment.FightEvents = new List<string>()
        {
            Name + ": Oh noooo, woooo",
            Name + ": Woooo",
            Name + ": I am a ghost, let's fight!"

        };

            AddWeakness("Salt");
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
}

