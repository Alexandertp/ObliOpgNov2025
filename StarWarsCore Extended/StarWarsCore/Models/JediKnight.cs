using System;
using System.ComponentModel.DataAnnotations;
using StarWarsCore.Helpers;

namespace StarWarsCore.Models
{
    // Star Warrior base class - the Jedi Knight
    public abstract class JediKnight
    {
        // enum for damage level
        public enum DamageLevel
        {
            Healthy = 95,
            Challenged = 50,
            Hurting = 25,
            Critical = 10,
            Wasted = 0
        }

        // enum for academy approved light saber tricks
        public enum LightSaberAction
        {
            Italian_Kneecap_Twister = 0,
            Tickle_Enemy_Nose = 1,
            Scorch_Enemy_Scalp = 2,
            Sizzle_Enemy_Boots = 3,
            Cut_Enemy_Hand_Off = 4,
            Cut_Enemy_Leg_Off = 5,
            Wound_Enemy_Privates = 6,
            Scorch_Enemy_Butt = 7,
            Pierce_Enemy_Chest = 8,
            Nick_Enemy_Ear = 9,
            A_Real_Close_Shave = 10
        }

        // Some jedi knight fields and their properties, which we will 
        // want to get and set from the method running the fight

        // private backing field
        private DamageLevel currentDamageLevel;
        // public property              
        public DamageLevel CurrentDamageLevel
        {
            get { return currentDamageLevel; }
            set { currentDamageLevel = value; }
        }

        private LightSaberAction currentLightSaberAction;
        public LightSaberAction CurrentLightSaberAction
        {
            get { return currentLightSaberAction; }
            set { currentLightSaberAction = value; }
        }

        private bool deceased;
        public bool Deceased
        {
            get { return deceased; }
            set { deceased = value; }
        }

        private AttackRecorder fightLog;
        public AttackRecorder FightLog
        {
            get { return fightLog; }
            set { fightLog = value; }
        }

        private string lastWords;
        public string LastWords
        {
            get { return lastWords; }
            set { lastWords = value; }
        }

        // info fields and properties we need in order to set up a new recruit
        private string name;

        // MVC models support auto generated js for client side validation. Yes. It's black magic..
        [Required(ErrorMessage = "Please enter your name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string email;

        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+",
            ErrorMessage = "Please enter a valid email address")] 
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string lightSaberColor;
        [Required(ErrorMessage = "Please enter your preferred light saber color")]
        public string LightSaberColor
        {
            get { return lightSaberColor; }
            set { lightSaberColor = value; }
        }

        private bool? darkSide;

        // using a nullable bool for the DarkSide property allows us to validate a 'no choice'
        [Required(ErrorMessage = "Please specify if you are drawn to the dark side or not!")]
        public bool? DarkSide
        {
            get { return darkSide; }
            set { darkSide = value; }
        }

        private int numOfDeaths;        
        public int NumOfDeaths
        {
            get { return numOfDeaths; }
            set { numOfDeaths = value; }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }


        // some default enemy slanders here        
        private string[] enemySlanders = { "Avast, ye nasty lookin' slimeball - come get some!", 
                                           "I'll have you set up real nice real soon with a silly-looking toothless grin.",
                                           "Dude, you fight like a guy who experiments way too much with alcohol-brain osmosis.", 
                                           "Arrrrr! I'll have your arm off before long, too!",
                                           "You'll soon be very sorry that you ever set foot in this town.",
                                           "Dis mean ole worl' gone' have me punched both black and blue, but it also gonna do the same to you - scumbag!",
                                           "I'll kick your butt to Tatooine and back - you miserable meat popsicle pumped up on hydrogen derivates!",
                                           "Hey, Mr. two-brain-cells-kissing... watch this!" };

        // attack another jedi knight with the help of magic random numbers       
        public void AttackEnemy(JediKnight jediKnight, JediKnight opponent)
        {
            try
            {
                // setup for an attack using a standard lightsaber action, registering opponent's resulting damage level, and slandering him                
                // null ref check would be well placed here, but the try-catch will handle them.
                if (opponent.CurrentDamageLevel == DamageLevel.Wasted)
                {
                    // He's already finito.. see if we can make him join the game again!
                    jediKnight.FightLog.FightEvents.Add(jediKnight.Name + " takes a poke trying to off " + opponent.Name + " who is already at the Rigor Mortis stage. Tsk, tsk.. a bit necro-phixated, are we?");
                    // Rescusiate.. err, recycle!
                    jediKnight.FightLog.FightEvents.Add(jediKnight.Name + " gets all squishy-feely at seeing " + opponent.Name + " so horribly pale, and takes him - ok, drags him.. along to the Jawa sandcrawler for recycling.");

                    // At this point, let Obi-Wan intervene to save only Luke from the Jawa recycling bin
                    if (opponent.Name == "Luke Skywalker")
                    {   
                        // match functionality of saveLuke method on ObiWan object
                        opponent.Deceased = false;
                        opponent.CurrentDamageLevel = DamageLevel.Challenged;
                        // add some output to fight log text stream
                        jediKnight.FightLog.FightEvents.Add("At this very moment, Obi-Wan snaps out of the void and rescues Luke from certain rearrangement of his internal organs.");
                        jediKnight.FightLog.FightEvents.Add("Luke is now back in the game with a damage level of " + opponent.CurrentDamageLevel); 
                    }
                    else
                    {
                        opponent.CurrentDamageLevel = DamageLevel.Critical;
                        opponent.Deceased = false;
                        jediKnight.FightLog.FightEvents.Add(opponent.Name + " didn't bring in much of a recycle fee for " + jediKnight.Name + 
                            " - but the Jawas were real nice. Our hero was kicked out in the desert again alive, minus only a few insignificant internal body parts and sporting a damage level of: "
                            + opponent.CurrentDamageLevel);
                    }
                }

                else
                {
                    int randomInt = RandomGenerator.Rand.Next(11);
                    // record which lightsaber maneouver was randomly chosen
                    CurrentLightSaberAction = (LightSaberAction)randomInt;
                    jediKnight.FightLog.FightEvents.Add(jediKnight.Name + " attacks " + opponent.Name
                                             + " by deftly applying the Light Saber Academy approved maneouver "
                                             + CurrentLightSaberAction);

                    // Make a stab using the current DamageLevel cast to an int. Record which new damage level resulted from the attack.
                    randomInt = RandomGenerator.Rand.Next((int)(opponent.CurrentDamageLevel));
                    // Test to see that the thread sleep is enough to give us a new random number every time
                    ErrorLogger.SaveMsg("Random health level number generated from max value " + (int)(opponent.CurrentDamageLevel) + " is:" + randomInt);
                    // Series of if statements, to determine which damage level is closest to the generated number
                    if (randomInt >= 50)
                        opponent.CurrentDamageLevel = DamageLevel.Healthy;
                    else if (randomInt < 50 && randomInt > 25)
                        opponent.CurrentDamageLevel = DamageLevel.Challenged;
                    else if (randomInt <= 25 && randomInt > 10)
                        opponent.CurrentDamageLevel = DamageLevel.Critical;
                    else // less than or equal to 10, we's long gone dude
                    {
                        opponent.CurrentDamageLevel = DamageLevel.Wasted;
                        opponent.Deceased = true;
                    }

                    // special handling for the one absolutely fatal case of Light Saber Action
                    if (CurrentLightSaberAction == LightSaberAction.Pierce_Enemy_Chest)
                    {
                        opponent.CurrentDamageLevel = DamageLevel.Wasted;
                        opponent.Deceased = true;
                        jediKnight.FightLog.FightEvents.Add(jediKnight.Name + " says: Hah, gotcha there... hole in one, dude!");
                    }

                    
                    // Pick a random slander expression for posterity, except if he is already dead as a result of that cool last attack of yours.
                    if (opponent.CurrentDamageLevel > DamageLevel.Wasted)
                    {
                        // System.Threading.Thread.Sleep(2500); // skip a heartbeat, so we get a new random seed number
                        jediKnight.FightLog.FightEvents.Add(opponent.Name + " now has a damage level of: " + opponent.CurrentDamageLevel + " based on a randomInt of " + randomInt);
                        randomInt = RandomGenerator.Rand.Next(0, enemySlanders.Length);
                        string slander = enemySlanders[randomInt];
                        jediKnight.FightLog.FightEvents.Add(jediKnight.Name + " says: " + slander);
                    }
                    else if (opponent.CurrentDamageLevel == DamageLevel.Wasted)
                    {
                        if (CurrentLightSaberAction != LightSaberAction.Pierce_Enemy_Chest)
                        {
                            jediKnight.FightLog.FightEvents.Add(opponent.Name + " now has a damage level of: " + opponent.CurrentDamageLevel + " based on a randomInt of " + randomInt);
                        }
                        jediKnight.FightLog.FightEvents.Add(opponent.Name + " hoarsely whispers: " + opponent.LastWords);
                        jediKnight.FightLog.FightEvents.Add(jediKnight.Name + " says: Rest in pieces, " + opponent.Name + ".");
                        jediKnight.FightLog.FightEvents.Add(opponent.Name + " has bought the big farm in the sky.");
                        // increment the counter for opponent's number of deaths
                        opponent.NumOfDeaths++; // = opponent.NumOfDeaths + 1;
                    }
                }
            }
            catch (Exception e)
            {
                // log the exception msg to a text file or db
                string msg = e.Message;
                ErrorLogger.SaveMsg(msg);
            }            
        }
    }
}
