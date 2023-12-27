using EpicDuelTheGame.Commands;
using EpicDuelTheGame.Services;
using EpicDuelTheGame.Stores;
using System.Windows.Input;

namespace EpicDuelTheGame.ViewModels;

public class StartViewModel : ViewModelBase
{
    public ICommand NavigateChooseHeroCommand { get; }
    public ICommand NavigateLoadGameCommand { get; }

    public StartViewModel(NavigationStore navigationStore)
    {
        NavigateChooseHeroCommand
            = new NavigateCommand<ChooseHeroViewModel>(new NavigationService<ChooseHeroViewModel>(navigationStore, () => new ChooseHeroViewModel(navigationStore)));

        NavigateLoadGameCommand 
            = new NavigateCommand<LoadGameViewModel>(new NavigationService<LoadGameViewModel>(navigationStore, () => new LoadGameViewModel(navigationStore)));
    }
}