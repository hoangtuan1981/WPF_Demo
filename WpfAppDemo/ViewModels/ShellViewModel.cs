using Caliburn.Micro;

namespace WpfAppDemo.ViewModels;

public class ShellViewModel : Screen
{
    private string _firstName = "John";
    private string _lastName = "Doe";

    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            NotifyOfPropertyChange(() => FirstName);
            NotifyOfPropertyChange(() => FullName); // Cập nhật FullName khi FirstName thay đổi
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            NotifyOfPropertyChange(() => LastName);
            NotifyOfPropertyChange(() => FullName);
        }
    }

    public string FullName => $"{FirstName} {LastName}";

    // Hàm này sẽ tự động map với Button có x:Name="ClearWindow"
    public void ClearWindow()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }
}