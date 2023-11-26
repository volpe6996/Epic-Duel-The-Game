using EpicDuelTheGame.Services;
using EpicDuelTheGame.Stores;
using EpicDuelTheGame.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EpicDuelTheGame.Commands
{
    // przypisujemy nazwy nadane przez usera dla samego siebie i opponenta oraz nawigujemy do GameView
    public class StartTheGameCommand : CommandBase
    {
        private readonly ChooseHeroViewModel _chooseHeroViewModel;
        private readonly NavigationStore _navigationStore;

        private readonly ICommand NavigateToGameViewCommand;

        public StartTheGameCommand(ChooseHeroViewModel chooseHeroViewModel, NavigationStore navigationStore)
        {
            _chooseHeroViewModel = chooseHeroViewModel;
            _navigationStore = navigationStore;

            NavigateToGameViewCommand = new NavigateCommand<GameViewModel>(new NavigationService<GameViewModel>(_navigationStore, () => new GameViewModel(_navigationStore, _chooseHeroViewModel)));
        }

        public override void Execute(object parameter)
        {
            if (_chooseHeroViewModel.SelectedHeroUser != null && _chooseHeroViewModel.SelectedHeroOpponent != null)
            {
                _chooseHeroViewModel.SelectedHeroUser.Name = _chooseHeroViewModel.UserEnterdName;
                _chooseHeroViewModel.SelectedHeroOpponent.Name = _chooseHeroViewModel.OpponentEnterdName;

                NavigateToGameViewCommand.Execute(parameter);
            }
        }
    }
}
