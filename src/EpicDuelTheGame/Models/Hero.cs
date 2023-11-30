using EpicDuelTheGame.Abstract;
using System;

namespace EpicDuelTheGame.Models;

public abstract class Hero : ClassBase, IHero, ILogger, IRandomAuthtorization
{
    public string? Name { get; set; }

    private int _hp;
    public int Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            OnPropertyChanged(nameof(Hp));
        }
    }

    private int _strength;
    public int Strength
    {
        get { return _strength; }
        set
        {
            _strength = value;
            OnPropertyChanged(nameof(Strength));
        }
    }

    private int _dexterity;
    public int Dexterity
    {
        get { return _dexterity; }
        set
        {
            _dexterity = value;
            OnPropertyChanged(nameof(Dexterity));
        }
    }

    private int _intelligence;
    public int Intelligence
    {
        get { return _intelligence; }
        set
        {
            _intelligence = value;
            OnPropertyChanged(nameof(Intelligence));
        }
    }

    private int _mana;
    public int Mana
    {
        get { return _mana; }
        set
        {
            _mana = value;
            OnPropertyChanged(nameof(Mana));
        }
    }

    // warrior
    private int? _counterattack;
    public int? Counterattack
    {
        get { return _counterattack; }
        set
        {
            _counterattack = value;
            OnPropertyChanged(nameof(Counterattack));
        }
    }

    // sorcerer
    private int? _healing;
    public int? Healing
    {
        get { return _healing; }
        set
        {
            _healing = value;
            OnPropertyChanged(nameof(Healing));
        }
    }

    // ranger
    private int? _dodge;
    public int? Dodge
    {
        get { return _dodge; }
        set
        {
            _dodge = value;
            OnPropertyChanged(nameof(Dodge));
        }
    }

    public event Action<string> LogEvent;

    public Hero(string name)
    {
        this.Name = name;
    }

    protected virtual void Init(int hp, int strength, int dexterity, int intelligence, int mana)
    {
        this.Hp = hp;
        this.Strength = strength;
        this.Dexterity = dexterity;
        this.Intelligence = intelligence;
        this.Mana = mana;
    }

    public virtual void UseUltimate(Hero opponent) { }

    //public virtual int WeakDamage(Hero opponent)
    //{
    //    Mana += 8;

    //    // wysokosc zadanych obrazen = zadane obrazenia-zniejszone obrazenia 
    //    var weakDamage = (int)Math.Ceiling((decimal)(Strength / 3 + Intelligence / 9)) - (int)Math.Ceiling((decimal)Dexterity / 8);
    //    Log($"{this.Name} zdal {weakDamage} obrazen {opponent.Name}owi!(+8 many)");

    //    return weakDamage;
    //}

    public virtual void WeakDamage(Hero opponent)
    {
        Mana += 8;

        // wysokosc zadanych obrazen = zadane obrazenia-zniejszone obrazenia 
        var weakDamage = (int)Math.Ceiling((decimal)(Strength / 3 + Intelligence / 9)) - (int)Math.Ceiling((decimal)Dexterity / 8);

        opponent.Hp -= weakDamage;

        Log($"{this.Name} zdal {weakDamage} obrazen {opponent.Name}owi!(+8 many)");
    }

    //public virtual int StrongDamage(Hero opponent)
    //{
    //    if (OperationAuthtorization(Strength) && Mana >= 20)
    //    {
    //        Mana -= 20;
    //        var strongDamage = (int)Math.Ceiling((decimal)(Strength / 2 + Intelligence / 4));
    //        Log($"{this.Name} zdal {strongDamage} obrazen {opponent.Name}owi(-20 many)");

    //        return strongDamage;
    //    }
    //    else if(OperationAuthtorization(Strength) && Mana < 20)
    //    {
    //        Log($"Atak siê nie powiód³, za ma³o many!");
    //        return 0;
    //    }
    //    else
    //    {
    //        Log($"Mocny atak siê nie powiód³!");
    //        return 0;
    //    }
    //}

    public virtual void StrongDamage(Hero opponent)
    {
        if(Mana < 20)
            Log($"Atak siê nie powiód³, za ma³o many!");
        else if(OperationAuthtorization(Strength))
        {
            Mana -= 20;
            var strongDamage = (int)Math.Ceiling((decimal)(Strength / 2 + Intelligence / 4));

            opponent.Hp -= strongDamage;

            Log($"{this.Name} zdal {strongDamage} obrazen {opponent.Name}owi(-20 many)");
        }
        else
            Log($"Mocny atak siê nie powiód³!");
    }

    public virtual void UpIntelligence()
    {
        if(Mana >= 50)
        {
            Mana -= 50;

            Intelligence += 5;
            Strength += 5;
            Dexterity += 5;
            Hp += 40;

            Log("Zwiêkszono o 5: inteligencjê, si³ê, zrêcznoœæ. (+40 HP, -50 MANA)");
        }
        else
            Log("Za ma³o many!");
    }

    public void Log(string message)
    {
        LogEvent?.Invoke(message);
    }

    public bool OperationAuthtorization(int factor)
    {
        var rng = new Random();
        var upperRange = Intelligence + factor * 3;
        var rngNbr = rng.Next(0, 700);

        if (rngNbr > 0 && rngNbr < upperRange)
            return true;
        else return false;
    }
}