using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWindow.ApiClasses
{
        public class OrderModel
    {
        public OrderModel(string OrderStatus )
        {
            this.OrderStatus = OrderStatus; 
        }


        public string OrderID { get; set; }
        public string UserID { get; set; }
        public string ServiceDateRequest { get; set; }
        public string OrderDate { get; set; }
        public string OrderTotal { get; set; }
        public string CategoryName { get; set; }
        public string ServiceName { get; set; }
        public string OrderStatus { get; set; }
        public string OrderDateComplete { get; set; }
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TotalPrice { get; set; }
        public string AmountPaid { get; set; }
        public string DateOfCompletetion { get; set; }

        public override string ToString()
        {
            return ServiceDateRequest + " - " + FirstName + " " + LastName + " Order ID: "+OrderID+",  "+CategoryName+": "+ServiceName+",  Status: "+OrderStatus;
        }
    }
}
