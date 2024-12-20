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

		public record ColorCard : TakiCard {
			public required CardColor Color { get; init; }
			public required ColorCardFigure Figure { get; init; }

			public bool IsNormalNumber() {
				ColorCardFigure[] numberFigures =
					{ColorCardFigure.N1, ColorCardFigure.N2, ColorCardFigure.N3, ColorCardFigure.N4, ColorCardFigure.N5, ColorCardFigure.N6, ColorCardFigure.N7,
					ColorCardFigure.N8, ColorCardFigure.N9};
				return numberFigures.Contains(Figure);
			}
		}

		public record NeutralCard : TakiCard {
			public required NeutralCardFigure CardFigure { get; init; }
		}

		public bool IsValidPlayOn(TakiCard other) {
			//Kings are always playable and can always be played on
			//Change color is always playable (EXCEPT if you have extra draws, which is covered in TakiGame)

			if(this is NeutralCard nC) {
				if(nC.CardFigure == NeutralCardFigure.King || nC.CardFigure == NeutralCardFigure.ChangeColor) {
					return true;
				}
			}
			
			if (other is NeutralCard otherNC && otherNC.CardFigure == NeutralCardFigure.King) {
				return true;
			}


			//If there are colors to match...

			if (this is ColorCard thisCC && other is ColorCard otherCC) {
				if(thisCC.Color == otherCC.Color) {
					return true;
				}

				//If figures match... (color card)
				if(thisCC.Figure == otherCC.Figure) {
					return true;
				}
			}

			//If there are figures to match... (neutral card)
			nC = this as NeutralCard;
			otherNC = other as NeutralCard;
			if (nC.CardFigure == otherNC.CardFigure) {
				return true;
			}

			//If nothing matches...

			return false;
		}
	}
}