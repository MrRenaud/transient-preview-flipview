using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace TemplatedControl
{
    [TemplatePart(Name = PreviewPartName, Type = typeof(ListView))]
    public sealed class TransientPreviewFlipView : FlipView
    {
        private const string PreviewPartName = "PART_Preview";
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        public TransientPreviewFlipView()
        {
            DefaultStyleKey = typeof(TransientPreviewFlipView);
            PointerEntered += TransientPreviewFlipView_PointerEntered;

            _dispatcherTimer.Interval = TransientTimeSpan;
            _dispatcherTimer.Tick += _timer_Tick;
        }

        void _timer_Tick(object sender, object e)
        {
            VisualStateManager.GoToState(this, "Normal", true);
            _dispatcherTimer.Stop();
        }

        void TransientPreviewFlipView_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "PreviewEnabled", true);
            _dispatcherTimer.Start();
        }

        public static readonly DependencyProperty TransientTimeSpanProperty = DependencyProperty.Register(
            "TimeSpan", typeof(TimeSpan), typeof(TransientPreviewFlipView), new PropertyMetadata(new TimeSpan(0, 0, 0, 3)));

        public TimeSpan TransientTimeSpan
        {
            get { return (TimeSpan)GetValue(TransientTimeSpanProperty); }
            set { SetValue(TransientTimeSpanProperty, value); }
        }


        public static readonly DependencyProperty PreviewDataTemplateProperty = DependencyProperty.Register(
            "PreviewDataTemplate", typeof(DataTemplate), typeof(TransientPreviewFlipView), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate PreviewDataTemplate
        {
            get { return (DataTemplate)GetValue(PreviewDataTemplateProperty); }
            set { SetValue(PreviewDataTemplateProperty, value); }
        }


        protected override void OnApplyTemplate()
        {
            this._previewListView = this.GetTemplateChild(PreviewPartName) as ListView;
            if (this._previewListView != null)
            {
                _dispatcherTimer.Start();
            }

            base.OnApplyTemplate();
        }

        private ListView _previewListView;
    }
}
