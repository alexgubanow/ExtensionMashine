using Newtonsoft.Json;

namespace controlExtension.ViewModel
{
    [JsonObject(MemberSerialization.OptIn)]
    class settings
    {
        [JsonProperty]
        public int comPortNum { get; set; }
    }
}
