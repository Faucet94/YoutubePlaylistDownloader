using System.Windows;
using System.Windows.Controls;

namespace YoutubePlaylistDownloader.Controls
{
    public partial class GlassMorphicPanel : UserControl
    {
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(double), typeof(GlassMorphicPanel), 
                new PropertyMetadata(15.0));

        public static readonly DependencyProperty GlassOpacityProperty =
            DependencyProperty.Register("GlassOpacity", typeof(double), typeof(GlassMorphicPanel),
                new PropertyMetadata(0.1, OnGlassOpacityChanged));

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public double GlassOpacity
        {
            get => (double)GetValue(GlassOpacityProperty);
            set => SetValue(GlassOpacityProperty, value);
        }

        public GlassMorphicPanel()
        {
            InitializeComponent();
        }

        private static void OnGlassOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GlassMorphicPanel panel)
            {
                panel.GlassBackground.Opacity = (double)e.NewValue;
            }
        }
    }
} 