using Newtonsoft.Json;

namespace League.api.Models
{
    public class Player
    {
        // tells the serialiser not to put anything in this property
        [JsonIgnore]
        public int ID { get; set; }
        //  tells the serialiser, when you recieve a variable called id put it below
        [JsonProperty("ID")]
        public string DummyInt { get; set; }
        public string AccID { get; set; }
        [JsonProperty("Name")]
        public string SummonerName { get; set; }
        public string PUUID { get; set; }
        [JsonProperty("summonerLevel")]
        public int SummonerLevel { get; set; }
        public List<Match> Matches { get; set; }
    }
}
