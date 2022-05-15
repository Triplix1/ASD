using System;
using System.Collections.Generic;
using System.IO;

namespace ip15_tonokonog_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int person = 4;         
            Audience audience = new Audience("Input.txt", person);
            audience.GetOutput();
        }
    }

    public class Audience
    {
        string file;
        int U;
        int M;
        int person;
        public Audience(string file, int person)
        {
            this.file = file;
            this.person = person;
            using (StreamReader sr = new StreamReader(file))
            {
                if (file.Length == 0)
                {
                    U = 0;
                    M = 0;  
                }
                else
                {
                    string[] line = sr.ReadLine().Split(" ");
                    U = Convert.ToInt32(line[0]);
                    M = Convert.ToInt32(line[1]);
                }                
            }            
        }
        
        public void GetOutput()
        {
            int[,] UM = ReadFile();
            UM = GetCompareArr(UM,person);
            
            int[,] result = GetResultArr(UM, U);
            result = MergeSort(result, U);
            WriteFile(result);
        }
        int[,] GetCompareArr(int[,] arr, int per)
        {
            int[,] newarr = new int[U, M];
            int[] pos = new int[M];
            for (int i = 0; i < M; i++)
            {
                pos[arr[per-1,i]-1] = i;
            }
            for (int i = 0; i < U; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    newarr[i, j] = arr[i, pos[j]];
                }
            }
            return newarr;
        }
        void WriteFile(int[,] arr)
        {
            using (StreamWriter wr = new StreamWriter("Output.txt"))
            { wr.WriteLine(arr[0, 0].ToString());
                for (int i = 1; i < U; i++)
                {
                    wr.WriteLine(arr[i, 0].ToString() + " " +  arr[i, 1].ToString());
                }
            }
        }
        int[,] GetResultArr(int [,] arr, int U)
        {
            int[,] result = new int[U, 2];
            int count;
            for (int i = 0; i < U; i++)
            {
                count = 0;
                result[i, 0] = i + 1;
                int[] tmp = new int[M];
                for (int j = 0; j < M; j++)
                {
                    tmp[j] = arr[i, j];
                }
                tmp = Decomposition.CountInv(tmp, ref count);
                result[i, 1] = count;
            }
            return result; 
        }
        int[,] ReadFile()
        {
            int[,] UM = new int[U, M];
            using (StreamReader sr = new StreamReader(file))
            {
                string text = sr.ReadToEnd();
                string[] lines = text.Split("\n");
                for (int i = 1; i <= U; i++)
                {
                    string[] raiting = lines[i].Split();
                    for (int j = 1; j <= M; j++)
                    {                        
                        UM[i-1, j-1] = Convert.ToInt32(raiting[j]);
                    }

                }
            }
            return UM;
        }

        int[,] MergeSort(int[,] arr ,int U)
        {                        

            if (U == 1)
            {
                return arr;
            }
            
            int half = U / 2;
            int[,] leftarr = new int[half, 2];
            int[,] rightarr = new int[U - half,2];
            CopyTo(arr, leftarr, 0, half);
            CopyTo(arr, rightarr, half, U - half);
            leftarr = MergeSort(leftarr, half);
            rightarr = MergeSort(rightarr, U-half);
            arr = Merge(leftarr, rightarr); 
            return arr;
        }

        int[,] Merge(int[,] left, int[,] right)
        {
            int[,] lef = new int[left.Length/2 +1, 2];
            int[,] righ = new int[right.Length/2 + 1, 2];
            int[,] arr = new int[left.Length/2 + right.Length/2, 2];
            CopyTo(left, lef, 0, left.Length/2);
            lef[left.Length/2,0] = int.MaxValue;
            lef[left.Length/2,1] = int.MaxValue;
            CopyTo(right, righ, 0, right.Length/2);
            righ[right.Length/2,0] = int.MaxValue;
            righ[right.Length/2,1] = int.MaxValue;

            int a = 0, b = 0;

            for (int i = 0; i < left.Length/2 + right.Length/2; i++)
            {
                if (lef[a,1] <= righ[b,1])
                {
                    arr[i,1] = lef[a,1];
                    arr[i,0] = lef[a,0];                    
                    a++;
                }
                else
                {
                    arr[i,0] = righ[b,0];
                    arr[i,1] = righ[b,1];
                    b++;

                }
            }
            return arr;
        }

        private static void CopyTo(int[,] f, int[,] to, int startpos, int length)
        {
            for (int i = 0; i < length; i++)
            {
                to[i,0] = f[startpos + i,0];
                to[i,1] = f[startpos + i,1];
            }
        }
    }

    public static class Decomposition
    {
        public static int[] CountInv(int[] arr, ref int count)
        {
            int length = arr.Length;

            if (length == 1)
            {
                return arr;
            }
                         
            int half = length / 2;
            int[] leftarr = new int[half];
            int[] rigtharr = new int[length - half];
            CopyTo(arr, leftarr, 0, half);
            CopyTo(arr, rigtharr, half, length - half); 
            leftarr = CountInv(leftarr, ref count);
            rigtharr = CountInv(rigtharr, ref count);
            arr = CountSplitInv(leftarr, rigtharr, ref count);
            
            return arr;
        }

        public static int[] CountSplitInv(int[] left, int[] right, ref int x)
        {
            int[] lef = new int[left.Length+1];
            int[] righ = new int[right.Length+1];
            int[] arr = new int[left.Length + right.Length];
            CopyTo(left, lef, 0, left.Length);
            lef[left.Length] = int.MaxValue;
            CopyTo(right, righ, 0, right.Length);
            righ[right.Length] = int.MaxValue;

            int a = 0, b = 0; 
            
            for (int i = 0; i < left.Length + right.Length; i++)
            {
                if (lef[a] <= righ[b])
                {
                    arr[i] = lef[a];
                    a++;                    
                }
                else
                {
                    x += left.Length - a;
                    arr[i] = righ[b];
                    b++;
                    
                }
            }
            return arr;
        }
        private static void CopyTo(int[] f, int[] to, int startpos, int length)
        {
            for (int i = 0; i < length; i++)
            {
                to[i] = f[startpos + i];
            }
        }
    }       
}
