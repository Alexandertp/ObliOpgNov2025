namespace StarWarsCore.Models;

public class Dean : Hunter
{
    public Dean()
    {
        Name = "Dean";
        CurrentDamageLevel = DamageLevel.Healthy;
        Console.WriteLine((int)CurrentDamageLevel);
        
    }
}