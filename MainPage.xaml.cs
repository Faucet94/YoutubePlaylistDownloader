using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using YoutubePlaylistDownloader.ViewModels;
using System.Runtime.Versioning;

namespace YoutubePlaylistDownloader
{
    [SupportedOSPlatform("windows")]
    public partial class MainPage : UserControl
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = App.ServiceProvider.GetRequiredService<MainPageViewModel>();
            DataContext = _viewModel;

            GlobalConsts.HideHomeButton();
            GlobalConsts.ShowSettingsButton();
            GlobalConsts.ShowAboutButton();
            GlobalConsts.ShowHelpButton();
            GlobalConsts.MainPage = this;
        }

        public MainPage Load()
        {
            GlobalConsts.HideHomeButton();
            GlobalConsts.ShowSettingsButton();
            GlobalConsts.ShowAboutButton();
            GlobalConsts.ShowHelpButton();
            return this;
        }

        public void ChangeToQueueTab()
        {
            MetroAnimatedTabControl.SelectedItem = QueueMetroTabItem;
        }
    }
}
