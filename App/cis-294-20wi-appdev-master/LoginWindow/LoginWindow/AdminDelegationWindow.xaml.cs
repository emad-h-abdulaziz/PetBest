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
using LoginWindow.ApiClasses;

namespace LoginWindow
{
    /// <summary>
    /// Interaction logic for AdminDelagationWindow.xaml
    /// </summary>
    public partial class AdminDelagationWindow : Window
    {
        private List<UserModel> users;
        private List<OrderModel> orders;
        private UserModel currentUser;
        private UserModel selectedEmployee;
        private OrderModel selectedOrder;
        public AdminDelagationWindow(UserModel user)
        {
            InitializeComponent();
            currentUser = user;
        }

        private async void frmAdminDelegation_Loaded(object sender, RoutedEventArgs e)
        {
            users = await UserProcessor.ShowAllEmployees(currentUser.Token);
            orders = await OrderProcessor.ShowAllProgressOrders(currentUser.Privileges, currentUser.Token);
            foreach (UserModel m in users)
            {
                lstEmployees.Items.Add(m);
            }
            foreach(OrderModel o in orders)
            {
                lstOrders.Items.Add(o);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAssign_Click(object sender, RoutedEventArgs e)
        {
            if(lstEmployees.SelectedIndex==-1||lstOrders.SelectedIndex==-1)
            {
                MessageBox.Show("Please select both an employee and an order", "Error");
            }
            else
            {
                try
                {
                    selectedEmployee = (UserModel)lstEmployees.SelectedItem;
                    selectedOrder = (OrderModel)lstOrders.SelectedItem;

                    OrderProcessor.AssignEmployeeToOrder(selectedEmployee.UserID, selectedOrder.OrderID,currentUser.Token);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
