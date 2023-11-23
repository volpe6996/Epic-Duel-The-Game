using System.Windows.Input;
using EpicDuelTheGame.Commands;
using EpicDuelTheGame.Services;
using EpicDuelTheGame.Stores;

namespace EpicDuelTheGame.ViewModels;

public class StartViewModel : ViewModelBase
{
    public ICommand NavigateChooseHeroCommand { get; }

    public StartViewModel(NavigationStore navigationStore)
    {
        // NavigateChooseHeroCommand = new NavigateCommand<ChooseHeroViewModel>(navigationStore, () => new ChooseHeroViewModel(navigationStore ));

        NavigateChooseHeroCommand 
            = new NavigateCommand<ChooseHeroViewModel>(new NavigationService<ChooseHeroViewModel>(navigationStore, () => new ChooseHeroViewModel(navigationStore)));
    }
}