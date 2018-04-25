using Newtonsoft.Json;

namespace controlExtension
{
    [JsonObject(MemberSerialization.OptIn)]
    class settings
    {
        [JsonProperty]
        public int comPortNum { get; set; }
    }
}
