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
            Console.WriteLine("Введите название города: ");
            string cityName = Console.ReadLine();
            bool findCity = false;
            foreach(City city in citiesNames)
            {
                if(city.Name == cityName)
                {
                    findCity = true;
                    break;
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
            //Newtonsoft
            
            WeatherResponse weather = JsonConvert.DeserializeObject<WeatherResponse>(weatherStr);
            Console.ReadLine();
            Console.WriteLine($"Город: {weather.Name}");
            Console.WriteLine($"Координаты: Широта {weather.Coord.Lon}, долгота {weather.Coord.Lat}");
            Console.WriteLine($"Погода: {weather.Weather[0].Main}. Описание: {weather.Weather[0].Description}");
            Console.WriteLine($"Температура: {weather.Main.Temp}. Чуствуется как {weather.Main.Feels_like}. Давление {weather.Main.Pressure}");
            Console.WriteLine($"Видимость {weather.Visibility}");
            Console.WriteLine($"Скорость ветра {weather.Wind.Speed} м/с. Направление {weather.Wind.Deg} градусов");
            //отдельный класс для работы погоды
            //относительный путь для сохранения файла с именами городов
            //dot net gz stream | gzstream
            //закрепы
            //https://metanit.com/sharp/net/2.2.php
            //https://openweathermap.org/current    
            //https://home.openweathermap.org/api_keys
        }
    }
}
