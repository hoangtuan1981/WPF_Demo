using Caliburn.Micro;
using WpfAppDemo.Views;

namespace WpfAppDemo.ViewModels;

public class LoginViewModel : Screen
{
    private readonly IEventAggregator _eventAggregator;

    public LoginViewModel(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
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

    //public async Task Login(string password)
    public async Task Login()//string password)
    {
        //Investigate PasswordBox in view
        //var view = (LoginView)this.GetView();
        //var password = view.GetPassword();
        //Password = password;
        if (Username == "admin" && Password == "123")
        {
            await _eventAggregator.PublishOnUIThreadAsync(
                new LoginSuccessEvent());
        }
    }
}