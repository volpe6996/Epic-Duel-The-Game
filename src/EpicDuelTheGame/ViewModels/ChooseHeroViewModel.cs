using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using EpicDuelTheGame.Commands;
using EpicDuelTheGame.Models;
using EpicDuelTheGame.Services;
using EpicDuelTheGame.Stores;

namespace EpicDuelTheGame.ViewModels;

public class ChooseHeroViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public ICommand NavigateGameViewCommand { get; }

    public ICommand UserImageClickedCommand { get; }
    public ICommand OpponentImageClickedCommand { get; }

    private HeroImageModel _selectedUserImage;
    public HeroImageModel SelectedUserImage
    {
        get { return _selectedUserImage; }
        set
        {
            if (_selectedUserImage != value)
            {
                _selectedUserImage = value;
                OnPropertyChanged(nameof(SelectedUserImage));

                HeroHeader = $"Your hero is {SelectedUserImage.HeroName}";
                OnPropertyChanged(nameof(HeroHeader));
            }
        }
    }

    private HeroImageModel _selectedOpponentImage;
    public HeroImageModel SelectedOpponentImage
    {
        get { return _selectedOpponentImage; }
        set
        {
            if (_selectedOpponentImage != value)
            {
                _selectedOpponentImage = value;
                OnPropertyChanged(nameof(SelectedOpponentImage));

                OpponentHeader = $"Your opponent's hero is {SelectedOpponentImage.HeroName}";
                OnPropertyChanged(nameof(OpponentHeader));
            }
        }
    }

    public string HeroHeader { get; set; }
    public string OpponentHeader { get; set; }

    private ObservableCollection<HeroImageModel> _images;
    public ObservableCollection<HeroImageModel> Images
    {
        get { return _images; }
        set
        {
            if (_images != value)
                _images = value;
        }
    }

    public ChooseHeroViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;

        NavigateGameViewCommand = new NavigateCommand<GameViewModel>(
            new NavigationService<GameViewModel>(navigationStore, () => new GameViewModel(navigationStore)));

        Images = new ObservableCollection<HeroImageModel>
        {
            new HeroImageModel { HeroName="Poziomka", Path = "/images/poziomka.jpg" },
            new HeroImageModel { HeroName="Piston", Path = "/images/piston.jpg" },
            new HeroImageModel { HeroName="Menel", Path = "/images/menel.jpg" },
        };

        // sprawdziæ czy damy radê sensownie to po³¹czyæ
        UserImageClickedCommand = new UserImageClickedCommand(this);
        OpponentImageClickedCommand = new OpponentImageClickedCommand(this);
    }
}