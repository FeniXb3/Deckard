using System.Collections.Generic;

namespace Deckard
{
    public interface IShuffler
    {
        List<Card> Shuffle(Deck deck);
    }
}
