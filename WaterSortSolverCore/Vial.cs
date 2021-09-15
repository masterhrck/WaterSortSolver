using System;
using System.Collections.Generic;
using System.Linq;

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

		public byte TotalQuantity => (byte)this.Select(liquid => (int)liquid.Quantity).Sum();

		public bool IsEmpty => this.Count == 0;

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
