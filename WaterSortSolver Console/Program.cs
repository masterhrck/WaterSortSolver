using System;
using System.Collections.Generic;
using WaterSortSolverCore;

namespace WaterSortSolver_Console
{
	class Program
	{
		static void Main()
		{
			Board board = new();

			Console.Write("Number of vials: ");
			int nVials = int.Parse(Console.ReadLine());

			List<string> colorList = new();

			Console.WriteLine("Enter colors of liquids and quantity if != 1, top to bottom");

			for (int iVial = 0; iVial < nVials; iVial++)
			{
				Vial vial = new();
				Console.WriteLine("Vial #" + (iVial + 1));

				List<string> inputList = new();

				while (true)
				{
					string input = Console.ReadLine();

					if (string.IsNullOrWhiteSpace(input))
						break;

					inputList.Add(input);
				}

				for (int i = inputList.Count - 1; i >= 0; i--)
				{
					Liquid liquid = new();

					var rawValues = inputList[i].Split(' ');
					string color = rawValues[0];

					if (!colorList.Contains(color))
						colorList.Add(color);

					liquid.Color = (byte)colorList.IndexOf(color);
					liquid.Quantity = rawValues.Length > 1 ? byte.Parse(rawValues[1]) : (byte)1;

					vial.Add(liquid);
				}

				board.Add(vial);
			}

			Solver.Solve(board);

			Console.WriteLine("Press any key to exit..");
			Console.ReadKey();
		}
	}
}
