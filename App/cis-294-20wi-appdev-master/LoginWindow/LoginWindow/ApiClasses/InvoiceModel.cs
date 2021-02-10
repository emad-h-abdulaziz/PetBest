using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWindow.ApiClasses
{
    public class InvoiceModel
    {
        public string InvoiceID { get; set; }
        public string OrderID { get; set; }
        public string CustomerID { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceTotal { get; set; }
        public string PaymentTotal { get; set; }
        public string InvoiceDueDate { get; set; }
        public string PaymentDate { get; set; }
        public string IsPayed { get; set; }
        public string PaymentMethod { get; set; }

        public string Notes { get; set; }

        
        public override string ToString()
        {

            return "Invoice ID: " + InvoiceID + " Invoice Date: " + InvoiceDate + " Invoice Total: " + InvoiceTotal + " Invoice Due Date: " + InvoiceDueDate;
        }
    }
}
