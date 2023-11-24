using ConsoleTextGame.Abstract;
using System;

namespace EpicDuelTheGame.Models;

public class Sorcerer : Hero, IManipulateStats
{
    public int Healing { get; set; }
    
    public Sorcerer(string name)
    {
        Init(name, 30, 40, 30, 30, 15);
    }

    private void Init(string name, int hp, int strength, int dexterity, int intelligence, int healing)
    {
        this.Name = name;
        this.Hp = hp;
        this.Strength = strength;
        this.Dexterity = dexterity;
        this.Intelligence = intelligence;
        this.Healing = healing;
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

    public void UpHealing()
    {
        
    }
}