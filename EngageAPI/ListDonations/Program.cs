//One-off program to read all Activities.  Contains all of the necessary
//data definitions).

using System;
using System.Net;
using Newtonsoft.Json;


// Class containing the executable.
public class ListDonationApp
{
    public ListDonationApp()
    {
    }

    //Display the search results on the console.
    public void ShowResults(EngageAPI.Activity.SearchResults searchResult)
    {
        Console.WriteLine("\n----------------- Results -----------------");
        Console.WriteLine("Server ID:              {0}", searchResult.header.serverId);
        Console.WriteLine("ProcessingTime:         {0}", searchResult.header.processingTime);
        Console.WriteLine("\n----------------- Payload -----------------");
        EngageAPI.Activity.SearchResultsPayload p = searchResult.payload;
        Console.WriteLine("Total:                  {0}", p.total);
        Console.WriteLine("Offset:                 {0}", p.offset);
        Console.WriteLine("Activities:             {0}", p.activities.Length);
        Console.WriteLine("\n----------------- Activities -----------------");
        foreach (EngageAPI.Activity.Activity s in p.activities)
        {
            Console.WriteLine("activityType         {0}", s.activityType);
            Console.WriteLine("activityId           {0}", s.activityId);
            Console.WriteLine("activityFormName     {0}", s.activityFormName);
            Console.WriteLine("activityFormId       {0}", s.activityFormId);
            Console.WriteLine("supporterId          {0}", s.supporterId);
            Console.WriteLine("activityDate         {0}", s.activityDate);
            Console.WriteLine("lastModified         {0}", s.lastModified);

            Console.WriteLine("donationId           {0}", s.donationId);
            Console.WriteLine("totalReceivedAmount  {0}", s.totalReceivedAmount);
            Console.WriteLine("oneTimeAmount        {0}", s.oneTimeAmount);
            Console.WriteLine("donationType         {0}", s.donationType);
            Console.WriteLine("accountType          {0}", s.accountType);
            Console.WriteLine("accountNumber        {0}", s.accountNumber);
            Console.WriteLine("accountExpiration    {0}", s.accountExpiration);
            Console.WriteLine("accountProvider      {0}", s.accountProvider);
            Console.WriteLine("paymentProcessorName {0}", s.paymentProcessorName);
            Console.WriteLine("fundName             {0}", s.fundName);
            Console.WriteLine("fundGLCode           {0}", s.fundGLCode);
            Console.WriteLine("designation          {0}", s.designation);
            Console.WriteLine("dedicationType       {0}", s.dedicationType);
            Console.WriteLine("dedication           {0}", s.dedication);
            Console.WriteLine("notify               {0}", s.notify);

            Console.WriteLine();

        }

    }
    //Application starts here...
    public static void Main(string[] args)
    {
        // Retrieve the Engage token from the environment.
        string authToken = Environment.GetEnvironmentVariable("TOKEN");
        if (authToken == null)
        {
            Console.WriteLine("Error:  The environment variable TOKEN must contain your Engage API token.");
            Environment.Exit(0);
        }

        // List Activities since a date.
        string operation = "/api/integration/ext/v1/activities/search";
        EngageAPI.Activity.SearchRequest searchRequest = new EngageAPI.Activity.SearchRequest();
        searchRequest.modifiedFrom = "2015-10-01T00:00:00.00Z";
        searchRequest.offset = 0;
        // TODO: fetch this count from a Metrics object.
        searchRequest.count = 20;
        searchRequest.type = EngageAPI.Activity.Constants.Fundraise;
        var url = EngageAPI.Constants.APIHost + EngageAPI.Activity.Constants.SearchEndpoint;
        ListDonationApp app = new ListDonationApp();
        do
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers["authToken"] = authToken;
                Console.WriteLine("\n----------------- Read from     {0} -----------------", searchRequest.offset);

                EngageAPI.Activity.SearchRequestPayload requestPayload = new EngageAPI.Activity.SearchRequestPayload();
                requestPayload.payload = searchRequest;
                string payload = JsonConvert.SerializeObject(requestPayload);
                Console.WriteLine("request payload is     {0}", payload);
                string s = client.UploadString(url, EngageAPI.Constants.SearchMethod, payload);
                EngageAPI.Activity.SearchResults results = JsonConvert.DeserializeObject<EngageAPI.Activity.SearchResults>(s);
                app.ShowResults(results);
                searchRequest.count = results.payload.activities.Length;
                searchRequest.offset += searchRequest.count;
            }
        } while (searchRequest.count != 0);
    }
}
