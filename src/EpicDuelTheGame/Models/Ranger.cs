using System;

namespace EpicDuelTheGame.Models;

public class Ranger : Hero
{
    private int _dodge;
    public int Dodge
    {
        get { return _dodge; }
        set
        {
            _dodge = value;
            OnPropertyChanged(nameof(Dodge));
        }
    }
    
    public Ranger(string name, string description, string path) : base(name, description, path)
    {
        this.Name = name == "" ? "Ranger" : name;
        Init(35, 30, 40, 35, 50, 20);
    }

    protected override void Init(int hp, int strength, int dexterity, int intelligence, int mana)
    {
        base.Init(hp, strength, dexterity, intelligence, mana);
    }

    protected void Init(int hp, int strength, int dexterity, int intelligence, int mana, int dodge)
    {
        Dodge = dodge;
        this.Init(hp, strength, dexterity, intelligence, mana);
    }
}