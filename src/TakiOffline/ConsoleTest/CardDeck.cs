using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest {
	public class CardDeck<T> {
		public readonly List<T> Cards;

		public CardDeck() {
			Cards = [];
		}

		public CardDeck(IEnumerable<T> cards) {
			Cards = cards.ToList();
		}

		public T Draw() {
			T topCard = Cards.Last();
			Cards.RemoveAt(Cards.Count - 1);
			return topCard;
		}

		public T Peek() {
			return Cards.Last();
		}

		public void Shuffle() {
			Cards.FisherYatesShuffle();
		}
	}
}
