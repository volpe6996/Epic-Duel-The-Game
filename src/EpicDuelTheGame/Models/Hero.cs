using EpicDuelTheGame.Abstract;
using System;
using System.Threading.Tasks;

namespace EpicDuelTheGame.Models;

public enum HeroTypes
{
    Warrior,
    Sorcerer,
    Ranger
}

public class Hero : ClassBase, ILogger, IRandomAuthtorization
{
    public string? Name { get; set; }

    private int _hp;
    public int Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;

            // on start: MaxHp = 100 but could go higher
            if (value > MaxHp)
                MaxHp = value;

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
    private int _strengthBoost = 0;

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
    private int _dexterityBoost = 0;
    private int _takenDexterity = 0;

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

            // on start: MaxHp = 100 but could go higher
            if (value > MaxMana)
                MaxMana = value;

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

    private int _latestDamageValue;
    public int LatestDamageValue
    {
        get { return _latestDamageValue; }
        set
        {
            if (value >= 0)
                _latestDamageValue = value;

            OnPropertyChanged(nameof(LatestDamageValue));
        }
    }

    private int _maxHp = 100;
    public int MaxHp
    {
        get { return _maxHp; }
        set
        {
            _maxHp = value;
            OnPropertyChanged(nameof(MaxHp));
        }
    }

    private int _maxMana = 100;
    public int MaxMana
    {
        get { return _maxMana; }
        set
        {
            _maxMana = value;
            OnPropertyChanged(nameof(MaxMana));
        }
    }

    private bool _damageRelatedTurn;
    public bool DamageRelatedTurn
    {
        get { return _damageRelatedTurn; }
        set
        {
            _damageRelatedTurn = value;
            OnPropertyChanged(nameof(DamageRelatedTurn));
        }
    }

    public HeroTypes HeroType;

    public bool IsFirstSpellActive = false;
    private int FirstSpellActiveCounter = 0;

    private bool IsSecondSpellActive = false;
    private int SecondSpellActiveCounter = 0;

    private int UsedCriticals = 0;

    public event Action<string> LogEvent;

    public Hero(string name, HeroTypes heroType)
    {
        if (name == string.Empty)
        {
            if (heroType == HeroTypes.Warrior)
                name = "Warrior";
            else if (heroType == HeroTypes.Sorcerer)
                name = "Sorcerer";
            else if (heroType == HeroTypes.Ranger)
                name = "Ranger";
        }

        this.Name = name;
        HeroType = heroType;

        Init();
    }

    private void Init()
    {
        if (HeroType == HeroTypes.Warrior)
        {
            Hp = 50;
            Strength = 40;
            Dexterity = 25;
            Intelligence = 20;
            Mana = 45;
            Counterattack = 50;
        }
        else if (HeroType == HeroTypes.Sorcerer)
        {
            Hp = 35;
            Strength = 35;
            Dexterity = 30;
            Intelligence = 30;
            Mana = 45;
            Healing = 50;
        }
        else if (HeroType == HeroTypes.Ranger)
        {
            Hp = 40;
            Strength = 30;
            Dexterity = 40;
            Intelligence = 35;
            Mana = 50;
            Dodge = 50;
        }
    }

    public bool UpIntelligence()
    {
        if (Mana >= 50)
        {
            Mana -= 50;
            Intelligence += 5;
            Strength += 5;
            Dexterity += 5;
            Hp += 40;

            Log("Zwiêkszono o 5: inteligencjê, si³ê, zrêcznoœæ. (+40 HP, -50 MANA)");

            return true;
        }
        else
        {
            Log("Za ma³o many!");
            return false;
        }

        if (HeroType == HeroTypes.Warrior)
        {

        }
        else if (HeroType == HeroTypes.Sorcerer)
        {

        }
        else if (HeroType == HeroTypes.Ranger)
        {

        }

        DamageRelatedTurn = false;
    }

    public void WeakDamage(Hero opponent)
    {
        Mana += 8;

        // wysokosc zadanych obrazen = zadane obrazenia-zniejszone obrazenia 
        var weakDamage = (int)Math.Ceiling((decimal)(Strength / 3 + Intelligence / 9)) - (int)Math.Ceiling((decimal)opponent.Dexterity / 8);

        LatestDamageValue = weakDamage;
        opponent.Hp -= weakDamage;

        Log($"{this.Name} zdal {weakDamage} obrazen {opponent.Name}owi!(+8 many)");

        if (HeroType == HeroTypes.Warrior)
        {

        }
        else if (HeroType == HeroTypes.Sorcerer)
        {

        }
        else if (HeroType == HeroTypes.Ranger)
        {

        }

        DamageRelatedTurn = true;
    }

    public void StrongDamage(Hero opponent)
    {
        if (HeroType == HeroTypes.Ranger && IsFirstSpellActive && UsedCriticals <= 3 && Mana >= 10)
        {
            Mana -= 10;
            var strongDamage = (int)Math.Ceiling((decimal)(Strength / 2 + Intelligence / 4)) - (int)Math.Ceiling((decimal)opponent.Dexterity / 8);

            opponent.Hp -= strongDamage;
            UsedCriticals++;

            Log($"{this.Name} zdal {strongDamage} obrazen {opponent.Name}owi(-10 many)");
        }
        else
        {
            if (Mana < 20)
                Log($"Atak siê nie powiód³, za ma³o many!");
            else if (OperationAuthtorization(Strength))
            {
                Mana -= 20;
                var strongDamage = (int)Math.Ceiling((decimal)(Strength / 2 + Intelligence / 4)) - (int)Math.Ceiling((decimal)opponent.Dexterity / 8);

                opponent.Hp -= strongDamage;

                Log($"{this.Name} zdal {strongDamage} obrazen {opponent.Name}owi(-20 many)");
            }
            else
                Log($"Mocny atak siê nie powiód³!");
        }

        DamageRelatedTurn = true;
    }

    public void UseUltimate(Hero opponent)
    {
        if (HeroType == HeroTypes.Warrior)
        {
            if (opponent.LatestDamageValue == 0)
                Log("Przeciwnik nie zada³ obra¿eñ!");
            else if (!opponent.DamageRelatedTurn)
                Log($"Nie mo¿esz kontratakowaæ 2 razy z rzêdu!");
            else if (Mana < 45)
                Log("Za ma³o many!");
            else
            {
                var damage = (int)Math.Ceiling(2d * opponent.LatestDamageValue);

                opponent.Hp -= damage;

                LatestDamageValue = damage;
                Mana -= 45;
                Log($"Kontratak! Zadane obrazenia: {damage} (-45 many)");

                opponent.DamageRelatedTurn = false;
                DamageRelatedTurn = true;
            }
        }
        else if (HeroType == HeroTypes.Sorcerer)
        {
            if (Mana < 50)
                Log("Za ma³o many!");
            else
            {
                var upHpValue = (int)Math.Ceiling(0.8 * MaxHp);
                var hpDiffrence = Math.Abs(upHpValue - Hp);

                if (!(Hp > upHpValue))
                {
                    Hp = upHpValue;
                    Mana -= 50;
                    Log($"Zwiêkszono HP o: {hpDiffrence}HP (-50 many)");
                }
                else
                    Log("Obcena wartoœæ HP jest wiêksza od wartoœci zwiêkszenia!");
            }
        }
        else if (HeroType == HeroTypes.Ranger)
        {
            if (!opponent.DamageRelatedTurn)
                Log("Przeciwnik nie wykona³ ruchu, nie zada³ obra¿eñ!");
            else if (Mana < 35)
                Log("Za ma³o many!");
            else
            {
                var fullDodgeAndSmallHeal = opponent.LatestDamageValue + (int)Math.Ceiling(0.3 * MaxHp);

                Hp += fullDodgeAndSmallHeal;
                Mana -= 35;
                Log($"Pe³ny unik! Odzyskano i zregenerowano {fullDodgeAndSmallHeal}HP (-35 many)");
            }
        }
    }

    public bool UseFirstSpell(Hero opponent)
    {
        if (!IsSecondSpellActive)
        {
            if (HeroType == HeroTypes.Warrior)
            {
                if (Mana >= 25)
                {
                    if (IsFirstSpellActive)
                    {
                        FirstSpellActiveCounter = 0;
                        Mana -= 25;

                        Log("Odnowiono pierwsze zaklêcie!");
                    }
                    else
                    {
                        _strengthBoost = (int)Math.Ceiling(Strength * 0.4);
                        _dexterityBoost = (int)Math.Ceiling(Dexterity * 0.4);

                        Strength += _strengthBoost;
                        Dexterity += _dexterityBoost;

                        Mana -= 25;
                        IsFirstSpellActive = true;

                        Log("Aktywowano pierwsze zaklêcie!");
                    }

                    return true;
                }
                else
                {
                    Log("Za ma³o many!");
                    return false;
                }
            }
            else if (HeroType == HeroTypes.Sorcerer)
            {
                if (Mana >= 40)
                {
                    var hpToTakeFromOpponent = (int)Math.Ceiling(opponent.Hp * 0.35);
                    opponent.Hp -= hpToTakeFromOpponent;
                    Hp += hpToTakeFromOpponent;

                    Mana -= 40;

                    Log($"Aktywowano pierwsze zaklêcie! Zabrano przeciwnikowi {hpToTakeFromOpponent} HP!");
                    return true;
                }
                else
                {
                    Log("Za ma³o many!");
                    return false;
                }
            }
            else if (HeroType == HeroTypes.Ranger) // 3 gwarantowane criticale, -10many/atak
            {
                if (Mana >= 30)
                {
                    if (IsFirstSpellActive)
                    {
                        UsedCriticals = 0;
                        Mana -= 30;

                        Log("Odnowiono pierwsze zaklêcie!");
                    }
                    else
                    {
                        Mana -= 30;
                        IsFirstSpellActive = true;

                        Log("Aktywowano pierwsze zaklêcie! Masz gwarantowane 3 criticale!");
                    }
                    return true;
                }
                else
                {
                    Log("Za ma³o many!");
                    return false;
                }
            }
        }
        else
        {
            Log("Nie mo¿esz stacowaæ speli!");
            return false;
        }

        return false;
    }

    public bool UseSecondSpell(Hero opponent)
    {
        if (!IsFirstSpellActive)
        {
            if (HeroType == HeroTypes.Warrior)
            {
                if (Mana >= 15)
                {
                    if (IsSecondSpellActive)
                    {
                        SecondSpellActiveCounter = 0;
                        Mana -= 15;

                        Log("Odnowiono drugie zaklêcie!");
                    }
                    else
                    {
                        _dexterityBoost = (int)Math.Ceiling(Dexterity * 0.6);

                        Dexterity += _dexterityBoost;

                        Mana -= 15;
                        IsSecondSpellActive = true;

                        Log("Aktywowano drugie zaklêcie!");
                    }
                    
                    return true;
                }
                else
                {
                    Log("Za ma³o many!");
                    return false;
                }
            }
            else if (HeroType == HeroTypes.Sorcerer)
            {
                if (Mana >= 30)
                {
                    var manaToTakeFromOpponent = (int)Math.Ceiling(opponent.Hp * 0.25);
                    opponent.Mana -= manaToTakeFromOpponent;

                    Mana -= 30;

                    Log($"Aktywowano drugie zaklêcie! Zabrano przeciwnikowi {manaToTakeFromOpponent} many!");

                    return true;
                }
                else
                {
                    Log("Za ma³o many!");
                    return false;
                }
            }
            else if (HeroType == HeroTypes.Ranger)
            {
                if (Mana >= 35)
                {
                    if (IsSecondSpellActive)
                    {
                        SecondSpellActiveCounter = 0;
                        Mana -= 35;

                        Log("Odnowiono drugie zaklêcie!");
                    }
                    else
                    {
                        _takenDexterity = opponent.Dexterity;
                        opponent.Dexterity = 0;

                        Mana -= 35;
                        IsSecondSpellActive = true;

                        Log($"Aktywowano drugie zaklêcie! Zabrano przeciwnikowi ca³¹ zwinnoœæ!");
                    }
                    return true;
                }
                else
                {
                    Log("Za ma³o many!");
                    return false;
                }
            }
        }
        else
        {
            Log("Nie mo¿esz stacowaæ speli!");
            return false;
        }

        return false;
    }

    public async Task CheckSpellStatus(Hero opponent)
    {
        if (HeroType == HeroTypes.Warrior)
        {
            if (IsFirstSpellActive)
            {
                if (FirstSpellActiveCounter != 3)
                    FirstSpellActiveCounter++;
                else if (FirstSpellActiveCounter == 3)
                {
                    Strength -= _strengthBoost;
                    Dexterity -= _dexterityBoost;

                    _strengthBoost = 0;
                    _dexterityBoost = 0;

                    IsFirstSpellActive = false;
                    FirstSpellActiveCounter = 0;

                    Log("Pierwsze zaklêcie wygas³o!");
                    await Task.Delay(1500);
                }
            }

            if (IsSecondSpellActive)
            {
                if (SecondSpellActiveCounter != 2)
                    SecondSpellActiveCounter++;
                else if (SecondSpellActiveCounter == 2)
                {
                    Dexterity -= _dexterityBoost;

                    _dexterityBoost = 0;

                    IsSecondSpellActive = false;
                    SecondSpellActiveCounter = 0;

                    Log("Drugie zaklêcie wygas³o!");
                    await Task.Delay(1500);
                }
            }
        }
        else if (HeroType == HeroTypes.Ranger)
        {
            if (IsFirstSpellActive && UsedCriticals == 3)
            {
                IsFirstSpellActive = false;
                UsedCriticals = 0;

                Log("Pierwsze zaklêcie wygas³o!");
                await Task.Delay(1500);
            }

            if (IsSecondSpellActive)
            {
                if (SecondSpellActiveCounter != 2)
                    SecondSpellActiveCounter++;
                else if (SecondSpellActiveCounter == 2)
                {
                    opponent.Dexterity += _takenDexterity;
                    _takenDexterity = 0;

                    IsSecondSpellActive = false;
                    SecondSpellActiveCounter = 0;

                    Log("Drugie zaklêcie wygas³o!");
                    await Task.Delay(1500);
                }
            }
        }
    }

    public void Log(string message)
    {
        LogEvent?.Invoke(message);
    }

    public void ClearLog()
    {
        Log("");
    }

    public bool OperationAuthtorization(int factor)
    {
        var rng = new Random();
        var upperRange = Intelligence + factor * 3;
        var rngNbr = rng.Next(0, 450);

        return rngNbr > 0 && rngNbr < upperRange;
    }
}