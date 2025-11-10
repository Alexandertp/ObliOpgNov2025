using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StarWarsCore.Models;
using StarWarsCore.Helpers;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StarWarsCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good morning" : "Good afternoon";
            return View();
        }

        [HttpGet]
        public ViewResult RecruitForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RecruitForm(ThePlayer newWarrior)
        {
            // check user input
            if (ModelState.IsValid)
            {
                try
                {
                    // Set up a game log of Fight Events
                    AttackRecorder gameLog = new AttackRecorder();
                    gameLog.FightEvents = new List<string>() { "..the game log was the only book in town that the Evil Emperor wanted to read." + "</br>" };

                    // Set up a Jedi Warrior object - using the data entered in the form
                    ThePlayer myWarrior = new ThePlayer();
                    myWarrior.Name = newWarrior.Name;
                    myWarrior.Email = newWarrior.Email;
                    myWarrior.LightSaberColor = newWarrior.LightSaberColor;
                    myWarrior.CurrentDamageLevel = JediKnight.DamageLevel.Healthy;
                    myWarrior.DarkSide = newWarrior.DarkSide;
                    myWarrior.Deceased = false;

                    
                    // start up first knight in game log
                    gameLog.FightEvents.AddRange(ListOutput(myWarrior.FightLog.FightEvents));

                    // Set up some more Jedi objects
                    Darth DarthV = new Darth();
                    // start up second knight in game log
                    gameLog.FightEvents.AddRange(ListOutput(DarthV.FightLog.FightEvents));

                    Luke LukeS = new Luke();
                    // start up third knight in game log
                    gameLog.FightEvents.AddRange(ListOutput(LukeS.FightLog.FightEvents));

                    List<JediKnight> myJedis = new List<JediKnight>();
                    myJedis.Add(myWarrior);
                    myJedis.Add(DarthV);
                    myJedis.Add(LukeS);

                    // Dark horses: Obi-Wan and The Emperor
                    Obi_Wan ObiWan = new Obi_Wan();
                    gameLog.FightEvents.AddRange(ListOutput(ObiWan.FightLog.FightEvents));

                    EvilEmperor Palpatine = new EvilEmperor();
                    gameLog.FightEvents.AddRange(ListOutput(Palpatine.FightLog.FightEvents));
                    // clear a few lines of output for spacing the battle data from preliminary speech
                    gameLog.FightEvents.Add("</br></br>");

                    List<Hunter>hunters = new List<Hunter>();
                    List<Monster>monsters = new List<Monster>();
                    Sam sam = new Sam();
                    Dean dean = new Dean();
                    Castiel castiel = new Castiel();
                    hunters.Add(sam);
                    hunters.Add(dean);
                    hunters.Add(castiel);

                    Vampire vampBob = new Vampire("Bob");
                    Ghost ghost = new Ghost("Magrethe");
                    Demon demonAsmon = new Demon("Asmongold");
                    monsters.Add(vampBob);
                    monsters.Add(ghost);
                    monsters.Add(demonAsmon);

                    gameLog.FightEvents.Add("</br>" + hunters[0].Name);

                    // CombatLoop der kører indtil enten alle hunters eller monsters er døde
                    while (hunters.Any(x => x.isDead == false) && monsters.Any(x => x.isDead == false))
                    {
                        int initiative = 0;
                        do
                        {
                            initiative = RandomGenerator.Rand.Next(hunters.Count);
                        } while (hunters[initiative].isDead);
                        int offer = 0;
                        do
                        {
                           offer = RandomGenerator.Rand.Next(monsters.Count);
                        } while (monsters[offer].isDead);
                        // hunters angriber først
                        hunters[initiative].Fight(hunters[initiative], monsters[offer]);
                        // Tilføj en actionComment fra den samme hunter som lige har angrebet og tilføj den til gameLog
                        gameLog.FightEvents.Add(hunters[initiative].ActionComment.FightEvents[RandomGenerator.Rand.Next(hunters[initiative].ActionComment.FightEvents.Count)]);
                        do 
                        {
                            initiative = RandomGenerator.Rand.Next(monsters.Count);
                        } while (monsters[initiative].isDead);
                        do
                        {
                            offer = RandomGenerator.Rand.Next(hunters.Count);
                        } while (hunters[offer].isDead);
                        monsters[initiative].Fight(monsters[initiative], hunters[offer]);
                        gameLog.FightEvents.Add(monsters[initiative].ActionComment.FightEvents[RandomGenerator.Rand.Next(monsters[initiative].ActionComment.FightEvents.Count)]);

                    }
                    // Run battle scenario umpteen times, enough to kill one of our protagonists outright a few times over. And to bring in Obi-Wan.
                    for (int i = 1; i < 25; i++)
                    {
                        // Pick a random Jedi Knight to kick into action courtesy of our Helper class                             
                        // int randomInt = RandomGenerator.Rand.Next(0, myJedis.Count);
                        // JediKnight warrior = myJedis[randomInt];
                        // Let ol' Palpatine decide who's next, rather than calling up the same guy by chance
                        JediKnight warrior = Palpatine.PickJediKnight(myJedis);

                        // Empty the individual fight log before next batch recording of events
                        warrior.FightLog.FightEvents.Clear();
                        warrior.FightLog.FightEvents.Add("The Evil Emperor randomly points a bony finger at " + warrior.Name + " and says: 'do some nasty work for me - now!'");

                        // Attack opponents, but only if they are on the opposing side, and only if you aren't dead yourself..yet.            
                        switch (warrior.Name)
                        {
                            case "Darth Vader":
                                if (!warrior.Deceased)
                                {
                                    warrior.AttackEnemy(warrior, LukeS);
                                    if (LukeS.CurrentDamageLevel == JediKnight.DamageLevel.Hurting)
                                    {
                                        // a few words of desparate pleading from Obi-Wan, since his protegé Luke is decidedly getting nastified by the dark lord of the Sith
                                        warrior.FightLog.FightEvents.Add("Luke hears a well-known voice from beyond the grave.. Obi-Wan says: " + ObiWan.Dismay);
                                    }
                                    // Attack the player if he's not a Sith
                                    if (myWarrior.DarkSide == false) { warrior.AttackEnemy(warrior, myWarrior); }

                                    // Attack Obi-Wan, if he is in the game
                                    if (myJedis.Contains(ObiWan)) { warrior.AttackEnemy(warrior, ObiWan); }

                                    // if Darth Vader is pressured with a health level of critical..bring his boss' secret weapon to bear
                                    // if ((warrior.currentDamageLevel == JediKnight.DamageLevel.Critical) || warrior.NumOfDeaths > 3)
                                    if (warrior.CurrentDamageLevel == JediKnight.DamageLevel.Critical)
                                    {
                                        warrior.FightLog.FightEvents.Add("Darth Vader hears a well-known voice from the void.. The Evil Emperor says: " + Palpatine.Dismay);
                                        // Let Palpatine at the bastards! Critical condition or three times under the floorboards is enough for Darth.
                                        Palpatine.DarkSideBoost(myJedis);
                                        warrior.FightLog.FightEvents.Add("The Evil Emperor has shown his hand in dealing the Jedis a jaw-crusher, resetting a few damage levels.");
                                    }
                                    if (warrior.CurrentDamageLevel == JediKnight.DamageLevel.Hurting)
                                    {
                                        warrior.FightLog.FightEvents.Add("Darth Vader hears a well-known voice from the void.. The Evil Emperor says: " + Palpatine.Encouragement);
                                    }
                                }
                                else // this happens if the Emperor points a bony finger at a dead DV
                                {
                                    warrior.FightLog.FightEvents.Add(warrior.Name + " unfortunately feels a bit out of it right now, and is rather actively knockin' on the Force's Door.");
                                    // Darth is a mean ole bean, so we'll re-life him only with a lousy medicare allowance of 'Hurting'
                                    warrior.Deceased = false;
                                    warrior.CurrentDamageLevel = JediKnight.DamageLevel.Hurting;
                                    warrior.FightLog.FightEvents.Add("Wow, that door-mojo worked... " + warrior.Name + " is in hurting condition, but still bouncing back for some mean ole revenge!");
                                }

                                // add player fight events to game log in sequence
                                // gameLog.FightEvents.AddRange(warrior.fightLog.FightEvents);
                                HtmlEncodeFightEvents(gameLog, warrior);
                                break;

                            case "Luke Skywalker":
                                if (!warrior.Deceased)
                                {
                                    // Attack Dad!
                                    warrior.AttackEnemy(warrior, DarthV);
                                    if (myWarrior.DarkSide == true) { warrior.AttackEnemy(warrior, myWarrior); }

                                    // if Luke Skywalker is is pressured with a health level of critical, let's bring Obi-Wan into the game. 
                                    // NB: Only once, though.
                                    if ((warrior.CurrentDamageLevel == JediKnight.DamageLevel.Critical)
                                        && (!myJedis.Contains(ObiWan)))
                                    {
                                        ObiWan.SaveLuke(warrior);
                                        myJedis.Add(ObiWan);
                                        warrior.FightLog.FightEvents.Add("Obi-Wan helps Luke get healthy again and decides that now's the perfect time to join the fun! Woo-hoo!");
                                    }
                                    // If he's only hurting, encourage him a bit
                                    if (warrior.CurrentDamageLevel == JediKnight.DamageLevel.Hurting)
                                    {
                                        // moral support from Obi-Wan for Luke
                                        warrior.FightLog.FightEvents.Add("Luke hears a voice from just beyond his skull.. Obi-Wan says: " + ObiWan.Encouragement);
                                    }
                                }
                                else
                                {
                                    warrior.FightLog.FightEvents.Add(warrior.Name + " unfortunately feels a bit out of it right now, and is feebly knockin' on the Force's Door, in a Lukey way.");
                                    // give Luke an advantage when killed, by rescusiating him and by setting his health to medium
                                    warrior.Deceased = false;
                                    warrior.CurrentDamageLevel = JediKnight.DamageLevel.Challenged;
                                    warrior.FightLog.FightEvents.Add(warrior.Name + " apparently has mucho clout with the Force's Door and is now very much alive and bouncing back for some swashbuckling revenge!");
                                    warrior.FightLog.FightEvents.Add(warrior.Name + " faintly hears the whispering voice of " + ObiWan.Name + ": " + ObiWan.Encouragement);
                                }

                                // add player fight events to game log in sequence
                                // gameLog.FightEvents.AddRange(warrior.fightLog.FightEvents);
                                HtmlEncodeFightEvents(gameLog, warrior);
                                break;

                            case "Obi-Wan Kenobi":
                                if (!warrior.Deceased)
                                {
                                    // Attack Darth!
                                    warrior.AttackEnemy(warrior, DarthV);
                                    // Attack the player object, if he's in a Sith kind of way
                                    if (myWarrior.DarkSide == true) { warrior.AttackEnemy(warrior, myWarrior); }
                                }
                                else
                                {
                                    warrior.FightLog.FightEvents.Add(warrior.Name + " unfortunately feels a bit out of it right now, and is feebly knockin' on the Force's Door, in an Obi-Wan kind of way.");
                                    // give Obi-Wan an advantage when killed, by rescusiating him and by setting his health to medium
                                    warrior.Deceased = false;
                                    warrior.CurrentDamageLevel = JediKnight.DamageLevel.Challenged;
                                    warrior.FightLog.FightEvents.Add(warrior.Name + " apparently has mucho clout with the Force's Door and is now very much alive and bouncing back for some swashbuckling revenge!");
                                }
                                // add player fight events to game log in sequence
                                // gameLog.FightEvents.AddRange(warrior.fightLog.FightEvents);
                                HtmlEncodeFightEvents(gameLog, warrior);
                                break;

                            // default case will be our guy on the dance floor wanting a serious piece of the action
                            default:
                                if (!warrior.Deceased)
                                {
                                    if (warrior.DarkSide == false) { warrior.AttackEnemy(warrior, DarthV); }
                                    if (warrior.DarkSide == true) { warrior.AttackEnemy(warrior, LukeS); }
                                }
                                else
                                {
                                    warrior.FightLog.FightEvents.Add(warrior.Name + " unfortunately feels a bit out of it right now, and is knockin' on the Force's Door.");
                                    warrior.Deceased = false;
                                    warrior.CurrentDamageLevel = JediKnight.DamageLevel.Challenged;
                                    warrior.FightLog.FightEvents.Add(warrior.Name + " apparently has some clout with the Force's Door and is now enlivened..bent on some challenged-level revenge!");
                                }
                                // add player fight events to game log in sequence
                                // gameLog.FightEvents.AddRange(warrior.fightLog.FightEvents);
                                HtmlEncodeFightEvents(gameLog, warrior);
                                break;
                        }
                    }

                    // Conclude fight at this point - check who's still vital, and do the victory roll
                    //foreach (JediKnight warrior in myJedis)
                    //{
                    //    if (!warrior.Deceased)
                    //    {
                    //        gameLog.FightEvents.Add("Ho ho ho! " + warrior.Name + "'s still around, with a rude health of: " + warrior.currentDamageLevel
                    //           + ". He has visited the earthworms " + warrior.NumOfDeaths + " times during the scrap.");

                    //    }
                    //    else
                    //    {
                    //        gameLog.FightEvents.Add("Awww, " + warrior.Name + " is sleeping with the fishes! He has visited the salty grave " 
                    //            + warrior.NumOfDeaths + " times during the scrap.");
                    //    }
                    //}

                    // Set up a comparison based on how many deaths and what current health a Jedi Knight has to determine who won overall
                    // Reference, http://stackoverflow.com/questions/230588/problem-sorting-lists-using-delegates:   
                    // myList.Sort( delegate (MyType t1, MyType t2) { return (t1.ID.CompareTo(t2.ID)); } );

                    myJedis.Sort(delegate (JediKnight j1, JediKnight j2) { return (j1.NumOfDeaths.CompareTo(j2.NumOfDeaths)); });

                    // print out the guy who died the least amount of times
                    gameLog.FightEvents.Add("<p><div><ul>");
                    gameLog.FightEvents.Add("<li>" + "The honorable Jedi Knight " + myJedis[0].Name + " is the victor of the game, with " + myJedis[0].NumOfDeaths + " death experiences!" + "</li>");

                    List<JediKnight> myHealthVictorList = myJedis;
                    // take out the victor, his health ain't so interesting.
                    myHealthVictorList.Remove(myJedis[0]);
                    // re-order the list according to current health level, and then reverse it to print out a descending list of jedi dudes based on current health.
                    myHealthVictorList.Sort(delegate (JediKnight j1, JediKnight j2) { return (j1.CurrentDamageLevel.CompareTo(j2.CurrentDamageLevel)); });
                    myHealthVictorList.Reverse();
                    foreach (JediKnight warrior in myHealthVictorList)
                    {
                        if (!warrior.Deceased)
                        {
                            string myIntro = ((bool)warrior.DarkSide ? "The Sith Lord " : "The honorable Jedi Knight ");
                            gameLog.FightEvents.Add("<li>" + myIntro + warrior.Name + " has survived the last game iteration with a rude health of " + warrior.CurrentDamageLevel + "."
                                + ". He has visited the earthworms " + warrior.NumOfDeaths + " times during the scrap." + "</li>");
                        }
                        if (warrior.Deceased)
                            gameLog.FightEvents.Add("<li>" + "Awww, " + warrior.Name + " is sleeping with the fishes, and so has a lousy current health certificate! He has visited the salty grave "
                                + warrior.NumOfDeaths + " times during the scrap." + "</li>");
                    }
                    gameLog.FightEvents.Add("</ul></div></p>");

                    // Set up viewbag list of event strings
                    ViewBag.FightDescription = new List<string> { "<p><div class='lead'>" + "A long time ago in a galaxy far, far away...." + "</div></p>" };

                    // put the game log contents into the ViewBag
                    foreach (string FightEvent in gameLog.FightEvents)
                    {
                        ViewBag.FightDescription.Add(FightEvent);
                    }

                    // put the game log contents into a long html string and stuff it into tempdata
                    string htmlPageWithDiv = "<!DOCTYPE html><html><head><title>Jedi_Vs_Sith</title></head><body><div id=battledata><ul>";
                    foreach (string FightEvent in gameLog.FightEvents)
                    {
                        // htmlPageWithDiv = htmlPageWithDiv + "<li>" + FightEvent + "</li>";
                        htmlPageWithDiv = htmlPageWithDiv + FightEvent; // li elements are already added
                    }
                    htmlPageWithDiv = htmlPageWithDiv + "</ul></div></body></html>";

                    TempData.Add("FightEvents", htmlPageWithDiv);

                    return View("Jedi_Vs_Sith", myWarrior);
                }
                catch (Exception e)
                {
                    // log the exception msg to a text file or db
                    string msg = e.Message;
                    ErrorLogger.SaveMsg(msg);
                    return View();
                }
            }
            else
            {
                // Request: log hvilket field der giver en valideringsfejl
                // se https://laptrinhx.com/how-to-get-all-errors-from-asp-net-mvc-modelstate-252836070/
                // og https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.modelstate.errors?view=aspnet-mvc-5.2
                var modelStateErrors = GetErrorListFromModelState(ModelState);
                if (modelStateErrors.Count > 0)
                {
                    foreach (string errMsg in modelStateErrors)
                    {
                        ErrorLogger.SaveMsg("Error logged from RecruitForm else clause: " + errMsg);
                    }
                }

                // validation error - redisplay form with error messages
                return View();
            }

        }

        private List<string> ListOutput(List<string> input)
        {
            var output = new List<string>();
            foreach (string x in input)
            {
                output.Add("<li>" + x + "</li>");
            }
            return output;
        }

        private void HtmlEncodeFightEvents(AttackRecorder gameLog, JediKnight warrior)
        {
            // Add a picture of our jedi knight or sith lord as a div to the output stream
            StringBuilder builder = new StringBuilder();
            builder.Append("<div class='row'>");
            builder.Append("<div class='column1'><img src='" + warrior.ImageUrl + "'></div>");
            builder.Append("<div class='column2'>");
            // add player fight events to game log in sequence, as an unordered list
            builder.Append("<ul>");
            foreach (string x in warrior.FightLog.FightEvents)
            {
                // if(warrior.Name == "Luke Skywalker")
                if (x.Contains("Luke"))
                {
                    builder.Append("<li class='luke'>" + x + "</li>");
                }
                else
                {
                    builder.Append("<li>" + x + "</li>");
                }
            }
            builder.Append("</ul>");
            builder.Append("</div>");
            builder.Append("</div>"); // last div closes bootstrap row            

            gameLog.FightEvents.Add(builder.ToString());
        }

        // The whole model can't be passed via an actionlink, only simple strings. 
        // We can pass an id for a record in a db, and rehydrate model from db afterwards - or just pass in the player's email address input.
        public IActionResult EmailBattleData(string playerEmail, string playerName)
        {
            EmailSender mySender = new EmailSender();

            if (playerEmail == null)
            {
                playerEmail = "finnvs@gmail.com";
                // playerEmail = TempData["PlayerEmail"].ToString();
            }
            // Async
            // Task<string> success = mySender.SendEmail(playerEmail, "Fight Log for " + playerName, (string)TempData["FightEvents"]);
            // Synkront - tillader tilbagemelding til bruger onscreen
            string success = mySender.Email(playerEmail, "Fight Log Transcript for " + playerName, (string)TempData["FightEvents"]);
            TempData.Add("SuccessMsg", success);
            // display result of email operation as an html string taken from TempData
            return View("MailSuccess");
            // TODO: check if it's better to render a partial view instead here?            
        }

        // To test if view behaves as expected, by calling /TestLayout
        public IActionResult TestLayout()
        {
            return View("MailSuccess");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static List<string> GetErrorListFromModelState(ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }
    }
}
