using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadCars
{
    class Race
    {
        public Race()
        {
            Cars = new List<CarTeam>();
            Top = new List<CarTeam>();
            Threads = new List<Thread>();
        }

        private const int TotalDistance = 3340;
        private List<Thread> Threads { get; set; }
        private List<CarTeam> Top { get; set; }
        public List<CarTeam> Cars { get; set; }

        public void StartRace()
        {
            TotalCarInfo();
            ReadySteadyGo();
            StartThreads();
            CarInfoPerSecond();
            ThreadsJoin();
            ShowWinners();
        }

        private void ShowWinners()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"1-st place - {Top[0].Name} [{Top[0].Distance}] Time : {Top[0].Time.Elapsed.TotalSeconds}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"2-st place - {Top[1].Name} [{Top[1].Distance}] Time: { Top[2].Time.Elapsed.TotalSeconds}");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"3-st place - {Top[2].Name} [{Top[2].Distance}] Time : {Top[3].Time.Elapsed.TotalSeconds}");
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 3; i < Top.Count; i++)
            {
                Console.WriteLine($"{i}-st place - {Top[i].Name} [{Top[i].Distance}] Time : {Top[i].Time.Elapsed.TotalSeconds}");
            }
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void Ride(object obj)
        {
            CarTeam car = obj as CarTeam;
            int acceleration = car.MidleSpeed / car.TimeToMiddleSpeed;

            for (; ; )
            {
                if (car.Distance >= TotalDistance)
                {
                    car.Time.Stop();
                    Top.Add(car);
                    break;
                }

                Thread.Sleep(1000);

                if (car.CurrentSpeed >= car.MidleSpeed)
                    car.CurrentSpeed = car.MidleSpeed;
                else
                    car.CurrentSpeed += acceleration;

                car.Distance += acceleration;
            }

            Thread.CurrentThread.Abort();
        }

        private void ThreadsJoin()
        {
            foreach (var thread in Threads)
            {
                thread.Join();
            }
        }

        private void StartThreads()
        {
            foreach (var car in Cars)
            {
                Thread carThread = new Thread(new ParameterizedThreadStart(Ride)) { Name = car.Name };
                Threads.Add(carThread);
            }

            for (int i = 0; i < Threads.Count; i++)
            {
                Threads[i].Start(Cars[i]);
                Cars[i].Time.Start();
            }
        }

        async private void CarInfoPerSecond()
        {
            await Task.Run(()=>CarInfo());
        }

        private void CarInfo()
        {
            for (; ; )
            {
                Thread.Sleep(1000);
                if (Top.Count != Cars.Count)
                {                                        
                    Console.Clear();
                    foreach (var car in Cars)
                    {
                        Console.WriteLine($"Team: { car.Name}, Currentt speed: { car.CurrentSpeed}, Distance: { car.Distance}");
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void ReadySteadyGo()
        {
            Console.WriteLine("\n Press eny key to start race!");
            Console.ReadKey();
            for (int i = 3; i >= 0; i--)
            {
                switch (i)
                {
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }

                Console.Clear();
                Console.WriteLine("===============");

                if (i == 0)
                    Console.WriteLine("======GO!======");
                else
                    Console.WriteLine($"======={i}=======");

                Console.WriteLine("===============");
                Thread.Sleep(1000);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void TotalCarInfo()
        {
            foreach (var car in Cars)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine($"Name: {car.Name}");
                Console.WriteLine($"Middle speed: {car.MidleSpeed}");
                Console.WriteLine($"Time to middle speed: {car.TimeToMiddleSpeed}");
            }
            Console.WriteLine("-------------------");
        }
    }
}
