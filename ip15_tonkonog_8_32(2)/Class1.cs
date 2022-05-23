using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ip15_tonkonog_8_32_2_
{
    internal static class WorkWithFile
    {
        public static Coord[] Read(string file)
        {
            Coord[] coords;
            FileStream fs = new FileStream(file, FileMode.Open);
            using (StreamReader sr = new StreamReader(fs))
            {
                int numCities = Convert.ToInt32(sr.ReadLine());
                coords = new Coord[numCities];
                for (int i = 0; i < numCities; i++)
                {
                    string[] tmp = sr.ReadLine().Split();
                    coords[i] = new Coord(Convert.ToInt32(tmp[0]), Convert.ToInt32(tmp[1]));
                }
            }
            return coords;
        }

        public static void WriteToFile(string path, int[] bestPath, double bestDistance)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            using (StreamWriter sr = new StreamWriter(fs))
            {
                sr.WriteLine(bestDistance.ToString("F1"));
                for (int i = 0; i < bestPath.Length; i++)
                {
                    sr.Write(bestPath[i].ToString() + " ");
                }
            }
        }
    }
}
