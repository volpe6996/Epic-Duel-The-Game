using EpicDuelTheGame.Abstract;
using EpicDuelTheGame.Commands;
using EpicDuelTheGame.Models;
using EpicDuelTheGame.Services;
using EpicDuelTheGame.Stores;
using System;
using System.CodeDom;
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

    public ICommand NavigateToStartViewCommand { get; }
    public ICommand UserAttacksCommand { get; }
    public ICommand UserUpIntelligenceCommand { get; }

    //private int _turn = new Random().Next(0, 2);
    private int _turn = 1;
    public int Turn
    {
        get { return _turn; }
        set
        {
            _turn = value;
            OnTurnChanged?.Invoke();
            OnPropertyChanged(nameof(Turn));
        }
    }

    public event Action OnTurnChanged;

    public GameViewModel(NavigationStore navigationStore, Hero userHero, Hero opponentHero)
    {
        _navigationStore = navigationStore;

        _userHero = userHero;
        _userHero.LogEvent += HandleLogEvent;

        _opponentHero = opponentHero;

        NavigateToStartViewCommand = new NavigateCommand<StartViewModel>(new NavigationService<StartViewModel>(navigationStore, () => new StartViewModel(navigationStore)));

        UserAttacksCommand = new HandleHeroAttackCommand(this, _userHero, _opponentHero, 0);

        UserUpIntelligenceCommand = new HandleHeroOperationCommand(this, UserHero.UpIntelligence);
    }

    public void HandleLogEvent(string message)
    {
        UserLog = message;
    }
}