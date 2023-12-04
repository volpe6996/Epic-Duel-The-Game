using EpicDuelTheGame.Abstract;
using EpicDuelTheGame.Models;
using EpicDuelTheGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicDuelTheGame.Services
{
    public class AiDecisionPathService : IRandomAuthtorization
    {
        private GameViewModel _gameViewModel;
        private Hero _aiHero;

        public AiDecisionPathService(GameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
            _aiHero = gameViewModel.OpponentHero;
        }

        public void Decide()
        {
            //if(_gameViewModel.Turn == 1)
            //    _gameViewModel.OpponentAttacksCommand.Execute("Weak");

            if(_aiHero.Mana >= 50 && OperationAuthtorization(65))
                _gameViewModel.OpponentUpIntelligenceCommand.Execute(null);
            else
                _gameViewModel.OpponentAttacksCommand.Execute("Weak");
        }

        // skala 1%
        public bool OperationAuthtorization(int successRate)
        {
            var rng = new Random();
            var upperBound = successRate;
            var rngNbr = rng.Next(0, 101);

            return rngNbr <= upperBound;
        }
    }
}
