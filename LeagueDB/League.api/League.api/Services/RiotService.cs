using League.api.Models;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using League.api.Controllers;
using Dapper;

namespace League.api.Services
{
    public class SqlInteractions
    {
        // ctr k + ctrl c comment block
        //SELECT * FROM Summoners
        //LEFT JOIN SummonerMatches ON SummonerMatches.SummonerID = Summoners.ID
        //LEFT JOIN Matches ON SummonerMatches.MatchID = Matches.ID
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
                        using (SqlCommand command = new SqlCommand("SELECT COUNT(*) from Summoners where SummonerName like @name", sql))
                        {
                            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = summonerName;
                            var check = command.ExecuteScalar();
                            logger.LogInformation(check.ToString() + "--------- HERE---------------");
                            // if the data doesnt exist, insert into summoners table
                            if((int)check <= 0)
                            {
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

        //public int SendMatch(int GameID, int GameDuration, ILogger<RiotController> logger)
        //{
        //    // interpolated sql string that can put in the summoners name via data.
        //    string query = "insert into Matches(GameID, GameDuration) values(@GameID, @GameDuration) select scope_identity()";
        //    // only uses the sql connection while actually sending data, then closing it after use.
        //    // this also helps for making sure that there is a connection before attempting to connect
        //    try
        //    {
        //        using (SqlConnection sql = new SqlConnection(SqlConnectionString))
        //        {
        //            try
        //            {
        //                sql.Open();
        //                using (SqlCommand cmd = new SqlCommand(query, sql))
        //                {
        //                    // sets the params to make sure that the data will go into the sql db nicely
        //                    cmd.Parameters.Add("@GameID", System.Data.SqlDbType.NVarChar).Value = GameID;
        //                    cmd.Parameters.Add("@GameDuration", System.Data.SqlDbType.NVarChar).Value = GameDuration;
        //                    // executes the query
        //                    return (int)cmd.ExecuteScalar();
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                // log error for debug
        //                logger.LogInformation(e.ToString());
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logger.LogInformation(e.ToString());
        //    }
        //    return 0;
        //}

    }

    public class RiotService : IRiotService
    {
        // easier to manage declaration
        private readonly string SqlConnectionString = @"Server=sqldata,1433;User ID =sa; Password=<leagueDBpass>; Initial Catalog=Riot; TrustServerCertificate=true";
        private readonly string Token = "RGAPI-f5ca82c3-cd95-4513-b3f7-eaea927bd1fd";
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
        public async Task<List<Match?>?> GetMatchesAsync(Player player)
        {
           List<string>? Ids = await GetMatchIdsAsync(player.PUUID);

            if (Ids?.Any() == true)
            {
                // use () to avoid using new type if explicitly stated
                List<Task<Match?>> matchTasks = new();
                foreach (string x in Ids)
                {
                    // temp var
                    matchTasks.Add(GetMatchAsync(x));
                }
                // waits for all tasks to finish
                await Task.WhenAll(matchTasks);

                List<Match?> matchList = matchTasks.Select(x => x.Result).ToList();
                await PopulateTable(matchList, player);
                return matchList;
            }
            return null;
        }

        private async Task<List<string>?> GetMatchIdsAsync(string puuid)
        {
            var client = getRiotClient();
            // string iterpolation requires $, allowing for the link to be changed depending on the variable value.
            var response = await client.GetAsync($"https://europe.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids");
            // turns the data into a json file which allows for easier manipulation
            try
            {
                string temp = await response.Content.ReadAsStringAsync();
                var DeRes = JsonConvert.DeserializeObject<List<String>>(temp);
                return DeRes;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        private async Task<Match?> GetMatchAsync(string matchId)
        {
            // queries the database to see if there is existing matches already stored
            Match? match = await GetExistingMatchAsync(matchId);

            // if not query riots api
            if (match == null)
            {
                var client = getRiotClient();
                var Win = await client.GetAsync($"https://europe.api.riotgames.com/lol/match/v5/matches/{matchId}");

                if (Win.IsSuccessStatusCode)
                {
                    // convert it to match object
                    match = JsonConvert.DeserializeObject<Match>(await Win.Content.ReadAsStringAsync());
                    if(match?.info == null || match.metadata == null)
                    {
                        return null;
                    }

                    // call the save match function to save it to the database if it is new information
                    match.Id = await SaveMatchAsync(match);
                }
            }

            return match;
        }

        private async Task<Match?> GetExistingMatchAsync(string matchId)
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                // open a connection to database
                await conn.OpenAsync();
                // select all matches that have matching gameIDs
                var match = await conn.QueryAsync<Match>("SELECT * FROM Matches WHERE LOWER(GameID) = @gameID", new { gameID = matchId.ToLower() });
                // return what was found.
                return match?.FirstOrDefault();
            }
        }

        private async Task<int> SaveMatchAsync(Match match)
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                var id = await conn.ExecuteScalarAsync<int>(@"INSERT INTO Matches(GameID, GameDuration) VALUES(@gameID, @gameDuration) SELECT SCOPE_IDENTITY()", new
                {
                    gameID = match.metadata.matchId,
                    gameDuration = match.info.gameDuration,
                 });
                return id;

            }
        }

        // ? - warns the compiler that a element could be null, and if it is it'll skip the code path.
        public async Task<Player?> GetPlayerAsync(string summonerName)
        {
            Player player = await GetExistingPlayerAsync(summonerName);

            if (player == null)
            {
                // init reusable client
                var client = getRiotClient();

                // send request to the riot api
                var response = await client.GetAsync($"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/{summonerName}");
                // attempt to deserialise it into a player object
                try
                {
                    // converts the serialied object into a desearialized object, for instance this is converted into a player object.
                    player = JsonConvert.DeserializeObject<Player>(await response.Content.ReadAsStringAsync());

                    player.ID = await SavePlayerAsync(player);
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            return player;
        }

        private async Task<Player?> GetExistingPlayerAsync(string summonerName)
        {
            using(SqlConnection connection = new SqlConnection(SqlConnectionString))
            {
                //open connection
                await connection.OpenAsync();
                // check if the player is already in the database, while also making the string clean - protecting from sql injections
                var player = await connection.QueryAsync<Player>("SELECT * FROM Summoners WHERE LOWER(SummonerName) = @summonerName", new { summonerName = summonerName.ToLower() });
                // will 
                return player?.FirstOrDefault();
            }
        }

        private async Task<int> SavePlayerAsync(Player player)
        {
            using(SqlConnection connection = new SqlConnection(SqlConnectionString))
            {

                // open a new connection to the database
                await connection.OpenAsync();
                // store an int of the id of the inserted data
                var id = await connection.ExecuteScalarAsync<int>(@"INSERT INTO Summoners (SummonerName, PUUID, Summoner_Level) VALUES (@name, @puuid, @summoner_level)
                                                               SELECT SCOPE_IDENTITY()", 
                    new
                    {
                        //fills the parameters with the correct data types
                        name = player.SummonerName,
                        puuid = player.PUUID,
                        summoner_level=player.SummonerLevel,
                    });

                return id;
            }
        }

        // need to write a method that will fill in the summonermatches table, it needs to retrieve each summoner, their kills. the win bool and their deaths
        public async Task PopulateTable(List<Match?> matchList, Player player)
        {
            // needs to get the list of matches from the inputted summoner
            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();
                foreach (Match match in matchList.Where( x => x != null))
                {
                    dynamic check = await connection.QueryAsync<dynamic>(@"SELECT MatchID, SummonerID, Win, Kills, Deaths FROM SummonerMatches WHERE MatchID=@matchID AND SummonerID=@summonerID", new { matchID = match.Id, summonerID = player.ID,});

                    if (check == null || Enumerable.Count(check) == 0)
                    {
                        Participant? participant = match.info?.participants?.FirstOrDefault(x => x.puuid == player.PUUID);
                        // as the method is async it automatically returns a task, meaning i dont need to declare a task in a return statement.
                        await connection.ExecuteAsync(@"INSERT INTO SummonerMatches (MatchID, SummonerID, Win, Kills, Deaths) VALUES (@matchID, @summonerID, @win, @kills, @deaths)",
                                                        new
                                                        {
                                                            matchID = match.Id,
                                                            summonerID = player.ID,
                                                            // ?? is c# equivalent of OR
                                                            win = participant?.win ?? false,
                                                            kills = participant?.kills ?? 0,
                                                            deaths = participant?.deaths ?? 0 ,
                                                        });
                    }
                    else if (Enumerable.Count(check) > 0)
                    {
                        check = check[0];
                        match.Win = check?.Win ?? false;
                        match.Kills = check?.Kills ?? 0;
                        match.Deaths = check?.Deaths ?? 0;
                    }
                }

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
