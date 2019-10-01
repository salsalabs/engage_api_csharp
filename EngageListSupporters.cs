//One-off program to read all supporters.  Contains all of the necessary
//data definitions).

using System;
using System.Net;
using Newtonsoft.Json;

// Standard header returned for an API access.
public struct EngageHeader
{
    public int processingTime;
    public string serverId;
}
//A contact for a supporter.
public struct Contact
{
    public string type;
    public string value;
    public string status;
    public string optInDate;
}

//A custom fields.  Note that custom fields are allocated
//when then are defined.  If a supporter does not have a 
//custom field, then it will not appear.
public struct CustomFieldValue
{
    public string fieldId;
    public string name;
    public string value;
    public string type;
}

//A supporter's address.
public struct Address
{
    public string addressLine1;
    public string addressLine2;
    public string addressLine3;
    public string city;
    public string state;
    public string postalCode;
    public string county;
    public string country;
    public string federalHouseDistrict;
    public string stateHouseDistrict;
    public string stateSenateDistrict;
    public string countyDistrict;
    public string municipalityDistrict;
    public string lattitude;
    public string longitude;
    public string status;
    public string optInDate;
}
//A supporter.
public struct Supporter
{
    public string readOnly;
    public string supporterId;
    public string firstName;
    public string lastName;
    public string createdDate;
    public string lastModified;
    public Address address;
    public Contact[] contacts;
    public CustomFieldValue[] customFieldValues;
    public object dedication;
    public string result;
}
//Payload returned by searching for supporters.
public struct SupporterSearchResultsPayload
{
    public int total;
    public int offset;
    public Supporter[] supporters;

}
//Request requests a search for supporters.  Barebones for now.  
//Add variations later.
public struct SupporterSearchRequest
{
    public string modifiedFrom;
    public string modifiedTo;
    public int offset;
    public int count;
}

//Payload for a supporter search request.
public struct SupporterSearchRequestPayload
{
    public SupporterSearchRequest payload;
}

// Results returned by searching for supporters.
public struct SupporterSearchResults
{
    public string ID;
    public string timestamp;
    public SupporterSearchResultsPayload payload;
}
// Class containing the executable.
public class ListSupporters
{

    //Display the search results on the console.
    public void ShowResults(SupporterSearchResults searchResult)
    {
        Console.WriteLine("\n----------------- Results -----------------");
        Console.WriteLine("ID:           {0}", searchResult.ID);
        Console.WriteLine("Timestamp:    {0}", searchResult.timestamp);
        Console.WriteLine("\n----------------- Payload -----------------");
        SupporterSearchResultsPayload p = searchResult.payload;
        Console.WriteLine("Total:        {0}", p.total);
        Console.WriteLine("Offset:       {0}", p.offset);
        Console.WriteLine("Supporters:   {0}", p.supporters.Length);
        Console.WriteLine("\n----------------- Supporters -----------------");
        foreach (Supporter s in p.supporters)
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
                foreach (Contact c in s.contacts)
                {
                    Console.WriteLine("Contact:      {0} {1}", c.type, c.value);
                }
            }
            if (s.customFieldValues.Length != 0)
            {
                foreach (CustomFieldValue c in s.customFieldValues)
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
    public void Main()
    {
        string authToken = "VQexhhrG_7seUusAQ1aaHblMEAOYfLsy-nwXH-7RhF2PZshoS_RcsRtLCGLv4hJ13uNVAyYPqkYOkL6-8l3-9fFIBj5qGZER85TV3j7-PY6rh633tso7j4IfcAqTBPqoneAy4pn7kO5HMWrUOPiE0A";
        string hostName = "https://api.salsalabs.org";

        // List supporters since a date.
        string operation = "/api/integration/ext/v1/supporters/search";
        SupporterSearchRequest searchRequest = new SupporterSearchRequest();
        searchRequest.modifiedFrom = "2016-05-26T11:49:24.905Z";
        searchRequest.offset = 0;
        // TODO: fetch this count from a Metrics object.
        searchRequest.count = 20;
        var url = hostName + operation;

        do
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers["authToken"] = authToken;
                Console.WriteLine("\n----------------- Read from {0} -----------------", searchRequest.offset);

                SupporterSearchRequestPayload requestPayload = new SupporterSearchRequestPayload();
                requestPayload.payload = searchRequest;
                string payload = JsonConvert.SerializeObject(requestPayload);
                Console.WriteLine("request payload is {0}", payload);
                string s = client.UploadString(url, "POST", payload);
                SupporterSearchResults results = JsonConvert.DeserializeObject<SupporterSearchResults>(s);
                ShowResults(results);
                searchRequest.count = results.payload.supporters.Length;
                searchRequest.offset += searchRequest.count;
            }
        } while (searchRequest.count != 0);
    }
}
