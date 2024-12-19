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

			ColorActionCardType[] colorActionCardTypes = [ColorActionCardType.ChangeDirection, ColorActionCardType.Stop, ColorActionCardType.Plus, ColorActionCardType.Taki];
			
			foreach(ColorActionCardType type in colorActionCardTypes) {
				foreach (CardColor color in cardColors) {
					for (int i = 1; i <= 2; i++) {
						cards.Add(new ColorCard.ColorActionCard {
							CardType = type,
							Color = color
						});
					}
				}
			}

			NeutralActionCardType[] neutralActionCardTypes = [NeutralActionCardType.King, NeutralActionCardType.SuperTaki];

			foreach(NeutralActionCardType type in neutralActionCardTypes) {
				for (int i = 1; i <= 2; i++) {
					cards.Add(new NeutralActionCard {
						CardType = type
					});
				}
			}

			for (int i = 1; i <= 4; i++) {
				cards.Add(new NeutralActionCard {
					CardType = NeutralActionCardType.ChangeColor
				});
			}

			return cards.ToArray();
		}
	}
}
