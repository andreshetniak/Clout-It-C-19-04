using System;

namespace ThreadCars
{
    class Program
    {

        static void Main(string[] args)
        {
            Random rnd = new Random();
            Race race = new Race();

            race.Cars.Add(new CarTeam() { Name = "Red Bull", MidleSpeed = rnd.Next(250, 270) });
            race.Cars.Add(new CarTeam() { Name = "Brawn GP", MidleSpeed = rnd.Next(250, 270) });
            race.Cars.Add(new CarTeam() { Name = "McLaren", MidleSpeed = rnd.Next(250, 270) });
            race.Cars.Add(new CarTeam() { Name = "Mercedes", MidleSpeed = rnd.Next(250, 270) });
            race.Cars.Add(new CarTeam() { Name = "Renault", MidleSpeed = rnd.Next(250, 270) });
            race.Cars.Add(new CarTeam() { Name = "Williams", MidleSpeed = rnd.Next(250, 270) });
            race.Cars.Add(new CarTeam() { Name = "BMW", MidleSpeed = rnd.Next(250, 270) });
            race.Cars.Add(new CarTeam() { Name = "Ferrari", MidleSpeed = rnd.Next(250, 270) });

            race.StartRace();
        }
    }
}
