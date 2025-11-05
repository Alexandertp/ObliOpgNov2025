using System.Collections.Generic;
using StarWarsCore.Helpers;


namespace StarWarsCore.Models
{
    public class EvilEmperor : JediKnight
    {
        public string Encouragement = "Nice! Good boy, Darth. Now, just breathe on the enemy..heheh";
        public string Dismay = "Darth. Concentrate now, apprentice - cut out those blissful-idiot " +
                               "thoughts of forgiveness - just off him, dammit!";
        private JediKnight lastPickedJedi { get; set; }

        public EvilEmperor()
        {
            Name = "Emperor First Class Palpatine";
            Email = "evildude@deathstar.com";
            LightSaberColor = "SmokeyJoeBlack";
            CurrentDamageLevel = DamageLevel.Healthy;
            DarkSide = true;
            Deceased = false;
            LastWords = "Aaaaargh, Darth..! So, you got me... but, what's it say in your contract about " +
                        "killing your own boss?? Haw! Haw! Gotcha!";
            FightLog = new AttackRecorder();
            FightLog.FightEvents = new List<string>()
                {
                    Name + " cracks a very white knuckle and smirks - sporting a really evil, toothless smile for the camera. Haw haw!"
                };

            NumOfDeaths = 0;
            lastPickedJedi = null;
            ImageUrl = "../images/yoda.jpg";
        }

        public void DarkSideBoost(List<JediKnight> fightingFools)
        {
            // Palpatine has a special ability to do someting nasty to enemy do-gooder health in general - and he uses it!
            foreach (JediKnight fightingFool in fightingFools)
            {

                if (fightingFool.DarkSide == false)
                {
                    // Let's bring 'em down a notch, hah!
                    switch (fightingFool.CurrentDamageLevel)
                    {
                        case DamageLevel.Healthy:
                            fightingFool.CurrentDamageLevel = DamageLevel.Challenged;
                            break;
                        case DamageLevel.Challenged:
                            fightingFool.CurrentDamageLevel = DamageLevel.Hurting;
                            break;
                        case DamageLevel.Hurting:
                            fightingFool.CurrentDamageLevel = DamageLevel.Critical;
                            break;
                        case DamageLevel.Critical:
                            fightingFool.CurrentDamageLevel = DamageLevel.Wasted;
                            break;
                        default:
                            fightingFool.CurrentDamageLevel = DamageLevel.Critical;
                            break;
                    }

                    fightingFool.FightLog.FightEvents.Add(fightingFool.Name + " has been touched by Palpatine's evil blue lightning and now has a damage level of " + fightingFool.CurrentDamageLevel);
                    ErrorLogger.SaveMsg(fightingFool.Name + " has been touched by Palpatine's evil blue lightning and now has a damage level of " + fightingFool.CurrentDamageLevel);
                }
                else
                {
                    // Let's help the baddie side a notch up
                    switch (fightingFool.CurrentDamageLevel)
                    {
                        case DamageLevel.Healthy:
                            // cool, do nothing
                            break;
                        case DamageLevel.Challenged:
                            fightingFool.CurrentDamageLevel = DamageLevel.Healthy;
                            break;
                        case DamageLevel.Hurting:
                            fightingFool.CurrentDamageLevel = DamageLevel.Challenged;
                            break;
                        case DamageLevel.Critical:
                            fightingFool.CurrentDamageLevel = DamageLevel.Hurting;
                            break;
                        default:
                            fightingFool.CurrentDamageLevel = DamageLevel.Healthy;
                            break;
                    }

                    fightingFool.FightLog.FightEvents.Add(fightingFool.Name + " has been touched by Palpatine's benign white lightning and now has a damage level betterment of " + fightingFool.CurrentDamageLevel);
                    ErrorLogger.SaveMsg(fightingFool.Name + " has been touched by Palpatine's benign white lightning and now has a damage level betterment of " + fightingFool.CurrentDamageLevel);

                }

            }
        }

        public JediKnight PickJediKnight(List<JediKnight> fightingFools)
        {
            
            // Pick a random Jedi Knight to kick into action
            int randomInt1 = RandomGenerator.Rand.Next(0, fightingFools.Count);           
            JediKnight warrior = fightingFools[randomInt1];

            // Nested if statement will allow for two attack runs in a row from the same side by random chance
            if (lastPickedJedi == null)
            {
                lastPickedJedi = warrior;
                ErrorLogger.SaveMsg("Palpatine has picked " + warrior.Name + " as the very first combattant today!");
                return warrior;
            }
            else if (lastPickedJedi.Name != warrior.Name)
            {
                lastPickedJedi = warrior;
                ErrorLogger.SaveMsg("First condition is hit, and ol' Palpatine has picked " + warrior.Name);
                return warrior;
            }
            else
            {
                foreach (JediKnight fool in fightingFools)
                {
                    // pick the first guy who ain't dead yet from the opposing team
                    if ((fool.DarkSide != lastPickedJedi.DarkSide) && (fool.Name != lastPickedJedi.Name) && (fool.Deceased == false))
                    {                      
                            lastPickedJedi = fool;
                            ErrorLogger.SaveMsg("Else clause is hit, and ol' Palpatine has just picked " + fool.Name);
                            return fool;                   
                    }
                    // no happy jack - pick the dead guy up from the floor and call the force medics
                    else if ((fool.DarkSide != lastPickedJedi.DarkSide) && (fool.Name != lastPickedJedi.Name) && fool.Deceased)
                    {
                        fool.CurrentDamageLevel = DamageLevel.Hurting;
                        lastPickedJedi = fool;
                        ErrorLogger.SaveMsg("Else if clause is hit, and ol' Palpatine has just set a damage level of Hurting for "
                            + fool.Name + " who was previously offed, now returning him safely to the battle field once again!");
                        return fool;
                    }
                }
            }
            // default: if all else fails, return the original randomly picked Jedi            
            ErrorLogger.SaveMsg("Default clause is hit, and " + warrior.Name + " was picked.");
            return warrior;
        }

    }
}
