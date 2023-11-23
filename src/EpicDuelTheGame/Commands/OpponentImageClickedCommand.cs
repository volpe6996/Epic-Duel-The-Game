using EpicDuelTheGame.Models;
using EpicDuelTheGame.ViewModels;
using EpicDuelTheGame.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicDuelTheGame.Commands
{
    public class OpponentImageClickedCommand : CommandBase
    {
        private readonly ChooseHeroViewModel _chooseHeroViewModel;

        public OpponentImageClickedCommand(ChooseHeroViewModel chooseHeroViewModel)
        {
            _chooseHeroViewModel = chooseHeroViewModel;
        }

        public override void Execute(object parameter)
        {
            _chooseHeroViewModel.SelectedOpponentImage = (HeroImageModel)parameter;
        }
    }
}
