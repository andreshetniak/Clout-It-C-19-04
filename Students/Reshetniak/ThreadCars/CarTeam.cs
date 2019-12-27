using System.Diagnostics;

namespace ThreadCars
{
    class CarTeam
    {
        public int Distance { get; set; } = 0;
        public string Name { get; set; }
        public int MidleSpeed { get; set; }
        public int TimeToMiddleSpeed { get; set; } = 3;
        public int CurrentSpeed { get; set; }
        public Stopwatch Time { get; set; } = new Stopwatch();
    }
}
