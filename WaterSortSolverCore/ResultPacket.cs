using System.Collections.Generic;

namespace WaterSortSolverCore
{
	class ResultPacket
	{
		public List<GameState> NewGameStates = new List<GameState>();
		public GameState WinGameState;
		public bool IsWin = false;
	}
}
