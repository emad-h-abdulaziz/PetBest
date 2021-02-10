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
    /// Interaction logic for BrowsePetWindow.xaml
    /// </summary>
    public partial class BrowsePetWindow : Window
    {
        private UserModel sUser;
        private UserModel cUser;
        List<PetModel> pets;

        public BrowsePetWindow()
        {
            InitializeComponent();
            FillListBox();
        }

        public BrowsePetWindow(UserModel selectedUser,UserModel currentUser)
        {
            InitializeComponent();
            this.cUser = currentUser;
            this.sUser = selectedUser;
            FillListBox();
        }


        private async void FillListBox()
        {
            pets = await UserProcessor.ShowPetsByCustomer(sUser.UserID,cUser.Token);

            foreach (PetModel p in pets)
            {
                    lstPets.Items.Add(p);
            }
        }

        public PetModel GetSelectedPet()
        {
            this.ShowDialog();
            return (PetModel)lstPets.SelectedItem;
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (lstPets.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a pet");
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
