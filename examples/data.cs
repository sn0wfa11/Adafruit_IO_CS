using System;
using System.Collections.Generic;
using Adafruit_IO_CS;  // Required library

namespace AdafruitIO_Data_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an AdafruitIOHTTP Object and specify your UserID, and API Key
            AdafruitIOHTTP aio = new AdafruitIOHTTP(<Adafruit IO Username>, <Adafruit IO API Key>);

            // Sending Data

            // Simple send string
            aio.SendData("testgroup.testDataKey", "TestData1");

            // Send as Data object examples
            AdafruitIO.Data testData2 = new AdafruitIO.Data("TestData2");
            aio.SendData("testgroup.testDataKey", testData2);

            aio.SendData("testgroup.testDataKey", new AdafruitIO.Data("TestData3"));

            AdafruitIO.Data testData4 = new AdafruitIO.Data("TestData4", "42.026944", "93.646944", "900");
            aio.SendData("testgroup.testDataKey", testData4);

            AdafruitIO.Data testData5 = new AdafruitIO.Data("TestData4", 42.026944, 93.646944, 900);
            aio.SendData("testgroup.testDataKey", testData5, 2); // Sending with rounding precision (example of 2 decimal places)

            // Send Data and then Deserialize response into a Data object
            string jsonResponse = aio.SendData("testgroup.testDataKey", "TestSendRec");
            AdafruitIO.Data newData = AdafruitIO.Data.Deserialize(jsonResponse);
            Console.WriteLine(newData.value);
            Console.WriteLine(newData.created_at.ToString());
            Console.WriteLine(newData.id);

            // Serialize a Data object to a JSON string
            Console.WriteLine(newData.Serialize());

            // Receiving Data

            // Simple Recieve Methods
            Console.WriteLine(aio.Receive("testgroup.testDataKey").value);
            Console.WriteLine(aio.ReceiveNext("testgroup.testDataKey").value);
            Console.WriteLine(aio.ReceivePrevious("testgroup.testDataKey").value);

            // Raw JSON String
            Console.WriteLine(aio.ReceiveDataRaw("testgroup.testDataKey"));
            Console.WriteLine(aio.ReceiveDataRaw("testgroup.testDataKey", "0ERA3E1E6XKTQE4QWN1CM2P5PS")); // Single data entry using DataID

            // Receiving Data Sets

            // As Array
            AdafruitIO.Data[] testDataArray = aio.ReceiveDataArray("testgroup.testDataKey");
            for (int x = 0; x < testDataArray.Length; x++)
                Console.WriteLine(testDataArray[x].value + " ID: " + testDataArray[x].id);

            // As List
            List<AdafruitIO.Data> testDataList = aio.ReceiveDataList("testgroup.testDataKey");
            for (int x = 0; x < testDataList.Count; x++)
                Console.WriteLine(testDataList[x].value + " ID: " + testDataList[x].id);

            // Single Data Entry using Data ID
            AdafruitIO.Data testDataSingle = aio.ReceiveDataArray("testgroup.testDataKey", "0ERA3E1E6XKTQE4QWN1CM2P5PS")[0];

            // Delete Data
            aio.DeleteData("testgroup.testDataKey", "0ERA3E1E6XKTQE4QWN1CM2P5PS");

            Console.ReadLine();
        }
    }
}