using System.Collections.Generic;

namespace WaterSortSolverCore
{
	public class Board : List<Vial>
	{
		public Board() : base()
		{

		}

		public Board ShallowCopy()
		{
			return (Board)this.MemberwiseClone();
		}

		public bool IsWin => this.TrueForAll(vial => vial.IsEmpty || vial.TopLiquid.Quantity == 4);
	}
}
