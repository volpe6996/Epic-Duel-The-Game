using EpicDuelTheGame.Services;
using EpicDuelTheGame.ViewModels;

namespace EpicDuelTheGame.Commands;

public class NavigateCommand<TViewModel> : CommandBase
    where TViewModel : ViewModelBase
{
    private readonly NavigationService<TViewModel> _navigationService;

    public NavigateCommand(NavigationService<TViewModel> navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object parameter)
    {
        _navigationService.Navigate();
    }
}