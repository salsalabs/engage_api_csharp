using System;
namespace EngageAPI
{
    //Constants used by EngageAPI for ACID functions.  Upsert is a common
    //usage and is *not* described in Engage.
    public static class Constants
    {
        public const string APIHost = "https://api.salsalabs.org";
        public const string SearchMethod = "POST";
        public const string AddMethod = "PUT";
        public const string UpdateMethod = AddMethod;
        public const string UpsertMethod = AddMethod;
        public const string DeleteMethod = "DELETE";
    }
}
