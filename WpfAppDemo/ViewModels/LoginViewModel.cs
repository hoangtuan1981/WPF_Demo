using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfAppDemo.ViewModels;

public class LoginViewModel : Screen
{
    private readonly ShellViewModel _shellViewModel;

    public LoginViewModel(ShellViewModel shellViewModel)
    {
        _shellViewModel = shellViewModel;
    }

    private string _username;
    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            NotifyOfPropertyChange(() => Username);
        }
    }

    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            NotifyOfPropertyChange(() => Password);
        }
    }

    public async Task Login()
    {
        // fake login
        if (Username == "admin" && Password == "123")
        {
            await _shellViewModel.ShowMainView();
        }
    }
}