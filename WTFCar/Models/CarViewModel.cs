using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WTFCar.Models
{
    public class CarViewModel
    {
        public string Year { get; set; }
        public string Make { get; set; }

        public string Model { get; set; }

        public string ForumURL { get; set; }

        public CarViewModel(string year, string make, string model)
        {
            this.Year = year;
            this.Make = make;
            this.Model = Model;
        }
    }
}