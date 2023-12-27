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

            UserLabel = $"Twoim herosem jest {value}";
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

            OpponentLabel = $"Herosem twojego przeciwnika jest {value}";
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
            new ChooseHeroModel(
                HeroType.Warrior,
                "/images/poziomka.jpg",
                "Nie ma problemu, który nie móg³by rozwi¹zaæ piêœci¹. Warrior - mistrz w rozjaœnianiu sytuacji... zazwyczaj poprzez wymachiwanie ogromnym mieczem.",
                "Kontratak - po udanym uderzeniu przeciwnika dokonaj kontrataku w wysokoœci 80% zadanych tobie obra¿eñ. Otrzymujesz darmowy ruch. (-45 many)",
                "Sok z gumijagód - Boost o 40% do si³y i zrêcznoœci. Trwa 3 tury (-25 many)",
                "Gruba skóra - Zmniejsz otrzymywane obra¿enia. Boost +250% do zwinnoœci. Dzia³a 2 tury (-15 many)"),
            new ChooseHeroModel(
                HeroType.Sorcerer,
                "/images/piston.jpg",
                "Sorcerer - mistrz magii i nieprzewidywalnych wybuchów. Zawsze przygotowany do zadania czaru, zw³aszcza jeœli to zadanie obejmuje przekszta³cenie wrogów w kaczki.",
                "Leczenie - odzyskaj do 80% maksymalnej wartoœci twojego HP (-50 many)",
                "Wampir - Zabierz 25% maksymalnej wartoœci HP przeciwnika i daj sobie. (-40 many)",
                "Wysysanie dusz - Zabierz 20 many przeciwnika. (-30 many)"),
            new ChooseHeroModel(
                HeroType.Ranger,
                "/images/menel.jpg",
                "Ranger - zwinny ³ucznik, który zawsze trzyma siê z dala od zamieszania. Jego umiejêtnoœæ strzelania z ³uku równie dobra, co jego zdolnoœæ do unikania niewygodnych rozmów.",
                "Pe³ny unik - po udanym uderzeniu przeciwnika dokonaj pe³nego uniku, odzyskaj stracone HP, a tak¿e zregeneruj 30% maksymalnej wartoœci swojego HP (-35 many)",
                "Sokole oko - Uzyskaj 3 gwarantowane criticale. Ka¿dy znich od teraz zabiera 10 many, a nie 20 (-30 many)",
                "Skutecznoœæ - Wyzeruj zwinnoœæ przeciwnika na 5 tur (-35 many)"),
        };

        UserHeroChoosedCommand = new HeroChoosedCommand(this, "User");
        OpponentHeroChoosedCommand = new HeroChoosedCommand(this, "Opponent");

        StartTheGameCommand = new StartTheGameCommand(this, navigationStore);
    }
}