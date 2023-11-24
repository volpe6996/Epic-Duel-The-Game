namespace EpicDuelTheGame.Models;

public abstract class Hero
{
    public string Name { get; protected set; }
    public int Hp { get; set; }          
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Intelligence { get; set; }

    public void Test()
    {
        
    }
}