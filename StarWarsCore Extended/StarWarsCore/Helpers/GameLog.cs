using StarWarsCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsCore.Helpers
{
    public class GameLog : IObserver<JediKnight>
    {
        private IDisposable unsubscriber;
        private string instName;
        public List<String> FightEvents { get; set; }

        public enum OutputMode
        {
            UnorderedList = 0,
            BootstrapRow = 1
        }

        public OutputMode currentOutputMode { get; set; } = OutputMode.UnorderedList;

        public GameLog(string name)
        {
            instName = name;
            FightEvents = new List<String>();            
        }

        public string Name
        { get { return instName; } }

        public virtual void Subscribe(IObservable<JediKnight> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void OnCompleted()
        {
            FightEvents.Add("The Jedi Tracker has now completed transmitting data to " + Name + ". Thus endeth thys tragic tale. Or something..");            
            Unsubscribe();
        }

        public virtual void OnError(Exception e)
        {
            this.FightEvents.Add(this.Name + ": The jedi or sith dude cannot be determined.");
        }

        public virtual void OnNext(JediKnight jedi)
        {
            switch (currentOutputMode)
            {
                case OutputMode.UnorderedList:
                    // jedi.FightLog.FightEvents.Add(jedi.Name + " senses that something is up, and mumbles to himself: " + jedi.SuspiciousWords);
                    FightEvents.AddRange(ListOutput(jedi.FightLog.FightEvents));
                    break;
                case OutputMode.BootstrapRow:
                    // Test output to screen here to check that this output mode is hit
                    // jedi.FightLog.FightEvents.Add(jedi.Name + ": Hey. You! Stop observin' me or I will fry your brains out!");
                    HtmlEncodeFightEvents(jedi);
                    break;
                default:
                    FightEvents.AddRange(ListOutput(jedi.FightLog.FightEvents));
                    break;
            }
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
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

        private void HtmlEncodeFightEvents(JediKnight warrior)
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
                    // sickly green bgcolor for all output associated with Luke
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

            FightEvents.Add(builder.ToString());
        }
    }
}
