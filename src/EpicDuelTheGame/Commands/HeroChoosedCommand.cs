using EpicDuelTheGame.Models;
using EpicDuelTheGame.ViewModels;

namespace EpicDuelTheGame.Commands
{
    public class HeroChoosedCommand : CommandBase
    {
        private readonly ChooseHeroViewModel _chooseHeroViewModel;
        private readonly string _selectionDirection;

        public HeroChoosedCommand(ChooseHeroViewModel chooseHeroViewModel, string userOrOpponent)
        {
            _chooseHeroViewModel = chooseHeroViewModel;
            _selectionDirection = userOrOpponent;
        }

        public override void Execute(object parameter)
        {
            if (_selectionDirection == "User")
                _chooseHeroViewModel.SelectedHeroUser = parameter as ChooseHeroModel;
            else if (_selectionDirection == "Opponent")
                _chooseHeroViewModel.SelectedHeroOpponent = parameter as ChooseHeroModel;

        }
    }
}
