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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Lauren Rivier 2/9/2020 test pull from/push to repository 

        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
          /* if both textboxes are not empty
            * the login window will close and the
            * success window will open.
            * Ali*/
            if (isValid())
            {   
                /* the program will send the username and password text fields to the 
                 * UserProcessor class to make sure that the values are correct
                 * Ali*/
                var userInfo = await UserProcessor.LoadUser(txtUsername.Text,txtPassword.Password);
                /* If the user inputs an invalid username or password
                 * the User object's properites will all be null, so
                 * exception logic is required here
                 * Ali*/
                try
                {
                    if (userInfo.Privileges == "Admin")
                    {
                        UIRedesign s = new UIRedesign(userInfo);
                        Console.WriteLine(userInfo.Token);
                        s.Show();
                        this.Close();
                    }
                    else if (userInfo.Privileges == "Employee")
                    {

                    }
                    else
                    {
                        MessageBox.Show("Customers not permitted on this platform.", "Login Failed");
                    }
                }
                catch
                {
                    txtPassword.Clear();
                    MessageBox.Show("Either username or passord is incorrect.", "Login Failed");
                }
            }

        }

        /* Validation function for username and password textboxes.
         * The purpose of this function is to make sure that the textboxes
         * are not empty at the time the login button is clicked.
         * Ali */
        private bool isValid()
        { 
            if(txtPassword.Password == string.Empty || txtUsername.Text == string.Empty)
            {
                MessageBox.Show("Both the Username and Password fields must be filled out","Error");
                return false;                
            }
            else
            {
                return true;
            }
            
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        /* I created 2 textboxes underneath the input textboxes that contain the watermark
         * texts which exchange visibility based on if there is text or the input textbox 
         * is selected. (The following 4 functions take care of that. Hakim Alkafah*/
        private void txtUNwatermark_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUsername.Visibility = System.Windows.Visibility.Visible;
            txtUNwatermark.Visibility = System.Windows.Visibility.Collapsed;
            txtUsername.Focus();
        }

        private void txtUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtUsername.Text))
            {
                txtUsername.Visibility = System.Windows.Visibility.Collapsed;
                txtUNwatermark.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void txtPDwatermark_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Visibility = System.Windows.Visibility.Visible;
            txtPDwatermark.Visibility = System.Windows.Visibility.Collapsed;
            txtPassword.Focus();
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                txtPassword.Visibility = System.Windows.Visibility.Collapsed;
                txtPDwatermark.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
