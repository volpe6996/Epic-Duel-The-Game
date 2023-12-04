using EpicDuelTheGame.Abstract;
using EpicDuelTheGame.Commands;
using EpicDuelTheGame.Models;
using EpicDuelTheGame.Services;
using EpicDuelTheGame.Stores;
using System;
using System.CodeDom;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EpicDuelTheGame.ViewModels;

public class GameViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

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

    private string _userLog;
    public string UserLog
    { 
        get { return _userLog; }
        set
        {
            _userLog = value;
            OnPropertyChanged(nameof(UserLog));
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

    private string _opponentLog;
    public string OpponentLog
    {
        get { return _opponentLog; }
        set
        {
            _opponentLog = value;
            OnPropertyChanged(nameof(OpponentLog));
        }
    }

    public ICommand NavigateToStartViewCommand { get; }
    public ICommand UserAttacksCommand { get; }
    public ICommand UserUpIntelligenceCommand { get; }

    public ICommand OpponentAttacksCommand { get; }
    public ICommand OpponentUpIntelligenceCommand { get; }

    private readonly AiDecisionPathService _aiDecisionPathService;

    private int _turn = new Random().Next(0, 2);
    //private int _turn = 0;
    public int Turn
    {
        get { return _turn; }
        set
        {
            _turn = value;
            OnTurnChanged?.Invoke();
            OnPropertyChanged(nameof(Turn));

            if(value == 1)
                _aiDecisionPathService.Decide();
        }
    }

    public event Action OnTurnChanged;

    public GameViewModel(NavigationStore navigationStore, Hero userHero, Hero opponentHero)
    {
        _navigationStore = navigationStore;

        _userHero = userHero;
        _userHero.LogEvent += HandleUserLogEvent;

        _opponentHero = opponentHero;
        _opponentHero.LogEvent += HandleOpponentLogEvent;

        NavigateToStartViewCommand = new NavigateCommand<StartViewModel>(new NavigationService<StartViewModel>(navigationStore, () => new StartViewModel(navigationStore)));

        // USER - 0, AI - 1

        UserAttacksCommand = new HandleHeroAttackCommand(this, _userHero, _opponentHero, 0);
        OpponentAttacksCommand = new HandleHeroAttackCommand(this, _opponentHero, _userHero, 1);

        UserUpIntelligenceCommand = new HandleHeroOperationCommand(this, UserHero.UpIntelligence, 0);
        OpponentUpIntelligenceCommand = new HandleHeroOperationCommand(this, OpponentHero.UpIntelligence, 1);

        _aiDecisionPathService = new AiDecisionPathService(this);

        if (Turn == 1)
            _aiDecisionPathService.Decide();
    }

    public void HandleUserLogEvent(string message)
    {
        UserLog = message;
    }

    public void HandleOpponentLogEvent(string message)
    {
        OpponentLog = message;
    }
}