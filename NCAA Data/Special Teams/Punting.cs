using System;
using Newtonsoft.Json.Linq;

namespace NCAA_Data
{
	public class Punting : Base
	{
        private List<PlayerStatistics> filteredStats; // Declare filteredStats as a class-level variable

        public override void GetParameters()
        {
            StatCat = "punting";
            base.GetParameters();

            Console.WriteLine("Which punting stat would you like to see: LONG, In 20, YDS, YPP, NO");
            StatType = Console.ReadLine();
        }

        public override async Task DisplayStatistics()
        {
            string selectedCategory = "punting";
            string key = File.ReadAllText("appsettings.json"); //reads the content (our API Key) from the "appsettings.json file; storing into the variable key

            string APIkey = JObject.Parse(key).GetValue("APIKey").ToString();

            var footballURL = $"https://api.collegefootballdata.com/stats/player/" +    //making the API call
            $"season?year={Year}&conference={Conference}&seasonType={Season}&category={StatCat}";

            List<PlayerStatistics> playerStatsList = await PlayerStats.GetPlayerStats(footballURL);

            filteredStats = playerStatsList.Where(ps => ps.StatType.ToLower() == StatType.ToLower()).ToList();

            if (filteredStats.Count == 0)
            {
                Console.WriteLine("No statistics found for the selected stat type.");
                return; // Exit the method if no statistics are found
            }

            Console.WriteLine("Sort order: Enter 'asc' for least to greatest or 'desc' for greatest to least");
            string sortOrder = Console.ReadLine().ToLower();

            if (sortOrder == "asc")
            {
                filteredStats.Sort((ps1, ps2) => int.Parse(ps1.Stat).CompareTo(int.Parse(ps2.Stat)));
            }
            else if (sortOrder == "desc")
            {
                filteredStats.Sort((ps1, ps2) => int.Parse(ps2.Stat).CompareTo(int.Parse(ps1.Stat)));
            }
            else
            {
                Console.WriteLine("Invalid sort order. Please enter 'asc' for least to greatest or 'desc' for greatest to least.");
                return; // Exit the method if the sort order is invalid
            }
            foreach (PlayerStatistics playerStats in filteredStats)
            {
                if (playerStats.Category.ToLower() == selectedCategory)
                {
                    Console.WriteLine($"Player: {playerStats.PlayerName}");
                    Console.WriteLine($"Team: {playerStats.Team}");
                    Console.WriteLine($"Conference: {playerStats.Conference}");
                    Console.WriteLine($"Category: {playerStats.Category}");
                    Console.WriteLine($"Stat Type: {playerStats.StatType}");
                    Console.WriteLine($"Result: {playerStats.Stat} {StatType}");
                    Console.WriteLine("------------------------------------------");
                }
            }
        }
    }
}