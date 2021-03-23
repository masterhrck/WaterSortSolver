using System;
using System.Collections.Generic;

namespace WaterSortSolverCore
{
	public static class Solver
	{
		public static void Solve(Board initialBoard)
		{
			Stack<GameState> stack = new Stack<GameState>();
			stack.Push(new GameState(initialBoard));

			int bestMoves = int.MaxValue;
			UInt64 iterationCount = 0;
			while (stack.Count > 0)
			{
				GameState state = stack.Pop();

				//Find new moves
				for (int sourceVialIndex = 0; sourceVialIndex < state.Board.Count; sourceVialIndex++)
				{
					for (int destVialIndex = 0; destVialIndex < state.Board.Count; destVialIndex++)
					{
						if (sourceVialIndex == destVialIndex)
							continue;

						iterationCount++;

						if (IsMoveValid(sourceVialIndex, destVialIndex, state.Board))
						{
							Board newBoard = PerformMove(sourceVialIndex, destVialIndex, state.Board);
							string newGameLog = state.GameLog + (sourceVialIndex + 1) + " -> " + (destVialIndex + 1) + " (" + iterationCount + ")" + "\n";
							GameState newState = new GameState(newBoard, newGameLog, state.nMoves + 1);

							if (newState.Board.IsWin)
							{
								if (newState.nMoves < bestMoves)
								{
									bestMoves = newState.nMoves;
									Console.WriteLine($"Found new best solution ({newState.nMoves} moves):");
									Console.WriteLine(newState.GameLog);
								}
							}
							else
							{
								stack.Push(newState);
							}
						}
					}
				}
			}
		}

		private static bool IsMoveValid(int sourceVialIndex, int destVialIndex, Board board)
		{
			Vial sourceVial = board[sourceVialIndex];
			Vial destVial = board[destVialIndex];

			if (sourceVial.IsEmpty)
				return false;

			if (sourceVial.Count == 1 && destVial.IsEmpty)
				return false;

			if (sourceVial.TopLiquid.Quantity > (4 - destVial.TotalQuantity))
				return false;

			if (!destVial.IsEmpty && sourceVial.TopLiquid.Color != destVial.TopLiquid.Color)
				return false;

			return true;
		}

		private static Board PerformMove(int sourceVialIndex, int destVialIndex, Board board)
		{
			Board newBoard = new Board(board);

			//Add liquid to destination
			if (board[destVialIndex].IsEmpty)
			{
				newBoard[destVialIndex].Add(board[sourceVialIndex].TopLiquid);
			}
			else
			{
				newBoard[destVialIndex].TopLiquid.Quantity += board[sourceVialIndex].TopLiquid.Quantity;
			}

			//Remove liquid from source
			newBoard[sourceVialIndex].RemoveAt(board[sourceVialIndex].Count - 1);

			return newBoard;
		}
	}
}
