using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WpfAppDemo.ViewModels;

namespace WpfAppDemo;

public class AppBootstrapper : BootstrapperBase
{
    private readonly SimpleContainer _container = new();
    public AppBootstrapper()
    {
        Initialize();
    }

    protected override async void OnStartup(object sender, StartupEventArgs e)
    {
        //// Khởi chạy ứng dụng và hiển thị ShellViewModel đầu tiên
        await DisplayRootViewForAsync<ShellViewModel>();
    }

    protected override void Configure()
    {
        _container.Singleton<IWindowManager, WindowManager>();
        _container.Singleton<IEventAggregator, EventAggregator>();

        //_container.PerRequest<LoginViewModel>();
        //_container.PerRequest<ShellViewModel>();
    }
}