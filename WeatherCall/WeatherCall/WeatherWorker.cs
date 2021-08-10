using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;


namespace WeatherCall
{
    class WeatherWorker
    {
        public WeatherResponse wr;
        public FileStream RequestCities()
        {
            if(!new FileInfo("city.list.json").Exists)
            {
                WebRequest request = WebRequest.Create($"http://bulk.openweathermap.org/sample/city.list.json.gz");
                WebResponse response = request.GetResponse();
                using (GZipStream gz = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                {
                    gz.CopyTo(new FileStream("city.list.json", FileMode.Create));
                    return new FileStream("city.list.json", FileMode.Create);

                }
            }
            else
            {
                FileStream file = new FileStream("city.list.json", FileMode.Open);
                return file;
            }
        }
        public City[] ReadCityList()
        {
            using (StreamReader sr = new StreamReader(RequestCities()))
            {
                string cityes = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<City[]>(cityes);
            }
        }
        public void WriteInfo(string weatherStr)
        {
            WeatherResponse weather = JsonConvert.DeserializeObject<WeatherResponse>(weatherStr);
            Console.ReadLine();
            Console.WriteLine($"Город: {weather.Name}");
            Console.WriteLine($"Координаты: Широта {weather.Coord.Lon}, долгота {weather.Coord.Lat}");
            Console.WriteLine($"Погода: {weather.Weather[0].Main}. Описание: {weather.Weather[0].Description}");
            Console.WriteLine($"Температура: {weather.Main.Temp}. Чуствуется как {weather.Main.Feels_like}. Давление {weather.Main.Pressure}");
            Console.WriteLine($"Видимость {weather.Visibility}");
            Console.WriteLine($"Скорость ветра {weather.Wind.Speed} м/с. Направление {weather.Wind.Deg} градусов");
        }
    }
}
