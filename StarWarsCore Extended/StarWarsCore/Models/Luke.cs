using System.Collections.Generic;
using StarWarsCore.Helpers;

namespace StarWarsCore.Models
{
    public class Luke : JediKnight
    {
        public Luke()
        {
            Name = "Luke Skywalker";
            Email = "ls@tattooine.com";
            LightSaberColor = "Blue";
            CurrentDamageLevel = JediKnight.DamageLevel.Healthy;
            DarkSide = false;
            Deceased = false;
            FightLog = new AttackRecorder();
            FightLog.FightEvents = new List<string>()
                {
                    Name + " loosens his tie and straps on Leia's bra for verisimilitude.",
                    Name + " unstraps his lightsaber. It's blue!",
                    Name + " looks at his Dad and quotes Groucho Marx: 'I have a mind to join a club and beat you over the head with it.'",
                    Name + " privately thinks of joining a different club altogether. Preferably one in a galaxy far, far away.."
                };

            LastWords = "Aaaaargh, I never thought to take out life insurance! How pathetic of me in this cruel, cruel world.";
            NumOfDeaths = 0;
            ImageUrl = "../images/luke.jpg";
        }
    }
}
