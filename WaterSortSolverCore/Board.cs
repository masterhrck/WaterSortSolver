using System;
using System.Collections.Generic;
using System.Text;

namespace WaterSortSolverCore
{
	public class Board : List<Vial>
	{
		public Board(Board board) : base()
		{
			for(int iVial = 0; iVial < board.Count; iVial++)
			{
				Vial newVial = new Vial();

				for(int iLiquid = 0; iLiquid < board[iVial].Count; iLiquid++)
				{
					Liquid newLiquid = new Liquid();

					newLiquid.Color = board[iVial][iLiquid].Color;
					newLiquid.Quantity = board[iVial][iLiquid].Quantity;

					newVial.Add(newLiquid);
				}

				this.Add(newVial);
			}
		}

		public Board() : base()
		{

		}

		public bool IsWin
		{
			get
			{
				for(int i=0; i < this.Count; i++)
				{
					if (!this[i].IsEmpty && this[i].TopLiquid.Quantity != 4)
						return false;
				}
				return true;
			}
		}
	}
}
