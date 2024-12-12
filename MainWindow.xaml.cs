using System;
using System.Windows;

namespace YoutubePlaylistDownloader
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Content = new MainPage();
        }
    }
} 