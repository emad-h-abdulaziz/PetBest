using LoginWindow.ApiClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoginWindow
{
    /// <summary>
    /// Interaction logic for employeeAppoinments.xaml
    /// </summary>
    public partial class employeeAppoinments : Window
    {
        List<OrderModel> assignedOrders;
        List<UserModel> employees;
        DateTime presentDate = DateTime.Today;
        DateTime futureDate = DateTime.Now;
        string text;
        
        private UserModel currentUser;
        public employeeAppoinments(UserModel user)
        {
            InitializeComponent();
            currentUser = user;
        }

        private async void EmployeeAppointmentsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            assignedOrders = await OrderProcessor.ShowAllProgressOrders(currentUser.Privileges, currentUser.Token);
            employees = await UserProcessor.ShowAllEmployees(currentUser.Token);

            foreach(UserModel m in employees)
            {
                cmbEmployees.Items.Add(m.Firstname + " "  + m.Lastname);
            }

            text = this.cmbEmployees.Text;
            populateListboxes(text);
        }

        // Added some code to make listbox populate depending on time frame -- LR 

        private void populateListboxes(string text)
        {
            
            lstUpcomingAppointments.Items.Clear();



            if (assignedOrders != null)
            {

                foreach (OrderModel m in assignedOrders)
                {
                    if (cmbEmployees.SelectedIndex == 0)
                    {

                        if (cmbApptTime.SelectedIndex == 0)
                        {


                            if (DateTime.Parse(m.ServiceDateRequest) >= presentDate)
                            {
                                if (DateTime.Parse(m.ServiceDateRequest) <= presentDate.AddDays(7))
                                {

                                    lstUpcomingAppointments.Items.Add(m);
                                }
                            }
                        }

                        if (cmbApptTime.SelectedIndex == 1)
                        {


                            if (DateTime.Parse(m.ServiceDateRequest) >= presentDate)
                            {
                                if (DateTime.Parse(m.ServiceDateRequest) <= presentDate.AddDays(30))
                                {
                                    lstUpcomingAppointments.Items.Add(m);
                                }
                            }
                        }

                    }
                    else
                    {

                        PopulateEmployeeListBox();
                        return;

                    }
                }
            }

           
        }

        // If a user wants to go into an appt and change status this is how they will do it -- LR

        private void BtnManage_Click(object sender, RoutedEventArgs e)
        {
            
            
            OrderModel selectedOrder = (OrderModel)lstUpcomingAppointments.SelectedItem;
            if (lstUpcomingAppointments.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an appointment.","Error");
                return;
            }
            String date = DateTime.Now.ToString();


            ViewAppointment appt = new ViewAppointment(selectedOrder,currentUser);
            appt.ShowDialog();
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CmbApptTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            populateListboxes(text);
        }

        private void CmbEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            populateListboxes(text);
        }

        private void PopulateEmployeeListBox()
        {
            string test = cmbEmployees.SelectedItem.ToString();
            
            try
            {
                foreach (UserModel e in employees)
                {
                    if (e.Firstname + " " + e.Lastname == test)
                    {
                        foreach (OrderModel om in assignedOrders)
                        {
                            if(om.EmployeeID == e.UserID)
                            {
                                if (cmbApptTime.SelectedIndex == 0)
                                {
                                    if (DateTime.Parse(om.ServiceDateRequest) >= presentDate)
                                    {
                                        if (DateTime.Parse(om.ServiceDateRequest) <= presentDate.AddDays(7))
                                        {

                                            lstUpcomingAppointments.Items.Add(om);
                                        }
                                    }
                                }

                                if (cmbApptTime.SelectedIndex == 1)
                                {


                                    if (DateTime.Parse(om.ServiceDateRequest) >= presentDate)
                                    {
                                        if (DateTime.Parse(om.ServiceDateRequest) <= presentDate.AddDays(30))
                                        {
                                            lstUpcomingAppointments.Items.Add(om);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            
        }
    }
}
