using EpicDuelTheGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EpicDuelTheGame.Views
{
    /// <summary>
    /// Interaction logic for LoadGameView.xaml
    /// </summary>
    public partial class LoadGameView : UserControl
    {
        public event Action<object> OnListViewSelectedElement;

        public LoadGameView()
        {
            InitializeComponent();
        }

        private void DoubleClickEvent(object saveFileName, MouseButtonEventArgs e)
        {
            this.OnListViewSelectedElement += ((LoadGameViewModel)DataContext).HandleListViewSelectedElement;

            OnListViewSelectedElement?.Invoke(((ListViewItem)saveFileName).Content);
        }
    }
}
