using StarWarsCore.Helpers;
using System.Xml.Linq;

namespace StarWarsCore.Models
{
    public class Demon : Monster
    {
        public Demon(string name)
        {
            Name = name;
            isDead = false;
            Greeting = "Grrrr, I am an evil demon.";
            LastWords = "Ahhhh, I'm going back to hell!";
            CurrentDamageLevel = Creature.DamageLevel.Healthy;
            imageURL = "../images/DemonAttack.gif style='width:450px;height:auto'";
            ActionComment = new AttackRecorder();
            ActionComment.FightEvents = new List<string>()
        {
            Name + ": Screw you!",
            Name + ": I'm gonna kill you!"

        };

            AddWeakness("Holy Water");
            AddWeakness("Exorcism");

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
