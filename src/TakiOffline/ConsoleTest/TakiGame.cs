using static ConsoleTest.TakiException;
using static ConsoleTest.TakiCard;
using static ConsoleTest.TakiCard.ColorCard;

using Color = ConsoleTest.TakiCard.CardColor;
using ColAc_F = ConsoleTest.TakiCard.ColorActionCardFigure;
using NeuAc_F = ConsoleTest.TakiCard.NeutralActionCardFigure;
using Num_F = ConsoleTest.TakiCard.NumberCardFigure;

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
		private Color? _activeTaki = null;

		private bool _turnOrderReversed = false;

		private bool _stopActive = false;

		private bool Plus2IsActive() => _activePlus2Stacks > 0;
		private int _activePlus2Stacks = 0;
		private int _nextPlus2Stacks = 0;

		private Color? _activeChangeColor = null;

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

		private void ThrowInvalidCardPlay() {
			throw new InvalidTakiMoveException("That card can't be played right now.");
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

			if (Plus2IsActive()) {
				throw new InvalidTakiMoveException("A player may not draw (through this method) while they have an active plus 2 on them.");
			}

			CurrentPlayerDraws1();

			if (_drawPile.Cards.Count == 0) {
				_drawPile = new CardDeck<TakiCard>(_discardPile);
				_discardPile.Clear();
				_drawPile.Shuffle();
			}
		}

		/*
		Taki rule FAQs I want to implement, by their titles in the website
		(These are complicated and annoying, so I'm saving them for after I've got the basic rules down)
		"Do I win if I play +2 as a last card?"
		"Can I end with a + card as a last card?"

		! "Can I play a Change Color at the end of a TAKI run?" (Force color to be that of the taki)
		! "If I play the King card and play a Super-TAKI on top of it, can I choose the color of the TAKI?" (Have the player choose the color like with change color)
		*/

		public void PlayCard(int actingPlayer, TakiCard card) {
			ThrowIfPlayerActedOutOfTurn(actingPlayer);

			if (!CurrentPlayer().HasCard(card)) {
				throw new InvalidTakiMoveException($"The current player can't play a [PLACEHOLDER] because they don't have one.");
			}

			TakiCard leadingCard = LeadingCard();

			//PLAY CONDITIONS

			//(I could have foregone the active taki check, since the color of the lead must be
			//the color of the active taki if one exists. But this is much more readable.)
			bool KingOrColorOrSymbolCheck() {
				//Kings can always be played on
				if (leadingCard.IsFigure(NeuAc_F.King)) { return true; }

				//Check if figures match
				if (card.IsFigure(leadingCard)) { return true; }

				ColorCard colorCard = (ColorCard)card;
				//Check if there's a matching taki active
				if (_activeTaki == colorCard.Color) { return true; }
				//Check if a matching change color is on top
				if (_activeChangeColor == colorCard.Color) { return true; }

				//Either of the next two failing means there's no color match. this plus no symbol match means not playable

				//If there's *a* "change color" on top, given the previous "change color" check, it *must* be non-matching
				if (_activeChangeColor is not null) { return false; }
				//If there's *a* taki active, given the previous taki check, the card on top must be of a non matching color
				//(Either part of the taki or the taki card itself)
				if(_activeTaki is not null) { return false; }

				//If lead is neither a king (checked directly), nor change color (otherwise we would have returned already),
				//nor super taki (same thing), then by elimination it *must* be a ColorCard. So it's safe to cast and check.
				ColorCard leadingColorCard = (ColorCard)leadingCard;
				if(colorCard.Color == leadingColorCard.Color) { return true; }

				return false;
			}

			//These need a king as lead, a matching color or symbol, or an active taki with matching color.
			//Also, you can't play them when a plus 2 is on you.
			if (card is NumberCard || card.IsFigure(ColAc_F.Taki, ColAc_F.ChangeDirection, ColAc_F.Stop, ColAc_F.Plus)) {
				if (!KingOrColorOrSymbolCheck() || Plus2IsActive()) { ThrowInvalidCardPlay(); }
			}
			//A plus 2 is identical, except that it *can* be played on another plus 2.
			else if (card.IsFigure(ColAc_F.Plus2)) {
				if (!KingOrColorOrSymbolCheck()) { ThrowInvalidCardPlay(); }
			}
			//And a super taki is neutral, so it doesn't care about colors or symbols.
			//But it also isn't a plus 2, so it can't be played on an active one.
			//However, if one is played on a king, the player must *choose* the color of the taki instead of it being determined by the lead.
			//In that case, this method is inadequate. There is no "chosen color" paramater. So this throws.
			else if (card.IsFigure(NeuAc_F.SuperTaki)) {
				if (Plus2IsActive()) { ThrowInvalidCardPlay(); }
				if (leadingCard.IsFigure(NeuAc_F.King)) {
					throw new ArgumentException
						("A super taki *can* be played here (on a king), but no 'chosen color' paramater was provided. Use PlayCardWithColorChoice.");
				}
			}
			//A "change color" has an identical play condition in terms of rules, but there's another paramater problem like with the super taki.
			//You can only play a "change color" here becuase I want to support the rule of
			//it *automatically* (without player choice) changing its color to that of the active taki, if one exists.
			//But if there *isn't* an active taki, we once again need the chosen color paramater. So this throws.
			else if (card.IsFigure(NeuAc_F.ChangeColor)) {
				if (Plus2IsActive()) { ThrowInvalidCardPlay(); }
				if(_activeTaki is null) {
					throw new ArgumentException
						("A 'change color' *can* be played here (outside a taki run), but no 'chosen color' paramater was provided. Use PlayCardWithColorChoice.");
				}
			}
			//If all of the above "if"s didn't trigger, the card must be a king, and a king is always playable

			//GAME EFFECTS

			if(!card.IsFigure(NeuAc_F.SuperTaki, NeuAc_F.ChangeColor, NeuAc_F.King) && !card.IsFigure(ColAc_F.Taki)) {
				if(((ColorCard)card).Color != _activeTaki) {
					_activeTaki = null;
				}
			}

			if(card.IsFigure(NeuAc_F.ChangeColor, NeuAc_F.King)) {
				_activeTaki = null;
			}

			if (card.IsFigure(ColAc_F.Taki)) {
				_activeTaki = ((ColorCard)card).Color;
			}

			if (card.IsFigure(NeuAc_F.SuperTaki)) {
				_activeTaki = _activeChangeColor is not null ? 
			}
		}



		/*
		 * if (move.PlayerIndex != _currentPlayerIndex) {
				throw new InvalidTakiMoveException
					($"A player may only act on their turn. Player {move.PlayerIndex} tried to act on player {_currentPlayerIndex}'s turn.");
			}
		*/
	}
}
