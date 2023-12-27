using EpicDuelTheGame.Models;
using EpicDuelTheGame.Services;
using EpicDuelTheGame.Stores;
using EpicDuelTheGame.ViewModels;
using System.Windows.Input;

namespace EpicDuelTheGame.Commands
{
    // przypisujemy nazwy nadane przez usera dla samego siebie i opponenta oraz nawigujemy do GameView
    public class StartTheGameCommand : CommandBase
    {
        private readonly ChooseHeroViewModel _chooseHeroViewModel;
        private readonly NavigationStore _navigationStore;

        private Hero SelectedHeroUser;
        private Hero SelectedHeroOpponent;

        private readonly string _userHeroName;
        private readonly string _opponentHeroName;

        private ICommand NavigateToGameViewCommand { set; get; }

        public StartTheGameCommand(ChooseHeroViewModel chooseHeroViewModel, NavigationStore navigationStore)
        {
            _chooseHeroViewModel = chooseHeroViewModel;
            _navigationStore = navigationStore;

            _userHeroName = chooseHeroViewModel.UserEnterdName;
            _opponentHeroName = chooseHeroViewModel.OpponentEnterdName;
        }

        public override void Execute(object parameter)
        {
            if (_chooseHeroViewModel.SelectedHeroUser != null && _chooseHeroViewModel.SelectedHeroOpponent != null)
            {
                if (_chooseHeroViewModel.SelectedHeroUser.HeroType == HeroType.Warrior)
                    SelectedHeroUser = new Hero(_chooseHeroViewModel.UserEnterdName, HeroType.Warrior, PlayerType.User);
                else if (_chooseHeroViewModel.SelectedHeroUser.HeroType == HeroType.Sorcerer)
                    SelectedHeroUser = new Hero(_chooseHeroViewModel.UserEnterdName, HeroType.Sorcerer, PlayerType.User);
                else
                    SelectedHeroUser = new Hero(_chooseHeroViewModel.UserEnterdName, HeroType.Ranger, PlayerType.User);

                if (_chooseHeroViewModel.SelectedHeroOpponent.HeroType == HeroType.Warrior)
                    SelectedHeroOpponent = new Hero(_chooseHeroViewModel.OpponentEnterdName, HeroType.Warrior, PlayerType.Ai);
                else if (_chooseHeroViewModel.SelectedHeroOpponent.HeroType == HeroType.Sorcerer)
                    SelectedHeroOpponent = new Hero(_chooseHeroViewModel.OpponentEnterdName, HeroType.Sorcerer, PlayerType.Ai);
                else
                    SelectedHeroOpponent = new Hero(_chooseHeroViewModel.OpponentEnterdName, HeroType.Ranger, PlayerType.Ai);

                NavigateToGameViewCommand = new NavigateCommand<GameViewModel>(new NavigationService<GameViewModel>(_navigationStore, () => new GameViewModel(_navigationStore, SelectedHeroUser, SelectedHeroOpponent)));
                NavigateToGameViewCommand.Execute(parameter);
            }
        }
    }
}
