using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherCall
{
    class WeatherResponse
    {
        public Coord Coord { get; set; }
        public Weather[] Weather { get; set; }
        public Main Main { get; set; }
        public string Name { get; set; }
        public int Visibility { get; set; }
        public Wind Wind { get; set; }
    }
}
