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
    /// Interaction logic for UIRedesign.xaml
    /// </summary>
    public partial class UIRedesign : Window
    {
        private List<UserModel> users;
        private List<PetModel> pets;
        private List<OrderModel> orders, pastOrders;
        private List<List<InvoiceModel>> invoices = new List<List<InvoiceModel>>();
        private List<InvoiceModel> usedInvoices = new List<InvoiceModel>();
        private PetModel selectedPet, updatedPet, createdPet;
        private UserModel currentUser, selectedUser, updatedUser, createdUser;
        private AddressModel userAddress, updatedAddress;

        public UIRedesign(UserModel user)
        {
            InitializeComponent();
            currentUser = user;
        }
        public UIRedesign()
        {
            InitializeComponent();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableUserTextbox();
            users = await UserProcessor.GetUsers(currentUser.Token);
            foreach(UserModel m in users)
            {
                lstUsers.Items.Add(m);
            }
            lblHireDate.Visibility = Visibility.Hidden;
            txtHireDate.Visibility = Visibility.Hidden;
            lblBirthDate.Visibility = Visibility.Hidden;
            txtBirthDate.Visibility = Visibility.Hidden;
            Console.WriteLine(currentUser.Token);
        }

        // Fill List Boxes
        private async void FillUserListBox()
        {
            lstUsers.Items.Clear();

            users = await UserProcessor.GetUsers(currentUser.Token);
            foreach(UserModel m in users)
            {
                if(txtSort.Text == "")
                {
                    lstUsers.Items.Add(m);
                }
                else
                {
                    try
                    {
                        if (m.Lastname != null && m.Lastname.ToUpper() == txtSort.Text.ToUpper())
                        {
                            lstUsers.Items.Add(m);
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("User with last name of " + txtSort.Text + " does not exist." + ex);
                    }
                    
                }
            }
        }

        private async void FillPetListBox()
        {
            lstPets.Items.Clear();
            btnCreatePet.IsEnabled = true;
            lstPets.IsEnabled = true;

            pets = await UserProcessor.ShowPetsByCustomer(selectedUser.UserID, currentUser.Token);

            if(pets != null)
            {
                foreach (PetModel p in pets)
                {
                    lstPets.Items.Add(p);
                }
            }
        }
        private async void FillOrderListBox()
        {
            lstOrders.Items.Clear();
            lstOrders.IsEnabled = true;

            orders = await OrderProcessor.ShowCustomerActiveOrders(selectedUser.UserID, currentUser.Token);
            pastOrders = await OrderProcessor.ShowPastCustomerOrders(selectedUser.UserID, currentUser.Token);

            if (orders != null || pastOrders != null)
            {
                if (orders != null)
                {
                    foreach (OrderModel o in orders)
                    {
                        lstOrders.Items.Add(o);
                    }
                }
                if (pastOrders != null)
                {
                    foreach (OrderModel o in pastOrders)
                    {
                        lstOrders.Items.Add(o);
                    }
                }
            }
            else
            {
                lstOrders.Items.Add("NO ACTIVE ORDERS FOR THIS CUSTOMER");
            }
        }
        private async void FillInvoiceListBox(UserModel user)
        {
            lstInvoices.Items.Clear();
            usedInvoices.Clear();
            invoices.Clear();
            lstInvoices.IsEnabled = true;
            if(await InvoiceProcessor.getOverdueInvoices(currentUser.Privileges, currentUser.Token) != null)
                invoices.Add(await InvoiceProcessor.getOverdueInvoices(currentUser.Privileges, currentUser.Token));

            if(await InvoiceProcessor.getUnderdueInvoices(currentUser.Privileges, currentUser.Token) != null)
                invoices.Add(await InvoiceProcessor.getUnderdueInvoices(currentUser.Privileges, currentUser.Token));

            if(await InvoiceProcessor.getUnpaidInvoices(currentUser.Privileges, currentUser.Token) != null)
                invoices.Add(await InvoiceProcessor.getUnpaidInvoices(currentUser.Privileges, currentUser.Token));

            if(await InvoiceProcessor.getPaidinvoices(currentUser.Privileges, currentUser.Token) != null)
                invoices.Add(await InvoiceProcessor.getPaidinvoices(currentUser.Privileges, currentUser.Token));

            if (invoices != null)
            {
                foreach (List<InvoiceModel> i in invoices)
                {
                    foreach (InvoiceModel n in i)
                    {
                        if (n.CustomerID == user.UserID)
                        {
                            if (usedInvoices.Count == 0)
                            {
                                usedInvoices.Add(n);
                            }
                            else
                            {
                                foreach (InvoiceModel invoice in usedInvoices)
                                {
                                    if (n.InvoiceID == invoice.InvoiceID)
                                    {
                                        lstInvoices.Items.Add(n);                                        
                                    }
                                }
                                usedInvoices.Add(n);
                            }
                        }
                    }
                }
                if(lstInvoices.Items.IsEmpty)
                {
                    lstInvoices.Items.Add("NO INVOICES FOR THIS CUSTOMER");
                }
            }
            else
            {
                lstInvoices.Items.Add("NO INVOICES FOR THIS CUSTOMER");
            }
        }

        // Selection & Text Changed Events
        private void lstPets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstPets.SelectedIndex != -1)
            {
                selectedPet = (PetModel)lstPets.SelectedItem;

                EnablePetTextbox();

                txtPetName.Text = selectedPet.FullName;
                txtPetWeight.Text = selectedPet.Weight;
                txtPetBreed.Text = selectedPet.Breed;
                txtPetDOB.Text = selectedPet.Birthdate;
                txtPetColor.Text = selectedPet.Color;

                btnCreatePet.IsEnabled = false;

            }
        }

        private async void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstUsers.SelectedIndex != -1)
            {
                try
                {
                    selectedUser = (UserModel)lstUsers.SelectedItem;
                    userAddress = await UserProcessor.GetAddress(currentUser.Token, selectedUser.UserID);
                    lblHireDate.Visibility = Visibility.Hidden;
                    txtHireDate.Visibility = Visibility.Hidden;
                    lblBirthDate.Visibility = Visibility.Hidden;
                    txtBirthDate.Visibility = Visibility.Hidden;
                    ClearPetInfo();
                    ClearUserInfo();
                    EnableUserTextbox();
                    EnablePetTextbox();

                    btnCreateUser.IsEnabled = false;
                    btnClear.IsEnabled = true;
                    btnCreatePet.IsEnabled = true;

                    // User related text boxes become populated
                    txtFirstName.Text = selectedUser.Firstname;
                    txtLastName.Text = selectedUser.Lastname;
                    txtEmail.Text = selectedUser.Email;
                    txtUsername.Text = selectedUser.Username;                    
                    txtPassword.Text = "";

                    txtAddress.Text = userAddress.Line1;
                    txtCity.Text = userAddress.City;
                    txtState.Text = userAddress.State;
                    txtZip.Text = userAddress.ZipCode;
                    txtPhone.Text = userAddress.Phone;

                    if (selectedUser.Privileges == "Admin")
                    {
                        comboPriv.SelectedIndex = 1;
                        lstInvoices.Items.Clear();
                        DisablePetTextbox();
                        lstPets.Items.Clear();
                        lstOrders.Items.Clear();
                    }
                    else if (selectedUser.Privileges == "Employee")
                    {
                        comboPriv.SelectedIndex = 2;
                        lblHireDate.Visibility = Visibility.Visible;
                        txtHireDate.Visibility = Visibility.Visible;
                        lblBirthDate.Visibility = Visibility.Visible;
                        txtBirthDate.Visibility = Visibility.Visible;
                        txtHireDate.Text = selectedUser.HireDate;
                        txtBirthDate.Text = selectedUser.BirthDate;
                        lstInvoices.Items.Clear();
                        lstPets.Items.Clear();
                        lstOrders.Items.Clear();
                        DisablePetTextbox();
                    }
                    else
                    {
                        comboPriv.SelectedIndex = 3;
                        FillOrderListBox();
                        FillPetListBox();
                        FillInvoiceListBox(selectedUser);
                    }
                   
                }
                catch(Exception)
                {
                    MessageBox.Show("This user has incomplete data");
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnSave.IsEnabled = true;
            btnSavePet.IsEnabled = true;
            btnClear.IsEnabled = true;
        }

        private void lstInvoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstInvoices.SelectedIndex != -1)
                btnViewInvoice.IsEnabled = true;
        }

        private void comboPriv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboPriv.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Employee")
            {
                lblHireDate.Visibility = Visibility.Visible;
                txtHireDate.Visibility = Visibility.Visible;
                lblBirthDate.Visibility = Visibility.Visible;
                txtBirthDate.Visibility = Visibility.Visible;
            }
            else
            {
                lblHireDate.Visibility = Visibility.Hidden;
                txtHireDate.Visibility = Visibility.Hidden;
                lblBirthDate.Visibility = Visibility.Hidden;
                txtBirthDate.Visibility = Visibility.Hidden;
            }
        }
        

        // Validation
        private bool IsValidUserData()
        {

            if (comboPriv.SelectedIndex == 0)
            {
                MessageBox.Show("User privilege not selected");
                return false;
            }
            if (comboPriv.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Employee" && !(String.IsNullOrEmpty(txtPassword.Text)))
            {
                return Validator.IsPresent(txtFirstName) &&
                    Validator.IsPresent(txtLastName) &&
                    Validator.IsPresent(txtEmail) &&
                    Validator.IsValidEmail(txtEmail) &&
                    Validator.IsPresent(txtAddress) &&
                    Validator.IsPresent(txtCity) &&
                    Validator.IsPresent(txtZip) &&
                    Validator.IsPresent(txtPhone) &&
                    Validator.IsPhoneNumber(txtPhone) &&
                    Validator.IsPresent(txtState) &&
                    Validator.IsPresent(txtBirthDate) &&
                    Validator.IsDateTime(txtBirthDate) &&
                    Validator.IsPresent(txtHireDate) &&
                    Validator.IsDateTime(txtHireDate) &&
                    Validator.IsPresent(txtPassword);
            }
            if (!(String.IsNullOrEmpty(txtPassword.Text)))
            {
                return Validator.IsPresent(txtFirstName) &&
                    Validator.IsPresent(txtLastName) &&
                    Validator.IsPresent(txtEmail) &&
                    Validator.IsValidEmail(txtEmail) &&
                    Validator.IsPresent(txtAddress) &&
                    Validator.IsPresent(txtCity) &&
                    Validator.IsPresent(txtZip) &&
                    Validator.IsPresent(txtPhone) &&
                    Validator.IsPhoneNumber(txtPhone) &&
                    Validator.IsPresent(txtState) &&
                    Validator.IsPresent(txtPassword);
            }
            
            else
            {
                return Validator.IsPresent(txtFirstName) &&
                    Validator.IsPresent(txtLastName) &&
                    Validator.IsPresent(txtEmail) &&
                    Validator.IsValidEmail(txtEmail) &&
                    Validator.IsPresent(txtAddress) &&
                    Validator.IsPresent(txtCity) &&
                    Validator.IsPresent(txtZip) &&
                    Validator.IsPresent(txtPhone) &&
                    Validator.IsPhoneNumber(txtPhone) &&                 
                    Validator.IsPresent(txtState);
            }
        }

        private bool IsValidPetData()
        {
            return Validator.IsPresent(txtPetName) &&
                Validator.IsPresent(txtPetDOB) &&
                Validator.IsDateTime(txtPetDOB)&&
                Validator.IsPresent(txtPetWeight) &&
                Validator.IsPresent(txtPetColor) &&
                Validator.IsPresent(txtPetBreed);
        }

        private bool CompareUpdatedUser()
        {
            if (updatedUser.Firstname == selectedUser.Firstname &&
                updatedUser.Lastname == selectedUser.Lastname &&
                updatedUser.Email == selectedUser.Email &&
                updatedUser.Username == selectedUser.Username &&
                updatedUser.Privileges == selectedUser.Privileges&&
                updatedAddress.Line1 == userAddress.Line1&&
                updatedAddress.City==userAddress.City&&
                updatedAddress.State==userAddress.State&&
                updatedAddress.ZipCode==userAddress.ZipCode&&
                updatedAddress.Phone==userAddress.Phone)
            {
                return false;
            }
            else
            {
                selectedUser.Firstname = updatedUser.Firstname;
                selectedUser.Lastname = updatedUser.Lastname;
                selectedUser.Email = updatedUser.Email;
                selectedUser.Privileges = updatedUser.Privileges;
                selectedUser.Username = updatedUser.Username;
                userAddress.Line1 = updatedAddress.Line1;
                userAddress.City = updatedAddress.City;
                userAddress.State = updatedAddress.State;
                userAddress.ZipCode = updatedAddress.ZipCode;
                userAddress.Phone = updatedAddress.Phone;
                if(updatedUser.Password != null)
                {
                    selectedUser.Password = updatedUser.Password;
                }
                else
                {
                    selectedUser.Password = null;
                }    

                return true;
            }
        }

        private bool CompareUpdatedPet()
        {
            if (selectedPet != null)
            {
                if (updatedPet.FullName == selectedPet.FullName &&
                    updatedPet.Birthdate == selectedPet.Birthdate &&
                    updatedPet.Breed == selectedPet.Breed &&
                    updatedPet.Color == selectedPet.Color &&
                    updatedPet.Weight == selectedPet.Weight)
                {
                    return false;
                }
                else
                {
                    selectedPet.FullName = updatedPet.FullName;
                    selectedPet.Birthdate = updatedPet.Birthdate;
                    selectedPet.Breed = updatedPet.Breed;
                    selectedPet.Color = updatedPet.Color;
                    selectedPet.Weight = updatedPet.Weight;

                    return true;
                }
            }
            return false;
        }


        // Button Click Events
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            btnClearAll.Visibility = Visibility.Visible;
            btnClearPet.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            btnClear.IsEnabled = false;
        }

        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            ClearUserInfo();
            ClearPetInfo();
            DisablePetTextbox();
            lstPets.Items.Clear();
            if(txtSort.Text != "")
            {
                txtSort.Text = "";
                FillUserListBox();
            }
            else
            {
                lstUsers.SelectedItem = null;
            }

            btnCreateUser.IsEnabled = true;
            btnCreatePet.IsEnabled = false;
            btnClear.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnSavePet.IsEnabled = false;
            btnClearAll.Visibility = Visibility.Hidden;
            btnClearPet.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
        }

        private void btnClearPet_Click(object sender, RoutedEventArgs e)
        {
            ClearPetInfo();

            btnCreatePet.IsEnabled = true;
            btnClear.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnSavePet.IsEnabled = false;
            btnClearAll.Visibility = Visibility.Hidden;
            btnClearPet.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnClearAll.Visibility = Visibility.Hidden;
            btnClearPet.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
            btnClear.IsEnabled = true;
        }

        private async void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            if(IsValidUserData())
            {
                createdUser = new UserModel(txtFirstName.Text, txtLastName.Text, txtEmail.Text, comboPriv.Text, txtUsername.Text,txtBirthDate.Text,txtHireDate.Text, txtPassword.Text);

                await UserProcessor.CreateUser(createdUser.Firstname, createdUser.Lastname, createdUser.Username, createdUser.Password, createdUser.Email, createdUser.Privileges,createdUser.BirthDate,createdUser.HireDate, currentUser.Token);

                if (createdUser.Privileges == "Customer")
                {
                    btnCreatePet.IsEnabled = true;
                    EnablePetTextbox();
                }

                btnCreateUser.IsEnabled = false;
                selectedUser = await UserProcessor.GetUsersByUsername(currentUser.Token,createdUser.Username);
                await UserProcessor.CreateAddress(selectedUser.UserID, txtAddress.Text, null, txtCity.Text, txtState.Text, txtZip.Text, txtPhone.Text,currentUser.Token);
                lstUsers.Items.Add(selectedUser);
            }
        }

        private async void btnCreatePet_Click(object sender, RoutedEventArgs e)
        {
            if(IsValidPetData())
            {
                DateTime birthdate = DateTime.Parse(txtPetDOB.Text);
                createdPet = new PetModel(txtPetName.Text, birthdate.ToString("yyyy-MM-dd"), txtPetColor.Text, txtPetWeight.Text, txtPetBreed.Text);
                lstPets.Items.Add(createdPet);
                lstPets.IsEnabled = true;
                await PetProcessor.CreatePet(createdPet.FullName, createdPet.Birthdate, createdPet.Color, createdPet.Weight, createdPet.Breed, selectedUser.UserID, currentUser.Token);

                btnCreatePet.IsEnabled = false;
                selectedPet = createdPet;
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidUserData())
            {
                bool passwordChanged = false;

                updatedUser = new UserModel(txtFirstName.Text, txtLastName.Text, txtEmail.Text, comboPriv.Text, txtUsername.Text,txtBirthDate.Text,txtHireDate.Text);
                updatedAddress = new AddressModel(txtAddress.Text, txtCity.Text, txtState.Text, txtZip.Text, txtPhone.Text);
                if (!(String.IsNullOrEmpty(txtPassword.Text)))
                {
                    updatedUser.Password = txtPassword.Text;
                    passwordChanged = true;
                }

                bool userChanged = CompareUpdatedUser();

                if (userChanged == true && passwordChanged == true)
                {
                    await UserProcessor.UpdateUser(selectedUser.UserID, selectedUser.Firstname, selectedUser.Lastname, selectedUser.Username, selectedUser.Password, selectedUser.Email, selectedUser.Privileges, currentUser.Token);
                    await UserProcessor.UpdateAddress(selectedUser.UserID, userAddress.Line1, userAddress.Line2, userAddress.City, userAddress.State, userAddress.ZipCode, userAddress.Phone, currentUser.Token);
                    FillUserListBox();
                }
                else if (userChanged == true && passwordChanged == false)
                {
                    await UserProcessor.UpdateUser(selectedUser.UserID, selectedUser.Firstname, selectedUser.Lastname, selectedUser.Username, selectedUser.Email, selectedUser.Privileges, currentUser.Token);
                    await UserProcessor.UpdateAddress(selectedUser.UserID, userAddress.Line1, userAddress.Line2, userAddress.City, userAddress.State, userAddress.ZipCode, userAddress.Phone, currentUser.Token);
                    FillUserListBox();
                }

                btnSave.IsEnabled = false;
            }
        }

        private async void btnSavePet_Click(object sender, RoutedEventArgs e)
        {
            updatedPet = new PetModel(txtPetName.Text, txtPetDOB.Text, txtPetColor.Text, txtPetWeight.Text, txtPetBreed.Text);

            bool petChanged = CompareUpdatedPet();

            if (petChanged == true)
            {
                await PetProcessor.UpdatePet(selectedPet.PetID, selectedPet.FullName, selectedPet.Birthdate, selectedPet.Color, selectedPet.Weight, selectedPet.Breed, currentUser.Token);
                FillPetListBox();
            }

            btnSavePet.IsEnabled = false;
        }

        private void btnAssignEmp_Click(object sender, RoutedEventArgs e)
        {
            AdminDelagationWindow frmAdminDelegation = new AdminDelagationWindow(currentUser);
            frmAdminDelegation.ShowDialog();
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            ReportsWindow reports = new ReportsWindow(currentUser);
            reports.ShowDialog();
        }

        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            ServiceWindow service = new ServiceWindow(currentUser);

            service.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void btnViewInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstInvoices.SelectedItem != null)
                {
                    InvoiceWindow i = new InvoiceWindow((InvoiceModel)lstInvoices.SelectedItem, currentUser);
                    i.Show();
                }
                else
                {
                    MessageBox.Show("Please select an invoice");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Invalid Invoice");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            FillUserListBox();
            btnClear.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ServiceWindow service = new ServiceWindow(currentUser);
            service.ShowDialog();
        }

        private void BtnSchedule_Click(object sender, RoutedEventArgs e)
        {
            employeeAppoinments appt = new employeeAppoinments(currentUser);
            appt.ShowDialog();
        }

        // Clear Text Functions
        private void ClearPetInfo()
        {
            txtPetName.Text = "";
            txtPetWeight.Text = "";
            txtPetBreed.Text = "";
            txtPetDOB.Text = "";
            txtPetColor.Text = "";
        }

        private void ClearUserInfo()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtEmail.Text = "";
            txtZip.Text = "";
            txtPhone.Text = "";
            txtState.Text = "";
            txtBirthDate.Text = "";
            txtHireDate.Text = "";
            comboPriv.SelectedIndex = 0;
        }

        // Enable & Disable Textbox Functions
        private void DisablePetTextbox()
        {
            txtPetName.IsEnabled = false;
            txtPetWeight.IsEnabled = false;
            txtPetBreed.IsEnabled = false;
            txtPetDOB.IsEnabled = false;
            txtPetColor.IsEnabled = false;
        }

        private void EnablePetTextbox()
        {
            txtPetName.IsEnabled = true;
            txtPetWeight.IsEnabled = true;
            txtPetBreed.IsEnabled = true;
            txtPetDOB.IsEnabled = true;
            txtPetColor.IsEnabled = true;
        }

        private void EnableUserTextbox()
        {
            txtFirstName.IsEnabled = true;
            txtLastName.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtUsername.IsEnabled = true;
            txtPassword.IsEnabled = true;
            comboPriv.IsEnabled = true;
        }
    }
}
