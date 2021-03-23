namespace WaterSortSolverCore
{
	class GameState
	{
		public Board Board;
		public string GameLog = "";
		public int nMoves = 0;

		public GameState(Board board)
		{
			Board = board;
		}

		public GameState(Board board, string gameLog, int nMoves)
		{
			Board = board;
			GameLog = gameLog;
			this.nMoves = nMoves;
		}
	}
}
