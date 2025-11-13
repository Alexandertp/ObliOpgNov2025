using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StarWarsCore.Models;
using StarWarsCore.Helpers;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.InteropServices;

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
                    gameLog.FightEvents = new List<string>();

                    // Set up a Jedi Warrior object - using the data entered in the form
                    ThePlayer myWarrior = new ThePlayer();
                    myWarrior.Name = newWarrior.Name;
                    myWarrior.Email = newWarrior.Email;
                    myWarrior.LightSaberColor = newWarrior.LightSaberColor;
                    myWarrior.CurrentDamageLevel = JediKnight.DamageLevel.Healthy;
                    myWarrior.DarkSide = newWarrior.DarkSide;
                    myWarrior.Deceased = false;

                    List <Creature> hunters = new List <Creature>();
                    List <Creature> monsters = new List <Creature>();
                    Sam sam = new Sam();
                    Dean dean = new Dean();
                    Castiel castiel = new Castiel();
                    hunters.Add(sam);
                    hunters.Add(dean);
                    hunters.Add(castiel);

                    Vampire vampBob = new Vampire("Vampire Bob");
                    Ghost ghost = new Ghost("Ghost Magrethe");
                    Demon demonAsmon = new Demon("Demon Asmongold");
                    monsters.Add(vampBob);
                    monsters.Add(ghost);
                    monsters.Add(demonAsmon);

                    castiel.currentWeapon = castiel.Weapons[RandomGenerator.Rand.Next(0, castiel.Weapons.Count)];
                    dean.currentWeapon = dean.Weapons[RandomGenerator.Rand.Next(0, dean.Weapons.Count)];
                    sam.currentWeapon = sam.Weapons[RandomGenerator.Rand.Next(0, sam.Weapons.Count)];

                    gameLog.FightEvents.Add(castiel.Name + " retrieves his trusty " + castiel.currentWeapon + "</br>");
                    gameLog.FightEvents.Add(dean.Name + " opens his trunk and gets out his " + dean.currentWeapon + "</br>");
                    gameLog.FightEvents.Add(sam.Name + " finds a random " + sam.currentWeapon + " on the ground." + "</br>");



                    // CombatLoop der kører indtil enten alle hunters eller monsters er døde
                    while (hunters.Any(x => x.isDead == false) && monsters.Any(x => x.isDead == false))
                    {
                        gameLog.FightEvents.Add(FightRound(hunters, monsters));
                        gameLog.FightEvents.Add(FightRound(monsters, hunters));
                    }
                    gameLog.FightEvents.Add("</br>");
                    gameLog.FightEvents.Add("<hr>");
                    gameLog.FightEvents.Add("*******RESULT OF FIGHT******* </br>");
                    gameLog.FightEvents.Add(EndFightResult(hunters, monsters));
                    

                    // Set up viewbag list of event strings
                    ViewBag.FightDescription = new List<string> { "<p><div class='lead'>" + "" + "</div></p>" };

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

        private string FightRound(List <Creature> attacker, List <Creature> defender) 
        {
            string roundResult = "";
            int initiative = 0;
            do
            {
                initiative = RandomGenerator.Rand.Next(attacker.Count);
            } while (attacker[initiative].isDead);
            int offer = 0;
            do
            {
                offer = RandomGenerator.Rand.Next(defender.Count);
            } while (defender[offer].isDead);
            // hunters angriber først
            defender[initiative].Fight(attacker[initiative], defender[offer]);
            // Tilføj en actionComment fra den samme hunter som lige har angrebet og tilføj den til gameLog
            roundResult += attacker[initiative].ActionComment.FightEvents[RandomGenerator.Rand.Next(attacker[initiative].ActionComment.FightEvents.Count)];
            roundResult += "</br>"; // Tilføj linjeskift
            if (attacker[initiative] is Hunter hunter)
            {
                roundResult += hunter.Name + " hits " + defender[offer].Name + " with his " + hunter.currentWeapon;

            }
            else
            {
                roundResult += attacker[initiative].Name + " uses their supernatural strength to send an attack towards " + defender[offer].Name;
                
            }
            if (defender[offer].isDead)
            {
                attacker[initiative].killCount++;
                defender[offer].killedByName = attacker[initiative].Name;
            }
            roundResult += "</br>" + "<img src = " + attacker[initiative].imageURL + ">" + "</img>";
            roundResult += "</br>";
            roundResult += defender[offer].Name + " is " + defender[offer].CurrentDamageLevel.ToString();
            roundResult += "</br>";

            Castiel castiel = defender.OfType<Castiel>().FirstOrDefault(castiel => !castiel.isDead);

            if (castiel != null && 
                defender[offer].Name != "Castiel" && 
                (int)defender[offer].CurrentDamageLevel <= 2 &&
                defender[offer] is Hunter woundedHunter)
            {
                castiel.SaveABrother(woundedHunter);
                roundResult += castiel.Name + " you can't die yet " + woundedHunter.Name + "</br>";
                
                // Tilføj en GIF
                roundResult += "<img src='/images/Castiel SaveABrother.gif' alt='Castiel saves a hunter!' style='width:450px;height:auto;'></br>";
            }
            return roundResult;
        }
        public string EndFightResult(List <Creature> hunters, List <Creature> monsters)
        {
            string result = "";
            foreach (Creature hunter in hunters)
            {
                result += hunter.Name + " has killed " + hunter.killCount + " monsters." + "</br>"; 
                if (hunter.killedByName != null)
                {
                result += hunter.Name + " was killed by " + hunter.killedByName + "</br>";
                }
                else
                {
                    result += hunter.Name + " managed to stay alive. </br>";
                }
            }
            result += "<hr>";
            foreach (Creature monster in monsters)
            {
                result += monster.Name + " has killed " + monster.killCount + " hunters. " + "</br>";
                if (monster.killedByName != null)
                {
                result += monster.Name + " was killed by " + monster.killedByName + "</br>";
                }
                else
                {
                    result += monster.Name + " managed to stay alive. </br>";
                }
            }
            return result;
        }
    }
}

