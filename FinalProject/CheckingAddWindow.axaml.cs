using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace FinalProject;

public partial class CheckingAddWindow : Window
{
    public DB db = new DB();
    
        
    public CheckingAddWindow()
    {
        InitializeComponent();
        using (db.GetConnection())
        {
            db.OpenConnection();
            
            var query = "SELECT employee_name FROM Employees";
            using (var command = new MySqlCommand(query, db.GetConnection()))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    {
                        EmployeeComboBox.Items.Add(reader.GetString("employee_name"));     
                    }
                }    
            }
            
            db.CloseConnection();
        }
        
        using (db.GetConnection())
        {
            db.OpenConnection();
            
            var query = "SELECT department_name FROM Departments";
            using (var command = new MySqlCommand(query, db.GetConnection()))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    { 
                        DepartmentComboBox.Items.Add(reader.GetString("department_name"));    
                    }
                }    
            }
            
            db.CloseConnection();
        }
        
        using (db.GetConnection())
        {
            db.OpenConnection();
            
            var query = "SELECT area_name FROM Areas";
            using (var command = new MySqlCommand(query, db.GetConnection()))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    {   
                        AreaComboBox.Items.Add(reader.GetString("area_name"));      
                    }
                }    
            }
            
            db.CloseConnection();
        }
        
        using (db.GetConnection())
        {
            db.OpenConnection();
            
            var query = "SELECT result_name FROM Results";
            using (var command = new MySqlCommand(query, db.GetConnection()))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    {  
                        ResultComboBox.Items.Add(reader.GetString("result_name"));    
                    }
                }    
            }
            
            db.CloseConnection();
        }
    }

    private void AddChecking(object? sender, RoutedEventArgs e)
    {
        MySqlCommand command = new MySqlCommand("INSERT INTO sql12659906.Checkings (employee, department, area, check_date, result)" +
                                                "VALUES (@employee, @department, @area,@check_date,@result)",db.GetConnection());
        command.Parameters.Add("@employee", MySqlDbType.VarChar).Value = EmployeeComboBox.SelectedIndex+1;
        command.Parameters.Add("@department", MySqlDbType.VarChar).Value = DepartmentComboBox.SelectedIndex+1;
        command.Parameters.Add("@area", MySqlDbType.VarChar).Value = AreaComboBox.SelectedIndex+1;
        command.Parameters.Add("@check_date", MySqlDbType.DateTime).Value = DateTextBox.SelectedDate;
        command.Parameters.Add("@result", MySqlDbType.VarChar).Value = ResultComboBox.SelectedIndex+1;
        
        db.OpenConnection();
        command.ExecuteNonQuery();
        db.CloseConnection();
        
        CheckingWindow w = new CheckingWindow();
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