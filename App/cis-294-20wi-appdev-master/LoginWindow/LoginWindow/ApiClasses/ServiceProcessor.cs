using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoginWindow.ApiClasses
{
    public class ServiceProcessor
    {
        /* Returns a list of all services
         * Hishaam*/
        public static async Task<List<ServiceModel>> ShowAllServices(string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","showAllServices"),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    ServiceResultModel result = await response.Content.ReadAsAsync<ServiceResultModel>();
                    return result.Services;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /* Submits a service request to create an order
         * Ali*/
        public static async void SubmitOrder(string userID, string serviceID, string appointmentDate, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","submitOrder"),
                new KeyValuePair<string, string>("userID",userID),
                new KeyValuePair<string, string>("serviceID",serviceID),
                new KeyValuePair<string, string>("appointmentDate",appointmentDate),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Order Submitted!", "Success");
                }
                else
                {
                    MessageBox.Show(response.ReasonPhrase, "Error");
                }
            }
        }
    }
}
