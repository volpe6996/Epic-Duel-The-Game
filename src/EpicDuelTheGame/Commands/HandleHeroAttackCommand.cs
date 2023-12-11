using EpicDuelTheGame.Models;
using EpicDuelTheGame.ViewModels;
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

        public override async void Execute(object parameter)
        {
            await _attacker.CheckSpellStatus(_victim);
            _victim.ClearLog();

            if (parameter.ToString() == "Weak")
                _attacker.WeakDamage(_victim);
            else if (parameter.ToString() == "Strong")
                _attacker.StrongDamage(_victim);
            else if (parameter.ToString() == "Ult")
                _attacker.UseUltimate(_victim);

            // zmiana tury po ruchu, ult warriora nie zmienia tury, darmowy ruch
            if (_attacker.HeroType == HeroTypes.Warrior && parameter.ToString() == "Ult")
                _gameViewModel.Turn += 0;
            else
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
