using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace BingImageSearch.Common
{
    public sealed class StatusControl : Control
    {
        public StatusControl()
        {
            this.DefaultStyleKey = typeof(StatusControl);
        }

        public string TemporaryMessage
        {
            get { return (string)GetValue(TemporaryMessageProperty); }
            set { SetValue(TemporaryMessageProperty, value); }
        }

        public static readonly DependencyProperty TemporaryMessageProperty = DependencyProperty.Register("TemporaryMessage", typeof(string), typeof(StatusControl), new PropertyMetadata(null, OnTemporaryMessageChanged));

        private static void OnTemporaryMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace((string)e.NewValue))
            {
                VisualStateManager.GoToState((Control)d, "TemporaryMessageHidden", false);
                VisualStateManager.GoToState((Control)d, "TemporaryMessageVisible", true);
            }
        }

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register("IsLoading", typeof(bool), typeof(StatusControl), new PropertyMetadata(false, OnIsLoadingChanged));

        private static void OnIsLoadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState((Control)d, (bool)e.NewValue ? "Loading" : "NotLoading", true);
        }

        public string LoadingMessage
        {
            get { return (string)GetValue(LoadingMessageProperty); }
            set { SetValue(LoadingMessageProperty, value); }
        }

        public static readonly DependencyProperty LoadingMessageProperty = DependencyProperty.Register("LoadingMessage", typeof(string), typeof(StatusControl), new PropertyMetadata(null));

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        public static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register("ErrorMessage", typeof(string), typeof(StatusControl), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(StatusControl), new PropertyMetadata(null));

        public string CommandText
        {
            get { return (string)GetValue(CommandTextProperty); }
            set { SetValue(CommandTextProperty, value); }
        }

        public static readonly DependencyProperty CommandTextProperty = DependencyProperty.Register("CommandText", typeof(string), typeof(StatusControl), new PropertyMetadata(null, OnCommandTextChanged));

        private static void OnCommandTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState((Control)d, string.IsNullOrEmpty(e.NewValue as string) ? "HideCommand" : "ShowCommand", true);
        }
    }
}
