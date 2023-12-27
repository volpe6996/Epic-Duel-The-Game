using EpicDuelTheGame.ViewModels;
using Newtonsoft.Json;
using System;

namespace EpicDuelTheGame.Stores;

public class NavigationStore
{
    public event Action CurrentViewModelChanged;
    [JsonProperty]
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }
}