using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest {
	public abstract record TakiCard {
		public enum CardColor {
			Red, Green, Blue, Yellow
		}

		public enum CardNumber {
			N1, N2, N3, N4, N5, N6, N7, N8, N9
		}

		public enum ColorActionCardType {
			ChangeDirection, Stop, Plus, Taki, Plus2
		}

		public enum NeutralActionCardType {
			King, SuperTaki, ChangeColor
		}

		public abstract record ColorCard : TakiCard {
			public required CardColor Color { get; init; }

			public record NumberCard : ColorCard {
				

				public required CardNumber Number { get; init; }
			}

			public record ColorActionCard : ColorCard {
				public required ColorActionCardType CardType { get; init; }
			}
		}

		public record NeutralActionCard : TakiCard {
			public required NeutralActionCardType CardType { get; init; }
		}
	}
}