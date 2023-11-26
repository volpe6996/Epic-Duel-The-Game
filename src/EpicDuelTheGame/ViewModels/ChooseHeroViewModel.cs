using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using EpicDuelTheGame.Commands;
using EpicDuelTheGame.Models;
using EpicDuelTheGame.Services;
using EpicDuelTheGame.Stores;

namespace EpicDuelTheGame.ViewModels;

public class ChooseHeroViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    private ObservableCollection<Hero> _heroes;
    public ObservableCollection<Hero> Heroes
    {
        get { return _heroes; }
        set
        {
            if (_heroes != value)
                _heroes = value;
        }
    }

    // user related fields & properties

    private Hero _selectedHeroUser;
    public Hero SelectedHeroUser
    {
        get { return _selectedHeroUser; }
        set
        {
            _selectedHeroUser = value;
            OnPropertyChanged(nameof(SelectedHeroUser));

            UserTextBoxVisibility = "Visible";

            if (SelectedHeroUser.GetType() == typeof(Warrior))
            {
                UserWarriorStatsVisibility = "Visible";
                UserSorcererStatsVisibility = "Hidden";
                UserRangerStatsVisibility = "Hidden";
            }
            else if (SelectedHeroUser.GetType() == typeof(Sorcerer))
            {
                UserWarriorStatsVisibility = "Hidden";
                UserSorcererStatsVisibility = "Visible";
                UserRangerStatsVisibility = "Hidden";
            }
            else if (SelectedHeroUser.GetType() == typeof(Ranger))
            {
                UserWarriorStatsVisibility = "Hidden";
                UserSorcererStatsVisibility = "Hidden";
                UserRangerStatsVisibility = "Visible";
            }
        }
    }

    private string _userTextBoxVisibility = "Hidden";
    public string UserTextBoxVisibility
    {
        get { return _userTextBoxVisibility; }
        set
        {
            _userTextBoxVisibility = value;
            OnPropertyChanged(nameof(UserTextBoxVisibility));
        }
    }

    private string? _userEnterdName = null;
    public string UserEnterdName
    {
        get { return _userEnterdName; }
        set
        {
            _userEnterdName = value;
            OnPropertyChanged(nameof(UserEnterdName));

            UserLabel = $"Twoim przeciwnikiem jest {value}";
        }
    }

    private string _userLabel;
    public string UserLabel
    {
        get { return _userLabel; }
        set
        {
            _userLabel = value;
            OnPropertyChanged(nameof(UserLabel));
        }
    }

    private string _userWarriorStatsVisibility = "Hidden";
    public string UserWarriorStatsVisibility
    {
        get { return _userWarriorStatsVisibility; }
        set
        {
            _userWarriorStatsVisibility = value;
            OnPropertyChanged(nameof(UserWarriorStatsVisibility));
        }
    }

    private string _userSorcererStatsVisibility = "Hidden";
    public string UserSorcererStatsVisibility
    {
        get { return _userSorcererStatsVisibility; }
        set
        {
            _userSorcererStatsVisibility = value;
            OnPropertyChanged(nameof(UserSorcererStatsVisibility));
        }
    }

    private string _userRangerStatsVisibility = "Hidden";
    public string UserRangerStatsVisibility
    {
        get { return _userRangerStatsVisibility; }
        set
        {
            _userRangerStatsVisibility = value;
            OnPropertyChanged(nameof(UserRangerStatsVisibility));
        }
    }

    // opponent related fields & properties

    private Hero _selectedHeroOpponent;
    public Hero SelectedHeroOpponent
    {
        get { return _selectedHeroOpponent; }
        set 
        {
            _selectedHeroOpponent = value;
            OnPropertyChanged(nameof(SelectedHeroOpponent));

            OpponentTextBoxVisibility = "Visible";

            if (SelectedHeroOpponent.GetType() == typeof(Warrior))
            {
                OpponentWarriorStatsVisibility = "Visible";
                OpponentSorcererStatsVisibility = "Hidden";
                OpponentRangerStatsVisibility = "Hidden";
            }
            else if (SelectedHeroOpponent.GetType() == typeof(Sorcerer))
            {
                OpponentWarriorStatsVisibility = "Hidden";
                OpponentSorcererStatsVisibility = "Visible";
                OpponentRangerStatsVisibility = "Hidden";
            }
            else if (SelectedHeroOpponent.GetType() == typeof(Ranger))
            {
                OpponentWarriorStatsVisibility = "Hidden";
                OpponentSorcererStatsVisibility = "Hidden";
                OpponentRangerStatsVisibility = "Visible";
            }
        }
    }

    private string _opponentTextBoxVisibility = "Hidden";
    public string OpponentTextBoxVisibility
    {
        get{ return _opponentTextBoxVisibility; }
        set 
        {
            _opponentTextBoxVisibility = value;
            OnPropertyChanged(nameof(OpponentTextBoxVisibility));
        }
    }

    private string _opponentEnterdName;
    public string OpponentEnterdName
    {
        get { return _opponentEnterdName; }
        set
        {
            _opponentEnterdName = value;
            OnPropertyChanged(nameof(OpponentEnterdName));

            OpponentLabel = $"Twoim przeciwnikiem jest {value}";
        }
    }

    private string? _opponentLabel = null;
    public string OpponentLabel
    {
        get { return _opponentLabel; }
        set
        {
            _opponentLabel = value;
            OnPropertyChanged(nameof(OpponentLabel));
        }
    }

    private string _opponentWarriorStatsVisibility = "Hidden";
    public string OpponentWarriorStatsVisibility
    {
        get { return _opponentWarriorStatsVisibility; }
        set 
        {
            _opponentWarriorStatsVisibility = value;
            OnPropertyChanged(nameof(OpponentWarriorStatsVisibility));
        }
    }

    private string _opponentSorcererStatsVisibility = "Hidden";
    public string OpponentSorcererStatsVisibility
    {
        get { return _opponentSorcererStatsVisibility; }
        set
        {
            _opponentSorcererStatsVisibility = value;
            OnPropertyChanged(nameof(OpponentSorcererStatsVisibility));
        }
    }

    private string _opponentRangerStatsVisibility = "Hidden";
    public string OpponentRangerStatsVisibility
    {
        get { return _opponentRangerStatsVisibility; }
        set
        {
            _opponentRangerStatsVisibility = value;
            OnPropertyChanged(nameof(OpponentRangerStatsVisibility));
        }
    }

    public ICommand NavigateGameViewCommand { get; }
    public ICommand NavigateStartViewCommand { get; }

    public ICommand StartTheGameCommand { get; }

    public ICommand UserHeroChoosedCommand { get; }
    public ICommand OpponentHeroChoosedCommand { get; }

    public ChooseHeroViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;

        NavigateGameViewCommand = new NavigateCommand<GameViewModel>(new NavigationService<GameViewModel>(navigationStore, () => new GameViewModel(navigationStore, this)));
        NavigateStartViewCommand = new NavigateCommand<StartViewModel>(new NavigationService<StartViewModel>(navigationStore, () => new StartViewModel(navigationStore)));

        Heroes = new ObservableCollection<Hero>
        {
            new Warrior("", "Jakiœ super opis Warriora, mojego niepokonanego bohatera", "/images/poziomka.jpg"),
            new Sorcerer("", "Jakiœ super opis Sorcerera, mojego niepokonanego bohatera", "/images/piston.jpg"),
            new Ranger("", "Jakiœ super opis Rangera, mojego niepokonanego bohatera", "/images/menel.jpg"),
        };

        UserHeroChoosedCommand = new HeroChoosedCommand(this, "User");
        OpponentHeroChoosedCommand = new HeroChoosedCommand(this, "Opponent");

        StartTheGameCommand = new StartTheGameCommand(this, navigationStore);
    }
}