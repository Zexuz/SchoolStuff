using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMDB.MovieDatabase.Domain.ValueObjects
{
    public class ProductionYear
    {
        private readonly static int FirstMovieProductionYear = 1896;
        public int Value { get; set; }

        public ProductionYear() { }
        public ProductionYear(int year)
        {
            if (IsValid(year))
            {
                Value = year;
            }
            else
            {
                throw new ArgumentException($"Production year must be a valid year later than {FirstMovieProductionYear}.");
            }

        } 

        private static bool IsValid(int year)
        {
            return year >= FirstMovieProductionYear && year <= DateTime.Today.Year;
        }
        public static bool TryParse(string productionYear, out ProductionYear result)
        {
            result = null;
            int year;
            if (int.TryParse(productionYear, out year))
            {
                if (IsValid(year))
                {
                    result = new ProductionYear(year);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
