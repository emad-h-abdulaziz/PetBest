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
    /// Interaction logic for AdminSuccessWindow.xaml
    /// </summary>
    public partial class AdminSuccessWindow : Window
    {
        List<UserModel> users;
        private UserModel currentUser;


        public AdminSuccessWindow(UserModel user)
        {
            /* This constructor catches the text from the 
             * username textbox and sets the label to be
             * the same as the username entered.
             * Ali*/
            InitializeComponent();
            currentUser = user;
           
            lblName.Content = currentUser.Firstname + " " + currentUser.Lastname;
        }

        /* Code for the return/logout button
         * Closes the success window and opens
         * the login window.
         * Ali*/
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            users = await UserProcessor.GetUsers(currentUser.Token);
            foreach (UserModel m in users)
            {
                lstUsers.Items.Add(m);
            }
        }

        /* Fill Customer List Box Method updates user list box every time user returns to form
         * Lauren
         */

        private void FillCustomerListBox()
        {
            lstUsers.Items.Clear();

            foreach (UserModel m in users)
            {
                lstUsers.Items.Add(m);
            }
        }

        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUsers.SelectedIndex != -1)
                btnUpdateUser.IsEnabled = true;
            else
                btnUpdateUser.IsEnabled = false;
        }

        /* Create User Method 
         * Lauren
         */

        private async void BtnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            UserProfile up = new UserProfile();

            UserModel createdUser = up.GetNewUser();
            
            if (createdUser != null)
            {
                lstUsers.Items.Add(createdUser);
                //await UserProcessor.CreateUser(createdUser.Firstname, createdUser.Lastname, createdUser.Username, createdUser.Password, createdUser.Email, createdUser.Privileges, currentUser.Token);
            }


        }

        /* Update User Method takes user selected from list box, brings them to an update user form and sends back 
         * object with updated fields to be sent to UpdateUser API
         * Lauren
         */

        private async void BtnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            btnUpdateUser.IsEnabled = false;

            UserModel oldUser = (UserModel)lstUsers.SelectedItem;

            UserProfile up = new UserProfile(oldUser,currentUser);

            UserModel updatedUser = up.GetNewUser();



            if (updatedUser != null)
            {
                oldUser.Firstname = updatedUser.Firstname;
                oldUser.Lastname = updatedUser.Lastname;
                oldUser.Email = updatedUser.Email;
                oldUser.Privileges = updatedUser.Privileges;
                oldUser.Username = updatedUser.Username;
                if (updatedUser.Password == null)
                {
                    oldUser.Password = null;
                }
                else
                {
                    oldUser.Password = updatedUser.Password;
                }
                flag = true;
            }

            FillCustomerListBox();
            if (oldUser.Password == null && flag == true)
            {
                await UserProcessor.UpdateUser(oldUser.UserID, oldUser.Firstname, oldUser.Lastname, oldUser.Username, oldUser.Email, oldUser.Privileges , currentUser.Token);
            }
            else if (oldUser.Password != null && flag == true)
            {
                await UserProcessor.UpdateUser(oldUser.UserID, oldUser.Firstname, oldUser.Lastname, oldUser.Username, oldUser.Password, oldUser.Email, oldUser.Privileges , currentUser.Token);
            }

                btnUpdateUser.IsEnabled = false;
        }


        private void BtnManageServices_Click(object sender, RoutedEventArgs e)
        {
            ServiceWindow service = new ServiceWindow(currentUser);

            service.ShowDialog();

        }

        private void btnDelegateOrders_Click(object sender, RoutedEventArgs e)
        {
            AdminDelagationWindow frmAdminDelegation = new AdminDelagationWindow(currentUser);
            frmAdminDelegation.ShowDialog();
        }

        private void btnViewServices_Click(object sender, RoutedEventArgs e)
        {
            ViewServiceWindow vs = new ViewServiceWindow(currentUser);

            vs.ShowDialog();
        }

        private void BtnViewReports_Click(object sender, RoutedEventArgs e)
        {
            ReportsWindow reports = new ReportsWindow(currentUser);
            reports.ShowDialog();
        }

        private void btnUIRedesign_Click(object sender, RoutedEventArgs e)
        {
            UIRedesign ui = new UIRedesign(currentUser);
            ui.Show();
        }
    }
}
