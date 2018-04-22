using Newtonsoft.Json;

namespace controlExtension.ViewModel
{
    [JsonObject(MemberSerialization.OptIn)]
    class exper
    {
        [JsonProperty]
        public int comPortNum { get; set; }
    }
}
