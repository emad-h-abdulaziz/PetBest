using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWindow.ApiClasses
{
    public class AddressModel
    {
        public AddressModel(string l, string c, string s, string z,string p)
        {
            Line1 = l;
            City = c;
            State = s;
            ZipCode = z;
            Phone = p;
        }
        public string AddressID { get; set; }
        public string UserID { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
    }
}
