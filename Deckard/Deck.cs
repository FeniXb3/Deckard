using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Deck
    {
        IShuffler Shuffler;

        public void Shuffle()
        {
            Shuffler.Shuffle(this);
        }
    }
}
