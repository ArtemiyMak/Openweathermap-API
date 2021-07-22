using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;


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
    }
}
