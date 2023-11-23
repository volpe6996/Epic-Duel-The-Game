using EpicDuelTheGame.Stores;

namespace EpicDuelTheGame.ViewModels;

public class GameViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public GameViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }
}