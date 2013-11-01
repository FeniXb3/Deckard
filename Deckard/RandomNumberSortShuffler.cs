using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class RandomNumberSortShuffler : IShuffler
    {
        public List<Card> Shuffle(Deck deck)
        {
            var sortedCards = deck.Cards.OrderBy(g => Guid.NewGuid());

            return sortedCards.ToList();
        }
    }
}
