using EpicDuelTheGame.Services;
using EpicDuelTheGame.Stores;
using EpicDuelTheGame.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EpicDuelTheGame.Commands
{
    public class LoadGameCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        private GameViewModel _gameViewModel;

        private ICommand NavigateToGameViewCommand { set; get; }

        private readonly string SaveFileName;

        public LoadGameCommand(NavigationStore navigationStore, object saveFileName)
        {
            _navigationStore = navigationStore;
            SaveFileName = saveFileName.ToString();
        }

        public override void Execute(object parameter)
        {
            Deserialize();

            NavigateToGameViewCommand = new NavigateCommand<GameViewModel>(new NavigationService<GameViewModel>(_navigationStore, () => _gameViewModel));
            NavigateToGameViewCommand.Execute(parameter);
        }

        private void Deserialize()
        {
            string filePath = $@"D:\EpicDuelTheGameSaves\{SaveFileName}.json";

            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);

                GameViewModelMapper(JsonConvert.DeserializeObject<GameViewModel>(jsonContent));
            }
        }

        private void GameViewModelMapper(GameViewModel deserializedViewModel)
        {
            _gameViewModel = new GameViewModel(this._navigationStore, deserializedViewModel.UserHero, deserializedViewModel.OpponentHero);
            _gameViewModel.Turn = deserializedViewModel.Turn;
            _gameViewModel.NumberOfTurns = deserializedViewModel._numberOfTurns;
            _gameViewModel.Log = deserializedViewModel.Log;
            _gameViewModel.UserFirstSpellGreenZIndex = deserializedViewModel.UserFirstSpellGreenZIndex;
            _gameViewModel.UserFirstSpellRedZIndex = deserializedViewModel.UserFirstSpellRedZIndex;
            _gameViewModel.UserSecondSpellGreenZIndex = deserializedViewModel.UserSecondSpellGreenZIndex;
            _gameViewModel.UserSecondSpellRedZIndex = deserializedViewModel.UserSecondSpellRedZIndex;
        }
    }
}
