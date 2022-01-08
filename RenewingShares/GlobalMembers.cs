using System;
using System.Collections.Generic;
using System.Text;

namespace RenewingShares
{
    public class GlobalMembers
    {
        public GlobalMembers(int shares)
        {
            ListOfPoints = new List<Tuple<int, int>>[shares];
            mainPoints = new List<Tuple<int, int>>(shares);
        }
        List<Tuple<int, int>>[] ListOfPoints;
        List<Tuple<int, int>> mainPoints;


        // Implementation of Shamir's secret sharing algorithm

        // Function to calculate the value  of y
        // y = poly[0] + x*poly[1] + x^2*poly[2] + ...
        public static int CalculateY(int x, List<int> poly)
        {
            // Initializing y
            int y = 0;
            int temp = 1;

            // Iterating through the array
            foreach (var coeff in poly)
            {

                // Computing the value of y
                y += (coeff * temp);
                temp *= x;
            }
            return y;
        }

        public static int CalculateYWithoutFreeMember(int x, List<int> poly)
        {
            // Initializing y
            int y = 0;
            int temp = x;

            // Iterating through the array
            foreach (var coeff in poly)
            {

                // Computing the value of y
                y += (coeff * temp);
                temp *= x;
            }
            return y;

        }

        // Function to perform the secret sharing algorithm
        public List<Tuple<int, int>> SecretSharing(int S, int N, int K)
        {
            // A list to store the polynomial coefficient of K-1 degree
            List<int> poly = new List<int>(K);

            // Randomly choose K - 1 numbers but not zero and poly[0] is the secret

            poly.Add(S);
            for (int j = 1; j < K; ++j)
            {
                int p = 0;
                while (p == 0)
                {
                    // To keep the random values in range not too high we are taking mod with a prime number around 1000
                    p = (RandomNumbers.NextNumber() % 997);
                }

                poly.Add(p);
            }

            // Generating N points from the polynomial we created
            for (int j = 1; j <= N; ++j)
            {
                int x = j;
                int y = CalculateY(x, poly);

                // Points created on sharing
                mainPoints.Add(new Tuple<int, int>(x, y));
            }

            return mainPoints;
        }

        public List<Tuple<int, int>> GenerateNewShares(List<Tuple<int, int>> points, int N, int K)
        {
            // A list to store the polynomial coefficient of K-1 degree
            List<int> poly = new List<int>();

            for (int i = 0; i < N; i++)
            {
                // Randomly choose K - 1 numbers but not zero 
                // Create polynomial for this
                for (int j = 1; j < K; ++j)
                {
                    int p = 0;
                    while (p == 0)
                    {
                        p = (RandomNumbers.NextNumber() % 997);
                    }

                    poly.Add(p);
                }

                // Generating N points from the polynomial we created
                for (int j = 1; j <= N; ++j)
                {
                    int x = j;
                    int y = CalculateYWithoutFreeMember(x, poly);

                    // Points created on sharing
                    points.Add(new Tuple<int, int>(x, y));
                }

                ListOfPoints[i] = points;
                points = new List<Tuple<int, int>>();
                poly = new List<int>();

            }
             //Generating new shares, storing these tuples in list
            List<Tuple<int, int>> newShares = GenerateShares();
            return newShares;
        }

        public List<Tuple<int, int>> GenerateShares()
        {
            int newY = 0;
            List<Tuple<int, int>> points = new List<Tuple<int, int>>();

            for (int j = 0; j < ListOfPoints.Length; j++)
            {
                for (int k = 0; k < ListOfPoints[j].Count; k++)
                {
                    newY += ListOfPoints[k][j].Item2;
                }
                newY += mainPoints[j].Item2;

                points.Add(new Tuple<int, int>(ListOfPoints[j][j].Item1, newY));
                newY = 0;
            }

            return points;
        }


        // Function to generate the secret back from the given points
        // Lagrange Basis Polynomial
        public int GenerateSecret(int[] x, int[] y, int M)
        {

            int res = 0;
            Fraction start; 

            // Loop to iterate through the given points
            for (int i = 0; i < M; ++i)
            {
                start = new Fraction(1, 1);
                // Initializing the fraction
                Fraction l = new Fraction(y[i], 1);
                for (int j = 0; j < M; ++j)
                {
                    
                    // Computing the lagrange terms
                    if (i != j)
                    {
                        Fraction temp = new Fraction(-x[j], x[i] - x[j]);
                        start *= temp;
                    }

                }

                l *= start;
                res += l.num / l.den;

            }

            // Return the secret
            return res;
        }

        internal static class RandomNumbers
        {
            private static System.Random r;

            public static int NextNumber()
            {
                if (r == null)
                    Seed();

                return r.Next();
            }

            public static int NextNumber(int ceiling)
            {
                if (r == null)
                    Seed();

                return r.Next(ceiling);
            }

            public static void Seed()
            {
                r = new System.Random();
            }

            public static void Seed(int seed)
            {
                r = new System.Random(seed);
            }
        }
    }
}

