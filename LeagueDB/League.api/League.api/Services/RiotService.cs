using League.api.Models;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using League.api.Controllers;

namespace League.api.Services
{
    public class SqlInteractions
    {
        private readonly string SqlConnectionString = @"Server=sqldata,1433;User ID =sa; Password=<leagueDBpass>; Initial Catalog=Riot; TrustServerCertificate=true";

        public int SendSummoner(string summonerName, string puuid, int summonerLevel, ILogger<RiotController> logger)
        {
            // interpolated string that can put in the summoners name via data.
            string query = "insert into Summoners(SummonerName, Puuid, Summoner_level) values(@SummonerName, @Puuid, @Summoner_level) select scope_identity()";
            // only uses the sql connection while actually sending data, then closing it after use.
            // this also helps for making sure that there is a connection before attempting to connect
            try
            {
                using (SqlConnection sql = new SqlConnection(SqlConnectionString))
                {
                    try
                    {
                        sql.Open();
                        using (SqlCommand cmd = new SqlCommand(query, sql))
                        {
                            // sets the params to make sure that the data will go into the sql db nicely
                            cmd.Parameters.Add("@SummonerName", System.Data.SqlDbType.NVarChar).Value = summonerName;
                            cmd.Parameters.Add("@Puuid", System.Data.SqlDbType.NVarChar).Value = puuid;
                            cmd.Parameters.Add("@Summoner_level", System.Data.SqlDbType.Int).Value = summonerLevel;
                            // executes the query
                            return (int)cmd.ExecuteScalar();
                        }
                    }
                    catch (Exception e)
                    {
                        // log error for debug
                        logger.LogInformation(e.ToString());
                    }
                }
            }
            catch(Exception e)
            {
                logger.LogInformation(e.ToString());
            }
            return 0;
        }
        public int SendMatch(int GameID, int GameDuration, ILogger<RiotController> logger)
        {
            // interpolated sql string that can put in the summoners name via data.
            string query = "insert into Matches(GameID, GameDuration) values(@GameID, @GameDuration) select scope_identity()";
            // only uses the sql connection while actually sending data, then closing it after use.
            // this also helps for making sure that there is a connection before attempting to connect
            try
            {
                using (SqlConnection sql = new SqlConnection(SqlConnectionString))
                {
                    try
                    {
                        sql.Open();
                        using (SqlCommand cmd = new SqlCommand(query, sql))
                        {
                            // sets the params to make sure that the data will go into the sql db nicely
                            cmd.Parameters.Add("@GameID", System.Data.SqlDbType.NVarChar).Value = GameID;
                            cmd.Parameters.Add("@GameDuration", System.Data.SqlDbType.NVarChar).Value = GameDuration;
                            // executes the query
                            return (int)cmd.ExecuteScalar();
                        }
                    }
                    catch (Exception e)
                    {
                        // log error for debug
                        logger.LogInformation(e.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                logger.LogInformation(e.ToString());
            }
            return 0;
        }

    }

    public class RiotService : IRiotService
    {    
        // easier to manage declaration
        private readonly string Token = "RGAPI-a23d6084-5119-4015-b55c-9df3c129d34e";
        private readonly string TokenKey = "X-Riot-Token";
        private readonly IHttpClientFactory clientFactory;

        public RiotService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
            // allows for http clients to be made
        }
        /*
        public async Task<IActionResult> ShowMatchHistory([FromQuery] string puuid)
        {
            // string iterpolation requires $, allowing for the link to be changed depending on the variable value.
            var response = await client.GetAsync($"https://europe.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids");

            try
            {
                var DeRes = JsonConvert.DeserializeObject<List<String>>(await response.Content.ReadAsStringAsync());
                // if deres is not null it will select a new object, basically a lambada expression to loop through the items in the list.
                var NewRes = DeRes?.Select(x => new Match() { MatchID = x });
                for (int i = 0; i < NewRes.Count(); i++)
                {
                    string ID = NewRes.ElementAt(i).MatchID;
                    var Win = await client.GetAsync($"https://europe.api.riotgames.com/lol/match/v5/matches/{ID}");
                    var DeWin = JsonConvert.DeserializeObject<Team>(await response.Content.ReadAsStringAsync());
                    // todo: deserialize as enitre value then pull multiple things from it, allows for a more informative display

                }
                return Ok(NewRes);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        */

        //kind of the method in charge to deal with the other methods
        public async Task<List<Match>> GetMatchesAsync(string puuid)
        {
            List<string> Ids = await GetMatchIdsAsync(puuid);
            // use () to avoid using new type if explicitly stated
            List<Match> matches = new();
            foreach (string x in Ids)
            {
                // temp var
                var temp = await GetMatchAsync(x);
                matches.Add(temp);
            }
            return matches;
        }
        private async Task<List<string>> GetMatchIdsAsync(string puuid)
        {
            var client = getRiotClient();
            // string iterpolation requires $, allowing for the link to be changed depending on the variable value.
            var response = await client.GetAsync($"https://europe.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids");
            // turns the data into a json file which allows for easier manipulation
            var DeRes = JsonConvert.DeserializeObject<List<String>>(await response.Content.ReadAsStringAsync());
            return DeRes;
        }
        private async Task<Match> GetMatchAsync(string matchId)
        {
            var client = getRiotClient();
            var Win = await client.GetAsync($"https://europe.api.riotgames.com/lol/match/v5/matches/{matchId}");
            var DeWin = JsonConvert.DeserializeObject<Match>(await Win.Content.ReadAsStringAsync());
            return DeWin;
        }

        // ? - warns the compiler that a element could be null, and if it is it'll skip the code path.
        public async Task<Player?> GetPlayerAsync(string summonerName)
        {
            // init reusable client
            var client = getRiotClient();
       
            // send request to the riot api
            var response = await client.GetAsync($"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/{summonerName}");
            // attempt to deserialise it into a player object
            try
            {
                // converts the serialied object into a desearialized object, for instance this is converted into a player object.
                var DeserialisedResponse = JsonConvert.DeserializeObject<Player>(await response.Content.ReadAsStringAsync());
                return DeserialisedResponse;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        private HttpClient getRiotClient()
        {
            //creates client 
            var client = clientFactory.CreateClient();
            // feed in token
            client.DefaultRequestHeaders.Add(TokenKey, Token);
            return client;
        }
    }
}
