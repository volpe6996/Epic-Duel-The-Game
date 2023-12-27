using EpicDuelTheGame.Commands;
using EpicDuelTheGame.Models;
using EpicDuelTheGame.Services;
using EpicDuelTheGame.Stores;
using Newtonsoft.Json;
using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;

namespace EpicDuelTheGame.ViewModels;

public class GameViewModel : ViewModelBase
{
    public NavigationStore _navigationStore { private get; set; }

    private Hero _userHero;
    public Hero UserHero
    {
        get { return _userHero; }
        set
        {
            _userHero = value;
            OnPropertyChanged(nameof(UserHero));
        }
    }

    private int _userFirstSpellGreenZIndex = 0;
    public int UserFirstSpellGreenZIndex
    {
        get { return _userFirstSpellGreenZIndex; }
        set
        {
            _userFirstSpellGreenZIndex = value;
            OnPropertyChanged(nameof(UserFirstSpellGreenZIndex));
        }
    }

    private int _userFirstSpellRedZIndex = 1;
    public int UserFirstSpellRedZIndex
    {
        get { return _userFirstSpellRedZIndex; }
        set
        {
            _userFirstSpellRedZIndex = value;
            OnPropertyChanged(nameof(UserFirstSpellRedZIndex));
        }
    }

    private int _userSecondSpellGreenZIndex = 0;
    public int UserSecondSpellGreenZIndex
    {
        get { return _userSecondSpellGreenZIndex; }
        set
        {
            _userSecondSpellGreenZIndex = value;
            OnPropertyChanged(nameof(UserSecondSpellGreenZIndex));
        }
    }

    private int _userSecondSpellRedZIndex = 1;
    public int UserSecondSpellRedZIndex
    {
        get { return _userSecondSpellRedZIndex; }
        set
        {
            _userSecondSpellRedZIndex = value;
            OnPropertyChanged(nameof(UserSecondSpellRedZIndex));
        }
    }

    private Hero _opponentHero;
    public Hero OpponentHero
    {
        get { return _opponentHero; }
        set
        {
            _opponentHero = value;
            OnPropertyChanged(nameof(OpponentHero));
        }
    }

    private int _opponentFirstSpellGreenZIndex = 0;
    public int OpponentFirstSpellGreenZIndex
    {
        get { return _opponentFirstSpellGreenZIndex; }
        set
        {
            _opponentFirstSpellGreenZIndex = value;
            OnPropertyChanged(nameof(OpponentFirstSpellGreenZIndex));
        }
    }

    private int _opponentFirstSpellRedZIndex = 1;
    public int OpponentFirstSpellRedZIndex
    {
        get { return _opponentFirstSpellRedZIndex; }
        set
        {
            _opponentFirstSpellRedZIndex = value;
            OnPropertyChanged(nameof(OpponentFirstSpellRedZIndex));
        }
    }

    private int _opponentSecondSpellGreenZIndex = 0;
    public int OpponentSecondSpellGreenZIndex
    {
        get { return _opponentSecondSpellGreenZIndex; }
        set
        {
            _opponentSecondSpellGreenZIndex = value;
            OnPropertyChanged(nameof(OpponentSecondSpellGreenZIndex));
        }
    }

    private int _opponentSecondSpellRedZIndex = 1;
    public int OpponentSecondSpellRedZIndex
    {
        get { return _opponentSecondSpellRedZIndex; }
        set
        {
            _opponentSecondSpellRedZIndex = value;
            OnPropertyChanged(nameof(OpponentSecondSpellRedZIndex));
        }
    }

    private string _log;
    public string Log
    {
        get { return _log; }
        set
        {
            _log = value;
            OnPropertyChanged(nameof(Log));
        }
    }

    [JsonProperty]
    public int _numberOfTurns { get; private set; } = 0;
    [JsonIgnore]
    public int NumberOfTurns
    {
        get { return _numberOfTurns; }
        set
        {
            if (value > 1) // on game load
                _numberOfTurns = value;
            else
                _numberOfTurns += 1;
            OnPropertyChanged(nameof(NumberOfTurns));
        }
    }

    [JsonIgnore]
    public ICommand NavigateToStartViewCommand { get; set; }
    [JsonIgnore]
    public ICommand SaveGameCommand { get; }

    [JsonIgnore]
    public ICommand UserAttacksCommand { get; }
    [JsonIgnore]
    public ICommand UserUpIntelligenceCommand { get; }
    [JsonIgnore]
    public ICommand UserUseFirstSpellCommand { get; }
    [JsonIgnore]
    public ICommand UserUseSecondSpellCommand { get; }

    [JsonIgnore]
    public ICommand OpponentAttacksCommand { get; }
    [JsonIgnore]
    public ICommand OpponentUpIntelligenceCommand { get; }
    [JsonIgnore]
    public ICommand OpponentUseFirstSpellCommand { get; }
    [JsonIgnore]
    public ICommand OpponentUseSecondSpellCommand { get; }

    private readonly AiDecisionPathService _aiDecisionPathService;

    private int _turn = 0;
    public int Turn
    {
        get { return _turn; }
        set
        {
            _turn = value;
            OnTurnChanged?.Invoke();
            OnPropertyChanged(nameof(Turn));

            if (Turn != 2)
            {
                if(!CheckGameStatus() && value == 1)
                {
                    _aiDecisionPathService.Decide();
                }

                NumberOfTurns++;
            }
        }
    }

    public event Action OnTurnChanged;

    public GameViewModel(NavigationStore navigationStore, Hero userHero, Hero opponentHero)
    {
        _navigationStore = navigationStore;

        _userHero = userHero;
        _userHero.LogEvent += HandleLogEvent;
        _userHero.OnFirstSpell += HandleFirstSpellChanged;
        _userHero.OnSecondSpell += HandleSecondSpellChanged;

        _opponentHero = opponentHero;
        _opponentHero.LogEvent += HandleLogEvent;
        _opponentHero.OnFirstSpell += HandleFirstSpellChanged;
        _opponentHero.OnSecondSpell += HandleSecondSpellChanged;

        NavigateToStartViewCommand = new NavigateCommand<StartViewModel>(new NavigationService<StartViewModel>(_navigationStore, () => new StartViewModel(_navigationStore)));
        SaveGameCommand = new SaveGameCommand(this);

        // USER - 0, AI - 1

        // USER COMMANDS
        {
            UserAttacksCommand = new HandleHeroAttackCommand(this, _userHero, _opponentHero, 0);
            UserUpIntelligenceCommand = new HandleHeroOperationCommand(this, UserHero.UpIntelligence, 0, UserHero, OpponentHero);
            UserUseFirstSpellCommand = new HandleHeroOperationCommand(this, UserHero.UseFirstSpell, 0, UserHero, OpponentHero);
            UserUseSecondSpellCommand = new HandleHeroOperationCommand(this, UserHero.UseSecondSpell, 0, UserHero, OpponentHero);
        }

        // AI COMMANDS 
        {
            OpponentAttacksCommand = new HandleHeroAttackCommand(this, _opponentHero, _userHero, 1);
            OpponentUpIntelligenceCommand = new HandleHeroOperationCommand(this, OpponentHero.UpIntelligence, 1, OpponentHero, UserHero);
            OpponentUseFirstSpellCommand = new HandleHeroOperationCommand(this, OpponentHero.UseFirstSpell, 1, OpponentHero, UserHero);
            OpponentUseSecondSpellCommand = new HandleHeroOperationCommand(this, OpponentHero.UseSecondSpell, 1, OpponentHero, UserHero);
        }

        _aiDecisionPathService = new AiDecisionPathService(this);

        if (Turn == 1)
            _aiDecisionPathService.Decide();
    }

    public void HandleLogEvent(string message)
    {
        Log = message;
    }

    // on default red - 1 green - 0, on spell active red - 1 green - 2
    public void HandleFirstSpellChanged(bool IfUser)
    {
        if(IfUser)
        {
            if (UserFirstSpellGreenZIndex == 2)
                UserFirstSpellGreenZIndex = 0;
            else
                UserFirstSpellGreenZIndex = 2;
        }
        else
        {
            if (OpponentFirstSpellGreenZIndex == 2)
                OpponentFirstSpellGreenZIndex = 0;
            else
                OpponentFirstSpellGreenZIndex = 2;
        }
    }

    public void HandleSecondSpellChanged(bool IfUser)
    {
        if (IfUser)
        {
            if (UserSecondSpellGreenZIndex == 2)
                UserSecondSpellGreenZIndex = 0;
            else
                UserSecondSpellGreenZIndex = 2;
        }
        else
        {
            if (OpponentSecondSpellGreenZIndex == 2)
                OpponentSecondSpellGreenZIndex = 0;
            else
                OpponentSecondSpellGreenZIndex = 2;
        }
    }

    public bool CheckGameStatus()
    {
        if (UserHero.Hp <= 0)
        {
            Log = $"{OpponentHero.Name} zwyciê¿a!";
            Turn = 2;

            return true;
        }
        else if(OpponentHero.Hp <= 0)
        {
            Log = $"{UserHero.Name} zwyciê¿a!";
            Turn = 2;

            return true;

        }

        return false;
    }
}