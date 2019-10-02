using System;
namespace EngageAPI
{
    //Base is a class that accepts an Engage API token and host URL.
    //The token is required.  The host URL is optional and is used to
    //point API reqests at one of Salsa's internal systems.
    public class Base {
        private const string defaultHost = "https://api.salsalabs.org/";
        private string token, host;
        public Base(string inToken, string inHost) {
            token = inToken;
            if (inHost != null && inHost.Length != 0) {
                host = inHost;
            } else {
                host = defaultHost;
            }
        }
        public string Host {
            get {
                return host;
            }
        }
        public string Token {
            get { 
                return token;
            }
        }
    }
}
