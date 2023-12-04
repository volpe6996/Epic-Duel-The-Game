using EpicDuelTheGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EpicDuelTheGame.Commands
{
    public class HandleHeroOperationCommand : CommandBase
    {
        private Action _operation;
        private readonly GameViewModel _gameViewModel;
        private int _activeOnTurn;

        public HandleHeroOperationCommand(GameViewModel gameViewModel, Action operation, int activeOnTurn)
        {
            _gameViewModel = gameViewModel;
            _operation = operation;
            _activeOnTurn = activeOnTurn;

            gameViewModel.OnTurnChanged += OnTurnChanged;
        }

        public override void Execute(object parameter)
        {
            _operation.Invoke();

            // zmiana tury po ruchu
            _gameViewModel.Turn = (_gameViewModel.Turn == 0) ? 1 : 0;
        }

        public override bool CanExecute(object parameter)
        {
            return _gameViewModel.Turn == _activeOnTurn && _gameViewModel.UserHero.Mana >= 50;
        }

        private void OnTurnChanged()
        {
            OnCanExecuteChanged();
        }
    }
}
