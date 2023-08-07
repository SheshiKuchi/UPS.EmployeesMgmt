using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UPS.EmployeesMgmt.Models;
using UPS.EmployeesMgmt.Services;

namespace UPS.EmployeesMgmt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       

        public MainWindow()
        {
            InitializeComponent();            
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

       /// <summary>
       /// fetches the employees data by calling restapi
       /// </summary>
       public async void GetEmployees()
        {
            GrdEmpList.ItemsSource = null;
            var restApiData = new RestApiData();
            string response = await restApiData.RestApiOperation("users","Get", new Employee());
           var employees = JsonConvert.DeserializeObject<List<Employee>>(response);        
            GrdEmpList.ItemsSource = employees;
            LblEmployees.Content = $"Employee(s) : {(employees!=null?employees.Count:0)}";
        }
        private void BtnEmployees_Click(object sender, RoutedEventArgs e)
        {
            GetEmployees();
        }

        private void GrdEmpList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            var emp = row.DataContext as Employee;
            var employeeRef = new EditEmployee();
            employeeRef.Owner = this;
            employeeRef.ShowEmloyee(emp);
        }

        /// <summary>
        /// load the employee data in window load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetEmployees();
        }

        private void BtnEmpAdd_Click(object sender, RoutedEventArgs e)
        {
           
            AddEmployee addEmpRef = new AddEmployee();
            addEmpRef.Owner = this;
            addEmpRef.Show();
           
        }
    }
}
