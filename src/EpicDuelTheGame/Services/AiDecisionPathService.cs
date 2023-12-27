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
            await Task.Delay(1750);

            if (_aiHero.HeroType == HeroType.Warrior)
            {
                if (_aiHero.Mana >= Globals.WARRIOR_UP_INTELLIGENCE_MANA_REQ && OperationAuthtorization(85))
                {
                    if(OperationAuthtorization(65))
                        _gameViewModel.OpponentUpIntelligenceCommand.Execute(null);
                    else
                        _gameViewModel.OpponentAttacksCommand.Execute("Ult");
                }
                else if (_aiHero.Mana >= Globals.WARRIOR_SPELL1_MANA_REQ && (!_aiHero.IsFirstSpellActive && !_aiHero.IsSecondSpellActive) && OperationAuthtorization(10))
                    _gameViewModel.OpponentUseFirstSpellCommand.Execute(_gameViewModel.UserHero);
                else if (_aiHero.Mana >= Globals.STRONG_ATTACK_MANA_REQ && OperationAuthtorization(15))
                    _gameViewModel.OpponentAttacksCommand.Execute("Strong");
                else if (_aiHero.Mana >= Globals.WARRIOR_SPELL2_MANA_REQ && (!_aiHero.IsSecondSpellActive && !_aiHero.IsFirstSpellActive) && OperationAuthtorization(10))
                    _gameViewModel.OpponentUseSecondSpellCommand.Execute(_gameViewModel.UserHero);
                else
                    _gameViewModel.OpponentAttacksCommand.Execute("Weak");
            }
            else if (_aiHero.HeroType == HeroType.Sorcerer)
            {
                if (_aiHero.Mana >= Globals.SORCERER_UP_INTELLIGENCE_MANA_REQ && OperationAuthtorization(75))
                {
                    if (OperationAuthtorization(60))
                        _gameViewModel.OpponentUpIntelligenceCommand.Execute(null);
                    else
                        _gameViewModel.OpponentAttacksCommand.Execute("Ult");
                }
                else if (_aiHero.Mana >= Globals.SORCERER_SPELL1_MANA_REQ && (!_aiHero.IsFirstSpellActive && !_aiHero.IsSecondSpellActive) && OperationAuthtorization(10))
                    _gameViewModel.OpponentUseFirstSpellCommand.Execute(_gameViewModel.UserHero);
                else if (_aiHero.Mana >= Globals.STRONG_ATTACK_MANA_REQ && OperationAuthtorization(15))
                    _gameViewModel.OpponentAttacksCommand.Execute("Strong");
                else if (_aiHero.Mana >= Globals.SORCERER_SPELL2_MANA_REQ && (!_aiHero.IsSecondSpellActive && !_aiHero.IsFirstSpellActive) && OperationAuthtorization(10))
                    _gameViewModel.OpponentUseSecondSpellCommand.Execute(_gameViewModel.UserHero);
                else
                    _gameViewModel.OpponentAttacksCommand.Execute("Weak");
            }
            else if (_aiHero.HeroType == HeroType.Ranger)
            {
                if(_aiHero.Mana >= Globals.RANGER_ULT_STRONG_ATTACK_MANA_REQ && _aiHero.IsFirstSpellActive)
                {
                    _gameViewModel.OpponentAttacksCommand.Execute("Strong");
                }
                else
                {
                    if (_aiHero.Mana >= Globals.RANGER_UP_INTELLIGENCE_MANA_REQ && OperationAuthtorization(55))
                        _gameViewModel.OpponentUpIntelligenceCommand.Execute(null);
                    else if (_aiHero.Mana >= Globals.RANGER_ULT_MANA_REQ && OperationAuthtorization(35))
                        _gameViewModel.OpponentAttacksCommand.Execute("Ult");
                    else if (_aiHero.Mana >= Globals.RANGER_SPELL1_MANA_REQ && (!_aiHero.IsFirstSpellActive && !_aiHero.IsSecondSpellActive) && OperationAuthtorization(10))
                        _gameViewModel.OpponentUseFirstSpellCommand.Execute(_gameViewModel.UserHero);
                    else if (_aiHero.Mana >= Globals.STRONG_ATTACK_MANA_REQ && OperationAuthtorization(15))
                        _gameViewModel.OpponentAttacksCommand.Execute("Strong");
                    else if (_aiHero.Mana >= Globals.RANGER_SPELL2_MANA_REQ && (!_aiHero.IsSecondSpellActive && !_aiHero.IsFirstSpellActive) && OperationAuthtorization(10))
                        _gameViewModel.OpponentUseSecondSpellCommand.Execute(_gameViewModel.UserHero);
                    else
                        _gameViewModel.OpponentAttacksCommand.Execute("Weak");
                }
            }

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
