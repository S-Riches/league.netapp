namespace League.api.Models
{
    public class Match
    {
        public Metadata metadata { get; set; }
        public Info info { get; set; }
    }

    public class Metadata
    {
        public string dataVersion { get; set; }
        public string matchId { get; set; }
        public string[] participants { get; set; }
    }

    public class Info
    {
        public long gameCreation { get; set; }
        public int gameDuration { get; set; }
        public long gameEndTimestamp { get; set; }
        public long gameId { get; set; }
        public string gameMode { get; set; }
        public string gameName { get; set; }
        public long gameStartTimestamp { get; set; }
        public string gameType { get; set; }
        public string gameVersion { get; set; }
        public int mapId { get; set; }
        public Participant[] participants { get; set; }
        public string platformId { get; set; }
        public int queueId { get; set; }
        public Team[] teams { get; set; }
        public string tournamentCode { get; set; }
    }

    public class Participant
    {
        public int assists { get; set; }
        public int baronKills { get; set; }
        public int bountyLevel { get; set; }
        public Challenges challenges { get; set; }
        public int champExperience { get; set; }
        public int champLevel { get; set; }
        public int championId { get; set; }
        public string championName { get; set; }
        public int championTransform { get; set; }
        public int consumablesPurchased { get; set; }
        public int damageDealtToBuildings { get; set; }
        public int damageDealtToObjectives { get; set; }
        public int damageDealtToTurrets { get; set; }
        public int damageSelfMitigated { get; set; }
        public int deaths { get; set; }
        public int detectorWardsPlaced { get; set; }
        public int doubleKills { get; set; }
        public int dragonKills { get; set; }
        public bool firstBloodAssist { get; set; }
        public bool firstBloodKill { get; set; }
        public bool firstTowerAssist { get; set; }
        public bool firstTowerKill { get; set; }
        public bool gameEndedInEarlySurrender { get; set; }
        public bool gameEndedInSurrender { get; set; }
        public int goldEarned { get; set; }
        public int goldSpent { get; set; }
        public string individualPosition { get; set; }
        public int inhibitorKills { get; set; }
        public int inhibitorTakedowns { get; set; }
        public int inhibitorsLost { get; set; }
        public int item0 { get; set; }
        public int item1 { get; set; }
        public int item2 { get; set; }
        public int item3 { get; set; }
        public int item4 { get; set; }
        public int item5 { get; set; }
        public int item6 { get; set; }
        public int itemsPurchased { get; set; }
        public int killingSprees { get; set; }
        public int kills { get; set; }
        public string lane { get; set; }
        public int largestCriticalStrike { get; set; }
        public int largestKillingSpree { get; set; }
        public int largestMultiKill { get; set; }
        public int longestTimeSpentLiving { get; set; }
        public int magicDamageDealt { get; set; }
        public int magicDamageDealtToChampions { get; set; }
        public int magicDamageTaken { get; set; }
        public int neutralMinionsKilled { get; set; }
        public int nexusKills { get; set; }
        public int nexusLost { get; set; }
        public int nexusTakedowns { get; set; }
        public int objectivesStolen { get; set; }
        public int objectivesStolenAssists { get; set; }
        public int participantId { get; set; }
        public int pentaKills { get; set; }
        public Perks perks { get; set; }
        public int physicalDamageDealt { get; set; }
        public int physicalDamageDealtToChampions { get; set; }
        public int physicalDamageTaken { get; set; }
        public int profileIcon { get; set; }
        public string puuid { get; set; }
        public int quadraKills { get; set; }
        public string riotIdName { get; set; }
        public string riotIdTagline { get; set; }
        public string role { get; set; }
        public int sightWardsBoughtInGame { get; set; }
        public int spell1Casts { get; set; }
        public int spell2Casts { get; set; }
        public int spell3Casts { get; set; }
        public int spell4Casts { get; set; }
        public int summoner1Casts { get; set; }
        public int summoner1Id { get; set; }
        public int summoner2Casts { get; set; }
        public int summoner2Id { get; set; }
        public string summonerId { get; set; }
        public int summonerLevel { get; set; }
        public string summonerName { get; set; }
        public bool teamEarlySurrendered { get; set; }
        public int teamId { get; set; }
        public string teamPosition { get; set; }
        public int timeCCingOthers { get; set; }
        public int timePlayed { get; set; }
        public int totalDamageDealt { get; set; }
        public int totalDamageDealtToChampions { get; set; }
        public int totalDamageShieldedOnTeammates { get; set; }
        public int totalDamageTaken { get; set; }
        public int totalHeal { get; set; }
        public int totalHealsOnTeammates { get; set; }
        public int totalMinionsKilled { get; set; }
        public int totalTimeCCDealt { get; set; }
        public int totalTimeSpentDead { get; set; }
        public int totalUnitsHealed { get; set; }
        public int tripleKills { get; set; }
        public int trueDamageDealt { get; set; }
        public int trueDamageDealtToChampions { get; set; }
        public int trueDamageTaken { get; set; }
        public int turretKills { get; set; }
        public int turretTakedowns { get; set; }
        public int turretsLost { get; set; }
        public int unrealKills { get; set; }
        public int visionScore { get; set; }
        public int visionWardsBoughtInGame { get; set; }
        public int wardsKilled { get; set; }
        public int wardsPlaced { get; set; }
        public bool win { get; set; }
    }

    public class Challenges
    {
        public int _12AssistStreakCount { get; set; }
        public int abilityUses { get; set; }
        public int acesBefore15Minutes { get; set; }
        public float alliedJungleMonsterKills { get; set; }
        public int baronTakedowns { get; set; }
        public int blastConeOppositeOpponentCount { get; set; }
        public int bountyGold { get; set; }
        public int buffsStolen { get; set; }
        public int completeSupportQuestInTime { get; set; }
        public float controlWardTimeCoverageInRiverOrEnemyHalf { get; set; }
        public int controlWardsPlaced { get; set; }
        public float damagePerMinute { get; set; }
        public float damageTakenOnTeamPercentage { get; set; }
        public int dancedWithRiftHerald { get; set; }
        public int deathsByEnemyChamps { get; set; }
        public int dodgeSkillShotsSmallWindow { get; set; }
        public int doubleAces { get; set; }
        public int dragonTakedowns { get; set; }
        public float earliestBaron { get; set; }
        public float earliestDragonTakedown { get; set; }
        public float earlyLaningPhaseGoldExpAdvantage { get; set; }
        public float effectiveHealAndShielding { get; set; }
        public int elderDragonKillsWithOpposingSoul { get; set; }
        public int elderDragonMultikills { get; set; }
        public int enemyChampionImmobilizations { get; set; }
        public float enemyJungleMonsterKills { get; set; }
        public int epicMonsterKillsNearEnemyJungler { get; set; }
        public int epicMonsterKillsWithin30SecondsOfSpawn { get; set; }
        public int epicMonsterSteals { get; set; }
        public int epicMonsterStolenWithoutSmite { get; set; }
        public int flawlessAces { get; set; }
        public int fullTeamTakedown { get; set; }
        public float gameLength { get; set; }
        public int getTakedownsInAllLanesEarlyJungleAsLaner { get; set; }
        public float goldPerMinute { get; set; }
        public int hadAfkTeammate { get; set; }
        public int hadOpenNexus { get; set; }
        public int immobilizeAndKillWithAlly { get; set; }
        public int initialBuffCount { get; set; }
        public int initialCrabCount { get; set; }
        public float jungleCsBefore10Minutes { get; set; }
        public int junglerKillsEarlyJungle { get; set; }
        public int junglerTakedownsNearDamagedEpicMonster { get; set; }
        public int kTurretsDestroyedBeforePlatesFall { get; set; }
        public float kda { get; set; }
        public int killAfterHiddenWithAlly { get; set; }
        public float killParticipation { get; set; }
        public int killedChampTookFullTeamDamageSurvived { get; set; }
        public int killsNearEnemyTurret { get; set; }
        public int killsOnLanersEarlyJungleAsJungler { get; set; }
        public int killsOnOtherLanesEarlyJungleAsLaner { get; set; }
        public int killsOnRecentlyHealedByAramPack { get; set; }
        public int killsUnderOwnTurret { get; set; }
        public int killsWithHelpFromEpicMonster { get; set; }
        public int knockEnemyIntoTeamAndKill { get; set; }
        public int landSkillShotsEarlyGame { get; set; }
        public int laneMinionsFirst10Minutes { get; set; }
        public float laningPhaseGoldExpAdvantage { get; set; }
        public int legendaryCount { get; set; }
        public int lostAnInhibitor { get; set; }
        public float maxCsAdvantageOnLaneOpponent { get; set; }
        public int maxKillDeficit { get; set; }
        public int maxLevelLeadLaneOpponent { get; set; }
        public float moreEnemyJungleThanOpponent { get; set; }
        public int multiKillOneSpell { get; set; }
        public int multiTurretRiftHeraldCount { get; set; }
        public int multikills { get; set; }
        public int multikillsAfterAggressiveFlash { get; set; }
        public int mythicItemUsed { get; set; }
        public int outerTurretExecutesBefore10Minutes { get; set; }
        public int outnumberedKills { get; set; }
        public int outnumberedNexusKill { get; set; }
        public int perfectDragonSoulsTaken { get; set; }
        public int perfectGame { get; set; }
        public int pickKillWithAlly { get; set; }
        public int poroExplosions { get; set; }
        public int quickCleanse { get; set; }
        public int quickFirstTurret { get; set; }
        public int quickSoloKills { get; set; }
        public int riftHeraldTakedowns { get; set; }
        public int saveAllyFromDeath { get; set; }
        public int scuttleCrabKills { get; set; }
        public int skillshotsDodged { get; set; }
        public int skillshotsHit { get; set; }
        public int snowballsHit { get; set; }
        public int soloBaronKills { get; set; }
        public int soloKills { get; set; }
        public int soloTurretsLategame { get; set; }
        public int stealthWardsPlaced { get; set; }
        public int survivedSingleDigitHpCount { get; set; }
        public int survivedThreeImmobilizesInFight { get; set; }
        public int takedownOnFirstTurret { get; set; }
        public int takedowns { get; set; }
        public int takedownsAfterGainingLevelAdvantage { get; set; }
        public int takedownsBeforeJungleMinionSpawn { get; set; }
        public int takedownsFirst25Minutes { get; set; }
        public int takedownsInAlcove { get; set; }
        public int takedownsInEnemyFountain { get; set; }
        public int teamBaronKills { get; set; }
        public float teamDamagePercentage { get; set; }
        public int teamElderDragonKills { get; set; }
        public int teamRiftHeraldKills { get; set; }
        public int threeWardsOneSweeperCount { get; set; }
        public int tookLargeDamageSurvived { get; set; }
        public int turretPlatesTaken { get; set; }
        public int turretTakedowns { get; set; }
        public int turretsTakenWithRiftHerald { get; set; }
        public int twentyMinionsIn3SecondsCount { get; set; }
        public int unseenRecalls { get; set; }
        public float visionScoreAdvantageLaneOpponent { get; set; }
        public float visionScorePerMinute { get; set; }
        public int wardTakedowns { get; set; }
        public int wardTakedownsBefore20M { get; set; }
        public int wardsGuarded { get; set; }
        public float fastestLegendary { get; set; }
        public int highestChampionDamage { get; set; }
        public float shortestTimeToAceFromFirstTakedown { get; set; }
        public int teleportTakedowns { get; set; }
        public float earliestElderDragon { get; set; }
        public float firstTurretKilledTime { get; set; }
        public int highestCrowdControlScore { get; set; }
        public int fasterSupportQuestCompletion { get; set; }
        public int highestWardKills { get; set; }
    }

    public class Perks
    {
        public Statperks statPerks { get; set; }
        public Style[] styles { get; set; }
    }

    public class Statperks
    {
        public int defense { get; set; }
        public int flex { get; set; }
        public int offense { get; set; }
    }

    public class Style
    {
        public string description { get; set; }
        public Selection[] selections { get; set; }
        public int style { get; set; }
    }

    public class Selection
    {
        public int perk { get; set; }
        public int var1 { get; set; }
        public int var2 { get; set; }
        public int var3 { get; set; }
    }

    public class Team
    {
        public Ban[] bans { get; set; }
        public Objectives objectives { get; set; }
        public int teamId { get; set; }
        public bool win { get; set; }
    }

    public class Objectives
    {
        public Baron baron { get; set; }
        public Champion champion { get; set; }
        public Dragon dragon { get; set; }
        public Inhibitor inhibitor { get; set; }
        public Riftherald riftHerald { get; set; }
        public Tower tower { get; set; }
    }

    public class Baron
    {
        public bool first { get; set; }
        public int kills { get; set; }
    }

    public class Champion
    {
        public bool first { get; set; }
        public int kills { get; set; }
    }

    public class Dragon
    {
        public bool first { get; set; }
        public int kills { get; set; }
    }

    public class Inhibitor
    {
        public bool first { get; set; }
        public int kills { get; set; }
    }

    public class Riftherald
    {
        public bool first { get; set; }
        public int kills { get; set; }
    }

    public class Tower
    {
        public bool first { get; set; }
        public int kills { get; set; }
    }

    public class Ban
    {
        public int championId { get; set; }
        public int pickTurn { get; set; }
    }
}