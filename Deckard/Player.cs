using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Player
    {
        public Card DrawnCard;

        public void DrawFrom(Deck deck)
        {
            DrawnCard = deck.Top.DeepCopy();
            deck.Cards.Remove(deck.Top);
        }
    }
}
