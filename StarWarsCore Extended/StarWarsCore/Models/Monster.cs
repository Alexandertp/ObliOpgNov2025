namespace StarWarsCore.Models;

public class Monster : Creature
{
    public Monster()
    {
        Weaknesses.Add("Angel Blade");
        Weaknesses.Add("Colt");
    }
    public List<string> Weaknesses =  new List<string>();

    public void Fight(Monster attacker, Hunter defender)
    {
        base.Fight(attacker, defender, 1);
    }
}