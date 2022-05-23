using System;
using System.IO;
namespace ip15_tonkonog_8_32_2_
{
    class Program
    {
        public static void Main(string[] args)
        {
            var trail = Algorithm.Search("input3.txt", 9, 1000, out double bestDistance);
            WorkWithFile.WriteToFile("output3.txt", trail, bestDistance);
        }
    }
    internal static class Algorithm
    {
        private static Random random = new Random(0);

        private static int alpha = 3;

        private static int beta = 2;

        private static double rho = 0.01;

        private static double Q = 2.0;
        public static int[] Search(string file, int numAnts, int numTimes, out double bestDistance)
        {

            Coord[] coords = WorkWithFile.Read(file);
            
            int numCities = coords.Length;

            double[,] dists = InitialiseDistance(coords);

            Ant[] ants = InitAnts(numAnts, numCities);            

            int[] bestTrail = BestTrail(ants, dists);
            bestDistance = Length(bestTrail, dists);

            double[,] pheromones = InitPheromones(numCities);

            int time = 0;
            while (time < numTimes)
            {
                ReloadAnts(ants, pheromones, dists);
                UpdatePheromones(pheromones, ants, dists);

                int[] currBestTrail = BestTrail(ants, dists);
                double currBestLength = Length(currBestTrail, dists);
                if (currBestLength < bestDistance)
                {
                    bestDistance = currBestLength;
                    bestTrail = currBestTrail;                    
                }
                time += 1;
            }

            Console.WriteLine("\nBest trail found:");
            Display(bestTrail);
            Console.WriteLine("\nLength of best trail: " + bestDistance.ToString("F1"));

            return bestTrail;
        }

        private static Ant[] InitAnts(int numAnts, int numCities)
        {
            Ant[] ants = new Ant[numAnts];
            for (int k = 0; k <= numAnts - 1; k++)
            {
                int start = random.Next(0, numCities);
                ants[k] = new Ant(RandomTrail(start, numCities));
            }
            return ants;
        }

        private static int[] RandomTrail(int start, int numCities)
        {
            int[] trail = new int[numCities+1];

            for (int i = 0; i < numCities ; i++)
            {
                trail[i] = i;
            }

            for (int i = 0; i < numCities; i++)
            {
                int r = random.Next(i, numCities);
                int tmp = trail[r];
                trail[r] = trail[i];
                trail[i] = tmp;
            }

            int idx = IndexOfStart(trail, start);
            int temp = trail[0];
            trail[0] = trail[idx];
            trail[idx] = temp;

            trail[numCities] = trail[0]; 

            return trail;
        }

        private static int IndexOfStart(int[] trail, int target)
        {
            for (int i = 0; i <= trail.Length - 1; i++)
            {
                if (trail[i] == target)
                {
                    return i;
                }
            }
            throw new Exception("Target not found in IndexOfTarget");
        }

        private static double Length(int[] trail, double[,] dists)
        {
            
            double result = 0.0;
            for (int i = 0; i <= trail.Length - 2; i++)
            {
                result += Distance(trail[i], trail[i + 1], dists);
            }
            return result;
        }

        private static int[] BestTrail(Ant[] ants, double[,] dists)
        {
            double bestLength = Length(ants[0].Trail, dists);
            int idxBestLength = 0;
            for (int k = 1; k <= ants.Length - 1; k++)
            {
                double len = Length(ants[k].Trail, dists);
                if (len < bestLength)
                {
                    bestLength = len;
                    idxBestLength = k;
                }
            }
            int[] bestTrail = (int[])ants[idxBestLength].Trail.Clone();
            return bestTrail;
        }

        private static double[,] InitPheromones(int numCities)
        {
            double[,] pheromones = new double[numCities,numCities];
            
            for (int i = 0; i < numCities; i++)
            {
                for (int j = 0; j < numCities; j++)
                {
                    pheromones[i,j] = 0.01;
                }
            }
            return pheromones;
        }

        private static void ReloadAnts(Ant[] ants, double[,] pheromones, double[,] dists)
        {
            int numCities = ants[0].Trail.Length;
            for (int i = 0; i < ants.Length; i++)
            {
                int start = random.Next(0, numCities - 1);
                int[] newTrail = BuildTrail(start, pheromones, dists);
                ants[i] = new Ant(newTrail);
            }
        }

        private static int[] BuildTrail(int start, double[,] pheromones, double[,] dists)
        {
            int numCities = (int)Math.Sqrt(pheromones.Length);
            int[] trail = new int[numCities + 1];
            bool[] visited = new bool[numCities];
            trail[0] = start;
            visited[start] = true;
            for (int i = 0; i < numCities-1; i++)
            {
                int cityX = trail[i];
                int next = NextCity(cityX, visited, pheromones, dists);
                trail[i + 1] = next;
                visited[next] = true;
            }
            trail[numCities] = start; 
            return trail;
        }

        private static int NextCity(int city1, bool[] visited, double[,] pheromones, double[,] dists)
        {
            double[] probs = MoveProbs(city1, visited, pheromones, dists);

            double[] cumul = new double[probs.Length + 1];
            for (int i = 0; i <= probs.Length - 1; i++)
            {
                cumul[i + 1] = cumul[i] + probs[i];
            }

            double p = random.NextDouble();

            for (int i = 0; i <= cumul.Length - 2; i++)
            {
                if (p >= cumul[i] && p < cumul[i + 1])
                {
                    return i;
                }
            }
            throw new Exception();
        }

        private static double[] MoveProbs(int cityX, bool[] visited, double[,] pheromones, double[,] dists)
        {
            int numCities = (int)Math.Sqrt(pheromones.Length);
            double[] taueta = new double[numCities];
            double sum = 0.0;
            for (int i = 0; i <= taueta.Length - 1; i++)
            {
                if (i == cityX)
                {
                    taueta[i] = 0.0;
                }
                else if (visited[i] == true)
                {
                    taueta[i] = 0.0;
                }
                else
                {
                    taueta[i] = Math.Pow(pheromones[cityX,i], alpha) * Math.Pow((1.0 / Distance(cityX, i, dists)), beta);
                }
                sum += taueta[i];
            }

            double[] probs = new double[numCities];
            for (int i = 0; i <= probs.Length - 1; i++)
            {
                probs[i] = taueta[i] / sum;
            }
            return probs;
        }

        private static void UpdatePheromones(double[,] pheromones, Ant[] ants, double[,] dists)
        {
            for (int i = 0; i < Math.Sqrt(pheromones.Length); i++)
            {
                for (int j = i + 1; j < Math.Sqrt(pheromones.Length); j++)
                {
                    for (int k = 0; k <= ants.Length - 1; k++)
                    {
                        double length = Length(ants[k].Trail, dists);
                        double decrease = (1.0 - rho) * pheromones[i,j];
                        double increase = 0.0;
                        if (EdgeInTrail(i, j, ants[k].Trail) == true)
                        {
                            increase = (Q / length);
                        }

                        pheromones[i,j] = decrease + increase;

                        if (pheromones[i,j] < 0.0001)
                        {
                            pheromones[i,j] = 0.0001;
                        }
                        else if (pheromones[i,j] > 100000.0)
                        {
                            pheromones[i,j] = 100000.0;
                        }

                        pheromones[j,i] = pheromones[i,j];
                    }
                }
            }
        }

        private static bool EdgeInTrail(int cityX, int cityY, int[] trail)
        {
            int lastIndex = trail.Length - 1;
            int idx = IndexOfStart(trail, cityX);

            if (idx == 0 && trail[1] == cityY)
            {
                return true;
            }
            else if (idx == 0 && trail[lastIndex] == cityY)
            {
                return true;
            }
            else if (idx == 0)
            {
                return false;
            }
            else if (idx == lastIndex && trail[lastIndex - 1] == cityY)
            {
                return true;
            }
            else if (idx == lastIndex && trail[0] == cityY)
            {
                return true;
            }
            else if (idx == lastIndex)
            {
                return false;
            }
            else if (trail[idx - 1] == cityY)
            {
                return true;
            }
            else if (trail[idx + 1] == cityY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static double[,] InitialiseDistance(Coord[] coord)
        {
            int num = coord.Length;
            double[,] dists = new double[num,num];            
            for (int i = 0; i < num; i++)
            {
                for (int j = i + 1; j < num; j++)
                {                    
                    dists[i,j] = coord[i].GetDistance(coord[j]);
                    dists[j,i] = dists[i,j];
                }
            }
            return dists;
        }
        private static double Distance(int city1, int city2, double[,] dists)
        {
            return dists[city1, city2];
        }
        private static void Display(int[] trail)
        {
            for (int i = 0; i <= trail.Length - 1; i++)
            {
                Console.Write(trail[i] + " ");
                if (i > 0 && i % 20 == 0)
                {
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("");
        }      
    }
}
