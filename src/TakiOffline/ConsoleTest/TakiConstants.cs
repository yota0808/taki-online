using static ConsoleTest.Utilities;
using static ConsoleTest.TakiCard;

namespace ConsoleTest {
	public static class TakiConstants {
		public static TakiCard[] StandardDecklist() {
			List<TakiCard> cards = [];

			//2 of each color of each color card

			CardColor[] cardColors = Enum.GetValues<CardColor>();
			ColorCardFigure[] colorCardFigures = Enum.GetValues<ColorCardFigure>();

			foreach(ColorCardFigure figure in colorCardFigures) {
				foreach(CardColor color in cardColors) {
					for(int i = 1; i <= 2; i++) {
						cards.Add(new ColorCard {
							Color = color,
							Figure = figure
						});
					}
				}
			}

			//2 kings and super takis

			foreach(NeutralCardFigure figure in new NeutralCardFigure[] {NeutralCardFigure.King, NeutralCardFigure.SuperTaki } ) {
				for(int i = 1; i <= 2; i++) {
					cards.Add(new NeutralCard { CardFigure = figure });
				}
			}

			//4 "change color"s

			for (int i = 1; i <= 4; i++) {
				cards.Add(new NeutralCard {
					CardFigure = NeutralCardFigure.ChangeColor
				});
			}

			return cards.ToArray();
		}
	}
}
