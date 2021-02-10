using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LoginWindow.ApiClasses
{
    class InvoiceProcessor
    {
        //  getUnpaidInvoices - Returns all invoices that have not been paid (user must be admin). -Abad M
        public static async Task<List<InvoiceModel>> getUnpaidInvoices (string privileges, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","getUnpaidInvoices"),
                new KeyValuePair<string, string>("privileges",privileges),
                new KeyValuePair<string, string>("token",token)
            };

            
            HttpContent q = new FormUrlEncodedContent(queries);

           
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<InvoiceModel> result = await response.Content.ReadAsAsync<List<InvoiceModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        //  getPaidInvoice - Returns all invoices that have been paid (user must be admin). -Abad M

        public static async Task<List<InvoiceModel>> getPaidinvoices(string privileges, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","getPaidInvoices"),
                new KeyValuePair<string, string>("privileges",privileges),
                new KeyValuePair<string, string>("token",token)
            };


            HttpContent q = new FormUrlEncodedContent(queries);


            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<InvoiceModel> result = await response.Content.ReadAsAsync<List<InvoiceModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        //  getOverdueInvoice - Returns all invoices that are past their duedate. Abad M

        public static async Task<List<InvoiceModel>> getOverdueInvoices(string privileges, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","getOverdueInvoices"),
                new KeyValuePair<string, string>("privileges",privileges),
                new KeyValuePair<string, string>("token",token)
            };


            HttpContent q = new FormUrlEncodedContent(queries);


            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<InvoiceModel> result = await response.Content.ReadAsAsync<List<InvoiceModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
        public static async Task<List<InvoiceModel>> getUnderdueInvoices(string privileges, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","getUnderdueInvoices"),
                new KeyValuePair<string, string>("privileges",privileges),
                new KeyValuePair<string, string>("token",token)
            };


            HttpContent q = new FormUrlEncodedContent(queries);


            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<InvoiceModel> result = await response.Content.ReadAsAsync<List<InvoiceModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
        public static async Task<InvoiceModel> EmailInvoice(string userID,string orderStatus, string orderTotal, string paymentTotal, string dateOfCompletion, string notes, string orderID, string servicename, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","emailInvoice"),
                new KeyValuePair<string, string>("userID",userID),
                new KeyValuePair<string, string>("orderStatus",orderStatus),
                new KeyValuePair<string, string>("orderTotal",orderTotal),
                new KeyValuePair<string, string>("paymentTotal",paymentTotal),
                new KeyValuePair<string, string>("dateOfCompletion",dateOfCompletion),
                new KeyValuePair<string, string>("notes",notes),
                new KeyValuePair<string, string>("orderID",orderID),
                new KeyValuePair<string, string>("servicename",servicename),
                new KeyValuePair<string, string>("token",token)
            };


            HttpContent q = new FormUrlEncodedContent(queries);


            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
    }
}
