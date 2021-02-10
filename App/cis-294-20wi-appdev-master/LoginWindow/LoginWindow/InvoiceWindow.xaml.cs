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
    /// Interaction logic for InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window
    {
        private InvoiceModel currentInvoice;
        private UserModel currentUser;
        private UserModel invoiceUser;
        private OrderModel invoiceOrder;
        private AddressModel userAddress;
        public InvoiceWindow(InvoiceModel i, UserModel c)
        {
            InitializeComponent();
            currentInvoice = i;
            currentUser = c;
            
        }

        public InvoiceWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtInvoiceNumber.Text = currentInvoice.InvoiceID;
            txtOrderNumber.Text = currentInvoice.OrderID;
            txtBalance.Text = currentInvoice.InvoiceTotal;
            txtAmountPaid.Text = currentInvoice.PaymentTotal;
            invoiceUser = await UserProcessor.GetUsers(currentUser.Token, currentInvoice.CustomerID);
            userAddress = await UserProcessor.GetAddress(currentUser.Token, invoiceUser.UserID);
            //invoiceOrder = await OrderProcessor.GetOrderByOrderID(currentInvoice.OrderID, currentUser.Token);
            txtClientName.Text = invoiceUser.Firstname + " " + invoiceUser.Lastname;
            txtEmail.Text = invoiceUser.Email;
            txtStartDtae1.Text = currentInvoice.InvoiceDate;
            txtEndDate1.Text = currentInvoice.InvoiceDueDate;
            txtPaymentDate1.Text = currentInvoice.PaymentDate;
            txtAddress.Text = userAddress.Line1;
            txtCity.Text = userAddress.City;
            txtState.Text = userAddress.State;
            txtZIp.Text = userAddress.ZipCode;
            txtPhone.Text = userAddress.Phone;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;

                PrintDialog pd = new PrintDialog();
                FlowDocument fd = new FlowDocument();
                fd.ColumnWidth = 800;
                fd.Blocks.Add(new Paragraph(new Run("Pet Best LLC")));
                fd.Blocks.Add(new Paragraph(new Run("Customer: " + invoiceUser.Firstname + " " + invoiceUser.Lastname)));
                fd.Blocks.Add(new Paragraph(new Run("Address: " + userAddress.Line1 + " " + userAddress.City+" "+userAddress.State)));
                fd.Blocks.Add(new Paragraph(new Run("ZipCode: " + userAddress.ZipCode )));
                fd.Blocks.Add(new Paragraph(new Run("Phone #: " + userAddress.Phone)));
                fd.Blocks.Add(new Paragraph(new Run("Invoice ID #: "+currentInvoice.InvoiceID)));
                fd.Blocks.Add(new Paragraph(new Run("Order ID #: " + currentInvoice.OrderID)));
                fd.Blocks.Add(new Paragraph(new Run("Invoice Date: " + currentInvoice.InvoiceDate+"\t\t\t"+"Due Date: "+currentInvoice.InvoiceDueDate)));
                fd.Blocks.Add(new Paragraph(new Run("Invoice Total: " + currentInvoice.InvoiceTotal+"\t\t\t"+"Payment Total: "+currentInvoice.PaymentTotal)));
                fd.Blocks.Add(new Paragraph(new Run("Notes: \n"+currentInvoice.Notes)));

                IDocumentPaginatorSource idocument = fd as IDocumentPaginatorSource;

                pd.PrintDocument(idocument.DocumentPaginator, "Printing flow document...");
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        private async void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            await InvoiceProcessor.EmailInvoice(invoiceUser.UserID, invoiceOrder.OrderStatus, invoiceOrder.OrderTotal, currentInvoice.PaymentTotal, invoiceOrder.OrderDateComplete, currentInvoice.Notes, currentInvoice.OrderID, invoiceOrder.ServiceName,currentUser.Token);
        }
    }
}
