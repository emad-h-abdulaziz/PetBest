using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWindow.ApiClasses
{
    public class ServiceModel
    {
        public string ServiceID { get; set; }
        public string CategoryID { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string DateAdded { get; set; }
        public string Price { get; set; }

        public override string ToString()
        {
            return ServiceID + " - " + ServiceName;
        }
    }
}
