

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

		public bool IsFigure(params NumberCardFigure[] figures) => this is NumberCard card && figures.Contains(card.Figure);
		public bool IsFigure(params ColorActionCardFigure[] figures) => this is ColorActionCard card && figures.Contains(card.Figure);
		public bool IsFigure(params NeutralActionCardFigure[] figures) => this is NeutralActionCard card && figures.Contains(card.Figure);

		public bool IsFigure(TakiCard other) {
			if(this is NumberCard numberCard && other is NumberCard otherNumberCard && numberCard.Figure == otherNumberCard.Figure) {
				return true;
			}
			if (this is ColorActionCard colorActionCard && other is ColorActionCard otherColorActionCard && colorActionCard.Figure == otherColorActionCard.Figure) {
				return true;
			}
			if (this is NeutralActionCard neutralActionCard && other is NeutralActionCard otherNeutralActionCard && neutralActionCard.Figure == otherNeutralActionCard.Figure) {
				return true;
			}
			return false;
		}
	}
}