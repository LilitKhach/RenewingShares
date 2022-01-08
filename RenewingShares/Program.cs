using System;
using System.Collections.Generic;

namespace RenewingShares
{
    class Program
    {
        static void Main(string[] args)
        {
            int S = 24;
            int N = 5;
            int K = 3;

			List<Tuple<int, int>> newPoints;
			List<Tuple<int, int>> points;

			GlobalMembers gm = new GlobalMembers(N);
			points = gm.SecretSharing(S, N, K);

			Console.Write("Secret is divided to ");
			Console.Write(N);
			Console.Write(" Parts - ");
			Console.Write("\n");

			for (int i = 0; i < N; ++i)
			{
				Console.Write(points[i].Item1);
				Console.Write(" ");
				Console.Write(points[i].Item2);
				Console.Write("\n");
			}

			Console.Write("We can generate Secret from any of ");
			Console.Write(K);
			Console.Write(" Parts");
			Console.Write("\n");

			Console.Write($"Enter number of points to restore the secret, at least {K} points are required");
            Console.WriteLine("\n");

			// Input any M points from these to get back our secret code.
			int M = int.Parse(Console.ReadLine()); //3;

			// M can be greater than or equal to threshold
			if (M < K)
			{
				Console.Write("Points are less than threshold ");
				Console.Write(K);
				Console.Write(" Points Required");
				Console.Write("\n");
				M = int.Parse(Console.ReadLine());
			}

			int[] x = new int[M];
			int[] y = new int[M];

			for (int i = 0; i < M; ++i)
			{
				x[i] = points[i].Item1;
				y[i] = points[i].Item2;
			}

            // Get back our result.
            Console.WriteLine("\n");
			Console.Write("Our Secret Code is : ");
			Console.Write(gm.GenerateSecret(x, y, M));
			Console.Write("\n");

			// GENERATING NEW SHARES
            Console.WriteLine("Now we are going to generate new shares!");
            Console.WriteLine("\n");

			newPoints = gm.GenerateNewShares(new List<Tuple<int, int>>(N), N, K);
			Console.Write("Secret is divided to ");
			Console.Write(N);
			Console.Write(" Parts - ");
			Console.Write("\n");

			for (int i = 0; i < N; ++i)
			{
				Console.Write(newPoints[i].Item1);
				Console.Write(" ");
				Console.Write(newPoints[i].Item2);
				Console.Write("\n");
			}

			int newM = M; // Console.Read();
		
			int[] newX = new int[newM];
			int[] newY = new int[newM];

			for (int i = 0; i < M; ++i)
			{
				newX[i] = newPoints[i].Item1;
				newY[i] = newPoints[i].Item2;
			}

			// Get back our result again.
			Console.Write("Our Secret Code is : ");
			Console.Write(gm.GenerateSecret(newX, newY,newM));
			Console.Write("\n");

		}
	}
}
