using System;
using System.IO;
using CsvHelper;

namespace Dark_Sky_Comparisons
{
    public class Location
    {
        public string zip_code { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string county { get; set; }

        public Location()
        {
            string zip_code = string.Empty;
            string latitude = string.Empty;
            string longitude = string.Empty;
            string city = string.Empty;
            string state = string.Empty;
            string county = string.Empty;
        }

        public void SearchLocation(string testZip)
        {
            using (var reader = new StreamReader("zip_codes_states.csv"))
            {
                var csv = new CsvReader(reader);
                csv.Read();
                csv.ReadHeader();

                var record = new Location();
                var records = csv.EnumerateRecords(record);
                foreach (var r in records)
                {
                    if (r.zip_code == testZip)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Zip Code: " + r.zip_code);
                        this.zip_code = r.zip_code;
                        this.latitude = r.latitude;
                        this.longitude = r.longitude;
                        this.city = r.city;
                        this.state = r.state;
                        this.county = r.county;
                        Console.WriteLine("Location identified as: " + this.city + ", " + this.state + ".");
                        Console.WriteLine();
                        csv.Dispose();
                        return; //Zip Code Found
                    }
                }

                //Zip Code Not Found
                csv.Dispose();
                Console.WriteLine("Zip Code " + testZip + " Not Found.  Please enter a new Zip Code.");
                var newZip = Console.ReadLine();
                Console.WriteLine();
                SearchLocation(newZip);
            }
        }
    }
}
