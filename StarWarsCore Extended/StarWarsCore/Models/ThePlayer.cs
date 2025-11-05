using StarWarsCore.Helpers;

namespace StarWarsCore.Models
{
    public class ThePlayer : JediKnight
    {
        public ThePlayer()
        {
            // Init the personal fight log for our guy
            FightLog = new AttackRecorder();
            FightLog.FightEvents = new List<string>()
                    {
                        Name + " loosens his tie",
                        Name + " unstraps his lightsaber and admires it's wonderfully " + LightSaberColor + " sheen.",
                        Name + " quotes Caesar: 'Jacta Alea Est', dude!",
                        Name + " shuffles his feet and looks a bit shyly at the other guys.. quite imposing figures, actually.."
                    };
            LastWords = "Aaaaargh, I.. really hate it when they creep up behind you like that.";
            ImageUrl = "../images/badass.jpg";
        }
    }
}
