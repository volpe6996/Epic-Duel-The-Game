using EpicDuelTheGame.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EpicDuelTheGame.Models;

public enum HeroType
{
    Warrior,
    Sorcerer,
    Ranger
}

public enum PlayerType
{
    User,
    Ai
}

public class Hero : ClassBase, ILogger, IRandomAuthtorization
{
    public string? Name { get; set; }
    public PlayerType PlayerType { get; set; }

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

    private bool _damageRelatedTurn = false;
    public bool DamageRelatedTurn
    {
        get { return _damageRelatedTurn; }
        set
        {
            _damageRelatedTurn = value;
            OnPropertyChanged(nameof(DamageRelatedTurn));
        }
    }

    private HeroType _heroType;
    public HeroType HeroType
    {
        get { return _heroType; }
        set
        {
            _heroType = value;
            OnPropertyChanged(nameof(HeroType));
        }
    }

    private bool _isFirstSpellActive = false;
    public bool IsFirstSpellActive
    {
        get { return _isFirstSpellActive; }
        set
        {
            _isFirstSpellActive = value;
            OnPropertyChanged(nameof(IsFirstSpellActive));
        }
    }
    private int FirstSpellActiveCounter = 0;

    private bool _isSecondSpellActive = false;
    public bool IsSecondSpellActive
    {
        get { return _isSecondSpellActive; }
        set
        {
            _isSecondSpellActive = value;
            OnPropertyChanged(nameof(IsSecondSpellActive));
        }
    }
    private int SecondSpellActiveCounter = 0;

    private int UsedCriticals = 0;

    public event Action<string> LogEvent;
    public event Action<bool> OnFirstSpell;
    public event Action<bool> OnSecondSpell;

    public Hero(string name, HeroType heroType, PlayerType playerType)
    {
        List<string> listaImion = new List<string>
        {
            "Aragorn",
            "Legolas",
            "Gandalf",
            "Frodo",
            "Arwen",
            "Elrond",
            "Gimli",
            "Galadriel",
            "Eldamar",
            "Glorfindel",
            "Lúthien",
            "Eärendil",
            "Finrod",
            "Amdir",
            "Arwen",
            "Aricane",
            "Elowen",
            "Thalion",
            "Elysium",
            "Draven",
            "Seraphina",
            "Calarian",
            "Lirael",
            "Zephyros",
            "Evadne",
            "Galadriel",
            "Kaelith",
            "Selenea",
            "Alarion",
            "Aerendir",
            "Mirrindor",
            "Sylvaris",
            "Lyrastra",
            "Vaelin",
            "Isolde",
            "Erevan",
            "Valorian",
            "Maelis",
            "Aeliana",
            "Eirian",
            "Faldrin",
            "Elaria",
            "Orionis",
            "Morwen",
            "Thessalia"
        };

        if (name == string.Empty)
        {
            if (heroType == HeroType.Warrior)
                name = listaImion[new Random().Next(listaImion.Count)];
            else if (heroType == HeroType.Sorcerer)
                name = listaImion[new Random().Next(listaImion.Count)];
            else if (heroType == HeroType.Ranger)
                name = listaImion[new Random().Next(listaImion.Count)];

            listaImion.Remove(name);
        }

        this.Name = name;
        HeroType = heroType;
        PlayerType = playerType;

        Init();
    }

    private void Init()
    {
        if (HeroType == HeroType.Warrior)
        {
            Hp = Globals.WARRIOR_START_HP;
            Strength = Globals.WARRIOR_START_STRENGTH;
            Dexterity = Globals.WARRIOR_START_DEXTERITY;
            Intelligence = Globals.WARRIOR_START_INTELLIGENCE;
            Mana = Globals.WARRIOR_START_MANA;
        }
        else if (HeroType == HeroType.Sorcerer)
        {   
            Hp = Globals.SORCERER_START_HP;
            Strength = Globals.SORCERER_START_STRENGTH;
            Dexterity = Globals.SORCERER_START_DEXTERITY;
            Intelligence = Globals.SORCERER_START_INTELLIGENCE;
            Mana = Globals.SORCERER_START_MANA;
        }
        else if (HeroType == HeroType.Ranger)
        {
            Hp = Globals.RANGER_START_HP;
            Strength = Globals.RANGER_START_STRENGTH;
            Dexterity = Globals.RANGER_START_DEXTERITY;
            Intelligence = Globals.RANGER_START_INTELLIGENCE;
            Mana = Globals.RANGER_START_MANA;
        }
    }

    public bool UpIntelligence()
    {
        if (HeroType == HeroType.Warrior && Mana >= Globals.WARRIOR_UP_INTELLIGENCE_MANA_REQ)
        {
            Mana -= Globals.WARRIOR_UP_INTELLIGENCE_MANA_REQ;
            Intelligence += 5;
            Strength += 3;
            Dexterity += 5;
            Hp += 40;

            Log($"Zwiêkszono o 5: inteligencjê, si³ê, zrêcznoœæ. (+35 HP, -{Globals.WARRIOR_UP_INTELLIGENCE_MANA_REQ} MANY)");

            DamageRelatedTurn = false;

            return true;
        }
        else if (HeroType == HeroType.Sorcerer && Mana >= Globals.SORCERER_UP_INTELLIGENCE_MANA_REQ)
        {
            Mana -= Globals.SORCERER_UP_INTELLIGENCE_MANA_REQ;
            Intelligence += 4;
            Strength += 5;
            Dexterity += 5;
            Hp += 40;

            Log($"Zwiêkszono o 5: inteligencjê, si³ê, zrêcznoœæ. (+40 HP, -{Globals.SORCERER_UP_INTELLIGENCE_MANA_REQ} MANY)");

            DamageRelatedTurn = false;

            return true;
        }
        else if (HeroType == HeroType.Ranger && Mana >= Globals.RANGER_UP_INTELLIGENCE_MANA_REQ)
        {
            Mana -= Globals.RANGER_UP_INTELLIGENCE_MANA_REQ;
            Intelligence += 4;
            Strength += 4;
            Dexterity += 8;
            Hp += 40;

            Log($"Zwiêkszono o 5: inteligencjê, si³ê, zrêcznoœæ. (+40 HP, -{Globals.RANGER_UP_INTELLIGENCE_MANA_REQ} MANY)");

            DamageRelatedTurn = false;

            return true;
        }
        else
        {
            Log("Za ma³o many!");
            return false;
        }

    }

    public void WeakDamage(Hero opponent)
    {
        Mana += Globals.MANA_REWARD;

        // wysokosc zadanych obrazen = zadane obrazenia-zniejszone obrazenia 
        var weakDamage = (int)Math.Ceiling((decimal)(Strength / 5 + Intelligence / 10)) - (int)Math.Ceiling((decimal)opponent.Dexterity / 10);

        LatestDamageValue = weakDamage;
        opponent.Hp -= weakDamage;

        Log($"{this.Name} zada³ {weakDamage} obra¿eñ {opponent.Name}owi!(+{Globals.MANA_REWARD} many)");

        DamageRelatedTurn = true;
    }

    public bool StrongDamage(Hero opponent)
    {
        if (HeroType == HeroType.Ranger && IsFirstSpellActive && UsedCriticals <= 3)
        {
            if(Mana >= Globals.RANGER_ULT_STRONG_ATTACK_MANA_REQ)
            {
                Mana -= Globals.RANGER_ULT_STRONG_ATTACK_MANA_REQ;
                var strongDamage = (int)Math.Ceiling((decimal)(Strength / 3 + Intelligence / 9)) - (int)Math.Ceiling((decimal)opponent.Dexterity / 9);

                opponent.Hp -= strongDamage;
                UsedCriticals++;

                Log($"Critical! {this.Name} zada³ {strongDamage} obra¿eñ {opponent.Name}owi(-10 many)");

                DamageRelatedTurn = true;
                return true;
            }
            else
            {
                Log("Za ma³o many na criticala!");
                return false;
            }
        }
        else
        {
            if (Mana < Globals.STRONG_ATTACK_MANA_REQ)
            {
                Log($"Atak siê nie powiód³, za ma³o many!");
                return false;
            }
            else if (OperationAuthtorization(Strength))
            {
                Mana -= Globals.STRONG_ATTACK_MANA_REQ;

                var strongDamage = (int)Math.Ceiling((decimal)(Strength / 3 + Intelligence / 9)) - (int)Math.Ceiling((decimal)opponent.Dexterity / 9);

                opponent.Hp -= strongDamage;

                Log($"Mocny atak! {this.Name} zada³ {strongDamage} obra¿eñ {opponent.Name}owi(-20 many)");

                DamageRelatedTurn = true;
                return true;
            }
            else
            {
                Log("Atak siê nie powiód³! Za ma³o lacku");
                DamageRelatedTurn = false;

                return true;
            }
        }
    }

    public bool UseUltimate(Hero opponent)
    {
        if (HeroType == HeroType.Warrior)
        {
            if (!opponent.DamageRelatedTurn)
            {
                Log("Przeciwnik nie zada³ obra¿eñ!");
                return false;
            }
            else if (Mana < Globals.WARRIOR_ULT_MANA_REQ)
            {
                Log("Za ma³o many!");
                return false;
            }
            else
            {
                var damage = (int)Math.Ceiling(0.8 * opponent.LatestDamageValue);

                opponent.Hp -= damage;

                LatestDamageValue = damage;
                Mana -= Globals.WARRIOR_ULT_MANA_REQ;
                Log($"Kontratak! Zadane obra¿enia: {damage} (-45 many)");

                opponent.DamageRelatedTurn = false;
                DamageRelatedTurn = true;

                return true;
            }
        }
        else if (HeroType == HeroType.Sorcerer)
        {
            if (Mana < Globals.SORCERER_ULT_MANA_REQ)
            {
                Log("Za ma³o many!");
                return false;
            }
            else
            {
                var upHpValue = (int)Math.Ceiling(0.8 * MaxHp);
                var hpDiffrence = Math.Abs(upHpValue - Hp);

                if (Hp != upHpValue)
                {
                    Hp = upHpValue;
                    Mana -= Globals.SORCERER_ULT_MANA_REQ;
                    Log($"Zwiêkszono HP o: {hpDiffrence}HP (-50 many)");

                    DamageRelatedTurn = false;

                    return true;
                }
                else
                {
                    Log("Ty pazerna œwinio! Obcena wartoœæ HP jest niemniejsza od wartoœci zwiêkszenia!");
                    DamageRelatedTurn = false;

                    return true;
                }
            }
        }
        else if (HeroType == HeroType.Ranger)
        {
            if (!opponent.DamageRelatedTurn)
            {
                Log("Przeciwnik nie wykona³ ruchu, nie zada³ obra¿eñ");
                return false;
            }
            else if (Mana < Globals.RANGER_ULT_MANA_REQ)
            {
                Log("Za ma³o many!");
                return false;
            }
            else
            {
                var fullDodgeAndSmallHeal = opponent.LatestDamageValue + (int)Math.Ceiling(0.3 * MaxHp);

                Hp += fullDodgeAndSmallHeal;
                Mana -= Globals.RANGER_ULT_MANA_REQ;
                Log($"Pe³ny unik! Odzyskano i zregenerowano {fullDodgeAndSmallHeal}HP (-35 many)");

                DamageRelatedTurn = false;

                return true;
            }
        }

        return true;
    }

    public bool UseFirstSpell(Hero opponent)
    {
        if (!IsSecondSpellActive)
        {
            if (HeroType == HeroType.Warrior)
            {
                if (Mana >= Globals.WARRIOR_SPELL1_MANA_REQ)
                {
                    if (IsFirstSpellActive)
                    {
                        FirstSpellActiveCounter = 0;
                        Mana -= Globals.WARRIOR_SPELL1_MANA_REQ;

                        Log("Odnowiono pierwsze zaklêcie!");
                    }
                    else
                    {
                        _strengthBoost = (int)Math.Ceiling(Strength * 0.4);
                        _dexterityBoost = (int)Math.Ceiling(Dexterity * 0.4);

                        Strength += _strengthBoost;
                        Dexterity += _dexterityBoost;

                        Mana -= Globals.WARRIOR_SPELL1_MANA_REQ;
                        IsFirstSpellActive = true;

                        if(PlayerType == PlayerType.User)
                            OnFirstSpellChanged(true);
                        else
                            OnFirstSpellChanged(false);

                        Log("Aktywowano pierwsze zaklêcie!");
                    }

                    DamageRelatedTurn = false;
                    return true;
                }
                else
                {
                    Log("Za ma³o many!");
                    return false;
                }
            }
            else if (HeroType == HeroType.Sorcerer)
            {
                if (Mana >= Globals.SORCERER_SPELL1_MANA_REQ)
                {
                    var hpTakenFromOpponent = (int)Math.Ceiling(opponent.MaxHp * 0.25);
                    LatestDamageValue = hpTakenFromOpponent;
                    opponent.Hp -= hpTakenFromOpponent;
                    Hp += hpTakenFromOpponent;

                    Mana -= Globals.SORCERER_SPELL1_MANA_REQ;

                    Log($"Aktywowano pierwsze zaklêcie! Zabrano przeciwnikowi {hpTakenFromOpponent} HP!");

                    DamageRelatedTurn = false;

                    return true;
                }
                else
                {
                    Log("Za ma³o many!");
                    return false;
                }
            }
            else if (HeroType == HeroType.Ranger) // 3 gwarantowane criticale, -10many/atak
            {
                if (Mana >= Globals.RANGER_SPELL1_MANA_REQ)
                {
                    if (IsFirstSpellActive)
                    {
                        UsedCriticals = 0;
                        Mana -= Globals.RANGER_SPELL1_MANA_REQ;

                        Log("Odnowiono pierwsze zaklêcie!");
                    }
                    else
                    {
                        Mana -= Globals.RANGER_SPELL1_MANA_REQ;
                        IsFirstSpellActive = true;

                        if (PlayerType == PlayerType.User)
                            OnFirstSpellChanged(true);
                        else
                            OnFirstSpellChanged(false);

                        Log("Aktywowano pierwsze zaklêcie! Masz gwarantowane 3 criticale!");
                    }
                    DamageRelatedTurn = false;

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
            if (HeroType == HeroType.Warrior)
            {
                if (Mana >= Globals.WARRIOR_SPELL2_MANA_REQ)
                {
                    if (IsSecondSpellActive)
                    {
                        SecondSpellActiveCounter = 0;
                        Mana -= Globals.WARRIOR_SPELL2_MANA_REQ;

                        Log("Odnowiono drugie zaklêcie!");
                    }
                    else
                    {
                        _dexterityBoost = (int)Math.Ceiling(Dexterity * 2.5);

                        Dexterity += _dexterityBoost;

                        Mana -= Globals.WARRIOR_SPELL2_MANA_REQ;
                        IsSecondSpellActive = true;

                        if (PlayerType == PlayerType.User)
                            OnSecondSpellChanged(true);
                        else
                            OnSecondSpellChanged(false);

                        Log("Aktywowano drugie zaklêcie!");
                    }
                    DamageRelatedTurn = false;

                    return true;
                }
                else
                {
                    Log("Za ma³o many!");
                    return false;
                }
            }
            else if (HeroType == HeroType.Sorcerer)
            {
                if (Mana >= Globals.SORCERER_SPELL2_MANA_REQ)
                {
                    opponent.Mana -= 20;

                    Mana -= Globals.SORCERER_SPELL2_MANA_REQ;

                    Log($"Aktywowano drugie zaklêcie! Zabrano przeciwnikowi 20 many!");

                    DamageRelatedTurn = false;

                    return true;
                }
                else
                {
                    Log("Za ma³o many!");
                    return false;
                }
            }
            else if (HeroType == HeroType.Ranger)
            {
                if (Mana >= Globals.RANGER_SPELL2_MANA_REQ)
                {
                    if (IsSecondSpellActive)
                    {
                        SecondSpellActiveCounter = 0;
                        Mana -= Globals.RANGER_SPELL2_MANA_REQ;

                        Log("Odnowiono drugie zaklêcie!");
                    }
                    else
                    {
                        _takenDexterity = opponent.Dexterity;
                        opponent.Dexterity = 0;

                        Mana -= Globals.RANGER_SPELL2_MANA_REQ;
                        IsSecondSpellActive = true;

                        if (PlayerType == PlayerType.User)
                            OnSecondSpellChanged(true);
                        else
                            OnSecondSpellChanged(false);

                        Log($"Aktywowano drugie zaklêcie! Zabrano przeciwnikowi ca³¹ zwinnoœæ!");
                    }
                    DamageRelatedTurn = false;

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

        return true;
    }

    public async Task CheckSpellStatus(Hero opponent)
    {
        if (HeroType == HeroType.Warrior)
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

                    if (PlayerType == PlayerType.User)
                        OnFirstSpellChanged(true);
                    else
                        OnFirstSpellChanged(false);

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

                    if (PlayerType == PlayerType.User)
                        OnSecondSpellChanged(true);
                    else
                        OnSecondSpellChanged(false);

                    SecondSpellActiveCounter = 0;

                    Log("Drugie zaklêcie wygas³o!");
                    await Task.Delay(1500);
                }
            }
        }
        else if (HeroType == HeroType.Ranger)
        {
            if (IsFirstSpellActive && UsedCriticals == 3)
            {
                IsFirstSpellActive = false;

                if (PlayerType == PlayerType.User)
                    OnFirstSpellChanged(true);
                else
                    OnFirstSpellChanged(false);

                UsedCriticals = 0;

                Log("Pierwsze zaklêcie wygas³o!");
                await Task.Delay(1500);
            }

            if (IsSecondSpellActive)
            {
                if (SecondSpellActiveCounter != 5)
                    SecondSpellActiveCounter++;
                else if (SecondSpellActiveCounter == 5)
                {
                    opponent.Dexterity += _takenDexterity;
                    _takenDexterity = 0;

                    IsSecondSpellActive = false;

                    if (PlayerType == PlayerType.User)
                        OnSecondSpellChanged(true);
                    else
                        OnSecondSpellChanged(false);

                    SecondSpellActiveCounter = 0;

                    Log("Drugie zaklêcie wygas³o!");
                    await Task.Delay(1500);
                }
            }
        }
    }

    public void OnFirstSpellChanged(bool IfUser)
    {
        OnFirstSpell?.Invoke(IfUser);
    }

    public void OnSecondSpellChanged(bool IfUser)
    {
        OnSecondSpell?.Invoke(IfUser);
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