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
    /// Interaction logic for PetProfile.xaml
    /// </summary>
    public partial class PetProfile : Window
    {
        UserModel currentUser;
        UserModel selectedUser;
        List<PetModel> pets;
        public PetProfile(UserModel cUser, UserModel sUser)
        {
            InitializeComponent();
            currentUser = cUser;
            selectedUser = sUser;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void PetProfileWindow_Loaded(object sender, RoutedEventArgs e)
        {
            pets = await PetProcessor.ShowPetsByCustomer(selectedUser.UserID, currentUser.Token);
            foreach (PetModel p in pets)
            {
                lstPets.Items.Add(p);
            }
        }
    }
}
