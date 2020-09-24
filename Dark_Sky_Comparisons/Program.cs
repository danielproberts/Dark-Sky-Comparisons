using System;
using System.IO;
using System.Configuration;

namespace Dark_Sky_Comparisons
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize Program Variables
            string userSelection;
            string url;
            string zip1 = "";
            string zip2 = "";
            string apiKey = ConfigurationManager.AppSettings["api_key"];
            string savePath = "";

            //Request Data From User
            Console.Clear();
            CenterTitle("Weather Comparisons - Powered by Dark Sky");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Please enter the ZIP Code for the first city you would like to compare: ");
            zip1 = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Please enter the ZIP Code for the second city you would like to compare: ");
            zip2 = Console.ReadLine();
            Console.WriteLine();

            //Check for Same Zip Code
            while (zip1 == zip2)
            {
                Console.WriteLine("Can't compare a location to itself.  Please enter a new Zip Code for the second location: ");
                zip2 = Console.ReadLine();
            }
            Console.WriteLine("Input has been accepted.  Press any key to continue.");
            Console.ReadKey();
            Console.Clear();

            CenterTitle("Searching for Zip Codes...");

            //Set up Data for location1
            DarkSkyData city1 = new DarkSkyData();
            Location location1 = new Location();
            location1.SearchLocation(zip1);
            url = @"https://api.darksky.net/forecast/" + apiKey + "/" + location1.latitude + "," + location1.longitude;
            city1 = city1.pullForecast(url);
            city1.cityName = location1.city;


            //Set up Data for location2
            DarkSkyData city2 = new DarkSkyData();
            Location location2 = new Location();
            location2.SearchLocation(zip2);
            url = @"https://api.darksky.net/forecast/" + apiKey + "/" + location2.latitude + "," + location2.longitude;
            city2 = city2.pullForecast(url);
            city2.cityName = location2.city;

            //Round Temperature Values - Prevents Arithmetic Anomalies
            city1.currently.temperature = System.Math.Round(city1.currently.temperature);
            city2.currently.temperature = System.Math.Round(city2.currently.temperature);

            //Final Setup
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            //Main Program Flow
            while (true)
            {
                Console.Clear();
                CenterTitle("Weather Comparisons - Powered by Dark Sky");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Which metric would you like to compare?");
                Console.WriteLine();
                Console.WriteLine("1 - Temperature");
                Console.WriteLine("2 - Humidity");
                Console.WriteLine("3 - Wind");
                Console.WriteLine("4 - UV Index");
                Console.WriteLine();
                Console.WriteLine("s - Save Comparison Data");
                Console.WriteLine();
                Console.WriteLine("q - Quit");
                Console.WriteLine();
                Console.WriteLine("Please enter a selection, or enter 'q' to quit: ");
                userSelection = Console.ReadLine();
                Console.Clear();


                //Compare Temperature
                if (userSelection == "1")
                {
                    CenterTitle("Weather Comparisons - Powered by Dark Sky");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("The current temperature in " + location1.city + ", " + location1.state + " is: " + System.Math.Round(city1.currently.temperature) + "\x00B0" + "F.");
                    Console.WriteLine("The current temperature in " + location2.city + ", " + location2.state + " is: " + System.Math.Round(city2.currently.temperature) + "\x00B0" + "F.");
                    Console.WriteLine();
                    if (city1.currently.temperature > city2.currently.temperature)
                    {
                        Console.WriteLine("It is currently " + ((System.Math.Round(city1.currently.temperature)) - (System.Math.Round(city2.currently.temperature))) + "\x00B0" + "F warmer in " + city1.cityName + " than " + city2.cityName + ".");
                    }
                    else if (city1.currently.temperature < city2.currently.temperature)
                    {
                        Console.WriteLine("It is currently " + ((System.Math.Round(city2.currently.temperature)) - (System.Math.Round(city1.currently.temperature))) + "\x00B0" + "F warmer in " + city2.cityName + " than " + city1.cityName + ".");
                    }
                    else
                    {
                        Console.WriteLine("It is currently the same temperature in " + city1.cityName + " and " + city2.cityName + ".");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }

                //Compare Humidity
                else if (userSelection == "2")
                {
                    CenterTitle("Weather Comparisons - Powered by Dark Sky");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("The current humidity in " + city1.cityName + " is: " + (city1.currently.humidity) * 100 + "%");
                    Console.WriteLine("The current humidity in " + city2.cityName + " is: " + (city2.currently.humidity) * 100 + "%");
                    Console.WriteLine();
                    if (city1.currently.humidity > city2.currently.humidity)
                    {
                        Console.WriteLine("It is currently " + (city1.currently.humidity * 100 - city2.currently.humidity * 100) + "% more humid in " + city1.cityName + " than in " + city2.cityName + ".");
                    }
                    else if (city1.currently.humidity < city2.currently.humidity)
                    {
                        Console.WriteLine("It is currently " + (city2.currently.humidity * 100 - city1.currently.humidity * 100) + "% more humid in " + city2.cityName + " than in " + city1.cityName + ".");
                    }
                    else
                    {
                        Console.WriteLine("The humidity is currently the same in " + city1.cityName + " and " + city2.cityName + ".");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }

                //Compare Wind
                else if (userSelection == "3")
                {
                    CenterTitle("Weather Comparisons - Powered by Dark Sky");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("The current wind speed in " + city1.cityName + " is: " + city1.currently.windSpeed + " Miles per Hour.");
                    Console.WriteLine("The current wind speed in " + city2.cityName + " is: " + city2.currently.windSpeed + " Miles per Hour.");
                    Console.WriteLine();
                    if (city1.currently.windSpeed > city2.currently.windSpeed)
                    {
                        Console.WriteLine("The wind is currently blowing " + (city1.currently.windSpeed - city2.currently.windSpeed) + " Miles per Hour faster in " + city1.cityName + " than in " + city2.cityName + ".");
                    }
                    else if (city1.currently.windSpeed < city2.currently.windSpeed)
                    {
                        Console.WriteLine("The wind is currently blowing " + (city2.currently.windSpeed - city1.currently.windSpeed) + " Miles per Hour faster in " + city2.cityName + " than in " + city1.cityName + ".");
                    }
                    else
                    {
                        Console.WriteLine("The wind speed is currently the same in " + city1.cityName + " and " + city2.cityName + ".");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }

                //Compare UV Index
                if (userSelection == "4")
                {
                    CenterTitle("Weather Comparisons - Powered by Dark Sky");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("The current UV Index in " + location1.city + ", " + location1.state + " is: " + city1.currently.uvIndex + ".");
                    Console.WriteLine("The current UV Index in " + location2.city + ", " + location2.state + " is: " + city2.currently.uvIndex + ".");
                    Console.WriteLine();
                    if (city1.currently.uvIndex > city2.currently.uvIndex)
                    {
                        Console.WriteLine("The UV Index is currently " + (city1.currently.uvIndex - city2.currently.uvIndex) + " point(s) higher in " + city1.cityName + " than " + city2.cityName + ".");
                    }
                    else if (city1.currently.uvIndex < city2.currently.uvIndex)
                    {
                        Console.WriteLine("The UV Index is currently " + (city2.currently.uvIndex - city1.currently.uvIndex) + " point(s) higher in " + city2.cityName + " than " + city1.cityName + ".");
                    }
                    else
                    {
                        Console.WriteLine("The UV Index is currently the same in " + city1.cityName + " and " + city2.cityName + ".");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }

                //Save Comparison Data
                else if (userSelection == "s")
                {
                    CenterTitle("Weather Comparisons - Powered by Dark Sky");
                    Console.WriteLine();
                    Console.WriteLine();

                    using (var write = new StreamWriter("ComparisonData_" + city1.cityName + "_" + city2.cityName + "_" + DateTime.Today.ToString("yyyyMMdd") + ".txt"))
                    {
                        write.WriteLine("Comparison Data - Powered by Dark Sky");
                        write.WriteLine("Process Date: " + DateTime.Today.ToString("MM-dd-yyyy"));
                        write.WriteLine("Process Time: " + string.Format("{0:HH:mm:sstt}", DateTime.Now));
                        write.WriteLine();
                        write.WriteLine("** Current Weather Data for " + city1.cityName + ", " + location1.state + " **");
                        write.WriteLine("Current Conditions: " + city1.currently.summary);
                        write.WriteLine("The current temperature is: " + System.Math.Round(city1.currently.temperature) + "\x00B0" + "F.");
                        write.WriteLine("The current apparent temperature is: " + System.Math.Round(city1.currently.apparentTemperature) + "\x00B0" + "F.");
                        write.WriteLine("The current humidity is: " + city1.currently.humidity * 100 + "%.");
                        write.WriteLine("The current wind speed is: " + city1.currently.windSpeed + "MPH.");
                        write.WriteLine("The current UV Index is: " + city1.currently.uvIndex + ".");
                        write.WriteLine();
                        
                        write.WriteLine("** Current Weather Data for " + city2.cityName + ", " + location2.state + " **");
                        write.WriteLine("Current Conditions: " + city2.currently.summary);
                        write.WriteLine("The current temperature is: " + System.Math.Round(city2.currently.temperature) + "\x00B0" + "F.");
                        write.WriteLine("The current apparent temperature is: " + System.Math.Round(city2.currently.apparentTemperature) + "\x00B0" + "F.");
                        write.WriteLine("The current humidity is: " + city2.currently.humidity * 100 + "%.");
                        write.WriteLine("The current wind speed is: " + city2.currently.windSpeed + "MPH.");
                        write.WriteLine("The current UV Index is: " + city2.currently.uvIndex + ".");
                        write.WriteLine();

                        write.WriteLine("** Comparison Data for " + city1.cityName + ", " + location1.state + " and " + city2.cityName + ", " + location2.state + " **");

                        //Temperature Comparison
                        while (true)
                        {
                            if (city1.currently.temperature > city2.currently.temperature)
                            {
                                write.Write("It is currently " + System.Math.Round(city1.currently.temperature - city2.currently.temperature) + "\x00B0" + "F warmer in " + city1.cityName + " than " + city2.cityName);
                            }
                            else if (city1.currently.temperature < city2.currently.temperature)
                            {
                                write.Write("It is currently " + System.Math.Round(city2.currently.temperature - city1.currently.temperature) + "\x00B0" + "F warmer in " + city2.cityName + " than " + city1.cityName);
                            }
                            else
                            {
                                write.Write("It is currently the same temperature in " + city1.cityName + " and " + city2.cityName + ".");
                            }
                            write.WriteLine();
                            break;
                        }

                        //Apparent Temperature Comparison
                        while (true)
                        {
                            if (city1.currently.apparentTemperature > city2.currently.apparentTemperature)
                            {
                                write.Write("It currently feels " + System.Math.Round(city1.currently.apparentTemperature - city2.currently.apparentTemperature) + "\x00B0" + "F warmer in " + city1.cityName + " than " + city2.cityName + ".");
                            }
                            else if (city1.currently.apparentTemperature < city2.currently.apparentTemperature)
                            {
                                write.Write("It currently feels " + System.Math.Round(city2.currently.apparentTemperature - city1.currently.apparentTemperature) + "\x00B0" + "F warmer in " + city2.cityName + " than " + city1.cityName + ".");
                            }
                            else
                            {
                                write.Write("It currently feels the same temperature in " + city1.cityName + " and " + city2.cityName + ".");
                            }
                            write.WriteLine();
                            break;
                        }

                        //Humidity Comparison
                        while (true)
                        {
                            if (city1.currently.humidity > city2.currently.humidity)
                            {
                                write.Write("The humidity is currently " + (city1.currently.humidity * 100 - city2.currently.humidity * 100) + "% higher in " + city1.cityName + " than in " + city2.cityName + ".");
                            }
                            else if (city1.currently.humidity < city2.currently.humidity)
                            {
                                write.Write("The humidity is currently " + (city2.currently.humidity * 100 - city1.currently.humidity * 100) + "% higher in " + city2.cityName + " than in " + city1.cityName + ".");
                            }
                            else
                            {
                                write.Write("The humidity is currently the same in " + city1.cityName + " and " + city2.cityName + ".");
                            }
                            write.WriteLine();
                            break;
                        }

                        //Wind Speed Comparison
                        while (true)
                        {
                            if (city1.currently.windSpeed > city2.currently.windSpeed)
                            {
                                write.Write("The wind is currently blowing " + System.Math.Round((city1.currently.windSpeed - city2.currently.windSpeed), 2) + "MPH faster in " + city1.cityName + " than in " + city2.cityName + ".");
                            }
                            else if (city1.currently.windSpeed < city2.currently.windSpeed)
                            {
                                write.Write("The wind is currently blowing " + System.Math.Round((city2.currently.windSpeed - city1.currently.windSpeed), 2) + "MPH faster in " + city2.cityName + " than in " + city1.cityName + ".");
                            }
                            else
                            {
                                write.Write("The wind speed is currently the same in " + city1.cityName + " and " + city2.cityName + ".");
                            }
                            write.WriteLine();
                            break;
                        }

                        //UV Index Comparison
                        while (true)
                        {
                            if (city1.currently.uvIndex > city2.currently.uvIndex)
                            {
                                write.Write("The UV Index is currently " + (city1.currently.uvIndex - city2.currently.uvIndex) + " point(s) higher in " + city1.cityName + " than " + city2.cityName + ".");
                            }
                            else if (city1.currently.uvIndex < city2.currently.uvIndex)
                            {
                                write.Write("The UV Index is currently " + (city2.currently.uvIndex - city1.currently.uvIndex) + " point(s) higher in " + city2.cityName + " than " + city1.cityName + ".");
                            }
                            else
                            {
                                write.Write("The UV Index is currently the same in " + city1.cityName + " and " + city2.cityName + ".");
                            }
                            write.WriteLine();
                            break;
                        }

                        savePath = ((FileStream)(write.BaseStream)).Name;  //Saves File Path as a String for opening file through Console
                        write.Close();
                        Console.Clear();
                    }

                    //File Output
                    while (true)
                    {
                        Console.WriteLine("Your file has been generated.  Would you like to view it now?");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("1 - View File Contents");
                        Console.WriteLine("2 - Return to Main Menu");
                        Console.WriteLine("q - Quit");
                        userSelection = Console.ReadLine();
                        if (userSelection == "1")
                        {
                            using (var reader = new StreamReader(savePath))

                            {
                                Console.Clear();
                                while (!reader.EndOfStream)
                                {
                                    Console.WriteLine(reader.ReadLine());
                                }
                                reader.Close();
                                userSelection = "2";
                                Console.WriteLine();
                                Console.WriteLine("Press any key to return to the Main Menu.");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                        }
                        else if (userSelection == "2")
                        {
                            Console.Clear();
                            break;
                        }
                        else if (userSelection == "q")
                        {
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            continue;
                        }
                    }
                }

                //End Program
                if (userSelection == "q")
                {
                    break;
                }
            }
        }

        public static void CenterTitle(string s)
        {
            string windowHeader = s;
            Console.SetCursorPosition((Console.WindowWidth - windowHeader.Length) / 2, Console.CursorTop);
            Console.WriteLine(windowHeader);
        }
    }
}