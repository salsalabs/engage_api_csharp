//One-off program to add a supporter to Engage.  You provide the API token
//in an environment variable named "TOKEN".  You provide first name, last name
//email in the static strings.
//
//There are two possible outcomes.  The first is that the supporter is added
//to Engage.  That's a success indication.  The second is that the supporter
//already exists in Engage.  That's a found indication.  Note that a found
//supporter will have first and last name updated by the API call.
//See:
//https://help.salsalabs.com/hc/en-us/articles/224470107-Engage-API-Supporter-Data#adding-updating-and-deleting-supporters

using System;
using System.Net;
using Newtonsoft.Json;


// Class containing the executable.
public class AddSupporterApp
{
    public static string firstName = "Mergatroyd";
    public static string lastName = "Bzramlik";
    public static string email = "mergatroyd@cnn.com";

    public AddSupporterApp()
    {
    }

    //Display the search results on the console.
    public void ShowResults(EngageAPI.Supporter.UpsertResponse results)
    {
        Console.WriteLine("\n----------------- Results -----------------");
        Console.WriteLine("ID:           {0}", results.id);
        Console.WriteLine("Timestamp:    {0}", results.timeStamp);
        Console.WriteLine("\n----------------- Payload -----------------");
        EngageAPI.Supporter.UpsertResponsePayload p = results.payload;
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
            if (s.customFieldValues!= null && s.customFieldValues.Length != 0)
            {
                foreach (EngageAPI.Supporter.CustomFieldValue c in s.customFieldValues)
                {
                    if (c.value != null)
                    {
                        Console.WriteLine("CustomField: {0} = {1}", c.name, c.value);
                    }
                }
            }
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
        string host = Environment.GetEnvironmentVariable("HOST");
        if (host == null)
        {
            host = EngageAPI.Constants.APIHost;
        }

        //Add a supporter record using the static variables.
        EngageAPI.Supporter.Supporter supporter = new EngageAPI.Supporter.Supporter();
        supporter.firstName = firstName;
        supporter.lastName = lastName;

        EngageAPI.Supporter.Contact contact = new EngageAPI.Supporter.Contact();
        contact.type = EngageAPI.Supporter.Constants.Email;
        contact.value = email;
        contact.status = EngageAPI.Supporter.Constants.OptIn;
        supporter.contacts = new EngageAPI.Supporter.Contact[1];
        supporter.contacts[0] = contact;

        //supporter.address = new EngageAPI.Supporter.Address();
        //supporter.customFieldValues = new EngageAPI.Supporter.CustomFieldValue[0];

        EngageAPI.Supporter.UpsertRequest request = new EngageAPI.Supporter.UpsertRequest();
        request.payload = new EngageAPI.Supporter.UpsertRequestPayload();
        request.payload.supporters = new EngageAPI.Supporter.Supporter[1];
        request.payload.supporters[0] = supporter;

        var url = host + EngageAPI.Supporter.Constants.UpsertEndpoint;
        Console.WriteLine("url is {0}", url);
        AddSupporterApp app = new AddSupporterApp();
        using (var client = new WebClient())
        {
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers["authToken"] = authToken;

            JsonSerializerSettings settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            string payload = JsonConvert.SerializeObject(request, Formatting.None, settings);
            Console.WriteLine("request is {0}", payload);
            Console.WriteLine("method is {0}", EngageAPI.Constants.UpsertMethod);
            string s = client.UploadString(url, EngageAPI.Constants.UpsertMethod, payload);
            Console.WriteLine("Response:\n{0}\n", s);

            EngageAPI.Supporter.UpsertResponse results = JsonConvert.DeserializeObject<EngageAPI.Supporter.UpsertResponse>(s, settings);
            app.ShowResults(results);
        }
    }
}
