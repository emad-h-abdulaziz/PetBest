using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWindow.ApiClasses
{
    /* This is where all of the User information
     * will be stored
     * Ali*/
    public class UserModel
    {
        public UserModel()
        {

        }

        public UserModel(string Firstname, string Lastname, string Email, string Privileges, string Username,string Birthdate, string Hiredate)
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.Email = Email;
            this.Privileges = Privileges;
            this.Username = Username;
            this.BirthDate = Birthdate;
            this.HireDate = Hiredate;
        }

        public UserModel(string Firstname, string Lastname, string Email, string Privileges, string Username, string Birthdate, string Hiredate, string Password)
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.Email = Email;
            this.Privileges = Privileges;
            this.Username = Username;
            this.BirthDate = Birthdate;
            this.HireDate = Hiredate;
            this.Password = Password;
        }


        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }        
        public string Privileges { get; set; }
        public string UserID { get; set; } 
        public string Password { get; set; }
        public string HireDate { get; set; }
        public string BirthDate { get; set; }
        public string Token { get; set; }
        
        public override string ToString()
        {
                return "ID: " + UserID + ", " + Firstname + " " + Lastname + ", " + Privileges;
        }
    }
}
