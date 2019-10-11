﻿using System;
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
            public string Id;
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
            public string ID;
            public string timestamp;
            public SearchResultsPayload payload;
        }

        //Upsert (add/mmodify) request.

     

    }
}