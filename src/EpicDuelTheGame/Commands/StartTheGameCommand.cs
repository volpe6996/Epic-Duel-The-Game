using EpicDuelTheGame.Models;
using EpicDuelTheGame.Services;
using EpicDuelTheGame.Stores;
using EpicDuelTheGame.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private ICommand NavigateToGameViewCommand { set; get; }

        public StartTheGameCommand(ChooseHeroViewModel chooseHeroViewModel, NavigationStore navigationStore)
        {
            _chooseHeroViewModel = chooseHeroViewModel;
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            if (_chooseHeroViewModel.SelectedHeroUser != null && _chooseHeroViewModel.SelectedHeroOpponent != null)
            {
                if (_chooseHeroViewModel.SelectedHeroUser.HeroType == HeroTypes.Warrior)
                    SelectedHeroUser = new Warrior(_chooseHeroViewModel.UserEnterdName);
                else if(_chooseHeroViewModel.SelectedHeroUser.HeroType == HeroTypes.Sorcerer)
                    SelectedHeroUser = new Sorcerer(_chooseHeroViewModel.UserEnterdName);
                else
                    SelectedHeroUser = new Ranger(_chooseHeroViewModel.UserEnterdName);

                if (_chooseHeroViewModel.SelectedHeroOpponent.HeroType == HeroTypes.Warrior)
                    SelectedHeroOpponent = new Warrior(_chooseHeroViewModel.OpponentEnterdName);
                else if (_chooseHeroViewModel.SelectedHeroOpponent.HeroType == HeroTypes.Sorcerer)
                    SelectedHeroOpponent = new Sorcerer(_chooseHeroViewModel.OpponentEnterdName);
                else
                    SelectedHeroOpponent = new Ranger(_chooseHeroViewModel.OpponentEnterdName);

                NavigateToGameViewCommand = new NavigateCommand<GameViewModel>(new NavigationService<GameViewModel>(_navigationStore, () => new GameViewModel(_navigationStore, SelectedHeroUser, SelectedHeroOpponent)));
                NavigateToGameViewCommand.Execute(parameter);
            }
        }
    }
}
