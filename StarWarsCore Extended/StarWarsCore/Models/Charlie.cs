using StarWarsCore.Helpers;
using System.Xml.Linq;

namespace StarWarsCore.Models
{
    public class Charlie : Hunter
    {
        public Charlie()
        {
            Name = "Charlie";
            isDead = false;
            CurrentDamageLevel = Creature.DamageLevel.Healthy;
            Greeting = "I'm in. I've always wanted to say that!";
            LastWords = "Good luck saving the world. Peace out, bitches.";
            imageURL = "../images/CharlieAttack.gif style='width:450px;height:auto'";
            ActionComment = new AttackRecorder();
            ActionComment.FightEvents = new List<string>()
            {
                Name + ": Honestly, historically I've had this problem with authority. No offense.",
                Name + ": If you can't score at a reproductive rights function, then you simply cannot score.",
                Name + ": I was drunk. It was Comic-con.",
                Name + ": I should have taken that job at Google.",
                Name + ": I'm gonna kick it in the ass.",
                Name + ": Wait a second. Seriously? \"Wargames\"? Shall we play a game, bitches?",
                Name + ": Your password is \"winning\" with two 1's? Fail.",
                Name + ": I sing when I'm nervous; don't judge me!"
            };
        }
    }
}
