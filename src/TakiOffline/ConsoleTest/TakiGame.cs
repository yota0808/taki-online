using static ConsoleTest.TakiException;
using static ConsoleTest.TakiCard;
using static ConsoleTest.TakiCard.ColorCard;

namespace ConsoleTest {
	public class TakiGame {
		//Draw & discard piles
		private CardDeck<TakiCard> _drawPile;

		private TakiCard LeadingCard() => _discardPile.Last();
		private List<TakiCard> _discardPile;

		//Players
		private TakiPlayer CurrentPlayer() => _players[_currentPlayerIndex];
		private TakiPlayer[] _players;

		//Card effect flags
		private CardColor? _activeTaki = null;

		private bool _turnOrderReversed = false;

		private bool _stopActive = false;

		private bool Plus2Active() => _activePlus2Stacks > 0;
		private int _activePlus2Stacks = 0;
		private int _nextPlus2Stacks = 0;

		private CardColor? _activeChangeColor = null;

		private bool _kingTurn = false;

		//Misc flags
		private int _currentPlayerIndex = 0;

		public TakiGame(int playerCount) {
			_drawPile = new CardDeck<TakiCard>(TakiConstants.StandardDecklist());
			_drawPile.Shuffle();

			_players = new TakiPlayer[playerCount];
			for(int i = 0; i < playerCount; i++) {
				TakiPlayer player = new() { PlayerIndex = i };
				_players[i] = player;

				for (int k = 1; k <= 8; k++) {
					player.GiveCard(_drawPile.Draw());
				}
			}

			_discardPile = [];
			_discardPile.Add(_drawPile.Draw());
		}
		
		private void CurrentPlayerDraws1() {
			CurrentPlayer().GiveCard(_drawPile.Draw());
		}

		private void ThrowIfPlayerActedOutOfTurn(int actingPlayer) {
			if (actingPlayer != _currentPlayerIndex) {
				throw new InvalidTakiMoveException
					($"A player may only act on their turn. Player {actingPlayer} tried to act on player {_currentPlayerIndex}'s turn.");
			}
		}

		private void NextPlayer() {
			int i = _currentPlayerIndex;

			if (_turnOrderReversed) {
				do {
					if (i == 0) { i = _players.Length - 1; }
					else i--;
				}
				while (_players[i].HasWon == false);
			}
			else {
				do {
					if (i == _players.Length - 1) { i = 0; }
					else i++;
				}
				while (_players[i].HasWon == false);
			}

			_currentPlayerIndex = i;

			if (_stopActive) {
				_stopActive = false;
				NextPlayer();
			}
		}

		public void AcceptPlus2Draws(int actingPlayer) {
			ThrowIfPlayerActedOutOfTurn(actingPlayer);

			for (int i = 1; i <= _activePlus2Stacks; i++) {
				for (int k = 1; k <= 2; i++) {
					CurrentPlayerDraws1();
				}
			}

			_activePlus2Stacks = 0;
		}

		public void Draw(int actingPlayer) {
			ThrowIfPlayerActedOutOfTurn(actingPlayer);

			if (Plus2Active()) {
				throw new InvalidTakiMoveException("A player may not draw (through this method) while they have an active plus 2 on them.");
			}

			CurrentPlayerDraws1();

			if (_drawPile.Cards.Count == 0) {
				_drawPile = new CardDeck<TakiCard>(_discardPile);
				_discardPile.Clear();
				_drawPile.Shuffle();
			}
		}

		public void PlayCardNoParams(int actingPlayer, TakiCard card) {
			ThrowIfPlayerActedOutOfTurn(actingPlayer);
			
			if(!(card.IsFigure(NeutralActionCardFigure.SuperTaki) || card.IsFigure(NeutralActionCardFigure.ChangeColor) || card.IsFigure(NeutralActionCardFigure.King))
		}



		/*
		 * if (move.PlayerIndex != _currentPlayerIndex) {
				throw new InvalidTakiMoveException
					($"A player may only act on their turn. Player {move.PlayerIndex} tried to act on player {_currentPlayerIndex}'s turn.");
			}
		*/
	}
}
