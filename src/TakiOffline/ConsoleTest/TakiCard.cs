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

		public enum ColorActionCardFigure {
			ChangeDirection, Stop, Plus, Taki, Plus2
		}

		public enum NeutralActionCardFigure {
			King, SuperTaki, ChangeColor
		}

		public abstract record ColorCard : TakiCard {
			public required CardColor Color { get; init; }

			public record NumberCard : ColorCard {
				public required CardNumber Number { get; init; }
			}

			public record ColorActionCard : ColorCard {
				public required ColorActionCardFigure CardFigure { get; init; }
			}
		}

		public record NeutralActionCard : TakiCard {
			public required NeutralActionCardFigure CardFigure { get; init; }
		}

		public bool IsPlayableOn(TakiCard other) {
			//Kings are always playable and can always be played on

			if(this is NeutralActionCard c && c.CardFigure == NeutralActionCardFigure.King) {
				return true;
			}
			
			if (other is NeutralActionCard otherC && otherC.CardFigure == NeutralActionCardFigure.King) {
				return true;
			}

			//If there are colors to match...

			if (this is ColorCard thisCC && other is ColorCard otherCC) {
				if(thisCC.Color == otherCC.Color) {
					return true;
				}
			}

			//If there are figures to match...

			if (this is ColorCard.ColorActionCard thisCAC && other is ColorCard.ColorActionCard otherCAC) {
				if (thisCAC.CardFigure == otherCAC.CardFigure) {
					return true;
				}
			}

			if (this is ColorCard.ColorActionCard thisCAC && other is ColorCard.ColorActionCard otherCAC) {
				if(thisCAC.CardFigure == otherCAC.CardFigure) {
					return true;
				}
			}

			if (this is TakiCard.NeutralActionCard thisNAC && other is TakiCard.NeutralActionCard otherNAC) {
				if(thisNAC.CardFigure == otherNAC.CardFigure) {
					return true;
				}
			}

			//If nothing matches...

			return false;
		}
	}
}