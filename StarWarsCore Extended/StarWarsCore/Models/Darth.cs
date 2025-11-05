using System.Collections.Generic;
using StarWarsCore.Helpers;

namespace StarWarsCore.Models
{
    public class Darth : JediKnight
    {
        public Darth()
        {
            Name = "Darth Vader";
            Email = "dv@deathstar.com";
            LightSaberColor = "Red";
            CurrentDamageLevel = JediKnight.DamageLevel.Healthy;
            DarkSide = true;
            Deceased = false;
            FightLog = new AttackRecorder();
            FightLog.FightEvents = new List<string>()
                {
                    Name + " loosens his tie, or rather, what he *thinks* is his tie. And no, you don't want to know any more.",
                    Name + " unstraps his lightsaber. It's red!",
                    Name + " quotes Caesar: 'Jacta Alea Est', dude! Come get some, Luke..",
                    Name + " expels a horribly dry laugh from his asthmatic lungs and sneers at Luke."
                };

            LastWords = "Aaaaargh, I thought that little bugger was on top of the generator!";
            ImageUrl = "../images/darth.jpg";
        }
    }
}
