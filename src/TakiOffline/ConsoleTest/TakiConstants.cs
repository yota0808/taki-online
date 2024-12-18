using System.Drawing;
using static ConsoleTest.TakiCard;

namespace ConsoleTest {
	public static class TakiConstants {
		public static TakiCard[] StandardDeck() {
			List<TakiCard> deck = [];

			CardColor[] cardColors = Enum.GetValues<CardColor>();
			CardNumber[] cardNumbers = Enum.GetValues<CardNumber>();

			foreach(CardNumber number in cardNumbers) {
				foreach(CardColor color in cardColors) {
					for(int i = 1; i <= 2; i++) {
						deck.Add(new ColorCard.NumberCard {
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
						deck.Add(new ColorCard.ColorActionCard {
							CardType = type,
							Color = color
						});
					}
				}
			}

			NeutralActionCardType[] neutralActionCardTypes = [NeutralActionCardType.King, NeutralActionCardType.SuperTaki];

			foreach(NeutralActionCardType type in neutralActionCardTypes) {
				for (int i = 1; i <= 2; i++) {
					deck.Add(new NeutralActionCard {
						CardType = type
					});
				}
			}

			for (int i = 1; i <= 4; i++) {
				deck.Add(new NeutralActionCard {
					CardType = NeutralActionCardType.ChangeColor
				});
			}

			return deck.ToArray();

			//TODO : use the Fisher-Yates algorithm to shuffle this
		}
	}
}
