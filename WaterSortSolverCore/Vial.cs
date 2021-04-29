using System;
using System.Collections.Generic;

namespace WaterSortSolverCore
{
	public class Vial : List<Liquid>
	{
		public Vial() : base()
		{

		}

		public Vial(Vial vial) : base()
		{
			foreach (Liquid liquid in vial)
			{
				Add(new Liquid(liquid.Color, liquid.Quantity));
			}
		}

		public byte TotalQuantity
		{
			get
			{
				byte amount = 0;

				foreach (Liquid liquid in this)
				{
					amount += liquid.Quantity;
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

		public void RemoveTopLiquid()
		{
			RemoveAt(Count - 1);
		}
	}
}
