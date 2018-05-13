using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace controlExtension.ViewModel
{
    [JsonObject(MemberSerialization.OptIn)]
    public class exper
    {
        public exper()
        {
            time = new List<TimeSpan>();
            rawData = new List<double>();
            miu = new List<double>();
        }
        [JsonProperty]
        public DateTime tStart { get; set; }
        [JsonProperty]
        public double massa { get; set; }
        [JsonProperty]
        public double speed { get; set; }
        [JsonProperty]
        public double dT { get; set; }
        [JsonProperty]
        public List<TimeSpan> time { get; set; }
        [JsonProperty]
        public List<double> rawData { get; set; }
        [JsonProperty]
        public List<double> miu { get; set; }
    }
}