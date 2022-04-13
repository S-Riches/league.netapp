using League.api.Services;
using Microsoft.AspNetCore.Mvc;


// maps via program.cs
namespace League.api.Controllers
{
    // explicitly map the prefix of the url
    [Route("Riot")]
    public class RiotController : Controller
    {
        
        private readonly IRiotService riotService;
        private readonly ILogger<RiotController> logger;
        

        public RiotController(IRiotService riotService, ILogger<RiotController> logger)
        {
            // constructor
            this.logger = logger;
            this.riotService = riotService;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("Player")]
        public async Task<IActionResult> Player([FromQuery] string summonerName) {
            try
            {
                
                // awaits a response from the get player async function
                var DeserialisedResponse = await riotService.GetPlayerAsync(summonerName);
                // initialises a new SQL interactions class
                var sql = new SqlInteractions();
                string puuid = DeserialisedResponse.PUUID;
                int summonerLevel = (int)DeserialisedResponse.SummonerLevel;
                // calls the method and gives it access to the logger
                sql.SendSummoner(summonerName, puuid, summonerLevel, logger);
                // returns the value if all is gucii
                return Ok(DeserialisedResponse);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("Matches")]
        public async Task<IActionResult> ShowMatchHistory([FromQuery] string puuid)
        {
            try
            {
                var output = await riotService.GetMatchesAsync(puuid);
                var sql = new SqlInteractions();
                // just realised that ill need to loop through each of the inputs for this one.
                int GameID = output.Selec
                int GameDuration = 
                sql.SendMatch(GameID, GameDuration, logger);
                return Ok(output.Select(item => item.info));
            }
            catch
            {
                return BadRequest();
            }
        }

    }
        
}

// -- end of code here just wanted to remember some of these notes
        /*
        // ping these once this works to see the difference
        // async will allow for multiple queries 
        // non async SHOULD only allow for one user at a time, however net 6 handles this
        [HttpGet]
        [Route("NonAsync")]
        public IActionResult NonAsync()
        {
            logger.LogInformation("Starting");
            Thread.Sleep(10000);
            logger.LogInformation("Finished");
            return Ok();
        }

        [HttpGet]
        [Route("Async")]
        public async Task <IActionResult> Async()
        {
            logger.LogInformation("Starting");
            await Task.Delay(10000);
            logger.LogInformation("Finished");
            return Ok();
        }
        */

  