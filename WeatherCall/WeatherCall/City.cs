using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherCall
{
    class City
    {
        public float Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public CoordCity Coords { get; set; }
    }
}
