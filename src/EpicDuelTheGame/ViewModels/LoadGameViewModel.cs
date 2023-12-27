using EpicDuelTheGame.Commands;
using EpicDuelTheGame.Services;
using EpicDuelTheGame.Stores;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EpicDuelTheGame.ViewModels
{
    public class LoadGameViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ObservableCollection<string> SavesList { get; set; }

        public ICommand NavigateStartViewCommand { get; }

        public ICommand LoadGameCommand { get; set; }

        public LoadGameViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            NavigateStartViewCommand = new NavigateCommand<StartViewModel>(new NavigationService<StartViewModel>(navigationStore, () => new StartViewModel(navigationStore)));

            SavesList = new ObservableCollection<string>();

            RefreshFileList();
        }

        private void RefreshFileList()
        {
            string folderPath = @"D:/EpicDuelTheGameSaves";

            if (Directory.Exists(folderPath))
            {
                SavesList.Clear();
                foreach (string filePath in Directory.GetFiles(folderPath, "*.json"))
                {
                    var save = Path.GetFileNameWithoutExtension(filePath);
                    SavesList.Add(save);
                }
            }
        }

        public void HandleListViewSelectedElement(object saveFileName)
        {
            LoadGameCommand = new LoadGameCommand(_navigationStore, saveFileName);
            LoadGameCommand.Execute(null);
        }
    }
}
