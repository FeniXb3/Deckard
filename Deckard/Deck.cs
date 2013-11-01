using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deckard
{
    public class Deck
    {
        IShuffler Shuffler
        {
            set { shuffler = value; }
            get { return shuffler; }
        }
        private IShuffler shuffler;

        public Deck(IShuffler shuffler)
        {
            this.shuffler = shuffler;
        }

        public void Shuffle()
        {
            Shuffler.Shuffle(this);
        }
    }
}
