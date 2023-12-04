using System;
using System.Xml.Linq;

namespace EpicDuelTheGame.Models;

public class Sorcerer : Hero
{
    public Sorcerer(string name) : base(name)
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

    public override void WeakDamage(Hero opponent)
    {
        base.WeakDamage(opponent);
    }
}