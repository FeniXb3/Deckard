using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public interface IShuffler
    {
        void Shuffle(Deck deck);
    }
}
