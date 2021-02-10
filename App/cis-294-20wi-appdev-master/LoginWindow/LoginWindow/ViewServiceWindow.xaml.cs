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
    /// Interaction logic for ViewServiceWindow.xaml
    /// </summary>
    public partial class ViewServiceWindow : Window
    {
        private List<ServiceModel> services;
        private UserModel currentUser;

        public ViewServiceWindow(UserModel user)
        {
            InitializeComponent();
            currentUser = user;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            FillServiceListBox();
        }
        private async void FillServiceListBox()
        {
            lstService.Items.Clear();
            services = await ServiceProcessor.ShowAllServices(currentUser.Token);
            foreach (ServiceModel m in services)
            {
                lstService.Items.Add(m);
            }
        }

        public ServiceModel GetSelectedService()
        {
            this.ShowDialog();
            return (ServiceModel)lstService.SelectedItem;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            if(lstService.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a service");
                return;
            }
            this.Close();
        }
    }
}
