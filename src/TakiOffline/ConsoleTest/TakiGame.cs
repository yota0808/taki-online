using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest {
	public class TakiGame {
		public enum DirectionOfPlayType {
			Normal, Reverse
		}

		private DirectionOfPlayType _directionOfPlay;

		private CardDeck<TakiCard> _drawPile;

		private TakiPlayer[] _players;

		public TakiGame(int playerCount) {
			_drawPile = new CardDeck<TakiCard>(TakiConstants.StandardDecklist());
			_drawPile.Shuffle();

			_players = new TakiPlayer[playerCount];
			for(int i = 0; i < playerCount; i++) {
				TakiPlayer player = new TakiPlayer() { PlayerID = i };
				_players[i] = player;

				for (int k = 1; k <= 8; k++) {
					TakiCard dealtCard = _drawPile.Draw();

				}
			}
		}

		public void MakeMove(TakiMove move) {
			switch (move) {
				 
			}
		}
	}
}
