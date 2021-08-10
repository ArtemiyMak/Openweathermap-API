using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherCall
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherWorker weatherWorker = new WeatherWorker();
            City[] citiesNames;
            WebResponse response;
            using (StreamReader sr = new StreamReader(weatherWorker.RequestCities()))
            {
                string cityes = sr.ReadToEnd();
                citiesNames = JsonConvert.DeserializeObject<City[]>(cityes);
            }
            weatherWorker.RequestCities();
            bool findCity = false;
            string cityName = "";
            while (!findCity)
            {
                Console.WriteLine("Введите название города на английском: ");
                cityName = Console.ReadLine();
                foreach (City city in citiesNames)
                {
                    if (city.Name == cityName)
                    {
                        findCity = true;
                        break;
                    }
                }
                if(!findCity)
                {
                    Console.WriteLine("Город не найден.");
                }
            }
            if (findCity)
            {
                WebRequest request = WebRequest.Create($"http://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&lang=ru&appid=9c838ac25d8b91b6d60a5a823be85d70");
                response = request.GetResponse();
            }
            else
            {
                Console.WriteLine($"Город не обнаружен.");
                WebRequest request = WebRequest.Create($"http://api.openweathermap.org/data/2.5/weather?q=London&units=metric&lang=ru&appid=9c838ac25d8b91b6d60a5a823be85d70");
                response = request.GetResponse();
            }
            string weatherStr;
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                weatherStr = sr.ReadToEnd();
            }
            weatherWorker.WriteInfo(weatherStr);
        }
    }
}
