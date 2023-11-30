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
        private int _turn;

        public HandleHeroAttackCommand(GameViewModel gameViewModel, Hero attacker, Hero victim, int turn)
        {
            _gameViewModel = gameViewModel;
            _attacker = attacker;
            _victim = victim;

            gameViewModel.OnTurnChanged += OnTurnChanged;
            _turn = turn;
        }

        public override void Execute(object parameter)
        {
            if (parameter.ToString() == "Weak")
                _attacker.WeakDamage(_victim);
            else if (parameter.ToString() == "Strong")
                _attacker.StrongDamage(_victim);
            else if (parameter.ToString() == "Ult")
                _attacker.UseUltimate(_victim);


            if (_gameViewModel.Turn == 0)
                _gameViewModel.Turn = 1;
            else
                _gameViewModel.Turn = 0;

            CanExecute(parameter);
        }

        public override bool CanExecute(object parameter)
        {
            if (_gameViewModel.Turn == 0 && _turn == 0)
            {
                return base.CanExecute(parameter);
            }
            else if (_gameViewModel.Turn != 0 || _turn != 0)
            {
                return false;
            }

            if (_gameViewModel.Turn == 1 && _turn == 1)
            {
                return base.CanExecute(parameter);
            }
            else if (_gameViewModel.Turn != 1 || _turn != 1)
            {
                return false;
            }

            return true;
        }

        private void OnTurnChanged()
        {

            if (_gameViewModel.Turn == 0 && _turn == 0)
            {
                OnCanExecuteChanged();
            }

            if (_gameViewModel.Turn == 1 && _turn == 1)
            {
                OnCanExecuteChanged();
            }
        }
    }
}
