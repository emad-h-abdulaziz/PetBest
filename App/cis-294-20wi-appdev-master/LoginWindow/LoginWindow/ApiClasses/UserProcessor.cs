using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoginWindow.ApiClasses
{
    /* This is the class that will actually be 'talking' to the API
     * Ali*/
    public class UserProcessor
    {
        /* I passed in the username and password from the text fields 
         * to validate the key values. if the user enters an invalid
         * username or password, the program will display an invalid
         * message.
         * Ali*/
        public static async Task<UserModel> LoadUser(string username, string password)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","login"),
                new KeyValuePair<string, string>("username",username),
                new KeyValuePair<string, string>("password",password)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    UserResultModel result = await response.Content.ReadAsAsync<UserResultModel>();
                    return result.Result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }


        /* Create User Method to communicate with API 
         Lauren */


        public static async Task<UserModel> CreateUser(string firstName, string lastName, string userName, string password, string email, string privileges,string birthdate,string hiredate, string token)
        {
            DateTime bd, hd;
            if (privileges == "Employee")
            {
                 bd = DateTime.Parse(birthdate);
                 hd = DateTime.Parse(hiredate);
            }
            else
            {
                bd = DateTime.Parse("01/01/0001");
                hd = DateTime.Parse("01/01/0001");
            }

            string url = "https://cis294.hfcc.edu/api.php?";
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","createUser"),
                new KeyValuePair<string, string>("username",userName),
                new KeyValuePair<string, string>("password",password),
                new KeyValuePair<string, string>("firstName",firstName),
                new KeyValuePair<string, string>("lastName",lastName),
                new KeyValuePair<string, string>("privileges",privileges),
                new KeyValuePair<string, string>("email",email),
                new KeyValuePair<string, string>("birthdate",bd.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("hiredate",hd.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url,q))
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

        /* Method to communicate with updateUser API with password
         * Ali*/
        public static async Task<UserModel> UpdateUser(string userid, string firstName, string lastName, string userName,string password, string email, string privileges ,string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","updateUser"),
                new KeyValuePair<string, string>("userID",userid),
                new KeyValuePair<string, string>("username",userName),
                new KeyValuePair<string, string>("password",password),
                new KeyValuePair<string, string>("firstname",firstName),
                new KeyValuePair<string, string>("lastname",lastName),
                new KeyValuePair<string, string>("privileges",privileges),
                new KeyValuePair<string, string>("email",email),
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

        /* Method to communicate with updateUser API without password
         * Ali*/
        public static async Task<UserModel> UpdateUser(string userid, string firstName, string lastName, string userName, string email , string privileges , string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","updateUser"),
                new KeyValuePair<string, string>("userID",userid),
                new KeyValuePair<string, string>("username",userName),
                new KeyValuePair<string, string>("firstname",firstName),
                new KeyValuePair<string, string>("lastname",lastName),
                new KeyValuePair<string, string>("privileges",privileges),
                new KeyValuePair<string, string>("email",email),
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

        /* Method to communicate with getUsers API
         * Ali*/
        public static async Task<List<UserModel>> GetUsers(string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","getUsers"),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<UserModel> result = await response.Content.ReadAsAsync<List<UserModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        public static async Task<List<PetModel>> ShowPetsByCustomer(string userID, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","showPetsByCustomer"),
                new KeyValuePair<string, string>("userID",userID),
                new KeyValuePair<string, string>("token", token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<PetModel> result = await response.Content.ReadAsAsync<List<PetModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        public static async Task<List<UserModel>> ShowAllEmployees(string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","showAllEmployees"),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url,q))
            {
                if(response.IsSuccessStatusCode)
                {
                    List<UserModel> result = await response.Content.ReadAsAsync<List<UserModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task<UserModel> GetUsers(string token, string userID)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","getUsers"),
                new KeyValuePair<string, string>("userID",userID),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<UserModel> result = await response.Content.ReadAsAsync<List<UserModel>>();
                    return result[0];
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
        public static async Task<UserModel> GetUsersByUsername(string token, string username)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","getUsers"),
                new KeyValuePair<string, string>("username",username),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<UserModel> result = await response.Content.ReadAsAsync<List<UserModel>>();
                    return result[0];
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
        public static async Task<AddressModel> GetAddress(string token, string userID)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","getAddress"),
                new KeyValuePair<string, string>("userID",userID),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<AddressModel> result = await response.Content.ReadAsAsync<List<AddressModel>>();
                    return result[0];
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
        public static async Task<AddressModel> CreateAddress(string userID, string line1, string line2, string city, string state, string zipcode, string phone, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","addAddress"),
                new KeyValuePair<string, string>("userID",userID),
                new KeyValuePair<string, string>("line1",line1),
                new KeyValuePair<string, string>("line2",line2),
                new KeyValuePair<string, string>("city",city),
                new KeyValuePair<string, string>("state",state),
                new KeyValuePair<string, string>("zipcode",zipcode),
                new KeyValuePair<string, string>("phone",phone),
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
        public static async Task<AddressModel> UpdateAddress(string userID, string line1, string line2, string city, string state, string zipcode, string phone, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","UpdateAddress"),
                new KeyValuePair<string, string>("userID",userID),
                new KeyValuePair<string, string>("line1",line1),
                new KeyValuePair<string, string>("line2",line2),
                new KeyValuePair<string, string>("city",city),
                new KeyValuePair<string, string>("state",state),
                new KeyValuePair<string, string>("zipcode",zipcode),
                new KeyValuePair<string, string>("phone",phone),
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
