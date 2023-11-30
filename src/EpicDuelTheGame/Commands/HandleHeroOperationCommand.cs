using EpicDuelTheGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicDuelTheGame.Commands
{
    public class HandleHeroOperationCommand : CommandBase
    {
        private Action _operation;
        private readonly GameViewModel _gameViewModel;

        public HandleHeroOperationCommand(GameViewModel gameViewModel, Action operation)
        {
            _gameViewModel = gameViewModel;
            _operation = operation;
        }

        public override void Execute(object parameter)
        {
            _operation.Invoke();
        }
    }
}
