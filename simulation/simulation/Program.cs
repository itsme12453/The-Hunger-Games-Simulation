using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace solution
{
    class Program
    {
        private static Random Random = new Random();

        public static int RandomDelay(int speed)
        {
            switch (speed)
            {
                case 1:
                    return Random.Next(1, 5);
                case 2:
                    return Random.Next(100, 301);
                case 3:
                    return Random.Next(300, 801);
                default:
                    return Random.Next(300, 801);
            }
        }

        public class PhysicalStats
        {
            public double Height { get; set; }
            public double Mass { get; set; }
            public double Reach { get; set; }
            public int Exercise { get; set; }
            public float BMI { get; set; }
        }

        public class AppearanceStats
        {
            public int Attractiveness { get; set; }
            public int JawlineSharpness { get; set; }
            public int AcneLevel { get; set; }
            public bool Glasses { get; set; }
            public int HairLine { get; set; }
        }

        public class AgeStats
        {
            public DateTime DOB { get; set; }
        }

        public class HealthStats
        {
            public double Hydration { get; set; }
            public double Nutrition { get; set; }
            public double Sleep { get; set; }
            public double Energy { get; set; }
            public double Health { get; set; }
        }

        public class CombatStats
        {
            public double Strength { get; set; }
            public double Power { get; set; }
            public double Speed { get; set; }
            public double Agility { get; set; }
            public double Stamina { get; set; }

            public CombatStats()
            {
                Strength = Random.Next(10, 51);
                Power = Random.Next(10, 51);
                Speed = Random.Next(10, 51);
                Agility = Random.Next(10, 51);
                Stamina = Random.Next(10, 51);
            }

            public double Fitness()
            {
                return (Strength + Power + Speed + Agility + Stamina) / 5;
            }

            public string GetCombatStats()
            {
                return $"Strength: {Strength}, Power: {Power}, Speed: {Speed}, Agility: {Agility}, Stamina: {Stamina}";
            }
        }

        public class Person
        {
            public string Name { get; set; }
            public PhysicalStats Physical { get; set; }
            public AppearanceStats Appearance { get; set; }
            public AgeStats Age { get; set; }
            public HealthStats Health { get; set; }
            public int Willpower { get; set; }
            public CombatStats Combat { get; set; }
            public bool Dead { get; set; }
            public int DeathTime { get; set; }

            public Person(
                string name,
                double height,
                double mass,
                double reach,
                int exercise,
                int attractiveness,
                int jawlineSharpness,
                int acneLevel,
                bool glasses,
                int hairLine,
                DateTime dob,
                int willpower = -1,
                CombatStats combatStats = null)
            {
                Name = name;
                Physical = new PhysicalStats
                {
                    Height = height,
                    Mass = mass,
                    Reach = reach,
                    Exercise = exercise
                };

                Physical.BMI = (float)Math.Round(mass / Math.Pow(height / 100, 2), 1);

                Appearance = new AppearanceStats
                {
                    Attractiveness = attractiveness,
                    JawlineSharpness = jawlineSharpness,
                    AcneLevel = acneLevel,
                    Glasses = glasses,
                    HairLine = hairLine
                };

                Age = new AgeStats { DOB = dob };

                Health = new HealthStats
                {
                    Hydration = 100,
                    Nutrition = 100,
                    Sleep = 8,
                    Energy = 100,
                    Health = 100
                };

                Willpower = willpower == -1 ? Random.Next(1, 101) : willpower;

                Combat = combatStats ?? new CombatStats();

                Dead = false; // Initialize as False
                DeathTime = -1;
            }

            public void Eat(double calories)
            {
                if (Dead) return;

                if (Health.Nutrition < 50)
                {
                    double previousNutrition = Math.Round(Health.Nutrition, 1);
                    Health.Nutrition += calories * 0.1;
                    Health.Energy += calories * 0.05;
                    Health.Nutrition = Math.Min(100, Health.Nutrition);
                    Health.Energy = Math.Min(100, Health.Energy);

                    double newNutrition = Math.Round(Health.Nutrition, 1);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{Name} ate and increased nutrition from {previousNutrition} to {newNutrition}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            public void Drink(double amount)
            {
                if (Dead) return;

                if (Health.Hydration < 50)
                {
                    double previousHydration = Math.Round(Health.Hydration, 1);
                    Health.Hydration += amount;
                    Health.Hydration = Math.Min(100, Health.Hydration);

                    double newHydration = Math.Round(Health.Hydration, 1);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{Name} drank and increased hydration from {previousHydration} to {newHydration}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            public string Run(double distance)
            {
                if (Dead) return "";

                double staminaCost = Math.Ceiling(distance * 1.5);
                Combat.Stamina = Math.Max(0, Combat.Stamina - staminaCost);

                // Slightly increase max stamina for running
                Combat.Stamina = Math.Min(100, Combat.Stamina + 0.5);

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                return $"{Name} ran a distance of {distance} units and lost {staminaCost} stamina.";
            }

            public void Fight(Person opponent)
            {
                if (Dead || opponent.Dead) return;

                Console.WriteLine($"Fight between {Name} and {opponent.Name} begins!");

                Console.WriteLine($"{Name}: Health = {Health.Health}, Stamina = {Combat.Stamina}, Strength = {Combat.Strength}, Agility = {Combat.Agility}");
                Console.WriteLine($"{opponent.Name}: Health = {opponent.Health.Health}, Stamina = {opponent.Combat.Stamina}, Strength = {opponent.Combat.Strength}, Agility = {opponent.Combat.Agility}");

                int round = 1;
                while (round <= 5 && Health.Health > 0 && opponent.Health.Health > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Round {round}:");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;

                    // Randomly determine who attacks first
                    Person attacker, defender;
                    if (Random.Next(0, 2) == 0)
                    {
                        attacker = this;
                        defender = opponent;
                    }
                    else
                    {
                        attacker = opponent;
                        defender = this;
                    }

                    int baseDamage = Random.Next(10, 21);
                    double fatigueFactor = 1.5 - (defender.Combat.Stamina / 100);
                    int adjustedDamage = (int)(baseDamage * fatigueFactor);
                    defender.Health.Health -= adjustedDamage;

                    Console.WriteLine($"    {attacker.Name} attacks {defender.Name} with a punch, causing {adjustedDamage} damage.");
                    Console.WriteLine($"    {defender.Name}'s health is now {defender.Health.Health}.");

                    int staminaLoss = Random.Next(5, 11);
                    defender.Combat.Stamina = Math.Max(0, defender.Combat.Stamina - staminaLoss);
                    Console.WriteLine($"    {defender.Name} lost {staminaLoss} stamina due to exhaustion.");

                    defender.Combat.Agility = Math.Max(0, defender.Combat.Agility - Random.Next(1, 3));
                    defender.Combat.Power = Math.Max(0, defender.Combat.Power - Random.Next(1, 3));

                    if (defender.Health.Health <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{defender.Name} is defeated!");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                    round++;
                }

                Console.WriteLine("Fight over.");
            }

            public double CalculateDecrease(double stat, double baseDecrease, double willpowerMultiplier)
            {
                double randomFactor = Random.NextDouble() * baseDecrease;
                double severityMultiplier = stat < 50 ? 1.5 : 1.0;
                return randomFactor * severityMultiplier * willpowerMultiplier;
            }

            public void PassHour()
            {
                if (Dead) return;

                double hydrationDecrease = CalculateDecrease(Health.Hydration, 3, WillpowerInfluence());
                double nutritionDecrease = CalculateDecrease(Health.Nutrition, 3, WillpowerInfluence());
                double sleepDecrease = CalculateDecrease(Health.Sleep, 2, WillpowerInfluence());
                double energyDecrease = CalculateDecrease(Health.Energy, 3, WillpowerInfluence());
                double healthIncrease = 1; // Slow healing over time

                Health.Hydration = Math.Max(0, Health.Hydration - hydrationDecrease);
                Health.Nutrition = Math.Max(0, Health.Nutrition - nutritionDecrease);
                Health.Sleep = Math.Max(0, Health.Sleep - sleepDecrease);
                Health.Energy = Math.Max(0, Health.Energy - energyDecrease);
                Health.Health = Math.Min(100, Health.Health + healthIncrease);

                if (Health.Nutrition <= 0 || Health.Hydration <= 0)
                {
                    Dead = true;
                }
            }

            public string GetStats()
            {
                double roundedHydration = Math.Round(Health.Hydration, 1);
                double roundedNutrition = Math.Round(Health.Nutrition, 1);
                double roundedSleep = Math.Round(Health.Sleep, 1);
                double roundedEnergy = Math.Round(Health.Energy, 1);
                double roundedWillpower = Math.Round((double)Willpower, 1);

                double combatFitness = Math.Round(Combat.Fitness(), 1);

                return $"{Name} | Hydration: {roundedHydration} | Nutrition: {roundedNutrition} | Sleep: {roundedSleep} | Energy: {roundedEnergy} | Willpower: {roundedWillpower} | Fitness: {combatFitness}";
            }

            public double WillpowerInfluence()
            {
                return 1 - ((Willpower - 50) / 100);
            }
        }

        public class Simulation
        {
            private static Random Random = new Random();

            public static void AdjustNutritionAfterTraining(List<Person> people)
            {
                foreach (var person in people)
                {
                    if (person.Health.Nutrition == 0)
                    {
                        int addedNutrition = Random.Next(30, 61);
                        person.Health.Nutrition = addedNutrition;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{person.Name}'s nutrition was adjusted to {addedNutrition} after training.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }

            public static Person GenerateRandomPerson(int personNum)
            {
                string name = $"Person {personNum}";
                double height = Random.Next(150, 200);
                double mass = Random.Next(50, 100);
                double reach = Random.Next(50, 100);
                int exercise = Random.Next(0, 10);
                int attractiveness = Random.Next(1, 11);
                int jawlineSharpness = Random.Next(1, 11);
                int acneLevel = Random.Next(1, 11);
                bool glasses = Random.Next(0, 2) == 1;
                int hairLine = Random.Next(1, 11);
                DateTime dob = new DateTime(Random.Next(1980, 2005), Random.Next(1, 13), Random.Next(1, 29));
                int willpower = Random.Next(1, 101);

                CombatStats combatStats = new CombatStats();

                return new Person(name, height, mass, reach, exercise, attractiveness, jawlineSharpness, acneLevel, glasses, hairLine, dob, willpower, combatStats);
            }

            public static void TrainingArc(List<Person> people)
            {
                var initialCombatStats = new Dictionary<string, string>();

                foreach (var person in people)
                {
                    if (!initialCombatStats.ContainsKey(person.Name))
                    {
                        initialCombatStats[person.Name] = person.Combat.GetCombatStats();
                    }
                }

                for (int i = 1; i <= 48; i++)
                {
                    foreach (var person in people)
                    {
                        person.Combat.Strength = Math.Min(100, person.Combat.Strength + Random.Next(1, 3));
                        person.Combat.Agility = Math.Min(100, person.Combat.Agility + Random.Next(1, 3));
                        person.Combat.Stamina = Math.Min(100, person.Combat.Stamina + Random.Next(1, 3));

                        person.PassHour();
                    }
                }

                Console.WriteLine("Training Results:");
                foreach (var person in people)
                {
                    if (initialCombatStats.ContainsKey(person.Name))
                    {
                        string initialStat = initialCombatStats[person.Name];
                        string finalStat = person.Combat.GetCombatStats();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Initial Stats for {person.Name}: {initialStat}, Nutrition: {Math.Round(person.Health.Nutrition)}");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Final Stats for {person.Name}: {finalStat}, Nutrition: {Math.Round(person.Health.Nutrition)}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                people.Sort((p1, p2) => p2.Combat.Fitness().CompareTo(p1.Combat.Fitness()));

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Top 5 Best Performing:");
                for (int i = 0; i < Math.Min(5, people.Count); i++)
                {
                    var person = people[i];
                    Console.WriteLine($"    {person.Name}: {person.Combat.GetCombatStats()}, Willpower: {Math.Round((double)person.Willpower)}");
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bottom 5 Worst Performing:");
                for (int i = people.Count - 1; i >= Math.Max(people.Count - 5, 0); i--)
                {
                    var person = people[i];
                    Console.WriteLine($"    {person.Name}: {person.Combat.GetCombatStats()}, Willpower: {Math.Round((double)person.Willpower)}");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }

            public static void HungerGames(int populationCount, int speed)
            {
                Console.WriteLine("Creating Population...");
                var people = new List<Person>();
                var deadPeople = new List<Person>();
                int timeElapsed = 0;

                for (int i = 1; i <= populationCount; i++)
                {
                    people.Add(GenerateRandomPerson(i));
                }

                Console.WriteLine("Population Created");

                Console.WriteLine("Training in progress...");
                TrainingArc(people);
                Console.WriteLine("Training complete!");

                Console.WriteLine("Providing food to hungry...");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                AdjustNutritionAfterTraining(people);
                Console.WriteLine("Food Provided!");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine();
                Console.WriteLine("Let the");
                Console.WriteLine("██╗░░██╗██╗░░░██╗███╗░░██╗░██████╗░███████╗██████╗░  ░██████╗░░█████╗░███╗░░░███╗███████╗░██████╗");
                Console.WriteLine("██║░░██║██║░░░██║████╗░██║██╔════╝░██╔════╝██╔══██╗  ██╔════╝░██╔══██╗████╗░████║██╔════╝██╔════╝");
                Console.WriteLine("███████║██║░░░██║██╔██╗██║██║░░██╗░█████╗░░██████╔╝  ██║░░██╗░███████║██╔████╔██║█████╗░░╚█████╗░");
                Console.WriteLine("██╔══██║██║░░░██║██║╚████║██║░░╚██╗██╔══╝░░██╔══██╗  ██║░░╚██╗██╔══██║██║╚██╔╝██║██╔══╝░░░╚═══██╗");
                Console.WriteLine("██║░░██║╚██████╔╝██║░╚███║╚██████╔╝███████╗██║░░██║  ╚██████╔╝██║░░██║██║░╚═╝░██║███████╗██████╔╝");
                Console.WriteLine("╚═╝░░╚═╝░╚═════╝░╚═╝░░╚══╝░╚═════╝░╚══════╝╚═╝░░╚═╝  ░╚═════╝░╚═╝░░╚═╝╚═╝░░░░░╚═╝╚══════╝╚═════╝░");
                Console.WriteLine("BEGIN!");

                while (people.Count > 1)
                {
                    timeElapsed++;

                    foreach (var participant in people.ToList())
                    {
                        if (participant.Dead)
                        {
                            if (participant.DeathTime == -1)
                            {
                                participant.DeathTime = timeElapsed;
                            }
                            if (!deadPeople.Contains(participant))
                            {
                                deadPeople.Add(participant);
                            }
                            people.Remove(participant);
                            continue;
                        }

                        if (participant.Health.Nutrition <= 0 || participant.Health.Hydration <= 0)
                        {
                            participant.Dead = true;
                            if (participant.DeathTime == -1)
                            {
                                participant.DeathTime = timeElapsed;
                            }
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{participant.Name} died due to critical health.");
                            Console.ForegroundColor = ConsoleColor.White;
                            if (!deadPeople.Contains(participant))
                            {
                                deadPeople.Add(participant);
                            }
                            people.Remove(participant);
                            continue;
                        }

                        if (participant.Health.Nutrition < 50 && Random.Next(0, 2) == 0)
                        {
                            participant.Eat(Random.Next(500, 1500));
                        }
                        else if (Random.Next(0, 5) == 0)
                        {
                            participant.Eat(Random.Next(500, 1500));
                        }

                        if (participant.Health.Hydration < 50 && Random.Next(0, 2) == 0)
                        {
                            participant.Drink(Random.Next(20, 100));
                        }
                        else if (Random.Next(0, 5) == 0)
                        {
                            participant.Drink(Random.Next(20, 100));
                        }

                        if (Random.Next(0, 4) == 0)
                        {
                            int opponentIndex = Random.Next(0, people.Count);
                            var opponent = people[opponentIndex];

                            if (!opponent.Dead && opponent.Name != participant.Name)
                            {
                                if (Random.Next(0, 2) == 0)
                                {
                                    var distance = Random.Next(1, 10);
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine(participant.Run(distance));
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                else
                                {
                                    participant.Fight(opponent);

                                    if (opponent.Health.Health <= 0)
                                    {
                                        opponent.Dead = true;
                                        if (opponent.DeathTime == -1)
                                        {
                                            opponent.DeathTime = timeElapsed;
                                        }
                                        if (!deadPeople.Contains(opponent))
                                        {
                                            deadPeople.Add(opponent);
                                        }
                                        people.Remove(opponent);
                                    }
                                }
                            }
                        }

                        participant.PassHour();
                        Thread.Sleep(RandomDelay(speed));
                    }
                }

                if (people.Count == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"The winner is {people[0].Name}");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                deadPeople.Sort((a, b) => a.DeathTime.CompareTo(b.DeathTime));

                Console.WriteLine("Leaderboard:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"#1: {people[0].Name} | DID NOT DIE");
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < deadPeople.Count; i++)
                {
                    var person = deadPeople[deadPeople.Count - i - 1];
                    Console.WriteLine($"#{i + 2}: {person.Name} | Survival Time: {person.DeathTime}");
                }
            }
        }

        static void Main(string[] args)
        {
            int speed, peopleCount;

            do
            {
                Console.WriteLine("How fast do you want the simulation to run?");
                Console.WriteLine("1. Instant");
                Console.WriteLine("2. Fast");
                Console.WriteLine("3. Medium");

                if (int.TryParse(Console.ReadLine(), out speed) && (speed >= 1 && speed <= 3))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                }
            } while (true);

            do
            {
                Console.WriteLine("How big do you want the population to be?");
                if (int.TryParse(Console.ReadLine(), out peopleCount) && peopleCount > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid number. Please enter a valid positive integer.");
                }
            } while (true);

            Simulation.HungerGames(peopleCount, speed);

            Console.ReadLine();
        }
    }
}
