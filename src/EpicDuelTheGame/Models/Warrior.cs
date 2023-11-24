using ConsoleTextGame.Abstract;
using System;

namespace EpicDuelTheGame.Models;

public class Warrior : Hero, IManipulateStats
{
    public int Defence { get; set; }

    public Warrior(string name)
    {
        Init(name, 50, 35, 25, 20, 40);
    }

    private void Init(string name, int hp, int strength, int dexterity, int intelligence, int defence)
    {
        this.Name = name;
        this.Hp = hp;
        this.Strength = strength;
        this.Dexterity = dexterity;
        this.Intelligence = intelligence;
        this.Defence = defence;
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

    public void UpDefence()
    {
        
    }
}