using System;
using System.IO;
using System.Net;

//https://github.com/adafruit/Adafruit_IO_Python/blob/master/Adafruit_IO/client.py

public class Adafruit_IO_CS
{
	public class IO_HTTP
    {
        private string base_url = "https://io.adafruit.com/api/v2/";
        private string api_version = "v2";
        public string aio_username { get; set; }
        public string aio_key { get; set; }

        public IO_HTTP()
        { }

        public IO_HTTP(string aio_username, string aio_key)
        {
            this.aio_username = aio_username;
            this.aio_key = aio_key;
        }

        public string get_feed(string feed_name)
        {

        }

        private string compose_url(string path)
        {
            return base_url + "/api/" + api_version + "/" + aio_username + "/" + path;
        }

        private string GET_request(string path)
        {
            WebRequest request = WebRequest.Create(compose_url(path));
            request.Method = "GET";
            request.Headers["X-AIO-Key"] = aio_key;
            WebResponse webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(webStream);
            return reader.ReadToEnd();
        }
    }
}
