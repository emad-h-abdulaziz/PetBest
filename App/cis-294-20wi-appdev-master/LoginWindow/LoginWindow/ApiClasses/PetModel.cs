using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWindow.ApiClasses
{
    public class PetModel
    {
        public PetModel()
        {

        }

        public PetModel(string FullName, string Birthdate, string Color, string Weight, string Breed)
        {
            this.FullName = FullName;
            this.Birthdate = Birthdate;
            this.Color = Color;
            this.Weight = Weight;
            this.Breed = Breed;
        }

        public string PetID { get; set; }
        public string FullName { get; set; }
        public string Birthdate { get; set; }
        public string Age { get; set; }
        public string Color { get; set; }
        public string Weight { get; set; }
        public string Breed { get; set; }
        public string UserID { get; set; }

        public override string ToString()
        {
            return "ID: " + PetID + " Name: " + FullName + " DOB: " + Birthdate + " " + Age + " Specs: " + Color + " " + Weight + " lb(s) " + Breed;
        }

    }
}
