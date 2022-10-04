namespace CA221003
{
    internal class Program
    {
        static Random rnd = new();
        static List<Fish> fishList = new();
        static void Main(string[] args)
        {
            ListaFeltoltes();
            ListaKiiras(fishList);

            RagadozokSzama();
            LegnagyobbSuly();
            KepesMelybenUszni();
            Emulacio();
            EmulacioAmigNemMaradNovenyevo();
            //ListaKiiras();
        }

        private static void EmulacioAmigNemMaradNovenyevo()
        {
            List<Fish> eatenFish = new();

            using StreamWriter sw = new(@"..\..\..\res\log.txt");

            int i = 0;
            while (fishList.Count(f => !f.Predator) > 0)
            {
                sw.WriteLine($"{i + 1}. iteráció:");

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
                            sw.WriteLine("Táplálkozás történik:");
                            sw.WriteLine(predator.ToString);
                            sw.WriteLine(herbivore.ToString);

                            float weightGain = predator.Weight;

                            if (predator.Weight * 1.09f < 40)
                            {
                                predator.Weight *= 1.09f;
                            }
                            else
                            {
                                predator.Weight = 40f;
                            }

                            weightGain = Math.Abs(weightGain - predator.Weight);
                            sw.WriteLine($"A növényevő {herbivore.Species} elpusztul. A ragadozó {predator.Species} {weightGain:0.00} kg-t hízott.");
                            eatenFish.Add(new Fish(herbivore));
                            fishList.Remove(herbivore);
                        }
                        else
                        {
                            sw.WriteLine("A ragadozó úgy dönt, hogy nem éhes.");
                            sw.WriteLine(predator.ToString);
                            sw.WriteLine(herbivore.ToString);
                        }
                    }
                    else
                    {
                        sw.WriteLine("A ragadozó nem tud beúszni a növényevő sávjába:");
                        sw.WriteLine(predator.ToString);
                        sw.WriteLine(herbivore.ToString);
                    }
                }
                else
                {
                    sw.WriteLine("A két hal életmódja megegyezik.");
                    sw.WriteLine(fishList[x].ToString);
                    sw.WriteLine(fishList[y].ToString);
                }

                sw.WriteLine("--------------------");
                i++;
            }
        }

        private static void Emulacio()
        {
            List<Fish> eatenFish = new();

            using StreamWriter sw = new(@"..\..\..\res\log.txt");

            for (int i = 0; i < 100; i++)
            {
                sw.WriteLine($"{i+1}. iteráció:");

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
                            sw.WriteLine("Táplálkozás történik:");
                            sw.WriteLine(predator.ToString);
                            sw.WriteLine(herbivore.ToString);

                            float weightGain = predator.Weight;

                            if (predator.Weight * 1.09f < 40)
                            {
                                predator.Weight *= 1.09f;
                            }
                            else
                            {
                                predator.Weight = 40f;
                            }

                            weightGain = Math.Abs(weightGain - predator.Weight);
                            sw.WriteLine($"A növényevő {herbivore.Species} elpusztul. A ragadozó {predator.Species} {weightGain:0.00} kg-t hízott.");
                            eatenFish.Add(new Fish(herbivore));
                            fishList.Remove(herbivore);
                        }
                        else
                        {
                            sw.WriteLine("A ragadozó úgy dönt, hogy nem éhes.");
                            sw.WriteLine(predator.ToString);
                            sw.WriteLine(herbivore.ToString);
                        }
                    }
                    else
                    {
                        sw.WriteLine("A ragadozó nem tud beúszni a növényevő sávjába:");
                        sw.WriteLine(predator.ToString);
                        sw.WriteLine(herbivore.ToString);
                    }
                }
                else
                {
                    sw.WriteLine("A két hal életmódja megegyezik.");
                    sw.WriteLine(fishList[x].ToString);
                    sw.WriteLine(fishList[y].ToString);
                }

                sw.WriteLine("--------------------");
            }

            /*
            Console.WriteLine(eatenFish.Count + " halat ettek meg.");
            Console.WriteLine("Tömegük összesen: " + eatenFish.Sum(f => f.Weight) + " kg");
            ListaKiiras(eatenFish);
            */
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

        private static void ListaKiiras(List<Fish> fishList)
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