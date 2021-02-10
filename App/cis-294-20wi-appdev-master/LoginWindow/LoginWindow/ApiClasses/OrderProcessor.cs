using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoginWindow.ApiClasses
{
    class OrderProcessor
    {
        /*Returns a list of all orders
         * Ali*/
        public static async Task<List<OrderModel>> ShowAllOrders(string privileges, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","showAllOrders"),
                new KeyValuePair<string, string>("privileges",privileges),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<OrderModel> result = await response.Content.ReadAsAsync<List<OrderModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /*Returns a list of all completed orders
         * Ali*/
        public static async Task<List<OrderModel>> ShowAllCompleteOrders(string privileges, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","showAllCompleteOrders"),
                new KeyValuePair<string, string>("privileges",privileges),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<OrderModel> result = await response.Content.ReadAsAsync<List<OrderModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /*Returns a list of all cancelled orders
         * Ali*/
        public static async Task<List<OrderModel>> ShowAllCancelledOrders(string privileges, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","showAllCompleteOrders"),
                new KeyValuePair<string, string>("privileges",privileges),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<OrderModel> result = await response.Content.ReadAsAsync<List<OrderModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /*Returns a list of all orders in progress
         * Ali*/
        public static async Task<List<OrderModel>> ShowAllProgressOrders(string privileges, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","showAllProgessOrders"),
                new KeyValuePair<string, string>("privileges",privileges),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<OrderModel> result = await response.Content.ReadAsAsync<List<OrderModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async void AssignEmployeeToOrder(string userID, string orderID, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","assignEmployeeToOrder"),
                new KeyValuePair<string, string>("userID",userID),
                new KeyValuePair<string, string>("orderID",orderID),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Employee assigned to order successfully","Success");
                }
                else
                {
                    MessageBox.Show(response.ReasonPhrase, "Error");
                }
            }
        }

        /*Returns orders associated with the employee ID passed in
         * Ali*/
        public static async Task<List<OrderModel>> GetAssignedRequest (string userID, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","getAssignedRequest"),
                new KeyValuePair<string, string>("userID",userID),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<OrderModel> result = await response.Content.ReadAsAsync<List<OrderModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }



        public static async Task<List<OrderModel>> OrderUpdate(string orderID, string userID, string orderStatus, string totalPrice, string amountPaid, string dateOfCompletetion,string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","orderUpdate"),
                new KeyValuePair<string, string>("orderID",orderID),
                new KeyValuePair<string, string>("userID",userID),
                new KeyValuePair<string, string>("orderStatus",orderStatus),
                new KeyValuePair<string, string>("totalPrice", totalPrice),
                new KeyValuePair<string, string>("amountPaid",amountPaid),
                new KeyValuePair<string, string>("dateOfCompletetion",dateOfCompletetion),
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
        public static async Task<List<OrderModel>> ShowCustomerActiveOrders(string userID, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","showCustomerActiveOrders"),
                new KeyValuePair<string, string>("userID",userID),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<OrderModel> result = await response.Content.ReadAsAsync<List<OrderModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task<List<OrderModel>> ShowPastCustomerOrders(string userID, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","showPastCustomerOrders"),
                new KeyValuePair<string, string>("userID",userID),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<OrderModel> result = await response.Content.ReadAsAsync<List<OrderModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task<List<OrderModel>> SubmitOrder(string userID, string serviceID, string orderDate, string serviceDateRequest, string orderTotal, string notes, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","submitOrder"),
                new KeyValuePair<string, string>("userID",userID),
                new KeyValuePair<string, string>("serviceID",serviceID),
                new KeyValuePair<string, string>("orderDate",orderDate),
                new KeyValuePair<string, string>("appointmentDate",serviceDateRequest),
                new KeyValuePair<string, string>("orderTotal",orderTotal),
                new KeyValuePair<string, string>("notes",notes),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<OrderModel> result = await response.Content.ReadAsAsync<List<OrderModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task<OrderModel> GetOrderByOrderID(string orderID, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","submitOrder"),
                new KeyValuePair<string, string>("orderID",orderID),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    OrderResultModel result = await response.Content.ReadAsAsync<OrderResultModel>();
                    return result.Result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }

}
