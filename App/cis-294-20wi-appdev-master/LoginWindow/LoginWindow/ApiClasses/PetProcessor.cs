using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LoginWindow.ApiClasses
{
    class PetProcessor
    {
        /* this function creates a pet in the database
         * with the information that is passed in
         * Ali*/
        public static async Task<PetModel> CreatePet(string fullName, string birthDate, string color, string weight, string breed, string userID, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","createPet"),
                new KeyValuePair<string, string>("fullname",fullName),
                new KeyValuePair<string, string>("birthdate",birthDate),
                new KeyValuePair<string, string>("color",color),
                new KeyValuePair<string, string>("pweight",weight),
                new KeyValuePair<string, string>("breed",breed),
                new KeyValuePair<string, string>("userID",userID),
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

        /* this function updates a pet in the database
         * Hishaam*/
        public static async Task<PetModel> UpdatePet(string petID, string fullName, string birthDate, string color, string weight, string breed, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php";
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","updatePet"),
                new KeyValuePair<string, string>("petID", petID),
                new KeyValuePair<string, string>("fullname",fullName),
                new KeyValuePair<string, string>("birthdate",birthDate),
                new KeyValuePair<string, string>("color",color),
                new KeyValuePair<string, string>("weight",weight),
                new KeyValuePair<string, string>("breed",breed),
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

        /* this function returns one pet object
         * based on the petID passed in
         * Ali*/
        public static async Task<PetModel> ReadPet(string petID, string token)
        {
            string url = "https://cis294.hfcc.edu/api.php?";

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("request","readPet"),
                new KeyValuePair<string, string>("petID",petID),
                new KeyValuePair<string, string>("token",token)
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, q))
            {
                if (response.IsSuccessStatusCode)
                {
                    PetResultModel result = await response.Content.ReadAsAsync<PetResultModel>();
                    return result.Result;
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
                new KeyValuePair<string, string>("token",token)
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

    }
}
