using Avalonia.Controls;
using Avalonia.Interactivity;

namespace FinalProject;

public partial class MainWindow : Window
{
    private DB db = new DB();
    public MainWindow()
    {
        InitializeComponent();
        db.CloseConnection();
    }

    private void CheckingWindow(object? sender, RoutedEventArgs e)
    {
        CheckingWindow win = new CheckingWindow();
        win.Show();
        this.Close();
    }

    private void AreasWindow(object? sender, RoutedEventArgs e)
    {
        AreasWindow win = new AreasWindow();
        win.Show();
        this.Close();
    }

    private void Exit(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}