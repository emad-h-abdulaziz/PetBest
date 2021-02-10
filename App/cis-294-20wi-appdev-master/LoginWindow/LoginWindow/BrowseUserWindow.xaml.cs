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
    /// Interaction logic for BrowseUserWindow.xaml
    /// </summary>
    public partial class BrowseUserWindow : Window
    {

        List<UserModel> users;
        private UserModel currentUser;
        string privilegeType;

        public BrowseUserWindow(UserModel user,string privilegeType)
        {
            InitializeComponent();
            currentUser = user;
            this.privilegeType = privilegeType;
            FillListBox();
        }

        //For some reason this function is never being executed... -Ali
        //private async void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    users = await UserProcessor.GetUsers(currentUser.Token);
        //    foreach (UserModel m in users)
        //    {
        //        //if (m.Privileges == "Customer")
        //        //{
        //        //    lstUsers.Items.Add(m);
        //        //}
        //    }
            
        //}

        //This one works though -Ali
        private async void FillListBox()
        {
            users = await UserProcessor.GetUsers(currentUser.Token);
            foreach (UserModel m in users)
            {
                if (m.Privileges == privilegeType)
                {
                    lstUsers.Items.Add(m);
                    
                }
                
              
            }
        }

        public UserModel GetSelectedUser()
        {
            this.ShowDialog();
            return (UserModel)lstUsers.SelectedItem;
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a user");
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
