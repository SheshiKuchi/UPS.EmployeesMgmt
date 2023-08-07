using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UPS.EmployeesMgmt.Models;
using UPS.EmployeesMgmt.Services;

namespace UPS.EmployeesMgmt
{
    /// <summary>
    /// Interaction logic for EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Window
    {
        public EditEmployee()
        {
            InitializeComponent();
          
        }
        public Employee Employee { get; set; }
        RestApiData restApiData = new RestApiData();
        MainWindow mainWinRef = new MainWindow();
        public void ShowEmloyee(Employee emp)
        {
            TxtBxEmpId.Text = emp.EmpId.ToString();
            TxtBxEmail.Text = emp.Email;
            TxtBxEmpName.Text = emp.Name;
            TxtBxGender.Text = emp.Gender;
            TxtBxStatus.Text = emp.Status;
            Show();
           
        }

        private void BtnEditCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void TxtBxEmpId_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtBxEmpId.Text = Regex.Replace(TxtBxEmpId.Text, "[^0-9]+", "");
        }
        private void BtnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee editEmpRow = new Employee();
            editEmpRow.EmpId = Convert.ToInt32(TxtBxEmpId.Text);
            editEmpRow.Name=TxtBxEmpName.Text;
            editEmpRow.Gender=TxtBxGender.Text;
            editEmpRow.Status=TxtBxStatus.Text;
            editEmpRow.Email=TxtBxEmail.Text;
            UpdateEmployeeData(editEmpRow);
            this.Close();
            MessageBox.Show($"Employee-{editEmpRow.Name} record is updated, please click on Refresh Data button to see the updated data");
            
        }

        private async void UpdateEmployeeData(Employee emp)
        {
          
            await restApiData.RestApiOperation("users/"+emp.EmpId, "PUT", emp);
            mainWinRef.GetEmployees();
        
        }

        private async void BtnEmpDel_Click(object sender, RoutedEventArgs e)
        {
            var empId = Convert.ToInt32(TxtBxEmpId.Text);
            await restApiData.RestApiOperation("users/" + empId, "DELETE", new Employee());
            MessageBox.Show($"Employee-{empId} record is deleted!");
            //Refresh the main grid for updated data
            mainWinRef.BtnEmployees.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
