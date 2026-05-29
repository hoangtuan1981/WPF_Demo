using System.Windows;
using System.Windows.Controls;

namespace WpfAppDemo.Helpers;


public static class PasswordBoxHelper
{
    private static bool _isUpdating = false;

    public static readonly DependencyProperty BindPasswordProperty =
        DependencyProperty.RegisterAttached(
            "BindPassword",
            typeof(bool),
            typeof(PasswordBoxHelper),
            new PropertyMetadata(false, OnBindPasswordChanged));

    public static readonly DependencyProperty BoundPasswordProperty =
        DependencyProperty.RegisterAttached(
            "BoundPassword",
            typeof(string),
            typeof(PasswordBoxHelper),
            new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

    public static bool GetBindPassword(DependencyObject dp)
    {
        return (bool)dp.GetValue(BindPasswordProperty);
    }

    public static void SetBindPassword(DependencyObject dp, bool value)
    {
        dp.SetValue(BindPasswordProperty, value);
    }

    public static string GetBoundPassword(DependencyObject dp)
    {
        return (string)dp.GetValue(BoundPasswordProperty);
    }

    public static void SetBoundPassword(DependencyObject dp, string value)
    {
        dp.SetValue(BoundPasswordProperty, value);
    }

    private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PasswordBox passwordBox)
        {
            if (_isUpdating)
                return;

            passwordBox.PasswordChanged -= PasswordChanged;
            passwordBox.Password = e.NewValue?.ToString() ?? "";
            passwordBox.PasswordChanged += PasswordChanged;
        }
    }

    private static void OnBindPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PasswordBox passwordBox)
        {
            if ((bool)e.NewValue)
                passwordBox.PasswordChanged += PasswordChanged;
            else
                passwordBox.PasswordChanged -= PasswordChanged;
        }
    }

    private static void PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is PasswordBox passwordBox)
        {
            _isUpdating = true;
            SetBoundPassword(passwordBox, passwordBox.Password);
            _isUpdating = false;
        }
    }
}



//public static class PasswordBoxHelper
//{
//    // Property để bật/tắt binding
//    public static readonly DependencyProperty BindPasswordProperty =
//        DependencyProperty.RegisterAttached(
//            "BindPassword",
//            typeof(bool),
//            typeof(PasswordBoxHelper),
//            new PropertyMetadata(false, OnBindPasswordChanged));

//    public static readonly DependencyProperty BoundPasswordProperty =
//        DependencyProperty.RegisterAttached(
//            "BoundPassword",
//            typeof(string),
//            typeof(PasswordBoxHelper),
//            new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

//    // Get/Set BindPassword
//    public static bool GetBindPassword(DependencyObject dp)
//    {
//        return (bool)dp.GetValue(BindPasswordProperty);
//    }

//    public static void SetBindPassword(DependencyObject dp, bool value)
//    {
//        dp.SetValue(BindPasswordProperty, value);
//    }

//    // Get/Set BoundPassword
//    public static string GetBoundPassword(DependencyObject dp)
//    {
//        return (string)dp.GetValue(BoundPasswordProperty);
//    }

//    public static void SetBoundPassword(DependencyObject dp, string value)
//    {
//        dp.SetValue(BoundPasswordProperty, value);
//    }

//    // Khi PasswordBox thay đổi => update ViewModel
//    private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//    {
//        if (d is PasswordBox passwordBox)
//        {
//            passwordBox.PasswordChanged -= PasswordChanged;
//            passwordBox.Password = (string)e.NewValue;
//            passwordBox.PasswordChanged += PasswordChanged;
//        }
//    }

//    // Khi bật BindPassword
//    private static void OnBindPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//    {
//        if (d is PasswordBox passwordBox)
//        {
//            bool wasBound = (bool)e.OldValue;
//            bool needToBind = (bool)e.NewValue;

//            if (wasBound)
//                passwordBox.PasswordChanged -= PasswordChanged;

//            if (needToBind)
//                passwordBox.PasswordChanged += PasswordChanged;
//        }
//    }

//    // Sync từ UI → ViewModel
//    private static void PasswordChanged(object sender, RoutedEventArgs e)
//    {
//        var passwordBox = sender as PasswordBox;
//        SetBoundPassword(passwordBox, passwordBox.Password);
//    }
//}