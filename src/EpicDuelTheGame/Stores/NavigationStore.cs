using EpicDuelTheGame.ViewModels;
using System;

namespace EpicDuelTheGame.Stores;

public class NavigationStore
{
    public event Action CurrentViewModelChanged;
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnCurrentViewModelChanged();

            // tez wersja alternatywna, zamiast voida ktory robi za fasade odrazu robimy invoke eventu
            // CurrentViewModelChanged?.Invoke();
        }
    }

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }
}