using Newtonsoft.Json;
using System.Collections.Generic;


namespace LogMailGunSvc.Entities
{
    public class RootMGRC
    {
        public List<Item> items { get; set; }
        public Paging paging { get; set; }
    }

    public class Item
    {
        public List<object> tags { get; set; }
        public double timestamp { get; set; }
        public Storage storage { get; set; }
        public Envelope envelope { get; set; }

        [JsonProperty("recipient-domain", NullValueHandling = NullValueHandling.Ignore)]
        public string recipientdomain { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }
        public List<object> campaigns { get; set; }

        [JsonProperty("user-variables")]
        public UserVariables uservariables { get; set; }
        public Flags flags { get; set; }

        [JsonProperty("log-level")]
        public string loglevel { get; set; }
        public Message message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string recipient { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string @event { get; set; }

        [JsonProperty("delivery-status")]
        public DeliveryStatus deliverystatus { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string method { get; set; }

        [JsonProperty("originating-ip", NullValueHandling = NullValueHandling.Ignore)]
        public string originatingip { get; set; }
    }

    public class Storage
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string url { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string region { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string env { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string key { get; set; }
    }

    public class Envelope
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string transport { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string sender { get; set; }

        [JsonProperty("sending-ip", NullValueHandling = NullValueHandling.Ignore)]
        public string sendingip { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string targets { get; set; }
    }

    public class UserVariables
    {
    }

    public class Flags
    {
        [JsonProperty("is-routed")]
        public bool isrouted { get; set; }

        [JsonProperty("is-authenticated")]
        public bool isauthenticated { get; set; }

        [JsonProperty("is-system-test")]
        public bool issystemtest { get; set; }

        [JsonProperty("is-test-mode")]
        public bool istestmode { get; set; }
    }

    public class Message
    {
        public Headers headers { get; set; }
        public List<object> attachments { get; set; }
        public int size { get; set; }
    }

    public class Headers
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string to { get; set; }

        [JsonProperty("message-id", NullValueHandling = NullValueHandling.Ignore)]
        public string messageid { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string from { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string subject { get; set; }
    }

    public class DeliveryStatus
    {
        public bool tls { get; set; }

        [JsonProperty("mx-host", NullValueHandling = NullValueHandling.Ignore)]
        public string mxhost { get; set; }

        [JsonProperty("attempt-no", NullValueHandling = NullValueHandling.Ignore)]
        public int attemptno { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string description { get; set; }

        [JsonProperty("session-seconds")]
        public double sessionseconds { get; set; }
        public bool utf8 { get; set; }

        [JsonProperty("enhanced-code", NullValueHandling = NullValueHandling.Ignore)]
        public string enhancedcode { get; set; }
        public int code { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string message { get; set; }

        [JsonProperty("certificate-verified")]
        public bool certificateverified { get; set; }
    }

    public class Paging
    {
        public string previous { get; set; }
        public string first { get; set; }
        public string last { get; set; }
        public string next { get; set; }
    }
}
