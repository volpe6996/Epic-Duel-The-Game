using EpicDuelTheGame.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace EpicDuelTheGame.Commands
{
    public class SaveGameCommand : CommandBase
    {
        private readonly GameViewModel _gameViewModel;

        public SaveGameCommand(GameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
            gameViewModel.OnTurnChanged += OnTurnChanged;
        }

        public override void Execute(object parameter)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            var saveName = $"Pojedynek {_gameViewModel.UserHero.Name} vs {_gameViewModel.OpponentHero.Name} - {currentDateTime}";

            Directory.CreateDirectory(@"D:\EpicDuelTheGameSaves");

            File.WriteAllText($@"D:\EpicDuelTheGameSaves\{saveName}.json", JsonConvert.SerializeObject(_gameViewModel, settings));

            using (StreamWriter file = File.CreateText($@"D:\EpicDuelTheGameSaves\{saveName}.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                serializer.Serialize(file, _gameViewModel);
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _gameViewModel.Turn == 0;
        }

        private void OnTurnChanged()
        {
            OnCanExecuteChanged();
        }
    }
}
