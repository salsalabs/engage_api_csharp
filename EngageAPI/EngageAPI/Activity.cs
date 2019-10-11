using System;
namespace EngageAPI
{
    namespace Activity
    {
        //Constants for searching, adding and removing activities.
        public static class Constants
        {
            public const string SearchEndpoint = "/api/integration/ext/v1/activities/search";

            public const string SubscriptionManagement = "SUBSCRIPTION_MANAGEMENT";
            public const string Subscribe = "SUBSCRIBE";
            public const string Fundraise = "FUNDRAISE";
            public const string Petition = "PETITION";
            public const string TargetedLetter = "TARGETED_LETTER";
            public const string TicketedEvent = "TICKETED_EVENT";
            public const string P2PEvent = "P2P_EVENT";
        }

        //Activity header.
        public struct RequestHeader
        {
            string refId;
        }

        //Request requests a search for activities.  Barebones for now.  
        //Add variations later.
        public struct SearchRequest
        {
            public string modifiedFrom;
            public string modifiedTo;
            public string[] activityIds;
            public int offset;
            public int count;
            public string type;
        }

        //Payload for a activity search request.
        public struct SearchRequestPayload
        {
            public RequestHeader header;
            public SearchRequest payload;
        }

        // Response header.
        public struct ResponseHeader
        {
            public int processingTime;
            public string serverId;
        }
        //A basic activity.
        public struct Activity
        {
            public string activityType;
            public string activityId;
            public string activityFormName;
            public string activityFormId;
            public string supporterId;
            public string activityDate;
            public string lastModified;
            //Start donation specific fields.
            public string donationId;
            public string totalReceivedAmount;
            public string oneTimeAmount;
            public string donationType;
            public string accountType;
            public string accountNumber;
            public string accountExpiration;
            public string accountProvider;
            public string paymentProcessorName;
            public string fundName;
            public string fundGLCode;
            public string designation;
            public string dedicationType;
            public string dedication;
            public string notify;
            public Transaction[] transactions;
        }

        //Activity response payload.
        public struct SearchResultsPayload
        {
            public int total;
            public int offset;
            public int count;
            public Activity[] activities;
        }

        //Acivity response.
        public struct SearchResults
        {
            public ResponseHeader header;
            public SearchResultsPayload payload;
        }

        //Transaction.  A donation record may contain several transactions,
        //particulary in the case of recurring donations or events.
        public struct Transaction
        {
            public string transactionId;
            public string type;
            public string reason;
            public string date;
            public string amount;
            public string feesPaid;
            public string gatewayTransactionId;
        }

    }
}
