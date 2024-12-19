using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleTest.TakiException;

namespace ConsoleTest {
	public class TakiGame {
		private int _extraDraws = 0;

		public enum DirectionOfPlayType {
			Normal, Reverse
		}

		private DirectionOfPlayType _directionOfPlay;

		//In case of super taki or change color.
		private TakiCard.CardColor? _colorOverride = null;

		private CardDeck<TakiCard> _drawPile;
		private List<TakiCard> _discardPile;

		private TakiCard _leadingCard => _discardPile.Last();
		private TakiPlayer _currentPlayer => _players[_currentPlayerIndex];

		private TakiPlayer[] _players;
		private int _currentPlayerIndex = 0;

		public TakiGame(int playerCount) {
			_drawPile = new CardDeck<TakiCard>(TakiConstants.StandardDecklist());
			_drawPile.Shuffle();

			_players = new TakiPlayer[playerCount];
			for(int i = 0; i < playerCount; i++) {
				TakiPlayer player = new TakiPlayer() { PlayerIndex = i };
				_players[i] = player;

				for (int k = 1; k <= 8; k++) {
					player.GiveCard(_drawPile.Draw());
				}
			}

			_discardPile = [];
			_discardPile.Add(_drawPile.Draw());
		}

		public void NextPlayer() {
			int i = _currentPlayerIndex;
			do {
				if (i == _players.Length - 1) { i = 1; }
				else i++;
			}
			while (_players[i].HasWon == false);

			_currentPlayerIndex = i;
		}

		public void MakeMove(TakiMove move) {
			if(move.PlayerIndex != _currentPlayerIndex) {
				throw new InvalidTakiMoveException
					($"A player may only act on their turn. Player {move.PlayerIndex} tried to act on player {_currentPlayerIndex}'s turn.");
			}

			switch (move) {
				case TakiMove.DrawCard:
					HandleDraw();
					break;
				case TakiMove.PlayCard.PlaySimpleCard pSC:
					HandlePlaySimpleCard(pSC.GetCard());
					break;
			}
		}

		private void HandleDraw() {
			if (_extraDraws > 0) {
				_extraDraws--;
			}
			else _currentPlayer.GiveCard(_drawPile.Draw());
			if (_drawPile.Cards.Count == 0) {
				_drawPile = new CardDeck<TakiCard>(_discardPile);
				_discardPile.Clear();
				_drawPile.Shuffle();
			}
		}

		private void HandlePlaySimpleCard(TakiCard card) {
			if (!card.IsValidPlayOn(_leadingCard)) {
				throw new TakiException.InvalidTakiMoveException
					($"The card isn't playable on the leading card.");
			}

			switch (card) {
				case TakiCard.ColorCard.NumberCard nC:
			}
		}
	}
}
