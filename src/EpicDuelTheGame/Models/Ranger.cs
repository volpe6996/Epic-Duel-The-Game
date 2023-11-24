using ConsoleTextGame.Abstract;
using System;

namespace EpicDuelTheGame.Models;

public class Ranger : Hero, IManipulateStats
{
    public int Dodge { get; set; }
    
    public Ranger(string name)
    {
        Init(name, 35, 30, 40, 35, 20);
    }

    private void Init(string name, int hp, int strength, int dexterity, int intelligence, int dodge)
    {
        this.Name = name;
        this.Hp = hp;
        this.Strength = strength;
        this.Dexterity = dexterity;
        this.Intelligence = intelligence;
        this.Dodge = dodge;
    }

    public void UpHp()
    {
        throw new NotImplementedException();
    }

    public void UpStrength()
    {
        throw new NotImplementedException();
    }

    public void UpDexterity()
    {
        throw new NotImplementedException();
    }

    public void UpIntelligence()
    {
        throw new NotImplementedException();
    }

    public void UpDodge()
    {
        
    }
}