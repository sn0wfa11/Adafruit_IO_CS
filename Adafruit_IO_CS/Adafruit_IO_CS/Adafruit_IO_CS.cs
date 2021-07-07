using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Adafruit_IO_CS
{
    public class AdafruitIO
    {
        /// <summary>
        /// An Object containing Adafruit IO Data information.
        /// </summary>
        public class Data
        {
            /// <summary>
            /// Adafruit IO Data Value
            /// </summary>
            [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
            public string value { get; set; } = "";

            // Sendable Metadata Fields

            /// <summary>
            /// Adafruit IO Data Latitude
            /// </summary>
            public string lat { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Data Longitude
            /// </summary>
            public string lon { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Data Elevation
            /// </summary>
            public string ele { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Data Created At
            /// </summary>
            public DateTime created_at { get; set; } = default(DateTime);

            // Other metadata fields

            /// <summary>
            /// Adafruit IO Data ID
            /// </summary>
            public string id { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Data Created Epoch
            /// </summary>
            public decimal created_epoch { get; set; } = default(decimal);
            /// <summary>
            /// Adafruit IO Data Updated At
            /// </summary>
            public DateTime updated_at { get; set; } = default(DateTime);
            /// <summary>
            /// Adafruit IO Data Completed At
            /// </summary>
            public string completed_at { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Data Feed ID
            /// </summary>
            public long feed_id { get; set; } = default(long);
            /// <summary>
            /// Adafruit IO Data Expiration
            /// </summary>
            public string expiration { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Data Position
            /// </summary>
            public string position { get; set; } = default(string);

            // Constructors

            /// <summary>
            /// Empty Data Constructor
            /// </summary>
            public Data() { }

            /// <summary>
            /// Data Constructor with Value Only
            /// </summary>
            /// <param name="value">Adafruit IO Feed Value</param>
            public Data(string value) { this.value = value; }

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

            // Methods

            /// <summary>
            /// Serialize the Data object to JSON
            /// </summary>
            /// <returns>JSON String</returns>
            public string Serialize()
            {
                return JsonSerializer.Serialize(this, _serializerOptions);
            }

            /// <summary>
            /// Return the Value field for the Data object
            /// </summary>
            /// <returns>Data Value String</returns>
            public override string ToString()
            {
                return value;
            }

            // Static Methods

            /// <summary>
            /// Deserializes a json String to a Data Object
            /// </summary>
            /// <param name="jsonString">String to Deserialize</param>
            /// <returns>AdafruitIOHTTP.Data Object</returns>
            public static Data Deserialize(string jsonString)
            {
                return JsonSerializer.Deserialize<Data>(jsonString);
            }

            /// <summary>
            /// Deserializes a json string to a Data object array
            /// </summary>
            /// <param name="jsonString">String to Deserialize</param>
            /// <returns>Array of AdafruitIOHTTP.Data Objects</returns>
            public static Data[] DeserializeArray(string jsonString)
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
            public static List<Data> DeserializeList(string jsonString)
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

            private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };
        }

        /// <summary>
        /// An object containing Adafruit IO Feed information.
        /// </summary>
        public class Feed
        {
            // Fields

            /// <summary>
            /// Adafruit IO Feed Name
            /// </summary>
            public string name { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Feed Description
            /// </summary>
            public string description { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Feed Unit Type
            /// </summary>
            public string unit_type { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Feed Unit Symbol
            /// </summary>
            public string unit_symbol { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Feed History Kept
            /// True for history, false for no history
            /// </summary>
            public bool history { get; set; } = default(bool);
            /// <summary>
            /// Adafruit IO Feed Visibility
            /// </summary>
            public string visibility { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Feed License
            /// </summary>
            public string license { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Feed Status
            /// </summary>
            public string status { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Feed Status Notify
            /// </summary>
            public bool status_notify { get; set; } = default(bool);
            /// <summary>
            /// Adafruit IO Feed Status Timeout
            /// </summary>
            public int status_timeout { get; set; } = default(int);
            /// <summary>
            /// Adafruit IO Feed Key
            /// </summary>
            public string key { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Feed ID
            /// </summary>
            public long id { get; set; } = default(long);
            /// <summary>
            /// Adafruit IO Feed Last Value
            /// Last data value sent to this feed.
            /// </summary>
            public string last_value { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Feed Created At
            /// </summary>
            public DateTime created_at { get; set; } = default(DateTime);
            /// <summary>
            /// Adafruit IO Feed Updated At
            /// </summary>
            public DateTime updated_at { get; set; } = default(DateTime);

            // Constructors

            /// <summary>
            /// Empty Constructor
            /// </summary>
            public Feed() { }

            /// <summary>
            /// Base Constructor with Feed name
            /// </summary>
            /// <param name="name">Name of new feed</param>
            public Feed(string name) { this.name = name; }

            // Methods

            /// <summary>
            /// Serializes the Feed object to a JSON string.
            /// </summary>
            /// <returns>JSON String</returns>
            public string Serialize()
            {
                return JsonSerializer.Serialize(this, _serializerOptions);
            }

            /// <summary>
            /// Returns the Feed's key
            /// </summary>
            /// <returns>Feed Key String</returns>
            public override string ToString()
            {
                return key;
            }

            // Static Methods

            /// <summary>
            /// Deserializes a json string to a Feed object.
            /// </summary>
            /// <param name="jsonString">String to Deserialize</param>
            /// <returns>AdafruitIOHTTP.Feed Object</returns>
            public static Feed Deserialize(string jsonString)
            {
                return JsonSerializer.Deserialize<Feed>(jsonString);
            }

            /// <summary>
            /// Deserializes a json string to a Feed object array
            /// </summary>
            /// <param name="jsonString">String to Deserialize</param>
            /// <returns>Array of AdafruitIOHTTP.Feed Objects</returns>
            public static Feed[] DeserializeArray(string jsonString)
            {
                try
                {
                    return JsonSerializer.Deserialize<Feed[]>(jsonString);
                }
                catch (JsonException)
                {
                    Feed[] result = new Feed[1];
                    result[0] = JsonSerializer.Deserialize<Feed>(jsonString);
                    return result;
                }
            }

            /// <summary>
            /// Deserializes a json string to a Feed object List
            /// </summary>
            /// <param name="jsonString">String to Deserialize</param>
            /// <returns>List of AdafruitIOHTTP.Feed objects</returns>
            public static List<Feed> DeserializeList(string jsonString)
            {
                try
                {
                    return JsonSerializer.Deserialize<List<Feed>>(jsonString);
                }
                catch (JsonException)
                {
                    List<Feed> result = new List<Feed>();
                    result.Add(JsonSerializer.Deserialize<Feed>(jsonString));
                    return result;
                }
            }

            private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };
        }

        /// <summary>
        /// An object containing Adafruit IO Group information.
        /// </summary>
        public class Group
        {
            // Fields

            /// <summary>
            /// Adafruit IO Group Description
            /// </summary>
            public string description { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Group Source Keys
            /// </summary>
            public string source_keys { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Group ID
            /// </summary>
            public long id { get; set; } = default(long);
            /// <summary>
            /// Adafruit IO Group Source
            /// </summary>
            public string source { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Group Key
            /// </summary>
            public string key { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Group Feeds
            /// Array of AdafruitIOHTTP Feeds objects
            /// </summary>
            public Feed[] feeds { get; set; }
            /// <summary>
            /// Adafruit IO Group Properties
            /// </summary>
            public string properties { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Group Name
            /// </summary>
            public string name { get; set; } = default(string);

            /// <summary>
            /// Group empty constructor
            /// </summary>
            public Group() { }

            /// <summary>
            /// Group constructor with name value
            /// </summary>
            /// <param name="name">Name of the new Group</param>
            public Group(string name) { this.name = name; }

            // Methods

            /// <summary>
            /// Serializes the Group object to JSON string
            /// </summary>
            /// <returns>JSON String</returns>
            public string Serialize()
            {
                return JsonSerializer.Serialize(this, _serializerOptions);
            }

            /// <summary>
            /// Returns the Group's Key
            /// </summary>
            /// <returns>Group Key String</returns>
            public override string ToString()
            {
                return key;
            }

            // Static Methods

            /// <summary>
            /// Deserializes a json string to a Group object.
            /// </summary>
            /// <param name="jsonString">String to Deserialize</param>
            /// <returns>AdafruitIOHTTP.Group Object</returns>
            public static Group Deserialize(string jsonString)
            {
                return JsonSerializer.Deserialize<Group>(jsonString);
            }

            /// <summary>
            /// Deserializes a json string to a Group object array
            /// </summary>
            /// <param name="jsonString">String to Deserialize</param>
            /// <returns>Array of AdafruitIOHTTP.Group Objects</returns>
            public static Group[] DeserializeArray(string jsonString)
            {
                try
                {
                    return JsonSerializer.Deserialize<Group[]>(jsonString);
                }
                catch (JsonException)
                {
                    Group[] result = new Group[1];
                    result[0] = JsonSerializer.Deserialize<Group>(jsonString);
                    return result;
                }
            }

            /// <summary>
            /// Deserializes a json string to a Group object List
            /// </summary>
            /// <param name="jsonString">String to Deserialize</param>
            /// <returns>List of AdafruitIOHTTP.Group objects</returns>
            public static List<Group> DeserializeList(string jsonString)
            {
                try
                {
                    return JsonSerializer.Deserialize<List<Group>>(jsonString);
                }
                catch (JsonException)
                {
                    List<Group> result = new List<Group>();
                    result.Add(JsonSerializer.Deserialize<Group>(jsonString));
                    return result;
                }
            }

            private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };
        }

        /// <summary>
        /// An object containing Adafruit IO Time information.
        /// </summary>
        public class Time
        {
            // Fields

            /// <summary>
            /// Adafruit IO Time Year
            /// </summary>
            public int year { get; set; } = default(int);
            /// <summary>
            /// Adafruit IO Time Month
            /// </summary>
            public int mon { get; set; } = default(int);
            /// <summary>
            /// Adafruit IO Time Day of Month
            /// </summary>
            public int mday { get; set; } = default(int);
            /// <summary>
            /// Adafruit IO Time Hour
            /// </summary>
            public int hour { get; set; } = default(int);
            /// <summary>
            /// Adafruit IO Time Minute
            /// </summary>
            public int min { get; set; } = default(int);
            /// <summary>
            /// Adafruit IO Time Second
            /// </summary>
            public int sec { get; set; } = default(int);
            /// <summary>
            /// Adafruit IO Time Day of Week
            /// </summary>
            public int wday { get; set; } = default(int);
            /// <summary>
            /// Adafruit IO Time Day of Year
            /// </summary>
            public int yday { get; set; } = default(int);
            /// <summary>
            /// Adafruit IO Time Is Daylight Savings Time
            /// </summary>
            public int isdst { get; set; } = default(int);

            // Static Methods

            /// <summary>
            /// Deserializes a json String to a Time object
            /// </summary>
            /// <param name="jsonString"></param>
            /// <returns>AdafruitIOHTTP.Time object</returns>
            public static Time Deserialize(string jsonString)
            {
                return JsonSerializer.Deserialize<Time>(jsonString);
            }

            private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };
        }

        /// <summary>
        /// An object containing Adafruit IO Randomizer information.
        /// </summary>
        public class Random
        {
            // Fields

            /// <summary>
            /// Adafruit IO Randomizer ID
            /// </summary>
            public int id { get; set; } = default(int);
            /// <summary>
            /// Adafruit IO Randomizer Value Type
            /// </summary>
            public string value_type { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Randomizer Value
            /// </summary>
            public object value { get; set; } = default(object);
            /// <summary>
            /// Adafruit IO Randomizer Seed
            /// </summary>
            public string seed { get; set; } = default(string);
            /// <summary>
            /// Adafruit IO Randomizer Time Slice
            /// </summary>
            public long time_slice { get; set; } = default(long);

            // Static Methods

            /// <summary>
            /// Deserializes a json String to a Random Object
            /// </summary>
            /// <param name="jsonString">String to Deserialize</param>
            /// <returns>AdafruitIOHTTP.Random Object</returns>
            public static Random Deserialize(string jsonString)
            {
                return JsonSerializer.Deserialize<Random>(jsonString);
            }

            private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };
        }
    }

    public class AdafruitIOHTTP
    {
        private string _baseURL = "https://io.adafruit.com/";
        private string _apiVersion = "v2";
        /// <summary>
        /// Adafruit IO Username
        /// </summary>
        public string aioUsername { get; set; }
        /// <summary>
        /// Adafruit IO API Key
        /// </summary>
        public string aioKey { get; set; }

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

        // Data Functions

        /// <summary>
        /// Get most recent data from specified feed.
        /// </summary>
        /// <param name="feed">Adafruit IO Feed Key</param>
        /// <returns>AdafruitIOHTTP.Data Object</returns>
        public AdafruitIO.Data Receive(string feed)
        {
            string path = "feeds/" + feed + "/data/last";
            return AdafruitIO.Data.Deserialize(_GetRequest(path));
        }

        /// <summary>
        /// Gets the next unread data entry from specified feed.
        /// </summary>
        /// <param name="feed">Adafruit IO Feed Key</param>
        /// <returns>AdafruitIOHTTP.Data Object</returns>
        public AdafruitIO.Data ReceiveNext(string feed)
        {
            string path = "feeds/" + feed + "/data/next";
            return AdafruitIO.Data.Deserialize(_GetRequest(path));
        }

        /// <summary>
        /// Gets the previous data entry from specified feed.
        /// </summary>
        /// <param name="feed">Adafruit IO Feed Key</param>
        /// <returns>AdafruitIOHTTP.Data Object</returns>
        public AdafruitIO.Data ReceivePrevious(string feed)
        {
            string path = "feeds/" + feed + "/data/previous";
            return AdafruitIO.Data.Deserialize(_GetRequest(path));
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
        public AdafruitIO.Data[] ReceiveDataArray(string feed, string dataID = null)
        {
            return AdafruitIO.Data.DeserializeArray(ReceiveDataRaw(feed, dataID));
        }

        /// <summary>
        /// Gets feed data from Adafruit IO and returns a list of Data objects.
        /// If dataID is null then it returns all data for a feed.
        /// </summary>
        /// <param name="feed">Adafruit IO Feed Key</param>
        /// <param name="dataID">ID of data entry, null for all feed data</param>
        /// <returns>A list of AdafruitIOHTTP.Data Objects</returns>
        public List<AdafruitIO.Data> ReceiveDataList(string feed, string dataID = null)
        {
            return AdafruitIO.Data.DeserializeList(ReceiveDataRaw(feed, dataID));
        }

        /// <summary>
        /// Gets time from Adafruit IO based on IP location
        /// </summary>
        /// <returns>Standard DateTime object</returns>
        public DateTime RecieveDateTime()
        {
            AdafruitIO.Time time = ReceiveTime();
            return (new DateTime(time.year, time.mon, time.mday, time.hour, time.min, time.sec));
        }

        /// <summary>
        /// Sends an AdafruitIOHTTP.Data Object to Adafruit IO
        /// </summary>
        /// <param name="feed">Feed Key for Adafruit IO</param>
        /// <param name="data">AdafruitIOHTTP.Data Object to be Sent</param>
        /// <returns>Web Request Return</returns>
        public string SendData(string feed, AdafruitIO.Data data)
        {
            string path = "feeds/" + feed + "/data";
            return _PostRequest(path, data.Serialize());
        }

        /// <summary>
        /// Sends a Data Value to Adafruit IO
        /// </summary>
        /// <param name="feed">Feed Key for Adafruit IO</param>
        /// <param name="value">Value to be sent.</param>
        /// <returns>Web Request Return</returns>
        public string SendData(string feed, string value)
        {
            return SendData(feed, new AdafruitIO.Data(value));
        }

        /// <summary>
        /// Helper function to make appending data easy.
        /// Sends a Data Value to Adafruit IO
        /// </summary>
        /// <param name="feed">Feed Key for Adafruit IO</param>
        /// <param name="value">Value to be sent.</param>
        /// <returns>Web Request Return</returns>
        public string Append(string feed, string value)
        {
            return SendData(feed, value);
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
            return SendData(feed, new AdafruitIO.Data(value, lat, lon, ele, created_at));
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
            return SendData(feed, new AdafruitIO.Data(value, lat.ToString(), lon.ToString(), ele.ToString(), created_at));
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
            return SendData(feed, new AdafruitIO.Data(value, lat.ToString(), lon.ToString(), ele.ToString(), created_at));
        }

        /// <summary>
        /// Sends an AdafruitIOHTTP.Data Object to Adafruit IO with Rounding of lat, lon, and ele
        /// </summary>
        /// <param name="feed">Feed Key for Adafruit IO</param>
        /// <param name="data">AdafruitIOHTTP.Data Object to be Sent</param>
        /// <param name="precision">Decimal precision</param>
        /// <returns>Web Request Return</returns>
        public string SendData(string feed, AdafruitIO.Data data, short precision)
        {
            AdafruitIO.Data rounded = data;
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

        // Feeds Functions

        /// <summary>
        /// Retrieve all feeds or specified feed.
        /// If a feed is not listed, all feeds will be returned
        /// </summary>
        /// <param name="feedKey">Feed key, or ID</param>
        /// <returns>JSON feed string</returns>
        public string FeedsRaw(string feedKey = null)
        {
            string path = "feeds/";
            if (feedKey != null)
                path += feedKey;
            return _GetRequest(path);
        }

        /// <summary>
        /// Retrieve all feeds or specified feed as an array of AdafruitIOHTTP.Feed objects.
        /// If a feed is not listed, all feeds will be returned
        /// </summary>
        /// <param name="feedKey">Feed key, or ID</param>
        /// <returns>Array of AdafruitIOHTTP.Feed objects</returns>
        public AdafruitIO.Feed[] FeedsArray(string feedKey = null)
        {
            return AdafruitIO.Feed.DeserializeArray(FeedsRaw(feedKey));
        }

        /// <summary>
        /// Retrieve all feeds or specified feed as a list of AdafruitIOHTTP.Feed objects.
        /// If a feed is not listed, all feeds will be returned
        /// </summary>
        /// <param name="feedKey">Feed key, or ID</param>
        /// <returns>List of AdafruitIOHTTP.Feed objects</returns>
        public List<AdafruitIO.Feed> FeedsList(string feedKey = null)
        {
            return AdafruitIO.Feed.DeserializeList(FeedsRaw(feedKey));
        }

        /// <summary>
        /// Retrieve a single specified feed as an AdafruitIOHTTP.Feed object.
        /// </summary>
        /// <param name="feedKey">Feed key, or ID</param>
        /// <returns>AdafruitIOHTTP.Feed object</returns>
        public AdafruitIO.Feed GetFeed(string feedKey)
        {
            return AdafruitIO.Feed.DeserializeArray(FeedsRaw(feedKey))[0];
        }

        /// <summary>
        /// Creates a specified Adafruit IO Feed
        /// </summary>
        /// <param name="feed">AdafruitIOHTTP.Feed object</param>
        /// <param name="groupKey">Group key or ID</param>
        /// <returns>Web Request Return</returns>
        public string CreateFeed(AdafruitIO.Feed feed, string groupKey = null)
        {
            string path = "feeds/";
            if (groupKey != null)
                path = "/groups/" + groupKey + "/feeds";
            return _PostRequest(path, feed.Serialize());
        }

        /// <summary>
        /// Quickly Creates a new Adafruit IO Feed with just the name.
        /// </summary>
        /// <param name="feedName">Name of new Feed</param>
        /// <param name="groupKey">Optional Group Key for new feed</param>
        /// <returns></returns>
        public string CreateFeed(string feedName, string groupKey = null)
        {
            return CreateFeed(new AdafruitIO.Feed(feedName), groupKey);
        }

        /// <summary>
        /// Deletes a specified feed from Adafruit IO
        /// </summary>
        /// <param name="feedKey">Key or ID of feed to be deleted</param>
        /// <returns>Web request response</returns>
        public string DeleteFeed(string feedKey)
        {
            string path = "feeds/" + feedKey;
            return _DeleteRequest(path);
        }

        /// <summary>
        /// Deletes a specified feed from Adafruit IO with a Feed object as the base.
        /// </summary>
        /// <param name="feed">Adafruit IO Feed object to be deleted</param>
        /// <returns></returns>
        public string DeleteFeed(AdafruitIO.Feed feed)
        {
            return DeleteFeed(feed.key);
        }

        // Groups Functions

        /// <summary>
        /// Retrieve all groups or specified group.
        /// If a group is not listed, all groups will be returned
        /// </summary>
        /// <param name="groupKey">Group key, or ID</param>
        /// <returns>JSON feed string</returns>
        public string GroupsRaw(string groupKey = null)
        {
            string path = "groups/";
            if (groupKey != null)
                path += groupKey;
            return _GetRequest(path);
        }

        /// <summary>
        /// Retrieve all groups or specified group as an array of AdafruitIOHTTP.Group objects.
        /// If a group is not listed, all groups will be returned
        /// </summary>
        /// <param name="groupKey">Group key, or ID</param>
        /// <returns>Array of AdafruitIOHTTP.Group objects</returns>
        public AdafruitIO.Group[] GroupsArray(string groupKey = null)
        {
            return AdafruitIO.Group.DeserializeArray(GroupsRaw(groupKey));
        }

        /// <summary>
        /// Retrieve all groups or specified group as a list of AdafruitIOHTTP.Group objects.
        /// If a group is not listed, all groups will be returned
        /// </summary>
        /// <param name="groupKey">Group key, or ID</param>
        /// <returns>List of AdafruitIOHTTP.Group objects</returns>
        public List<AdafruitIO.Group> GroupsList(string groupKey = null)
        {
            return AdafruitIO.Group.DeserializeList(GroupsRaw(groupKey));
        }

        /// <summary>
        /// Retrieve a single specified group as an AdafruitIOHTTP.Group object.
        /// </summary>
        /// <param name="groupKey">Group key, or ID</param>
        /// <returns>AdafruitIOHTTP.Group object</returns>
        public AdafruitIO.Group GetGroup(string groupKey)
        {
            return AdafruitIO.Group.DeserializeArray(GroupsRaw(groupKey))[0];
        }

        /// <summary>
        /// Creates a specified Adafruit IO Group
        /// </summary>
        /// <param name="group">AdafruitIOHTTP.Group object</param>
        /// <returns>Web Request Return</returns>
        public string CreateGroup(AdafruitIO.Group group)
        {
            string path = "groups/";
            return _PostRequest(path, group.Serialize());
        }

        /// <summary>
        /// Deletes a specified group from Adafruit IO
        /// </summary>
        /// <param name="groupKey">Key or ID of group to be deleted</param>
        /// <returns>Web request response</returns>
        public string DeleteGroup(string groupKey)
        {
            string path = "groups/" + groupKey;
            return _DeleteRequest(path);
        }

        // Time Functions

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
        public AdafruitIO.Time ReceiveTime()
        {
            return AdafruitIO.Time.Deserialize(ReceiveTimeRaw());
        }

        // Weather Functions

        /// <summary>
        /// Gets weather from Adafruit IO
        /// </summary>
        /// <param name="weatherID">Weather ID for Adafruit IO</param>
        /// <returns>JSON string of weather</returns>
        public string ReceiveWeatherRaw(int weatherID = 0)
        {
            string path = "integrations/weather";
            if (weatherID != 0)
                path += "/" + weatherID.ToString();
            return _GetRequest(path);
        }

        // Randomizer Functions

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
        public AdafruitIO.Random ReceiveRandom(int randomizerID = 0)
        {
            return AdafruitIO.Random.Deserialize(ReceiveRandomRaw(randomizerID));
        }

        // HTTP Functions

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

        /// <summary>
        /// Handles any HTTP Errors
        /// </summary>
        /// <param name="webResponse">Response from Adafruit IO</param>
        /// <param name="response">Respose Message</param>
        private void _HandleError(HttpWebResponse webResponse, string response = null)
        {
            if (webResponse.StatusCode == (HttpStatusCode)429)
                throw new ThrottlingError("Exceeded the limit of Adafruit IO requests in a short period of time. Please reduce the rate of requests and try again later.");
            else if (webResponse.StatusCode >= HttpStatusCode.BadRequest)
                throw new RequestError("Adafruit IO request failed: " + webResponse.StatusCode.ToString() + " " + webResponse.StatusDescription + " " + response);
        }

        // Custom Exceptions

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
