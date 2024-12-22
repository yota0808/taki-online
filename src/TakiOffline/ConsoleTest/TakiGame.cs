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

		private bool _reverseTurnOrder = false;
		private bool _nextPlayerIsStopped = false;
		private bool _kingTurn = false;
		private bool _takiIsActive = false;

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

		private void NextPlayer() {
			int i = _currentPlayerIndex;

			if (_reverseTurnOrder) {
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

			if (_nextPlayerIsStopped) {
				_nextPlayerIsStopped = false;
				NextPlayer();
			}
		}

		public void MakeMove(TakiMove move) {
			if(move.PlayerIndex != _currentPlayerIndex) {
				throw new InvalidTakiMoveException
					($"A player may only act on their turn. Player {move.PlayerIndex} tried to act on player {_currentPlayerIndex}'s turn.");
			}

			switch (move) {
				case TakiMove.DrawCard:
					Draw();
					break;
				case TakiMove.PlayCard.PlayCardNoParamaters pSC:
					PlaySimpleCard(pSC.GetCard());
					break;
			}
		}

		private void Draw() {
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

		private void PlaySimpleCard(TakiCard card) {
			if (!card.IsValidPlayOn(_leadingCard)) {
				throw new InvalidTakiMoveException
					($"The card isn't playable on the leading card.");
			}
			
			if(card is TakiCard.ColorCard) {
				TakiCard.ColorCard cC = card as TakiCard.ColorCard;

				//Numbers (which aren't plus 2) don't do anything special

				bool _dontEndTurn = false;
				switch (cC.Figure) {
					case TakiCard.ColorCardFigure.ChangeDirection:
						_reverseTurnOrder = !_reverseTurnOrder;
						break;
					case TakiCard.ColorCardFigure.Stop:
						_nextPlayerIsStopped = true;
						break;
					case TakiCard.ColorCardFigure.Plus:
						_dontEndTurn = true;
						break;
					case TakiCard.ColorCardFigure.Taki:

				}
			}

			
		}
	}
}
