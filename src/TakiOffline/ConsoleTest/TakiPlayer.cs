﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest {
	public class TakiPlayer {
		public readonly List<TakiCard> Hand;
		public required int PlayerID { get; init; }
		public bool HasWon { get; private set; }

		public TakiPlayer() {
			Hand = [];
			HasWon = false;
		}

		public void MarkAsWon() {
			HasWon = true;
		}

		public void GiveCard(TakiCard card) {
			Hand.
		}
	}
}
