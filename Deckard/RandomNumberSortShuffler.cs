using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class RandomNumberSortShuffler : IShuffler
    {
        public void Shuffle(Deck deck)
        {
            deck.Cards = deck.Cards.OrderBy(g => Guid.NewGuid()).ToList();
        }
    }
}
