using NCAA_Data;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace MyNamespace
{
    internal static class PlayerStatsAPI
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Which side of the ball would you like to focus on: offense, defense, or special teams");
            var sideOfBall = Console.ReadLine().ToLower();

            Rushing rushing = new Rushing();
            if (sideOfBall == "offense")
            {
                rushing.GetParameters();
                await rushing.DisplayStatistics();
            }

        }
    }
}