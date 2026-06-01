using Caliburn.Micro;

namespace WpfAppDemo.ViewModels;

//Conductor<Screen> = Conductor<object>
public class ShellViewModel :
    Conductor<object>,
    IHandle<LoginSuccessEvent>
{
    private readonly IEventAggregator _eventAggregator;
    private readonly LoginViewModel _loginViewModel;
    private readonly MainViewModel _mainViewModel;

    public ShellViewModel(
        IEventAggregator eventAggregator,
        LoginViewModel loginViewModel,
        MainViewModel mainViewModel)
    {
        _eventAggregator = eventAggregator;

        _loginViewModel = loginViewModel;
        _mainViewModel = mainViewModel;

        _eventAggregator.SubscribeOnPublishedThread(this);
    }

    protected override async Task OnActivateAsync(
        CancellationToken cancellationToken)
    {
        await ActivateItemAsync(_loginViewModel, cancellationToken);
    }

    public async Task HandleAsync(
        LoginSuccessEvent message,
        CancellationToken cancellationToken)
    {
        await ActivateItemAsync(_mainViewModel, cancellationToken);
    }
}

//public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
//{
//    private readonly LoginViewModel _loginViewModel;

//    public ShellViewModel(LoginViewModel loginViewModel)
//    {
//        _loginViewModel = loginViewModel;
//    }

//    protected override async Task OnActivateAsync(System.Threading.CancellationToken cancellationToken)
//    {
//        await ActivateItemAsync(_loginViewModel, cancellationToken);
//    }

//    public async Task ShowMainView()
//    {
//        var mainView = IoC.Get<MainViewModel>();

//        await ActivateItemAsync(mainView);
//    }
//}

#region "Old code"
//public class ShellViewModel : Screen
//{
//    private string _firstName = "John";
//    private string _lastName = "Doe";

//    public string FirstName
//    {
//        get => _firstName;
//        set
//        {
//            _firstName = value;
//            NotifyOfPropertyChange(() => FirstName);
//            NotifyOfPropertyChange(() => FullName); // Cập nhật FullName khi FirstName thay đổi
//        }
//    }

//    public string LastName
//    {
//        get => _lastName;
//        set
//        {
//            _lastName = value;
//            NotifyOfPropertyChange(() => LastName);
//            NotifyOfPropertyChange(() => FullName);
//        }
//    }

//    public string FullName => $"{FirstName} {LastName}";

//    // Hàm này sẽ tự động map với Button có x:Name="ClearWindow"
//    public void ClearWindow()
//    {
//        FirstName = string.Empty;
//        LastName = string.Empty;
//    }
//}
#endregion
