namespace ConsoleTest {
	public abstract record TakiMove {
		public required int PlayerIndex { get; init; }
		
		public record DrawCard : TakiMove {}
		
		public abstract record PlayCard : TakiMove {
			public abstract TakiCard GetCard();

			public record PlaySimpleCard : PlayCard {
				private TakiCard _card;
				public required TakiCard Card {
					init {
						if(value is TakiCard.NeutralCard nAC && nAC.CardFigure == TakiCard.NeutralCard.NeutralCardFigure.ChangeColor) {
							throw new ArgumentException("The 'Change color' card must have the selected color as an argument.");
						}
						else {
							_card = value;
						}
					}
				}

				public override TakiCard GetCard() => _card;
			}

			public record PlayChangeColor : PlayCard {
				public override TakiCard GetCard() {
					return new TakiCard.NeutralCard() { CardFigure = TakiCard.NeutralCard.NeutralCardFigure.ChangeColor };
				}

				public required TakiCard.ColorCard.CardColor SelectedColor { get; init; }
			}
		}

		//Implement later
		//public record SayClosedTaki : TakiMove { }
	}
}
