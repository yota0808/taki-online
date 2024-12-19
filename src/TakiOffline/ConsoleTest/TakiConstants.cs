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

			NeutralActionCardFigure[] neutralActionCardTypes = [NeutralActionCardFigure.King, NeutralActionCardFigure.SuperTaki];

			foreach(NeutralActionCardFigure type in neutralActionCardTypes) {
				for (int i = 1; i <= 2; i++) {
					cards.Add(new NeutralActionCard {
						CardFigure = type
					});
				}
			}

			for (int i = 1; i <= 4; i++) {
				cards.Add(new NeutralActionCard {
					CardFigure = NeutralActionCardFigure.ChangeColor
				});
			}

			return cards.ToArray();
		}
	}
}
