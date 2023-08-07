using System;

namespace NCAA_Data
{
    internal static class PlayerStatsAPI
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Which side of the ball would you like to focus on: offense, defense, or special teams");
            var sideOfBall = Console.ReadLine().ToLower();

            if (sideOfBall == "offense")
            {
                OffenseCategories offenseCategories = new OffenseCategories();
                await offenseCategories.ChooseCategory();
            }
            else if (sideOfBall == "defense")
            {
                Defense defense = new Defense();
                defense.GetParameters();
                await defense.DisplayStatistics();
            }
            else if (sideOfBall == "special teams")
            {
                SpecialTeamsCategories specialTeams = new SpecialTeamsCategories();
                await specialTeams.ChooseCategory();
            }
        }
    }
}