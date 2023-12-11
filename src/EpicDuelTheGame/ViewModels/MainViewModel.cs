using EpicDuelTheGame.Stores;

namespace EpicDuelTheGame.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

    public MainViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

        // wersja alternatywna, zamiast osobnego voida robimy lambde z OpPropertyChanged
        // _navigationStore.CurrentViewModelChanged += () => OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}