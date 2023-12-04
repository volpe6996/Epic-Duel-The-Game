using EpicDuelTheGame.Abstract;
using EpicDuelTheGame.Models;
using EpicDuelTheGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicDuelTheGame.Commands
{
    public class HandleHeroAttackCommand : CommandBase
    {
        private readonly GameViewModel _gameViewModel;
        private Hero _attacker;
        private Hero _victim;
        private int _activeOnTurn;

        public HandleHeroAttackCommand(GameViewModel gameViewModel, Hero attacker, Hero victim, int activeOnTurn)
        {
            _gameViewModel = gameViewModel;
            _attacker = attacker;
            _victim = victim;
            _activeOnTurn = activeOnTurn;

            gameViewModel.OnTurnChanged += OnTurnChanged;
        }

        public override void Execute(object parameter)
        {
            if (parameter.ToString() == "Weak")
                _attacker.WeakDamage(_victim);
            else if (parameter.ToString() == "Strong")
                _attacker.StrongDamage(_victim);
            else if (parameter.ToString() == "Ult")
                _attacker.UseUltimate(_victim);

            // zmiana tury po ruchu
            _gameViewModel.Turn = (_gameViewModel.Turn == 0) ? 1 : 0;
        }

        public override bool CanExecute(object parameter)
        {
            return _gameViewModel.Turn == _activeOnTurn;
        }

        private void OnTurnChanged()
        {
            OnCanExecuteChanged();
        }
    }
}
