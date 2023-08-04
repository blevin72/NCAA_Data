using Newtonsoft.Json.Linq;
namespace MyNamespace
{
    internal class PlayerStatsAPI
    {
        static async Task Main(string[] args)
        {
            string key = File.ReadAllText("appsettings.json"); //reads the content (our API Key) from the "appsettings.json file; storing into the variable key

            string APIkey = JObject.Parse(key).GetValue("APIKey").ToString();   /*(1) JObject.Parse:the content of our variable "string key" is parsed into a JObject
                                                                                see 'ExampleResponse.Json' file for example of an object*/

                                                                                //(2) GetValue Method: is extracting the value from the APIKey (in appsettings.Json file)

                                                                                /*(3) Converting the value from our APIKey to a string since our response from our API
                                                                                call is in string format*/

            //bool pickTeam = false;

            Console.WriteLine("Which year would you like to see stats for?");   //receiving user input
            var year = int.Parse(Console.ReadLine());

            Console.WriteLine("What conference would you like to see a list of players from?"); //receiving user input
            var conference = Console.ReadLine();

            //Console.WriteLine("Would you like to choose a specific team?");
            //Console.ReadLine();

            Console.WriteLine("Which season would you like to see: regular, postseason, or both?"); //receiving user input
            var season = Console.ReadLine();

            /*Creating a dictionary for the user to select what type of stat from each category
             user will choose from one of three dictionaries to access nested dictionaries*/
            Dictionary<string, Dictionary<string, List<string>>> statCategories = new Dictionary<string, Dictionary<string, List<string>>>
            {
                {
                    "offense", new Dictionary<string, List<string>>
                    {
                        { "rushing", new List<string> { "YDS", "TD", "CAR", "YPC", "LONG" } },
                        { "passing", new List<string> { "YDS", "TD", "COMPLETIONS", "ATT", "YPA", "INT", "PCT" } },
                        { "receiving", new List<string> { "YDS", "TD", "REC", "YPR", "LONG", } }
                    }
                },
                {
                    "defense", new Dictionary<string, List<string>>
                    {
                        { "tackles", new List<string> { "INT", "TFL", "TOT", "SOLO" } },
                        { "rushing", new List<string> { "QB HUR", "SACKS" } },
                        { "passing", new List<string> { "INT", "YDS", "PD" } }
                    }
                },
                {
                    "special teams", new Dictionary<string, List<string>>
                    {
                        { "kicking", new List<string> { "FGM", "FGA", "XPM", "XPA", "PCT", "PTS", "" } },
                        { "punting", new List<string> { "LONG","IN 20", "YDS", "YPP", "NO"} },
                        { "kick returns", new List<string> {"YDS", "TD", "LONG", "NO"} },
                        { "punt returns", new List<string> {"YDS", "TD", "LONG"} }
                    }
                },
            };

            Console.WriteLine("Choose a main category: offense, defense, or special teams");
            var mainCategory = Console.ReadLine().ToLower();

            if (statCategories.TryGetValue(mainCategory, out Dictionary<string, List<string>> statTypesDict))
            {
                Console.WriteLine("Choose a stat category: " + string.Join(", ", statTypesDict.Keys));
                var statCat = Console.ReadLine().ToLower();

                if (statTypesDict.TryGetValue(statCat, out List<string> statTypes))
                {
                    Console.WriteLine("Choose a stat type: " + string.Join(", ", statTypes));
                    var selectedStatType = Console.ReadLine().ToUpper();

                    var footballURL = $"https://api.collegefootballdata.com/stats/player/" +    //making the API call
                $"season?year={year}&conference={conference}&seasonType={season}&category={statCat}"; //paramters include: year, conference, season, statType

                    Console.WriteLine();

                    // Makes an API call using the 'footballURL' and gets the result as a list of player statistics.
                    List<PlayerStatistics> playerStatsList = await PlayerStats.GetRushingYardsAsync(footballURL);

                    // Display the individual rushing yards for each player.
                    foreach (var playerStats in playerStatsList)
                    {
                        Console.WriteLine($"{playerStats.PlayerName}: {playerStats.RushingYards} {selectedStatType}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid stat category selection.");
                }
            }
            else
            {
                Console.WriteLine("TEST HERE");
            }
        }
    }
}

