using Newtonsoft.Json;
using System.Collections.Generic;

namespace controlExtension.ViewModel
{
    [JsonObject(MemberSerialization.OptIn)]
    public class exper
    {
        [JsonProperty]
        public double massa { get; set; }
        [JsonProperty]
        public double speed { get; set; }
        [JsonProperty]
        public double dT { get; set; }
        [JsonProperty]
        public List<double> rawData { get; set; }
        [JsonProperty]
        public List<double> miu { get; set; }
    }
}