using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace task2._1
{
    class Program
    {
        static int Number(string txt)
        {
            txt = txt.Trim(' ');
            int num = Convert.ToInt32(txt);
            return num;
        }
        static int[] Arr(string txt,int n)
        {
            txt = txt.Trim();
            Regex r = new Regex(@"[\s]+");
            string[] temp = r.Split(txt);
            int[] arr = new int[n];
            for(int i = 0; i < n; i++)
            {
                temp[i] = temp[i].Trim(' ');
                arr[i] = Convert.ToInt32(temp[i]);
            }
            return arr;
        }

        static int[] MergeSort(int[] input, int left, int right,FileStream f,StreamWriter w)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                MergeSort(input, left, middle,f,w);
                MergeSort(input, middle + 1, right,f,w);

                //Merge
                int[] leftArray = new int[middle - left + 1];
                int[] rightArray = new int[right - middle];

                Array.Copy(input, left, leftArray, 0, middle - left + 1);
                Array.Copy(input, middle + 1, rightArray, 0, right - middle);
                int i = 0;
                int j = 0;
                for (int k = left; k < right + 1; k++)
                {
                    
                    if (i == leftArray.Length)
                    {
                        input[k] = rightArray[j];
                        j++;
                    }
                    else if (j == rightArray.Length)
                    {
                        input[k] = leftArray[i];
                        i++;
                    }
                    else if (leftArray[i] <= rightArray[j])
                    {
                        input[k] = leftArray[i];
                        i++;
                    }
                    else
                    {
                        input[k] = rightArray[j];
                        j++;
                    }
                }
                w.Write(left+1 + " " + (right + 1) + " " + input[left] + " " + input[right]);
                w.WriteLine();
            }
            return input;
        }
        static void Main(string[] args)
        {
            FileStream f = new FileStream("input.txt", FileMode.OpenOrCreate);
            StreamReader r = new StreamReader(f);
            string s1 = r.ReadLine();
            string s2 = r.ReadLine();
            r.Close();
            f.Close();
            int num = Number(s1);
            int[] arr = Arr(s2, num);
            FileStream f1 = new FileStream("output.txt", FileMode.OpenOrCreate);
            StreamWriter w = new StreamWriter(f1);
            arr=MergeSort(arr, 0, arr.Length - 1,f1,w);
            foreach(int x in arr)
            {
                w.Write(x + " ");
            }
            w.Close();
            f1.Close();
        }
    }
}
