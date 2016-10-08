using System;

namespace IMDBThingy.lib.ValueObjects {

    public class ProductionYear {

        public int Year { get; }

        public ProductionYear(int year) {
            if(year > DateTime.Now.Year || year< 1800) throw new Exception("I have no time to implement a new exception for this, but the year is invalid");
            Year = year;
            Year = year;
        }

        public override string ToString() {
            return Year.ToString();
        }

    }

}