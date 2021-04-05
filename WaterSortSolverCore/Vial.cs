using System;
using System.Collections.Generic;

namespace WaterSortSolverCore
{
	public class Vial : List<Liquid>
	{
		public byte TotalQuantity
		{
			get
			{
				byte amount = 0;
				for (int i = 0; i < this.Count; i++)
				{
					amount += this[i].Quantity;
				}
				return amount;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return Count == 0;
			}
		}

		public Liquid TopLiquid
		{
			get
			{
				if (this.IsEmpty)
					throw new InvalidOperationException();

				return this[Count - 1];
			}
		}
	}
}
