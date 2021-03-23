using System;
using System.Collections.Generic;
using WaterSortSolverCore;

namespace WaterSortSolver_Console
{
	class Program
	{
		static void Main(string[] args)
		{
			Board board = new Board();

			Console.Write("Number of vials: ");
			int nVials = int.Parse(Console.ReadLine());

			Console.WriteLine("Enter colors of liquids and quantity if != 1, top to bottom");

			for (int iVial = 0; iVial < nVials; iVial++)
			{
				Vial vial = new Vial();

				Console.WriteLine("Vial #" + (iVial + 1));
				List<string> inputList = new List<string>();
				while (true)
				{
					string input = Console.ReadLine();
					if (string.IsNullOrWhiteSpace(input))
						break;
					else
						inputList.Add(input);
				}

				for (int i = inputList.Count - 1; i >= 0; i--)
				{
					Liquid liquid = new Liquid();

					var rawValues = inputList[i].Split(' ');

					liquid.Color = rawValues[0];
					liquid.Quantity = rawValues.Length > 1 ? int.Parse(rawValues[1]) : 1;

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
