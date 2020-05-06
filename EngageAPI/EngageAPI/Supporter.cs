using System;
namespace EngageAPI
{
    namespace Supporter
    {
        //Constants for searching, adding and removing supporters.
        public static class Constants
        {
            public const string SearchEndpoint = "/api/integration/ext/v1/supporters/search";
            public const string AddEndpoint = "/api/integration/ext/v1/supporters";
            public const string UpdateEndpoint = AddEndpoint;
            public const string UpsertEndpoint = AddEndpoint;
            public const string DeleteEndpoint = AddEndpoint;
            public const string Email = "EMAIL";
            public const string HomePhone = "HOME_PHONE";
            public const string CellPhone = "CELL_PHONE";
            public const string WorkPhone = "WORK_PHONE";
            public const string FaceBookID = "FACEBOOK_ID";
            public const string TwitterID = "TWITTER_ID";
            public const string LinkedInID = "LINKEDIN_ID";
            public const string OptIn = "OPT_IN";
            public const string OptOut = "OPT_OUT";
            public const string HardBounce = "HARD_BOUNCE";
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
            public string SupporterId;
            public string firstName;
            public string lastName;
            public string createdDate;
            public string lastModified;
            public Address address;
            public Contact[] contacts;
            public CustomFieldValue[] customFieldValues;
            public string result;
        }
        //Payload returned by searching for s.
        public struct SearchResultsPayload
        {
            public int total;
            public int offset;
            public Supporter[] supporters;

        }
        //Request requests a search for supporters modified between
        //'modifiedFrom' and 'modifiedTo'.
        //Add variations later.
        public struct SearchRequest
        {
            public string modifiedFrom;
            public string modifiedTo;
            public int offset;
            public int count;
        }

        //Payload for a supporter search request.
        public struct SearchRequestPayload
        {
            public SearchRequest payload;
        }

        // Wrapper around list of returned Supporters.
        public struct SearchResults
        {
            public string id;
            public string timestamp;
            public SearchResultsPayload payload;
        }

        //Upsert (add/modify) request.  Engage uses the same call sequence
        //for both add and modify.  Having an upsert will reduce confustion.
        public struct UpsertRequest
        {
            public UpsertRequestPayload payload;
        }

        //Upsert payload.  Contains the list of supporters to add/modify.
        public struct UpsertRequestPayload
        {
            public Supporter[] supporters;
        }

        //Upsert response.  
        public struct UpsertResponse
        {
            public string id;
            public string timeStamp;
            public UpsertResponsePayload payload;
        }

        //Upsert response payload.  Wraps around a list of added/modified
        //supporters.
        public struct UpsertResponsePayload
        {
            public Supporter[] supporters;
        }

    }
}