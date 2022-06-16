using System;
using System.IO;

namespace ip15_tonkonoh_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int maxvalue;
            int[,] vw;
            using (StreamReader sr = new StreamReader("input_500.txt"))
            {
                string[] line = sr.ReadLine().Split();
                maxvalue = Convert.ToInt32(line[0]);
                int num = Convert.ToInt32(line[1]);
                vw = new int[num, 2];
                for (int i = 0; i < num; i++)
                {
                    string[] tmp = sr.ReadLine().Split();
                    vw[i, 0] = Convert.ToInt32(tmp[0]);
                    vw[i, 1] = Convert.ToInt32(tmp[1]);
                }
            }
            int result = FindTheBest(maxvalue, vw);
            Console.WriteLine(result);
            FileStream fs = new FileStream("output_500.txt", FileMode.OpenOrCreate);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(result.ToString());
            }
        }
        public static int FindTheBest(int bag, int[,] vw)
        {
            int num = vw.Length / 2;
            int[,] result = new int[num + 1, bag + 1];
            for (int i = 0; i < num + 1; i++)
            {
                for (int j = 0; j < bag + 1; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        result[i, j] = 0;
                    }
                    else
                    {
                        if (j >= vw[i - 1, 1])
                        {
                            result[i, j] = Math.Max(result[i - 1, j], result[i - 1, j - vw[i - 1, 1]] + vw[i - 1, 0]);
                        }
                        else
                        {
                            result[i, j] = result[i - 1, j];
                        }
                    }
                }
            }
            return result[num, bag];
        }
    }
}
