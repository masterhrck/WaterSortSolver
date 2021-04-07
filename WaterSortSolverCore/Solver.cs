using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WaterSortSolverCore
{
	public static class Solver
	{
		public static void Solve(Board initialBoard)
		{
			const int maxTasks = 5;

			int movesCap = int.MaxValue;
			StackedList<GameState> stack = new();
			stack.Push(new GameState(initialBoard));

			List<Task<ResultPacket>> tasks = new();

			StartTasks();

			while (tasks.Count > 0)
			{
				int index = Task.WaitAny(tasks.ToArray());

				ResultPacket result = tasks[index].Result;
				tasks.RemoveAt(index);

				if (result.IsWin && result.WinGameState.nMoves <= movesCap)
				{
					Console.WriteLine($"Found solution ({result.WinGameState.nMoves} moves):");
					Console.WriteLine(result.WinGameState.GameLog);

					movesCap = result.WinGameState.nMoves - 1;
					Console.WriteLine($"Attempting to find solution with {movesCap} moves..\n");

					stack.RemoveAll(item => item.nMoves >= movesCap - 1);
				}
				else if (result.NewGameStates.Count != 0 && result.NewGameStates[0].nMoves < movesCap)
				{
					stack.AddRange(result.NewGameStates);
				}

				StartTasks();
			}

			void StartTasks()
			{
				int tasksToRun = Math.Min(maxTasks - tasks.Count, stack.Count);
				for (int i = 0; i < tasksToRun; i++)
				{
					GameState state = stack.Pop();
					Task<ResultPacket> task = Task.Run(() => ProcessGameState(state));
					tasks.Add(task);
				}
			}
		}

		private static ResultPacket ProcessGameState(GameState state)
		{
			ResultPacket resultPacket = new();
			//Find new moves
			for (int sourceVialIndex = 0; sourceVialIndex < state.Board.Count; sourceVialIndex++)
			{
				for (int destVialIndex = 0; destVialIndex < state.Board.Count; destVialIndex++)
				{
					if (sourceVialIndex == destVialIndex)
						continue;

					if (IsMoveValid(sourceVialIndex, destVialIndex, state.Board))
					{
						Board newBoard = PerformMove(sourceVialIndex, destVialIndex, state.Board);
						string newGameLog = state.GameLog + (sourceVialIndex + 1) + " -> " + (destVialIndex + 1) + "\n";
						GameState newState = new(newBoard, newGameLog, state.nMoves + 1);

						if (newState.Board.IsWin)
						{
							resultPacket.IsWin = true;
							resultPacket.WinGameState = newState;
							return resultPacket;
						}
						else
						{
							resultPacket.NewGameStates.Add(newState);
						}
					}
				}
			}

			return resultPacket;
		}

		private static bool IsMoveValid(int sourceVialIndex, int destVialIndex, Board board)
		{
			Vial sourceVial = board[sourceVialIndex];
			Vial destVial = board[destVialIndex];

			if (sourceVial.IsEmpty)
				return false;

			if (sourceVial.Count == 1 && destVial.IsEmpty)
				return false;

			if (destVial.TotalQuantity > 3)
				return false;

			if (!destVial.IsEmpty && sourceVial.TopLiquid.Color != destVial.TopLiquid.Color)
				return false;

			return true;
		}

		private static Board PerformMove(int sourceVialIndex, int destVialIndex, Board board)
		{
			Board newBoard = new(board);
			Vial sourceVial = newBoard[sourceVialIndex];
			Vial destVial = newBoard[destVialIndex];

			if (destVial.IsEmpty)
			{
				destVial.Add(sourceVial.TopLiquid);
				sourceVial.RemoveTopLiquid();
			}
			else
			{
				byte transferredQuantity = (byte)Math.Min(sourceVial.TopLiquid.Quantity, 4 - destVial.TotalQuantity);

				destVial.TopLiquid.Quantity += transferredQuantity;
				sourceVial.TopLiquid.Quantity -= transferredQuantity;

				if (sourceVial.TopLiquid.Quantity == 0)
					sourceVial.RemoveTopLiquid();
			}

			return newBoard;
		}
	}
}
