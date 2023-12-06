using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace FinalProject;

public partial class AreaAddWindow : Window
{
    public AreaAddWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void AddArea(object? sender, RoutedEventArgs e)
    {
        DB db = new DB();
        MySqlCommand command = new MySqlCommand("INSERT INTO sql12659906.Areas (name)" +
                                                "VALUES (@name)",db.GetConnection());
        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = AreaNameTextBox.Text;
        
        db.OpenConnection();
        command.ExecuteNonQuery();
        db.CloseConnection();
        
        AreasWindow w = new AreasWindow();
        w.Show();
        this.Close();
    }

    private void Cancel(object? sender, RoutedEventArgs e)
    {
        MainWindow win = new MainWindow();
        win.Show();
        this.Close();
    }
}