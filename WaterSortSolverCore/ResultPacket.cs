using System.Collections.Generic;

namespace WaterSortSolverCore
{
	class ResultPacket
	{
		public List<GameState> NewGameStates = new();
		public GameState WinGameState;
		public bool IsWin = false;
	}
}
