using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using UPS.EmployeesMgmt.Models;
using UPS.EmployeesMgmt.Services;

namespace UPS.EmployeesMgmt
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        public AddEmployee()
        {
            InitializeComponent();
        }
        RestApiData restApiData= new RestApiData();
        private void TxtBxAddEmpId_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtBxAddEmpId.Text = Regex.Replace(TxtBxAddEmpId.Text, "[^0-9]+", "");
        }

        private void BtnAddCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void AddRecord(Employee emp)
        {
           await restApiData.RestApiOperation("users", "POST", emp);
           
        }

        private void BtnAddEmp_Click(object sender, RoutedEventArgs e)
        {
            Employee empRecord = new Employee();
            empRecord.EmpId = Convert.ToInt32(TxtBxAddEmpId.Text);
            empRecord.Name = TxtBxAddEmpName.Text;
            empRecord.Gender = TxtBxAddGender.Text;
            empRecord.Status = TxtBxAddStatus.Text;
            empRecord.Email = TxtBxAddEmail.Text;
            AddRecord(empRecord);
            this.Close();
            MessageBox.Show($"Employee- {empRecord.Name} is added! please click on Refresh Data button to see the updated data");
        }
    }
}
