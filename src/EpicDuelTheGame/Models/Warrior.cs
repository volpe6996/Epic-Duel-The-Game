using EpicDuelTheGame.Abstract;
using System;

namespace EpicDuelTheGame.Models;

public class Warrior : Hero, IRandomAuthtorization
{
    public Warrior(string name) : base(name)
    {
        this.Name = name == "" ? "Warrior" : name;
        Init(50, 35, 25, 20, 50, 40);
    }

    protected override void Init(int hp, int strength, int dexterity, int intelligence, int mana)
    {
        base.Init(hp, strength, dexterity, intelligence, mana);
    }

    protected void Init(int hp, int strength, int dexterity, int intelligence, int mana, int counterattack)
    {
        Counterattack = counterattack;
        this.Init(hp, strength, dexterity, intelligence, mana);
    }

    public void SetHp(int setHpValue)
    {
        Hp = setHpValue;
    }

    //public new int UseUltimate(Hero opponent)
    //{
    //    return (int)(Counterattack * Intelligence);
    //}

    //public override int UseUltimate()
    //{
    //    if(OperationAuthtorization((int)Counterattack) && Mana > 50)
    //    {
    //        Mana -= 30;
    //        return (int)Math.Ceiling((decimal)(Strength / 4 + Intelligence / 9));
    //    }

    //    return 0;
    //}

    //public override int WeakDamage(Hero opponent)
    //{
    //    return base.WeakDamage(opponent);
    //}

    public override void WeakDamage(Hero opponent)
    {
        base.WeakDamage(opponent);
    }

    //public override int StrongDamage(Hero opponent)
    //{
    //    if (OperationAuthtorization(Strength))
    //        return (int)Math.Ceiling((decimal)(Strength / 2 + Intelligence / 4));

    //    return 0;
    //}

    //public override int StrongDamage(Hero opponent)
    //{
    //    return base.StrongDamage(opponent);
    //}

    public override void StrongDamage(Hero opponent)
    {
        base.StrongDamage(opponent);
    }

    //public bool OperationAuthtorization(int factor)
    //{
    //    var rng = new Random();
    //    var operationChance = Intelligence + factor * 3;
    //    var rngNbr = rng.Next(0, 1000);

    //    if (rngNbr > 0 && rngNbr < operationChance)
    //        return true;
    //    else return false;
    //}
}