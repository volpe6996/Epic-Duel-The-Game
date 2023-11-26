namespace EpicDuelTheGame.Models;

public abstract class Hero : ClassBase
{
    public string? Name { get; set; }
    public string Description { get; private set; }
    public string Path { get; private set; }

    private int _hp;
    public int Hp
    {
        get { return _hp; }
        private set
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

    public Hero(string name, string description, string path)
    {
        this.Name = name;
        this.Description = description;
        this.Path = path;
    }

    protected virtual void Init(int hp, int strength, int dexterity, int intelligence, int mana)
    {
        this.Hp = hp;
        this.Strength = strength;
        this.Dexterity = dexterity;
        this.Intelligence = intelligence;
        this.Mana = mana;
    }
}