using System;
namespace NCAA_Data
{
	public abstract class Base
	{
        protected string StatCat { get; set; }
        protected int Year { get; set; }
        protected string Conference { get; set; }
        protected string Season { get; set; }
        protected string StatType { get; set; }

        public virtual void GetParameters()
        {
            StatCat = "";

            Console.WriteLine("Which year would you like to see stats for?");
            Year = int.Parse(Console.ReadLine());

            Console.WriteLine("What conference would you like to see a list of players from?");
            Conference = Console.ReadLine();

            Console.WriteLine("Which season would you like to see: regular, postseason, or both?");
            Season = Console.ReadLine();
        }
		public abstract Task DisplayStatistics();
	}
}

