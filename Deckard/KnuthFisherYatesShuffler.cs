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

        public List<Card> Shuffle(Deck deck)
        {
            Random R = new Random((int)DateTime.Now.Ticks);

            List<Card> newCardsOrder = deck.Cards.ToList();

            for (int i = 0; i < newCardsOrder.Count; i++)
            {
                Swap(newCardsOrder, i, i + R.Next(newCardsOrder.Count - i));
            }

            return newCardsOrder;
        }
    }
}
