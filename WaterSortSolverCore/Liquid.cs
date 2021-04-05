namespace WaterSortSolverCore
{
	public class Liquid
	{
		public byte Color;
		public byte Quantity = 0;

		public Liquid()
		{

		}

		public Liquid(byte color, byte quantity)
		{
			Color = color;
			Quantity = quantity;
		}
	}
}
