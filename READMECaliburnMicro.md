1. Base Caliburn.Micro
    - Caliburn auto map giữa View, ViewModel, Model thông qua cách đặt tên.
    - Ví dụ: login form sẽ có: 
        - `LoginView.xaml`: View
        - `LoginViewModel.cs`: ViewModel
        - 

2. Bootstrapper
    1. class AppBootstrapper : BootstrapperBase
        - đăng ký DI cho container, các view models (LoginViewModel)
        - OnStartup: start-up 1 view thông qua view models
    - 

3. Screen
        - class LoginViewModel : Screen
        - Screen đại diện cho 1 màn hình/view đơn lẻ.
        - Nó cung cấp:
            - Lifecycle của ViewModel
            - Quản lý trạng thái active/deactive
            - Hỗ trợ binding notify
            - Hỗ trợ close màn hình
            - Các lifecycle quan trọng:
                OnInitialize()
                OnActivate()
                OnDeactivate()
                CanClose()
                TryClose()
            - Khi nào dùng Screen
                - ViewModel là màn hình độc lập
                - Không quản lý child screens
                - Không navigation nhiều màn hình
            - Screen Dùng cho:
                - Form
                - Dialog
                - Detail screen
                - Popup

4. Conductor<T>

        - class ShellViewModel : Conductor<object>, IHandle<LoginSuccessEvent>
        - Conductor<T> là class dùng để:
            - Quản lý các Screens/ViewModels khác
        - thường là:
            - MainWindow
            - Main layout
            - Navigation root

        - Chức năng chính:
            - ActivateItemAsync(new CustomerViewModel()); : Hiển thị screen
            - DeactivateItemAsync(screen, true); : Đóng screen
            - await ActivateItemAsync(new DashboardViewModel()); : Chuyển màn hình
        - Các loại Conductor phổ biến
            - Conductor<object> : 
                - Chỉ active 1 screen tại 1 thời điểm
            - Conductor.Collection.OneActive: 
                - Nhiều screens nhưng chỉ 1 active.
            - Conductor.Collection.AllActive: 
                - Nhiều screens active cùng lúc.
                - Dashboard widgets, Dock panels
            - Conductor Dùng cho:
                - Navigation
                - Multi tabs
                - Window management
                - Dynamic screen hosting
            - So sánh:
                - Conductor<object>
                - Conductor<Screen>.Collection.OneActive
                => Conductor<object> = Conductor<Screen>



5. IHandle<T> (EventAggregator)

    - IHandle<T> là implementation của EventAggregator pattern trong Caliburn.
    - So sánh Caliburn.Micro IEventAggregator.PublishOnBackgroundThreadAsync và IEventAggregator.PublishOnUIThreadAsync
    - Class này lắng nghe event: LoginSuccessEvent
    - Flow thực tế:
        - LoginViewModel publish event:
            await _eventAggregator.PublishOnUIThreadAsync(new LoginSuccessEvent());
        - ShellViewModel nhận event:

            public class ShellViewModel : Conductor<object>, IHandle<LoginSuccessEvent>
            {
                public async Task HandleAsync(
                    LoginSuccessEvent message,
                    CancellationToken cancellationToken)
                {
                    await ActivateItemAsync(
                        new DashboardViewModel());
                }
            }

        - Hoặc: 
            - Inject
                private readonly IEventAggregator _eventAggregator;

                public LoginViewModel(IEventAggregator eventAggregator)
                {
                    _eventAggregator = eventAggregator;
                }
            - Subscribe
                _eventAggregator.Subscribe(this);

    - Kiến trúc đúng

        - LoginViewModel --> publish event
        - ShellViewModel --> điều phối navigation
        - => Loose coupling.


    - Mapping tư duy

        WPF Caliburn vs Web Analogy

        | WPF Caliburn        | Web Analogy                     |
        |-------------------|--------------------------------|
        | Screen            | React component / page         |
        | Conductor         | Router / navigation manager    |
        | EventAggregator   | Event bus / message bus        |





https://caliburnmicro.com/documentation/Tutorials/WPF/AddDialogToShellView

public Task About()
{
  return _windowManager.ShowDialogAsync(IoC.Get<AboutViewModel>());
}

_windowManager.ShowWindowAsync(IoC.Get<AboutViewModel>());  

https://caliburnmicro.com/documentation/actions
    [Export("Shell", typeof(IShell))]

    $eventArgs
    $dataContext
    $source
    $view
    $executionContext
    $this
    