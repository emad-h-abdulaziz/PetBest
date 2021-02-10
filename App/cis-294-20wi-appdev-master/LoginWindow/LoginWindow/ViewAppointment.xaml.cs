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
    /// Interaction logic for ViewAppointment.xaml
    /// </summary>
    public partial class ViewAppointment : Window
    {
        public OrderModel selectedOrder;
        public OrderModel updatedOrder;
        public UserModel currentUser;
        public List<ServiceModel> services;

        public ViewAppointment()
        {
            InitializeComponent();
            MessageBox.Show(selectedOrder.TotalPrice);
        }

        public ViewAppointment(OrderModel selectedOrder,UserModel currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            this.selectedOrder = selectedOrder;
            txtCustomerName.Text = selectedOrder.FirstName + " " + selectedOrder.LastName;
            txtOrderId.Text = selectedOrder.OrderID;
            txtService.Text = selectedOrder.ServiceName;
            txtCategory.Text = selectedOrder.CategoryName;
            
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

           services= await ServiceProcessor.ShowAllServices(currentUser.Token);
            foreach(ServiceModel m in services)
            {
                if (m.ServiceName == selectedOrder.ServiceName)
                {
                    txtPrice.Text = m.Price;
                }
            }
        }

        //private void BtnSave_Click(object sender, RoutedEventArgs e)
        //{
        //    if (cmboService.SelectedIndex == 0)
        //    {
        //        selectedOrder.OrderStatus = "In Progress";
        //        updatedOrder = new OrderModel(selectedOrder.OrderStatus);
        //    }
        //    if (cmboService.SelectedIndex == 1)
        //    {
        //        selectedOrder.OrderStatus = "Complete";
        //        updatedOrder = new OrderModel(selectedOrder.OrderStatus);

        //    }


        //    this.Close();
        //}

        //public OrderModel GetUpdatedOrder()
        //{
        //    this.ShowDialog();
        //    return updatedOrder;
        //}
    }
}
