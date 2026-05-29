# WPF_Demo

Demo project sử dụng **WPF + MVVM + Caliburn.Micro** để xây dựng ứng dụng desktop theo kiến trúc tách biệt UI và business logic.

---

# 1. Kiến trúc tổng thể

Project sử dụng:

- WPF (Windows Presentation Foundation)
- MVVM Pattern
- Caliburn.Micro Framework
- Dependency Injection
- Data Binding
- Convention over Configuration

Luồng hoạt động chính:

```text
User Action
    ↓
View (XAML)
    ↓ Binding
ViewModel
    ↓
Service / Business Logic
    ↓
Model
```

---

# 2. WPF hoạt động như thế nào?

## 2.1 Rendering Engine

WPF sử dụng:

- DirectX để render UI
- Vector-based rendering
- Hardware acceleration

=> UI scale tốt trên màn hình DPI cao.

WPF không render theo kiểu WinForms (GDI).

---

## 2.2 XAML

WPF dùng XAML để mô tả UI declarative.

Ví dụ:

```xml
<Button Content="Save" Width="120" />
```

XAML sẽ được compile thành:

- BAML
- Object tree runtime

Khi application chạy:

```text
XAML
→ Parse
→ Visual Tree
→ Render bằng DirectX
```

---

## 2.3 Visual Tree vs Logical Tree

## Logical Tree

Mô tả cấu trúc control logic.

Ví dụ:

```text
Window
 └── Grid
      └── Button
```

## Visual Tree

Bao gồm toàn bộ internal control template.

Ví dụ Button thực tế:

```text
Button
 └── Border
      └── ContentPresenter
```

Caliburn.Micro thường thao tác trên Logical Tree.

---

## 2.4 Dependency Property

WPF không dùng property thường cho UI state.

Nó dùng:

```csharp
DependencyProperty
```

Lợi ích:

- Binding
- Animation
- Styling
- Change notification
- Default value
- Value inheritance

Ví dụ:

```csharp
public static readonly DependencyProperty TitleProperty =
    DependencyProperty.Register(
        nameof(Title),
        typeof(string),
        typeof(MainWindow));
```

---

## 2.5 Data Binding

Core mạnh nhất của WPF.

Ví dụ:

```xml
<TextBox Text="{Binding UserName}" />
```

WPF sẽ:

```text
View ↔ Binding Engine ↔ ViewModel
```

Nếu ViewModel implement:

```csharp
INotifyPropertyChanged
```

thì UI auto update.

---

## 2.6 Commanding

WPF tránh xử lý event trong code-behind.

Thay vào đó dùng:

```csharp
ICommand
```

Ví dụ:

```xml
<Button Command="{Binding SaveCommand}" />
```

=> đúng kiến trúc MVVM.

---

# 3. MVVM trong project

## Model

Chứa:

- Entity
- DTO
- Business data

Ví dụ:

```csharp
public class Customer
{
    public string Name { get; set; }
}
```

---

## View

Chỉ chứa:

- XAML
- UI layout
- Binding

Không chứa business logic.

Ví dụ:

```xml
<TextBox Text="{Binding CustomerName}" />
```

---

## ViewModel

Chứa:

- State của UI
- Commands
- Business flow
- Gọi service

Ví dụ:

```csharp
public class ShellViewModel : Screen
{
    public string Title { get; set; }

    public void Save()
    {
    }
}
```

---

# 4. Caliburn.Micro hoạt động như thế nào?

Caliburn.Micro là framework MVVM tối giản cho WPF.

Nó giảm:

- Boilerplate code
- Manual binding
- Manual wiring
- Event handling

---

# 5. Bootstrap Application

Điểm khởi động:

```text
App.xaml
    ↓
Bootstrapper
    ↓
IoC Container
    ↓
ShellViewModel
    ↓
ShellView
```

Ví dụ:

```csharp
public class Bootstrapper : BootstrapperBase
{
    public Bootstrapper()
    {
        Initialize();
    }

    protected override void OnStartup(object sender, StartupEventArgs e)
    {
        DisplayRootViewFor<ShellViewModel>();
    }
}
```

---

# 6. Convention over Configuration

Điểm mạnh lớn nhất của Caliburn.Micro.

## Naming Convention

```text
ShellViewModel
↔
ShellView
```

Framework tự động bind ViewModel với View.

Không cần:

```xml
DataContext="..."
```

---

# 7. Action Message

Caliburn.Micro tự bind event → method.

Ví dụ:

```xml
<Button x:Name="Save" />
```

Tự động gọi:

```csharp
public void Save()
```

Không cần:

```xml
Command="{Binding SaveCommand}"
```

---

# 8. Property Notification

Caliburn.Micro cung cấp:

```csharp
PropertyChangedBase
```

hoặc:

```csharp
Screen
```

Ví dụ:

```csharp
public class ShellViewModel : Screen
{
    private string _title;

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            NotifyOfPropertyChange(() => Title);
        }
    }
}
```

---

# 9. Screen Lifecycle

`Screen` hỗ trợ lifecycle.

Ví dụ:

```csharp
OnInitialize()
OnActivate()
OnDeactivate()
CanCloseAsync()
```

Dùng cho:

- Navigation
- Dialog
- Cleanup resource
- Lazy loading

Ví dụ:

```csharp
protected override async Task OnActivateAsync(CancellationToken cancellationToken)
{
    await LoadData();
}
```

---

# 10. Conductor

Caliburn.Micro hỗ trợ quản lý nhiều screen.

Ví dụ:

```csharp
Conductor<IScreen>.Collection.OneActive
```

Dùng cho:

- Tab navigation
- Multi window
- Dynamic screen
- Region management

Ví dụ:

```csharp
public class ShellViewModel : Conductor<IScreen>
{
}
```

---

# 11. IoC Container

Caliburn.Micro hỗ trợ Dependency Injection.

Ví dụ:

```csharp
container.Singleton<ICustomerService, CustomerService>();
```

ViewModel constructor:

```csharp
public ShellViewModel(ICustomerService service)
{
}
```

---

# 12. Binding Convention của Caliburn.Micro

Ví dụ:

```xml
<TextBox x:Name="UserName" />
```

Tự động bind:

```csharp
public string UserName { get; set; }
```

---

# 13. Flow thực tế của project

## Startup

```text
App.xaml
→ Bootstrapper
→ IoC container
→ ShellViewModel
→ ShellView
```

---

## User click button

```text
Button Click
→ Caliburn Action
→ ViewModel Method
→ Service
→ Update Property
→ Notify UI
→ UI Refresh
```

---

# 14. Vì sao dùng Caliburn.Micro?

## Ưu điểm

- MVVM clean
- Ít boilerplate
- Convention-based
- Navigation mạnh
- Testable
- Dễ maintain
- Dễ scale project lớn

---

## Nhược điểm

- Magic convention khó debug cho người mới
- Hidden binding behavior
- Learning curve cao hơn MVVM cơ bản

---

# 15. Cấu trúc thư mục đề xuất

```text
WPF_Demo
│
├── Models
├── ViewModels
├── Views
├── Services
├── Helpers
├── Converters
├── Resources
├── Bootstrapper.cs
├── App.xaml
└── ShellView.xaml
```

---

# 16. Best Practices

## Nên

- Tách business logic khỏi View
- Dùng async/await
- Dùng service layer
- Dùng ObservableCollection
- Dùng DI
- Tách ResourceDictionary

---

## Không nên

- Viết logic trong code-behind
- Access database trực tiếp từ ViewModel
- Binding quá sâu
- UI thread blocking

---

# 17. Build & Run

## Requirements

- Visual Studio 2022+
- .NET SDK
- Windows 10/11

---

## Restore packages

```bash
dotnet restore
```

---

## Build

```bash
dotnet build
```

---

## Run

```bash
dotnet run
```

---

# 18. Kiến thức quan trọng cần hiểu khi đọc source

## WPF

- Visual Tree
- Logical Tree
- Binding Engine
- Dependency Property
- Routed Event
- Dispatcher Thread
- DataTemplate
- ControlTemplate

---

## Caliburn.Micro

- Screen
- Conductor
- EventAggregator
- Bootstrapper
- Convention Binding
- ActionMessage
- IoC

---

# 19. Root cause vì sao WPF + Caliburn.Micro phù hợp enterprise

## WPF giải quyết

- UI desktop mạnh
- Rich UI
- Data binding
- Hardware rendering

---

## MVVM giải quyết

- Separation of concerns
- Testability
- Maintainability

---

## Caliburn.Micro giải quyết

- Giảm boilerplate MVVM
- Tăng tốc development
- Navigation lifecycle
- Convention automation

---

# 20. Tài liệu tham khảo

- WPF Official Documentation
- Caliburn.Micro Documentation
- MVVM Pattern
- Dependency Injection
- Data Binding

---

# 21. Tổng kết

Project minh họa cách xây dựng ứng dụng desktop hiện đại bằng:

- WPF
- MVVM
- Caliburn.Micro
- Dependency Injection
- Convention-based architecture

Kiến trúc này phù hợp:

- ERP
- MES
- Trading system
- POS
- Enterprise desktop application
- Internal management system

vì dễ mở rộng, dễ test và dễ maintain trong production system.

