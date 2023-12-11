using EpicDuelTheGame.Abstract;
using EpicDuelTheGame.Models;
using EpicDuelTheGame.ViewModels;
using System;
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

        public async Task Decide()
        {
            //if(_gameViewModel.Turn == 1)
            //    _gameViewModel.OpponentAttacksCommand.Execute("Weak");

            await Task.Delay(1750);

            if (_aiHero.Mana >= 50 && OperationAuthtorization(50))
                //_gameViewModel.OpponentUpIntelligenceCommand.Execute(_gameViewModel.UserHero);
                _gameViewModel.OpponentUpIntelligenceCommand.Execute(null);
            else if (_aiHero.Mana >= 50 && OperationAuthtorization(90))
                _gameViewModel.OpponentAttacksCommand.Execute("Ult");
            else
                _gameViewModel.OpponentAttacksCommand.Execute("Weak");

            //if(!_aiHero.IsFirstSpellActive)
            //    _gameViewModel.OpponentUseFirstSpellCommand.Execute(_gameViewModel.UserHero);
            //else
            //    _gameViewModel.OpponentAttacksCommand.Execute("Weak");

            //_gameViewModel.OpponentAttacksCommand.Execute("Ult");

            //_gameViewModel.OpponentAttacksCommand.Execute("Weak");

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
