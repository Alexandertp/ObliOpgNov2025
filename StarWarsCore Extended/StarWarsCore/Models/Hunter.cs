using System.Security.Cryptography.X509Certificates;

namespace StarWarsCore.Models;

public class Hunter : Creature
{
    public Hunter()
    {
        Weapons.Add("Shotgun");
        Weapons.Add("Knife");
        Weapons.Add("Pistol");
        Weapons.Add("Holy Water");
        Weapons.Add("Salt");
        Weapons.Add("Dead Man's Blood");
        Weapons.Add("Machete");
        Weapons.Add("Silver Blade");
        Weapons.Add("Exorcism");
        Weapons.Add("Colt");
    }
    public List<string> Weapons = new List<string>();
}