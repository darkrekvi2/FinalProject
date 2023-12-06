using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace FinalProject;

public partial class AreasWindow : Window
{
    private string _sql =
        "select * from Areas ";
    public DB _Db = new DB();
    private List<Area> _areas;
    
    public AreasWindow()
    {
        _Db.CloseConnection();
        InitializeComponent();
        ShowTable(_sql);
    }

    public void ShowTable(string sqlstring)
    {
        _Db.OpenConnection();
        MySqlConnection _connection = _Db.GetConnection();
        _areas = new List<Area>();
        MySqlCommand command = new MySqlCommand(sqlstring, _connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read() && reader.HasRows)
        {
            var current = new Area()
            {
                Area_ID = reader.GetInt32("id"),
                Name = reader.GetString("area_name")
            };
            _areas.Add(current);
        }
        _Db.CloseConnection();
        AreaGrid.ItemsSource = _areas;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow win = new MainWindow();
        win.Show();
        this.Close();
    }

    private void AddChecking(object? sender, RoutedEventArgs e)
    {
        AreaAddWindow win = new AreaAddWindow();
        win.Show();
        this.Close();
    }

    private void Delete(object? sender, RoutedEventArgs e)
    {
        DB db = new DB();
        using (db.GetConnection())
        {
            db.OpenConnection();
            using (var cmd = new MySqlCommand("DELETE FROM Areas WHERE check_id LIKE " + idTextBox.Text, db.GetConnection()))
            {
                cmd.ExecuteNonQuery();
                ShowTable(_sql);
                db.CloseConnection();
            }
        }
    }

    public bool check = true;
    private void Sorting(object? sender, RoutedEventArgs e)
    {
        if (check == true)
        {
            check = false;
            ShowTable(
                "select * from Areas " +
                "order by id asc");
            
        }

        else
        {
            check = true;
            ShowTable(
                "select * from Areas " +
                "order by id desc");
            
        }
    }
    private void TxtSearch_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        string searchSql = "select * from Areas " +
                           "WHERE area_name LIKE '%" +
                           txtSearch.Text + "%';";
        ShowTable(searchSql);
    }
}