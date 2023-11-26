using System;
using System.Xml.Linq;

namespace EpicDuelTheGame.Models;

public class Sorcerer : Hero
{
    private int _healing;
    public int Healing
    {
        get { return _healing; }
        set 
        {
            _healing = value;
            OnPropertyChanged(nameof(Healing));
        }
    }
    
    public Sorcerer(string name, string description, string path) : base(name, description, path)
    {
        this.Name = name == "" ? "Sorcerer" : name;
        Init(30, 40, 30, 30, 50, 15);
    }

    protected override void Init(int hp, int strength, int dexterity, int intelligence, int mana)
    {
        base.Init(hp, strength, dexterity, intelligence, mana);
    }

    protected void Init(int hp, int strength, int dexterity, int intelligence, int mana, int healing)
    {
        Healing = healing;
        this.Init(hp, strength, dexterity, intelligence, mana);
    }
}