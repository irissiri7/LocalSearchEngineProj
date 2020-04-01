using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class SortingAlgoritm
    {

		public static void HeapSort<T>(List<T> list) where T : IComparable<T>
		{
			int heapSize = list.Count;

			BuildMaxHeap(list);

			for (int i = heapSize - 1; i >= 1; i--)
			{
				Swap(list, i, 0);
				heapSize--;
				Sink(list, heapSize, 0);
			}
		}

		private static void BuildMaxHeap<T>(List<T> list) where T : IComparable<T>
		{
			int heapSize = list.Count;

			for (int i = (heapSize / 2) - 1; i >= 0; i--)
			{
				Sink(list, heapSize, i);
			}
		}

		private static void Sink<T>(List<T> list, int heapSize, int toSinkPos) where T : IComparable<T>
		{
			if (GetLeftKidPos(toSinkPos) >= heapSize)
			{
				// No left kid => no kid at all
				return;
			}


			int largestKidPos;
			bool leftIsLargest;

			if (GetRightKidPos(toSinkPos) >= heapSize || list[GetRightKidPos(toSinkPos)].CompareTo(list[GetLeftKidPos(toSinkPos)]) < 0)
			{
				largestKidPos = GetLeftKidPos(toSinkPos);
				leftIsLargest = true;
			}
			else
			{
				largestKidPos = GetRightKidPos(toSinkPos);
				leftIsLargest = false;
			}



			if (list[largestKidPos].CompareTo(list[toSinkPos]) > 0)
			{
				Swap(list, toSinkPos, largestKidPos);

				if (leftIsLargest)
				{
					Sink(list, heapSize, GetLeftKidPos(toSinkPos));

				}
				else
				{
					Sink(list, heapSize, GetRightKidPos(toSinkPos));
				}
			}

		}

		private static void Swap<T>(List<T> list, int pos0, int pos1)
		{
			T tmpVal = list[pos0];
			list[pos0] = list[pos1];
			list[pos1] = tmpVal;
		}

		private static int GetLeftKidPos(int parentPos)
		{
			return (2 * (parentPos + 1)) - 1;
		}

		private static int GetRightKidPos(int parentPos)
		{
			return 2 * (parentPos + 1);
		}

	}
}
