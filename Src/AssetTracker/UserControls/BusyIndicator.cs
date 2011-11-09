using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AssetTracker.UserControls
{
    [TemplateVisualState(Name = "Hidden", GroupName = "VisibilityStates"),
     TemplateVisualState(Name = "Visible", GroupName = "VisibilityStates"),
     TemplateVisualState(Name = "Idle", GroupName = "BusyStatusStates"),
     StyleTypedProperty(Property = "ProgressBarStyle", StyleTargetType = typeof (ProgressBar)),
     TemplateVisualState(Name = "Busy", GroupName = "BusyStatusStates"),
     StyleTypedProperty(Property = "OverlayStyle", StyleTargetType = typeof (Rectangle))]
    public class BusyIndicator : ContentControl
    {
        // Fields
        readonly DispatcherTimer _displayAfterTimer;
        public static readonly DependencyProperty BusyContentProperty = DependencyProperty.Register("BusyContent", typeof (object), typeof (BusyIndicator), new PropertyMetadata(null));
        public static readonly DependencyProperty BusyContentTemplateProperty = DependencyProperty.Register("BusyContentTemplate", typeof (DataTemplate), typeof (BusyIndicator), new PropertyMetadata(null));
        public static readonly DependencyProperty DisplayAfterProperty = DependencyProperty.Register("DisplayAfter", typeof (TimeSpan), typeof (BusyIndicator), new PropertyMetadata(TimeSpan.FromSeconds(0.1)));
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof (bool), typeof (BusyIndicator), new PropertyMetadata(false, OnIsBusyChanged));
        public static readonly DependencyProperty OverlayStyleProperty = DependencyProperty.Register("OverlayStyle", typeof (Style), typeof (BusyIndicator), new PropertyMetadata(null));
        public static readonly DependencyProperty ProgressBarStyleProperty = DependencyProperty.Register("ProgressBarStyle", typeof (Style), typeof (BusyIndicator), new PropertyMetadata(null));

        // Methods
        public BusyIndicator()
        {
            base.DefaultStyleKey = typeof (BusyIndicator);
            _displayAfterTimer = new DispatcherTimer();
            base.Loaded += delegate { _displayAfterTimer.Tick += DisplayAfterTimerElapsed; };
            base.Unloaded += delegate
            {
                _displayAfterTimer.Tick -= DisplayAfterTimerElapsed;
                _displayAfterTimer.Stop();
            };
        }

        protected virtual void ChangeVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, IsBusy ? "Busy" : "Idle", useTransitions);
            VisualStateManager.GoToState(this, IsContentVisible ? "Visible" : "Hidden", useTransitions);
        }

        void DisplayAfterTimerElapsed(object sender, EventArgs e)
        {
            _displayAfterTimer.Stop();
            IsContentVisible = true;
            ChangeVisualState(true);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ChangeVisualState(false);
        }

        protected virtual void OnIsBusyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (IsBusy)
            {
                if (DisplayAfter.Equals(TimeSpan.Zero))
                {
                    IsContentVisible = true;
                }
                else
                {
                    _displayAfterTimer.Interval = DisplayAfter;
                    _displayAfterTimer.Start();
                }
            }
            else
            {
                _displayAfterTimer.Stop();
                IsContentVisible = false;
            }
            ChangeVisualState(true);
        }

        static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BusyIndicator) d).OnIsBusyChanged(e);
        }

        // Properties
        public object BusyContent
        {
            get { return base.GetValue(BusyContentProperty); }
            set { base.SetValue(BusyContentProperty, value); }
        }

        public DataTemplate BusyContentTemplate
        {
            get { return (DataTemplate) base.GetValue(BusyContentTemplateProperty); }
            set { base.SetValue(BusyContentTemplateProperty, value); }
        }

        public TimeSpan DisplayAfter
        {
            get { return (TimeSpan) base.GetValue(DisplayAfterProperty); }
            set { base.SetValue(DisplayAfterProperty, value); }
        }

        public bool IsBusy
        {
            get { return (bool) base.GetValue(IsBusyProperty); }
            set { base.SetValue(IsBusyProperty, value); }
        }

        protected bool IsContentVisible { get; set; }

        public Style OverlayStyle
        {
            get { return (Style) base.GetValue(OverlayStyleProperty); }
            set { base.SetValue(OverlayStyleProperty, value); }
        }

        public Style ProgressBarStyle
        {
            get { return (Style) base.GetValue(ProgressBarStyleProperty); }
            set { base.SetValue(ProgressBarStyleProperty, value); }
        }
    }
}