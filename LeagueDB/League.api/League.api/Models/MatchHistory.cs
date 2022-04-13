namespace League.api.Models
{
    public class MatchHistory
    {
        public class Matches
        {
            public string MatchID { get; set; }
        }
        public List<Matches> Matches { get; set; }
    }
}
