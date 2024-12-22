using static ConsoleTest.TakiCard;

namespace ConsoleTest {
	public record TakiState {
		private CardDeck<TakiCard> _drawPile;
		private List<TakiCard> _discardPile;
		private TakiPlayer[] _players;
	}
}
