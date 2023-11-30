using System;

namespace EpicDuelTheGame.Models;

public class Ranger : Hero
{
    public Ranger(string name) : base(name)
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

    //public override int WeakDamage(Hero opponent)
    //{
    //    return base.WeakDamage(opponent);
    //}

    public override void WeakDamage(Hero opponent)
    {
        base.WeakDamage(opponent);
    }
}