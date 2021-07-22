using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherCall
{
    class Sys
    {
        public int Type { get; set; }
        public int Id { get; set; }
        public string Country { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
    }
}
