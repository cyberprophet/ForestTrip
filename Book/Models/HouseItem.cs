using Newtonsoft.Json;

using System.Runtime.Serialization;

namespace ShareInvest.Models;

struct HouseItem
{
    [DataMember, JsonProperty("id")]
    internal string Id
    {
        get; set;
    }

    [DataMember, JsonProperty("text")]
    internal string Name
    {
        get; set;
    }

    [DataMember, JsonProperty("region")]
    internal string Region
    {
        get; set;
    }
}