using static ConsoleTest.Utilities;
using static ConsoleTest.TakiCard;

namespace ConsoleTest {
	public static class TakiConstants {
		public static TakiCard[] StandardDecklist() {
			List<TakiCard> cards = [];

			CardColor[] cardColors = Enum.GetValues<CardColor>();
			CardNumber[] cardNumbers = Enum.GetValues<CardNumber>();

			foreach(CardNumber number in cardNumbers) {
				foreach(CardColor color in cardColors) {
					for(int i = 1; i <= 2; i++) {
						cards.Add(new ColorCard.NumberCard {
							Color = color,
							Number = number
						});
					}
				}
			}

			ColorActionCardFigure[] colorActionCardTypes = [ColorActionCardFigure.ChangeDirection, ColorActionCardFigure.Stop, ColorActionCardFigure.Plus, ColorActionCardFigure.Taki];
			
			foreach(ColorActionCardFigure type in colorActionCardTypes) {
				foreach (CardColor color in cardColors) {
					for (int i = 1; i <= 2; i++) {
						cards.Add(new ColorCard.ColorActionCard {
							CardFigure = type,
							Color = color
						});
					}
				}
			}

			NeutralCardFigure[] neutralActionCardTypes = [NeutralCardFigure.King, NeutralCardFigure.SuperTaki];

			foreach(NeutralCardFigure type in neutralActionCardTypes) {
				for (int i = 1; i <= 2; i++) {
					cards.Add(new NeutralCard {
						CardFigure = type
					});
				}
			}

			for (int i = 1; i <= 4; i++) {
				cards.Add(new NeutralCard {
					CardFigure = NeutralCardFigure.ChangeColor
				});
			}

			return cards.ToArray();
		}
	}
}
