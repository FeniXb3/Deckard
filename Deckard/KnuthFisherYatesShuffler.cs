using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard.Specs
{
    public class KnuthFisherYatesShuffler : IShuffler
    {
        static void Swap(IList<Card> list, int indexA, int indexB)
        {
            Card tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        public void Shuffle(Deck deck)
        {
            Random R = new Random((int)DateTime.Now.Ticks);
            
            for (int i = 0; i < deck.Cards.Count; i++)
            {
                Swap(deck.Cards, i, i + R.Next(deck.Cards.Count - i));
            }
        }
    }
}
