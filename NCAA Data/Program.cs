using Newtonsoft.Json.Linq;
namespace NCAA_Data
{
    internal class PlayerStatsAPI
    {
        static void Main(string[] args)
        {
            ByConference byConference = new ByConference();
            byConference.SearchByConference();
        }
    }
}

