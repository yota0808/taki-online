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

		public enum ColorCardFigure {
			//Numbers
			N1, N2, N3, N4, N5, N6, N7, N8, N9,
			//Color action cards
			ChangeDirection, Stop, Plus, Taki, Plus2,			
		}

		public enum NeutralCardFigure {
			King, SuperTaki, ChangeColor
		}

		public abstract record ColorCard : TakiCard {
			public required CardColor Color { get; init; }
			public required ColorCardFigure Figure { get; init; }
		}

		public record NeutralCard : TakiCard {
			public required NeutralCardFigure CardFigure { get; init; }
		}

		public bool IsPlayableOn(TakiCard other) {
			//Kings are always playable and can always be played on

			if(this is NeutralCard c && c.CardFigure == NeutralCardFigure.King) {
				return true;
			}
			
			if (other is NeutralCard otherC && otherC.CardFigure == NeutralCardFigure.King) {
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

			if (this is TakiCard.NeutralCard thisNAC && other is TakiCard.NeutralCard otherNAC) {
				if(thisNAC.CardFigure == otherNAC.CardFigure) {
					return true;
				}
			}

			//If nothing matches...

			return false;
		}
	}
}