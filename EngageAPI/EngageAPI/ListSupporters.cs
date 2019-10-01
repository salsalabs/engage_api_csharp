//One-off program to read all supporters.  Contains all of the necessary
//data definitions).

using System;
using System.Net;
using Newtonsoft.Json;


    // Class containing the executable.
    public class ListSupporterApp
    {
        public static string hostName = "https://api.salsalabs.org";

        public ListSupporterApp()
            {
            }

        //Display the search results on the console.
        public void ShowResults(EngageAPI.Supporter.SearchResults searchResult)
            {
            Console.WriteLine("\n----------------- Results -----------------");
            Console.WriteLine("ID:           {0}", searchResult.ID);
            Console.WriteLine("Timestamp:    {0}", searchResult.timestamp);
            Console.WriteLine("\n----------------- Payload -----------------");
            EngageAPI.Supporter.SearchResultsPayload p = searchResult.payload;
            Console.WriteLine("Total:        {0}", p.total);
            Console.WriteLine("Offset:       {0}", p.offset);
            Console.WriteLine("Supporters:   {0}", p.supporters.Length);
            Console.WriteLine("\n----------------- Supporters -----------------");
            foreach (EngageAPI.Supporter.Supporter s in p.supporters)
            {
                Console.WriteLine("Result:       {0}", s.result);
                Console.WriteLine("Name:         {0} {1}", s.firstName, s.lastName);
                Console.WriteLine("Created:      {0}", s.createdDate);
                if (s.address.city != null)
                {
                    Console.WriteLine("Address:      {0} {1} {2}", s.address.city, s.address.state, s.address.postalCode);
                }
                if (s.contacts.Length != 0)
                {
                    foreach (EngageAPI.Supporter.Contact c in s.contacts)
                    {
                        Console.WriteLine("Contact:      {0} {1}", c.type, c.value);
                    }
                }
                if (s.customFieldValues.Length != 0)
                {
                    foreach (EngageAPI.Supporter.CustomFieldValue c in s.customFieldValues)
                    {
                        if (c.value != null)
                        {
                            Console.WriteLine("CustomField: {0} = {1}", c.name, c.value);
                        }
                    }
                }
                if (s.dedication != null)
                {
                    Console.WriteLine("Dedication:   {0}", s.dedication);
                }
                Console.WriteLine();
            }

        }
        //Application starts here...
        public static void Main2(string[] args)
        {
        // Retrieve the Engage token from the environment.
        string authToken = Environment.GetEnvironmentVariable("TOKEN");
        if (authToken == null)
        {
            Console.WriteLine("Error:  The environment variable TOKEN must contain your Engage API token.");
            Environment.Exit(0);
        }
        // List supporters since a date.
        string operation = "/api/integration/ext/v1/activities/search";
            EngageAPI.Supporter.SearchRequest searchRequest = new EngageAPI.Supporter.SearchRequest();
            searchRequest.modifiedFrom = "2016-05-26T11:49:24.905Z";
            searchRequest.offset = 0;
            // TODO: fetch this count from a Metrics object.
            searchRequest.count = 20;
            var url = ListSupporterApp.hostName + operation;
            ListSupporterApp app = new ListSupporterApp();
            do
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers["authToken"] = authToken;
                    Console.WriteLine("\n----------------- Read from {0} -----------------", searchRequest.offset);

                    EngageAPI.Supporter.SearchRequestPayload requestPayload = new EngageAPI.Supporter.SearchRequestPayload();
                    requestPayload.payload = searchRequest;
                    string payload = JsonConvert.SerializeObject(requestPayload);
                    Console.WriteLine("request payload is {0}", payload);
                    string s = client.UploadString(url, "POST", payload);
                    EngageAPI.Supporter.SearchResults results = JsonConvert.DeserializeObject<EngageAPI.Supporter.SearchResults>(s);
                    app.ShowResults(results);
                    searchRequest.count = results.payload.supporters.Length;
                    searchRequest.offset += searchRequest.count;
                }
            } while (searchRequest.count != 0);
        }
    }
