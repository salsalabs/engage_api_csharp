using System;
using System.Net;
using System.Collections;
using Newtonsoft.Json;

class ListSupporters
{
	static void Main ()
	{
		string authTokenToken = "your-incredibly-long-token-here";
		string hostName = "https://api.salsalabs.org";

		// List supporters since a date.
		string operation = "/api/integration/ext/v1/supporters/search";
		Hashtable payloadInner = new Hashtable ();
		payloadInner.Add("modifiedFrom","2016-05-26T11:49:24.905Z");
		payloadInner.Add("offset", 0);
		payloadInner.Add("count", 10);
		Console.WriteLine ("Converted payload innner.  Results:");
		Console.WriteLine (payloadInner);
		Hashtable payloadHash = new Hashtable();
		payloadHash.Add("payload", payloadInner);

		string payload = JsonConvert.SerializeObject(payloadHash);
		Console.WriteLine ("Converted JSON.  Results:");
		Console.WriteLine (payload);
		var url = hostName + operation;

		string result = "";
		using (var client = new WebClient())
		{
			client.Headers[HttpRequestHeader.ContentType] = "application/json"; 
			client.Headers["authToken"] = authTokenToken;
			result = client.UploadString(url, "POST", payload);
			Console.WriteLine ("Search for supporters since 2016-05-26.  Results:");
			Console.WriteLine(result);
			// Object data = JsonConvert.DeserializeObject(result);
		}


		// List supporters matching an email.
		payload = @"{
			""payload"":{
				""identifiers"":[
				""paulahouser24@yahoo.com"",
				""barney@google.bizi"",
				""fred@flinstone.bizi""],
				""identifierType"": ""EMAIL_ADDRESS""}
		}";
		using (var client = new WebClient())
		{
			client.Headers[HttpRequestHeader.ContentType] = "application/json"; 
			client.Headers["authToken"] = authTokenToken;
			result = client.UploadString(url, "POST", payload);
			Console.WriteLine ("Search for matching email address.  Results:");
			Console.WriteLine(result);
			// Object data = JsonConvert.DeserializeObject(result);
		}
	}
}
