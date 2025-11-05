using System.Collections.Generic;
using StarWarsCore.Helpers;

namespace StarWarsCore.Models
{
    public class Obi_Wan : JediKnight
    {
        public string Encouragement = "Hang on, Luke. Use the fart, Luke!";
        public string Dismay = "Luke. Concentrate, boy - why did it ever come to this?? Cream him, dammit!";
        
        public Obi_Wan()
        {
            Name = "Obi-Wan Kenobi";            
            Email = "obiwan@tattooine.com";
            LightSaberColor = "White";
            CurrentDamageLevel = DamageLevel.Healthy;
            DarkSide = false;
            Deceased = false;            
            FightLog = new AttackRecorder();
            FightLog.FightEvents = new List<string>()
	            {
	                Name + ", unnoticed by everyone, invisibly slinks into his observer's position a good distance away from the scrap."	                
	            };
            LastWords = "Ok, so you got me there, mr. Evil Eyes. Or so you think, hahah, it's all just a jedi mind trick!!";
            NumOfDeaths = 0;
            ImageUrl = "../images/jokevader.jpg";
        }

        public void SaveLuke(JediKnight myLuke)
        {
            myLuke.Deceased = false;
            myLuke.CurrentDamageLevel = DamageLevel.Challenged;                   
        }
    }
}