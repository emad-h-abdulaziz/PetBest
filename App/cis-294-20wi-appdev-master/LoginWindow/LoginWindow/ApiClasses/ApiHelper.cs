using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LoginWindow.ApiClasses
{
    /* This is the class that will create the object that
     * will comunicate with the API
     * Ali */
    public class ApiHelper
    {
        /* I don't particularly know what the HttpClient class does
         * but we need it to comunitcate to a web server
         * Ali*/
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            /* This line of code will make sure that all incoming information will be recieved as JSON
             * Ali*/
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
