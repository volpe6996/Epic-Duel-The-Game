using EpicDuelTheGame.Models;
using EpicDuelTheGame.ViewModels;
using System;

namespace EpicDuelTheGame.Commands
{
    public class HandleHeroOperationCommand : CommandBase
    {
        private readonly GameViewModel _gameViewModel;
        private Func<bool> _operation = null;
        private Func<Hero, bool> _operationWithParameter = null;
        private int _activeOnTurn;
        private Hero _attacker;
        private Hero _victim;

        public HandleHeroOperationCommand(GameViewModel gameViewModel, Func<bool> operation, int activeOnTurn, Hero attacker, Hero victim)
        {
            _gameViewModel = gameViewModel;
            _operation = operation;
            _activeOnTurn = activeOnTurn;
            _attacker = attacker;
            _victim = victim;

            gameViewModel.OnTurnChanged += OnTurnChanged;
        }

        public HandleHeroOperationCommand(GameViewModel gameViewModel, Func<Hero, bool> operationWithParameter, int activeOnTurn, Hero attacker, Hero victim)
        {
            _gameViewModel = gameViewModel;
            _operationWithParameter = operationWithParameter;
            _activeOnTurn = activeOnTurn;
            _attacker = attacker;
            _victim = victim;

            gameViewModel.OnTurnChanged += OnTurnChanged;
        }

        public override async void Execute(object parameter)
        {
            bool changeTurn;

            await _attacker.CheckSpellStatus((Hero)parameter);
            _victim.ClearLog();

            if (_operation == null)
                changeTurn = _operationWithParameter.Invoke((Hero)parameter);
            else
                changeTurn = _operation.Invoke();

            _gameViewModel.Turn = (_gameViewModel.Turn == 0 && changeTurn) ? 1 : 0;
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
