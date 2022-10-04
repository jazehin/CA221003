namespace CA221003
{
    internal class Program
    {
        static Random rnd = new();
        static List<Fish> fishList = new();
        static void Main(string[] args)
        {
            ListaFeltoltes();
            ListaKiiras();

            RagadozokSzama();
            LegnagyobbSuly();
            KepesMelybenUszni();
            Emulacio();
            //ListaKiiras();
        }

        private static void Emulacio()
        {
            int numberOfEatenFish = 0;
            float weightOfEatenFish = 0;

            for (int i = 0; i < 100; i++)
            {
                int x = rnd.Next(fishList.Count);
                int y = rnd.Next(fishList.Count);

                while (x == y) y = rnd.Next(fishList.Count);

                if (fishList[x].Predator != fishList[y].Predator)
                {
                    Fish predator = fishList[x].Predator ? fishList[x] : fishList[y];
                    Fish herbivore = !fishList[x].Predator ? fishList[x] : fishList[y];

                    if (predator.SwimTop <= herbivore.SwimBottom && predator.SwimBottom >= herbivore.SwimTop)
                    {
                        if (rnd.Next(100) < 30)
                        {
                            if (predator.Weight * 1.09f < 40)
                            {
                                predator.Weight *= 1.09f;
                            }
                            else
                            {
                                predator.Weight = 40f;
                            }
                            numberOfEatenFish++;
                            weightOfEatenFish += herbivore.Weight;
                            fishList.Remove(herbivore);
                        }
                    }
                }
            }

            Console.WriteLine(numberOfEatenFish + " halat ettek meg.");
            Console.WriteLine("Tömegük összesen: " + weightOfEatenFish + " kg");
        }

        private static void KepesMelybenUszni()
        {
            float depthInMeters = 1.1f;

            int canSwimThere = fishList
                .Count(f => f.CanSwimAt(depthInMeters));

            Console.WriteLine($"{canSwimThere} db hal tud {depthInMeters:0.0} méteren úszni.");
        }

        private static void RagadozokSzama()
        {
            int numberOfPredators = fishList
                .Count(f => f.Predator);

            Console.WriteLine($"\n\nÖsszesen {numberOfPredators} db ragadozó van a 100 hal között.");
        }

        private static void LegnagyobbSuly()
        {
            float heaviestFishWeight = fishList
                .Max(f => f.Weight);

            Console.WriteLine($"Legnagyobb tömegű hal súlya: {heaviestFishWeight,5:0.00} kg");
        }

        private static void ListaKiiras()
        {
            Console.WriteLine("fajta\t\téletmód\t\tsúly\t\túszási sáv");
            Console.WriteLine(new string('-', 63));
            foreach (Fish fish in fishList)
            {
                HalKiir(fish);
            }
            Console.ResetColor();
        }

        private static void HalKiir(Fish fish)
        {
            Console.ForegroundColor = fish.Predator ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine(fish.ToString);
        }

        private static void ListaFeltoltes()
        {
            string[] speciesNames = { "ponty", "harcsa", "keszeg", "kárász", "aranyhal", "busa", "kráken" };
            for (int i = 0; i < 100; i++)
            {
                fishList.Add(new Fish(
                    predator: rnd.Next(100) >= 90,
                    weight: (float)rnd.Next(1, 81) / 2,
                    swimTop: rnd.Next(401),
                    swimDepth: rnd.Next(10, 401),
                    species: speciesNames[rnd.Next(speciesNames.Length)]
                ));

            }
        }
    }
}