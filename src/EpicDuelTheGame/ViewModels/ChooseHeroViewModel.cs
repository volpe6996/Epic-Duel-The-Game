using EpicDuelTheGame.Commands;
using EpicDuelTheGame.Models;
using EpicDuelTheGame.Services;
using EpicDuelTheGame.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EpicDuelTheGame.ViewModels;

public class ChooseHeroViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    private ObservableCollection<ChooseHeroModel> _choosableHeros;
    public ObservableCollection<ChooseHeroModel> ChoosableHeros
    {
        get { return _choosableHeros; }
        set
        {
            if (_choosableHeros != value)
                _choosableHeros = value;
        }
    }

    // user related fields & properties

    private ChooseHeroModel _selectedHeroUser;
    public ChooseHeroModel SelectedHeroUser
    {
        get { return _selectedHeroUser; }
        set
        {
            _selectedHeroUser = value;
            OnPropertyChanged(nameof(SelectedHeroUser));

            UserTextBoxVisibility = "Visible";

            if (SelectedHeroUser.HeroType == HeroTypes.Warrior)
            {
                UserWarriorStatsVisibility = "Visible";
                UserSorcererStatsVisibility = "Hidden";
                UserRangerStatsVisibility = "Hidden";
            }
            else if (SelectedHeroUser.HeroType == HeroTypes.Sorcerer)
            {
                UserWarriorStatsVisibility = "Hidden";
                UserSorcererStatsVisibility = "Visible";
                UserRangerStatsVisibility = "Hidden";
            }
            else if (SelectedHeroUser.HeroType == HeroTypes.Ranger)
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

    private ChooseHeroModel _selectedHeroOpponent;
    public ChooseHeroModel SelectedHeroOpponent
    {
        get { return _selectedHeroOpponent; }
        set
        {
            _selectedHeroOpponent = value;
            OnPropertyChanged(nameof(SelectedHeroOpponent));

            OpponentTextBoxVisibility = "Visible";

            if (SelectedHeroOpponent.HeroType == HeroTypes.Warrior)
            {
                OpponentWarriorStatsVisibility = "Visible";
                OpponentSorcererStatsVisibility = "Hidden";
                OpponentRangerStatsVisibility = "Hidden";
            }
            else if (SelectedHeroOpponent.HeroType == HeroTypes.Sorcerer)
            {
                OpponentWarriorStatsVisibility = "Hidden";
                OpponentSorcererStatsVisibility = "Visible";
                OpponentRangerStatsVisibility = "Hidden";
            }
            else if (SelectedHeroOpponent.HeroType == HeroTypes.Ranger)
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
        get { return _opponentTextBoxVisibility; }
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

    public ICommand NavigateStartViewCommand { get; }

    public ICommand StartTheGameCommand { get; }

    public ICommand UserHeroChoosedCommand { get; }
    public ICommand OpponentHeroChoosedCommand { get; }

    public ChooseHeroViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;

        NavigateStartViewCommand = new NavigateCommand<StartViewModel>(new NavigationService<StartViewModel>(navigationStore, () => new StartViewModel(navigationStore)));

        ChoosableHeros = new ObservableCollection<ChooseHeroModel>
        {
            new ChooseHeroModel(HeroTypes.Warrior, "Jakiœ super opis Warriora, mojego niepokonanego bohatera", "/images/poziomka.jpg"),
            new ChooseHeroModel(HeroTypes.Sorcerer, "Jakiœ super opis Sorcerera, mojego niepokonanego bohatera", "/images/piston.jpg"),
            new ChooseHeroModel(HeroTypes.Ranger, "Jakiœ super opis Rangera, mojego niepokonanego bohatera", "/images/menel.jpg"),
        };

        UserHeroChoosedCommand = new HeroChoosedCommand(this, "User");
        OpponentHeroChoosedCommand = new HeroChoosedCommand(this, "Opponent");

        StartTheGameCommand = new StartTheGameCommand(this, navigationStore);
    }
}