using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest {
	public static class Utilities {
		/*
		I understand the algorithm itself, but I'm not sure why it's the BEST algorithm.
		As in, an actual mathematical proof. Not by intuition.
		Maybe research this?
		*/
		public static IList<T> FisherYatesShuffle<T>(this IList<T> items) {
			Random random = new();

			for (int i = 0; i < items.Count - 1; i++) {
				int pos = random.Next(i, items.Count);
				T temp = items[i];
				items[i] = items[pos];
				items[pos] = temp;
			}
			return items;
		}
	}
}
