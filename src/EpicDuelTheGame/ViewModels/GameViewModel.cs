using EpicDuelTheGame.Commands;
using EpicDuelTheGame.Services;
using EpicDuelTheGame.Stores;
using System.Windows.Input;

namespace EpicDuelTheGame.ViewModels;

public class GameViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly ChooseHeroViewModel _chooseHeroViewModel;

    public ICommand NavigateeStartViewCommand { get; }

    public GameViewModel(NavigationStore navigationStore, ChooseHeroViewModel chooseHeroViewModel)
    {
        _navigationStore = navigationStore;
        _chooseHeroViewModel = chooseHeroViewModel;

        NavigateeStartViewCommand = new NavigateCommand<StartViewModel>(new NavigationService<StartViewModel>(navigationStore, () => new StartViewModel(navigationStore)));
    }
}