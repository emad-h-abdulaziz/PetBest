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
    /// Interaction logic for ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {


        private UserModel currentUser;
        UserModel selectedUser;
        PetModel selectedPet;
        ServiceModel selectedService;

        public ServiceWindow(UserModel user)
        {

            InitializeComponent();

            currentUser = user;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnBrowseClient_Click(object sender, RoutedEventArgs e)
        {
            BrowseUserWindow browseUser = new BrowseUserWindow(currentUser,"Customer");
            selectedUser = browseUser.GetSelectedUser();

            if (selectedUser != null)
            {
                txtClientName.Text = selectedUser.Firstname + " " + selectedUser.Lastname;
            }


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DateTime serviceDate;

            try
            {
                serviceDate = (DateTime)apptDate.SelectedDate;
            }
            catch
            {
                MessageBox.Show("Please enter valid date");
                return;
            }
            string hour = "0";
            string hours = cmbHour.SelectedItem.ToString();
            String hoursResult = hours.Substring(hours.Length - 2);
            int hoursTest = int.Parse(hoursResult);

            if (cmbAMPM.SelectedIndex == 1)
            {
                if(cmbHour.SelectedIndex <= 2)
                {
                    MessageBox.Show("Please select an appropriate time.");
                    return;
                }
                if (cmbHour.SelectedIndex == 3)
                {
                    hoursTest = 12;
                }
                else
                {
                    hoursTest += 12;
                    
                }

                hour = hoursTest.ToString();
            }
            else
            {
                if (cmbHour.SelectedIndex >= 3)
                {
                    MessageBox.Show("Please select a valid time");
                    return;
                }
                if (cmbHour.SelectedIndex==0)
                {
                    hour = "09";
                }
                else if (cmbHour.SelectedIndex == 1)
                {
                    hour = "10";
                }
                else if (cmbHour.SelectedIndex == 2)
                {
                    hour = "11";
                }
            }
            string minutes = cmbMinutes.SelectedItem.ToString();
            String minutesResult = minutes.Substring(minutes.Length - 2);
            string totalTime = hour + ":" + minutesResult + ":00";
            string totalDate = serviceDate.ToString("yyyy-MM-dd") + " " + totalTime;

            try
            {

                ServiceProcessor.SubmitOrder(selectedUser.UserID, selectedService.ServiceID,totalDate , currentUser.Token);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Close();
        }

        private void BtnBrowsePet_Click(object sender, RoutedEventArgs e)
        {
            BrowsePetWindow browsePet = new BrowsePetWindow(selectedUser,currentUser);
            selectedPet = browsePet.GetSelectedPet();

            if(selectedUser != null)
            {
               // txtPetName.Text = selectedPet.FullName;
            }
        }

        private void Btn_BrowseServiceClick(object sender, RoutedEventArgs e)
        {
            ViewServiceWindow viewService = new ViewServiceWindow(currentUser);
            selectedService = viewService.GetSelectedService();

            if(selectedService != null)
            {
                txtService.Text = selectedService.ServiceName;
            }

        }
    }
}
