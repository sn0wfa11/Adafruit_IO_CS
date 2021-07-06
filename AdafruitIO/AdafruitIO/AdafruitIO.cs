using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdafruitIO
{
    public class AdafruitIOHTTP
    {
        private string _baseURL = "https://io.adafruit.com/";
        private string _apiVersion = "v2";
        public string aioUsername { get; set; }
        public string aioKey { get; set; }

        public class Data
        {
            [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
            public string value { get; set; } = "";

            // Sendable Metadata Fields
            public string lat { get; set; } = default(string);
            public string lon { get; set; } = default(string);
            public string ele { get; set; } = default(string);
            public DateTime created_at { get; set; } = default(DateTime);

            // Other metadata fields
            public string id { get; set; } = default(string);
            public decimal created_epoch { get; set; } = default(decimal);
            public DateTime updated_at { get; set; } = default(DateTime);
            public string completed_at { get; set; } = default(string);
            public long feed_id { get; set; } = default(long);
            public string expiration { get; set; } = default(string);
            public string position { get; set; } = default(string);

            /// <summary>
            /// Empty Data Constructor
            /// </summary>
            public Data()
            { }

            /// <summary>
            /// Data Constructor with Value Only
            /// </summary>
            /// <param name="value">Adafruit IO Feed Value</param>
            public Data(string value)
            {
                this.value = value;
            }

            /// <summary>
            /// Data Constructor with Metadata
            /// </summary>
            /// <param name="feed">Feed Key for Adafruit IO</param>
            /// <param name="value">Value to be sent.</param>
            /// <param name="lat">Metadata Latitude (Optional)</param>
            /// <param name="lon">Metadata Longitude (Optional)</param>
            /// <param name="ele">Metadata Elevation (Optional)</param>
            /// <param name="created_at">Metadata Created At DateTime (Optional)</param>
            public Data(string value, string lat = default(string), string lon = default(string), string ele = default(string), DateTime createdAt = default(DateTime))
            {
                this.value = value;
                this.lat = lat;
                this.lon = lon;
                this.ele = ele;
                this.created_at = createdAt;
            }

            /// <summary>
            /// Data Constructor with Metadata
            /// </summary>
            /// <param name="feed">Feed Key for Adafruit IO</param>
            /// <param name="value">Value to be sent.</param>
            /// <param name="lat">Metadata Latitude (Optional)</param>
            /// <param name="lon">Metadata Longitude (Optional)</param>
            /// <param name="ele">Metadata Elevation (Optional)</param>
            /// <param name="created_at">Metadata Created At DateTime (Optional)</param>
            public Data(string value, decimal lat = default(decimal), decimal lon = default(decimal), decimal ele = default(decimal), DateTime createdAt = default(DateTime))
            {
                this.value = value;
                if (lat != default(decimal))
                    this.lat = lat.ToString();
                else
                    this.lat = null;

                if (lon != default(decimal))
                    this.lon = lon.ToString();
                else
                    this.lon = null;

                if (ele != default(decimal))
                    this.ele = ele.ToString();
                else
                    this.ele = null;

                this.created_at = createdAt;
            }

            /// <summary>
            /// Data Constructor with Metadata
            /// </summary>
            /// <param name="feed">Feed Key for Adafruit IO</param>
            /// <param name="value">Value to be sent.</param>
            /// <param name="lat">Metadata Latitude (Optional)</param>
            /// <param name="lon">Metadata Longitude (Optional)</param>
            /// <param name="ele">Metadata Elevation (Optional)</param>
            /// <param name="created_at">Metadata Created At DateTime (Optional)</param>
            public Data(string value, double lat = default(double), double lon = default(double), double ele = default(double), DateTime createdAt = default(DateTime))
            {
                this.value = value;
                if (lat != default(double))
                    this.lat = ((decimal)lat).ToString();
                else
                    this.lat = null;

                if (lon != default(double))
                    this.lon = ((decimal)lon).ToString();
                else
                    this.lon = null;

                if (ele != default(double))
                    this.ele = ((decimal)ele).ToString();
                else
                    this.ele = null;

                this.created_at = createdAt;
            }
        }

        public class Feed
        {
            public string name { get; set; } = "";
            public string key { get; set; } = "";
            public string description { get; set; } = "";
            public string unit_type { get; set; } = "";
            public string unit_symbol { get; set; } = "";
            public string history { get; set; } = "";
            public string visibility { get; set; } = "";
            public string license { get; set; } = "";
            public string status_notify { get; set; } = "";
            public string status_timeout { get; set; } = "";
        }

        public class Group
        {
            public string description { get; set; } = "";
            public string source_keys { get; set; } = "";
            public string id { get; set; } = "";
            public string source { get; set; } = "";
            public string key { get; set; } = "";
            public string feeds { get; set; } = "";
            public string properties { get; set; } = "";
            public string name { get; set; } = "";
        }

        public class Time
        {
            public int year { get; set; } = default(int);
            public int mon { get; set; } = default(int);
            public int mday { get; set; } = default(int);
            public int hour { get; set; } = default(int);
            public int min { get; set; } = default(int);
            public int sec { get; set; } = default(int);
            public int wday { get; set; } = default(int);
            public int yday { get; set; } = default(int);
            public int isdst { get; set; } = default(int);
        }

        public class Random
        {
            public int id { get; set; } = default(int);
            public string value_type { get; set; } = default(string);
            public object value { get; set; } = default(object);
            public string seed { get; set; } = default(string);
            public long time_slice { get; set; } = default(long);
        }

        /// <summary>
        /// Empty AdafruitIOHTTP Constructor
        /// </summary>
        public AdafruitIOHTTP()
        { }

        /// <summary>
        /// AdafruitIOHTTP Constuctor with Username and Key
        /// </summary>
        /// <param name="aioUsername">Adafruit IO Username</param>
        /// <param name="aioKey">Adafruit IO API Key</param>
        public AdafruitIOHTTP(string aioUsername, string aioKey)
        {
            this.aioUsername = aioUsername;
            this.aioKey = aioKey;
        }

        /// <summary>
        /// Get most recent data from specified feed.
        /// </summary>
        /// <param name="feed">Adafruit IO Feed Key</param>
        /// <returns>AdafruitIOHTTP.Data Object</returns>
        public Data Receive(string feed)
        {
            string path = "feeds/" + feed + "/data/last";
            return _DeserializeData(_GetRequest(path));
        }

        /// <summary>
        /// Gets the next unread data entry from specified feed.
        /// </summary>
        /// <param name="feed">Adafruit IO Feed Key</param>
        /// <returns>AdafruitIOHTTP.Data Object</returns>
        public Data ReceiveNext(string feed)
        {
            string path = "feeds/" + feed + "/data/next";
            return _DeserializeData(_GetRequest(path));
        }

        /// <summary>
        /// Gets the previous data entry from specified feed.
        /// </summary>
        /// <param name="feed">Adafruit IO Feed Key</param>
        /// <returns>AdafruitIOHTTP.Data Object</returns>
        public Data ReceivePrevious(string feed)
        {
            string path = "feeds/" + feed + "/data/previous";
            return _DeserializeData(_GetRequest(path));
        }

        /// <summary>
        /// Gets feed data from Adafruit IO and returns raw JSON string.
        /// If dataID is null then it returns all data for a feed.
        /// </summary>
        /// <param name="feed">Adafruit IO Feed Key</param>
        /// <param name="dataID">ID of data entry, null for all feed data</param>
        /// <returns>Raw JSON String</returns>
        public string ReceiveDataRaw(string feed, string dataID = null)
        {
            string path = "feeds/" + feed + "/data";
            if (dataID != null)
                path += "/" + dataID;
            return _GetRequest(path);
        }

        /// <summary>
        /// Gets feed data from Adafruit IO and returns an array of Data objects.
        /// If dataID is null then it returns all data for a feed.
        /// </summary>
        /// <param name="feed">Adafruit IO Feed Key</param>
        /// <param name="dataID">ID of data entry, null for all feed data</param>
        /// <returns>An array of AdafruitIOHTTP.Data Objects</returns>
        public Data[] ReceiveDataArray(string feed, string dataID = null)
        {
            string path = "feeds/" + feed + "/data";
            if (dataID != null)
                path += "/" + dataID;
            return _DeserializeDataArray(_GetRequest(path));
        }

        /// <summary>
        /// Gets feed data from Adafruit IO and returns a list of Data objects.
        /// If dataID is null then it returns all data for a feed.
        /// </summary>
        /// <param name="feed">Adafruit IO Feed Key</param>
        /// <param name="dataID">ID of data entry, null for all feed data</param>
        /// <returns>A list of AdafruitIOHTTP.Data Objects</returns>
        public List<Data> ReceiveDataList(string feed, string dataID = null)
        {
            string path = "feeds/" + feed + "/data";
            if (dataID != null)
                path += "/" + dataID;
            return _DeserializeDataList(_GetRequest(path));
        }

        /// <summary>
        /// Gets time from Adafruit IO based on IP location
        /// </summary>
        /// <returns>Raw JSON time string</returns>
        public string ReceiveTimeRaw()
        {
            string path = "integrations/time/struct.json";
            return _GetRequest(path);
        }

        /// <summary>
        /// Gets time from Adafruit IO based on IP location as an AdafruitIOHTTP Time object.
        /// </summary>
        /// <returns>AdafruitIOHTTP Time object</returns>
        public Time ReceiveTime()
        {
            string path = "integrations/time/struct.json";
            return _DeserializeTime(_GetRequest(path));
        }

        /// <summary>
        /// Gets time from Adafruit IO based on IP location
        /// </summary>
        /// <returns>Standard DateTime object</returns>
        public DateTime RecieveDateTime()
        {
            Time time = ReceiveTime();
            return (new DateTime(time.year, time.mon, time.mday, time.hour, time.min, time.sec));
        }

        /// <summary>
        /// Gets weather from Adafruit IO
        /// </summary>
        /// <param name="weatherID">Weather ID for Adafruit IO</param>
        /// <returns>JSON string of weather</returns>
        public string ReceiveWeather(int weatherID = 0)
        {
            string path = "integrations/weather";
            if (weatherID != 0)
                path += "/" + weatherID.ToString();
            return _GetRequest(path);
        }

        /// <summary>
        /// Gets a raw Randomizer json string from Adafruit IO
        /// </summary>
        /// <returns>Raw JSON time string</returns>
        public string ReceiveRandomRaw(int randomizerID = 0)
        {
            string path = "integrations/words";
            if (randomizerID != 0)
                path += "/" + randomizerID.ToString();
            return _GetRequest(path);
        }

        /// <summary>
        /// Get a Randomizer value from Adafruit IO as an AdafruitIOHTTP.Random object.
        /// </summary>
        /// <param name="randomizerID"></param>
        /// <returns>AdafruitIOHTTP.Random object</returns>
        public Random ReceiveRandom(int randomizerID = 0)
        {
            string path = "integrations/words";
            if (randomizerID != 0)
                path += "/" + randomizerID.ToString();
            return _DeserializeRandom(_GetRequest(path));
        }

        /// <summary>
        /// Sends an AdafruitIOHTTP.Data Object to Adafruit IO
        /// </summary>
        /// <param name="feed">Feed Key for Adafruit IO</param>
        /// <param name="data">AdafruitIOHTTP.Data Object to be Sent</param>
        /// <returns>Web Request Return</returns>
        public string SendData(string feed, Data data)
        {
            string path = "feeds/" + feed + "/data";
            return _PostRequest(path, _SerializeData(data));
        }

        /// <summary>
        /// Sends a Data Value to Adafruit IO
        /// </summary>
        /// <param name="feed">Feed Key for Adafruit IO</param>
        /// <param name="value">Value to be sent.</param>
        /// <returns>Web Request Return</returns>
        public string SendData(string feed, string value)
        {
            return SendData(feed, new Data(value));
        }

        /// <summary>
        /// Sends a Data Value to Adafruit IO with Optional Metadata
        /// </summary>
        /// <param name="feed">Feed Key for Adafruit IO</param>
        /// <param name="value">Value to be sent.</param>
        /// <param name="lat">Metadata Latitude (Optional)</param>
        /// <param name="lon">Metadata Longitude (Optional)</param>
        /// <param name="ele">Metadata Elevation (Optional)</param>
        /// <param name="created_at">Metadata Created At DateTime (Optional)</param>
        /// <returns>Web Request Return</returns>
        public string SendData(string feed, string value, string lat = default(string), string lon = default(string), string ele = default(string), DateTime created_at = default(DateTime))
        {
            return SendData(feed, new Data(value, lat, lon, ele, created_at));
        }

        /// <summary>
        /// Sends a Data Value to Adafruit IO with Optional Metadata
        /// </summary>
        /// <param name="feed">Feed Key for Adafruit IO</param>
        /// <param name="value">Value to be sent.</param>
        /// <param name="lat">Metadata Latitude (Optional)</param>
        /// <param name="lon">Metadata Longitude (Optional)</param>
        /// <param name="ele">Metadata Elevation (Optional)</param>
        /// <param name="created_at">Metadata Created At DateTime (Optional)</param>
        /// <returns>Web Request Return</returns>
        public string SendData(string feed, string value, decimal lat = default(decimal), decimal lon = default(decimal), decimal ele = default(decimal), DateTime created_at = default(DateTime))
        {
            return SendData(feed, new Data(value, lat.ToString(), lon.ToString(), ele.ToString(), created_at));
        }

        /// <summary>
        /// Sends a Data Value to Adafruit IO with Optional Metadata
        /// </summary>
        /// <param name="feed">Feed Key for Adafruit IO</param>
        /// <param name="value">Value to be sent.</param>
        /// <param name="lat">Metadata Latitude (Optional)</param>
        /// <param name="lon">Metadata Longitude (Optional)</param>
        /// <param name="ele">Metadata Elevation (Optional)</param>
        /// <param name="created_at">Metadata Created At DateTime (Optional)</param>
        /// <returns>Web Request Return</returns>
        public string SendData(string feed, string value, double lat = default(double), double lon = default(double), double ele = default(double), DateTime created_at = default(DateTime))
        {
            return SendData(feed, new Data(value, lat.ToString(), lon.ToString(), ele.ToString(), created_at));
        }

        /// <summary>
        /// Sends an AdafruitIOHTTP.Data Object to Adafruit IO with Rounding of lat, lon, and ele
        /// </summary>
        /// <param name="feed">Feed Key for Adafruit IO</param>
        /// <param name="data">AdafruitIOHTTP.Data Object to be Sent</param>
        /// <param name="precision">Decimal precision</param>
        /// <returns>Web Request Return</returns>
        public string SendData(string feed, Data data, short precision)
        {
            Data rounded = data;
            rounded.lat = Round(rounded.lat, precision);
            rounded.lon = Round(rounded.lon, precision);
            rounded.ele = Round(rounded.ele, precision);
            return SendData(feed, rounded);
        }

        /// <summary>
        /// Deletes data from an Adafruit IO Feed
        /// </summary>
        /// <param name="feed">Feed Key for Adafruit IO</param>
        /// <param name="dataID">Data ID of data to be deleted</param>
        /// <returns></returns>
        public string DeleteData(string feed, string dataID)
        {
            string path = "feeds/" + feed + "/data/" + dataID;
            return _DeleteRequest(path);
        }

        /// <summary>
        /// Composes the required url for an API request
        /// </summary>
        /// <param name="path">Path suffix for a given funcion</param>
        /// <returns>Full Adafruit IO Request Path</returns>
        private string _ComposeURL(string path)
        {
            return _baseURL + "api/" + _apiVersion + "/" + aioUsername + "/" + path;
        }

        /// <summary>
        /// Rounds a number string by coverting it to a decimal, rounding, then returning as a string
        /// </summary>
        /// <param name="number">A Number in String Form</param>
        /// <param name="precision">Number of Decimal Places</param>
        /// <returns>Rounded Number as a String</returns>
        public static string Round(string number, short precision)
        {
            decimal numDec = 0;
            if (number == null)
                return null;
            else
                if (decimal.TryParse(number, out numDec))
                return (Decimal.Round(numDec, precision)).ToString();
            else
                return null;
        }

        /// <summary>
        /// Generates an Adafruit IO Get Request
        /// </summary>
        /// <param name="path">Full Adafruit IO Request Path</param>
        /// <returns>json String Adafruit IO Response</returns>
        private string _GetRequest(string path)
        {
            WebRequest request = WebRequest.Create(_ComposeURL(path));
            request.Method = "GET";
            request.Headers["X-AIO-Key"] = aioKey;

            try
            {
                HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(webStream);
                string response = reader.ReadToEnd();
                return response;
            }
            catch (WebException ex)
            {
                _HandleError((HttpWebResponse)ex.Response);
                return null;
            }
        }

        /// <summary>
        /// Generates an Adafruit IO Delete Request
        /// </summary>
        /// <param name="path">Full Adafruit IO Request Path</param>
        /// <returns>json String Adafruit IO Response</returns>
        private string _DeleteRequest(string path)
        {
            WebRequest request = WebRequest.Create(_ComposeURL(path));
            request.Method = "DELETE";
            request.Headers["X-AIO-Key"] = aioKey;

            try
            {
                HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(webStream);
                string response = reader.ReadToEnd();
                return response;
            }
            catch (WebException ex)
            {
                _HandleError((HttpWebResponse)ex.Response);
                return null;
            }
        }

        /// <summary>
        /// Generates an Adafruit IO Post Request
        /// </summary>
        /// <param name="path">Full Adafruit IO Request Path</param>
        /// <param name="payload">json Data to be Sent</param>
        /// <returns>json String Adafruit IO Response</returns>
        private string _PostRequest(string path, string payload)
        {
            WebRequest request = WebRequest.Create(_ComposeURL(path));
            request.Method = "POST";
            request.Headers["X-AIO-Key"] = aioKey;
            byte[] byteArray = Encoding.UTF8.GetBytes(payload);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            try
            {
                HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(webStream);
                string response = reader.ReadToEnd();
                webResponse.Close();
                return response;
            }
            catch (WebException ex)
            {
                _HandleError((HttpWebResponse)ex.Response);
                return null;
            }
        }

        private void _HandleError(HttpWebResponse webResponse, string response = null)
        {
            if (webResponse.StatusCode == (HttpStatusCode)429)
                throw new ThrottlingError("Exceeded the limit of Adafruit IO requests in a short period of time. Please reduce the rate of requests and try again later.");
            else if (webResponse.StatusCode >= HttpStatusCode.BadRequest)
                throw new RequestError("Adafruit IO request failed: " + webResponse.StatusCode.ToString() + " " + webResponse.StatusDescription + " " + response);
        }

        /// <summary>
        /// Serializes a Data object to JSON
        /// </summary>
        /// <param name="data">AdafruitIOHTTP.Data object to be seralized</param>
        /// <returns>Serialized JSON string</returns>
        private static string _SerializeData(Data data)
        {
            return JsonSerializer.Serialize(data, _serializerOptions);
        }

        /// <summary>
        /// Deserializes a json String to a Data Object
        /// </summary>
        /// <param name="jsonString">String to Deserialize</param>
        /// <returns>AdafruitIOHTTP.Data Object</returns>
        private static Data _DeserializeData(string jsonString)
        {
            return JsonSerializer.Deserialize<Data>(jsonString);
        }

        /// <summary>
        /// Deserializes a json string to a Data object array
        /// </summary>
        /// <param name="jsonString">String to Deserialize</param>
        /// <returns>Array of AdafruitIOHTTP.Data Objects</returns>
        private static Data[] _DeserializeDataArray(string jsonString)
        {
            try
            {
                return JsonSerializer.Deserialize<Data[]>(jsonString);
            }
            catch (JsonException)
            {
                Data[] result = new Data[1];
                result[0] = JsonSerializer.Deserialize<Data>(jsonString);
                return result;
            }
        }

        /// <summary>
        /// Deserializes a json string to a Data object List
        /// </summary>
        /// <param name="jsonString">String to Deserialize</param>
        /// <returns>List of AdafruitIOHTTP.Data objects</returns>
        private static List<Data> _DeserializeDataList(string jsonString)
        {
            try
            {
                return JsonSerializer.Deserialize<List<Data>>(jsonString);
            }
            catch (JsonException)
            {
                List<Data> result = new List<Data>();
                result.Add(JsonSerializer.Deserialize<Data>(jsonString));
                return result;
            }
        }

        /// <summary>
        /// Deserializes a json String to a Time object
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns>AdafruitIOHTTP.Time object</returns>
        private static Time _DeserializeTime(string jsonString)
        {
            return JsonSerializer.Deserialize<Time>(jsonString);
        }

        /// <summary>
        /// Deserializes a json String to a Random Object
        /// </summary>
        /// <param name="jsonString">String to Deserialize</param>
        /// <returns>AdafruitIOHTTP.Random Object</returns>
        private static Random _DeserializeRandom(string jsonString)
        {
            return JsonSerializer.Deserialize<Random>(jsonString);
        }

        private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        [Serializable]
        public class RequestError : Exception
        {
            public RequestError() : base() { }
            public RequestError(string message) : base(message) { }
            public RequestError(string message, Exception inner) : base(message, inner) { }

            protected RequestError(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }

        [Serializable]
        public class ThrottlingError : Exception
        {
            public ThrottlingError() : base() { }
            public ThrottlingError(string message) : base(message) { }
            public ThrottlingError(string message, Exception inner) : base(message, inner) { }

            protected ThrottlingError(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}
