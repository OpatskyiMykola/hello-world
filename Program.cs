using System;
using System.Collections.Generic;


namespace LeftCrossing
{
    class Program
    {
        static void GenerateVectors(List<double> a, List<double> b, List<double> c, List<double> f)
        {
            int n = a.Capacity;
            double h = 1.0 / (n + 1);//step

            for (int i = 0; i < n; i++)
            {
                double first = 1 / Math.Pow(h, 2);
                double second = (1 + i * h) / (2 * h);

                a.Add(first + second);
                b.Add(first - second);
                c.Add(1 + first * 2);
                f.Add(2 / Math.Pow(1 + i * h, 3));
            }
        }

        static void PrintVector(List<double> array)
        {
            Console.Write("[ ");
            foreach (var item in array)
            {
                Console.Write(Math.Round(item, 4).ToString() + ' ');
            }
            Console.Write("]\n");

        }

        static List<double> CheckVector(int n)//перевірка результату, має збігатися
        {
            List<double> y = new List<double>(n + 1);
            double h = 1.0 / n;
            for (int i = 0; i < n + 1; i++)
            {
                y.Add(1 / (i * h + 1));
            }
            return y;
        }

        static void Straight(List<double> a, List<double> b, List<double> c, List<double> f, List<double> ksi, List<double> eta)//прямий хід, щукаємо коефіціжнти ксі і ета
        {
            ksi.Add(0);
            eta.Add(0.5);
            int n = a.Count;

            for (int i = n - 1, j = 0; i >= 0; i--, j++)
            {
                ksi.Add(a[i] / (c[i] - ksi[j] * b[i]));
                eta.Add((b[i] * eta[j] + f[i]) / (c[i] - ksi[j] * b[i]));
            }

            ksi.Reverse();
            eta.Reverse();
        }

        static List<double> Back(List<double> ksi, List<double> eta)//зворотній хід шукаємо у
        {
            int n = ksi.Count;
            List<double> y = new List<double>(n);
            y.Add(1);

            for (int i = 1; i < n; i++)
            {
                y.Add(ksi[i] * y[i - 1] + eta[i]);
            }

            return y;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Input N: ");
            int n = Convert.ToInt32(Console.ReadLine());

            List<double> a = new List<double>(n - 1);
            List<double> b = new List<double>(n - 1);
            List<double> c = new List<double>(n - 1);
            List<double> f = new List<double>(n - 1);

            GenerateVectors(a, b, c, f);

            Console.WriteLine("\n*** Vector a: ***");
            PrintVector(a);
            Console.WriteLine("\n*** Vector b: ***");
            PrintVector(b);
            Console.WriteLine("\n*** Vector c: ***");
            PrintVector(c);
            Console.WriteLine("\n*** Vector f: ***");
            PrintVector(f);

            List<double> ksi = new List<double>(n);
            List<double> eta = new List<double>(n);

            Straight(a, b, c, f, ksi, eta);

            Console.WriteLine("\n\n*** Vector ksi: ***");
            PrintVector(ksi);
            Console.WriteLine("\n\n*** Vector eta: ***");
            PrintVector(eta);

            Console.WriteLine("\n\nMy result: ");
            Console.WriteLine("\n\nMy result: ");
            var y = Back(ksi, eta);
            PrintVector(y);

            Console.WriteLine("\nChecking: ");
            var checkY = CheckVector(n);
            PrintVector(checkY);

        }
    }
}
