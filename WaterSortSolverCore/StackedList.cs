using System.Collections.Generic;

namespace WaterSortSolverCore
{
	class StackedList<T> : List<T>
	{
		public void Push(T item)
		{
			Add(item);
		}

		public T Pop()
		{
			T item = this[Count - 1];
			RemoveAt(Count - 1);
			return item;
		}
	}
}
