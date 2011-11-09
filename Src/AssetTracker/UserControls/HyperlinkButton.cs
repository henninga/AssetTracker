using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace AssetTracker.UserControls
{
    [TemplateVisualState(Name = "Disabled", GroupName = "CommonStates"),
     TemplateVisualState(Name = "Normal", GroupName = "CommonStates"),
     TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates"),
     TemplateVisualState(Name = "Pressed", GroupName = "CommonStates"),
     TemplateVisualState(Name = "Unfocused", GroupName = "FocusStates"),
     TemplateVisualState(Name = "Focused", GroupName = "FocusStates")]
    public class HyperlinkButton : ButtonBase
    {
        // Fields
        public static readonly DependencyProperty NavigateUriProperty = DependencyProperty.Register("NavigateUri", typeof (Uri), typeof (HyperlinkButton), null);
        public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register("TargetName", typeof (string), typeof (HyperlinkButton), null);

        // Methods
        public HyperlinkButton()
        {
            base.DefaultStyleKey = typeof (HyperlinkButton);
        }


        public void ChangeVisualState(bool useTransitions)
        {
            if (!base.IsEnabled)
            {
                VisualStateManager.GoToState(this, "Disabled", useTransitions);
            }
            else if (base.IsPressed)
            {
                VisualStateManager.GoToState(this, "Pressed", useTransitions);
            }
            else if (base.IsMouseOver)
            {
                VisualStateManager.GoToState(this, "MouseOver", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Normal", useTransitions);
            }
            if (base.IsFocused && base.IsEnabled)
            {
                VisualStateManager.GoToState(this, "Focused", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Unfocused", useTransitions);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ChangeVisualState(false);
        }

        // Properties
        [TypeConverter(typeof (UriTypeConverter))]
        public Uri NavigateUri
        {
            get { return (base.GetValue(NavigateUriProperty) as Uri); }
            set { base.SetValue(NavigateUriProperty, value); }
        }

        public string TargetName
        {
            get { return (base.GetValue(TargetNameProperty) as string); }
            set { base.SetValue(TargetNameProperty, value); }
        }
    }
}