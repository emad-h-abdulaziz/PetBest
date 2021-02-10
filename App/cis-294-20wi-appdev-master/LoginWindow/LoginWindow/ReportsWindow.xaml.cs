using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ReportsWindow.xaml
    /// </summary>
    public partial class ReportsWindow : Window
    {
        private UserModel currentUser;
        private UserModel selectedUser;
        private UserModel selectedEmployee;
        private List <OrderModel> selectedOrders;
        private DateTime startDate;
        private DateTime endDate;
        private List<PetModel> selectedPets;
        private List<InvoiceModel> selectedInvoices;

        public ReportsWindow()
        {
            InitializeComponent();
        }

        public ReportsWindow(UserModel currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
        }

        // Sending back a customer to window

        private void BtnBrowseCustomer_Click(object sender, RoutedEventArgs e)
        {

            BrowseUserWindow browseUser = new BrowseUserWindow(currentUser, "Customer");
            selectedUser = browseUser.GetSelectedUser();

            if (selectedUser != null)
            {
                txtCustomer.Text = selectedUser.Firstname + " " + selectedUser.Lastname;
            }
        }

        // Sending back an employee to window

        private void BtnBrowseEmployee_Click(object sender, RoutedEventArgs e)
        {
            BrowseUserWindow browseUser = new BrowseUserWindow(currentUser, "Employee");
            selectedEmployee = browseUser.GetSelectedUser();

            if (selectedEmployee != null)
            {
                txtEmployee.Text = selectedEmployee.Firstname + " " + selectedEmployee.Lastname;
            }
        }

        private async void BtnAppend_Click(object sender, RoutedEventArgs e)
        {
            // Date to use for string format
            try
            {

                // Validating Date & Report is Picked -- LR
                if (cmbReports.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a report");
                    return;
                }
           

                // If In-Progress Orders is checked -- Lr 

                if (cmbReports.SelectedIndex == 0)
                {
                    if (dateStart.SelectedDate == null || dateEnd.SelectedDate == null)
                    {
                        MessageBox.Show("Please select a start and end date");
                        return;
                    }

                    startDate = (DateTime)dateStart.SelectedDate;
                    endDate = (DateTime)dateEnd.SelectedDate;
                    selectedOrders = await OrderProcessor.ShowAllProgressOrders(currentUser.Privileges, currentUser.Token);
                    if(selectedUser == null && selectedEmployee == null)
                    {

                        lstReports.Items.Add("\t\t\t\t\tIn Progress Orders from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }
                    else if (selectedUser != null && selectedEmployee == null)
                    {

                        lstReports.Items.Add("\t\t\t\t\tIn Progress Orders for " + selectedUser.Firstname + " " + selectedUser.Lastname + " from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }
                    else if (selectedUser != null && selectedEmployee != null)
                    {

                        lstReports.Items.Add("\t\t\t\t\tIn Progress Orders for " + selectedUser.Firstname + " " + selectedUser.Lastname + " with " + selectedEmployee.Firstname + " " + selectedEmployee.Lastname + " from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }

                    FillListBox();

                }
               // If Completed Orders is checked -- Lr

                if(cmbReports.SelectedIndex == 1)
                {
                    if (dateStart.SelectedDate == null || dateEnd.SelectedDate == null)
                    {
                        MessageBox.Show("Please select a start and end date");
                        return;
                    }
                    startDate = (DateTime)dateStart.SelectedDate;
                    endDate = (DateTime)dateEnd.SelectedDate;
                    selectedOrders = await OrderProcessor.ShowAllCompleteOrders(currentUser.Privileges, currentUser.Token);
                    if (selectedUser == null && selectedEmployee == null)
                    {

                        lstReports.Items.Add("\t\t\t\t\tCompleted Orders from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }
                    else if (selectedUser != null && selectedEmployee == null)
                    {

                        lstReports.Items.Add("\t\t\t\t\tCompleted Orders for " + selectedUser.Firstname + " " + selectedUser.Lastname + " from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }
                    else if (selectedUser != null && selectedEmployee != null)
                    {

                        lstReports.Items.Add("\t\t\t\t\tCompleted Orders for " + selectedUser.Firstname + " " + selectedUser.Lastname + " with " + selectedEmployee.Firstname + " " + selectedEmployee.Lastname + " from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }
                    FillListBox();


                }

                // If Cancelled Orders is checked -- Lr

                if(cmbReports.SelectedIndex == 2)
                {
                    if (dateStart.SelectedDate == null || dateEnd.SelectedDate == null)
                    {
                        MessageBox.Show("Please select a start and end date");
                        return;
                    }
                    startDate = (DateTime)dateStart.SelectedDate;
                    endDate = (DateTime)dateEnd.SelectedDate;

                    selectedOrders = await OrderProcessor.ShowAllCancelledOrders(currentUser.Privileges, currentUser.Token); if (selectedUser == null && selectedEmployee == null)
                    {

                        lstReports.Items.Add("\t\t\t\t\tCancelled Orders from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }
                    else if (selectedUser != null && selectedEmployee == null)
                    {

                        lstReports.Items.Add("\t\t\t\t\tCancelled Orders for " + selectedUser.Firstname + " " + selectedUser.Lastname + " from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }
                    else if (selectedUser != null && selectedEmployee != null)
                    {

                        lstReports.Items.Add("\t\t\t\t\tCancelled Orders for " + selectedUser.Firstname + " " + selectedUser.Lastname + " with " + selectedEmployee.Firstname + " " + selectedEmployee.Lastname + " from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }
                    FillListBox();
                }

                // If Customer's Orders is checked -- Lr

                if(cmbReports.SelectedIndex == 3)
                {
                    if (dateStart.SelectedDate == null || dateEnd.SelectedDate == null)
                    {
                        MessageBox.Show("Please select a start and end date");
                        return;
                    }
                    startDate = (DateTime)dateStart.SelectedDate;
                    endDate = (DateTime)dateEnd.SelectedDate;
                    selectedOrders = await OrderProcessor.ShowAllOrders(currentUser.Privileges, currentUser.Token);
               
                    if (string.IsNullOrEmpty(txtCustomer.Text))
                    {
                        MessageBox.Show("Please select a customer");
                        return;
                    }
                    lstReports.Items.Add("\t\t\t\t\t" + txtCustomer.Text + "'s orders from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    FillListBox();
                }

                // If Pet's By Customer is checked -- Lr

                if (cmbReports.SelectedIndex == 4)
                {
                    if (string.IsNullOrEmpty(txtCustomer.Text))
                    {
                        MessageBox.Show("Please select a customer");
                        return;
                    }


                    lstReports.Items.Add("\t\t\t\t\t" + txtCustomer.Text + "'s Pets");

                    selectedPets = await PetProcessor.ShowPetsByCustomer(selectedUser.UserID, currentUser.Token);

                    foreach(PetModel m in selectedPets)
                    {
                        lstReports.Items.Add(m);
                    }
                }

                // If Employee Assigned Orders is checked -- Lr

                if (cmbReports.SelectedIndex == 5)
                {
                    if (string.IsNullOrEmpty(txtEmployee.Text))
                    {
                        MessageBox.Show("Please select an employee.");
                        return;
                    }
                    if (dateStart.SelectedDate == null || dateEnd.SelectedDate == null)
                    {
                        MessageBox.Show("Please select a start and end date");
                        return;
                    }
                    startDate = (DateTime)dateStart.SelectedDate;
                    endDate = (DateTime)dateEnd.SelectedDate;
                    lstReports.Items.Add("\t\t\t\t\t" + txtEmployee.Text + "'s schedule from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    selectedOrders = await OrderProcessor.GetAssignedRequest(selectedEmployee.UserID, currentUser.Token);


                    FillListBox();
                
                }

                // If Paid Invoices is checked -- LR

                if (cmbReports.SelectedIndex == 6)
                {
                    /// Validation
                    if (dateStart.SelectedDate == null || dateEnd.SelectedDate == null)
                    {
                        MessageBox.Show("Please select a start and end date");
                        return;
                    }
                    if (dateStart.SelectedDate == null || dateEnd.SelectedDate == null)
                    {
                        MessageBox.Show("Please select a start and end date");
                        return;
                    }
                    startDate = (DateTime)dateStart.SelectedDate;
                    endDate = (DateTime)dateEnd.SelectedDate;

                    /// Header If Statement

                    if (selectedUser != null)
                    {
                    lstReports.Items.Add("\t\t\t\t\t" + txtCustomer.Text + "'s paid invoices from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }
                    else
                    {
                    lstReports.Items.Add("\t\t\t\t\t" + "Paid Invoices from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }

                    /// Calling API Service for Unpaid Invoices

                    selectedInvoices = await InvoiceProcessor.getPaidinvoices(currentUser.Privileges, currentUser.Token);


                    /// Testing to see if there are any paid invoices for date 

                    if (selectedInvoices == null)
                    {
                        MessageBox.Show("There are no invoices to display.");
                        return;
                    }

                    /// Adding invoices in listbox

                    foreach (InvoiceModel m in selectedInvoices)
                    {

                        if(selectedUser != null)
                        {
                            if (m.CustomerID == selectedUser.UserID)
                            {
                                if (DateTime.Parse(m.InvoiceDate) >= dateStart.SelectedDate && DateTime.Parse(m.InvoiceDate) <= dateEnd.SelectedDate)
                                {
                                    lstReports.Items.Add(m);
                                }
                            }
                        }

                        else
                        {
                            if (DateTime.Parse(m.InvoiceDate) >= dateStart.SelectedDate && DateTime.Parse(m.InvoiceDate) <= dateEnd.SelectedDate)
                            {
                                lstReports.Items.Add(m);
                            }
                        }

                    }
                }

                // If Unpaid Invoices is checked -- LR

                if (cmbReports.SelectedIndex == 7)
                {
                    /// Validation

                    if (dateStart.SelectedDate == null || dateEnd.SelectedDate == null)
                    {
                        MessageBox.Show("Please select a start and end date");
                        return;
                    }
                    if (dateStart.SelectedDate == null || dateEnd.SelectedDate == null)
                    {
                        MessageBox.Show("Please select a start and end date");
                        return;
                    }
                    startDate = (DateTime)dateStart.SelectedDate;
                    endDate = (DateTime)dateEnd.SelectedDate;


                    /// Header If Statement

                    if (selectedUser != null)
                    {
                        lstReports.Items.Add("\t\t\t\t\t" + txtCustomer.Text + "'s unpaid invoices from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }
                    else
                    {
                        lstReports.Items.Add("\t\t\t\t\t" + "Unpaid Invoices from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }

                    /// Calling API Service for Unpaid Invoices 

                    selectedInvoices = await InvoiceProcessor.getUnpaidInvoices(currentUser.Privileges, currentUser.Token);

                    /// Testing to see if there are any paid invoices for date 

                    if (selectedInvoices == null)
                    {
                        MessageBox.Show("There are no invoices to display.");
                        return;
                    }

                    /// Adding invoices in listbox

                    foreach (InvoiceModel m in selectedInvoices)
                    {
                        if (selectedUser != null)
                        {
                            if (m.CustomerID == selectedUser.UserID)
                            {
                                if (DateTime.Parse(m.InvoiceDate) >= dateStart.SelectedDate && DateTime.Parse(m.InvoiceDate) <= dateEnd.SelectedDate)
                                {
                                    lstReports.Items.Add(m);
                                }
                            }
                        }

                        else
                        {
                            if (DateTime.Parse(m.InvoiceDate) >= dateStart.SelectedDate && DateTime.Parse(m.InvoiceDate) <= dateEnd.SelectedDate)
                            {
                                lstReports.Items.Add(m);
                            }
                        }
                    }
                }
                // test comment
                // If overdue invoices is checked -- LR

                if (cmbReports.SelectedIndex == 8)
                {

                    

                    if (dateStart.SelectedDate == null || dateEnd.SelectedDate == null)
                    {
                        MessageBox.Show("Please select a start and end date");
                        return;
                    }


                    startDate = (DateTime)dateStart.SelectedDate;
                    endDate = (DateTime)dateEnd.SelectedDate;

                    selectedInvoices = await InvoiceProcessor.getOverdueInvoices(currentUser.Privileges, currentUser.Token);

                    if (selectedInvoices == null)
                    {
                        MessageBox.Show("There are no invoices to display.");
                        return;
                    }

                    if (selectedUser != null)
                    {
                        lstReports.Items.Add("\t\t\t\t\t" + txtCustomer.Text + "'s overdue invoices from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }
                    else
                    {
                        lstReports.Items.Add("\t\t\t\t\t" + "Overdue invoices from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }

                    foreach (InvoiceModel m in selectedInvoices)
                    {
                        if (selectedUser != null)
                        {
                            if (m.CustomerID == selectedUser.UserID)
                            {
                                if (DateTime.Parse(m.InvoiceDate) >= dateStart.SelectedDate && DateTime.Parse(m.InvoiceDate) <= dateEnd.SelectedDate)
                                {
                                    lstReports.Items.Add(m);
                                }
                            }
                        }

                        else
                        {
                            if (DateTime.Parse(m.InvoiceDate) >= dateStart.SelectedDate && DateTime.Parse(m.InvoiceDate) <= dateEnd.SelectedDate)
                            {
                                lstReports.Items.Add(m);
                            }
                        }
                    }
                }

                // If due invoices is checked -- LR

                if (cmbReports.SelectedIndex == 9)
                {


                    if (dateStart.SelectedDate == null || dateEnd.SelectedDate == null)
                    {
                        MessageBox.Show("Please select a start and end date");
                        return;
                    }


                    startDate = (DateTime)dateStart.SelectedDate;
                    endDate = (DateTime)dateEnd.SelectedDate;

                    selectedInvoices = await InvoiceProcessor.getUnderdueInvoices(currentUser.Privileges, currentUser.Token);

                    if (selectedInvoices == null)
                    {
                        MessageBox.Show("There are no invoices to display.");
                        return;
                    }

                    if (selectedUser != null)
                    {
                        lstReports.Items.Add("\t\t\t\t\t" + txtCustomer.Text + "'s due invoices (not overdue) from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }
                    else
                    {
                        lstReports.Items.Add("\t\t\t\t\t" + "Due invoices (not overdue) from " + startDate.ToString("MM/dd/yyyy") + " to " + endDate.ToString("MM/dd/yyyy"));
                    }

                    foreach (InvoiceModel m in selectedInvoices)
                    {
                        if (selectedUser != null)
                        {
                            if (m.CustomerID == selectedUser.UserID)
                            {
                                if (DateTime.Parse(m.InvoiceDate) >= dateStart.SelectedDate && DateTime.Parse(m.InvoiceDate) <= dateEnd.SelectedDate)
                                {
                                    lstReports.Items.Add(m);
                                }
                            }
                        }

                        else
                        {
                            if (DateTime.Parse(m.InvoiceDate) >= dateStart.SelectedDate && DateTime.Parse(m.InvoiceDate) <= dateEnd.SelectedDate)
                            {
                                lstReports.Items.Add(m);
                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void FillListBox()
        {
            if (selectedOrders == null)
            {
                MessageBox.Show("There are no results to display.");
                return;
            }

            if (dateStart.SelectedDate == null || dateEnd.SelectedDate == null)
            {
                MessageBox.Show("Please select a start and end date");
                return;
            }

            startDate = DateTime.Parse(dateStart.SelectedDate.ToString());
            endDate = DateTime.Parse(dateEnd.SelectedDate.ToString());

            if (cmbReports.SelectedIndex >= 0 && cmbReports.SelectedIndex <= 3)
            {
                foreach (OrderModel m in selectedOrders)
                {
                    if (DateTime.Parse(m.ServiceDateRequest) >= startDate && DateTime.Parse(m.ServiceDateRequest) <= endDate)
                    {
                        if (string.IsNullOrEmpty(txtCustomer.Text) && string.IsNullOrEmpty(txtEmployee.Text))
                        {
                            lstReports.Items.Add(m);
                        }

                        if (string.IsNullOrEmpty(txtCustomer.Text) && !string.IsNullOrEmpty(txtEmployee.Text))
                        {
                            if (selectedEmployee.UserID == m.EmployeeID)
                            {
                                lstReports.Items.Add(m);
                            }
                        }
                        if (!string.IsNullOrEmpty(txtCustomer.Text) && string.IsNullOrEmpty(txtEmployee.Text))
                        {
                            if (txtCustomer.Text == (m.FirstName + " " + m.LastName))
                            {
                                lstReports.Items.Add(m);
                            }
                        }
                        if (!string.IsNullOrEmpty(txtCustomer.Text) && !string.IsNullOrEmpty(txtEmployee.Text))
                        {
                            if ((txtCustomer.Text == (m.FirstName + " " + m.LastName)) && selectedEmployee.UserID == m.EmployeeID)
                            {
                                lstReports.Items.Add(m);
                            }
                        }
                    }
                }
            }
            if(cmbReports.SelectedIndex == 5)
            {
                

                foreach (OrderModel m in selectedOrders)
                {

                    if (!string.IsNullOrEmpty(txtCustomer.Text) && !string.IsNullOrEmpty(txtEmployee.Text))
                    {
                        if (txtCustomer.Text == (m.FirstName + " " + m.LastName))
                        {
                            lstReports.Items.Add(m);
                        }
                    }
                    if (string.IsNullOrEmpty(txtCustomer.Text))
                    {
                        lstReports.Items.Add(m);
                    }

                }
            }
            
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            lstReports.Items.Clear();
            
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            int count=0;
            try
            {
                this.IsEnabled = false;

                PrintDialog pd = new PrintDialog();
                FlowDocument fd = new FlowDocument();
                fd.ColumnWidth = 800;                
                foreach (object item in lstReports.Items)
                {
                    ++count;
                    fd.Blocks.Add(new Paragraph(new Run("#" + count)));
                    fd.Blocks.Add(new Paragraph(new Run(item.ToString())));
                    fd.Blocks.Add(new Paragraph(new Run("----------------------------------------------------------------------------------------------------------------------------")));
                }
                IDocumentPaginatorSource idocument = fd as IDocumentPaginatorSource;

                pd.PrintDocument(idocument.DocumentPaginator, "Printing flow document...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        private void BtnClearFields_Click(object sender, RoutedEventArgs e)
        {
            txtCustomer.Clear();
            txtEmployee.Clear();
            dateStart.SelectedDate = null;
            dateStart.DisplayDate = DateTime.Today;
            dateEnd.SelectedDate = null;
            dateEnd.DisplayDate = DateTime.Today;
            cmbReports.SelectedIndex = -1;
        }

        private void BtnClearRow_Click(object sender, RoutedEventArgs e)
        {
            if (lstReports.SelectedIndex != -1)
            {
                lstReports.Items.Remove(lstReports.SelectedItem);
            }
        }

        private void BtnDone_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
