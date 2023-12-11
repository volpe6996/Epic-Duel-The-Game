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
                if (_chooseHeroViewModel.SelectedHeroUser.HeroType == HeroTypes.Warrior)
                    SelectedHeroUser = new Hero(_chooseHeroViewModel.UserEnterdName, HeroTypes.Warrior);
                else if (_chooseHeroViewModel.SelectedHeroUser.HeroType == HeroTypes.Sorcerer)
                    SelectedHeroUser = new Hero(_chooseHeroViewModel.UserEnterdName, HeroTypes.Sorcerer);
                else
                    SelectedHeroUser = new Hero(_chooseHeroViewModel.UserEnterdName, HeroTypes.Ranger);

                if (_chooseHeroViewModel.SelectedHeroOpponent.HeroType == HeroTypes.Warrior)
                    SelectedHeroOpponent = new Hero(_chooseHeroViewModel.OpponentEnterdName, HeroTypes.Warrior);
                else if (_chooseHeroViewModel.SelectedHeroOpponent.HeroType == HeroTypes.Sorcerer)
                    SelectedHeroOpponent = new Hero(_chooseHeroViewModel.OpponentEnterdName, HeroTypes.Sorcerer);
                else
                    SelectedHeroOpponent = new Hero(_chooseHeroViewModel.OpponentEnterdName, HeroTypes.Ranger);

                NavigateToGameViewCommand = new NavigateCommand<GameViewModel>(new NavigationService<GameViewModel>(_navigationStore, () => new GameViewModel(_navigationStore, SelectedHeroUser, SelectedHeroOpponent)));
                NavigateToGameViewCommand.Execute(parameter);
            }
        }
    }
}
