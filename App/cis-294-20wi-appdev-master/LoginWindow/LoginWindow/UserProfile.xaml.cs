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
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        private UserModel user = null;
        private UserModel selectedUser;
        private UserModel currentUser;
        // Constructor for CreateUser Button in AdminSuccessWindow.xaml
        // Lauren
        public UserProfile()
        {
            InitializeComponent();
            btnSave.Visibility = Visibility.Hidden;
            comboPriv.SelectedIndex = 0;
            //chkPassword.Visibility = Visibility.Hidden;
        }

        // Constructor for UpdateUser Button in AdminSuccessWindow.xaml
        // Lauren

       public UserProfile(UserModel m, UserModel c)
        {
            InitializeComponent();
            btnCreate.Visibility = Visibility.Hidden;

            txtFirstName.Text = m.Firstname;
            txtLastName.Text = m.Lastname;
            txtEmail.Text = m.Email;
            txtUsername.Text = m.Username;

            currentUser = c;
            selectedUser = m;

            if (m.Privileges == "admin")
            {
                comboPriv.SelectedIndex = 1;
            }
            else if(m.Privileges == "Employee")
            {
                comboPriv.SelectedIndex = 2;
            }
            else
            {
                comboPriv.SelectedIndex = 3;
            }

            //txtPassword.IsEnabled = false;

        }

        /* Get New User Method returns newly updated or created User Object
         * Lauren
         */

        public UserModel GetNewUser()
        {
            this.ShowDialog();
            return user;
        }

        /* IsValidData Method to validate text fields
         * Lauren
         */ 

        private bool IsValidData()
        {

            if(comboPriv.SelectedIndex == 0)
            {
                MessageBox.Show("User privilege not selected");
                return false;
            }

            if (!(String.IsNullOrEmpty(txtPassword.Text)))
            {
                return Validator.IsPresent(txtFirstName) &&
                    Validator.IsPresent(txtLastName) &&
                    Validator.IsPresent(txtEmail) &&
                    Validator.IsValidEmail(txtEmail) &&
                    Validator.IsPresent(txtUsername) &&
                    Validator.IsPresent(txtPassword);
            }
            else
            {
                return Validator.IsPresent(txtFirstName) &&
                    Validator.IsPresent(txtLastName) &&
                    Validator.IsPresent(txtEmail) &&
                    Validator.IsValidEmail(txtEmail) &&
                    Validator.IsPresent(txtUsername);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        /* Checks to see if data is valid and if so creates new object
         * Lauren
         */

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidData())
            {
                //user = new UserModel(txtFirstName.Text, txtLastName.Text, txtEmail.Text,comboPriv.Text,txtUsername.Text);
                if (!(String.IsNullOrEmpty(txtPassword.Text)))
                    user.Password = txtPassword.Text;
                
                //if (chkPassword.IsChecked == true)
                //    user.Password = txtPassword.Text;

                this.Close();
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidData())
            {
                //user = new UserModel(txtFirstName.Text, txtLastName.Text, txtEmail.Text, comboPriv.Text, txtUsername.Text, txtPassword.Text);
                this.Close();
            }
        }

        /*private void ChkPassword_Checked(object sender, RoutedEventArgs e)
        {
            txtPassword.IsEnabled = true;
        }

        private void ChkPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPassword.IsEnabled = false;
        }*/

        private void BtnViewPet_Click(object sender, RoutedEventArgs e)
        {
            PetProfile pp = new PetProfile(currentUser,selectedUser);

            pp.ShowDialog();
        }
    }
}
