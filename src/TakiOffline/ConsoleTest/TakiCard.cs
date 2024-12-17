using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest {
	public abstract record TakiCard {
		public record NumberCard(NumberCard.CardColor Color, int Number) : TakiCard {
			public enum CardColor {
				Red, Green, Blue, Yellow
			}
		}

		public record ActionCard() {
			public enum ActionCardType {
				ChangeDirection, Stop
			}
		}
	}
}