using Caliburn.Micro;
using System.Windows;
using WpfAppDemo.ViewModels;

namespace WpfAppDemo;

public class AppBootstrapper : BootstrapperBase
{
    private SimpleContainer _container;

    public AppBootstrapper()
    {
        Initialize();
    }

    protected override void Configure()
    {
        _container = new SimpleContainer();

        _container.Singleton<IWindowManager, WindowManager>();
        _container.Singleton<IEventAggregator, EventAggregator>();

        _container.PerRequest<ShellViewModel>();
        _container.PerRequest<LoginViewModel>();
        _container.PerRequest<MainViewModel>();
    }

    protected override object GetInstance(Type service, string key)
    {
        return _container.GetInstance(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
        return _container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
        _container.BuildUp(instance);
    }

    protected override async void OnStartup(object sender, StartupEventArgs e)
    {
        await DisplayRootViewForAsync<ShellViewModel>();
    }
}


#region "Old code"
//public class AppBootstrapper : BootstrapperBase
//{
//    private readonly SimpleContainer _container = new();
//    public AppBootstrapper()
//    {
//        Initialize();
//    }

//    protected override async void OnStartup(object sender, StartupEventArgs e)
//    {
//        //// Khởi chạy ứng dụng và hiển thị ShellViewModel đầu tiên
//        await DisplayRootViewForAsync<ShellViewModel>();
//    }

//    protected override void Configure()
//    {
//        _container.Singleton<IWindowManager, WindowManager>();
//        _container.Singleton<IEventAggregator, EventAggregator>();

//        //_container.PerRequest<LoginViewModel>();
//        //_container.PerRequest<ShellViewModel>();
//    }
//}
#endregion "Old code"
