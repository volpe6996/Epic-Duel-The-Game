using System;

namespace EpicDuelTheGame.Models;

public class Warrior : Hero
{
    private int _defence;
    public int Defence
    {
        get { return _defence; }
        set
        {
            _defence = value;
            OnPropertyChanged(nameof(Defence));
        }
    }

    public Warrior(string name, string description, string path) : base(name, description, path)
    {
        this.Name = name == "" ? "Warrior" : name;
        Init(50, 35, 25, 20, 50, 40);
    }

    protected override void Init(int hp, int strength, int dexterity, int intelligence, int mana)
    {
        base.Init(hp, strength, dexterity, intelligence, mana);
    }

    protected void Init(int hp, int strength, int dexterity, int intelligence, int mana, int defence)
    {
        Defence = defence;
        this.Init(hp, strength, dexterity, intelligence, mana);
    }
}