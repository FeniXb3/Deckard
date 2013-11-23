﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Player
    {
        public Card CardInHand;

        public void DrawFrom(Deck deck, int cardIndex = -1)
        {
            CardInHand = deck.TakeAndRemoveCard(cardIndex);
            CardInHand.OnDrawn(this, EventArgs.Empty);
        }

        public void PutCardIn(Deck deck)
        {
            deck.Cards.Add(CardInHand.DeepCopy());
            CardInHand = null;
        }

        public Deck Hand { get; set; }
    }
}
