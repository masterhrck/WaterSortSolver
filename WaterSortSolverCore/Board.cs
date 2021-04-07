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

		public bool IsWin
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (!this[i].IsEmpty && this[i].TopLiquid.Quantity != 4)
						return false;
				}
				return true;
			}
		}
	}
}
