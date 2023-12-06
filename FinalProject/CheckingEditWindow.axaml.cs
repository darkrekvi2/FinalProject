using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace FinalProject;

public partial class CheckingEditWindow : Window
{
    public CheckingEditWindow()
    {
        DB db = new DB();
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

    private void EditChecking(object? sender, RoutedEventArgs e)
    {
        DB db = new DB();

        string _sql = "SELECT * FROM Checkings";
        
        MySqlCommand command = new MySqlCommand("UPDATE Checkings SET employee = @employee, department = @department, " +
                                                "area = @area, check_date = @check_date, result = @result " +
                                                "WHERE check_id = @id",db.GetConnection());
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = Convert.ToInt32(idTextBox.Text);
        command.Parameters.Add("@employee", MySqlDbType.Int32).Value = EmployeeComboBox.SelectedIndex+1;
        command.Parameters.Add("@department", MySqlDbType.Int32).Value = DepartmentComboBox.SelectedIndex+1;
        command.Parameters.Add("@area", MySqlDbType.Int32).Value = AreaComboBox.SelectedIndex+1;
        command.Parameters.Add("@check_date", MySqlDbType.DateTime).Value = DateTextBox.SelectedDate;
        command.Parameters.Add("@result", MySqlDbType.Int32).Value = ResultComboBox.SelectedIndex+1;
        
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