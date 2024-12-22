

using static ConsoleTest.TakiCard.ColorCard;

namespace ConsoleTest {
	public abstract record TakiCard {
		public enum CardColor {
			Red, Green, Blue, Yellow
		}

		public enum ColorActionCardFigure {
			ChangeDirection, Stop, Plus, Taki, Plus2	
		}

		public enum NumberCardFigure {
			N1, N2, N3, N4, N5, N6, N7, N8, N9
		}

		public enum NeutralActionCardFigure {
			King, SuperTaki, ChangeColor
		}

		public abstract record ColorCard : TakiCard {
			public required CardColor Color { get; init; }

			public record NumberCard : ColorCard {
				public required NumberCardFigure Figure { get; init; }
			}

			public record ColorActionCard : ColorCard {
				public required ColorActionCardFigure Figure { get; init; }
			}
		}

		public record NeutralActionCard : TakiCard {
			public required NeutralActionCardFigure Figure { get; init; }
		}

		public bool IsFigure(params NumberCardFigure[] figures) {
			
		}

		public bool IsFigure(ColorActionCardFigure figure) => this is ColorActionCard card && card.Figure == figure;
		public bool IsFigure(NeutralActionCardFigure figure) => this is NeutralActionCard card && card.Figure == figure;
	}
}