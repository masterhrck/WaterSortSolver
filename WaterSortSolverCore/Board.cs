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
				foreach (Vial vial in this)
				{
					if (!vial.IsEmpty && vial.TopLiquid.Quantity != 4)
						return false;
				}

				return true;
			}
		}
	}
}
